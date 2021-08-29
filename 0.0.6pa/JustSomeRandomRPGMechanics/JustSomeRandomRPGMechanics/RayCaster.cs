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
        static public List<Distance> FindVisibleTiles(LiveTarget start)
        {
            perRadiusExploredCounter = 1;
            lastExploredTile = 0;
            previousRadiusExploredCounter = 0;
            visibleTiles = new List<Distance>();
            visibleTiles.Add(new Distance(start.PosX, start.PosY));
            for (int i = 1; i <= start.GetSightRadius(); i++)
            {
                ExploreFurther(start,perRadiusExploredCounter);
            }
            return visibleTiles;
        }
        static public List<Distance> ExperimentalFind(LiveTarget start)
        {
            visibleTiles = new List<Distance>();
            for(int i = -start.GetSightRadius(); i <= start.GetSightRadius(); i++)
            {
                for(int j=-start.GetSightRadius(); j <= start.GetSightRadius(); j++)
                {
                    if (start.PosX+i >= 0 && start.PosX+i <= MapLevelTracker.GetMapLevel(0).SizeX - 1 && start.PosY + j >= 0 && start.PosY + j <= MapLevelTracker.GetMapLevel(0).SizeY - 1)
                    {
                        if (SeeableByEntity(start, start.PosX + i, start.PosY + j))
                        {
                            visibleTiles.Add(new Distance(start.PosX + i, start.PosY + j));
                        }
                    }
                        
                }
            }
            return visibleTiles;
        }
        static private void ExploreFurther(LiveTarget start, int lastExploredCount)
        {
            Console.WriteLine(" " + lastExploredTile + " " + lastExploredCount);
            previousRadiusExploredCounter = perRadiusExploredCounter;
            perRadiusExploredCounter = 0;
            for (int i = lastExploredTile; i < lastExploredTile + lastExploredCount; i++)
            {
                DiscoverExplorableTile(start, i);
            }
            lastExploredTile += previousRadiusExploredCounter;
        }
        static private void DiscoverExplorableTile(LiveTarget start,int index)
        {
            if (visibleTiles[index].X > 0 && visibleTiles[index].X < MapLevelTracker.GetMapLevel(0).SizeX - 1 && visibleTiles[index].Y > 0 && visibleTiles[index].Y < MapLevelTracker.GetMapLevel(0).SizeY - 1)
            {
                if (!isObstacleAtLocation(visibleTiles[index].X, visibleTiles[index].Y))
                    DiscoverAroundTile(start,index);
;           }


        }
        static private void DiscoverAroundTile(LiveTarget start, int index)
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
                        if (!previouslyExplored && SeeableByEntity(start,visibleTiles[index].X + i, visibleTiles[index].Y + j))
                        {
                            visibleTiles.Add(new Distance(visibleTiles[index].X + i, visibleTiles[index].Y + j));
                            perRadiusExploredCounter++;
                        }

                    }
                }
            }
        }
        static private bool SeeableByEntity(LiveTarget start,int x, int y)
        {
            int diffX = x-start.PosX;
            int diffY = y-start.PosY;
            int multiplication = 0;
            int stepX = 0;
            int resid = 0;
            int stepY = 0;
            bool seenable = true;
            bool secDiffIs1 = false;
            if (Math.Abs(diffX) >= Math.Abs(diffY))
            {
                multiplication = Math.Abs(diffY);
                if (diffY == 0)
                    multiplication = 1;
                else if (Math.Abs(diffY) == 1&&Math.Abs(diffX)>4)//causes visible tiles at certain distances to be unseeable-second condition controls that distance
                {
                    multiplication = 2;
                    secDiffIs1 = true;
                }
                stepX = diffX / multiplication;
                resid = diffX - stepX * multiplication;
                stepY = secDiffIs1 == false ? diffY / multiplication: diffY > 0 ? 1 : -1;
            }
            else
            {
                multiplication = Math.Abs(diffX);
                if (diffX == 0)
                    multiplication = 1;
                else if (Math.Abs(diffX) == 1 && Math.Abs(diffY) > 4)
                {
                    multiplication = 2;
                    secDiffIs1 = true;
                }
                stepY = diffY / multiplication;
                resid = diffY - stepY * multiplication;
                stepX = secDiffIs1==false? diffX / multiplication: diffX>0?1:-1;
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
                        if (isObstacleAtLocation(start.PosX +residualCount+ stepX * i + (stepX + (residualCount != resid ? (stepX > 0 ? 1 : -1) : 0) - leftX), start.PosY + stepY * i))
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
                    if (i != multiplication - 1&&isObstacleAtLocation(start.PosX +residualCount+ stepX + stepX * i + (residualCount != resid ? (stepX > 0 ? 1 : -1) : 0), start.PosY + stepY + stepY * i))
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
                        if (isObstacleAtLocation(start.PosX+ stepX *i, start.PosY + residualCount+(stepY+ stepY *i + (residualCount != resid ? (stepY > 0 ? 1 : -1) : 0) - leftY)))
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
                    if (i != multiplication - 1&&isObstacleAtLocation(start.PosX + stepX+stepX*i, start.PosY + stepY + stepY * i + residualCount+(residualCount != resid ? (stepY > 0 ? 1 : -1) : 0)))
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
