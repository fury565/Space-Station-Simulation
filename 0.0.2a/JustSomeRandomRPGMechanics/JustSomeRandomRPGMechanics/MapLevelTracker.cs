using System;
using System.Collections.Generic;
using System.Text;

namespace JustSomeRandomRPGMechanics
{
    class MapLevelTracker
    {
        static List<Map> world;
        static NPCTracker npcTracker;
        public static bool displayed;
        public static void CreateWorld()
        {
            if (world == null)
            {
                world = new List<Map>();
                npcTracker = new NPCTracker();
            }
        }
        private MapLevelTracker()
        {
            world = new List<Map>();
        }
        public static void AddMapLevelToWorld(Map map)
        {
            bool LevelExists = false;
            for(int i = 0; i < world.Count; i++)
            {
                if (map.Level == world[i].Level)
                {
                    LevelExists = true;
                    break;
                }
            }
            if (!LevelExists)
                world.Add(map);
        }
        public static void RemoveMapLevelFromWorld(int level)
        {
            for (int i = 0; i < world.Count; i++)
            {
                if (level == world[i].Level)
                {
                    world.RemoveAt(i);
                    break;
                }
            }
        }
        public static Map GetMapLevel(int level)
        {
            for (int i = 0; i < world.Count; i++)
            {
                if (level == world[i].Level)
                {
                    return world[i];
                }
            }
            return new Map(10, 10, 99);//needs proper error code
        }
        public static NPCTracker GetNPCTracker()
        {
            return npcTracker;
        }
    }
}
