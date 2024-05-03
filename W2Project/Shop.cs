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
            lis_Items = new List<Item>();
            lis_stock = new List<int>();
            //                      Name          ID hie rnk type bA bD bH price Desc
            lis_Items.Add(new Item("낡은 옷",      0, 0, 1, 2, 0, 0, 0, 100, "낡은 옷"));
            lis_stock.Add(1);
            lis_Items.Add(new Item("수련자 갑옷",  1, 1, 2, 2, 0, 5, 0, 1000, "수련에 도움을 주는 갑옷입니다."));
            lis_stock.Add(1);                                   
            lis_Items.Add(new Item("무쇠 갑옷",    2, 2, 4 ,2, 0, 4, 0, 2000, "무쇠로 만들어져 튼튼한 갑옷입니다."));
            lis_stock.Add(1);                                   
            lis_Items.Add(new Item("스파르타 갑옷",3, 4, 8, 2, 0, 3, 0,  3500, "르탄이가 입었던 전설의 갑옷입니다."));
            lis_stock.Add(1);                                   
            lis_Items.Add(new Item("낡은 검",      4, 1, 2, 1, 5, 0 ,0, 600, "녹으로 뒤덮인 검입니다."));
            lis_stock.Add(1);                                   
            lis_Items.Add(new Item("청동 도끼",    5, 2, 4, 1, 3, 0, 0, 1500, "어디선가 사용된 도끼입니다."));
            lis_stock.Add(1);                                   
            lis_Items.Add(new Item("스파르타 창",  6, 4, 8, 1, 2, 0, 0, 2500, "르탄이가 사용했던 전설의 창입니다."));
            lis_stock.Add(1);
            lis_Items.Add(new Item("낡은 반지",   7, 1, 2, 3, 0, 0, 5, 500, "낡은 반지"));
            lis_stock.Add(1);
            if (instance == null)
                instance = this;
        }
        public Shop(List<Item> item_list) 
        {
            if (instance == null)
                instance = this;
            lis_Items = item_list;
            lis_stock = new List<int>();
            for(int a=0; a<lis_Items.Count; a++)
            {
                lis_stock.Add(1);
                if (lis_Items[a].GetType() == ItemType.Use)
                {
                    lis_stock[a] += 9;
                }
            }
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
}
