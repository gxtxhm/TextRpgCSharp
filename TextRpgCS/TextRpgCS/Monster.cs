using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRpgCS
{
    public class Monster
    {
        static int CountId = 1;

        int _hp = 30;

        public int Id { get; } = CountId;
        public string Name { get; set; }
        public int Level { get; set; } = 1;
        public int Exp { get; set; } = 10;
        public int Hp { get { return _hp; } set { if (value <= 0) { _hp = 0;Dead(); } else { _hp = value; } } }
        public int AttackPower { get; set; } = 10;

        public Monster()
        {
            Level = 1*CountId;
            Exp = 10*CountId;
            Hp = 30*CountId;
            AttackPower = 10*CountId/2;
            this.Name = $"몬스터{CountId++}";
        }

        public void Attack(Player player)
        {
            player.TakeDamage(AttackPower);
        }

        public void TakeDamage(int damage)
        {
            Hp -= damage;
            Console.WriteLine($"{Name}에게 데미지{damage}를 입혔습니다. {Name}의 체력 : {Hp}");
        }

        void Dead()
        {
            
        }
    }
}
