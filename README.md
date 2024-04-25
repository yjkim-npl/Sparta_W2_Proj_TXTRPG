# Week 2 project - Matryoshka
 - 마트료시카 게임은 스파르타 부트캠프에서 2주차 개인 과제로 만들어진 게임입니다. 

 ## 설명
 - 캐릭터가 장비를 착용할 때 장비의 크기 순서대로 착용하고 마치 그 모습이 러시아의 전통 인형 마트료시카와 유사하게 보여지기 때문에 마트료시카라는 제목을 가지게 되었습니다.
 - 플레이 방식은 콘솔에 입력하는 숫자로 행동을 선택할 수 있으며 던전에서 재화를 얻은 뒤 플레이어의 캐릭터를 강화시키는 것이 주 목표가 되겠습니다.

 ### 요구사항
 - Visual Studio 2022

 ## 시작 화면
 ![StartScene](https://github.com/yjkim-npl/Sparta_W2_Proj_TXTRPG/assets/69491656/2ec9252c-46bd-4072-b2cc-bdd8af5e873b)
 - 캐릭터의 이름을 설정할 수 있습니다.

 ## 메인 화면
 ![MainScene](https://github.com/yjkim-npl/Sparta_W2_Proj_TXTRPG/assets/69491656/3b2261c3-e30a-445f-a222-e3436815116e)

 - 시작화면에서 캐릭터의 이름을 설정한 뒤 보게되는 화면으로, 선택지를 통해서 행동을 결정할 수 있습니다.

 ## 캐릭터 상태창
![Status_default](https://github.com/yjkim-npl/Sparta_W2_Proj_TXTRPG/assets/69491656/9ebfbb8d-acf2-43c1-ad75-b5e5f8328fda)
 - 현재 캐릭터의 상세정보를 확인 할 수 있습니다.
 - 레벨에 따른 스탯은 기본값에 들어가며 착용한 장비에 따라 추가되는 스탯은 `+` 이후 값에 적용됩니다.

 ## 인벤토리
![Inventory_Equip](https://github.com/yjkim-npl/Sparta_W2_Proj_TXTRPG/assets/69491656/d44f23c0-f3c1-444e-91f7-635f014f087b)
 - 캐릭터가 소유한(구매 혹은 드랍으로 획득한) 장비를 확인할 수 있습니다.
 - 행동 명령에 따라서 인벤토리를 확인하는 모드와 장비를 착용/해제하는 모드로 전환할 수 있습니다.

 ## 상점
 ![Shop_buy](https://github.com/yjkim-npl/Sparta_W2_Proj_TXTRPG/assets/69491656/118cee14-505e-40f1-b360-e36da9f921d2)
 - 캐릭터가 소지한 골드를 소모하여 장비를 구매할 수 있습니다.
 - 소지금이 불충분하거나 상점의 재고가 소진하게된다면 구매를 못하니 주의하시기 바랍니다.

 ## 던전
 ![Dungeon](https://github.com/yjkim-npl/Sparta_W2_Proj_TXTRPG/assets/69491656/7113d937-f724-40d9-bb8e-a9e6f55e0aed)
 - 전투 시스템은 현재까지 미구현된 기능이며, 던전에 입장할 시 레벨을 기준으로 던전 난이도가 상승, 방어력을 기준으로 받는 데미지가 바뀌며 보상이 더 좋아집니다.

