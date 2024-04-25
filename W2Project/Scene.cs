using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace W2Project
{
    internal class Scene
    {
        public Scene()
        {
        }
        public void MoveScene(SceneType type, int opt = 0)
        {
            BaseScene();
            switch (type)
            {
                case SceneType.Start:
                    Console.SetCursorPosition(0,10);
                    Console.WriteLine("{0}",Program.CenterAlign("마트료시카 RPG",Console.WindowWidth));
                    Console.SetCursorPosition(35, 20);
                    Console.Write("이름을 입력하세요: ");
                    Console.SetCursorPosition(55, 20);
                    break;
                case SceneType.Main:
                    Console.SetCursorPosition(5, 3);
                    Console.Write("[스파르타 마을]");
                    Console.SetCursorPosition(5, 4);
                    Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                    Console.SetCursorPosition(5, 5);
                    Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
                    Console.SetCursorPosition(7, 7);
                    Console.WriteLine("1. 상태보기");
                    Console.SetCursorPosition(7, 8);
                    Console.WriteLine("2. 인벤토리");
                    Console.SetCursorPosition(7, 9);
                    Console.WriteLine("3. 상점");
                    Console.SetCursorPosition(7, 11);
                    Console.WriteLine("0. 게임 종료");
                    Console.SetCursorPosition(5, 22);
                    Console.WriteLine("원하시는 행동을 입력해주세요");
                    Console.SetCursorPosition(5, 23);
                    Console.Write(">>");
                    Console.SetCursorPosition(8, 23);
                    break;
                case SceneType.Status:
                    Console.SetCursorPosition(5, 3);
                    Console.WriteLine("[상태 보기]");
                    Console.SetCursorPosition(5, 4);
                    Console.WriteLine("캐릭터의 정보가 표시됩니다.");
                    Console.SetCursorPosition(7, 6);
                    Console.WriteLine("Level: {0}", Player.instance.GetStatus(Player.Status.LVL));
                    Console.SetCursorPosition(7, 7);
                    Console.WriteLine("{0} , ({1})", Player.instance.GetStatus(Player.Status.NAME), Player.instance.GetStatus(Player.Status.JOB));
                    Console.SetCursorPosition(7, 8);
                    Console.WriteLine("공격력: {0}", Player.instance.GetStatus(Player.Status.ATK));
                    if (Player.instance.GetStatus(Player.Status.BATK) != null)
                    {
                        Console.SetCursorPosition(18, 8);
                        Console.WriteLine("+ {0}", Player.instance.GetStatus(Player.Status.BATK));
                    }
                    Console.SetCursorPosition(7, 9);
                    Console.WriteLine("방어력: {0}", Player.instance.GetStatus(Player.Status.DEF));
                    if (Player.instance.GetStatus(Player.Status.BDEF) != null)
                    {
                        Console.SetCursorPosition(18, 9);
                        Console.WriteLine("+ {0}", Player.instance.GetStatus(Player.Status.BDEF));
                    }
                    Console.SetCursorPosition(7, 10);
                    Console.WriteLine("체 력: {0}", Player.instance.GetStatus(Player.Status.HP));
                    if (Player.instance.GetStatus(Player.Status.BHP) != null)
                    {
                        Console.SetCursorPosition(18, 10);
                        Console.WriteLine("+ {0}", Player.instance.GetStatus(Player.Status.BHP));
                    }
                    Console.SetCursorPosition(7, 11);
                    Console.WriteLine("Gold: {0}", Player.instance.GetStatus(Player.Status.GOLD) + " G");
                    Console.SetCursorPosition(7, 12);
                    Console.WriteLine("ExpL {0} / {1}", Player.instance.GetStatus(Player.Status.EXP), Player.instance.GetStatus(Player.Status.MEXP));
                    Console.SetCursorPosition(7, 16);
                    Console.WriteLine("0. 나가기");
                    Console.SetCursorPosition(5, 22);
                    Console.WriteLine("원하시는 행동을 입력해주세요");
                    Console.SetCursorPosition(5, 23);
                    Console.Write(">>");
                    Console.SetCursorPosition(8, 23);
                    break;
                case SceneType.Inventory:
                    Console.SetCursorPosition(5, 3);
                    Console.WriteLine("[인벤토리]");
                    Console.SetCursorPosition(5, 4);
                    Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");

                    Console.SetCursorPosition(7, 7);
                    Console.WriteLine("[아이템 목록]");
                    for(int a=0; a<Player.instance.GetNItems(); a++)
                    {
                        Item item = Player.instance.GetItem(a);
                        bool isEquip = Player.instance.GetEquipStatus(a);
                        Console.SetCursorPosition(7, 8+a);
                        if (opt == 0)
                            Console.WriteLine(isEquip ? "-  [E] " : "- ");
                        else if (opt == 1)
                            Console.WriteLine(isEquip ? "{0}. [E] " : "{1}",a+1,a+1);
                        else
                            Console.WriteLine(isEquip ? "-  [E] " : "- ");
                        Console.SetCursorPosition(15, 8 + a);
                        Console.WriteLine(item.GetName());
                        if (item.GetBAtk() != 0)
                        {
                            Console.SetCursorPosition(29, 8 + a);
                            Console.WriteLine("| 공격력 +");
                            Console.SetCursorPosition(41, 8 + a);
                            Console.WriteLine(item.GetBAtk().ToString());
                            Console.SetCursorPosition(43, 8 + a);
                            Console.WriteLine("|");
                            Console.SetCursorPosition(45, 8 + a);
                        }
                        if (item.GetBDef() != 0)
                        {
                            Console.SetCursorPosition(29, 8 + a);
                            Console.WriteLine("| 방어력 +");
                            Console.SetCursorPosition(41, 8 + a);
                            Console.WriteLine(item.GetBDef().ToString());
                            Console.SetCursorPosition(43, 8 + a);
                            Console.WriteLine("|");
                            Console.SetCursorPosition(45, 8 + a);
                        }
                        if(item.GetBDef() == 0 && item.GetBAtk() == 0)
                        {
                            Console.SetCursorPosition(29, 8 + a);
                            Console.WriteLine("|");
                            Console.SetCursorPosition(45, 8 + a);
                        }
                        Console.WriteLine(item.GetExplanation());

                    }

                    if (opt == 1)
                    {
                        Console.SetCursorPosition(5, 18);
                        Console.WriteLine("장착 / 장착 해제 하고싶은 아이템의 번호를 입력하세요");
                        Console.SetCursorPosition(5, 20);
                        Console.WriteLine("0. 나가기");
                        Console.SetCursorPosition(5, 23);
                        Console.Write(">>     ");
                        Console.SetCursorPosition(8, 23);
                        int itemNo = Program.Choice(0,Player.instance.GetNItems());
                        while (itemNo != 0)
                        {
                            if(itemNo <0 || itemNo > Player.instance.GetNItems())
                            {
                                Console.SetCursorPosition(5, 18);
                                Console.WriteLine("잘못된 입력입니다.장착 / 장착 해제 하고싶은 아이템의 번호를 입력하세요");
                            }
                            else if (Player.instance.GetEquipStatus(itemNo - 1) == true)
                            {
                                Player.instance.UpEquip(itemNo - 1);
                                Console.SetCursorPosition(5, 18);
                                Console.WriteLine("장착이 해제되었습니다.                                                     ");
                                Console.SetCursorPosition(5, 19);
                                Console.WriteLine("장착 / 장착 해제 하고싶은 아이템의 번호를 입력하세요");
                                Console.SetCursorPosition(7, 7 + itemNo);
                                Console.WriteLine("{0}.     ", itemNo);
                            }
                            else if (Player.instance.GetEquipStatus(itemNo - 1) == false)
                            {
                                Player.instance.Equip(itemNo - 1);
                                Console.SetCursorPosition(5, 18);
                                Console.WriteLine("장착되었습니다.                                                       ");
                                Console.SetCursorPosition(5, 19);
                                Console.WriteLine("장착 / 장착 해제 하고싶은 아이템의 번호를 입력하세요");
                                Console.SetCursorPosition(7, 7 + itemNo);
                                Console.WriteLine("{0}  [E] ", itemNo);
                            }
                            Console.SetCursorPosition(5, 20);
                            Console.WriteLine("0. 나가기");
                            Console.SetCursorPosition(5, 23);
                            Console.Write(">>     ");
                            Console.SetCursorPosition(8, 23);
                            itemNo = Program.Choice(0,Player.instance.GetNItems());
                        }
                        for(int a=0; a<Player.instance.GetNItems(); a++)
                        {
                            Console.SetCursorPosition(7,8+ a);
                            Console.WriteLine("- ");
                        }
                    }
                    Console.SetCursorPosition(5, 18);
                    Console.WriteLine("1. 장착관리                                                          ");
                    Console.SetCursorPosition(5, 19);
                    Console.WriteLine("0. 나가기                                             ");
                    Console.SetCursorPosition(5, 20);
                    Console.WriteLine("                                          ");

                    Console.SetCursorPosition(5, 22);
                    Console.WriteLine("원하시는 행동을 입력해주세요");
                    Console.SetCursorPosition(5, 23);
                    Console.Write(">>  ");
                    Console.SetCursorPosition(8, 23);
                    break;
                case SceneType.Shop:
                    Console.SetCursorPosition(5, 3);
                    Console.WriteLine("[상점]");
                    Console.SetCursorPosition(5, 4);
                    Console.WriteLine("필요한 아이템을 구매할 수 있는 상점입니다.");

                    Console.SetCursorPosition(7, 6);
                    Console.WriteLine("[보유골드]");
                    Console.SetCursorPosition(7, 7);
                    Console.WriteLine("{0,5} G", Player.instance.GetStatus(Player.Status.GOLD));
                    Console.SetCursorPosition(7, 9);
                    Console.WriteLine("[아이템 목록]");
                    for(int a=0; a<Shop.instance.GetNItem(); a++)
                    {
                        Item item = Shop.instance.GetItem(a);
                        Console.SetCursorPosition(7, 10+a);
                        Console.WriteLine("- ");
                        Console.SetCursorPosition(10, 10 + a);
                        Console.WriteLine(item.GetName());
                        if (item.GetBAtk() != 0)
                        {
                            Console.SetCursorPosition(24, 10 + a);
                            Console.WriteLine("| 공격력 +");
                            Console.SetCursorPosition(36, 10 + a);
                            Console.WriteLine(item.GetBAtk().ToString());
                            Console.SetCursorPosition(38, 10 + a);
                            Console.WriteLine("|");
                            Console.SetCursorPosition(40, 10 + a);
                            Console.WriteLine("{0,4} G", item.GetPrice());
                        }
                        if (item.GetBDef() != 0)
                        {
                            Console.SetCursorPosition(24, 10 + a);
                            Console.WriteLine("| 방어력 +");
                            Console.SetCursorPosition(36, 10 + a);
                            Console.WriteLine(item.GetBDef().ToString());
                            Console.SetCursorPosition(38, 10 + a);
                            Console.WriteLine("|");
                            Console.SetCursorPosition(40, 10 + a);
                            Console.WriteLine("{0,4} G", item.GetPrice());
                        }
                        Console.SetCursorPosition(47, 10 + a);
                        Console.WriteLine("|");
                        Console.SetCursorPosition(50, 10 + a);
                        Console.WriteLine(item.GetExplanation());
                    }

                    Console.SetCursorPosition(5,18);
                    Console.WriteLine("1. 아이템 구매");
                    Console.SetCursorPosition(5,19);
                    Console.WriteLine("0. 나가기");
                    if (opt == 1)
                    {
                        for(int a=0; a<Shop.instance.GetNItem(); a++)
                        {
                            Console.SetCursorPosition(7, 10+a);
                            Console.WriteLine("{0} ",a+1);
                        }
                        Console.SetCursorPosition(5, 18);
                        Console.WriteLine("구매하고싶은 아이템의 번호를 입력하세요.");
                        Console.SetCursorPosition(5, 19);
                        Console.WriteLine("0. 나가기");
                        Console.SetCursorPosition(5, 23);
                        Console.Write(">>     ");
                        Console.SetCursorPosition(8, 23);
                        int itemNo = Program.Choice(0,Shop.instance.GetNItem());
                        //int itemNo = int.Parse(Console.ReadLine());
                        while(itemNo !=0)
                        {
                            Console.SetCursorPosition(5, 19);
                            Console.WriteLine("0. 나가기");
                            Console.SetCursorPosition(5, 23);
                            Console.Write(">>     ");
                            Console.SetCursorPosition(5, 18);
                            if(int.Parse(Player.instance.GetStatus(Player.Status.GOLD)) < Shop.instance.GetItem(itemNo-1).GetPrice())
                            {
                                Console.WriteLine("소지금이 모자랍니다.                         ");
                            }
                            else if(Shop.instance.GetStock(itemNo-1) <=0)
                            {
                                Console.WriteLine("재고가 부족합니다.                         ");
                            }
                            else
                            {
                                Console.WriteLine("구매가 완료되었습니다.                     ");
                                Player.instance.BuyItem(Shop.instance.GetItem(itemNo-1));
                                Shop.instance.SellItem(itemNo-1);
                                Console.SetCursorPosition(7, 7);
                                Console.WriteLine("{0,5} G", Player.instance.GetStatus(Player.Status.GOLD));
                            }
                            Console.SetCursorPosition(8, 23);
                            itemNo = Program.Choice(0, Shop.instance.GetNItem());
                        }
                        for(int a=0; a<Shop.instance.GetNItem(); a++)
                        {
                            Console.SetCursorPosition(7,10+ a);
                            Console.WriteLine("- ");
                        }
                        Console.SetCursorPosition(5, 18);
                        Console.WriteLine("1. 아이템 구매                                         ");
                    }

                    Console.SetCursorPosition(5, 22);
                    Console.WriteLine("원하시는 행동을 입력해주세요                             ");
                    Console.SetCursorPosition(5, 23);
                    Console.Write(">>  ");
                    Console.SetCursorPosition(8, 23);
                    break;
                case SceneType.Dungeon:
                    break;
                case SceneType.End:
                    Console.SetCursorPosition(0,10);
                    Console.WriteLine("{0}",Program.CenterAlign("저장하시겠습니까?",Console.WindowWidth));
                    Console.SetCursorPosition(7,15);
                    Console.WriteLine("1. 저장 후 종료");
                    Console.SetCursorPosition(7,16);
                    Console.WriteLine("2. 그냥 종료");
                    Console.SetCursorPosition(5, 22);
                    Console.WriteLine("원하시는 행동을 입력해주세요 ");
                    Console.SetCursorPosition(5, 23);
                    Console.Write(">>  ");
                    break;
                default:
                    break;
            }

        }
        public void BaseScene()
        {
            Console.Clear();
            Console.WriteLine("--------------------------------------------------------------------------------------------");
            Console.SetCursorPosition(0, 29);
            Console.WriteLine("--------------------------------------------------------------------------------------------");
        }
    }
    public enum SceneType
    {
        Start,
        Main,
        Status,
        Inventory,
        Shop,
        Dungeon,
        End
    }
}
