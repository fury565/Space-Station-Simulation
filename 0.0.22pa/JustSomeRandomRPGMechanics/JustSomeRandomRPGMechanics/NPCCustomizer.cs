using System;
using System.Collections.Generic;
using System.IO;

namespace JustSomeRandomRPGMechanics
{
    public static class NPCCustomizer
    {
        static List<String> creaturedrops = new List<string>();
        public static void LoadCreatureDrops(string filename)
        {
            using (StreamReader file = new StreamReader(filename))
            {
                string line = file.ReadLine();

                while (line != null)
                {
                    creaturedrops.Add(line);
                    line = file.ReadLine();
                }
            }
        }
        public static List<IEntity> GenerateLoot()
        {
            List<IEntity> drop = new List<IEntity>();
            drop.Add(BaseNonTargettableEntityCollection.GetLootAtIndex(0));
            return drop;
        }
        public static List<IEntity> GenerateLoot(int creatureID)
        {
            List<IEntity> drop = new List<IEntity>();
            string[] items = creaturedrops[creatureID].Split(';');
            Random generator = new Random();
            for(int i = 0; i < items.Length; i++)
            {
                string[] item = items[i].Split(',');
                if (Double.Parse(item[2]) >= generator.NextDouble())
                {
                    drop.Add(BaseNonTargettableEntityCollection.GetLootAtIndex(Int32.Parse(item[0]) - 1));
                    while (drop[drop.Count-1].GetCount() < Int32.Parse(item[1]))
                        ((Item)drop[drop.Count - 1]).IncreaseCount(1);
                }
            }
            return drop;
        }
    }
}
