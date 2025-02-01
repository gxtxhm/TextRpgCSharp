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

    }


    internal class Character
    {
    }
}
