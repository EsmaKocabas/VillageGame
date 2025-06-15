using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village_Game
{
    public class Item : IComparable<Item>
    {
        
        public string name { get; set; }
        public string description { get; set; }
        public int power { get; set; }

    
    public Item(string name, string description, int power)
        {
          this.name = name;
          this.description = description;
          this.power = power;
        }
    

    public int CompareTo(Item? other)
        {
            if (other == null) return 1;
            
            return string.Compare(this.name, other.name, StringComparison.OrdinalIgnoreCase);
        }

        public override string ToString()
        {
            return ("İsim: " + name + ", Açıklama: " + description + ", Güç: " + power);
        }
    }
}