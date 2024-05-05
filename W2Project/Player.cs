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

        public int fHP_p_idx { get; private set; }
        public int fATK_p_idx { get; private set; }
        public int fDEF_p_idx { get; private set; }
        public int[] fPotion_idx { get; private set; }

        // hope to be use vector<pair(Item,bool)>...
        List<Item> lis_items;
        List<int> lis_equips; // for potion, the equip number means the number of potion

        List<(int status,int questID,int curr,int goal)> lis_quest; // quest status : 0. not accepted, 1. ongoing, 2. completed
        /* samples for lis_quest
         * 0, 1, 2 (status)
         * 0, 1, 3 (Quest ID)
         * 0, 0, 0 (current status)
         * 0, 1, 3 (goals)
         */

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
            fWeaponLvl = 0;
            fArmorLvl = 0;
            fRingLvl = 0;
            lis_items = new List<Item>();
            lis_equips = new List<int>();
            lis_quest = new List<(int,int,int,int)>(); 
            fPotion_idx = new int[3] { -1, -1, -1 };
        }

        public Player(int lvl, string name, string job, int atk, int batk, int def, int bdef, int HP, int MHP, int bHP, int gold, int exp, int mexp, List<Item> items, List<int> equips,List<(int,int,int,int)> quest)
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
            fPotion_idx = new int[3] { -1, -1, -1 };
            for (int a = 0; a < lis_items.Count; a++)
            {
                if (lis_equips[a] != 1) continue;
                if (lis_items[a].GetBAtk() > 0 && (fWeaponLvl & lis_items[a].GetHierachy()) != lis_items[a].GetHierachy())
                    fWeaponLvl += lis_items[a].GetHierachy();
                if (lis_items[a].GetBDef() > 0 && (fArmorLvl & lis_items[a].GetHierachy()) != lis_items[a].GetHierachy())
                    fArmorLvl += lis_items[a].GetHierachy();
                if (lis_items[a].GetBHP() > 0 && (fRingLvl & lis_items[a].GetHierachy()) != lis_items[a].GetHierachy())
                    fRingLvl += lis_items[a].GetHierachy();

                // potion
                if (lis_items[a].GetType() == ItemType.Use)
                {
                    switch(lis_items[a].GetID())
                    {
                        case 9: // HP potion
                            fPotion_idx[0] = a;
                            break;
                        case 10: // ATK potion
                            fPotion_idx[1] = a;
                            break;
                        case 11: // DEF potion
                            fPotion_idx[2] = a;
                            break;
                        default:
                            break;
                    }
                }
            }
            lis_quest = quest;
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
        }

        public bool Dead()
        {
            if (fHP <= 0)
            {
                fLvl--;
                fGold = fGold / 2;
                fExp = fExp / 2;
                fHP = fMaxHP + fBonusHP;
                return true;
            }
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
        public bool UsePotion(int opt)
        {
            if (fPotion_idx[opt] == -1)
            {
                return false;
            }
            switch (opt)
            {
                case 0: // HP
                    if (lis_equips[fPotion_idx[0]] > 0)
                    {
                        lis_equips[fPotion_idx[0]]--;
                        fHP += lis_items[fPotion_idx[0]].GetBHP();
                        if (fHP > fMaxHP + fBonusHP)
                            fHP = fMaxHP + fBonusHP;
                        return true;
                    }
                    else
                        return false;
                    break;
                case 1: // ATK
                    if (lis_equips[fPotion_idx[1]] > 0)
                    {
                        lis_equips[fPotion_idx[1]]--;
                        fAtk += lis_items[fPotion_idx[1]].GetBAtk();
                        return true;
                    }
                    else
                        return false;
                    break;
                case 2: // DEF
                    if (lis_equips[fPotion_idx[2]] > 0)
                    {
                        lis_equips[fPotion_idx[2]]--;
                        fDef += lis_items[fPotion_idx[2]].GetBDef();
                        return true;
                    }
                    else
                        return false;
                    break;
                default:
                    return false;
                    break;
            }
        }
        public int GetNumberOfPotion(int opt)
        {
            int result = lis_equips[fPotion_idx[opt]];
            return result;
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
        public int GetEquipStatus(int idx)
        {
            return lis_equips[idx];
        }

        public void Equip(int idx)
        {
            if (lis_equips[idx] == 0)
            {
                lis_equips[idx] = 1;
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
            if (lis_equips[idx] == 1)
            {
                lis_equips[idx] = 0;
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
        public void AddItem(Item item, int opt=1)
        {
            if (!lis_items.Contains(item))
                lis_items.Add(item);
            if(item.GetType() == ItemType.Use)
            {
                // assign index while pushing the list
                if (lis_items.Count > lis_equips.Count)
                {
                    lis_equips.Add(1);
                    switch(item.GetID())
                    {
                        case 9: // HP
                            fPotion_idx[0] = lis_equips.Count - 1;
                            break;
                        case 10: // ATK
                            fPotion_idx[1] = lis_equips.Count - 1;
                            break;
                        case 11: // DEF
                            fPotion_idx[2] = lis_equips.Count - 1;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    int idx = lis_items.IndexOf(item);
                    lis_equips[idx]++;
                }
            }
            else
            {
                lis_equips.Add(0);
            }
            if(opt == 1)
                fGold -= item.GetPrice();
        }

        public int GetNQuestAccepted()
        {
            return lis_quest.Count;
        }
        public (int,int,int,int) GetQuestStatusViaQID(int q_ID)
        {
            foreach(var  item in lis_quest) 
            {
                if(item.questID == q_ID)
                {
                    return (item.status, q_ID, item.curr, item.goal);
                }
            }
            return (0,0,0,0);
        }
        public (int,int,int,int) GetQuestStatusViaIndex(int idx)
        {
            return lis_quest[idx];
        }
        public bool AcceptQuest(int q_ID)
        {
            Quest quest = Program.quest_list[q_ID];
            if(GetQuestStatusViaQID(q_ID).Item1 == 1)
            {
                return false;
            }
            else if (quest.IsAcceptable(this))
            {
                int cur_quests = 0;
                for(int a=0; a<lis_quest.Count; a++)
                {
                    if (lis_quest[a].status == 1)
                        cur_quests++;
                }
                if(cur_quests > 3)
                {
                    return false;
                }
                else
                {
                    lis_quest.Add((1,q_ID,quest.GetDataInt(QuestIdx.CurrNum),quest.GetDataInt(QuestIdx.GoalNum)));
                    return true;
                }
            }
            else
                return false;
        }

        public bool CompleteQuest(int q_ID)
        {
            for(int a=0; a<lis_quest.Count; a++)
            {
                if (lis_quest[a].questID == q_ID && lis_quest[a].curr >= lis_quest[a].goal)
                {
                    lis_quest[a] = (2,q_ID, 0,0);

                    Item rwd_Item = new Item();
                    int rwd_Exp = 0;
                    int rwd_Gold = 0;
                    int rwd_ItemID = 0;
                    foreach (var quest in Program.quest_list)
                    {
                        if(quest.GetDataInt(QuestIdx.ID) == q_ID)
                        {
                            rwd_ItemID = quest.GetDataInt(QuestIdx.RewardItemID);
                            rwd_Exp = quest.GetDataInt(QuestIdx.RewardExp);
                            rwd_Gold = quest.GetDataInt(QuestIdx.RewardGold);
                            break;
                        }
                    }
                    foreach (var item in Program.item_list)
                    {
                        if (item.GetID() == rwd_ItemID)
                        {
                            rwd_Item = item;
                            break;
                        }
                    }

                    AddItem(rwd_Item, 0);
                    AddExp(rwd_Exp);
                    AddGold(rwd_Gold);
                    return true;
                }
            }
            return false;
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
