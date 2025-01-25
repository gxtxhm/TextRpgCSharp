using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRpgCS
{
    internal class ItemManager
    {
        public static ItemManager Instance { get; private set; }= new ItemManager();

        public Dictionary<string,int> Inventory { get; set; }

        public Dictionary<string,int> DurationPotions { get; set; }

        

        ItemManager() 
        {
            Inventory = new Dictionary<string,int>();
            DurationPotions = new Dictionary<string,int>();

        }

        public void Update()
        {
            
        }

        public void AddItem(Item item)
        {
            Inventory[item.Name] = (Inventory.ContainsKey(item.Name) ? Inventory[item.Name] : 0) + 1;
        }

        public void RemoveItem(Item item)
        {
            if(Inventory.ContainsKey(item.Name) )
            {
                if(Inventory.ContainsKey(item.Name))
                {
                    Inventory[item.Name]--;
                    if (Inventory[item.Name]==0)
                        Inventory.Remove(item.Name);
                }
            }
        }

        public void RegistPortion(Item portion)
        {
            DurationPortion? dp = portion as DurationPortion;
            if (dp == null)
            {
                Console.WriteLine("버프아이템 등록오류 : RegistPortion");
                return;
            }
            DurationPotions[dp.Name] = dp.Duration;
        }
    }
}
