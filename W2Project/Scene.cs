using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace W2Project
{
    internal class Scene
    {
        public int curPageInven = 0;
        public int curPageShop = 0;
        public int curPageQuest = 0;
        public int nItemsOnPage = 7;
        public int nQuestsOnPage = 4;
        int maxPageInven = 0;
        int maxPageShop = 0;
        int maxPageQuest = 0;
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
                case SceneType.Jobs:
                    Console.SetCursorPosition(0, 10);
                    Console.SetCursorPosition(29, 10); Console.WriteLine("플레이어의 직업을 선택해주세요.");
                    Console.SetCursorPosition(21, 12); Console.WriteLine("1. 전사 -> 체력 +100, 방어력 +3");
                    Console.SetCursorPosition(21, 13); Console.WriteLine("2. 궁수 -> 공격력 +5, 체력 -40, 방어력 -3");
                    Console.SetCursorPosition(21, 14); Console.WriteLine("3. 도적 -> 공격력 +3, 체력 -20, 방어력 -2");
                    Console.SetCursorPosition(0, 16); Console.Write(">>");
                    break;
                case SceneType.Main:
                    Console.SetCursorPosition(5, 3);
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.Write("[스파르타 마을]");
                    Console.ResetColor();
                    Console.SetCursorPosition(5, 4);  Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
                    Console.SetCursorPosition(5, 5);  Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
                    Console.SetCursorPosition(7, 7);  Console.WriteLine("1. 상태보기");
                    Console.SetCursorPosition(7, 8);  Console.WriteLine("2. 인벤토리");
                    Console.SetCursorPosition(7, 9);  Console.WriteLine("3. 상점");
                    Console.SetCursorPosition(7, 10); Console.WriteLine("4. 던전");
                    Console.SetCursorPosition(7, 11); Console.WriteLine("5. 휴식 및 강화");
                    Console.SetCursorPosition(7, 12); Console.WriteLine("6. 퀘스트 게시판");
                    Console.SetCursorPosition(7, 14); Console.WriteLine("0. 게임 종료");
                    Console.SetCursorPosition(5, 22); Console.WriteLine("원하시는 행동을 입력해주세요");
                    Console.SetCursorPosition(5, 23); Console.Write(">>");
                    Console.SetCursorPosition(8, 23);
                    break;
                case SceneType.Status:
                    Console.SetCursorPosition(5, 3);
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine("[상태 보기]");
                    Console.ResetColor();
                    Console.SetCursorPosition(5, 4); Console.WriteLine("캐릭터의 정보가 표시됩니다.");
                    Console.SetCursorPosition(7, 6); Console.WriteLine("{0} , ({1})", Player.instance.GetStatusStr(Player.Status.NAME), Player.instance.GetStatusStr(Player.Status.JOB));
                    Console.SetCursorPosition(7, 7); Console.WriteLine("Level");
                    Console.SetCursorPosition(13, 7); Console.WriteLine(":");
                    Console.SetCursorPosition(15, 7); Console.WriteLine("{0}",Player.instance.GetStatusInt(Player.Status.LVL));
                    Console.SetCursorPosition(7, 8); Console.WriteLine("ATK");
                    Console.SetCursorPosition(13, 8); Console.WriteLine(":");
                    Console.SetCursorPosition(15, 8); Console.WriteLine("{0}", Player.instance.GetStatusInt(Player.Status.ATK));
                    Console.SetCursorPosition(7, 9); Console.WriteLine("DEF");
                    Console.SetCursorPosition(13, 9); Console.WriteLine(":");
                    Console.SetCursorPosition(15, 9); Console.WriteLine("{0}", Player.instance.GetStatusInt(Player.Status.DEF));
                    Console.SetCursorPosition(7, 10); Console.WriteLine("HP");
                    Console.SetCursorPosition(13, 10); Console.WriteLine(":");
                    Console.SetCursorPosition(15, 10); Console.WriteLine("{0}", Player.instance.GetStatusInt(Player.Status.HP), Player.instance.GetStatusInt(Player.Status.MHP));
                    Console.SetCursorPosition(7, 11); Console.WriteLine("Gold");
                    Console.SetCursorPosition(13, 11); Console.WriteLine(":");
                    Console.SetCursorPosition(15, 11); Console.WriteLine("{0} G", Player.instance.GetStatusInt(Player.Status.GOLD));
                    Console.SetCursorPosition(7, 12); Console.WriteLine("Exp");
                    Console.SetCursorPosition(13, 12); Console.WriteLine(":");
                    Console.SetCursorPosition(15, 12); Console.WriteLine("{0} / {1}", Player.instance.GetStatusInt(Player.Status.EXP), Player.instance.GetStatusInt(Player.Status.MEXP));
                    if (Player.instance.GetStatusInt(Player.Status.BATK) != 0)
                    {
                        int hier = Player.instance.GetHierachyLvl(Player.Hierachy.Weapon);
                        string str_hier = "";
                        if ((hier & 1)== 1)
                            str_hier += "소";
                        if ((hier & 2)>>1 == 1)
                            str_hier += ",중";
                        if ((hier & 4)>>2 == 1)
                            str_hier += ",대";
                        Console.SetCursorPosition(18, 8);
                        Console.WriteLine("+ {0} ({1})", Player.instance.GetStatusInt(Player.Status.BATK),str_hier);
                    }
                    if (Player.instance.GetStatusInt(Player.Status.BDEF) != 0)
                    {
                        int hier = Player.instance.GetHierachyLvl(Player.Hierachy.Armor);
                        string str_hier = "";
                        if ((hier & 1)== 1)
                            str_hier += "소";
                        if ((hier & 2)>>1 == 1)
                            str_hier += ",중";
                        if ((hier & 4)>>2 == 1)
                            str_hier += ",대";
                        Console.SetCursorPosition(18, 9);
                        Console.WriteLine("+ {0} ({1})", Player.instance.GetStatusInt(Player.Status.BDEF), str_hier);
                    }
                    if (Player.instance.GetStatusInt(Player.Status.BHP) != 0)
                    {
                        int hier = Player.instance.GetHierachyLvl(Player.Hierachy.Ring);
                        string str_hier = "";
                        if ((hier & 1)== 1)
                            str_hier += "N";
                        if ((hier & 2)>>1 == 1)
                            str_hier += ",R";
                        if ((hier & 4)>>2 == 1)
                            str_hier += ",U";
                        if ((hier & 8)>>3 == 1)
                            str_hier += ",L";
                        int cur_HP = Player.instance.GetStatusInt(Player.Status.HP);
                        int cur_lngth = (int)(Math.Log10(cur_HP)+1);
                        Console.SetCursorPosition(16+cur_lngth, 10);
                        Console.WriteLine("/ ({0} + {1}) ({2})", Player.instance.GetStatusInt(Player.Status.MHP),Player.instance.GetStatusInt(Player.Status.BHP), str_hier);
                    }
                    else
                    {
                        int cur_HP = Player.instance.GetStatusInt(Player.Status.HP);
                        int cur_lngth = (int)(Math.Log10(cur_HP)+1);
                        Console.SetCursorPosition(16+cur_lngth, 10);
                        Console.WriteLine("/ {0}", Player.instance.GetStatusInt(Player.Status.MHP));

                    }
                    Console.SetCursorPosition(7, 16); Console.WriteLine("0. 나가기");
                    Console.SetCursorPosition(5, 22); Console.WriteLine("원하시는 행동을 입력해주세요");
                    Console.SetCursorPosition(5, 23); Console.Write(">>");
                    Console.SetCursorPosition(8, 23);
                    break;
                case SceneType.Inventory:
                    Console.SetCursorPosition(5, 3);
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine("[인벤토리]");
                    Console.ResetColor();
                    Console.SetCursorPosition(5, 4); Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");

                    Console.SetCursorPosition(7, 7); Console.WriteLine("[아이템 목록]");
                    maxPageInven = Player.instance.GetNItems() / nItemsOnPage;
                    curPageInven += (opt==8?-1:opt==9?1:0);
                    if(curPageInven <0)
                        curPageInven = 0;
                    if(curPageInven > maxPageInven)
                        curPageInven = maxPageInven;
                        Console.SetCursorPosition(65, 7); Console.WriteLine("{0} p / {1} p  (8)< | >(9)",curPageInven,maxPageInven);
                    for(int a=0; a<(curPageInven==maxPageInven?Player.instance.GetNItems()%nItemsOnPage:nItemsOnPage); a++)
                    {
                        Item item = Player.instance.GetItem(a +nItemsOnPage*curPageInven);
                        int isEquip = Player.instance.GetEquipStatus(a +nItemsOnPage*curPageInven);
                        Console.SetCursorPosition(7, 8+a);
                        if (opt == 0)
                            Console.WriteLine(isEquip==1 ? "-  [E] " : "- ");
                        else if (opt == 1)
                            Console.WriteLine(isEquip == 1 ? "{0}. [E] " : "{1}",a+1,a+1);
                        else
                            Console.WriteLine(isEquip == 1 ? "-  [E] " : "- ");
                        Console.SetCursorPosition(15, 8 + a); 
                        if(item.GetType() == ItemType.Use)
                        {
                            Console.WriteLine(item.GetName()+ " ({0})",Player.instance.GetNumberOfPotion(item.GetBHP()>0?0:item.GetBAtk()>0?1:item.GetBDef()>0?2:-1));
                        }
                        else
                        {
                            Console.WriteLine(item.GetName());
                        }
                        Console.SetCursorPosition(29, 8 + a);
                        switch(item.GetHierachy())
                        {
                            case 0:
                            case 1:
                                Console.WriteLine("| 소");
                                break;
                            case 2:
                                Console.WriteLine("| 중");
                                break;
                            case 4:
                                Console.WriteLine("| 대");
                                break;
                            default:
                                Console.WriteLine("|");
                                break;
                        }
                        Console.SetCursorPosition(34, 8 + a);
                        switch(item.GetRank())
                        {
                            case 1:
                                Console.WriteLine("| N");
                                break;
                            case 2:
                                Console.WriteLine("| R");
                                break;
                            case 4:
                                Console.WriteLine("| U");
                                break;
                            case 8:
                                Console.WriteLine("| L");
                                break;
                            default:
                                Console.WriteLine("|");
                                break;
                        }

                        if (item.GetBAtk() != 0)
                        {
                            Console.SetCursorPosition(39, 8 + a); Console.WriteLine("| 공격력 +");
                            Console.SetCursorPosition(51, 8 + a); Console.WriteLine(item.GetBAtk().ToString());
                            Console.SetCursorPosition(53, 8 + a); Console.WriteLine("|");
                            Console.SetCursorPosition(55, 8 + a);
                        }
                        if (item.GetBDef() != 0)
                        {
                            Console.SetCursorPosition(39, 8 + a); Console.WriteLine("| 방어력 +");
                            Console.SetCursorPosition(51, 8 + a); Console.WriteLine(item.GetBDef().ToString());
                            Console.SetCursorPosition(53, 8 + a); Console.WriteLine("|");
                            Console.SetCursorPosition(55, 8 + a);
                        }
                        if(item.GetBHP() != 0)
                        {
                            Console.SetCursorPosition(39, 8 + a); Console.WriteLine("| H   P +");
                            Console.SetCursorPosition(51, 8 + a); Console.WriteLine(item.GetBHP().ToString());
                            Console.SetCursorPosition(53, 8 + a); Console.WriteLine("|");
                            Console.SetCursorPosition(55, 8 + a);

                        }
                        if(item.GetBDef() == 0 && item.GetBAtk() == 0)
                        {
                            Console.SetCursorPosition(39, 8 + a); Console.WriteLine("|");
                            Console.SetCursorPosition(55, 8 + a);
                        }
                        Console.WriteLine(item.GetExplanation());

                    }
                    Console.SetCursorPosition(5, 19); Console.WriteLine("1. 장착관리");
                    Console.SetCursorPosition(5, 20); Console.WriteLine("{0,-70}","0. 나가기");
                    Console.SetCursorPosition(5, 21); Console.WriteLine(" ");

                    Console.SetCursorPosition(5, 22); Console.WriteLine("원하시는 행동을 입력해주세요");
                    Console.SetCursorPosition(5, 23); Console.Write(">>  ");
                    Console.SetCursorPosition(8, 23);

                    if (opt == 1)
                    {
                        Console.SetCursorPosition(65, 7); Console.WriteLine("{0,23}","");
                        Console.SetCursorPosition(5, 18); Console.WriteLine("장착 / 장착 해제 하고싶은 아이템의 번호를 입력하세요");
                        Console.SetCursorPosition(5, 19); Console.WriteLine("{0,15}","");
                        Console.SetCursorPosition(5, 20); Console.WriteLine("0. 나가기");
                        Console.SetCursorPosition(5, 23); Console.Write(">>     ");
                        Console.SetCursorPosition(8, 23);
                        int itemNo = Program.Choice(0,curPageInven==maxPageInven?Player.instance.GetNItems()%nItemsOnPage:nItemsOnPage);
                        int fType = new int();
                        int itemHier = new int();
                        int fWeaponLvl = new int();
                        int fArmorLvl = new int(); 
                        while (itemNo != 0 && itemNo <=nItemsOnPage)
                        {
                            fType      = (int)Player.instance.GetItem(nItemsOnPage*curPageInven + itemNo - 1).GetType();
                            itemHier   = Player.instance.GetItem(nItemsOnPage*curPageInven + itemNo - 1).GetHierachy();
                            fWeaponLvl = Player.instance.GetHierachyLvl(Player.Hierachy.Weapon);
                            fArmorLvl  = Player.instance.GetHierachyLvl(Player.Hierachy.Armor);
                            if (itemNo < 0 || itemNo > Player.instance.GetNItems())
                            {
                                Console.SetCursorPosition(5, 18); Console.WriteLine("잘못된 입력입니다.장착 / 장착 해제 하고싶은 아이템의 번호를 입력하세요");
                            }
                            else if (itemNo != 0 && (Player.instance.GetHierachyLvl((Player.Hierachy) fType) >> ((int)Math.Log2(itemHier)+1)) != 0)
                            {
                                Console.SetCursorPosition(5, 18); Console.WriteLine("착용하고 있는 사이즈보다 작습니다. 큰 사이즈의 아이템을 벗어주세요.            ");
                                Console.SetCursorPosition(5, 19); Console.WriteLine("장착 / 장착 해제 하고싶은 아이템의 번호를 입력하세요");
                            }
                            else if (Player.instance.GetEquipStatus(nItemsOnPage*curPageInven + itemNo - 1) == 1)
                            {
                                Player.instance.UpEquip(nItemsOnPage*curPageInven + itemNo - 1);
                                Console.SetCursorPosition(5, 18); Console.WriteLine("장착이 해제되었습니다.                                                     ");
                                Console.SetCursorPosition(5, 19); Console.WriteLine("장착 / 장착 해제 하고싶은 아이템의 번호를 입력하세요");
                                Console.SetCursorPosition(7, 7 + itemNo);
                                Console.WriteLine("{0}.     ", itemNo);
                            }
                            else if (Player.instance.GetEquipStatus(nItemsOnPage*curPageInven + itemNo - 1) == 0)
                            {
                                Player.instance.Equip(nItemsOnPage*curPageInven + itemNo - 1);
                                Console.SetCursorPosition(5, 18); Console.WriteLine("장착되었습니다.                                                       ");
                                Console.SetCursorPosition(5, 19); Console.WriteLine("장착 / 장착 해제 하고싶은 아이템의 번호를 입력하세요");
                                Console.SetCursorPosition(7, 7 + itemNo);
                                Console.WriteLine("{0}. [E] ", itemNo);
                            }
                            Console.SetCursorPosition(5, 20); Console.WriteLine("0. 나가기");
                            Console.SetCursorPosition(5, 23); Console.Write(">>     ");
                            Console.SetCursorPosition(8, 23);
                            itemNo = Program.Choice(0,curPageInven==maxPageInven?Player.instance.GetNItems()%nItemsOnPage:nItemsOnPage);
                            Console.SetCursorPosition(8, 23);
                            if (itemNo != 0)
                            {
                                fType = (int)Player.instance.GetItem(nItemsOnPage*curPageInven + itemNo - 1).GetType();
                                itemHier = Player.instance.GetItem(nItemsOnPage*curPageInven + itemNo - 1).GetHierachy();
                                fWeaponLvl = Player.instance.GetHierachyLvl(Player.Hierachy.Weapon);
                                fArmorLvl = Player.instance.GetHierachyLvl(Player.Hierachy.Armor);
                            }
                        }
                        for(int a=0; a<(curPageInven==maxPageInven?Player.instance.GetNItems()%nItemsOnPage:nItemsOnPage); a++)
                        {
                            Console.SetCursorPosition(7,8+ a);
                            Console.WriteLine("- ");
                        }
                        if (opt == 0)
                        {
                            Console.SetCursorPosition(65, 7); Console.WriteLine("{0} p / {1} p  (8)< | >(9)", curPageInven, maxPageInven);
                        }
                        Console.SetCursorPosition(5, 18); Console.WriteLine("{0,-70}","");
                        Console.SetCursorPosition(5, 19); Console.WriteLine("{0,-60}","1. 장착관리");
                        Console.SetCursorPosition(5, 20); Console.WriteLine("0. 나가기                                             ");
                        Console.SetCursorPosition(5, 22); Console.WriteLine("원하시는 행동을 입력해주세요                          ");
                        Console.SetCursorPosition(5, 23); Console.Write(">>  ");
                    }
                    break;
                case SceneType.Shop:
                    Console.SetCursorPosition(5, 3);
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine("[상점]");
                    Console.ResetColor();
                    Console.SetCursorPosition(5, 4); Console.WriteLine("필요한 아이템을 구매할 수 있는 상점입니다.");

                    Console.SetCursorPosition(7, 6); Console.WriteLine("[보유골드]");
                    Console.SetCursorPosition(7, 7); Console.WriteLine("{0,5} G", Player.instance.GetStatusInt(Player.Status.GOLD));
                    Console.SetCursorPosition(7, 9); Console.WriteLine("[아이템 목록]");
                    maxPageShop = Shop.instance.GetNItem() / nItemsOnPage;
                    curPageShop += (opt==8?-1:opt==9?1:0);
                    if(curPageShop <0)
                        curPageShop = 0;
                    if(curPageShop > maxPageShop)
                        curPageShop = maxPageShop;
                        Console.SetCursorPosition(65, 9); Console.WriteLine("{0} p / {1} p  (8)< | >(9)",curPageShop,maxPageShop);
                    for(int a=0; a<(curPageShop==maxPageShop?Shop.instance.GetNItem()%nItemsOnPage:nItemsOnPage); a++)
                    {
                        Item item = Shop.instance.GetItem(a + nItemsOnPage*curPageShop);
                        Console.SetCursorPosition(7, 10+a);
                        Console.WriteLine("- ");
                        Console.SetCursorPosition(10, 10 + a);
                        Console.WriteLine(item.GetName()+ " ({0})  ",Shop.instance.GetStock(nItemsOnPage*curPageShop+a));
                        if (item.GetBAtk() != 0)
                        {
                            Console.SetCursorPosition(29, 10 + a); Console.WriteLine("| 공격력 +");
                            Console.SetCursorPosition(41, 10 + a); Console.WriteLine(item.GetBAtk().ToString());
                        }
                        if (item.GetBDef() != 0)
                        {
                            Console.SetCursorPosition(29, 10 + a); Console.WriteLine("| 방어력 +");
                            Console.SetCursorPosition(41, 10 + a); Console.WriteLine(item.GetBDef().ToString());
                        }
                        if (item.GetBHP() != 0)
                        {
                            Console.SetCursorPosition(29, 10 + a); Console.WriteLine("| H  P +");
                            Console.SetCursorPosition(41, 10 + a); Console.WriteLine(item.GetBHP().ToString());
                        }
                        Console.SetCursorPosition(43, 10 + a); Console.WriteLine("|");
                        Console.SetCursorPosition(45, 10 + a); Console.WriteLine("{0,4} G", item.GetPrice());
                        Console.SetCursorPosition(52, 10 + a); Console.WriteLine("|");
                        Console.SetCursorPosition(55, 10 + a); Console.WriteLine(item.GetExplanation());
                    }

                    Console.SetCursorPosition(5,19); Console.WriteLine("1. 아이템 구매");
                    Console.SetCursorPosition(5,20); Console.WriteLine("0. 나가기");
                    if (opt == 1)
                    {
                        Console.SetCursorPosition(5, 19); Console.WriteLine("{0,20}", "");
                        for(int a=0; a<(curPageShop==maxPageShop?Shop.instance.GetNItem()%nItemsOnPage:nItemsOnPage); a++)
                        {
                            Console.SetCursorPosition(7, 10+a);
                            Console.WriteLine("{0} ",a+1);
                        }
                        Console.SetCursorPosition(5, 18); Console.WriteLine("구매하고싶은 아이템의 번호를 입력하세요.");
                        Console.SetCursorPosition(5, 20); Console.WriteLine("0. 나가기");
                        Console.SetCursorPosition(5, 23); Console.Write(">>     ");
                        Console.SetCursorPosition(8, 23);
                        int itemNo = Program.Choice(0,curPageShop==maxPageShop?Shop.instance.GetNItem()%nItemsOnPage:nItemsOnPage);
                        //int itemNo = int.Parse(Console.ReadLine());
                        while(itemNo !=0)
                        {
                            Console.SetCursorPosition(5, 20); Console.WriteLine("0. 나가기");
                            Console.SetCursorPosition(5, 23); Console.Write(">>     ");
                            Console.SetCursorPosition(5, 18);
                            if(Player.instance.GetStatusInt(Player.Status.GOLD) < Shop.instance.GetItem(curPageShop*nItemsOnPage + itemNo-1).GetPrice())
                            {
                                Console.WriteLine("소지금이 모자랍니다.                         ");
                            }
                            else if(Shop.instance.GetStock(curPageShop*nItemsOnPage + itemNo-1) <=0)
                            {
                                Console.WriteLine("재고가 부족합니다.                         ");
                            }
                            else
                            {
                                Console.WriteLine("구매가 완료되었습니다.                     ");
                                Player.instance.AddItem(Shop.instance.GetItem(curPageShop*nItemsOnPage + itemNo-1));
                                Shop.instance.SellItem(curPageShop*nItemsOnPage + itemNo-1);
                                Console.SetCursorPosition(10, 10 + itemNo -1);
                                Console.WriteLine(Shop.instance.GetItem(curPageShop* nItemsOnPage + itemNo -1).GetName()+ " ({0})  ",Shop.instance.GetStock(nItemsOnPage*curPageShop+itemNo -1));
                                Console.SetCursorPosition(7, 7);
                                Console.WriteLine("{0,5} G", Player.instance.GetStatusInt(Player.Status.GOLD));
                            }
                            Console.SetCursorPosition(8, 23);
                            itemNo = Program.Choice(0, Shop.instance.GetNItem());
                        }
                        for(int a=0; a<(curPageShop==maxPageShop?Shop.instance.GetNItem()%nItemsOnPage:nItemsOnPage); a++)
                        {
                            Console.SetCursorPosition(7,10+ a); Console.WriteLine("- ");
                        }
                        Console.SetCursorPosition(5, 18); Console.WriteLine("{0,50}","");
                        Console.SetCursorPosition(5, 19); Console.WriteLine("1. 아이템 구매                                         ");
                    }

                    Console.SetCursorPosition(5, 22); Console.WriteLine("원하시는 행동을 입력해주세요                             ");
                    Console.SetCursorPosition(5, 23); Console.Write(">>  ");
                    Console.SetCursorPosition(8, 23);
                    break;
                case SceneType.Dungeon:
                    Console.SetCursorPosition(5, 3);
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine("[던전]");
                    Console.ResetColor();
                    DungeonManager dungeonManager = DungeonManager.Instance;
                    dungeonManager.DungeonPhase();
                    //Console.SetCursorPosition(5, 4); Console.WriteLine("몬스터를 처치하여 경험치와 재화를 얻을 수 있습니다.");
                    //Console.SetCursorPosition(5, 5); Console.WriteLine("전투 시스템 미구현");
                    //Console.SetCursorPosition(7, 7);
                    //Console.WriteLine("+ {0} 경험치", 5*Player.instance.GetStatusInt(Player.Status.LVL));
                    //Player.instance.AddExp(5*Player.instance.GetStatusInt(Player.Status.LVL));
                    //Console.SetCursorPosition(7, 8);
                    //Console.WriteLine("+ {0} G",2*Player.instance.GetStatusInt(Player.Status.LVL));
                    //Player.instance.AddGold(2*Player.instance.GetStatusInt(Player.Status.LVL));
                    //Console.SetCursorPosition(7, 9);
                    //int damage = 10 *Player.instance.GetStatusInt(Player.Status.LVL)
                    //    - Player.instance.GetStatusInt(Player.Status.DEF) - Player.instance.GetStatusInt(Player.Status.BDEF) <= 0 ? 0 :
                    //    10 *Player.instance.GetStatusInt(Player.Status.LVL)- Player.instance.GetStatusInt(Player.Status.DEF) - Player.instance.GetStatusInt(Player.Status.BDEF);
                    //Console.WriteLine("- {0} HP", damage);
                    //Player.instance.Damage(damage);
                    Console.SetCursorPosition(5, 19); Console.WriteLine("0. 나가기");
                    Console.SetCursorPosition(5, 23); Console.Write(">>  ");
                    Console.SetCursorPosition(8, 23);
                    break;
                case SceneType.Quest:
                    Console.SetCursorPosition(5, 3);
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine("[퀘스트 보드]");
                    Console.ResetColor();

                    Console.SetCursorPosition(5, 4); Console.WriteLine("퀘스트 정보를 확인할 수 있습니다.");
                    Console.SetCursorPosition(7, 6); Console.WriteLine("[퀘스트 목록]");
                    maxPageQuest = Program.quest_list.Count / nQuestsOnPage;
                    curPageQuest += (opt == 8 ? -1 : opt == 9 ? 1 : 0);
                    if(curPageQuest < 0)
                        curPageQuest = 0;
                    if(curPageQuest > maxPageQuest)
                        curPageQuest = maxPageQuest;
                    Console.SetCursorPosition(65, 6); Console.WriteLine("{0} p / {1} p (8)< | >(9)", curPageQuest, maxPageQuest);
                    for(int a=0; a<(curPageQuest==maxPageQuest?Program.quest_list.Count%nQuestsOnPage:nQuestsOnPage); a++)
                    {
                        Quest quest = Program.quest_list[curPageQuest* nQuestsOnPage + a];
                        int progress = Player.instance.GetQuestStatusViaQID(quest.GetDataInt(QuestIdx.ID)).Item1;
                        Console.SetCursorPosition(7, 7 + 2*(a));
                        if (opt == 0)
                            Console.WriteLine(progress == 1 ? "- [진] " :progress==2?"- [완] ": "-     ");
                        else if (opt == 1)
                            Console.WriteLine(progress == 1 ? "{0}. [진] " :progress==2?"{1}. [완] ": "{2}.    ",a+1,a+1,a+1);
                        else
                            Console.WriteLine(progress == 1 ? "- [진] " :progress==2?"- [완] ": "-     ");
                        string str_q_type = "";
                        string targetName = "";
                        switch((QuestType)quest.GetDataInt(QuestIdx.Type))
                        {
                            case QuestType.Hunt:
                                str_q_type = "사냥";
                                targetName = "    ";
                                break;
                            case QuestType.Collect:
                                str_q_type = "수집";
                                foreach (var item in Program.item_list)
                                {
                                    if (item.GetID() == quest.GetDataInt(QuestIdx.TargetID))
                                    {
                                        targetName = item.GetName();
                                        break;
                                    }
                                }
                                break;
                            case QuestType.Support:
                                str_q_type = "지원";
                                targetName = "    ";
                                break;
                            default:
                                str_q_type = "기타";
                                targetName = "    ";
                                break;

                        }
                        Console.SetCursorPosition(15, 7 + 2*a); Console.WriteLine(str_q_type + " |");
                        Console.SetCursorPosition(21, 7 + 2*a); Console.WriteLine(quest.GetDataStr(QuestIdx.Description));
                        Console.SetCursorPosition(16, 8 + 2*a);
                        (int, int, int, int) quest_info = Player.instance.GetQuestStatusViaQID(quest.GetDataInt(QuestIdx.ID));
                        if (quest_info.Item1 != 0)
                            Console.WriteLine(targetName + " " + quest_info.Item3 + " / " + quest_info.Item4);
                        else
                            Console.WriteLine("");
                        string rwd_item_name = "";
                        Console.SetCursorPosition(50, 8 + 2*a);
                        foreach(var item in Program.item_list)
                        {
                            if (item.GetID() == quest.GetDataInt(QuestIdx.RewardItemID))
                            {
                                rwd_item_name = item.GetName();
                                Console.WriteLine("| 보상: {0}",rwd_item_name);
                                break;
                            }
                        }
                        Console.SetCursorPosition(21, 8 + 2*a); Console.WriteLine("{0}",quest.GetDataStr(QuestIdx.Condition));
                        Console.SetCursorPosition(17, 8 + 2*a); Console.WriteLine("조건: ");
                        Console.SetCursorPosition(32, 8 + 2*a); Console.WriteLine("|");
                        if(quest.GetDataInt(QuestIdx.Type) == (int)QuestType.Hunt)
                        {
                            string enemyName = "Mob1";
                            // Program.enemy_list를 채운 후 주석 해제할것 
//                            foreach (var enemy in Program.enemy_list)
//                            {
//                                if (enemy.ID == quest.GetDataInt(QuestIdx.TargetID))
//                                {
//                                    enemyName = enemy.Name;
                                    int goalNum = quest.GetDataInt(QuestIdx.GoalNum);
                                    Console.SetCursorPosition(35, 8 + 2 * a); Console.WriteLine("{0} {1}마리", enemyName,goalNum);
//                                    break;
//                                }
//                            }
                        }
                        else if (quest.GetDataInt(QuestIdx.Type) == (int)QuestType.Collect)
                        {
                            string itemName;
                            foreach (var item in Program.item_list)
                            {
                                if (item.GetID() == quest.GetDataInt(QuestIdx.TargetID))
                                {
                                    itemName = item.GetName().ToString();
                                    int goalNum = quest.GetDataInt(QuestIdx.GoalNum);
                                    Console.SetCursorPosition(35, 8 + 2 * a); Console.WriteLine("{0} {1}개", itemName, goalNum);
                                    break;
                                }
                            }
                        }
                        Console.SetCursorPosition(70, 8 + 2*a); Console.WriteLine("Gold: +{0}",quest.GetDataInt(QuestIdx.RewardGold));
                        Console.SetCursorPosition(80, 8 + 2*a); Console.WriteLine("Exp: +{0}",quest.GetDataInt(QuestIdx.RewardExp));

                    }
                    Console.SetCursorPosition(5, 19); Console.WriteLine("1. 퀘스트관리");
                    Console.SetCursorPosition(5, 20); Console.WriteLine("{0,-70}","0. 나가기");
                    Console.SetCursorPosition(5, 21); Console.WriteLine(" ");

                    Console.SetCursorPosition(5, 22); Console.WriteLine("원하시는 행동을 입력해주세요");
                    Console.SetCursorPosition(5, 23); Console.Write(">>  ");
                    Console.SetCursorPosition(8, 23);

                    if(opt == 1)
                    {
                        Console.SetCursorPosition(65, 7); Console.WriteLine("{0,23}","");
                        Console.SetCursorPosition(5, 18); Console.WriteLine("수락 / 완료 하고싶은 퀘스트의 번호를 입력하세요");
                        Console.SetCursorPosition(5, 19); Console.WriteLine("{0,15}","");
                        Console.SetCursorPosition(5, 20); Console.WriteLine("0. 나가기");
                        Console.SetCursorPosition(5, 23); Console.Write(">>     ");
                        Console.SetCursorPosition(8, 23);
                        int questNo = Program.Choice(0,curPageQuest==maxPageQuest?Program.quest_list.Count%nQuestsOnPage:nQuestsOnPage);
                        while (questNo != 0 && questNo <=nQuestsOnPage)
                        {
                            if (questNo < 0 || questNo > (curPageQuest==maxPageQuest?Program.quest_list.Count%nQuestsOnPage:nQuestsOnPage))
                            {
                                Console.SetCursorPosition(5, 18); Console.WriteLine("잘못된 입력입니다. 수락 / 완료 하고싶은 퀘스트의 번호를 입력하세요");
                            }
                            else if (Player.instance.AcceptQuest(nQuestsOnPage*curPageQuest + questNo -1))
                            {
                                Console.SetCursorPosition(5, 18); Console.WriteLine("퀘스트를 수락했습니다.                                                     ");
                                Console.SetCursorPosition(5, 19); Console.WriteLine("수락 / 완료 하고싶은 퀘스트의 번호를 입력하세요");
                                Console.SetCursorPosition(7, 7 + 2*(questNo-1));
                                Console.WriteLine("{0}. [진] ", questNo);
                            }
                            else if (Player.instance.CompleteQuest(nQuestsOnPage*curPageQuest + questNo -1))
                            {
                                Console.SetCursorPosition(5, 18); Console.WriteLine("퀘스트를 완료했습니다.                                                       ");
                                Console.SetCursorPosition(5, 19); Console.WriteLine("수락 / 완료 하고싶은 퀘스트의 번호를 입력하세요");
                                Console.SetCursorPosition(7, 7 + 2*(questNo-1));
                                Console.WriteLine("{0}. [완] ", questNo);
                            }
                            else
                            {
                                Console.SetCursorPosition(5, 18); Console.WriteLine("퀘스트를 받기엔 플레이어의 스테이터스가 부족합니다.                          ");
                                Console.SetCursorPosition(5, 19); Console.WriteLine("수락 / 완료 하고싶은 아이템의 번호를 입력하세요");
                            }
                            if (Player.instance.GetQuestStatusViaQID(nQuestsOnPage* curPageQuest + questNo -1).Item1 == 1 && // in progress
                                     Player.instance.GetQuestStatusViaQID(nQuestsOnPage* curPageQuest + questNo -1).Item4 != 0 && // goalNum is not equal to 0
                                     Player.instance.GetQuestStatusViaQID(nQuestsOnPage* curPageQuest + questNo -1).Item3 /       // when the currNum is not bigger than GoalNum
                                     Player.instance.GetQuestStatusViaQID(nQuestsOnPage* curPageQuest + questNo-1).Item4 <1)
                            {
                                Console.SetCursorPosition(5, 18); Console.WriteLine("{0,-50}","아직 완료하지 못한 퀘스트입니다.            ");
                                Console.SetCursorPosition(5, 19); Console.WriteLine("수락 / 완료 하고싶은 퀘스트의 번호를 입력하세요");
                            }
                            Console.SetCursorPosition(5, 20); Console.WriteLine("0. 나가기");
                            Console.SetCursorPosition(5, 23); Console.Write(">>     ");
                            Console.SetCursorPosition(8, 23);
                            questNo = Program.Choice(0,curPageQuest==maxPageQuest?Program.quest_list.Count%nQuestsOnPage:nQuestsOnPage);
                            Console.SetCursorPosition(8, 23);
                        }
                        for(int a=0; a<(curPageQuest==maxPageQuest?Program.quest_list.Count%nQuestsOnPage:nQuestsOnPage); a++)
                        {
                            Console.SetCursorPosition(7,7+ 2*a);
                            Console.WriteLine("- ");
                        }
                        Console.SetCursorPosition(65, 6); Console.WriteLine("{0} p / {1} p  (8)< | >(9)", curPageQuest, maxPageQuest);
                        Console.SetCursorPosition(5, 18); Console.WriteLine("{0,-70}","");
                        Console.SetCursorPosition(5, 19); Console.WriteLine("{0,-60}","1. 퀘스트 관리");
                        Console.SetCursorPosition(5, 20); Console.WriteLine("0. 나가기                                             ");
                        Console.SetCursorPosition(5, 22); Console.WriteLine("원하시는 행동을 입력해주세요                          ");
                        Console.SetCursorPosition(5, 23); Console.Write(">>  ");
                    }
                    break;
                case SceneType.Rest:
                    bool isDrinkable = false;
                    if(opt != 0)
                    {
                        isDrinkable = Player.instance.UsePotion(opt-1);
                    }
                    Console.SetCursorPosition(5, 3);
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.Write("[휴식 및 강화]");
                    Console.ResetColor();
                    Console.SetCursorPosition(5, 4);  Console.WriteLine("아이템을 사용하여 플레이어의 스텟을 올릴 수 있습니다.");
                    // 포션 보유 수량 및 효과; 체력포션, 공격력포션, 방어력 포션
                    // 현재 플레이어의 스텟 (공,방,체)
                    Console.SetCursorPosition(7, 6); Console.WriteLine("[플레이어 스텟]");
                    Console.SetCursorPosition(7, 7); Console.WriteLine("ATK: {0}",Player.instance.GetStatusInt(Player.Status.ATK));
                    Console.SetCursorPosition(7, 8); Console.WriteLine("DEF: {0}",Player.instance.GetStatusInt(Player.Status.DEF));
                    Console.SetCursorPosition(7, 9); Console.WriteLine("H P: {0} / {1}",Player.instance.GetStatusInt(Player.Status.HP), Player.instance.GetStatusInt(Player.Status.MHP));
                    Console.SetCursorPosition(7, 11); Console.WriteLine("[보유 포션]");
                    Console.SetCursorPosition(7, 12); Console.WriteLine("{0,9}","체력 포션");
                    Console.SetCursorPosition(7, 13); Console.WriteLine("{0,9}","공격 포션");
                    Console.SetCursorPosition(7, 14); Console.WriteLine("{0,9}","방어 포션");
                    for(int a=0; a < 3; a++) // 0: HP, 1: ATK; 2: DEF
                    {
                        int idx = Player.instance.fPotion_idx[a];
                        int count = idx==-1?0:Player.instance.GetEquipStatus(idx);
                        Console.SetCursorPosition(20, 12 + a); Console.WriteLine("({0})",count);
                        if(count >0)
                        switch(a)
                        {
                            case 0:
                                Console.SetCursorPosition(30, 12 + a);
                                Console.WriteLine("HP + {0}", Player.instance.GetItem(idx).GetBHP());
                                break;
                            case 1:
                                Console.SetCursorPosition(30, 12 + a);
                                Console.WriteLine("ATK + {0}", Player.instance.GetItem(idx).GetBAtk());
                                break;
                            case 2:
                                Console.SetCursorPosition(30, 12 + a);
                                Console.WriteLine("DEF + {0}", Player.instance.GetItem(idx).GetBDef());
                                break;
                        }
                    }
                    if(opt !=0)
                    {
                        if (isDrinkable)
                        {
                            Console.SetCursorPosition(5, 18); Console.WriteLine("포션을 사용했습니다.");
                        }
                        else
                        {
                            Console.SetCursorPosition(5, 18); Console.WriteLine("포션이 부족합니다.  ");
                        }
                    }
                    Console.SetCursorPosition(5, 19); Console.WriteLine("1. 체력 포션 사용하기");
                    Console.SetCursorPosition(30, 19); Console.WriteLine("2. 공격 포션 사용하기");
                    Console.SetCursorPosition(55, 19); Console.WriteLine("3. 방어 포션 사용하기");
                    Console.SetCursorPosition(5, 20); Console.WriteLine("0. 나가기");
                    Console.SetCursorPosition(5, 22); Console.WriteLine("원하시는 행동을 입력해주세요");
                    Console.SetCursorPosition(5, 23); Console.Write(">>");
                    Console.SetCursorPosition(8, 23);
                    break;
                case SceneType.End:
                    Console.SetCursorPosition(0, 10); Console.WriteLine("{0}",Program.CenterAlign("저장하시겠습니까?",Console.WindowWidth));
                    Console.SetCursorPosition(7, 15); Console.WriteLine("1. 저장 후 종료");
                    Console.SetCursorPosition(7, 16); Console.WriteLine("2. 그냥 종료");
                    Console.SetCursorPosition(5, 22); Console.WriteLine("원하시는 행동을 입력해주세요 ");
                    Console.SetCursorPosition(5, 23); Console.Write(">>  ");
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
        Jobs,
        Main,
        Status,
        Inventory,
        Shop,
        Dungeon,
        Quest,
        Rest,
        End
    }
}
