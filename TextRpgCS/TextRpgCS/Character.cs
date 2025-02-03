using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRpgCS
{
    public interface IGameCharacter
    {
        public int Hp { get; set; }
        public int AttackPower { get; set; }
        public string Name { get; set; }

         //delegate void OnDead();
         event Action OnDeadEvent;

         //delegate void OnAttack();
         event Action OnAttackEvent;
    }


    internal class Character
    {
    }
}
