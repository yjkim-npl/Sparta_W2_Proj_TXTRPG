using System.ComponentModel.Design;
using System.Xml.Serialization;

namespace W2Project
{
    internal class Program
    {
        public static List<Item> item_list;
        public static string CenterAlign(string text, int width)
        {
            return text.PadLeft((width - text.Length) / 2 + text.Length).PadRight(width);
        }
        public static int Choice(int min, int max)
        {
            string in_str = Console.ReadLine();
            int choice;
            while(!int.TryParse(in_str, out choice) || !(choice >= min && choice <=max))
            {
                Console.SetCursorPosition(5, 22);
                Console.WriteLine("원하시는 행동을 입력해주세요 ({0} ~ {1}중에서 골라주세요)",min, max);
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
                    Item item = new Item(comp[1], int.Parse(comp[2]), int.Parse(comp[3]), int.Parse(comp[4]), int.Parse(comp[5]), int.Parse(comp[6]), comp[7]);
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
                List<string>items = stats[12].Split(' ').ToList();
                List<string>equip = stats[13].Split(' ').ToList();
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
                    int.Parse(stats[0]),
                    stats[1],
                    stats[2],
                    int.Parse(stats[3]),
                    int.Parse(stats[4]),
                    int.Parse(stats[5]),
                    int.Parse(stats[6]),
                    int.Parse(stats[7]),
                    int.Parse(stats[8]),
                    int.Parse(stats[9]),
                    int.Parse(stats[10]),
                    int.Parse(stats[11]),
                    items_list,
                    equip_list
                    );
                sr.Close();
            }
            else
            {
                player = new Player(username);
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
                        choice = Choice(0, 3);
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
                        break;
                    case 1:
                        scene.MoveScene(SceneType.Status);
                        choice = Choice(0, 0);
                        break;
                    case 2:

                        if(equip_opt == 1)
                        {
                            scene.MoveScene(SceneType.Inventory, equip_opt);
                            equip_opt = Choice(0,1);
                            if (equip_opt == 0)
                                choice = equip_opt;
                        }
                        else
                        {
                            scene.MoveScene(SceneType.Inventory);
                            equip_opt = Choice(0,1);
                            if (equip_opt == 0)
                                choice = equip_opt;
                        }
                        break;
                    case 3:
                        if (shop_opt == 1)
                        {
                            scene.MoveScene(SceneType.Shop, shop_opt);
                            shop_opt = Choice(0, 1);
                            if (shop_opt == 0)
                                choice = 0;
                        }
                        else
                        {
                            scene.MoveScene(SceneType.Shop);
                            shop_opt = Choice(0, 1);
                            if (shop_opt == 0)
                                choice = 0;
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
                sw.WriteLine(Player.instance.GetStatus(Player.Status.NAME));
                sw.Close();

                // Save the data to dat file
                StreamWriter sw_dat = new StreamWriter($"../../../../Assets/PlayerData/{Player.instance.GetStatus(Player.Status.NAME)}.dat");
                sw_dat.WriteLine(Player.instance.GetStatus(Player.Status.LVL));
                sw_dat.WriteLine(Player.instance.GetStatus(Player.Status.NAME));
                sw_dat.WriteLine(Player.instance.GetStatus(Player.Status.JOB));
                sw_dat.WriteLine(Player.instance.GetStatus(Player.Status.ATK));
                sw_dat.WriteLine(Player.instance.GetStatus(Player.Status.BATK));
                sw_dat.WriteLine(Player.instance.GetStatus(Player.Status.DEF));
                sw_dat.WriteLine(Player.instance.GetStatus(Player.Status.BDEF));
                sw_dat.WriteLine(Player.instance.GetStatus(Player.Status.HP));
                sw_dat.WriteLine(Player.instance.GetStatus(Player.Status.BHP));
                sw_dat.WriteLine(Player.instance.GetStatus(Player.Status.GOLD));
                sw_dat.WriteLine(Player.instance.GetStatus(Player.Status.EXP));
                sw_dat.WriteLine(Player.instance.GetStatus(Player.Status.MEXP));
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
         
         
         
         