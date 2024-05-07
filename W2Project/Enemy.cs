using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;
using static W2Project.Battle;
using static System.Net.WebRequestMethods;
using System.Security.Cryptography;
using System.Xml.Linq;
using System.Collections;
using System.Reflection;
using System.Data;
using static W2Project.Player;


namespace W2Project
{
    public class Enemy
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public int Lvl { get; set; }
        public EnemyType Type { get; set; }
        public int Attack { get; set; }
        public int Def { get; set; }
        public int Health { get; set; }
        public int Gold { get; set; }
        public int Exp { get; set; }
        public bool IsDead { get; private set; } = false;

        public Enemy(string name, int id, int lvl, EnemyType type, int attack, int def, int health, int gold, int exp)
        {
            Name = name;
            ID = id;
            Lvl = lvl;
            Type = type;
            Attack = attack;
            Def = def;
            Health = health;
            Gold = gold;
            Exp = exp;
        }

        public void Damage(int amount)
        {
            Health -= amount;
            if (Health < 0)
                Health = 0;
        }

        public void Die()
        {
            IsDead = true;
        }
    }


    public class EnemyCsv
    {
        public static List<Enemy> LoadEnemiesFromCsv(string filePath)
        {
            List<Enemy> enemiesList = new List<Enemy>();

            using (StreamReader sr = new StreamReader(filePath))
            {
                sr.ReadLine();

                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] fields = line.Split(',');

                    string name = fields[0];
                    int id = int.Parse(fields[1]);
                    int lvl = int.Parse(fields[2]);
                    EnemyType type = (EnemyType)Enum.Parse(typeof(EnemyType), fields[3]);
                    int attack = int.Parse(fields[4]);
                    int def = int.Parse(fields[5]);
                    int health = int.Parse(fields[6]);
                    int gold = int.Parse(fields[7]);
                    int exp = int.Parse(fields[8]);

                    Enemy enemy = new Enemy(name, id, lvl, type, attack, def, health, gold, exp);
                    enemiesList.Add(enemy);
                }
            }

            return enemiesList;
        }
    }

    public class EnemyManager
    {
        private List<Enemy> type0Enemies;
        private List<Enemy> type1Enemies;
        private List<Enemy> type2Enemies;
        // 필요에 따라 다른 타입의 리스트 추가 가능

        public EnemyManager()
        {
            type0Enemies = new List<Enemy>();
            type1Enemies = new List<Enemy>();
            type2Enemies = new List<Enemy>();

            List<Enemy> enemiesList = EnemyCsv.LoadEnemiesFromCsv($"../../../../Assets/EnemyList.csv");

            // Add enemies to corresponding lists based on their types
            foreach (var enemy in enemiesList)
            {
                switch (enemy.Type)
                {
                    case EnemyType.None:
                        type0Enemies.Add(enemy);
                        break;
                    case EnemyType.Mob:
                        type1Enemies.Add(enemy);
                        break;
                    case EnemyType.Boss:
                        type2Enemies.Add(enemy);
                        break;
                    default:
                        throw new ArgumentException("Invalid enemy type.");
                }
            }
        }
        public Enemy TypeEnemy(int type)
        {
            Random random = new Random();
            List<Enemy> selectedEnemies = GetEnemyListByType(type);
            if (selectedEnemies.Count == 0)
            {
                throw new ArgumentException("해당 타입의 몬스터가 없습니다.");
            }

            int randomIndex = random.Next(0, selectedEnemies.Count);

            Enemy selectedEnemy = selectedEnemies[randomIndex];
            return new Enemy(selectedEnemy.Name, selectedEnemy.ID, selectedEnemy.Lvl, selectedEnemy.Type, selectedEnemy.Attack, selectedEnemy.Def, selectedEnemy.Health, selectedEnemy.Gold, selectedEnemy.Exp);
        }

        private List<Enemy> GetEnemyListByType(int type)
        {
            switch (type)
            {
                case 0:
                    return type0Enemies;
                case 1:
                    return type1Enemies;
                case 2:
                    return type2Enemies;
                // 다른 타입에 대한 처리 추가 가능
                default:
                    throw new ArgumentException("잘못된 적 유형입니다.");
            }
        }
    }
}
public enum EnemyType
{
    None,
    Mob,
    Boss
}

