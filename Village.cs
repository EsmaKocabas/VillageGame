using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village_Game
{
    public class Village
    {
        public string name { get; set; }
        public bool isRescued { get; set; }
        public ArrayList Items { get;set; } 


        public Village(string name,ArrayList item)
        {
            this.name = name;
            this.isRescued = false; //köy başlangıçta kurtarılmamış
            this.Items = item;

        }
        public void ShowItems()
        {
            Console.WriteLine("Köydeki eşyalar:"); 
            foreach (object obj in Items)
            {
                Item item = (Item)obj; // array list tanımlandıgı için item tipine dönüştürülür
                Console.WriteLine(item.name+ " Eşya açıklaması: " + item.description + ", Güç: " + item.power);
            }
        }
    }
}
