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
            }

            public void ShieldBash(Enemy enemy)
            {
                int skillDamage = (int)((Player.instance.GetStatusInt(Status.DEF) + Player.instance.GetStatusInt(Status.BDEF)) * 1.0);
                enemy.Damage(skillDamage);
            }

            public void Cleave(Enemy enemy)
            {
                int baseDamage = Player.instance.GetStatusInt(Status.ATK) + Player.instance.GetStatusInt(Status.BATK);
                int cleaveDamage = (int)(baseDamage * 1.5);

                enemy.Damage(cleaveDamage);
            }
        }

        public class ArcherSkills
        {
            public void DualShot(Enemy enemy)
            {
                skillDamage = (int)((Player.instance.GetStatusInt(Status.ATK) + Player.instance.GetStatusInt(Status.BATK))* 1.5);
                enemy.Damage(skillDamage);
            }
            public void MultiShot(List<Enemy> enemies)
            {
                foreach (Enemy enemy in enemies)
                {
                    int skillDamage = (int)(Player.instance.GetStatusInt(Status.ATK) * 1.2);
                    enemy.Damage(skillDamage);
                }
            }
            public void PiercingShot(Enemy enemy)
            {
                int baseDamage = Player.instance.GetStatusInt(Status.BATK);
                int skillDamage = (int)(baseDamage * 2.5);

                enemy.Damage(skillDamage);
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
            public void Backstab(Enemy enemy)
            {
                int baseDamage = (int)((Player.instance.GetStatusInt(Status.ATK) + Player.instance.GetStatusInt(Status.BATK)) * 2.0);
                int criticalDamage = (int)(baseDamage * 1.5);

                // 무작위로 즉사 효과 적용
                Random random = new Random();
                bool instantKill = random.Next(100) < 10; // 10% 확률로 즉사 효과 발동

                if (instantKill)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.SetCursorPosition(25, 11); Console.WriteLine("즉사!");
                    Console.ResetColor();
                    enemy.Damage(enemy.Health); // 적 즉사
                }
                else
                {
                    enemy.Damage(criticalDamage);
                }
            }
            public void ShadowStrike(Enemy enemy)
            {
                int totalDamage = 0;
                for (int i = 0; i < 2; i++)
                {
                    int skillDamage = (int)((Player.instance.GetStatusInt(Status.ATK) + Player.instance.GetStatusInt(Status.BATK)) * 0.9);
                    totalDamage += skillDamage;
                    enemy.Damage(skillDamage);
                }
            }
        }
    }
}
