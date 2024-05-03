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
        List<Enemy> enemiesList = EnemyCsv.LoadEnemiesFromCsv("../../../../Assets/EnemyList.csv");
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

                    string name = fields[1];
                    int id = int.Parse(fields[2]);
                    int lvl = int.Parse(fields[3]);
                    EnemyType type = (EnemyType)Enum.Parse(typeof(EnemyType), fields[4]);
                    int attack = int.Parse(fields[5]);
                    int def = int.Parse(fields[6]);
                    int health = int.Parse(fields[7]);
                    int gold = int.Parse(fields[8]);
                    int exp = int.Parse(fields[9]);

                    // 몬스터 객체를 생성하여 리스트에 추가합니다.
                    Enemy enemy = new Enemy(name, id, lvl, type, attack, def, health, gold, exp);
                    enemiesList.Add(enemy);
                }
            }

            return enemiesList;
        }
    }


    public enum EnemyType
    {
        Mob,
        Boss,
        None
    }
}
