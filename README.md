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
![MainScene](https://github.com/yjkim-npl/Sparta_W2_Proj_TXTRPG/assets/69491656/ca069ab5-52a1-4e35-8271-459ebc27df83)

 - 시작화면에서 캐릭터의 이름을 설정한 뒤 보게되는 화면으로, 선택지를 통해서 행동을 결정할 수 있습니다.

 ## 캐릭터 상태창
![Status](https://github.com/yjkim-npl/Sparta_W2_Proj_TXTRPG/assets/69491656/2ce7b58b-5f5a-4606-9b0a-6259bfe2840c)

 - 현재 캐릭터의 상세정보를 확인 할 수 있습니다.
 - 레벨에 따른 스탯은 기본값에 들어가며 착용한 장비에 따라 추가되는 스탯은 `+` 이후 값에 적용됩니다.

 ## 인벤토리
![Inventory](https://github.com/yjkim-npl/Sparta_W2_Proj_TXTRPG/assets/69491656/87eb83ee-b1d2-4022-bb66-ee7ee1199872)
 - 캐릭터가 소유한(구매 혹은 드랍으로 획득한) 장비를 확인할 수 있습니다.
 - 행동 명령에 따라서 인벤토리를 확인하는 모드와 장비를 착용/해제하는 모드로 전환할 수 있습니다.

 ## 상점
![Shop](https://github.com/yjkim-npl/Sparta_W2_Proj_TXTRPG/assets/69491656/d77dc3b9-cb1e-4a0a-a0b3-ada35a9a63f1)

 - 캐릭터가 소지한 골드를 소모하여 장비를 구매할 수 있습니다.
 - 소지금이 불충분하거나 상점의 재고가 소진하게된다면 구매를 못하니 주의하시기 바랍니다.

 ## 던전
![Battle](https://github.com/yjkim-npl/Sparta_W2_Proj_TXTRPG/assets/69491656/2e024629-25e3-431f-afc3-b3bccbdeaf26)

 - 플레이어의 스테이터스와 스킬에 따라 데미지가 바뀝니다. 
 - 전투에서 승리시 경험치와 골드를 얻을 수 있습니다.
 - 전투에서 사망시 사망 패널티를 받게 됩니다.

## 휴식 및 강화
![Rest](https://github.com/yjkim-npl/Sparta_W2_Proj_TXTRPG/assets/69491656/a3d92521-1905-42ee-8b72-8b8e97a759e7)

- 보유한 포션을 사용하여 플레이어의 공격력, 방어력, 체력을 향상시킬 수 있습니다.
- 포션은 상점 및 퀘스트로 획득이 가능합니다.

## 퀘스트 보드
![Quest](https://github.com/yjkim-npl/Sparta_W2_Proj_TXTRPG/assets/69491656/fd7df4f3-7fb8-4ac5-882c-883aa85f1fb2)

- 현재 플레이어의 정보에 따라서 받을 수 있는 퀘스트가 다릅니다.
- 조건을 달성시 완료가 가능하고, 완료시 보상을 얻을 수 있습니다.
