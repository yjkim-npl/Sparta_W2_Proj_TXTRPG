using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W2Project
{
    internal class Mob
    {
        int fID;
        int fName;
        int fHP;
        int fAtk;
        int fDef;
        int fGold;
        int fExp;
        MobType fMobType;
    }

    public enum MobType
    {
        None,
        Named,
        Boss
    }
}
