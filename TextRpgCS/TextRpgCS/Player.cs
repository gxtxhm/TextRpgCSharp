using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace TextRpgCS
{
    public class Player : IGameCharacter
    {
        public int MaxHp { get; set; } = 100;
        int _hp;
        int _exp = 0;

        //public delegate void OnDead();
        public event Action OnDeadEvent;

        //public delegate void OnAttack();
        public event Action OnAttackEvent;

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
        public int Hp { get { return _hp; }
            set {
                if (value <= 0) { _hp = 0; OnDeadEvent?.Invoke(); }
                else if (value > MaxHp) { _hp = MaxHp; }
                else { _hp = value; } 
            } 
        } 
        public int AttackPower { get; set; } = 10;
        // 높을수록 데미지 더 받음. 
        public float DefenseRate { get; set; } = 1;

        public Player() { _hp = MaxHp;OnDeadEvent += Dead; OnAttackEvent += PlayAttack; }

        public Player(string Name) : this()
        {
            this.Name = Name;
        }

        public void PrintPropertyValueByReflection(Player player)
        {
            Type type = typeof(Player);

            Console.WriteLine("=== Properties ===");
            var properties = type.GetProperties();

            foreach (var property in properties)
            {
                object value = property.GetValue(player); // ✅ 프로퍼티 값을 가져오기 위해 `GetValue(player)` 사용
                Console.WriteLine($"{property.PropertyType} {property.Name} = {value}");
                if(property.Name=="Hp")
                {
                    property.SetValue(player, 30);
                }
            }

            Console.WriteLine("=== Fields ===");
            var fields = type.GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            foreach (var field in fields)
            {
                object value = field.GetValue(player); // ✅ 필드 값을 가져오기 위해 `GetValue(player)` 사용
                Console.WriteLine($"{field.FieldType} {field.Name} = {value}");
            }
        }


        public void PrintInfo()
        {
            Console.WriteLine($"이름 : {Name}");
            Console.WriteLine($"레벨 : {Level}");
            Console.WriteLine($"경험치 : {Exp}/{MaxExp}");
            Console.WriteLine($"체력 : {Hp}");
            Console.WriteLine($"공격력 : {AttackPower}\n");
        }
        public void PlayAttack()
        {
            Console.WriteLine("플레이어가 공격모션을 실행합니다. in Player");
        }

        public void Attack(Monster monster)
        {
            OnAttackEvent?.Invoke();
            monster.TakeDamage(AttackPower);

        }

        public void TakeDamage(int damage)
        {
            Console.WriteLine($"몬스터에게 데미지{damage}를 입었습니다! 현재체력 : {Hp}");
            Hp -= (int)(damage*DefenseRate);
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
