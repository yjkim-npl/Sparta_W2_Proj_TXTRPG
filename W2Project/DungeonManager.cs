using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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

            int dungeonChoice;

            while (true)
            {
                string dungeonChoiceString = Console.ReadLine();

                if (int.TryParse(dungeonChoiceString, out dungeonChoice))
                {
                    if (dungeonChoice >= 0 && dungeonChoice <= 3)
                    {
                        break; // 올바른 입력이면 반복문을 빠져나옴
                    }
                    else
                    {
                        Console.SetCursorPosition(5, 22);
                        Console.WriteLine("잘못된 입력입니다. 0 ~ 3 사이의 숫자를 입력하세요.");
                        Console.SetCursorPosition(5, 23); Console.Write(">>     ");
                        Console.SetCursorPosition(8, 23);
                    }
                }
                else
                {
                    Console.SetCursorPosition(5, 22);
                    Console.WriteLine("잘못된 입력입니다. 0 ~ 3 사이의 숫자를 입력하세요.");
                    Console.SetCursorPosition(5, 23); Console.Write(">>     ");
                    Console.SetCursorPosition(8, 23);
                }
            }

            // 올바른 입력에 대한 처리
            switch (dungeonChoice)
            {
                case 1:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.SetCursorPosition(7, 7);
                    Console.WriteLine("1. 초급 던전을 선택했습니다.");
                    Console.SetCursorPosition(5, 22); Console.WriteLine("{0,-50}", " ");
                    Thread.Sleep(1000);
                    Type0 = true;
                    Console.ResetColor();
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.SetCursorPosition(7, 9);
                    Console.WriteLine("2. 중급 던전을 선택했습니다.");
                    Console.SetCursorPosition(5, 22); Console.WriteLine("{0,-50}", " ");
                    Thread.Sleep(1000);
                    Type1 = true;
                    Console.ResetColor();
                    break;
                case 3:
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.SetCursorPosition(7, 11);
                    Console.WriteLine("3. 상급 던전을 선택했습니다.");
                    Console.SetCursorPosition(5, 22); Console.WriteLine("{0,-50}", " ");
                    Thread.Sleep(1000);
                    Type2 = true;
                    Console.ResetColor();
                    break;
                case 0:
                    BaseScene();
                    Console.SetCursorPosition(5, 5);
                    Console.WriteLine("당신은 던전에서 빠져나왔다!");
                    break;
                default:
                    Console.SetCursorPosition(5, 22);
                    Console.WriteLine("잘못된 입력입니다. 0부터 3 사이의 숫자를 입력하세요.");
                    break;
            }
            Console.SetCursorPosition(8, 23);

            if (dungeonChoice >= 1 && dungeonChoice <= 3)
            {
                DungeonManager dungeonManager = DungeonManager.Instance;
                Battle battle = new Battle();
                battle.BattlePhase();
            }
        }
    
    public void BaseScene() // 기본 UI Mark.1
    {
        Console.Clear();
        Console.WriteLine("--------------------------------------------------------------------------------------------");
        Console.SetCursorPosition(0, 29);
        Console.WriteLine("--------------------------------------------------------------------------------------------");
    }

    public void DungeonResult() // 던전 보상 메소드
    {
        BaseScene();

        Console.SetCursorPosition(5, 3);
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Console.WriteLine("[던전 결과]");
        Console.ResetColor();

        int DungeonCheck = Type0 ? 0 : Type1 ? 1 : Type2 ? 2 : -1;

        if (Type0 = true) // 초급 던전
        {
            int playerBeforeGold = Player.instance.GetStatusInt(Player.Status.GOLD);
            int playerBeforeLVL = Player.instance.GetStatusInt(Player.Status.LVL); //플레이어 획득 전 레벨
            int playerBeforeEXP = Player.instance.GetStatusInt(Player.Status.EXP); //플레이어 획득 전 경험치
            Player.instance.AddGold(500);
            Player.instance.AddExp(30);
            Console.SetCursorPosition(7, 5); Console.WriteLine("- 획득 보상 -");
            Console.SetCursorPosition(7, 7); Console.WriteLine("[500Gold]].");
            Console.SetCursorPosition(7, 9); Console.WriteLine("[30EXP].");
            int playerAfterGold = Player.instance.GetStatusInt(Player.Status.GOLD); //플레이어 획득 후 골드
            int playerAfterLVL = Player.instance.GetStatusInt(Player.Status.LVL); //플레이어 획득 후 레벨
            int playerAfterEXP = Player.instance.GetStatusInt(Player.Status.EXP); //플레이어 획득 전 경험치
            Console.SetCursorPosition(7, 14); Console.WriteLine("{0,-60}", $"획득 경험치 : Lv.{playerBeforeLVL} EXP : {playerBeforeEXP} -> Lv.{playerAfterLVL} EXP : {playerAfterEXP}"); // 결과
            Console.SetCursorPosition(7, 12); Console.WriteLine("{0,-60}", $"획득 골드 : {playerBeforeGold} -> {playerAfterGold}"); // 결과

            DungeonManager.Instance.Type0 = false;
            DungeonManager.Instance.Type1 = false;
            DungeonManager.Instance.Type2 = false;
        }
        else if (Type1 = true) // 중급 던전
        {
            int playerBeforeGold = Player.instance.GetStatusInt(Player.Status.GOLD);
            int playerBeforeLVL = Player.instance.GetStatusInt(Player.Status.LVL); //플레이어 획득 전 레벨
            int playerBeforeEXP = Player.instance.GetStatusInt(Player.Status.EXP); //플레이어 획득 전 경험치
            Player.instance.AddGold(1000);
            Player.instance.AddExp(50);
            Console.SetCursorPosition(7, 5); Console.WriteLine("- 획득 보상 -");
            Console.SetCursorPosition(7, 7); Console.WriteLine("[1000Gold]].");
            Console.SetCursorPosition(7, 9); Console.WriteLine("[50EXP].");
            int playerAfterGold = Player.instance.GetStatusInt(Player.Status.GOLD); //플레이어 획득 후 골드
            int playerAfterLVL = Player.instance.GetStatusInt(Player.Status.LVL); //플레이어 획득 후 레벨
            int playerAfterEXP = Player.instance.GetStatusInt(Player.Status.EXP); //플레이어 획득 전 경험치
            Console.SetCursorPosition(7, 14); Console.WriteLine("{0,-60}", $"획득 경험치 : Lv.{playerBeforeLVL} EXP : {playerBeforeEXP} -> Lv.{playerAfterLVL} EXP : {playerAfterEXP}"); // 결과
            Console.SetCursorPosition(7, 12); Console.WriteLine("{0,-60}", $"획득 골드 : {playerBeforeGold} -> {playerAfterGold}"); // 결과

            DungeonManager.Instance.Type0 = false;
            DungeonManager.Instance.Type1 = false;
            DungeonManager.Instance.Type2 = false;
        }
        else if (Type2 = true) // 상급 던전
        {
            int playerBeforeGold = Player.instance.GetStatusInt(Player.Status.GOLD);
            int playerBeforeLVL = Player.instance.GetStatusInt(Player.Status.LVL); //플레이어 획득 전 레벨
            int playerBeforeEXP = Player.instance.GetStatusInt(Player.Status.EXP); //플레이어 획득 전 경험치
            Player.instance.AddGold(2000);
            Player.instance.AddExp(100);
            Console.SetCursorPosition(7, 5); Console.WriteLine("- 획득 보상 -");
            Console.SetCursorPosition(7, 7); Console.WriteLine("[2000Gold]].");
            Console.SetCursorPosition(7, 9); Console.WriteLine("[100EXP].");
            int playerAfterGold = Player.instance.GetStatusInt(Player.Status.GOLD); //플레이어 획득 후 골드
            int playerAfterLVL = Player.instance.GetStatusInt(Player.Status.LVL); //플레이어 획득 후 레벨
            int playerAfterEXP = Player.instance.GetStatusInt(Player.Status.EXP); //플레이어 획득 전 경험치
            Console.SetCursorPosition(7, 14); Console.WriteLine("{0,-60}", $"획득 경험치 : Lv.{playerBeforeLVL} EXP : {playerBeforeEXP} -> Lv.{playerAfterLVL} EXP : {playerAfterEXP}"); // 결과
            Console.SetCursorPosition(7, 12); Console.WriteLine("{0,-60}", $"획득 골드 : {playerBeforeGold} -> {playerAfterGold}"); // 결과

            DungeonManager.Instance.Type0 = false;
            DungeonManager.Instance.Type1 = false;
            DungeonManager.Instance.Type2 = false;
        }
        else
        {
            Console.WriteLine("버그 발생");
        }
    }
}
}

