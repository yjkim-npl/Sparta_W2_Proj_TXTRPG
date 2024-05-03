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
        public int Type { get; set; }
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
            Type = enemyType;
            //switch (enemyType)
            //{
            //    case 0:
            //        Type = EnemyType.None;
            //        break;
            //    case 1:
            //        Type = EnemyType.Mob;
            //        break;
            //    case 2:
            //        Type = EnemyType.Boss;
            //        break;
            //}
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

    class EnemyManager
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

            // 필요한 초기화 작업 수행 
            //(string name, int id, int lvl, int enemyType, int attack, int def, int health, int gold, int exp)

            type0Enemies.Add(new Enemy("Test1", 1, 1, 0, 10, 5, 20, 5, 10));
            type0Enemies.Add(new Enemy("Test2", 2, 1, 0, 12, 6, 22, 7, 12));
            type0Enemies.Add(new Enemy("Test3", 3, 2, 0, 15, 8, 30, 10, 15));

            type1Enemies.Add(new Enemy("Test mob1", 4, 2, 1, 15, 8, 30, 10, 15));
            type1Enemies.Add(new Enemy("Test mob2", 5, 2, 1, 18, 10, 35, 12, 20));
            type1Enemies.Add(new Enemy("Test mob3", 6, 3, 1, 30, 15, 100, 50, 100));

            type2Enemies.Add(new Enemy("Test boss1", 7, 3, 2, 30, 15, 100, 50, 100));
            type2Enemies.Add(new Enemy("Test boss2", 8, 3, 2, 35, 18, 120, 60, 120));
            type2Enemies.Add(new Enemy("Test boss3", 9, 4, 2, 40, 20, 140, 70, 140));
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
            Mob,
            Boss,
            None
        }
    
