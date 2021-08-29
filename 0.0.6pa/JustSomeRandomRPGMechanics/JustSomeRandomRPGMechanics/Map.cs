using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace JustSomeRandomRPGMechanics
{
    class Map
    {
        Tile[,] map;
        public int SizeX { get; private set; }
        public int SizeY { get; private set; }
        public int Level { get; private set; }
        static Map tempMap;
        static Structure tempStruct;
        public Map(int sizex,int sizey,int level)
        {
            map = new Tile[sizex, sizey];
            SizeX = sizex;
            SizeY = sizey;
            Level = level;
            FillWithEmptySpace();
        }
        public Map CloneMap()
        {
            Map copy = new Map(SizeX, SizeY, Level);
            for (int i = 0; i < copy.SizeX; i++)
            {
                for (int j = 0; j < copy.SizeY; j++)
                {
                    copy.ChangeAtLocation(i, j, GetTileAtLocation(i, j).GetTileDetails().mapTag);
                }
            }
            return copy;
        }
        public void FillWithEmptySpace()
        {
            for(int i = 0; i < SizeX; i++)
            {
                for (int j = 0; j < SizeY; j++)
                    map[i, j] = new Tile(TileCreator.ReturnTypeWithName("open space"));
            }
        }
        public void ChangeAtLocation(int x,int y,char mapTag)
        {
            Tile temp =new Tile(TileCreator.ReturnTypeWithTag(mapTag));
            map[x, y]=temp;
        }
        public Tile GetTileAtLocation(int x,int y)
        {
            return map[x, y];
        }
        public bool IsEntityTypeAtPosition(int x,int y,int ID)
        {
            foreach(IEntity entity in map[x, y].ReturnContents())
            {
                if (ID == entity.ReturnID())
                {
                    return true;
                }
            }
            return false;
        }
        public void DisplayTileContents(int x, int y)
        {
            //to be done
        }
        public static Map LoadDemoMap(string filename)//needs checkin for mapsize differences for real loading method
        {
            
            int i = 0;
            int j = 0;
            using (StreamReader file = new StreamReader(filename))
            {
                int sizex = Int32.Parse(file.ReadLine());
                int sizey = Int32.Parse(file.ReadLine());
                string level = file.ReadLine();
                tempMap = new Map(sizex, sizey, Int16.Parse(level));
                int entity = file.Read();
                while (entity != -1)
                {
                    if (entity == '\n')
                    {
                        i = 0;
                        j++;
                    }
                    else if (entity != '\r')//most windows test editors have \r\n at end of the line
                    {
                        tempMap.ChangeAtLocation(i, j, ((char)entity));
                        i++;
                        
                    }
                    entity = file.Read();
                }
                for (i = 0; i < sizex; i++)
                {
                    for (j = 0; j < sizey; j++)
                    {
                        if(tempMap.GetTileAtLocation(i,j).GetTileDetails().Name!="open space")
                        {
                            if (MapLevelTracker.GetStructureTracker().FindStructureWithComponentCoordinates(i, j) == null)
                            {
                                tempStruct = new Structure(i, j);
                                DiscoverStructure(i, j);
                                MapLevelTracker.GetStructureTracker().AddStructure(tempStruct);
                            }
                        }
                    }
                }
                return tempMap;
            }
            
        }
        static void DiscoverStructure(int x, int y)
        {
            if(tempMap.GetTileAtLocation(x,y).GetTileDetails().Name != "open space"){
                tempStruct.AddComponent(tempMap.GetTileAtLocation(x, y), x, y);
                tempMap.ChangeAtLocation(x, y, '.');
                DiscoverStructure(x - 1, y - 1);
                DiscoverStructure(x, y - 1);
                DiscoverStructure(x + 1, y - 1);
                DiscoverStructure(x - 1, y);
                DiscoverStructure(x + 1, y );
                DiscoverStructure(x - 1, y + 1);
                DiscoverStructure(x, y + 1);
                DiscoverStructure(x + 1, y + 1);
            }
        }
    }
}
