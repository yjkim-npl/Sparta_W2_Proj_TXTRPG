using System;
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
        int fMaxHP;
        int fBonusHP;
        int fGold;
        int fExp;
        int fMaxExp;

        int fWeaponLvl;
        int fArmorLvl;
        int fRingLvl;

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
            fMaxHP = 100;
            fBonusHP = 0;
            fGold = 11200;
            fExp = 0;
            fMaxExp = 10;
            lis_items = new List<Item>();
            lis_equips = new List<bool>();
            fWeaponLvl = 0;
            fArmorLvl = 0;
            fRingLvl = 0;
        }

        public Player(int lvl, string name, string job, int atk, int batk, int def, int bdef, int HP, int MHP, int bHP, int gold, int exp, int mexp, List<Item> items, List<bool> equips)
        {
            if (instance == null)
                instance = this;
            fLvl = lvl;
            fName = name;
            fJob = job;
            fAtk = atk;
            fBonusAtk = batk;
            fDef = def;
            fBonusDef = bdef;
            fHP = HP;
            fMaxHP = MHP;
            fBonusHP = bHP;
            fGold = gold;
            fExp = exp;
            fMaxExp = mexp;
            lis_items = items;
            lis_equips = equips;
            for (int a = 0; a < lis_items.Count; a++)
            {
                if (!lis_equips[a]) continue;
                if (lis_items[a].GetBAtk() > 0 && (fWeaponLvl & lis_items[a].GetHierachy()) != lis_items[a].GetHierachy())
                    fWeaponLvl += lis_items[a].GetHierachy();
                if (lis_items[a].GetBDef() > 0 && (fArmorLvl & lis_items[a].GetHierachy()) != lis_items[a].GetHierachy())
                    fArmorLvl += lis_items[a].GetHierachy();
                if (lis_items[a].GetBHP() > 0 && (fRingLvl & lis_items[a].GetHierachy()) != lis_items[a].GetHierachy())
                    fRingLvl += lis_items[a].GetHierachy();
            }
        }

        public void Warrior()
        {
            fJob = "Warrior";
            fHP = 200;
            fMaxHP = 200;
            fDef = 8;
        }

        public void Archer()
        {
            fJob = "Archer";
            fAtk = 15;
            fHP = 60;
            fMaxHP = 60;
            fDef = 2;
        }

        public void Chief()
        {
            fJob = "Chief";
            fAtk = 13;
            fHP = 80;
            fMaxHP = 80;
            fDef = 3;
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
                fMaxHP = 100 + 50 * (fLvl - 1);
                fHP = fMaxHP + fBonusHP;
            }
        }

        public void FullHealth()
        {
            fHP = fMaxHP + fBonusHP;
        }

        public void Damage(int damage)
        {
            fHP -= damage;
            if(Dead())
            {
                fLvl--;
                fGold = fGold / 2;
                fExp = fExp / 2;
                fHP = fMaxHP + fBonusHP;
            }
        }

        public bool Dead()
        {
            if (fHP <= 0) return true;
            else return false;
        }

        public void AddGold(int gold)
        {
            fGold += gold;
        }

        public int GetStatusInt(Status stat)
        {
            switch(stat)
            {
                case Status.LVL:
                    return fLvl;
                    break;
                case Status.ATK:
                    return fAtk;
                    break;
                case Status.BATK:
                    return fBonusAtk;
                    break;
                case Status.DEF:
                    return fDef;
                    break;
                case Status.BDEF:
                    return fBonusDef;
                    break;
                case Status.HP:
                    return fHP;
                    break;
                case Status.MHP:
                    return fMaxHP;
                    break;
                case Status.BHP:
                    return fBonusHP;
                    break;
                case Status.EXP:
                    return fExp;
                    break;
                case Status.MEXP:
                    return fMaxExp;
                    break;
                case Status.GOLD:
                    return fGold;
                    break;
                default:
                    GetStatusStr(stat);
                    break;
            }
            return -1;
        }
        public string GetStatusStr(Status stat)
        {
            switch(stat)
            {
                case Status.NAME:
                    return fName;
                    break;
                case Status.JOB:
                    return fJob;
                    break;
                default:
                    return GetStatusInt(stat).ToString();
                    break;
            }
        }
        public int GetNItems()
        {
            return lis_items.Count;
        }
        public Item GetItem(int idx)
        {
            return lis_items[idx];
        }
        public int GetHierachyLvl(Hierachy hier)
        {
            switch(hier)
            {
                case Hierachy.Weapon:
                    return fWeaponLvl;
                    break;
                case Hierachy.Armor:
                    return fArmorLvl;
                    break;
                case Hierachy.Ring:
                    return fRingLvl;
                    break;
                default:
                    return -1;
                    break;
            }
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
                Hierachy type = (Hierachy)(int)lis_items[idx].GetType();
                switch(type)
                {
                    case Hierachy.Weapon:
                        fWeaponLvl += lis_items[idx].GetHierachy();
                        break;
                    case Hierachy.Armor:
                        fArmorLvl += lis_items[idx].GetHierachy();
                        break;
                    case Hierachy.Ring:
                        fRingLvl += lis_items[idx].GetHierachy();
                        break;
                    default:
                        break;
                }
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
                Hierachy type = (Hierachy)(int)lis_items[idx].GetType();
                switch(type)
                {
                    case Hierachy.Weapon:
                        fWeaponLvl -= lis_items[idx].GetHierachy();
                        break;
                    case Hierachy.Armor:
                        fArmorLvl -= lis_items[idx].GetHierachy();
                        break;
                    case Hierachy.Ring:
                        fRingLvl -= lis_items[idx].GetHierachy();
                        break;
                    default:
                        break;
                }
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
            MHP,
            BHP,
            GOLD,
            EXP,
            MEXP
        }
        public enum Hierachy
        {
            None,
            Weapon,
            Armor,
            Ring
        }
    }
}
