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
                Console.BackgroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(0, GameVariables.MapDisplayHeight);
                string[] messages = DebugMessageHolder.GetMessageHistory();
                for(int i=DebugMessageHolder.GetLastMessageIndex();i>=0;i--)
                    DisplayMessage(messages[i]);
            }
        }
        public static void DisplayTileContent(List<IEntity> content)
        {
            MapLevelTracker.displayed = false;
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Tile Contents:");
            foreach(IEntity item in content)
            {
                builder.AppendLine(item.GetName()+" x"+item.GetCount());
            }
            Console.WriteLine(builder.ToString());
        }

        public static void DisplayWorldAroundPlayer()
        {
            if (!MapLevelTracker.displayed)
            {
                Map currentLevel = MapLevelTracker.GetMapLevel(0);
                DisplayWorldLevel(currentLevel);//add map level position variable for player if you need to support multiple levels
                MapLevelTracker.displayed=true;
            }
            HighlightLoot();
            DisplayWorldEntities();
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
                Console.BackgroundColor = ConsoleColor.Black;
                test.Append(new String(' ', GameVariables.WindowWidth - GameVariables.MapDisplayWidth));
                for (int i = xoffset; i < xoffset + xlimit; i++)
                {
                    test.Append(map.GetTileAtLocation(i, j).GetTileDetails().mapTag);
                }
                test.AppendLine();
                
            }
            Console.WriteLine(test.ToString());


        }
        public static void DisplayWorldEntities()
        {
            Player player = Player.GetPlayer();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(GameVariables.WindowWidth - GameVariables.MapDisplayWidth+player.PosX, player.PosY);
            Console.Write("P");
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(player.ShowStatus());
            foreach (LiveTarget npc in MapLevelTracker.GetNPCTracker().GetNPCS())
            {
                Console.SetCursorPosition(GameVariables.WindowWidth - GameVariables.MapDisplayWidth+npc.PosX, npc.PosY);
                Console.Write("N");
            }
        }
        public static void HighlightLoot()
        {
            Map world = MapLevelTracker.GetMapLevel(0);
            int xoffset = Player.GetPlayer().PosX - GameVariables.MapDisplayWidth;
            if (xoffset < 0)
                xoffset = 0;
            else if (xoffset > world.SizeX - GameVariables.MapDisplayWidth)
                xoffset = world.SizeX - GameVariables.MapDisplayWidth;
            int yoffset = Player.GetPlayer().PosY - GameVariables.MapDisplayHeight;
            if (yoffset < 0)
                yoffset = 0;
            else if (yoffset > world.SizeY - GameVariables.MapDisplayHeight)
                yoffset = world.SizeY - GameVariables.MapDisplayHeight;
            try
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                for (int i = xoffset; i < xoffset + GameVariables.MapDisplayWidth-1; i++)//-1 probably hardcode
                {
                    for (int j = yoffset; j < yoffset + GameVariables.MapDisplayHeight-1; j++)
                    {
                        if (world.GetTileAtLocation(i, j).ReturnContents().Count != 0)
                        {
                            Console.SetCursorPosition(GameVariables.WindowWidth - GameVariables.MapDisplayWidth+i, j);
                            Console.Write(world.GetTileAtLocation(i, j).GetTileDetails().mapTag);
                        }
                        
                    }
                    //DisplayDebugMessage(i.ToString());
                }
            }
            catch (IndexOutOfRangeException)
            {
                DisplayDebugMessage("De napravi mapu kako spada");
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
                    Console.Write(map.GetTileAtLocation(i, j).GetTileDetails().mapTag);
                }
                    
                Console.WriteLine();
            }
        }
        public static void CloakNPCPosition()
        {
            foreach (LiveTarget npc in MapLevelTracker.GetNPCTracker().GetNPCS())
            {
                Console.SetCursorPosition(GameVariables.WindowWidth - GameVariables.MapDisplayWidth+npc.PosX, npc.PosY);
                Console.Write(MapLevelTracker.GetMapLevel(0).GetTileAtLocation(npc.PosX, npc.PosY).GetTileDetails().mapTag);
            }
        }
    }
}
