using System;
using System.Collections.Generic;
using System.Text;

namespace JustSomeRandomRPGMechanics
{
    static class RayCaster
    {
        static List<Distance> visibleTiles;
        static int lastExploredTile;
        static int perRadiusExploredCounter;
        static int previousRadiusExploredCounter;
        static public List<Distance> FindVisibleTiles(int visionRadius,int startX,int startY)
        {
            perRadiusExploredCounter = 1;
            lastExploredTile = 0;
            previousRadiusExploredCounter = 0;
            visibleTiles = new List<Distance>();
            visibleTiles.Add(new Distance(startX, startY));
            for(int i = 1; i <= visionRadius; i++)
            {
                ExploreFurther(perRadiusExploredCounter);
            }
            return visibleTiles;
        }
        static private void ExploreFurther(int lastExploredCount)
        {
            Console.WriteLine(" "+lastExploredTile+" "+lastExploredCount);
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
            Structure tempStruct = MapLevelTracker.GetStructureTracker().FindStructureWithComponentCoordinates(visibleTiles[index].X, visibleTiles[index].Y);
            if (tempStruct != null)
            {
                if (tempStruct.designComponents[tempStruct.ReturnIndexOfComponentAtLocation(visibleTiles[index].X, visibleTiles[index].Y)].GetTileDetails().Passable)
                {
                    DiscoverAroundTile(index);
                }
            }
            else if (MapLevelTracker.GetMapLevel(0).GetTileAtLocation(visibleTiles[index].X, visibleTiles[index].Y).GetTileDetails().Passable)
                DiscoverAroundTile(index);

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
                            if(visibleTiles[k].X==visibleTiles[index].X+i&& visibleTiles[k].Y == visibleTiles[index].Y + j)
                            {
                                previouslyExplored = true;
                                break;
                            }
                        }
                        if (!previouslyExplored)
                        {
                            visibleTiles.Add(new Distance(visibleTiles[index].X + i, visibleTiles[index].Y + j));
                            perRadiusExploredCounter++;
                        }
                        
                    }
                }
            }
        }
    }
}
