﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using Newtonsoft.Json;

namespace TextRpgCS
    {
        public enum ItemType
        {
            HpPotion,
            AttackPotion,
            ShieldPotion,
            RandomPotion
        }

    // TODO : 아이템에 아이디 부여해서 관리하는 방식이 더 나을 것 같음.
    // ItemDataTableManager 같은것도 관리해서 하드코딩대신 사용하면 좋을듯.
    public class ItemManager
    {
        public static ItemManager Instance { get; private set; }= new ItemManager();

        // 아이템이름, 개수
        public Dictionary<string,List<Item>> Inventory { get; set; }

        public Dictionary<string, DurationItem> DurationItems { get; set; }

        // 나중에 이거활용해서 개선할 수 있으면 하기. 인벤토리를 list가 아닌 개수로하거나
        Dictionary<ItemType, Type> itemMap = new Dictionary<ItemType, Type>();

        private Dictionary<string, ItemData> itemConfigs;

        public event Action<string> OnUsedItem;
        

        ItemManager() 
        {
            Inventory = new Dictionary<string,List<Item>>();
            DurationItems = new Dictionary<string, DurationItem>();
            itemMap.Add(ItemType.HpPotion,typeof(HpPotion));
            itemMap.Add(ItemType.AttackPotion, typeof(AttackPotion));
            itemMap.Add(ItemType.ShieldPotion, typeof(ShieldPotion));
            itemMap.Add(ItemType.RandomPotion, typeof(RandomPotion));
        }

        public void Update()
        {
            if (DurationItems == null)
            {
                Console.WriteLine("durationItems Null!");
                return;
            }
            if (DurationItems.Count == 0) return;
            List<string> RemoveItems = new List<string>();
            foreach (var item in DurationItems)
            {
                item.Value.Duration--;
                if(item.Value.Duration <= 0 )
                {
                    item.Value.EndEffect(GameManager.Instance.Player);
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
            Item? it;
            if(Inventory.ContainsKey(itemName) )
            {
                it = Inventory[itemName][0];
                Inventory[itemName].RemoveAt(0);
                if (Inventory[itemName].Count()==0)
                    Inventory.Remove(itemName);
            }
            else
            {
                Console.WriteLine($"Error! 없는 아이템 사용!{itemName}");
            }
        }

        public void UsedItem(string itemName, Player player)
        {
            if (Inventory.ContainsKey(itemName) == false) return;

            Item it = Inventory[itemName][0];
            IUseableItem? useableItem = it as IUseableItem;
            if(useableItem==null)
            {
                Console.WriteLine("소비아이템이 아닙니다!");return;
            }

            useableItem.Use(player);

            OnUsedItem?.Invoke($"아이템 로그 : {it.Name}을 사용!");

            RemoveItem(itemName);
            if (it is DurationItem)
                RegistItem(it);
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


        public void LoadItemsFromJson()
        {
            string json = File.ReadAllText("items.json"); // ✅ JSON 파일 읽기
            itemConfigs = JsonConvert.DeserializeObject<Dictionary<string, ItemData>>(json); // ✅ JSON -> C# 객체 변환
        }

        public ItemData GetItemConfig(string itemType)
        {
            return itemConfigs != null && itemConfigs.ContainsKey(itemType) ? itemConfigs[itemType] : null;
        }
    }
}
