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
        public Enemy(string name, int id, int lvl, int enemyType, int attack, int def, int health, int gold, int exp)
        {
            Name = name;
            ID = id;
            Lvl = lvl;
            switch (enemyType)
            {
                case 0:
                    Type = EnemyType.Mob;
                    break;
                case 1:
                    Type = EnemyType.Boss;
                    break;
                default:
                    Type = EnemyType.None;
                    break;

            }
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


    //class EnemyCsv
    //{

    //    public static void Main(string[] args)
    //    {
    //        const string enemyFile = "../../../../Assets/EnemyList.csv";
    //        int index = 0;
    //        List<Enemy> EnemyList = new List<Enemy>();
    //        using (StreamReader sr = new StreamReader(new FileStream(enemyFile, FileMode.Open)))
    //        {
    //            while (false == sr.EndOfStream)
    //            {
    //                string readStr = sr.ReadLine();
    //                if (index++ >= 1)
    //                {
    //                    string[] splitData = readStr.Split(',');

    //                    Enemy temp = new Enemy();                       
    //                    temp.Name = splitData[0];
    //                    temp.ID = Convert.ToInt32(splitData[1]);
    //                    temp.Lvl = Convert.ToInt32(splitData[2]);
    //                    temp.Type = (EnemyType)Convert.ToInt32(splitData[3]);
    //                    temp.Attack = Convert.ToInt32(splitData[4]);
    //                    temp.Def = Convert.ToInt32(splitData[5]);
    //                    temp.Health = Convert.ToInt32(splitData[6]);
    //                    temp.Gold = Convert.ToInt32(splitData[7]);
    //                    temp.Exp = Convert.ToInt32(splitData[8]);
    //                    EnemyList.Add(temp);
    //                }
    //            }
    //        }
    //        foreach (var d in EnemyList)
    //        {
    //            Console.WriteLine("{0} {1} {2} {3} {4} {5} {6} {7} {8} {9}", d.Name, d.ID, d.Lvl, d.Type, d.Attack, d.Def, d.Health, d.Gold, d.Exp);
    //            Console.WriteLine();
    //        }
    //        string str = "0, 1, 2, 3, 4, 5, 6, 7, 8, 9";
    //        string[] splitRead = str.Split(',');
    //        for (int i = 0; i < splitRead.Length; i++)
    //        {
    //            Console.Write("{0}", splitRead[i]);
    //        }
    //    }

    }



public enum EnemyType
        {
            Mob,
            Boss,
            None
        }
    
