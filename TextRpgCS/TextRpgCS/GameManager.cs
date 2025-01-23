using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRpgCS
{
    class GameManager
    {
        public static GameManager Instance { get; }=new GameManager();

        public static int MonsterCount { get; } = 3;
        
        public  Monster[] monsters;

        Boss _boss;
        public Boss Boss { get { return _boss; } }

        public void Init()
        {
            monsters = new Monster[MonsterCount];
            for(int i = 0; i < MonsterCount; i++) 
            {
                monsters[i] = new Monster();
            }

            _boss = new Boss();
        }

        public void ResetMonter()
        {
            foreach(Monster monster in monsters)
            {
                monster.Hp = 30 * monster.Id;
            }
        }
    }
}
