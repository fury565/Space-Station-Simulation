using System;
using System.Collections.Generic;
using System.IO;

namespace JustSomeRandomRPGMechanics
{
    class Bestiary
    {
        static List<LiveTarget> creatures = new List<LiveTarget>();
        public static LiveTarget GetCreatureFromZone(int zoneID)
        {
            switch (zoneID){
                case 1:
                    return new LiveTarget(creatures[0]);
                default:
                    return new LiveTarget(creatures[0]);
            }
        }
        public static LiveTarget GetCreature(int creatureID)
        {
            return new LiveTarget(creatures[creatureID - 2000]);
        }
        public static void LoadDemo()
        {
            creatures.Add(new LiveTarget(2000,"Goblin",10, 2, 2,2,2,1));
        }
        public static void ReadCreaturesFromTextFile(string filename)
        {
            using (StreamReader file = new StreamReader(filename))
            {
                string line = file.ReadLine();

                while (line != null)
                {
                    string[] splitted = line.Split(',');
                    creatures.Add(new LiveTarget(Int32.Parse(splitted[0]), splitted[1], Int32.Parse(splitted[2]), Int32.Parse(splitted[3]), Int32.Parse(splitted[4]), Int32.Parse(splitted[5]), Int32.Parse(splitted[6]), Int32.Parse(splitted[7])));
                    line = file.ReadLine();
                }
            }
        }
    }
}
