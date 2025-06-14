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
        public Inventory()
        {
            itemTree = new AVLTree<Item>();
        }
        public void AddItem(Item item) // Envantere öğe ekleme
        {
            var currentItems = itemTree.InOrder();
            if (currentItems.Count >=maxCapacity)
                {
                    Console.WriteLine("Envanter dolu, öğe eklenemedi.");
                    return;
                }
                if (item != null)
                {
                    itemTree.Insert(item);
                    Console.WriteLine($"{item.name} envantere eklendi.");
                }
                else
                {
                    Console.WriteLine("Geçersiz öğe.");
                }
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
        
        public Item? RemoveItem(string itemName)
        {
            Item itemToRemove = itemTree.Search(new Item(itemName, "", 0));
            if (itemToRemove != null)
            {
                itemTree.Delete(itemToRemove);
                Console.WriteLine($"{itemName} envanterden çıkarıldı.");
                return itemToRemove;
            }
            else
            {
                Console.WriteLine($"{itemName} envanterde bulunamadı.");
                return null;
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


        public List<Item> Items;
        public int Capacity;

        public Inventory(int capacity)
        {
            Items = new List<Item>();
            Capacity = capacity;
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
            return Items.Count;
        }

        // public void ShowInventory()
        // {
        //     if (Items.Count == 0)
        //     {
        //         Console.WriteLine("Envanter boş.");
        //     }
        //     else
        //     {
        //         foreach (var item in Items)
        //         {
        //             Console.WriteLine("- " + item.name);
        //         }
        //     }
        // }
    }
}




       
      

       
