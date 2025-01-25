using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace TextRpgCS
{
    abstract class Item
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }

        public virtual void Use()
        {
            Console.WriteLine($"{Name}아이템을 사용!");
        }
    };

    class HpPortion : Item
    {
        public HpPortion()
        {
            Name = "HP포션";
            Description = "Hp 50을 회복합니다.";
        }

        public override void Use()
        {
            base.Use();
            GameManager.Instance.Player.Hp += 50;
        }
    };

    // 여기서 사용, 사용종료 구현각각해서 아이ㅏ템매니저에서 Update로 계산
    abstract class DurationPortion : Item
    {
        public int Duration = 3;
        public virtual void EndEffect() { }
    }

    class AttackPotion : DurationPortion
    {
        
        public AttackPotion()
        {
            Name = "공격력증가포션";
            Description = "공격력을 3턴동안 10증가시킵니다.";
        }
        public override void Use()
        {
            base.Use();
            // 세턴동안 진행
            
        }
        public override void EndEffect() 
        {

        }
    };

    class ShieldPotion : DurationPortion
    {

        public ShieldPotion()
        {
            Name = "방어력증가포션";
            Description = "3턴 동안 받는 피해가 50% 감소합니다.";
        }

        public override void Use()
        {
            base.Use();
            // 3턴 동안 피해감소
        }
        public override void EndEffect() { }
    };

    class RandomPortion : Item
    {
        public RandomPortion()
        {
            Name = "랜덤포션";
            Description = "랜덤으로 긍정적 또는 부정적 효과를 발생시킴.\r\n\r\n" +
                "긍정적 효과: 체력 전부 회복, 공격력 2배 증가.\r\n\r\n" +
                "부정적 효과: 체력 절반 감소, 방어력 감소.";
        }

        public override void Use()
        {
            base.Use();
            // 랜덤 효과 발생
        }
    }
}
