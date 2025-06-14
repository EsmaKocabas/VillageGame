using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Village_Game
{


    public class VillageGameManager
    {
        public Village currentVillage;
        public Inventory inventory;
        public ArrayList rescuedVillages = new ArrayList(); // Kurtarılan köylerin listesi
        public Queue<Village> villageQueue = new Queue<Village>(); // Köylerin kuyruğu

        public VillageGameManager()
        {
            villageQueue = new Queue<Village>(); // Köy kuyruğu
            rescuedVillages = new ArrayList();
            Inventory inventory = new Inventory(10); // Envanterin maksimum kapasitesi 10
            currentVillage = new Village ("Başlangıç Köyü", new ArrayList()); // Başlangıç köyü
            StartGame();
        }


        public void StartGame()
        {
            //Eşya olusturma
            Hashtable items = new Hashtable();
            items.Add("Kılıç", new Item("Kılıç", "Güçlü kılıç", 90));
            items.Add("Bıcak", new Item("Bıçak", "Keskin bıçak", 50));
            items.Add("Yiyecek", new Item("Yiyecek", "Ev yemeği", 30));
            items.Add("Balta", new Item("Balta", "Balta", 70));
            items.Add("Kalkan", new Item("Kalkan", "Savunma kalkanı", 50));
            items.Add("Altın", new Item("Altın", "Değerli altın", 10));
            items.Add("Harita", new Item("Harita", "Köylerin haritası", 10));
            items.Add("İksir", new Item("İksir", "İyileştirme", 60));

            // Köyleri olusturma
            Village[] village = new Village[7];
            village[0] = new Village("Orman Köyü", new ArrayList { items["Kılıç"], items["Yiyecek"], items["Altın"] });
            village[1] = new Village("Dağ Köyü", new ArrayList { items["Bıçak"], items["Balta"], items["Kalkan"] });
            village[2] = new Village("Yayla Köyü", new ArrayList { items["Yiyecek"], items["Altın"], items["Harita"] });
            village[3] = new Village("Deniz Köyü", new ArrayList { items["Kılıç"], items["İksir"], items["Altın"] });
            village[4] = new Village("Y Köyü", new ArrayList { items["Bıçak"], items["Yiyecek"], items["Harita"] });
            village[5] = new Village("Z Köyü", new ArrayList { items["Balta"], items["Kalkan"], items["İksir"] });
            village[6] = new Village("T Köyü", new ArrayList { items["Kılıç"], items["Bıçak"], items["Yiyecek"] });


            // Köyleri kuyruğa ekle
            for (int i = 0; i < village.Length; i++)
                villageQueue.Enqueue(village[i]);

            if(villageQueue.Count>0 && villageQueue.Peek()is Village vill){
                currentVillage = vill; // ilk köyü seç
            }
            else
            {
                throw new InvalidOperationException("Köy kuyruğu boş");

            }
        } 
           public Village GetCurrentVillage()
            {
                if (currentVillage == null)
                {
                    throw new InvalidOperationException("Köy seçilmedi");
                }
                
                return currentVillage;
            }

            // Kurtarılan köyleri döndür(boş)
            public ArrayList GetRescuedVillages()
            {
    
            return rescuedVillages;
            }

            // Kalan köyleri döndür
            public ArrayList GetRemainingVillages()
            {
            ArrayList remaining = new ArrayList();
                foreach (Village v in villageQueue)
                {
                    remaining.Add(v.name);
                }
                return remaining;
            }

            // Envanteri göster
            public void DisplayItems()
            {
                inventory.DisplayItems();
            }

            // Envantere eşya ekle
            public bool AddItemToInventory(Item item)
            {
                if (inventory == null)
                {
                    throw new InvalidOperationException("Envanter oluşturulmadı.");
                }

                if (inventory.GetItemCount() >= inventory.Capacity)
                {
                    Console.WriteLine("Envanter dolu, " + item.name + " eklenemedi.");
                    return false; // Envanter doluysa eşya eklenemez
                }

                inventory.AddItem(item);
                return true; // Eşya başarıyla eklendi
            }

            // Envanterden eşya çıkar
          public Item RemoveItemFromInventory(string itemName)
        {
            if (inventory == null || inventory.GetItemCount() == 0)
            {
                throw new InvalidOperationException("Envanter boş.");
            }

            Item itemToRemove = inventory.RemoveItem(itemName);
            if (itemToRemove == null)
            {
                throw new ArgumentException($"'{itemName}' isimli eşya envanterde bulunamadı.");
            }

            return itemToRemove;
        }

            // // Belirli bir eşyayı envanterden çıkar
            // public Item RemoveSpecificItemFromInventory(string itemName)
            // {
            //     if (inventory == null)
            //     {
            //         throw new InvalidOperationException("Envanter boş.");// Envanter boşsa hata atılır
            //     }
            //     return inventory.RemoveThatItem(itemName);
            // }

            
            public bool RescueCurrentVillage()
            {
                // köy yoksa false döndür
                if (currentVillage == null)
                    return false;

            // köydeki eşyaları çantaya ekle
            foreach (Object obj in currentVillage.Items)
            {
                Item item = (Item)obj; // arraylistten iteme dönüşür '2'
                if (inventory.GetItemCount() < inventory.Capacity)// Envanterde yer varsa ekler
                {
                    inventory.AddItem(item);
                }
                else
                {
                    Console.WriteLine("Envanter dolu, " + item.name + " eklenemedi.");
                }
            }

            // köy kurtarıldı 
            currentVillage.isRescued = true;

            rescuedVillages.Add(currentVillage.name);

                // köy kuyruktan cıkartılır
                villageQueue.Dequeue();

                // Diğer köyü seç
                if(villageQueue.Count > 0)
                {
                //   currentVillage = (Village)villageQueue.Peek();
                    if(villageQueue.Peek() is Village vill)
                    {
                        currentVillage = vill; // sonraki köyü seç
                    }
                    else
                    {
                        throw new InvalidOperationException("Kuyruktaki öğe köy değil");
                    }
                }
                else
                {
                    currentVillage = null; // Tüm köyler kurtarıldıysa null yap
                }

                 return true;
                }

            // Oyun bitti mi kontrol et
            public bool IsGameComplete()
            {
                return villageQueue.Count == 0;
            }
     }
}