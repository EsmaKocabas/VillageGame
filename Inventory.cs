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
        private AVLTree<Item> itemTree; // AVL ağacı ile sıralama için
        private const int maxCapacity = 10; // Envanter kapasitesi
        public List<Item> Items { get; set; } // Envanterdeki öğeler

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
            Capacity = capacity;
        }

        // PUSH: Envantere öğe ekle
        public void Push(Item item)
        {
            if (GetItemCount() >= Capacity)
            {
                Console.WriteLine("Envanter dolu, öğe eklenemedi.");
                return;
            }
            if (item != null)
            {
                itemTree.Insert(item);
                Items.Add(item);
                Console.WriteLine($"{item.name} envantere eklendi (push).");
            }
            else
            {
                Console.WriteLine("Geçersiz öğe.");
            }
        }

        // POP: Envanterden son eklenen öğeyi çıkar
        public Item? Pop()
        {
            if (Items.Count == 0)
            {
                Console.WriteLine("Envanter boş. Çıkarılacak eşya yok.");
                return null;
            }
            Item item = Items[Items.Count - 1];
            itemTree.Delete(item);
            Items.RemoveAt(Items.Count - 1);
            Console.WriteLine($"{item.name} envanterden çıkarıldı (pop).");
            return item;
        }

        public Item? RemoveItem(string itemName)
        {
            var item = Items.LastOrDefault(i => i.name == itemName);
            if (item != null)
            {
                itemTree.Delete(item);
                Items.Remove(item);
                Console.WriteLine($"{itemName} envanterden çıkarıldı.");
                return item;
            }
            Console.WriteLine($"{itemName} envanterde bulunamadı.");
            return null;
        }

        public void SearchItem(string itemName)
        {
            var item = itemTree.Search(new Item(itemName, "", 0));
            if (item != null)
            {
                Console.WriteLine($"{item.name} bulundu: {item.description}");
            }
            else
            {
                Console.WriteLine($"{itemName} bulunamadı.");
            }
        }

        public void DisplayItems()
        {
            List<Item> items = itemTree.InOrder();
            Console.WriteLine("Envanterdeki eşyalar:");
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
        }

        public bool UseItem(string itemName)
        {
            var item = Items.LastOrDefault(i => i.name.Equals(itemName, StringComparison.OrdinalIgnoreCase));
            if (item != null)
            {
                itemTree.Delete(item);
                Items.Remove(item);
                Console.WriteLine($"{item.name} kullanıldı ve envanterden çıkarıldı.");
                return true;
            }
            else
            {
                Console.WriteLine($"{itemName} envanterde bulunamadı, kullanılamadı.");
                return false;
            }
        }
        public void ListItems()
        {
            var items = itemTree.InOrder();
            Console.WriteLine("Envanterdeki öğeler (alfabetik sıra):");
            foreach (var item in items)
            {
                Console.WriteLine("- " + item.name);
            }
        }
        
        public int GetItemCount()
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
            Console.WriteLine($"Envanter dolu olduğu için {oldest.name} otomatik olarak silindi.");
        }
    }
}