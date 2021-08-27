using System;
using System.Collections.Generic;
using System.Text;

namespace JustSomeRandomRPGMechanics
{
    static class PathFinder
    {
        static List<TestPath> hell;
        static List<Distance> valueLocations;
        static List<bool> isTaken;
        static public Path FindPath(LiveTarget npc)
        {
            hell = new List<TestPath>();
            valueLocations = new List<Distance>();
            isTaken = new List<bool>();
            Player player = Player.GetPlayer();
            hell.Add(new TestPath(0, 0,player.PosX,player.PosY,new Distance(npc.PosX,npc.PosY)));
            valueLocations.Add(new Distance(npc.PosX, npc.PosY));
            isTaken.Add(false);
            int loopCounter = 0;
            while (true)
            {
                loopCounter++;
                DiscoverAroundTile(valueLocations[IndexOfSmallestDistanceValue()].X, valueLocations[IndexOfSmallestDistanceValue()].Y,npc);
                foreach(TestPath path in hell)
                {
                    if (path.CalculateDistanceToTarget() == 0||loopCounter>1000)//loopCounter useless after vision is implemented
                    {
                        Path realPath = new Path();
                        TestPath guide = path;
                        while (guide.path!= null)
                        {
                            realPath.AddPath(guide.distance);
                            guide = guide.path;
                        }
                        return realPath;
                    }
                }
            }
        }
        static public Distance FindPath(LiveTarget traveller, LiveTarget destinator)
        {
            Display.DisplayMessage("npcs too dumb for now");
            return new Distance(0, 0);
        }
        static private int IndexOfTileLocation(int x,int y)
        {
            int counter = 0;
            foreach(Distance location in valueLocations)
            {
                if (x == location.X && y == location.Y)
                    return counter;
                counter++;
            }
            return -1;
        }
        static private int IndexOfSmallestDistanceValue()
        {
            int smallestValue = hell[0].ReturnTotalDistance() + hell[0].CalculateDistanceToTarget();
            int smallestIndex = 0;
            int continueator= 0;
            for (int i = 0; i < hell.Count; i++)
            {
                if (!isTaken[i])
                {
                    smallestIndex = i;
                    smallestValue= hell[i].ReturnTotalDistance() + hell[i].CalculateDistanceToTarget();
                    continueator = i;
                    break;
                }

            }
            
            for(int i = continueator; i < hell.Count; i++)
            {
                if (!isTaken[i])
                {
                    if (smallestValue > hell[i].ReturnTotalDistance() + hell[i].CalculateDistanceToTarget())
                    {
                        smallestIndex = i;
                        smallestValue = hell[i].ReturnTotalDistance() + hell[i].CalculateDistanceToTarget();
                    }
                }
                
            }
            return smallestIndex;
        }
        static private bool isObstacleAtLocation(int x,int y)
        {
            if (x >= 0 && x < MapLevelTracker.GetMapLevel(0).SizeX && y >= 0 && y < MapLevelTracker.GetMapLevel(0).SizeY)
            {
                Structure tempStruct = MapLevelTracker.GetStructureTracker().FindStructureWithComponentCoordinates(x, y);
                if (tempStruct != null)
                {
                    if (!tempStruct.designComponents[tempStruct.ReturnIndexOfComponentAtLocation(x, y)].GetTileDetails().Passable)
                    {
                        return true;
                    }
                        
                    return false;
                }
                else if (!MapLevelTracker.GetMapLevel(0).GetTileAtLocation(x, y).GetTileDetails().Passable)
                    return true;

                else

                    return false;
                    
            } 
            else
            {
                return true;
            }
                
        }
        static private void DiscoverAroundTile(int x, int y,LiveTarget npc)
        {
            for(int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i != 0 || j != 0)
                    {
                        if (!isObstacleAtLocation(x + i, y + j)&&IndexOfTileLocation(x+i,y+j)==-1&&MapLevelTracker.GetNPCTracker().GetNPCatLocation(x+i,y+j) is NullTarget)
                        {
                            hell.Add(new TestPath(i, j, Player.GetPlayer().PosX, Player.GetPlayer().PosY ,new Distance(x+i,y+j), hell[IndexOfTileLocation(x, y)]));
                            isTaken.Add(false);
                            valueLocations.Add(new Distance(x + i, y + j));
                        }
                    }
                }
            }
            isTaken[IndexOfTileLocation(x, y)] = true;
        }
    }
}
