using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace TextRpgCS
{
    public abstract class Item
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public ItemType Type { get; protected set; }

        public virtual void Use()
        {
            Console.WriteLine($"{Name}아이템을 사용!");
            
        }
    };

    public class HpPortion : Item
    {
        public HpPortion()
        {
            Name = "HP포션";
            Description = "Hp 50을 회복합니다.";
            Type = ItemType.HpPortion;
        }

        public override void Use()
        {
            base.Use();
            GameManager.Instance.Player.Hp += 50;
            ItemManager.Instance.RemoveItem(Name);
        }
    };

    // 여기서 사용, 사용종료 구현각각해서 아이ㅏ템매니저에서 Update로 계산
    // 턴 개념이 있는 아이템은 이 클래스를 상속
    public abstract class DurationItem : Item
    {
        public int Duration = 3;
        public virtual void EndEffect() { Console.WriteLine($"{Name}아이템의 버프지속시간이 종료되었습니다."); }
    }

    public class AttackPotion : DurationItem
    {
        
        public AttackPotion()
        {
            Name = "공격력증가포션";
            Description = "공격력을 3턴동안 10증가시킵니다.";
            Type = ItemType.AttackPotion;
        }
        public override void Use()
        {
            base.Use();
            // 세턴동안 진행
            GameManager.Instance.Player.AttackPower += 10;
            Item? it= ItemManager.Instance.RemoveItem(Name);
            if(it!=null)
                ItemManager.Instance.RegistItem(it);
            
        }
        public override void EndEffect() 
        {
            //if (GameManager.Instance.Player == null) return;
            GameManager.Instance.Player.AttackPower -= 10;
        }
    };

    public class ShieldPotion : DurationItem
    {

        public ShieldPotion()
        {
            Name = "방어력증가포션";
            Description = "3턴 동안 받는 피해가 50% 감소합니다.";
            Type = ItemType.ShieldPotion;
        }

        public override void Use()
        {
            base.Use();
            // 3턴 동안 피해감소
            GameManager.Instance.Player.DefenseRate -=0.5f;
            Item? it = ItemManager.Instance.RemoveItem(Name);
            if (it != null)
                ItemManager.Instance.RegistItem(it);
        }
        public override void EndEffect() 
        {
            GameManager.Instance.Player.DefenseRate += 0.5f;
            
        }
    };

    public class RandomPortion : Item
    {
        public RandomPortion()
        {
            Name = "랜덤포션";
            Description = "랜덤으로 긍정적 또는 부정적 효과를 발생시킴.\r\n\r\n" +
                "긍정적 효과: 체력 전부 회복, 공격력 2배 증가.\r\n\r\n" +
                "부정적 효과: 체력 절반 감소, 방어력 감소.";
            Type = ItemType.RandomPortion;
        }

        public override void Use()
        {
            base.Use();
            // 랜덤 효과 발생
            Random random = new Random();
            
            int randomValue = random.Next(0, 4);

            switch(randomValue)
            {
                // hp 전부 회복
                case 0:
                    GameManager.Instance.Player.Hp = GameManager.Instance.Player.MaxHp;
                    break;
                // 공격력 2배
                case 1:
                    GameManager.Instance.Player.AttackPower *= 2;
                    break;
                // hp 절반으로 감소
                case 2:
                    GameManager.Instance.Player.Hp /= 2;
                    break;
                // 받는 데미지 2배
                case 3:
                    GameManager.Instance.Player.DefenseRate *= 2;
                    break;

            }
        }
    }
}
