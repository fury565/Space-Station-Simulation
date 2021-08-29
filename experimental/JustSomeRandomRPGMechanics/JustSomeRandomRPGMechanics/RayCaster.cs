using System;
using System.Collections.Generic;

namespace JustSomeRandomRPGMechanics
{
    static class RayCaster
    {
        static List<Distance> visibleTiles;
        static int lastExploredTile;
        static int perRadiusExploredCounter;
        static int previousRadiusExploredCounter;
        static public List<Distance> FindVisibleTiles(int visionRadius, int startX, int startY)
        {
            perRadiusExploredCounter = 1;
            lastExploredTile = 0;
            previousRadiusExploredCounter = 0;
            visibleTiles = new List<Distance>();
            visibleTiles.Add(new Distance(startX, startY));
            for (int i = 1; i <= visionRadius; i++)
            {
                ExploreFurther(perRadiusExploredCounter);
            }
            return visibleTiles;
        }
        static public List<Distance> ExperimentalFind(int visionRadius, int startX, int startY)
        {
            visibleTiles = new List<Distance>();
            for(int i = -visionRadius; i <= visionRadius; i++)
            {
                for(int j=-visionRadius; j <= visionRadius; j++)
                {
                    if (startX+i >= 0 && startX+i <= MapLevelTracker.GetMapLevel(0).SizeX - 1 && startY + j >= 0 && startY + j <= MapLevelTracker.GetMapLevel(0).SizeY - 1)
                    {
                        if (SeeableByPlayer(startX + i, startY + j))
                        {
                            visibleTiles.Add(new Distance(startX + i, startY + j));
                        }
                    }
                        
                }
            }
            return visibleTiles;
        }
        static private void ExploreFurther(int lastExploredCount)
        {
            Console.WriteLine(" " + lastExploredTile + " " + lastExploredCount);
            previousRadiusExploredCounter = perRadiusExploredCounter;
            perRadiusExploredCounter = 0;
            for (int i = lastExploredTile; i < lastExploredTile + lastExploredCount; i++)
            {
                DiscoverExplorableTile(i);
            }
            lastExploredTile += previousRadiusExploredCounter;
        }
        static private void DiscoverExplorableTile(int index)
        {
            if (visibleTiles[index].X > 0 && visibleTiles[index].X < MapLevelTracker.GetMapLevel(0).SizeX - 1 && visibleTiles[index].Y > 0 && visibleTiles[index].Y < MapLevelTracker.GetMapLevel(0).SizeY - 1)
            {
                if (!isObstacleAtLocation(visibleTiles[index].X, visibleTiles[index].Y))
                    DiscoverAroundTile(index);
;           }


        }
        static private void DiscoverAroundTile(int index)
        {

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i != 0 || j != 0)
                    {
                        bool previouslyExplored = false;
                        for (int k = lastExploredTile; k < visibleTiles.Count; k++)
                        {
                            if (visibleTiles[k].X == visibleTiles[index].X + i && visibleTiles[k].Y == visibleTiles[index].Y + j)
                            {
                                previouslyExplored = true;
                                break;
                            }
                        }
                        if (!previouslyExplored && SeeableByPlayer(visibleTiles[index].X + i, visibleTiles[index].Y + j))
                        {
                            visibleTiles.Add(new Distance(visibleTiles[index].X + i, visibleTiles[index].Y + j));
                            perRadiusExploredCounter++;
                        }

                    }
                }
            }
        }
        static private bool SeeableByPlayer(int x, int y)
        {
            int diffX = x-Player.GetPlayer().PosX;
            int diffY = y-Player.GetPlayer().PosY;
            int multiplication = 0;
            int stepX = 0;
            int resid = 0;
            int stepY = 0;
            bool seenable = true;
            if (Math.Abs(diffX) >= Math.Abs(diffY))
            {
                multiplication = Math.Abs(diffY);
                if (diffY == 0)
                    multiplication = 1;
                stepX = diffX / multiplication;
                resid = diffX - stepX * multiplication;
                stepY = diffY / multiplication;
            }
            else
            {
                multiplication = Math.Abs(diffX);
                if (diffX == 0)
                    multiplication = 1;
                stepY = diffY / multiplication;
                resid = diffY - stepY * multiplication;
                stepX = diffX / multiplication;
            }
            if (Math.Abs(diffX) >= Math.Abs(diffY))
            {
                int leftX = stepX;
                int residualCount = 0;
                for (int i = 0; i < multiplication; i++)
                {
                    if (Math.Abs(resid) > Math.Abs(residualCount))
                    {
                        if (resid > 0)
                        {
                            leftX++;
                        }
                        else if (resid < 0)
                        {
                            leftX--;
                        }
                        
                    }
                    while (leftX != 0)
                    {
                        if (isObstacleAtLocation(Player.GetPlayer().PosX +residualCount+ stepX * i + (stepX + (residualCount != resid ? (stepX > 0 ? 1 : -1) : 0) - leftX), Player.GetPlayer().PosY + stepY * i))
                        {
                            seenable = false;
                            break;
                        }
                        if (leftX > 0)
                            leftX--;
                        else
                            leftX++;
                    }
                    if (!seenable)
                        return false;
                    if (isObstacleAtLocation(Player.GetPlayer().PosX +residualCount+ stepX + stepX * i + (residualCount != resid ? (stepX > 0 ? 1 : -1) : 0), Player.GetPlayer().PosY + stepY + stepY * i) &&i!=multiplication-1)
                    {
                        seenable = false;
                        break;
                    }
                    leftX = stepX;
                    if (Math.Abs(resid) > Math.Abs(residualCount))
                    {
                        if (resid > 0)
                        {
                            residualCount++;
                        }
                        else if (resid < 0)
                        {
                            residualCount--;
                        }

                    }
                }
            }
            else
            {
                int leftY = stepY;
                int residualCount = 0;

                for (int i = 0; i < multiplication; i++)
                {
                    if (Math.Abs(resid) > Math.Abs(residualCount))
                    {
                        if (resid > 0)
                        {
                            leftY++;
                        }
                        else if (resid < 0)
                        {
                            leftY--;
                        }

                    }
                    while (leftY != 0)
                    {
                        if (isObstacleAtLocation(Player.GetPlayer().PosX+ stepX *i, Player.GetPlayer().PosY + residualCount+(stepY+ stepY *i + (residualCount != resid ? (stepY > 0 ? 1 : -1) : 0) - leftY)))
                        {
                            seenable = false;
                            break;
                        }
                        if (leftY > 0)
                            leftY--;
                        else
                            leftY++;
                    }
                    if (!seenable)
                        return false;
                    if (isObstacleAtLocation(Player.GetPlayer().PosX + stepX+stepX*i, Player.GetPlayer().PosY + stepY + stepY * i + residualCount + residualCount+(residualCount != resid ? (stepY > 0 ? 1 : -1) : 0))&&i!=multiplication-1)
                    {
                        seenable = false;
                        break;
                    }
                    leftY = stepY;
                    if (Math.Abs(resid) > Math.Abs(residualCount))
                    {
                        if (resid > 0)
                        {
                            residualCount++;
                        }
                        else if (resid < 0)
                        {
                            residualCount--;
                        }

                    }
                }
            }
            return seenable;
        }
        static private bool isObstacleAtLocation(int x, int y)
        {
            try
            {
                Structure tempStruct = MapLevelTracker.GetStructureTracker().FindStructureWithComponentCoordinates(x, y);
                if (tempStruct != null)
                {
                    if (tempStruct.designComponents[tempStruct.ReturnIndexOfComponentAtLocation(x, y)].GetTileDetails().Passable)
                    {
                        return false;
                    }
                    return true;
                }
                else if (MapLevelTracker.GetMapLevel(0).GetTileAtLocation(x, y).GetTileDetails().Passable)
                {
                    return false;
                }
                return true;
            }
            catch (IndexOutOfRangeException)
            {
                return true;
            }
        }
    }
}
