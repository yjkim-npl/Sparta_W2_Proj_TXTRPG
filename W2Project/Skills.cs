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
        static int skillDamage = 0;

        public class WarriorSkills
        {
            public void AlphaStrike(Enemy enemy)
            {
                skillDamage = (int)((Player.instance.GetStatusInt(Status.ATK) + Player.instance.GetStatusInt(Status.BATK))* 1.5);
                enemy.Damage(skillDamage);
//                Console.WriteLine("알파 스트라이크를 사용해서 {enemy.Name}에게 {alphaDamage}만큼의 데미지를 입혔습니다.");
            }
        }

        public class ArcherSkills
        {
            public void DualShot(Enemy enemy)
            {
                skillDamage = (int)((Player.instance.GetStatusInt(Status.ATK) + Player.instance.GetStatusInt(Status.BATK))* 1.5);
                enemy.Damage(skillDamage);
                Console.WriteLine("듀얼 샷을 사용해서 {enemy.Name}에게 {alphaDamage}만큼의 데미지를 입혔습니다.");
            }
        }

        public class ChiefSkills
        {
            public void VitalStrike(Enemy enemy)
            {
                skillDamage = (int)((Player.instance.GetStatusInt(Status.ATK) + Player.instance.GetStatusInt(Status.BATK))* 1.5);
                enemy.Damage(skillDamage);
                Console.WriteLine("바이탈 스트라이크를 사용해서 {enemy.Name}에게 {alphaDamage}만큼의 데미지를 입혔습니다.");
            }
        }
    }
}
