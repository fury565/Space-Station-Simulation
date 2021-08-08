using System;
using System.Collections.Generic;
using System.Text;

namespace JustSomeRandomRPGMechanics
{
    static class Display
    {
        public static void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }
        public static void DisplayDebugMessage(string message)
        {
            DebugMessageHolder.AddMessage(message);
            if (GameVariables.UseDebug)
            {
                Console.SetCursorPosition(0, GameVariables.MapDisplayHeight);
                string[] messages = DebugMessageHolder.GetMessageHistory();
                for(int i=DebugMessageHolder.GetLastMessageIndex();i>=0;i--)
                    DisplayMessage(messages[i]);
            }
        }
        public static void DisplayLoot(List<IEntity> loot)
        {
            StringBuilder builder = new StringBuilder();
            foreach(IEntity item in loot)
            {
                builder.AppendLine(item.GetName()+" x"+item.GetCount());
            }
            Console.WriteLine(builder);
        }

        public static void DisplayWorldAroundPlayer()
        {
            if (!MapLevelTracker.displayed)
            {
                Map currentLevel = MapLevelTracker.GetMapLevel(0);
                DisplayWorldLevel(currentLevel);//add map level position variable for player if you need to support multiple levels
                MapLevelTracker.displayed=true;
            }
            Player player = Player.GetPlayer();
            Console.SetCursorPosition(player.PosX, player.PosY);
            Console.Write("P");
            foreach (LiveTarget npc in MapLevelTracker.GetNPCTracker().GetNPCS())
            {
                Console.SetCursorPosition(npc.PosX, npc.PosY);
                Console.Write("N");
            }

        }
        public static void DisplayWorldLevel(Map map)
        {
            StringBuilder test = new StringBuilder(GameVariables.MapDisplayWidth);
            Map currentmap = MapLevelTracker.GetMapLevel(0);
            Console.SetCursorPosition(0, 0);
            int xoffset = Player.GetPlayer().PosX-GameVariables.MapDisplayWidth;
            if (xoffset < 0)
                xoffset = 0;
            else if (xoffset > currentmap.SizeX - GameVariables.MapDisplayWidth)
                xoffset = currentmap.SizeX - GameVariables.MapDisplayWidth;
            int yoffset = Player.GetPlayer().PosY - GameVariables.MapDisplayHeight;
            if (yoffset < 0)
                yoffset = 0;
            else if (yoffset > currentmap.SizeY - GameVariables.MapDisplayHeight)
                yoffset = currentmap.SizeY - GameVariables.MapDisplayHeight;
            int xlimit = 0;
            if (currentmap.SizeX > GameVariables.MapDisplayWidth)
                xlimit = GameVariables.MapDisplayWidth;
            else
                xlimit = currentmap.SizeX;
            int ylimit = 0;
            if (currentmap.SizeY > GameVariables.MapDisplayHeight)
                ylimit = GameVariables.MapDisplayHeight;
            else
                ylimit = currentmap.SizeY;
            for (int j = yoffset; j < yoffset+ylimit; j++)
            {
                test.Clear();
                for (int i = xoffset; i < xoffset + xlimit; i++)
                {
                    test.Append(map.GetTileAtLocation(i, j).GetTileType());
                }
                Console.WriteLine(test.ToString());
            }
        }
        public static void WorldData(Map map)
        {
            MapLevelTracker.displayed = false;
            Console.SetCursorPosition(0, 0);
            for (int j = 0; j < map.SizeY; j++)
            {
                for (int i = 0; i < map.SizeX; i++)
                {
                    Console.Write(i);
                    Console.Write(j);
                    Console.Write(map.GetTileAtLocation(i, j).GetTileType());
                }
                    
                Console.WriteLine();
            }
        }
        public static void CloakNPCPosition()
        {
            foreach (LiveTarget npc in MapLevelTracker.GetNPCTracker().GetNPCS())
            {
                Console.SetCursorPosition(npc.PosX, npc.PosY);
                Console.Write(MapLevelTracker.GetMapLevel(0).GetTileAtLocation(npc.PosX, npc.PosY).GetTileType());
            }
        }
    }
}
