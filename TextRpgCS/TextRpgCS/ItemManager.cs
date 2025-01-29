using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRpgCS
{
    public enum ItemType
    {
        HpPortion,
        AttackPotion,
        ShieldPotion,
        RandomPortion
    }

    internal class ItemManager
    {
        public static ItemManager Instance { get; private set; }= new ItemManager();

        // 아이템이름, 개수
        public Dictionary<string,List<Item>> Inventory { get; set; }

        public Dictionary<string, DurationItem> DurationItems { get; set; }

        // 나중에 이거활용해서 개선할 수 있으면 하기. 인벤토리를 list가 아닌 개수로하거나
        Dictionary<ItemType, Type> itemMap = new Dictionary<ItemType, Type>();

        ItemManager() 
        {
            Inventory = new Dictionary<string,List<Item>>();
            DurationItems = new Dictionary<string, DurationItem>();
            itemMap.Add(ItemType.HpPortion,typeof(HpPortion));
            itemMap.Add(ItemType.AttackPotion, typeof(AttackPotion));
            itemMap.Add(ItemType.ShieldPotion, typeof(ShieldPotion));
            itemMap.Add(ItemType.RandomPortion, typeof(RandomPortion));
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
#region 인벤토리 관련
        public void AddItem(Item item)
        {
            if(Inventory.ContainsKey(item.Name)==false)
            {
                Inventory[item.Name] = new List<Item>();
            }
            Inventory[item.Name].Add(item);
        }

        public void RemoveItem(string itemName)
        {
            if(Inventory.ContainsKey(itemName) )
            {
                Inventory[itemName].RemoveAt(0);
                if (Inventory[itemName].Count()==0)
                    Inventory.Remove(itemName);
            }
            else
            {
                Console.WriteLine($"Error! 없는 아이템 사용!{itemName}");
            }
        }

        
        public void PrintInventory()
        {
            Console.WriteLine("------인벤토리------");
            foreach (var item in Inventory)
            {
                Console.WriteLine($"{item.Key} : {item.Value.Count}개");
            }
            
        }
        #endregion

        #region 사용버프아이템 관련
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
            foreach(var item in DurationItems)
            {
                Console.WriteLine($"{item.Key} : {item.Value.Duration}턴 남음");
            }
        }
        #endregion
        
        public Item RandomCreateItem()
        {
            Random random = new Random();

            ItemType randomType = (ItemType)random.Next(Enum.GetValues(typeof(ItemType)).Length);
            Item item = (Item)Activator.CreateInstance(itemMap[randomType])!;

            AddItem(item);

            return item;
        }
    }
}
