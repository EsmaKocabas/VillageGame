using System;
using System.Collections.Generic;

namespace Village_Game
{
    public class AVLNode<T> where T : IComparable<T>
    {
        public T value;
        public AVLNode<T> Left;
        public AVLNode<T> Right;
        public int Height;

        public AVLNode(T value)
        {
            this.value = value;
            Height = 1;
        }
    }

    public class AVLTree<T> where T : IComparable<T>
    {
        private AVLNode<T> root;

        public void Insert(T value)
        {
            root = Insert(root, value);
        }

        private AVLNode<T> Insert(AVLNode<T> node, T value)
        {
            if (node == null)
                return new AVLNode<T>(value);

            int compare = value.CompareTo(node.value);
            if (compare < 0)
                node.Left = Insert(node.Left, value);
            else if (compare > 0)
                node.Right = Insert(node.Right, value);
            else
                return node; 

            node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
            return Balance(node);
        }

        public void Delete(T value)
        {
            root = Delete(root, value);
        }

        private AVLNode<T> Delete(AVLNode<T> node, T value)
        {
            if (node == null)
                return null;

            int compare = value.CompareTo(node.value);
            if (compare < 0)
                node.Left = Delete(node.Left, value);
            else if (compare > 0)
                node.Right = Delete(node.Right, value);
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
                    node.value = temp.value;
                    node.Right = Delete(node.Right, temp.value);
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

        public T Search(T value)
        {
            var node = SearchNode(root, value);
            return node != null ? node.value : default;
        }

        private AVLNode<T> SearchNode(AVLNode<T> node, T value)
        {
            if (node == null)
                return null;

            int compare = value.CompareTo(node.value);
            if (compare == 0)
                return node;
            else if (compare < 0)
                return SearchNode(node.Left, value);
            else
                return SearchNode(node.Right, value);
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
            list.Add(node.value);
            InOrderTraversal(node.Right, list);
        }

        private int GetHeight(AVLNode<T> node)
        {
            if (node == null)
                return 0;
            return node.Height;
            
        }

        private int GetBalance(AVLNode<T> node)
        {
            if (node == null)
                return 0;
            return GetHeight(node.Left) - GetHeight(node.Right);
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