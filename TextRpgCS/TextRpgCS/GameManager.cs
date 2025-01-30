using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TextRpgCS
{

    public enum ResultBattle
    {
        PlayerDie,
        MonsterDie,
        RetreatPlayer
    }


    public class GameManager
    {
        public static GameManager Instance { get; }=new GameManager();

        public static int MonsterCount { get; } = 3;

        public Player Player { get; private set; }
        public  Monster[] monsters;

        Boss _boss;
        public Boss Boss { get { return _boss; } }

        public void Init()
        {
            Player = new Player();
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

        public void PlayStartScene()
        {
            /*PrintStringByTick("어둠의 그림자가 세상을 덮쳤다.\n" +
            "당신은 이 세계를 구할 유일한 용사로 선택받았다.\n" +
            "지금부터의 여정은 쉽지 않을 것이다.\n" +
            "당신의 선택과 용기가 모든 것을 바꿀 것이다.\n\n",30);*/

            Console.WriteLine("조작 방법을 알려드리겠습니다.\n\n");

            Console.WriteLine("조작 방법:\n1. 숫자를 입력하여 선택지를 고릅니다.\n" +
                "2. 전투 중에는 '1'을 입력해 공격\n '2'를 입력해 아이템 사용.\n3. 게임 종료는 '0'을 입력하세요.\n");

            Console.WriteLine("용사의 이름은 무엇인가?\n");
            Player.Name = Console.ReadLine();
        }

        public void PrintPlayerInfo()
        {
            Console.WriteLine("캐릭터 상태창입니다.");
            Player.PrintInfo();
            ItemManager.Instance.PrintInventory();
        }

        public void ExitGame()
        {
            Console.WriteLine("게임을 종료합니다.");
            Environment.Exit(0);
        }

        public void PrintMainMenu()
        {
            Console.WriteLine(UtilTextManager.MainMenuChoice);
        }

        public ResultBattle StartBattle(Player player, Monster monster)
        {
            Console.WriteLine($"{monster.Name}이(가) 당신을 공격합니다!\n");
            int choice;
            while (player.Hp > 0 && monster.Hp > 0)
            {
                Console.WriteLine(UtilTextManager.ChoiceMenuInBattle);
                choice = int.Parse(Console.ReadLine());
                if (choice == 3)
                {
                    if (monster is Boss)
                        Console.WriteLine(UtilTextManager.RetreatBoss);
                    else
                    {
                        Console.WriteLine(UtilTextManager.ExitDungeon);
                        return ResultBattle.RetreatPlayer;
                    }
                }
                else if (choice == 2)
                {
                    // 인벤토리 보여주기
                    ItemManager.Instance.PrintInventory();
                }
                else
                {
                    Console.WriteLine($"용사{player.Name}가 {monster.Name}을 공격!");
                    player.Attack(monster);
                    if (monster.Hp <= 0) return ResultBattle.MonsterDie;
                    monster.Attack(player);
                }
            }
            return ResultBattle.PlayerDie;
        }

        public void MoveTown()
        {
            Console.WriteLine(UtilTextManager.EnterTown);
        }

        public void MoveDungeon()
        {
            Console.WriteLine(UtilTextManager.EnterDungeon);

            int choice = int.Parse(Console.ReadLine());

            if (choice == 2)// 입구에서 마을로 되돌아가기
            {
                Console.WriteLine(UtilTextManager.ExitDungeonEntrance);
                return;
            }
            PlayDungeon(Player);
        }

        void PlayDungeon(Player player)
        {
            // 던전 입장
            int count = 0;// 몬스터 등장 횟수
            int choice;
            for (int i = 0; i < GameManager.MonsterCount; i++)
            {
                Console.WriteLine(UtilTextManager.DungeonAppearedMonster[count]);

                ResultBattle result = StartBattle(player, monsters[count]);

                if (result == ResultBattle.RetreatPlayer) return;
                else if (result == ResultBattle.PlayerDie)
                {
                    Console.WriteLine(UtilTextManager.PlayerDead); return;
                }
                else
                {
                    Console.WriteLine($"{GameManager.Instance.monsters[count].Name}을 물리쳤습니다! " +
                        $"경험치 {GameManager.Instance.monsters[count].Exp}를 획득했습니다.\r\n");
                    player.GetExp(GameManager.Instance.monsters[count].Exp);
                }

                Console.WriteLine(UtilTextManager.NextStepChoice);

                choice = int.Parse(Console.ReadLine());

                if (choice == 1)
                {
                    Console.WriteLine(UtilTextManager.DungeonContinue[count]);
                }
                else if (choice == 2)
                {
                    Console.WriteLine(UtilTextManager.MoveTownAfterBattle);
                    return;
                }
                else
                {
                    Random random = new Random();

                    // 0~99 범위의 난수 생성
                    int randomValue = random.Next(0, 100);

                    if (randomValue < 45)
                    {
                        Item randitem = ItemManager.Instance.RandomCreateItem();
                        Console.WriteLine("당신은 주변을 탐색하던 중 희미하게 빛나는 물체를 발견했습니다.\r\n" +
                            $"가까이 다가가 확인하니, {randitem.Name}을(를) 발견했습니다!\r\n" +
                            "이 아이템은 당신의 여정에 큰 도움이 될 것입니다.");
                    }
                    else
                    {
                        Console.WriteLine("당신은 주변을 탐색했지만, 특별한 것을 발견하지 못했습니다.\r\n" +
                            "어둠 속에서는 아무것도 보이지 않으며, 조용히 다시 길을 준비합니다.");
                    }

                    Console.WriteLine(UtilTextManager.DungeonContinue[count]);
                }
                count++;
            }

            // 보스등장
            Console.WriteLine(UtilTextManager.AppearedBoss);

            ResultBattle resultBattle = GameManager.Instance.StartBattle(player, GameManager.Instance.Boss);

            if (resultBattle == ResultBattle.PlayerDie)
            {
                Console.WriteLine(UtilTextManager.PlayerDead); return;
            }
            else
            {
                Console.WriteLine($"{GameManager.Instance.Boss.Name}을 물리쳤습니다! " +
                    $"경험치 {GameManager.Instance.Boss.Exp}를 획득했습니다.\r\n");
                player.GetExp(GameManager.Instance.Boss.Exp);

                Console.WriteLine(UtilTextManager.ClearBoss);
            }
        }
    }
}
