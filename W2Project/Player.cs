﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W2Project
{
    public class Player
    {
        public static Player instance;
        int fLvl;
        string fName;
        string fJob;
        int fAtk;
        int fBonusAtk;
        int fDef;
        int fBonusDef;
        int fHP;
        int fBonusHP;
        int fGold;
        int fExp;
        int fMaxExp;

        // hope to be use vector<pair(Item,bool)>...
        List<Item> lis_items;
        List<bool> lis_equips;

        public Player(string in_name) // initial setting for player when this class was constructed
        {
            if (instance == null)
                instance = this;
            fLvl = 1;
            fName = in_name;
            fJob = "None";
            fAtk = 10;
            fBonusAtk = 0;
            fDef = 4;
            fBonusDef = 0;
            fHP = 100;
            fBonusHP = 0;
            fGold = 10000;
            fExp = 0;
            fMaxExp = 10;
            lis_items = new List<Item>();
            lis_equips = new List<bool>();
        }
        public void AddExp(int exp) // Level up logic was also included
        {
            fExp += exp;
            if (fExp >= fMaxExp)
            {
                fLvl++;
                fExp = 0;
                fMaxExp += fMaxExp;
                fAtk += 5;
                fDef += 2;
                fHP += 50;
            }
        }

        public string GetStatus(Status stat)
        {
            switch(stat)
            {
                case Status.LVL:
                    return fLvl.ToString();
                    break;
                case Status.NAME:
                    return fName;
                    break;
                case Status.JOB:
                    return fJob;
                    break;
                case Status.ATK:
                    return fAtk.ToString();
                    break;
                case Status.BATK:
                    return fBonusAtk.ToString();
                    break;
                case Status.DEF:
                    return fDef.ToString();
                    break;
                case Status.BDEF:
                    return fBonusDef.ToString();
                    break;
                case Status.HP:
                    return fHP.ToString();
                    break;
                case Status.BHP:
                    return fBonusHP.ToString();
                    break;
                case Status.EXP:
                    return fExp.ToString();
                    break;
                case Status.MEXP:
                    return fMaxExp.ToString();
                    break;
                case Status.GOLD:
                    return fGold.ToString();
                    break;
                default:
                    return "";
                    break;
            }
            return "";
        }
        public int GetNItems()
        {
            return lis_items.Count;
        }
        public Item GetItem(int idx)
        {
            return lis_items[idx];
        }
        public bool GetEquipStatus(int idx)
        {
            return lis_equips[idx];
        }

        public void Equip(int idx)
        {
            if (!lis_equips[idx])
            {
                lis_equips[idx] = true;
                fBonusAtk += lis_items[idx].GetBAtk();
                fBonusDef += lis_items[idx].GetBDef();
                fBonusHP += lis_items[idx].GetBHP();
            }
        }
        public void UpEquip(int idx)
        {
            if (lis_equips[idx])
            {
                lis_equips[idx] = false;
                fBonusAtk -= lis_items[idx].GetBAtk();
                fBonusDef -= lis_items[idx].GetBDef();
                fBonusHP -= lis_items[idx].GetBHP();
            }
        }
        public void BuyItem(Item item)
        {
            lis_items.Add(item);
            lis_equips.Add(false);
            fGold -= item.GetPrice();
        }

        public enum Status
        {
            LVL,
            NAME,
            JOB,
            ATK,
            BATK,
            DEF,
            BDEF,
            HP,
            BHP,
            GOLD,
            EXP,
            MEXP
        }
    }
}
