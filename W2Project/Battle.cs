﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using static System.Net.WebRequestMethods;
using static W2Project.Player;

namespace W2Project
{
    internal class Battle
    {
        Player player = Player.instance; // 플레이어 인스턴스
        private Random random = new Random(); // 랜덤 몬스터
        private List<Enemy> enemiesList; // 리스트 불러오기 
        private const int MIN_MONSTERS = 1; // 몬스터 최소 1마리
        private const int MAX_MONSTERS = 3; // 몬스터 최대 3마리
        private int totalGoldEarned = 0; // 플레이어가 얻는 총 골드
        private int totalExpEarned = 0; // 플레이어가 얻는 총 경험치

        public void BattlePhase()
        {
            BaseScene();
            Console.SetCursorPosition(5, 5); Console.WriteLine("몬스터 무리가 나타났다!");

            bool continueEncounter = true; // 전투 지속 여부
            enemiesList = new List<Enemy>(); // 리스트 초기화

            int numMonsters = random.Next(MIN_MONSTERS, MAX_MONSTERS + 1); // 전투가 시작될 때 몬스터 최소 최대 수 생성

            for (int i = 0; i < numMonsters; i++)
            {
                Enemy enemy = GenerateRandomEnemy();
                enemiesList.Add(enemy);
            }

            while (!player.Dead() && continueEncounter) // 플레이어가 죽지 않았고, 전투가 지속중일 때,
            {

                BaseBattleScene(); // 기본 배틀 UI 양식 불러오기
                monsterHuntList(); // 몬스터 리스트 불러오기

                Console.SetCursorPosition(8, 23); string input = Console.ReadLine();
                Console.SetCursorPosition(8, 23);

                if (input == "1")
                {
                    Console.SetCursorPosition(5, 19); Console.WriteLine("{0,-45}", " ");
                    Console.SetCursorPosition(5, 20); Console.WriteLine("{0,-45}", " ");
                    Console.SetCursorPosition(5, 22); Console.WriteLine("{0,-45}", "몬스터 번호를 입력하세요. ");
                    Console.SetCursorPosition(5, 23); Console.Write(">>     ");
                    Console.SetCursorPosition(40, 12); Console.Write("             ");
                    Console.SetCursorPosition(8, 23);

                    int monsterChoice;
                    string monsterChoiceString = Console.ReadLine();
                    bool isValidInput = int.TryParse(monsterChoiceString, out monsterChoice);

                    if (isValidInput && monsterChoice >= 1 && monsterChoice <= enemiesList.Count)
                    {
                        // 플레이어의 턴
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.SetCursorPosition(7, 11); Console.WriteLine("플레이어의 공격!");
                        Console.ResetColor();

                        Enemy enemy = enemiesList[monsterChoice - 1];

                        if (enemy.Health <= 0)
                        {
                            BaseScene();
                            Console.SetCursorPosition(5, 22); Console.WriteLine("이미 죽은 몬스터입니다.");
                            continue; // 반복문 상단으로 이동하여 다시 입력 요청
                        }

                        Console.SetCursorPosition(5, 22); Console.WriteLine("                               ");
                        int playerDamage = CriticalAttackDamage();
                        int enemyHealthBeforeAttack = enemy.Health; // 몬스터 공격 받기 전 체력
                        enemy.Damage(playerDamage);
                        int enemyHealthAfterAttack = enemy.Health; // 몬스터 공격 받은 후 체력
                        Console.SetCursorPosition(7, 12); Console.WriteLine("{0,-25}", $"몬스터의 현재 체력 : {enemyHealthBeforeAttack} -> {enemyHealthAfterAttack}"); // 결과
                    }
                    else
                    {
                        // 잘못된 입력이거나 몬스터 번호가 잘못된 경우
                        string message = enemiesList.Count == 1 ? "1에서" : $"1 ~ {enemiesList.Count}에서";
                        Console.SetCursorPosition(5, 22); Console.WriteLine($"{message} 골라주세요         ");
                        continue;
                    }

                    // 몬스터의 턴
                    foreach (var enemy in enemiesList)
                    {
                        if (enemy.Health <= 0)
                        {
                            int monsterGoldEarned = enemy.Gold; // 몬스터가 제공하는 골드
                            int monsterExpEarned = enemy.Exp; // 몬스터가 제공하는 경험치
                            totalGoldEarned += monsterGoldEarned; // 플레이어가 얻는 총 골드에 누적
                            totalExpEarned += monsterExpEarned; // 플레이어가 얻는 총 경험치에 누적
                            continue;
                        }

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.SetCursorPosition(7, 14); Console.WriteLine("몬스터의 공격!");
                        Console.ResetColor();

                        int playerHealthBeforeAttack = Player.instance.GetStatusInt(Player.Status.HP); //플레이어 공격 받기 전 체력
                        int PlayerDeffence = Player.instance.GetStatusInt(Player.Status.DEF);
                        int damage = enemy.Attack - PlayerDeffence;
                        damage = Math.Max(0, damage);
                        Player.instance.Damage(damage);
                        int playerHealthAfterAttack = Player.instance.GetStatusInt(Player.Status.HP); // 플레이어 공격 받은 후 체력

                        Console.SetCursorPosition(7, 15);
                        Console.WriteLine("{0,-60}", $"플레이어의 체력 : {playerHealthBeforeAttack} -> {playerHealthAfterAttack}"); // 결과

                        if (Player.instance.GetStatusInt(Player.Status.HP) <= 0)
                        {
                            continueEncounter = false; // 전투 종료
                            BattleFailureResult();
                            return;
                        }
                    }
                    bool allEnemiesDead = true; // 승리 조건
                    foreach (var enemy in enemiesList)
                    {
                        if (enemy.Health > 0) // 모든 몬스터가 죽었을 때,
                        {
                            allEnemiesDead = false; // false 값을 넣음
                            break;
                        }
                    }

                    if (allEnemiesDead) //승리
                    {
                        Console.WriteLine("전투에서 승리하였습니다!");
                        BattleClearResult();
                        return;
                    }

                }
                else if (input == "0")
                {
                    BaseScene();
                    Console.SetCursorPosition(5, 5); Console.WriteLine("당신은 도망쳤다!");
                    return;
                }
                else
                {
                    // 잘못된 입력이거나 몬스터 번호가 잘못된 경우
                    Console.SetCursorPosition(5, 22); Console.WriteLine("0 ~ 1 에서 골라주세요         ");
                    continue;
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

        public void BaseBattleScene() // 기본 UI Mark.2
        {
            Console.SetCursorPosition(5, 3);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("[배틀]");
            Console.ResetColor();
            Console.SetCursorPosition(5, 17);
            Console.WriteLine("{0,-60}", $"현재 체력 : {Player.instance.GetStatusInt(Player.Status.HP)}");
            Console.SetCursorPosition(5, 19); Console.WriteLine("1. 몬스터 공격");
            Console.SetCursorPosition(5, 20); Console.WriteLine("0. 도망");
            Console.SetCursorPosition(5, 23); Console.Write(">>      ");
            Console.SetCursorPosition(8, 23);
        }

        public void BattleClearResult() // 승리 메소드
        {
            Console.Clear();
            BaseScene();

            Console.SetCursorPosition(5, 3);
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("[배틀 결과]");
            Console.ResetColor();

            Console.SetCursorPosition(5, 5); Console.WriteLine("전투에서 승리하였습니다!");
            int playerBeforeGold = Player.instance.GetStatusInt(Player.Status.GOLD); //플레이어 획득 전 골드
            Player.instance.AddGold(totalGoldEarned); // 플레이어에게 총 골드 적용
            int playerAfterGold = Player.instance.GetStatusInt(Player.Status.GOLD); //플레이어 획득 후 골드
            Console.SetCursorPosition(7, 12); Console.WriteLine("{0,-60}", $"획득 골드 : {playerBeforeGold} -> {playerAfterGold}"); // 결과

            int playerBeforeLVL = Player.instance.GetStatusInt(Player.Status.LVL); //플레이어 획득 전 레벨
            int playerBeforeEXP = Player.instance.GetStatusInt(Player.Status.EXP); //플레이어 획득 전 경험치
            Player.instance.AddExp(totalExpEarned); // 플레이어에게 총 경험치 적용
            int playerAfterLVL = Player.instance.GetStatusInt(Player.Status.LVL); //플레이어 획득 후 레벨
            int playerAfterEXP = Player.instance.GetStatusInt(Player.Status.EXP); //플레이어 획득 전 경험치
            Console.SetCursorPosition(7, 13); Console.WriteLine("{0,-60}", $"획득 경험치 : Lv.{playerBeforeLVL} EXP : {playerBeforeEXP} -> Lv.{playerAfterLVL} EXP : {playerAfterEXP}" + $" (+{totalExpEarned})"); // 결과
        }

        public void BattleFailureResult() // 패배 메소드
        {
            Console.Clear();
            BaseScene();

            Console.SetCursorPosition(5, 3);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("[배틀 결과]");
            Console.ResetColor();
            int playerBeforeGold = Player.instance.GetStatusInt(Player.Status.GOLD); //플레이어 획득 전 골드
            int playerBeforeEXP = Player.instance.GetStatusInt(Player.Status.EXP); //플레이어 획득 전 경험치

            Player.instance.Dead();
            Console.SetCursorPosition(5, 5); Console.WriteLine("당신은 사망했습니다.");

            int playerAfterGold = Player.instance.GetStatusInt(Player.Status.GOLD); //플레이어 획득 후 골드
            Console.SetCursorPosition(7, 12); Console.WriteLine("{0,-60}", $"획득 골드 : {playerBeforeGold} -> {playerAfterGold}"); // 결과
            int playerBeforeLVL = Player.instance.GetStatusInt(Player.Status.LVL); //플레이어 획득 전 레벨

            int playerAfterLVL = Player.instance.GetStatusInt(Player.Status.LVL); //플레이어 획득 후 레벨
            int playerAfterEXP = Player.instance.GetStatusInt(Player.Status.EXP); //플레이어 획득 전 경험치
            Console.SetCursorPosition(7, 13); Console.WriteLine("{0,-60}", $"획득 경험치 : Lv.{playerBeforeLVL} EXP : {playerBeforeEXP} -> Lv.{playerAfterLVL} EXP : {playerAfterEXP}" + $" (+{totalExpEarned})"); // 결과
        }

        public int CriticalAttackDamage()
        {
            int baseDamage = Player.instance.GetStatusInt(Status.ATK);
            bool isCritical = IsCriticalHit();
            if (IsCriticalHit())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition(40, 12); Console.WriteLine("치명타!");
                baseDamage = (int)Math.Round(baseDamage * 1.6); // 치명타가 발생하면 공격력을 1.6 배로 증가
                Console.ResetColor();
            }
            return baseDamage;
        }

        private bool IsCriticalHit() // 치명타 발생 확률 설정
        {
            int criticalChance = 15; // 15%
            return random.Next(100) < criticalChance;
        }

        public void monsterHuntList()
        {
            for (int j = 0; j < enemiesList.Count; j++)
            {
                Enemy enemy = enemiesList[j];

                Console.SetCursorPosition(5, 7 + j);
                Console.WriteLine("{0,-60}", $"몬스터 {j + 1}: {(enemy.Health > 0 ? enemy.Name : "DEAD")}, 체력: {enemy.Health}");
            }
        }

        public Enemy GenerateRandomEnemy()
        {
            var enemies = new List<(string name, int id, int lvl, int typeNo, int attack, int def, int health, int gold, int exp)>
            {
            ("미니언", 1, 2, 0, 5, 5,15,15,10),
            ("공허충", 1, 3, 0, 9, 5,10,30,15),
            ("대포미니언", 1, 2, 1, 8, 10,25,100,50),
            };
            var randomEnemy = enemies[new Random().Next(0, enemies.Count)];
            return new Enemy(randomEnemy.name, randomEnemy.id, randomEnemy.lvl, randomEnemy.typeNo, randomEnemy.attack, randomEnemy.def, randomEnemy.health, randomEnemy.gold, randomEnemy.exp);
        }

    }
}