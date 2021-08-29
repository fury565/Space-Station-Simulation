using System;
using System.Collections.Generic;
using System.Text;

namespace JustSomeRandomRPGMechanics
{
    static class TurnHandler
    {
        static public void NextTurn()
        {
            PlayerMetabolism();
            NPCTurn();
        }
        static public void PlayerMetabolism()
        {
            Player.GetPlayer().Metabolism();
        }
        static public void NPCTurn()
        {
            Display.CloakNPCPosition();
            MapLevelTracker.GetNPCTracker().SearchSurroundings();
            MapLevelTracker.GetNPCTracker().MoveAllNPCS();
        }
        static public void PerishFood()
        {
            Display.DisplayMessage("Just put your food in space");
        }
    }
}
