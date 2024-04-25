using System.ComponentModel.Design;
using System.Xml.Serialization;

namespace W2Project
{
    internal class Program
    {
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
            bool isPlaying = false;
            Console.SetWindowSize(90, 30);
            Scene scene;
            // Test Scene

            // Start Scene
            scene = new Scene();
            string username = Console.ReadLine();
            Player player = new Player(username);
            // Current location is W2Project/W2Project/bin/Debug/net6.0/ 
            string item_list = "../../../../Assets/ItemList.csv";
            Shop shop = new Shop(item_list);

            // MainScene
//            scene.MoveScene(SceneType.Main);
            int choice = Choice(1, 3);
            int equip_opt = 0;
            int shop_opt = 0;
            while (isPlaying)
            {
                switch (choice)
                {
                    case 0:
                        scene.MoveScene(SceneType.Main);
                        choice = Choice(1, 3);
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
                        }
                        break;
                    default:
                        scene.MoveScene(SceneType.Main);
                        choice = Choice(0, 4);
                        break;
                }
            }
        }

    }
}        
         
         
         
         