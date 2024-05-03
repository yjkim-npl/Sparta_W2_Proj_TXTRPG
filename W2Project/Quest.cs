using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W2Project
{
    internal class Quest
    {
        int fID;
        string fPreCond;
        string fAchieveGoal;
        string fDescription;
        int fGoalNum;
        int fCurrNum;
        int fTargetID;
        QuestType fType;
        (Item, int, int) fReward;
        bool fCompleted;

        public Quest()
        {
            fID = 0;
            fPreCond = "";
            fAchieveGoal = "";
            fGoalNum = 1;
            fCurrNum = 0;
            fType = QuestType.None;
            fDescription = "";
            fCompleted = false;
        }
        public Quest(int ID, string preCond, int type, string achieveGoal, int itemID, int rwd_gold, int rwd_exp, bool completed, string desc)
        {
            fID = ID;
            fPreCond = preCond.Replace('(',' ').Replace(')',' ');
            fType = (QuestType)type;
            fAchieveGoal = achieveGoal;
            int[] str_sep = fAchieveGoal.Replace('(',' ').Replace(')',' ').Split(':').Select(Int32.Parse).ToArray();
            if(fType != QuestType.Support && str_sep.Length > 0 ) // Support quest doesn't have any elements
            {
                fTargetID = str_sep[0];
                fCurrNum = str_sep[1];
                fGoalNum = str_sep[2];
            }
            fDescription = desc;
            fReward = (Program.item_list[itemID], rwd_gold, rwd_exp);
            fCompleted = completed;
        }
        public bool IsAcceptable(Player player)
        {
            string[] str = fPreCond.Split(' ');
            int[] operID = new int[3] { -1,-1,-1}; // maximum 3 logics in string
            int[] comp = new int[2] {-1,-1}; // maximum 2 components on Player
            int[] value = new int[2] { -1,-1}; // maximum 2 value on player

            bool[] logics = new bool[2] { false, false};
            int logicIdx = 0;
            foreach(var a in str)
            {
                // value
                if(int.TryParse(a, out int ram))
                {
                    if (value[0] == -1)
                        value[0] = ram;
                    else if (value[1] == -1)
                        value[1] = ram;
                    continue;
                }
                switch(a)
                {
                    // comp
                    case "LVL":
                        if (comp[0] == -1)
                            comp[0] = (int)Player.Status.LVL;
                        else if (comp[1] == -1)
                            comp[1] = (int)Player.Status.LVL;
                        break;
//                    case "JOB": // string
//                        if (comp[0] == -1)
//                            comp[0] = (int)Player.Status.JOB;
//                        else if (comp[1] == -1)
//                            comp[1] = (int)Player.Status.JOB;
//                        break;
                    case "ATK":
                        if (comp[0] == -1)
                            comp[0] = (int)Player.Status.ATK;
                        else if (comp[1] == -1)
                            comp[1] = (int)Player.Status.ATK;
                        break;
                    case "DEF":
                        if (comp[0] == -1)
                            comp[0] = (int)Player.Status.DEF;
                        else if (comp[1] == -1)
                            comp[1] = (int)Player.Status.DEF;
                        break;
                    case "HP":
                        if (comp[0] == -1)
                            comp[0] = (int)Player.Status.HP;
                        else if (comp[1] == -1)
                            comp[1] = (int)Player.Status.HP;
                        break;
                    case "GOLD":
                        if (comp[0] == -1)
                            comp[0] = (int)Player.Status.GOLD;
                        else if (comp[1] == -1)
                            comp[1] = (int)Player.Status.GOLD;
                        break;

                    // logic operator
                    case "==":
                        if (operID[0] == -1)
                            operID[0] = 0;
                        else if (operID[1] == -1)
                            operID[1] = 0;
                        else if (operID[2] == -1)
                            operID[2] = 0;
                        break;
                    case ">":
                        if (operID[0] == -1)
                            operID[0] = 1;
                        else if (operID[1] == -1)
                            operID[1] = 1;
                        else if (operID[2] == -1)
                            operID[2] = 1;
                        break;
                    case ">=":
                        if (operID[0] == -1)
                            operID[0] = 2;
                        else if (operID[1] == -1)
                            operID[1] = 2;
                        else if (operID[2] == -1)
                            operID[2] = 2;
                        break;
                    case "<":
                        if (operID[0] == -1)
                            operID[0] = 3;
                        else if (operID[1] == -1)
                            operID[1] = 3;
                        else if (operID[2] == -1)
                            operID[2] = 3;
                        break;
                    case "<=":
                        if (operID[0] == -1)
                            operID[0] = 4;
                        else if (operID[1] == -1)
                            operID[1] = 4;
                        else if (operID[2] == -1)
                            operID[2] = 4;
                        break;
                    case "&&":
                        if (operID[0] == -1)
                            operID[0] = 5;
                        else if (operID[1] == -1)
                            operID[1] = 5;
                        else if (operID[2] == -1)
                            operID[2] = 5;
                        break;
                    case "||":
                        if (operID[0] == -1)
                            operID[0] = 6;
                        else if (operID[1] == -1)
                            operID[1] = 6;
                        else if (operID[2] == -1)
                            operID[2] = 6;
                        break;

                    default:
                        break;
                }
            }

            foreach(var a in operID)
            {
                switch (a)
                {
                    case 0: // ==
                        logics[logicIdx] = (player.GetStatusInt((Player.Status)comp[logicIdx]) == value[logicIdx] ? true : false);
                        break;
                    case 1: // >
                        logics[logicIdx] = (player.GetStatusInt((Player.Status)comp[logicIdx]) > value[logicIdx] ? true : false);
                        break;
                    case 2: // >=
                        logics[logicIdx] = (player.GetStatusInt((Player.Status)comp[logicIdx]) >= value[logicIdx] ? true : false);
                        break;
                    case 3: // <
                        logics[logicIdx] = (player.GetStatusInt((Player.Status)comp[logicIdx]) < value[logicIdx] ? true : false);
                        break;
                    case 4: // <=
                        logics[logicIdx] = (player.GetStatusInt((Player.Status)comp[logicIdx]) <= value[logicIdx] ? true : false);
                        break;
                    case 5: // &&
                    case 6: // ||
                        logicIdx++;
                        break;
                    default:
                        break;
                }
            }

            if (logicIdx == 0)
                return logics[0];
            else if (operID[1] == 5)
                return logics[0] && logics[1];
            else if (operID[1] == 6)
                return logics[0] || logics[1];
            return false;
        }
        public void Update()
        {

        }
        public string GetProgress()
        {
            // Assume the prefix follows the form of ("{0}: {1} / {2} ",fTargetID, fCurrNum, fGoalNum)
            string suffix = "";
            switch(fType)
            {
                case QuestType.Hunt:
                    suffix = $"사냥 {fCurrNum} / {fGoalNum}";
                    break;
                case QuestType.Collect:
                    suffix = $"수집 {fCurrNum} / {fGoalNum}";
                    break;
                case QuestType.Support:
                    suffix = "지원";
                    break;
                default:
                    break;

            }
            return "";
        }

        public (Item, int ,int) Complete()
        {
            fCompleted = true;
            return fReward;
        }

        public int GetDataInt(QuestIdx idx)
        {
            int a = 0;
            switch(idx)
            {
                case QuestIdx.ID:
                    a = fID;
                    break;
                case QuestIdx.GoalNum:
                    a = fGoalNum;
                    break;
                case QuestIdx.CurrNum:
                    a = fCurrNum;
                    break;
                case QuestIdx.TargetID:
                    a = fTargetID;
                    break;
                case QuestIdx.Type:
                    a = (int)fType;
                    break;
                case QuestIdx.RewardItemID:
                    a = fReward.Item1.GetID();
                    break;
                case QuestIdx.RewardGold:
                    a = fReward.Item2;
                    break;
                case QuestIdx.RewardExp:
                    a = fReward.Item3;
                    break;
                default:
                    a = -1;
                    break;

            }
            return a;
        }
        public string GetDataStr(QuestIdx idx)
        {
            string result;
            switch(idx)
            {
                case QuestIdx.Condition:
                    result = fPreCond;
                    break;
                case QuestIdx.Description:
                    result = fDescription;
                    break;
                default:
                    result = "";
                    break;
            }
            return result;
        }
        public bool GetDataBool(QuestIdx idx)
        {
            bool result = false;
            switch(idx)
            {
                case QuestIdx.Completed:
                    result = fCompleted;
                    break;
                default:
                    result = false;
                    break;
            }
            return result;
        }
    }
    public enum QuestType
    {
        None,
        Support,
        Hunt,
        Collect,
        Series
    }
    public enum QuestStatus
    {
        NotAccepted,
        InProgress,
        Completed
    }

    public enum QuestIdx
    {
        None,
        ID,
        Condition,
        Description,
        GoalNum,
        CurrNum,
        TargetID,
        Type,
        RewardItemID,
        RewardGold,
        RewardExp,
        Completed
    }

}
