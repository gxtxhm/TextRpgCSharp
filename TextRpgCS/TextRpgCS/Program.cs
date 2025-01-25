using System;
using TextRpgCS;

class Program
{

    static void Main(string[] args)
    {

        // main 
        GameManager.Instance.Init();
        GameManager.Instance.PlayStartScene();
        while (true)
        {
            GameManager.Instance.PrintMainMenu();
            int choiceMenu = int.Parse(Console.ReadLine());
            Console.WriteLine("\n");
            switch (choiceMenu)
            {
                case 1:
                    GameManager.Instance.MoveTown();
                    break;
                case 2:
                    GameManager.Instance.MoveDungeon();
                    GameManager.Instance.ResetMonter();
                    break;
                case 3:
                    GameManager.Instance.PrintPlayerInfo();
                    break;
                case 4:
                    GameManager.Instance.ExitGame();
                    break;
            }

        }
    }
};
