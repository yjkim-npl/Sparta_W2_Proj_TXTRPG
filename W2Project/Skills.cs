using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static W2Project.Player;
using static W2Project.Battle;

namespace W2Project
{
    internal class Skills
    {
        Player player = Player.instance;
        Battle battle = new Battle();

        public class WarriorSkills
        {
            public void AlphaStrike(Enemy enemy)
            {
                int alphaDamage = (int)(Player.instance.GetStatusInt(Status.ATK) * 1.5);
                enemy.Damage(alphaDamage);
                Console.WriteLine("알파 스트라이크를 사용해서 {enemy.Name}에게 {alphaDamage}만큼의 데미지를 입혔습니다.");
            }
        }
    }
}
