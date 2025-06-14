using System;
using System.Collections.Generic;

namespace Village_Game
{
    public class AVLNode<T> where T : IComparable<T>
    {
        public T Data;
        public AVLNode<T> Left;
        public AVLNode<T> Right;
        public int Height;

        public AVLNode(T data)
        {
            Data = data;
            Height = 1;
        }
    }

    public class AVLTree<T> where T : IComparable<T>
    {
        private AVLNode<T> root;

        public void Insert(T data)
        {
            root = Insert(root, data);
        }

        private AVLNode<T> Insert(AVLNode<T> node, T data)
        {
            if (node == null)
                return new AVLNode<T>(data);

            int compare = data.CompareTo(node.Data);
            if (compare < 0)
                node.Left = Insert(node.Left, data);
            else if (compare > 0)
                node.Right = Insert(node.Right, data);
            else
                return node; // aynı veri eklenmez

            node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
            return Balance(node);
        }

        public void Delete(T data)
        {
            root = Delete(root, data);
        }

        private AVLNode<T> Delete(AVLNode<T> node, T data)
        {
            if (node == null)
                return null;

            int compare = data.CompareTo(node.Data);
            if (compare < 0)
                node.Left = Delete(node.Left, data);
            else if (compare > 0)
                node.Right = Delete(node.Right, data);
            else
            {
                if (node.Left == null || node.Right == null)
                {
                    AVLNode<T> temp = node.Left ?? node.Right;
                    if (temp == null)
                        return null;
                    else
                        node = temp;
                }
                else
                {
                    AVLNode<T> temp = GetMinValueNode(node.Right);
                    node.Data = temp.Data;
                    node.Right = Delete(node.Right, temp.Data);
                }
            }

            node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
            return Balance(node);
        }

        private AVLNode<T> GetMinValueNode(AVLNode<T> node)
        {
            AVLNode<T> current = node;
            while (current.Left != null)
                current = current.Left;
            return current;
        }

        public T Search(T data)
        {
            var node = SearchNode(root, data);
            return node != null ? node.Data : default;
        }

        private AVLNode<T> SearchNode(AVLNode<T> node, T data)
        {
            if (node == null)
                return null;

            int compare = data.CompareTo(node.Data);
            if (compare == 0)
                return node;
            else if (compare < 0)
                return SearchNode(node.Left, data);
            else
                return SearchNode(node.Right, data);
        }

        public List<T> InOrder()
        {
            var result = new List<T>();
            InOrderTraversal(root, result);
            return result;
        }

        private void InOrderTraversal(AVLNode<T> node, List<T> list)
        {
            if (node == null) return;
            InOrderTraversal(node.Left, list);
            list.Add(node.Data);
            InOrderTraversal(node.Right, list);
        }

        private int GetHeight(AVLNode<T> node)
        {
            return node?.Height ?? 0;
        }

        private int GetBalance(AVLNode<T> node)
        {
            return node == null ? 0 : GetHeight(node.Left) - GetHeight(node.Right);
        }

        private AVLNode<T> Balance(AVLNode<T> node)
        {
            int balance = GetBalance(node);

            if (balance > 1 && GetBalance(node.Left) >= 0)
                return RotateRight(node);

            if (balance > 1 && GetBalance(node.Left) < 0)
            {
                node.Left = RotateLeft(node.Left);
                return RotateRight(node);
            }

            if (balance < -1 && GetBalance(node.Right) <= 0)
                return RotateLeft(node);

            if (balance < -1 && GetBalance(node.Right) > 0)
            {
                node.Right = RotateRight(node.Right);
                return RotateLeft(node);
            }

            return node;
        }

        private AVLNode<T> RotateLeft(AVLNode<T> x)
        {
            var y = x.Right;
            var T2 = y.Left;

            y.Left = x;
            x.Right = T2;

            x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;
            y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;

            return y;
        }

        private AVLNode<T> RotateRight(AVLNode<T> y)
        {
            var x = y.Left;
            var T2 = x.Right;

            x.Right = y;
            y.Left = T2;

            y.Height = Math.Max(GetHeight(y.Left), GetHeight(y.Right)) + 1;
            x.Height = Math.Max(GetHeight(x.Left), GetHeight(x.Right)) + 1;

            return x;
        }
    }
}