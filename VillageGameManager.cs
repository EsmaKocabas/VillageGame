using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village_Game
{
    public class VillageGameManager
    {
        public Village? currentVillage;
        public Inventory inventory;
        public ArrayList rescuedVillages = new ArrayList(); 
        public Queue<Village> villageQueue = new Queue<Village>(); 

        public VillageGameManager()
        {
            villageQueue = new Queue<Village>();
            rescuedVillages = new ArrayList();
            inventory = new Inventory(10);
            currentVillage = new Village("Başlangıç Köyü", new ArrayList());
            StartGame();
        }

        public void StartGame()
        {
            Hashtable items = new Hashtable();
            items.Add("Kılıç", new Item("Kılıç", "Güçlü kılıç", 90));
            items.Add("Bıçak", new Item("Bıçak", "Keskin bıçak", 50));
            items.Add("Yiyecek", new Item("Yiyecek", "Ev yemeği", 30));
            items.Add("Balta", new Item("Balta", "Balta", 70));
            items.Add("Kalkan", new Item("Kalkan", "Savunma kalkanı", 50));
            items.Add("Altın", new Item("Altın", "Değerli altın", 10));
            items.Add("Harita", new Item("Harita", "Köylerin haritası", 10));
            items.Add("İksir", new Item("İksir", "İyileştirme", 60));
            items.Add("Zırh", new Item("Zırh", "Savunma zırhı", 80));
            items.Add("Kıyafet", new Item("Kıyafet", "Güzel kıyafet", 20));
            items.Add("Küpe", new Item("Küpe", "Değerli küpe", 40));
            items.Add("Yüzük", new Item("Yüzük", "Güzel yüzük", 30));
            items.Add("Kemer", new Item("Kemer", "Güzel kemer", 20));
            items.Add("Çekiç", new Item("Çekiç", "Güçlü çekiç", 100));
            items.Add("Köpek", new Item("Köpek", "Sadık köpek", 60));

            Village[] village = new Village[7];
            village[0] = new Village("Orman Köyü", new ArrayList { items["Kılıç"], items["Yiyecek"], items["Altın"] });
            village[1] = new Village("Dağ Köyü", new ArrayList { items["Bıçak"], items["Balta"], items["Kalkan"] });
            village[2] = new Village("Yayla Köyü", new ArrayList { items["Zırh"], items["Yüzük"], items["Kemer"] });
            village[3] = new Village("Deniz Köyü", new ArrayList { items["Çekiç"], items["İksir"], items["Köpek"] });
            village[4] = new Village("Vadi Köyü", new ArrayList { items["Kıyafet"], items["Yiyecek"], items["Zırh"] });
            village[5] = new Village("Ova Köyü", new ArrayList { items["Balta"], items["Kalkan"], items["İksir"] });
            village[6] = new Village("Göl Köyü", new ArrayList { items["Kılıç"], items["Bıçak"], items["Yiyecek"] });

            for (int i = 0; i < village.Length; i++)
                villageQueue.Enqueue(village[i]);

            if (villageQueue.Count > 0 && villageQueue.Peek() is Village vill)
                currentVillage = vill;
            else
                throw new InvalidOperationException("Köy kuyruğu boş");
        }

        public Village? GetCurrentVillage()
        {
            if (currentVillage == null)
            {
                Console.WriteLine("Şu anda bir köy yok.");
                return null;
            }
            return currentVillage;
        }

        public ArrayList GetRescuedVillages()
        {
            return rescuedVillages;
        }

        public ArrayList GetRemainingVillages()
        {
            ArrayList remaining = new ArrayList();
            foreach (Village v in villageQueue)
                remaining.Add(v.name);
            return remaining;
        }

        public void DisplayItems()
        {
            inventory.DisplayItems();
            Console.WriteLine("Envanterdeki eşyalar: " + inventory.ItemCount() + "/" + inventory.Capacity);
        }

        public bool AddItemToInventory(Item item)
        {
            if (inventory == null)
                throw new InvalidOperationException("Envanter oluşturulmadı.");

            if (inventory.ItemCount() >= inventory.Capacity)
            {
                Console.WriteLine("Envanter dolu, " + item.name + " eklenemedi.");
                return false;
            }

            inventory.Push(item);
            return true;
        }

        public Item? RemoveItemFromInventory(string itemName)
        {
            Console.WriteLine(itemName + " isimli eşyayı envanterden çıkarmak istiyorsunuz...");
            DisplayItems();
            if (inventory == null || inventory.ItemCount() == 0)
            {
                Console.WriteLine("Envanter boş. Çıkarılacak eşya yok.");
                return null;
            }

            Item? itemToRemove = inventory.RemoveItem(itemName);
            if (itemToRemove == null)
            {
                Console.WriteLine(itemName + " isimli eşya envanterde bulunamadı.");
                return null;
            }
            return itemToRemove;
        }

        public Item? RemoveLastItemFromInventory()
        {
            if (inventory == null || inventory.ItemCount() == 0)
            {
                Console.WriteLine("Envanter boş. Çıkarılacak eşya yok.");
                return null;
            }
            return inventory.Pop();
        }

        public bool RescueCurrentVillage()
        {
            if (currentVillage == null)
                return false;

            
            if (currentVillage.name == "Vadi Köyü")
            {
                bool hasKilic = false;
                bool hasYiyecek = false;
                foreach (Item item in inventory.Items)
                {
                    if (item.name == "Kılıç")
                        hasKilic = true;
                    else if (item.name == "Yiyecek")
                        hasYiyecek = true;

                        if(hasKilic && hasYiyecek)
                            break;
                }

                if(!hasKilic || !hasYiyecek)
                {
                    Console.WriteLine("Vadi Köyü'nü kurtarmak için envanterde hem Kılıç hem de Yiyecek olmalı!");
                    return false;
                }


                inventory.UseItem("Kılıç");
                inventory.UseItem("Yiyecek");


                Console.WriteLine("Kılıç ve Yiyecek kullanıldı ve envanterden çıkarıldı.");
            }


            
            if (currentVillage.name == "Ova Köyü")
            {
                bool hasAltin = false;
                bool hasBicak = false;
                foreach (Item item in inventory.Items)
                {
                    if (item.name == "Altın")
                        hasAltin = true;
                    else if (item.name == "Bıçak")
                        hasBicak = true;

                    if (hasAltin && hasBicak)
                        break;
                }

                if (!hasAltin || !hasBicak)
                {
                    Console.WriteLine("Ova Köyü'nü kurtarmak için envanterde hem Altın hem de Bıçak olmalı!");
                    return false;
                }
                
                inventory.UseItem("Altın");
                inventory.UseItem("Bıçak");
            

                Console.WriteLine("Altın ve Bıçak kullanıldı ve envanterden çıkarıldı.");
            }

            
            if (currentVillage.name == "Göl Köyü")
            {
                bool hasBalta = false;
                bool hasKalkan = false;
                foreach (Item item in inventory.Items)
                {
                    if (item.name == "Balta")
                        hasBalta = true;
                    else if (item.name == "Kalkan")
                        hasKalkan = true;

                    if (hasBalta && hasKalkan)
                        break;
                }

                if (!hasBalta || !hasKalkan)
                {
                    Console.WriteLine("Göl Köyü'nü kurtarmak için envanterde hem Balta hem de Kalkan olmalı!");
                    return false;
                }

                inventory.UseItem("Balta");
                inventory.UseItem("Kalkan");

                Console.WriteLine("Balta ve Kalkan kullanıldı ve envanterden çıkarıldı.");
            }



            foreach (Object obj in currentVillage.Items)
            {
                if (inventory.ItemCount() >= inventory.Capacity)
                {
                    Console.WriteLine("Envanter dolu, daha fazla eşya eklenemedi.");
                    Console.WriteLine("Envanter dolu olduğu için en eski öğe otomatik olarak siliniyor.");
                    Console.WriteLine("Eğer onaylıyorsanız 'Evet' yazın, onaylamıyorsanız 'Hayır' yazın.");

                    string? response = Console.ReadLine();
                    if (response == null || !response.Equals("Evet", StringComparison.OrdinalIgnoreCase))
                    {
                        Console.WriteLine("Envanter dolu, öğe eklenemedi.");
                        inventory.RemoveOldestItem();
                    }
                    else
                    {
                        Console.WriteLine("Yeni öğe eklenecek.");
                    }
                }

                Item item = (Item)obj;
                Item newItem = new Item(item.name, item.description, item.power);
                inventory.Push(newItem);
            }

            currentVillage.isRescued = true;
            rescuedVillages.Add(currentVillage.name);

            villageQueue.Dequeue();

            if (villageQueue.Count > 0 && villageQueue.Peek() is Village nextVillage)
                currentVillage = nextVillage;
            else
                currentVillage = null;

            return true;
        }
       
        public bool IsGameComplete()
        {
            return villageQueue.Count == 0;
        }
    }
}