using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Net.WebRequestMethods;

namespace W2Project
{
    internal class Battle
    {
        Player player = Player.instance;

        public void BattlePhase()
        {
            Console.Clear();

            BaseScene();

            Console.SetCursorPosition(5, 3);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("[배틀]");
            Console.ResetColor();

            Console.SetCursorPosition(5, 3);
            

            bool continueEncounter = true;

            while (!player.Dead() && continueEncounter)
            {
                int numMonsters = new Random().Next(1, 4);

                for (int i = 0; i < numMonsters; i++)
                {
                    Enemy enemy = GenerateRandomEnemy();

                    Console.WriteLine($"야생의 {enemy.Name}가 나타났다!");

                    Console.SetCursorPosition(5, 5);
                    Console.WriteLine($"현재 체력 : {Player.instance.GetStatusInt(Player.Status.HP)}, 몬스터의 현재 체력 : {enemy.Health}");
                    Console.SetCursorPosition(5, 7); Console.WriteLine("1. 공격");
                    Console.SetCursorPosition(5, 8); Console.WriteLine("2. 도망");

                    Console.SetCursorPosition(5, 23); Console.Write(">>  ");
                    Console.SetCursorPosition(8, 23);


                    string input = Console.ReadLine();

                    if (input == "1")
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.SetCursorPosition(7, 10); Console.WriteLine("플레이어의 공격!");
                        Console.ResetColor();
                        enemy.Damage(Player.instance.GetStatusInt(Player.Status.ATK));
                        Console.SetCursorPosition(7, 11); Console.WriteLine("몬스터의 현재 체력 : " + enemy.Health);

                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.SetCursorPosition(7, 13); Console.WriteLine("몬스터의 공격!");
                        Console.ResetColor();
                        Player.instance.Damage(enemy.Attack);
                        Console.SetCursorPosition(7, 14); Console.WriteLine("플레이어의 현재 체력 : " + Player.instance.GetStatusInt(Player.Status.HP));

                        if (enemy.Health <= 0)
                        {
                            Console.SetCursorPosition(7, 10); Console.WriteLine("당신은 승리했다!");
                            Player.instance.AddGold(10);
                            break;
                        }
                    }
                    else if (input == "2")
                    {
                        Console.Clear();
                        BaseScene();
                        Console.SetCursorPosition(7, 7); Console.WriteLine("당신은 도망쳤다!");
                        continueEncounter = false;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("잘못된 입력입니다");
                    }
                    Console.WriteLine();
                }
            }

        }
        public void BaseScene()
        {
            Console.Clear();
            Console.WriteLine("--------------------------------------------------------------------------------------------");
            Console.SetCursorPosition(0, 29);
            Console.WriteLine("--------------------------------------------------------------------------------------------");
        }
        public class Enemy
        {
            public string Name { get; set; }
            public int Health { get; set; }
            public int Attack { get; set; }
            public Enemy(string name, int health, int attack)
            {
                Name = name;
                Health = health;
                Attack = attack;
            }
            public void Damage(int amount)
            {
                Health -= amount;
                if (Health < 0)
                    Health = 0;
            }
        }

        public static Enemy GenerateRandomEnemy()
        {
            var enemies = new List<(string name, int Health, int attack)>
            {
            ("고블린", 20, 10),
            ("오크", 40, 15),
            ("스켈레톤", 25,8)
            };

            var randomEnemy = enemies[new Random().Next(0, enemies.Count)];
            return new Enemy(randomEnemy.name, randomEnemy.Health, randomEnemy.attack);
        }
    }
}
//Monster enemy = new Monster { Name = "Goblin", Health = 50, Attack = 5 };



//void AttackTarget(Monster target)
//{
//    Console.WriteLine($"{Player.instance.GetStatusStr(Player.Status.NAME)}의 공격! \n{target.Name} for {Player.instance.GetStatusInt(Player.Status.ATK)} damage!");
//    target.Health -= Player.instance.GetStatusInt(Player.Status.ATK); 
//}

//void AttackCharacter()
//{
//    int playerHP = Player.instance.GetStatusInt(Player.Status.HP);
//    int enemyAttack = enemy.Attack;

//    // 공격 전에 플레이어의 체력이 0보다 큰지 확인
//    if (playerHP > 0)
//    {
//        // 적의 공격력을 플레이어의 체력에서 빼기
//        Player.instance.GetStatusInt(Player.Status.HP) = Mathf.Max(0, playerHP - enemyAttack);
//    }
//}


//    public static Enemy GenerateRandomEnemy()
//    {
//        // Define different types of enemies with their properties
//        var enemies = new List<(string name, int minHealth, int maxHealth, int attack)>
//{
//    ("Goblin", 20, 30, 10),
//    ("Orc", 30, 40, 15),
//    ("Skeleton", 15, 25, 8)
//    // Add more enemies as needed
//};

//        // Randomly select an enemy type
//        var randomEnemy = enemies[new Random().Next(0, enemies.Count)];

//        // Create and return the enemy object
//        return new Enemy(randomEnemy.name, randomEnemy.minHealth, randomEnemy.maxHealth, randomEnemy.attack);
//    }