using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W2Project
{

    internal class Shop
    {
        // prefer to use vector<pair>...
        List<Item> lis_Items;
        List<int> lis_stock;
        public static Shop instance;
        public Shop() 
        {
            if (instance == null)
                instance = this;
            lis_Items = new List<Item>();
            lis_stock = new List<int>();
            lis_Items.Add(new Item("수련자 갑옷",  1, 1, 0, 5, 1000, "수련에 도움을 주는 갑옷입니다."));
            lis_stock.Add(1);
            lis_Items.Add(new Item("무쇠 갑옷",    2, 2, 0, 4, 2000, "무쇠로 만들어져 튼튼한 갑옷입니다."));
            lis_stock.Add(1);
            lis_Items.Add(new Item("스파르타 갑옷",3, 3, 0, 3, 3500, "르탄이가 입었던 전설의 갑옷입니다."));
            lis_stock.Add(1);
            lis_Items.Add(new Item("낡은 검",      4, 1, 5, 0, 600, "녹으로 뒤덮인 검입니다."));
            lis_stock.Add(1);
            lis_Items.Add(new Item("청동 도끼",    5, 2, 3, 0, 1500, "어디선가 사용된 도끼입니다."));
            lis_stock.Add(1);
            lis_Items.Add(new Item("스파르타 창",  6, 3, 2, 0, 2500, "르탄이가 사용했던 전설의 창입니다."));
            lis_stock.Add(1);
        }
        public int GetNItem()
        {
            return lis_Items.Count;
        }
        public Item GetItem(int idx)
        {
            return lis_Items[idx];
        }
        public void SellItem(int idx) 
        {
            lis_stock[idx]--;
        }
        public int GetStock(int idx)
        {
            return lis_stock[idx];
        }
    }
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
        public Item(string name, int id, int hier, int bAtk, int bDef,  int price, string expalin)
        {
            fName = name;
            fID = id;
            fHierachy = hier;
            fBAtk = bAtk;
            fBDef = bDef;
            fPrice = price;
            fExplain = expalin;
        }
        public string GetName() {  return fName; }
        public string GetExplanation() { return fExplain; }
        public int GetBAtk() { return fBAtk;}
        public int GetBDef() { return fBDef;}
        public int GetBHP() { return fBHP; }
        public int GetPrice() { return fPrice;}
    }
}
