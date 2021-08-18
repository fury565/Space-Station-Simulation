using System;
using System.Collections.Generic;
using System.IO;

namespace JustSomeRandomRPGMechanics
{
    static class TileCreator
    {
        static List<TileType> types=new List<TileType>();
        static public void ReadTypesFromFile(string filename)
        {
            using (StreamReader file = new StreamReader(filename))
            {
                string line = file.ReadLine();

                while (line != null)
                {
                    string[] splitted = line.Split(',');
                    if (Int32.Parse(splitted[4]) == 0)
                    {
                        TileType type = new TileType(splitted[0], Char.Parse(splitted[1]), Int32.Parse(splitted[2]) == 1, Int32.Parse(splitted[3]) == 1,Int32.Parse(splitted[4])==1,"",splitted[5]);
                        AddType(type);
                    }
                    else
                    {
                        TileType type = new TileType(splitted[0], Char.Parse(splitted[1]), Int32.Parse(splitted[2]) == 1, Int32.Parse(splitted[3]) == 1, Int32.Parse(splitted[4]) == 1, splitted[5], splitted[6]);
                        AddType(type);
                    }
                    
                    line = file.ReadLine();
                }
            }
        }
        static public void AddType(TileType type)
        {
            types.Add(type);
        }
        static public TileType ReturnTypeWithName(string name)
        {
            foreach(TileType type in types)
            {
                if (type.Name == name)
                {
                    return type;
                }
            }
            Display.DisplayDebugMessage("TileTypeNotFoundException");
            return types[0];
        }
        static public TileType ReturnTypeWithTag(char tag)
        {
            foreach (TileType type in types)
            {
                if (type.mapTag == tag)
                {
                    return type;
                }
            }
            Display.DisplayDebugMessage("TileTypeNotFoundException");
            return types[0];
        }
    }
}
