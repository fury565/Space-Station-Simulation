using System;
using System.Collections.Generic;
using System.Text;

namespace JustSomeRandomRPGMechanics
{
    class CombatHandler//needs to be properly removed
    {
        public static void StartCombat()
        {
            /*LiveTarget enemy = Bestiary.GetCreature(1);
            bool victory = false;
            StringBuilder actionResult=new StringBuilder();
            while (true)
            {
                Console.Clear();
                Display.DisplayCombatInfo(enemy);
                Display.DisplayMessage(actionResult.ToString());
                actionResult.Clear();
                ConsoleKeyInfo pressed = Console.ReadKey();
                string response = KeyAction.CheckCombatPressedKey(pressed, enemy);
                actionResult.AppendLine(response);
                if (response == "You escaped")
                {
                    break;
                }
                if (enemy.HP > 0)
                {
                    Player.GetPlayer().TakeHit(enemy.Attack());
                    actionResult.AppendLine("Enemy took action");
                }
                else
                {
                    victory = true;
                    break;
                }
                if (Player.GetPlayer().HP <= 0)
                {
                    break;
                }

            }
            if (victory)
            {
                //GenerateLoot based on enemy ID
                List<Loot> loot = NPCCustomizer.GenerateLoot(0);
                Player.GetPlayer().GetInventory().Pickup(loot);
                Player.GetPlayer().IncreaseXP(enemy.Lvl * 3);
                Console.Clear();
                Display.DisplayMessage("You have won");
                Display.DisplayLoot(loot);
                Display.DisplayMessage("Press enter to continue");
                ConsoleKeyInfo pressed = Console.ReadKey();
                Console.Clear();
            }
            else if(Player.GetPlayer().HP <= 0)
            {
                GameOver.DeathInCombat(enemy);
            }*/
        }
    }
}
