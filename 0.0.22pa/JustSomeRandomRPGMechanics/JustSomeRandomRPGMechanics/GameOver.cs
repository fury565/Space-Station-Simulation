using System;
using System.Collections.Generic;
using System.Text;

namespace JustSomeRandomRPGMechanics
{
    static class GameOver
    {
        public static void DeathInCombat(LiveTarget enemy)
        {
            Console.Clear();
            Display.DisplayMessage("You have been slain by " + enemy.GetName());
            Display.DisplayMessage(StoryTeller.GetCombatTextByIndex(1));
            while (true)
            {
                ConsoleKeyInfo input = Console.ReadKey();
                KeyAction.CheckGameOverMenuKey(input);
            }
        }
    }
}
