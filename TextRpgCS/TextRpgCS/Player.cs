using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TextRpgCS
{
    class Player
    {
        int _hp=100;
        int _exp = 0;

        public string Name { get; set; }
        public int Level { get; set; } = 1;
        public int Exp { get { return _exp; } 
            set 
            {
                while (value >= MaxExp)
                {
                    Level++;
                    Console.WriteLine($"레벨업 했습니다! 현재 레벨 : {Level}");
                    AttackPower *= 2;
                    Hp = 100;
                    MaxExp *= 2;
                    value = (value - MaxExp > 0)? value-MaxExp : 0;
                }
                 _exp = value; 
            } 
        }
        public int MaxExp { get; set; } = 10;
        public int Hp { get { return _hp; } set { if (value <= 0) { _hp = 0; Dead(); } else { _hp = value; } } } 
        public int AttackPower { get; set; } = 10;

        public Player(string Name) 
        {
            this.Name = Name;
        }

        public void PrintInfo()
        {
            Console.WriteLine($"이름 : {Name}");
            Console.WriteLine($"레벨 : {Level}");
            Console.WriteLine($"경험치 : {Exp}/{MaxExp}");
            Console.WriteLine($"체력 : {Hp}");
            Console.WriteLine($"공격력 : {AttackPower}\n");
        }

        public void Attack(Monster monster)
        {
            monster.TakeDamage(AttackPower);
        }

        public void TakeDamage(int damage)
        {
            Hp -= damage;
            Console.WriteLine($"몬스터에게 데미지{damage}를 입었습니다! 현재체력 : {Hp}");
        }

        public void GetExp(int exp)
        {
            Exp += exp;
        }

        void Dead()
        {
            
        }
    }
}
