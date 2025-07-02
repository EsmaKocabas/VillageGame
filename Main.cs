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
            
            VillageGameManager gameManager = new VillageGameManager(); //oyun yönetimi

            Console.WriteLine("Köy Kurtarma Oyununa Hoş Geldiniz!");
            while (!gameManager.IsGameComplete())
            {

                Console.WriteLine("\nBulunduğun Köy: " + gameManager.GetCurrentVillage().name); 

                Console.WriteLine("1-Köydeki eşyaları göster");
                Console.WriteLine("2-Çantadaki eşyaları göster");
                Console.WriteLine("3-Köyleri sırayla kurtar");
                Console.WriteLine("4-Kurtarılan köyleri göster");
                Console.WriteLine("5-Kalan köyleri göster");
                Console.WriteLine("6-Eşyayı kullan veya eşyayı çantadan çıkar");
                Console.WriteLine("7-Oyun içinde ne kadar ilerledim?");
                Console.WriteLine("8-Eşya ara");
                Console.WriteLine("9-Oyundan Çık");

                Console.WriteLine("\nHangi işlemi yapmak istersiniz?  1-9 arasında numara girin");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        gameManager.GetCurrentVillage().ShowItems(); 
                        break;

                    case "2":
                        Console.WriteLine("\nÇantadaki eşyalar:");
                        gameManager.DisplayItems();
                        break;

                    case "3":
                        if (gameManager.RescueCurrentVillage())
                        {
                            Console.WriteLine("\nKöy kurtarıldı!");
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
                            Console.WriteLine("=> " + village);
                        } //kurtarılan köyler listelenir
                        break;

                    case "5":
                        Console.WriteLine("\nKalan köyler:");
                        foreach (var village in gameManager.GetRemainingVillages())
                        {
                            Console.WriteLine("=> " + village);
                        }
                        break;

                    case "6":
                        Console.Write("\nÇıkarmak veya kullanmak istediğiniz eşyanın adını girin: ");
                        string? itemName = Console.ReadLine();
                        if (itemName==null || itemName.Trim()=="")
                        {
                            Console.WriteLine("Lütfen geçerli bir eşya adı girin.");
                            break;
                        }
                        var removedItem = gameManager.RemoveItemFromInventory(itemName); // cantadan esya cıkarılır veya kulanılır
                        if (removedItem != null)
                        {
                            Console.WriteLine(removedItem.name + " envanterden çıkarıldı.");
                        }
                        else
                        {
                            Console.WriteLine("Envanterde bulunamadı.");
                            break;
                        }
                        break;


                    case "7":
                        Console.WriteLine("\nOyun ilerlemesi:");
                        Console.WriteLine("Kurtarılan köyler: " + gameManager.GetRescuedVillages().Count);
                        Console.WriteLine("Kalan köyler: " + gameManager.GetRemainingVillages().Count);
                        
                        Console.WriteLine("Envanterdeki eşyalar: " + gameManager.inventory.ItemCount());
                        break;

                    case "8":
                        Console.Write("\nAramak istediğiniz eşyanın adını girin: ");
                        string? searchName = Console.ReadLine();
                        if (searchName!= null && searchName.Trim() != "")  //eşya adı bos degil ise
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