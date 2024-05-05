using System.ComponentModel.Design;
using System.Xml.Serialization;

namespace W2Project
{
    internal class Program
    {
        public static List<Item> item_list;
        public static List<Enemy> enemy_list;
        bool loadData = false;

        public static string CenterAlign(string text, int width)
        {
            return text.PadLeft((width - text.Length) / 2 + text.Length).PadRight(width);
        }
        public static int Choice(int min, int max, int cutmin = -1, int cutmax = -1)
        {
            // 1 3 (-1 -1 ) 4
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

            Shop shop = new Shop();
            Scene scene = new Scene();

            // Start Scene
            scene.MoveScene(SceneType.Start);
            string username = Console.ReadLine();

            Player player;
            if(!File.Exists("../../../../Assets/PlayerList.txt"))
            {
                StreamWriter sw = new StreamWriter("../../../../Assets/PlayerList.txt");
                sw.Close();
            }
            string[] playerList = File.ReadAllLines("../../../../Assets/PlayerList.txt");
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
                List<string>equip = stats[15].Split(' ').ToList();
                List<Item> items_list = new List<Item>();
                List<bool> equip_list = new List<bool>();
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
                            equip_list.Add(true);
                        else
                            equip_list.Add(false);
                    }
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
                    equip_list
                    );
                sr.Close();
            }
            else
            {
                player = new Player(username);
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
            scene.MoveScene(SceneType.Jobs);
            string jobChoiceString = Console.ReadLine();

            if (int.Parse(jobChoiceString) == 1)
            {
                Player.instance.Warrior();
            }
            else if (int.Parse(jobChoiceString) == 2)
            {
                Player.instance.Archer();
            }
            else if (int.Parse(jobChoiceString) == 3)
            {
                Player.instance.Chief();
            }

            if (Player.instance.GetStatusStr(Player.Status.JOB) == "Warrior" || Player.instance.GetStatusStr(Player.Status.JOB) == "Archer" ||
                    Player.instance.GetStatusStr(Player.Status.JOB) == "Chief")
            {
                scene.MoveScene(SceneType.Main);
            }

            // MainScene
            scene.MoveScene(SceneType.Main);
            int choice = Choice(1, 3);
            int equip_opt = 0;
            int shop_opt = 0;
            bool wantSave = false;
            while (isPlaying)
            {
                switch (choice)
                {
                    case 0:
                        scene.MoveScene(SceneType.Main);
                        choice = Choice(0, 5);
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
                        if (
                            (Player.instance.GetStatusInt(Player.Status.HP)) +
                            (Player.instance.GetStatusInt(Player.Status.BHP)) <
                            100 + 
                            50*(Player.instance.GetStatusInt(Player.Status.LVL) -1) +
                                Player.instance.GetStatusInt(Player.Status.BHP)
                            )
                        {
                            Player.instance.FullHealth();
                        }
                        choice = 0;
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
                    if (Player.instance.GetEquipStatus(a) == true)
                        sw_dat.Write("1 ");
                    else
                        sw_dat.Write("0 ");
                }
                sw_dat.Close();
            }
        }

    }
}        
         
         
         
         