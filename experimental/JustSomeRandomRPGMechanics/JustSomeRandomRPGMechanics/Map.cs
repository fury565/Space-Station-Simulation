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
        public Map(int sizex,int sizey,int level)
        {
            map = new Tile[sizex, sizey];
            SizeX = sizex;
            SizeY = sizey;
            Level = level;
            FillWithEmptySpace();
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
                if (sizex >= GameVariables.MapDisplayWidth)
                    sizex = GameVariables.MapDisplayWidth - 1;
                if (sizey >= GameVariables.MapDisplayHeight)
                    sizey = GameVariables.MapDisplayHeight - 1;
                string level = file.ReadLine();
                Map testmap = new Map(sizex, sizey, Int16.Parse(level));
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
                        testmap.ChangeAtLocation(i, j, ((char)entity));
                        i++;
                        
                    }
                    entity = file.Read();
                }
                return testmap;
            }
        }
    }
}
