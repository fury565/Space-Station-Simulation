using System;
using System.Collections.Generic;
using System.Text;

namespace JustSomeRandomRPGMechanics
{
    static class KeyAction
    {
        public static void CheckPressedKey(ConsoleKeyInfo pressed)
        {
            if (pressed.Key == ConsoleKey.I)
            {
                Console.Clear();
                Display.DisplayMessage(Player.GetPlayer().GetInventory().GetContents());
            }
            else if (pressed.Key == ConsoleKey.C)
            {
                Console.Clear();
                Display.DisplayMessage(Player.GetPlayer().ShowStatus());
            }
            else if (pressed.Key == ConsoleKey.U)
            {
                Console.Clear();
                Player.GetPlayer().GetInventory().SelectItem();
            }
            else if (pressed.Key == ConsoleKey.R)
            {
                
            }
            else if (pressed.Key == ConsoleKey.H)
            {
                Console.Clear();
                Display.DisplayMessage("i-inventory c-character stats z-search h-help");
            }
            else if (pressed.Key == ConsoleKey.LeftArrow)
            {
                Player player = Player.GetPlayer();
                Console.SetCursorPosition(GameVariables.WindowWidth - GameVariables.MapDisplayWidth+player.PosX, player.PosY);
                Console.Write(MapLevelTracker.GetMapLevel(0).GetTileAtLocation(player.PosX, player.PosY).GetTileDetails().mapTag);
                Player.GetPlayer().Move(-1, 0);
            }
            else if (pressed.Key == ConsoleKey.RightArrow)
            {
                Player player = Player.GetPlayer();
                Console.SetCursorPosition(GameVariables.WindowWidth - GameVariables.MapDisplayWidth+player.PosX, player.PosY);
                Console.Write(MapLevelTracker.GetMapLevel(0).GetTileAtLocation(player.PosX, player.PosY).GetTileDetails().mapTag);
                Player.GetPlayer().Move(1, 0);
            }
            else if (pressed.Key == ConsoleKey.UpArrow)
            {
                Player player = Player.GetPlayer();
                Console.SetCursorPosition(GameVariables.WindowWidth - GameVariables.MapDisplayWidth+player.PosX, player.PosY);
                Console.Write(MapLevelTracker.GetMapLevel(0).GetTileAtLocation(player.PosX, player.PosY).GetTileDetails().mapTag);
                Player.GetPlayer().Move(0,-1);
            }
            else if (pressed.Key == ConsoleKey.DownArrow)
            {
                Player player = Player.GetPlayer();
                Console.SetCursorPosition(GameVariables.WindowWidth - GameVariables.MapDisplayWidth+player.PosX,player.PosY);
                Console.Write(MapLevelTracker.GetMapLevel(0).GetTileAtLocation(player.PosX, player.PosY).GetTileDetails().mapTag);
                Player.GetPlayer().Move(0,1);
            }
            else if (pressed.Key == ConsoleKey.G)
            {
                Player player = Player.GetPlayer();
                Console.SetCursorPosition(player.PosX, player.PosY);
                List<IEntity> tileContents= MapLevelTracker.GetMapLevel(0).GetTileAtLocation(player.PosX, player.PosY).ReturnContents();
                Display.DisplayTileContent(tileContents);
                bool[] selected=Player.GetPlayer().ChooseItems(tileContents);
                MapLevelTracker.GetMapLevel(0).GetTileAtLocation(player.PosX, player.PosY).RemoveSelected(selected);
            }
            else if (pressed.Key == ConsoleKey.E) {
                ConsoleKey key = Console.ReadKey().Key;
                if (key == ConsoleKey.NumPad1)
                {
                    Player.GetPlayer().Examine(-1, 1);
                }
                else if (key == ConsoleKey.NumPad2)
                {
                    Player.GetPlayer().Examine(0, 1);
                }
                else if (key == ConsoleKey.NumPad3)
                {
                    Player.GetPlayer().Examine(1, 1);
                }
                else if (key == ConsoleKey.NumPad4)
                {
                    Player.GetPlayer().Examine(-1, 0);
                }
                else if (key == ConsoleKey.NumPad5)
                {
                    Player.GetPlayer().Examine(0, 0);
                }
                else if (key == ConsoleKey.NumPad6)
                {
                    Player.GetPlayer().Examine(1, 0);
                }
                else if (key == ConsoleKey.NumPad7)
                {
                    Player.GetPlayer().Examine(-1, -1);
                }
                else if (key == ConsoleKey.NumPad8)
                {
                    Player.GetPlayer().Examine(0, -1);
                }
                else if (key == ConsoleKey.NumPad9)
                {
                    Player.GetPlayer().Examine(1, -1);
                }
            }
            else if (pressed.Key == ConsoleKey.Subtract)
            {
                Console.Clear();
                Display.DisplayMessage(BaseNonTargettableEntityCollection.GetAllItems());
            }
            else if (pressed.Key == ConsoleKey.Add)
            {
                MapLevelTracker.GetNPCTracker().AddNPC(new LiveTarget(Bestiary.GetCreature(2000)));
                foreach(var idiot in MapLevelTracker.GetNPCTracker().GetNPCS())
                {
                    idiot.GetInventory().Pickup(NPCCustomizer.GenerateLoot());
                }
                
            }
            else if (pressed.Key == ConsoleKey.Z)
            {
                Console.Clear();
                Display.WorldData(MapLevelTracker.GetMapLevel(0));
            }
            else if (pressed.Key == ConsoleKey.Divide)
            {
                GameVariables.UseDebug= !GameVariables.UseDebug;
            }
        }
        
        public static void CheckGameOverMenuKey(ConsoleKeyInfo pressed)
        {
            if (pressed.Key == ConsoleKey.L)
            {
                //Not yet Implemented
            }
            else if (pressed.Key == ConsoleKey.Q)
            {
                System.Environment.Exit(0);
            }
        }
    }
}
