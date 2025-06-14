using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Village_Game
{
    public class Item : IComparable<Item>
    {
        //Oyundaki eşyalar
        public string name { get; set; }
        public string description { get; set; }
        public int power { get; set; } //çanta veya köydeki ögeleri avl'de sıralamak için

    
    public Item(string name, string description, int power)
        {
          this.name = name;
          this.description = description;
          this.power = power;
        }
    

    public int CompareTo(Item? other)
        {
            if (other == null) return 1;
            // Sadece isme göre karşılaştır
            return string.Compare(this.name, other.name, StringComparison.OrdinalIgnoreCase);
        }

        public override string ToString()
        {
            return $"{name} - {description} (Power: {power})";
        }
    }
}