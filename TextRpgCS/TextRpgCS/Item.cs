﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace TextRpgCS
{
    public interface IUseableItem
    {
        public void Use(Player player);
    }
    public abstract class Item
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public ItemType Type { get; protected set; }

        //public virtual void Use()
        //{
        //    Console.WriteLine($"{Name}아이템을 사용!");
        //}
    };

    public class HpPotion : Item,IUseableItem
    {
        public int HealAmount { get; }

        public HpPotion()
        {
            var config = ItemManager.Instance.GetItemConfig("HpPotion");
            Type = config != null ? Enum.Parse<ItemType>(config.ItemType) : ItemType.HpPotion; // ✅ JSON에서 `ItemType` 불러오기
            Name = config != null ? config.Name : "체력 포션";
            Description = config != null ? config.Description : "사용 시 체력을 회복합니다.";
            HealAmount = config != null ? config.Effect : 50;
        }

        public void Use(Player player)
        {
            player.Hp += HealAmount;
        }
    };

    // 여기서 사용, 사용종료 구현각각해서 아이ㅏ템매니저에서 Update로 계산
    // 턴 개념이 있는 아이템은 이 클래스를 상속
    public abstract class DurationItem : Item, IUseableItem
    {
        public int Duration = 3;
        public virtual void Use(Player player) {  }
        public virtual void EndEffect(Player player) { Console.WriteLine($"{Name}아이템의 버프지속시간이 종료되었습니다."); }
    }

    public class AttackPotion : DurationItem
    {
        public int BonusDamage { get; }
        public AttackPotion()
        {
            var config = ItemManager.Instance.GetItemConfig("AttackPotion");
            Type = config != null ? Enum.Parse<ItemType>(config.ItemType) : ItemType.AttackPotion; // ✅ JSON에서 `ItemType` 불러오기
            Name = config != null ? config.Name : "공격력 증가 포션";
            Description = config != null ? config.Description : "사용 시 일정 시간 동안 공격력이 증가합니다.";
            BonusDamage = config != null ? config.Effect : 10;
            Duration = config != null ? config.Duration : 3;
        }
        public override void Use(Player player)
        {
            base.Use(player);
            // 세턴동안 진행
            player.AttackPower += BonusDamage;
        }
        public override void EndEffect(Player player) 
        {
            //if (GameManager.Instance.Player == null) return;
            GameManager.Instance.Player.AttackPower -= BonusDamage;
        }
    };

    public class ShieldPotion : DurationItem
    {
        public float BonusShield { get; }
        public ShieldPotion()
        {
            var config = ItemManager.Instance.GetItemConfig("ShieldPotion");
            Type = config != null ? Enum.Parse<ItemType>(config.ItemType) : ItemType.ShieldPotion; // ✅ JSON에서 `ItemType` 불러오기
            Name = config != null ? config.Name : "방어력증가포션";
            Description = config != null ? config.Description : "3턴 동안 받는 피해가 50% 감소합니다.";
            BonusShield = config != null ? config.Effect : 0.5f;
            Duration = config != null ? config.Duration : 3;

        }

        public override void Use(Player player)
        {
            base.Use(player);
            // 3턴 동안 피해감소
            player.DefenseRate -= BonusShield;
        }
        public override void EndEffect(Player player) 
        {
            GameManager.Instance.Player.DefenseRate += BonusShield;
        }
    };

    public class RandomPotion : Item, IUseableItem
    {
        private List<string> PositiveEffects;
        private List<string> NegativeEffects;
        private Random random = new Random();

        public RandomPotion()
        {
            var config = ItemManager.Instance.GetItemConfig("RandomPotion");
            Type = config != null ? Enum.Parse<ItemType>(config.ItemType) : ItemType.RandomPotion; // ✅ JSON에서 `ItemType` 불러오기
            Name = config != null ? config.Name : "랜덤 포션";
            Description = config != null ? config.Description : "랜덤으로 긍정적 또는 부정적 효과를 발생시킴.";
            PositiveEffects = config != null ? config.PositiveEffects : new List<string> { "HpFullRecovery", "DoubleAttackPower" };
            NegativeEffects = config != null ? config.NegativeEffects : new List<string> { "HalfHp", "DoubleDamageTaken" };
        }

        public void Use(Player player)
        {
            bool isPositive = random.Next(0, 2) == 0; // ✅ 50% 확률로 긍정 or 부정 효과 선택
            string selectedEffect = isPositive
                ? PositiveEffects[random.Next(PositiveEffects.Count)]
                : NegativeEffects[random.Next(NegativeEffects.Count)];

            ApplyEffect(player, selectedEffect);
        }
        private void ApplyEffect(Player player, string effect)
        {
            switch (effect)
            {
                case "HpFullRecovery":
                    player.Hp = player.MaxHp;
                    Console.WriteLine($"{player.Name}의 체력이 최대치로 회복되었습니다!");
                    break;

                case "DoubleAttackPower":
                    player.AttackPower *= 2;
                    Console.WriteLine($"{player.Name}의 공격력이 2배 증가했습니다!");
                    break;

                case "HalfHp":
                    player.Hp /= 2;
                    Console.WriteLine($"{player.Name}의 체력이 절반으로 감소했습니다!");
                    break;

                case "DoubleDamageTaken":
                    player.DefenseRate *= 2;
                    Console.WriteLine($"{player.Name}이 받는 피해가 2배 증가했습니다!");
                    break;
            }
        }
    }
}
