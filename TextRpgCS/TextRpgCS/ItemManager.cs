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

        // 아이템이름, 개수
        public Dictionary<string,int> Inventory { get; set; }

        public Dictionary<string, DurationItem> DurationItems { get; set; }

        

        ItemManager() 
        {
            Inventory = new Dictionary<string,int>();
            DurationItems = new Dictionary<string, DurationItem>();

        }

        public void Update()
        {
            List<string> RemoveItems = new List<string>();
            foreach (var item in DurationItems)
            {
                item.Value.Duration--;
                if(item.Value.Duration <= 0 )
                {
                    item.Value.EndEffect();
                    RemoveItems.Add(item.Key);
                }
            }
            foreach (var item in RemoveItems)
            {
                DurationItems.Remove(item);
            }
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

        public void RegistItem(Item item)
        {
            DurationItem? di = item as DurationItem;
            if (di == null)
            {
                Console.WriteLine("버프아이템 등록오류 : RegistItem");
                return;
            }
            DurationItems[di.Name] = di;
        }

        public void PrintDurationItemList()
        {

        }

        public void PrintInventory()
        {
            foreach (var item in Inventory)
            {
                
            }
            //Console.WriteLine($"이름 : {}");
        }
    }
}
