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
            //if (!MapLevelTracker.displayed)
            //{
                Map currentLevel = MapLevelTracker.GetMapLevel(0);
                DisplayWorldLevel(currentLevel);//add map level position variable for player if you need to support multiple levels
            List<Distance> visibleToPlayer = RayCaster.ExperimentalFind(Player.GetPlayer().GetSightRadius(), Player.GetPlayer().PosX, Player.GetPlayer().PosY);
            DisplayStructures(visibleToPlayer);
            DisplayFOV(visibleToPlayer);
            MapLevelTracker.displayed=true;
            //}
            HighlightLoot(visibleToPlayer);
            DisplayWorldEntities(visibleToPlayer);
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
                Console.Write(new String(' ', GameVariables.WindowWidth - GameVariables.MapDisplayWidth));
                Console.BackgroundColor = ConsoleColor.DarkGray;
                for (int i = xoffset; i < xoffset + xlimit; i++)
                {
                    test.Append(map.GetTileAtLocation(i, j).GetTileDetails().mapTag);
                }
                Console.WriteLine(test.ToString());
                test.Clear();
            }
            
            Console.BackgroundColor = ConsoleColor.Black;

        }
        public static void DisplayStructures(List<Distance> visibleToPlayer)
        {
            Map currentmap = MapLevelTracker.GetMapLevel(0);
            int xoffset = Player.GetPlayer().PosX - GameVariables.MapDisplayWidth;
            if (xoffset < 0)
                xoffset = 0;
            else if (xoffset > currentmap.SizeX - GameVariables.MapDisplayWidth)
                xoffset = currentmap.SizeX - GameVariables.MapDisplayWidth;
            int yoffset = Player.GetPlayer().PosY - GameVariables.MapDisplayHeight;
            if (yoffset < 0)
                yoffset = 0;
            else if (yoffset > currentmap.SizeY - GameVariables.MapDisplayHeight)
                yoffset = currentmap.SizeY - GameVariables.MapDisplayHeight;
            foreach (Structure structure in MapLevelTracker.GetStructureTracker().GetStructures())
            {
                int counter = 0;
                foreach(Distance location in structure.componentLocation)
                {
                    foreach(Distance playerVision in visibleToPlayer)
                    {
                        if(location.X==playerVision.X&& location.Y == playerVision.Y)
                        {
                            Console.SetCursorPosition(GameVariables.WindowWidth - GameVariables.MapDisplayWidth + xoffset + location.X, yoffset + location.Y);
                            Console.Write(structure.designComponents[counter].GetTileDetails().mapTag);
                        }

                    }
                    counter++;
                }
            }
        }
        public static void DisplayWorldEntities(List<Distance> visibleToPlayer)
        {
            Player player = Player.GetPlayer();
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(GameVariables.WindowWidth - GameVariables.MapDisplayWidth+player.PosX, player.PosY);
            Console.Write("P");
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(player.ShowStatus());
            foreach (LiveTarget npc in MapLevelTracker.GetNPCTracker().GetNPCS())
            {
                foreach(Distance location in visibleToPlayer)
                {
                    if (npc.PosX == location.X && npc.PosY == location.Y)
                    {
                        Console.SetCursorPosition(GameVariables.WindowWidth - GameVariables.MapDisplayWidth + npc.PosX, npc.PosY);
                        Console.Write("N");
                        break;
                    }
                }
                
            }
        }
        public static void HighlightLoot(List<Distance> visibleToPlayer)
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
            int xlimit = xoffset + GameVariables.MapDisplayWidth - 1;
            if (xlimit > world.SizeX)
                xlimit = world.SizeX;
            int ylimit = yoffset + GameVariables.MapDisplayHeight - 1;
            if (ylimit > world.SizeY)
                ylimit = world.SizeY;
            try
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                foreach (Distance location in visibleToPlayer)
                {
                    if (world.GetTileAtLocation(location.X, location.Y).ReturnContents().Count != 0)
                    {
                        Console.SetCursorPosition(GameVariables.WindowWidth - GameVariables.MapDisplayWidth+location.X, location.Y);
                        Console.Write(world.GetTileAtLocation(location.X, location.Y).GetTileDetails().mapTag);
                    }
                    //DisplayDebugMessage(i.ToString());
                }
            }
            catch (IndexOutOfRangeException)
            {
                DisplayDebugMessage("Netko je opet sjebo mapu");
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
        public static void DisplayStructureOptions(List<Tile> components, int index)
        {
            int counter = 0;
            Console.SetCursorPosition(0, GameVariables.WindowHeight/2);
            foreach(Tile component in components)
            {
                if (component.GetTileDetails().Interactable)
                {
                    if (counter == index)
                        Console.BackgroundColor=ConsoleColor.Blue;
                    DisplayMessage(component.GetTileDetails().Name);
                    Console.BackgroundColor = ConsoleColor.Black;
                    counter++;
                }
            }
        }
        public static void CloakNPCPosition()
        {
            foreach (LiveTarget npc in MapLevelTracker.GetNPCTracker().GetNPCS())
            {
                Console.SetCursorPosition(GameVariables.WindowWidth - GameVariables.MapDisplayWidth+npc.PosX, npc.PosY);
                Structure temp = MapLevelTracker.GetStructureTracker().FindStructureWithComponentCoordinates(npc.PosX, npc.PosY);
                if (temp == null)
                    Console.Write(MapLevelTracker.GetMapLevel(0).GetTileAtLocation(npc.PosX, npc.PosY).GetTileDetails().mapTag);
                else
                    Console.Write(temp.designComponents[temp.ReturnIndexOfComponentAtLocation(npc.PosX, npc.PosY)].GetTileDetails().mapTag);
            }
        }
        public static void CloakPlayerPosition()
        {
            Player player = Player.GetPlayer();
            Console.SetCursorPosition(GameVariables.WindowWidth - GameVariables.MapDisplayWidth + player.PosX, player.PosY);
            Structure temp = MapLevelTracker.GetStructureTracker().FindStructureWithComponentCoordinates(player.PosX,player.PosY);
            if (temp == null)
                Console.Write(MapLevelTracker.GetMapLevel(0).GetTileAtLocation(player.PosX, player.PosY).GetTileDetails().mapTag);
            else
                Console.Write(temp.designComponents[temp.ReturnIndexOfComponentAtLocation(player.PosX, player.PosY)].GetTileDetails().mapTag);
        }
        public static void DisplayFOV(List<Distance> visibleTiles)
        {
            //Console.Clear();
            foreach(Distance location in visibleTiles)
            {
                Console.SetCursorPosition(GameVariables.WindowWidth-GameVariables.MapDisplayWidth+ location.X, location.Y);
                Structure tempStruct = MapLevelTracker.GetStructureTracker().FindStructureWithComponentCoordinates(location.X, location.Y);
                if (tempStruct != null)
                {
                    Console.Write(tempStruct.designComponents[tempStruct.ReturnIndexOfComponentAtLocation(location.X, location.Y)].GetTileDetails().mapTag);
                }
                else
                    Console.Write(MapLevelTracker.GetMapLevel(0).GetTileAtLocation(location.X, location.Y).GetTileDetails().mapTag);
            }
        }
    }
}
