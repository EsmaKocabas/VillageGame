using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Village_Game;

namespace Village_Game
{
    public class Inventory
    {
        private AVLTree<Item> itemTree; 
        private const int maxCapacity = 10; 
        public List<Item> Items { get; set; }
        public int Capacity;

        public Inventory()
        {
            itemTree = new AVLTree<Item>();
            Items = new List<Item>();
            Capacity = maxCapacity;
        }

        public Inventory(int capacity)
        {
            itemTree = new AVLTree<Item>();
            Items = new List<Item>();
            this.Capacity = capacity;
        }

       
        public void Push(Item item)
        {
            if (ItemCount() >= Capacity)
            {
                Console.WriteLine("Çantada yer yok. Eşya eklenemedi");
                return;
            }
            if (item != null)
            {
                itemTree.Insert(item); //avl ağacına ekleme
                Items.Add(item);
                Console.WriteLine(item.name + " envantere eklendi.");
            }
            else
            {
                Console.WriteLine("Geçersiz öğe.");
            }
        }

        
        public Item? Pop()
        {
            if (Items.Count == 0)
            {
                Console.WriteLine("Çantada eşya yok. Çıkarılacak eşya yok.");
                return null;
            }
            Item item = Items[Items.Count - 1];
            itemTree.Delete(item);
            Items.RemoveAt(Items.Count - 1);
            Console.WriteLine(item.name+" envanterden çıkarıldı.");
            return item; //çıkarılan eşyayı döndurur
        }

        public Item? RemoveItem(string itemName)
        {
            Item? itemToRemove=null;
            foreach (var item in Items)
            {
                if (item.name==itemName)
                {
                    itemToRemove = item;
                    
                }
            }
            if (itemToRemove != null)
            {
                itemTree.Delete(itemToRemove);
                Items.Remove(itemToRemove); //esya cıkarılır
                Console.WriteLine(itemToRemove.name + " envanterden çıkarıldı."); 
                return itemToRemove;
            }
            else
            {
                Console.WriteLine(itemName+" envanterde bulunamadı.");
                return null;
            }
        }

        public void SearchItem(string itemName)
        {
            var item = itemTree.Search(new Item(itemName, "", 0)); //avl agacında search arama yapar
            if (item != null)
            {
                Console.WriteLine(item.name+" bulundu: " +item.description);
            }
            else
            {
                Console.WriteLine(itemName+" bulunamadı.");
            }
        }

        public void DisplayItems()
        {
            List<Item> items = itemTree.InOrder(); //ogeleri alır
            Console.WriteLine("Envanterdeki eşyalar:");
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
        }

        public bool UseItem(string itemName)
        {
            Item? itemToUse = null;
            foreach (var item in Items)
            {
                if (item.name == itemName)
                {
                    itemToUse = item;
                    break;
                }
            }
            if (itemToUse != null)
            {
                itemTree.Delete(itemToUse);
                Items.Remove(itemToUse);
                Console.WriteLine(itemToUse.name+" kullanıldı ve envanterden çıkarıldı.");
                return true;
            }
            else
            {
                Console.WriteLine(itemName+" envanterde bulunamadığı için kullanılamadı.");
                return false;
            }
        }
        public void ListItems()
        {
            var items = itemTree.InOrder();
            Console.WriteLine("Envanterdeki öğeler (alfabetik sıra):");
            foreach (var item in items)
            {
                Console.WriteLine(" - " + item.name);
            }
        }
        
        public int ItemCount()
        {
            return itemTree.InOrder().Count;
        }

        public void RemoveOldestItem()
        {
            if (Items.Count == 0)
            {
                Console.WriteLine("Envanterde silinecek eşya yok.");
                return;
            }
            Item oldest = Items[0];
            itemTree.Delete(oldest);
            Items.RemoveAt(0);
            Console.WriteLine("Envanter dolu olduğu için " + oldest.name + " silindi.");
        }
    }
}