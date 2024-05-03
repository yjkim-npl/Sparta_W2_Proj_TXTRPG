using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace W2Project
{
    public class Monster
    {
        string fName;
        int fID;
        int fLvl;
        MonsterType fType;
        int fAtk;
        int fDef;
        int fHP;
        int fGold;
        int fExp;
        public Monster()
        {
            fName = "";
            fID = 0;
            fLvl = 0;
            fType = MonsterType.None;
            fAtk = 0;
            fDef = 0;
            fHP = 0;
            fGold = 0;
            fExp = 0;
        }
        //        public Monster(int id)
        //        {
        ////            fName = 
        //        }
        public Monster(string name, int id, int lvl, int typeNo, int atk, int def, int hp, int gold, int exp)
        {
            fName = name;
            fID = id;
            fLvl = lvl;
            switch (typeNo)
            {
                case 0:
                    fType = MonsterType.Mob;
                    break;
                case 1:
                    fType = MonsterType.Boss;
                    break;
                default:
                    fType = MonsterType.None;
                    break;

            }
            fAtk = atk;
            fDef = def;
            fHP = hp;
            fGold = gold;
            fExp = exp;
            if (!File.Exists($"../../../../Assets/Monsters/{fID}.dat"))
            {
                StreamWriter sw = new StreamWriter($"../../../../Assets/Monsters/{fID}.dat");
                sw.WriteLine(fName);
                sw.WriteLine(fID);
                sw.WriteLine(fLvl);
                sw.WriteLine(fType);
                sw.WriteLine(fAtk);
                sw.WriteLine(fDef);
                sw.WriteLine(fHP);
                sw.WriteLine(fGold);
                sw.WriteLine(fExp);
            }
        }
        public string GetName() { return fName; }
        public int GetID() { return fID; }
        public int GetLvl() { return fLvl; }
        public MonsterType GetType() { return fType; }
        public int GetAtk() { return fAtk; }
        public int GetDef() { return fDef; }
        public int GetHP() { return fHP; }
        public int GetGold() { return fGold; }
        public int GetExp() { return fExp; }
    }
        public enum MonsterType
        {
            None,
            Mob,
            Boss
        }
    }
