using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace W2Project
{
    internal class DungeonManager
    {
        private static DungeonManager instance;
        public static DungeonManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DungeonManager();
                }
                return instance;
            }
        }

        public bool Type0 { get; set; } = false;
        public bool Type1 { get; set; } = false;
        public bool Type2 { get; set; } = false;

        public void DungeonPhase()
        {
            BaseScene();

            Console.SetCursorPosition(5, 3);
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("[던전]");
            Console.ResetColor();

            Console.SetCursorPosition(5, 5); Console.WriteLine("던전을 선택해주세요.");
            Console.SetCursorPosition(7, 7); Console.WriteLine("1. 초급 던전");
            Console.SetCursorPosition(7, 9); Console.WriteLine("2. 중급 던전");
            Console.SetCursorPosition(7, 11); Console.WriteLine("3. 상급 던전");

            Console.SetCursorPosition(5, 19); Console.WriteLine("0. 나가기");
            Console.SetCursorPosition(5, 23); Console.Write(">>     ");
            Console.SetCursorPosition(8, 23);

            string dungeonChoiceString = Console.ReadLine();

            int dungeonChoice;
            if (int.TryParse(dungeonChoiceString, out dungeonChoice))

            {
                switch (dungeonChoice)
                {
                    case 1:
                        Console.SetCursorPosition(5, 13); Console.WriteLine("1. 초급 던전을 선택했습니다.");
                        Type0 = true;
                        break;
                    case 2:
                        Console.SetCursorPosition(5, 14); Console.WriteLine("2. 중급 던전을 선택했습니다.");
                        Type1 = true;
                        break;
                    case 3:
                        Console.SetCursorPosition(5, 15); Console.WriteLine("3. 상급 던전을 선택했습니다.");
                        Type2 = true;
                        break;
                    case 0:
                        return; // 종료
                    default:
                        Console.WriteLine("잘못된 입력입니다. 0부터 3 사이의 숫자를 입력하세요.");
                        break;
                }
                Console.SetCursorPosition(8, 23);

                // 선택된 던전에 따라 전투 시작
                if (dungeonChoice >= 1 && dungeonChoice <= 3)
                {
                    DungeonManager dungeonManager = DungeonManager.Instance;
                    Battle battle = new Battle();
                    battle.BattlePhase();
                }
            }
        }
        public void BaseScene() // 기본 UI Mark.1
        {
            Console.Clear();
            Console.WriteLine("--------------------------------------------------------------------------------------------");
            Console.SetCursorPosition(0, 29);
            Console.WriteLine("--------------------------------------------------------------------------------------------");
        }
    }
}
