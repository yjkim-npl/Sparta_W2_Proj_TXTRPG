using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W2Project
{
    public class Item
    {
        string fName;
        int fID;
        int fHierachy;
        int fBAtk;
        int fBDef;
        int fBHP;
        int fPrice;
        string fExplain;
        public Item()
        {
            fName = "";
            fID = 0;
            fHierachy = 0;
            fBAtk = 0;
            fBDef = 0;
            fBHP = 0;
            fPrice = 0;
            fExplain = "";
        }
//        public Item(int id)
//        {
////            fName = 
//        }
        public Item(string name, int id, int hier, int bAtk, int bDef,  int price, string expalin)
        {
            fName = name;
            fID = id;
            fHierachy = hier;
            fBAtk = bAtk;
            fBDef = bDef;
            fPrice = price;
            fExplain = expalin;
            if (!File.Exists($"../../../../Assets/Items/{fID}.dat"))
            {
                StreamWriter sw = new StreamWriter($"../../../../Assets/Items/{fID}.dat");
                sw.WriteLine(fName);
                sw.WriteLine(fID);
                sw.WriteLine(fHierachy);
                sw.WriteLine(fBAtk);
                sw.WriteLine(fBDef);
                sw.WriteLine(fBHP);
                sw.WriteLine(fPrice);
            }
        }
        public int GetID() { return fID; }
        public string GetName() {  return fName; }
        public string GetExplanation() { return fExplain; }
        public int GetBAtk() { return fBAtk;}
        public int GetBDef() { return fBDef;}
        public int GetBHP() { return fBHP; }
        public int GetPrice() { return fPrice;}
        public int GetHierachy() { return fHierachy;}
    }
}
