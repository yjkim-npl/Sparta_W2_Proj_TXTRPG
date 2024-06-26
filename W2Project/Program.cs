﻿using System.ComponentModel.Design;
using System.Data.Common;
using System.Xml.Serialization;

namespace W2Project
{
    internal class Program
    {
        public static List<Item> item_list;
        public static List<Quest> quest_list;
        public static List<Enemy> enemy_list;

        public static string CenterAlign(string text, int width)
        {
            return text.PadLeft((width - text.Length) / 2 + text.Length).PadRight(width);
        }
        public static int Choice(int min, int max, int cutmin = -1, int cutmax = -1)
        {
            string in_str = Console.ReadLine();
            int choice;
            while(!int.TryParse(in_str, out choice) || !(choice >= min && choice <=max) || ((cutmin==-1&&cutmax==-1)?false:(choice >= cutmin && choice <= cutmax)))
            {
                if(cutmin == -1 && cutmax == -1)
                { 
                    Console.SetCursorPosition(5, 22);
                    Console.WriteLine("원하시는 행동을 입력해주세요 ({0} ~ {1}중에서 골라주세요)",min, max);
                }
                else
                { 
                    Console.SetCursorPosition(5, 22);
                    Console.WriteLine("{0} ~ {1} 제외한 {2} ~ {3}중에서 골라주세요,",cutmin,cutmax,min, max);
                }
                Console.SetCursorPosition(5, 23);
                Console.Write(">>     ");
                Console.SetCursorPosition(8, 23);
                in_str = Console.ReadLine();
            }
            return choice;
        }
        static void Main(string[] args)
        {
            bool LoadData = false;
            bool isPlaying = true;
            Console.SetWindowSize(90, 30);

            // Current location is W2Project/W2Project/bin/Debug/net6.0/
            // Load items from the list(csv)
            string str_item_list = "../../../../Assets/ItemList.csv";
            item_list = new List<Item>();
            StreamReader sr_item = new StreamReader(str_item_list);
            string line = sr_item.ReadLine(); // dummy line
            do
            {
                line = sr_item.ReadLine();
                Console.WriteLine(line);
                if (line != null)
                {
                    List<string> comp = line.Split(',').ToList();
                    //                string[] comp = new string[8];
                    //                    comp = line.Split(',');
                    int hier = comp[3] == "s" ? 1 : comp[3] == "m" ? 2 : comp[3] == "l" ? 4 : 0;
                    int rank = comp[4] == "n" ? 1 : comp[4] == "r" ? 2 : comp[4] == "u" ? 4 : comp[4] == "l" ? 8 : 0;
                    Item item = new Item(comp[1], int.Parse(comp[2]), hier, rank, int.Parse(comp[5]), int.Parse(comp[6]), int.Parse(comp[7]), int.Parse(comp[8]), int.Parse(comp[9]), comp[10]);
                    item_list.Add(item);
                }
                else
                    continue;
            } while (line != null);
            sr_item.Close();

            string str_quest_list = "../../../../Assets/QuestList.csv";
            quest_list = new List<Quest>();
            StreamReader sr_Quest = new StreamReader(str_quest_list);
            line = sr_Quest.ReadLine(); // dummy line

            do
            {
                line = sr_Quest.ReadLine();
                Console.WriteLine(line);
                if (line != null)
                {
                    List<string> comp = line.Split(',').ToList();
                    if (comp[0] == "#")
                        continue;

                    QuestType qt;
                    switch (comp[2])
                    {
                        case " Hunt":
                            qt = QuestType.Hunt;
                            break;
                        case " Collect":
                            qt = QuestType.Collect;
                            break;
                        case " Series":
                            qt = QuestType.Series;
                            break;
                        case " Support":
                            qt = QuestType.Support;
                            break;
                        default:
                            qt = QuestType.None;
                            break;
                    }
                    Quest q = new Quest(int.Parse(comp[0]), comp[1], (int)qt, comp[3], int.Parse(comp[4]), int.Parse(comp[5]), int.Parse(comp[6]), bool.Parse(comp[7]), comp[8]);
                    quest_list.Add(q);
                }
            } while (line != null);
            sr_Quest.Close();

            Shop shop = new Shop(item_list);
            Scene scene = new Scene();

            // Start Scene
            scene.MoveScene(SceneType.Start);
            string username = Console.ReadLine();

            Player player;
            // Check the Player name on Player list
            if(!File.Exists("../../../../Assets/PlayerList.txt"))
            {
                StreamWriter sw = new StreamWriter("../../../../Assets/PlayerList.txt");
                sw.Close();
            }
            string[] playerList = File.ReadAllLines("../../../../Assets/PlayerList.txt");
            // If there are data of previous play, load it and reconstruct the player
            if(playerList.Contains(username))
            {
                StreamReader sr = new StreamReader($"../../../../Assets/PlayerData/{username}.dat");
                List<string> stats = new List<string>();
                string comp = "";
                do
                {
                    comp = sr.ReadLine();
                    stats.Add(comp);
                }
                while (comp != null);
                List<string>items = stats[13].Split(' ').ToList();
                List<string>equip = stats[14].Split(' ').ToList();
                List<Item> items_list = new List<Item>();
                List<int> equip_list = new List<int>();
                for (int a = 0; a < items.Count; a++)
                {
                    Item item;
                    for (int b = 0; b < Program.item_list.Count; b++)
                    {
                        Item temp = Program.item_list[b];
                        if(temp.GetID() != a)
                        {
                            continue;
                        }
                        else
                        { 
                            item = temp;
                        }
                        items_list.Add(item);
                        if (equip[a] == "1")
                            equip_list.Add(1);
                        else if (equip[a] == "0")
                            equip_list.Add(0);
                        else if (int.TryParse(equip[a],out int num) && item.GetType()==ItemType.Use)
                        {
                            equip_list.Add(num);
                        }
                        else
                        {
                            equip_list.Add(0);
                        }
                    }
                }
                List<string> lis_quests_status = stats[15]==""? new List<string>() : stats[15].Split(' ').ToList();
                List<string> lis_quests_ID = stats[16]==""? new List<string>(): stats[16].Split(' ').ToList();
                List<string> lis_quests_Curr = stats[17] == "" ? new List<string>() : stats[17].Split(' ').ToList();
                List<string> lis_quests_Goal = stats[18] == "" ? new List<string>() : stats[18].Split(' ').ToList();
                List<(int, int, int, int)> lis_quest_info = new List<(int, int, int, int)>();
                for( int a=0; a<lis_quests_status.Count-1; a++) // last element is '\n'
                {
                    int status = int.Parse(lis_quests_status[a]);
                    int ID = int.Parse(lis_quests_ID[a]);
                    int Curr = int.Parse(lis_quests_Curr[a]);
                    int Goal = int.Parse((lis_quests_Goal[a]));
                    lis_quest_info.Add((status,ID, Curr, Goal));
                }
                player = new Player(
                    int.Parse(stats[0]), // lvl
                    stats[1],            // name
                    stats[2],            // job
                    int.Parse(stats[3]), // atk
                    int.Parse(stats[4]), // batk
                    int.Parse(stats[5]), // def
                    int.Parse(stats[6]), // bdef
                    int.Parse(stats[7]), // cHP
                    int.Parse(stats[8]), // mHP
                    int.Parse(stats[9]), // bHP
                    int.Parse(stats[10]),// Gold
                    int.Parse(stats[11]),// cExp
                    int.Parse(stats[12]),// mExp
                    items_list,
                    equip_list,
                    lis_quest_info
                    );
                sr.Close();
                switch(stats[2])
                {
                    case "Chief":
                    case "Archer":
                    case "Warrior":
                        LoadData = true;
                        break;
                }
            }
            else
            {
                player = new Player(username);
                scene.MoveScene(SceneType.Jobs);
                int jobChoice = Program.Choice(1, 3);

                switch(jobChoice)
                {
                    case 1:
                        Player.instance.Warrior();
                        break;
                    case 2:
                        Player.instance.Archer();
                        break;
                    case 3:
                        Player.instance.Chief();
                        break;
                    default:
                        break;
                }
            }


            //switch(int.Parse(jobChoiceString))
            //{
            //    case 1:
            //        Player.instance.Warrior();
            //        break;
            //    case 2:
            //        Player.instance.Archer();
            //        break;
            //    case 3:
            //        Player.instance.Chief();
            //        break;
            //}

            //bool resJobChoice = int.TryParse(jobChoiceString, out int jobChoice);

            //while(true)
            //{
            //    if (jobChoice < 1 || jobChoice > 3)
            //    {
            //        Console.WriteLine("잘못된 선택입니다. 다시 선택해주세요.");
            //        string jobChoiceString2 = Console.ReadLine();
            //        int.Parse (jobChoiceString2);
            //        if (jobChoice == 1)
            //        {
            //            Player.instance.Warrior();
            //        }
            //        else if (jobChoice == 2)
            //        {
            //            Player.instance.Archer();
            //        }
            //        else if (jobChoice == 3)
            //        {
            //            Player.instance.Chief();
            //        }
            //    }
            //}
            //if (int.TryParse(jobChoiceString, out int jobChoice))
            //{
            //    switch (jobChoice)
            //    {
            //        case 1:
            //            Player.instance.Warrior();
            //            break;
            //        case 2:
            //            Player.instance.Archer();
            //            break;
            //        case 3:
            //            Player.instance.Chief();
            //            break;
            //        default:
            //            Console.WriteLine("잘못된 선택입니다. 다시 선택해주세요.");
            //            break;
            //    }
            //}
//            if (int.Parse(jobChoiceString) == 1)
//            {
//                Player.instance.Warrior();
//            }
//            else if (int.Parse(jobChoiceString) == 2)
//            {
//                Player.instance.Archer();
//            }
//            else if (int.Parse(jobChoiceString) == 3)
//            {
//                Player.instance.Chief();
//            }

            // MainScene
            scene.MoveScene(SceneType.Main);
            int choice = Choice(1, 6);
            int equip_opt = 0;
            int rest_opt = 0;
            int shop_opt = 0;
            int quest_opt = 0;
            bool wantSave = false;
            while (isPlaying)
            {
                switch (choice)
                {
                    case 0:
                        scene.MoveScene(SceneType.Main);
                        choice = Choice(0, 6);
                        if (choice == 0)
                        {
                            scene.MoveScene(SceneType.End);
                            isPlaying = false;
                            int saveOpt = Choice(1, 2);
                            if (saveOpt == 1)
                                wantSave = true;
                            else if (saveOpt == 2)
                                wantSave = false;
                            else
                                wantSave = false;
                        }
                        while (choice ==4 && Player.instance.GetStatusInt(Player.Status.HP) <= 10)
                        {
                            Console.WriteLine("휴식이 필요합니다.");
                            choice = Choice(0, 5);
                        }

                        break;
                    case 1:
                        scene.MoveScene(SceneType.Status);
                        choice = Choice(0, 0);
                        break;
                    case 2:
                        if(equip_opt == 1)
                        {
                            scene.MoveScene(SceneType.Inventory, equip_opt);
                            equip_opt = Choice(0,9,scene.nItemsOnPage+1,7);
                            if (equip_opt == 0)
                                choice = equip_opt;
                        }
                        else
                        {
                            scene.MoveScene(SceneType.Inventory, equip_opt);
                            equip_opt = Choice(0, 9, 2, 7);
                            if (equip_opt == 0)
                                choice = equip_opt;
                        }
                        break;
                    case 3:
                        if (shop_opt == 1)
                        {
                            scene.MoveScene(SceneType.Shop, shop_opt);
                            shop_opt = Choice(0, 9,scene.nItemsOnPage+1,7);
                            if (shop_opt == 0)
                                choice = 0;
                        }
                        else
                        {
                            scene.MoveScene(SceneType.Shop, shop_opt);
                            shop_opt = Choice(0, 9, 2, 7);
                            if (shop_opt == 0)
                                choice = 0;
                        }
                        break;
                    case 4:
                        scene.MoveScene(SceneType.Dungeon);
                        choice = Choice(0, 0);
                        break;
                    case 5:
                        scene.MoveScene(SceneType.Rest, rest_opt);
                        rest_opt = Choice(0, 3);
                        if (rest_opt == 0)
                            choice = 0;
//                        if (
//                            (Player.instance.GetStatusInt(Player.Status.HP)) +
//                            (Player.instance.GetStatusInt(Player.Status.BHP)) <
//                            100 + 
//                            50*(Player.instance.GetStatusInt(Player.Status.LVL) -1) +
//                                Player.instance.GetStatusInt(Player.Status.BHP)
//                            )
//                        {
//                            Player.instance.FullHealth();
//                        }
                        break;
                    case 6:
                        if(quest_opt == 1)
                        {
                            scene.MoveScene(SceneType.Quest, quest_opt);
                            quest_opt = Choice(0, 9,scene.nQuestsOnPage+1,7);
                            if (quest_opt == 0)
                                choice = 0;
                        }
                        else
                        {
                            scene.MoveScene(SceneType.Quest,quest_opt);
                            quest_opt = Choice(0, 9,2,7);
                            if(quest_opt == 0)
                                choice= 0;
                        }
                        break;
                    default:
                        scene.MoveScene(SceneType.Main);
                        choice = Choice(0, 4);
                        break;
                }
            }

            if(wantSave)
            {
                // Add player name to the player list
                string[] prev_player_list = File.ReadAllLines("../../../../Assets/PlayerList.txt");
                StreamWriter sw = new StreamWriter("../../../../Assets/PlayerList.txt");
                for(int a=0; a<prev_player_list.Length; a++)
                {
                    sw.WriteLine(prev_player_list[a]);
                }
                sw.WriteLine(Player.instance.GetStatusStr(Player.Status.NAME));
                sw.Close();

                // Save the data to dat file
                StreamWriter sw_dat = new StreamWriter($"../../../../Assets/PlayerData/{Player.instance.GetStatusStr(Player.Status.NAME)}.dat");
                sw_dat.WriteLine(Player.instance.GetStatusInt(Player.Status.LVL));
                sw_dat.WriteLine(Player.instance.GetStatusStr(Player.Status.NAME));
                sw_dat.WriteLine(Player.instance.GetStatusStr(Player.Status.JOB));
                sw_dat.WriteLine(Player.instance.GetStatusInt(Player.Status.ATK));
                sw_dat.WriteLine(Player.instance.GetStatusInt(Player.Status.BATK));
                sw_dat.WriteLine(Player.instance.GetStatusInt(Player.Status.DEF));
                sw_dat.WriteLine(Player.instance.GetStatusInt(Player.Status.BDEF));
                sw_dat.WriteLine(Player.instance.GetStatusInt(Player.Status.HP));
                sw_dat.WriteLine(Player.instance.GetStatusInt(Player.Status.MHP));
                sw_dat.WriteLine(Player.instance.GetStatusInt(Player.Status.BHP));
                sw_dat.WriteLine(Player.instance.GetStatusInt(Player.Status.GOLD));
                sw_dat.WriteLine(Player.instance.GetStatusInt(Player.Status.EXP));
                sw_dat.WriteLine(Player.instance.GetStatusInt(Player.Status.MEXP));
                for(int a=0; a<Player.instance.GetNItems(); a++)
                {
                    sw_dat.Write(Player.instance.GetItem(a).GetID() + " ");
                }
                sw_dat.Write('\n');
                for(int a=0; a<Player.instance.GetNItems(); a++)
                {
                    sw_dat.Write("{0} ", Player.instance.GetEquipStatus(a));
                }
                sw_dat.Write('\n');
                List<int> lis_player_quest_status = new List<int>();
                List<int> lis_player_quest_qID = new List<int>();
                List<int> lis_player_quest_Curr = new List<int>();
                List<int> lis_player_quest_Goal = new List<int>();
                for(int a=0; a<Player.instance.GetNQuestAccepted(); a++)
                {
                    (int,int,int,int) q_stat = Player.instance.GetQuestStatusViaIndex(a);
                    lis_player_quest_status.Add(q_stat.Item1);
                    lis_player_quest_qID.Add(q_stat.Item2);
                    lis_player_quest_Curr.Add(q_stat.Item3);
                    lis_player_quest_Goal.Add(q_stat.Item4);
                }
                for(int a=0; a<lis_player_quest_status.Count; a++)
                    sw_dat.Write(lis_player_quest_status[a]+ " ");
                sw_dat.Write('\n');
                for(int a=0; a<lis_player_quest_qID.Count; a++)
                    sw_dat.Write(lis_player_quest_qID[a]+ " ");
                sw_dat.Write('\n');
                for(int a=0; a<lis_player_quest_Curr.Count; a++)
                    sw_dat.Write(lis_player_quest_Curr[a]+ " ");
                sw_dat.Write('\n');
                for(int a=0; a<lis_player_quest_Goal.Count; a++)
                    sw_dat.Write(lis_player_quest_Goal[a]+ " ");
                sw_dat.Write('\n');
                sw_dat.Close();
            }
        }

    }
}        
         
         
         
         