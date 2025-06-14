using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village_Game
{
    class Program
    {
        static void Main(string[] args)
        {
            VillageGameManager gameManager = new VillageGameManager();

            Console.WriteLine("İyi Oyunlar");
            while (!gameManager.IsGameComplete())
            {
                Console.WriteLine("\nBulunduğun Köy: " + gameManager.GetCurrentVillage().name);

                Console.WriteLine("1-Köyde bulunan eşyaları listele");
                Console.WriteLine("2-Çantadaki eşyaları listele");
                Console.WriteLine("3-Köyleri sırayla kurtar");
                Console.WriteLine("4-Kurtarılan köyleri göster");
                Console.WriteLine("5-Kalan köyleri göster");
                Console.WriteLine("6-Eşyayı kullan veya eşyayı çantadan çıkar");
                Console.WriteLine("7-Oyun içinde ne kadar ilerledim?");
                Console.WriteLine("8-Eşyayı ara");
                Console.WriteLine("9-Oyundan Çık");

                Console.WriteLine("\nHangi işlemi yapmak istersiniz?  1-9 arasında numara girin");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        gameManager.GetCurrentVillage().ShowItems();
                        break;

                    case "2":
                        Console.WriteLine("\nEnvanter:");
                        gameManager.DisplayItems();
                        break;

                    case "3":
                        if (gameManager.RescueCurrentVillage())
                        {
                            Console.WriteLine("\nKöy başarıyla kurtarıldı!");
                            if (gameManager.IsGameComplete())
                            {
                                Console.WriteLine("\nTebrikler tüm köyler kurtarıldı!");
                            }
                        }
                        else
                        {
                            Console.WriteLine("\nKöy kurtarılamadı!");
                        }
                        break;

                    case "4":
                        Console.WriteLine("\nKurtarılan köyler:");
                        foreach (var village in gameManager.GetRescuedVillages())
                        {
                            Console.WriteLine("- " + village);
                        }
                        break;

                    case "5":
                        Console.WriteLine("\nKalan Köyler:");
                        foreach (var village in gameManager.GetRemainingVillages())
                        {
                            Console.WriteLine("- " + village);
                        }
                        break;

                    case "6":
                        Console.Write("\nÇıkarmak veya kullanmak istediğiniz eşyanın adını girin: ");
                        string? itemName = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(itemName))
                        {
                            Console.WriteLine("Lütfen geçerli bir eşya adı girin.");
                            break;
                        }
                        var removedItem = gameManager.RemoveItemFromInventory(itemName);
                        if (removedItem != null)
                        {
                            Console.WriteLine(removedItem.name + " envanterden çıkarıldı.");
                        }
                        break;

                    case "7":
                        Console.WriteLine("\nOyun ilerlemesi:");
                        Console.WriteLine("Kurtarılan köyler: " + gameManager.GetRescuedVillages().Count);
                        Console.WriteLine("Kalan köyler: " + gameManager.GetRemainingVillages().Count);
                        // Doğru sayım için Inventory'den alınmalı
                        Console.WriteLine("Envanterdeki eşyalar: " + gameManager.inventory.GetItemCount());
                        break;

                    case "8":
                        Console.Write("\nAramak istediğiniz eşyanın adını girin: ");
                        string? searchName = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(searchName))
                        {
                            gameManager.inventory.SearchItem(searchName);
                        }
                        else
                        {
                            Console.WriteLine("Geçerli bir eşya adı giriniz.");
                        }
                        break;
                        

                    case "9":
                        Console.WriteLine("\nOyundan çıkıldı.İyi Günler!");
                        return;


                    default:
                        Console.WriteLine("\nLütfen geçerli bir tuşa basınız.");
                        break;
                }

                Console.WriteLine("\nDevam etmek için herhangi bir tuşa basın");
                Console.ReadKey();
            }
        }
    }
}