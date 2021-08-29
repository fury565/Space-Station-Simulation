using System;
using System.Collections.Generic;
using System.Text;

namespace JustSomeRandomRPGMechanics
{
    static class GameVariables
    {
        public static double HPGrowthRate = 2.2;
        public static double MPGrowthRate = 1.9;
        public static double LevelXPCostModifier = 1.3;
        public static int WindowWidth = Console.LargestWindowWidth;
        public static int WindowHeight = Console.LargestWindowHeight;
        public static int MapDisplayWidth = WindowWidth-50;
        public static int MapDisplayHeight = WindowHeight-10;
        public static bool UseDebug = false;

    }
}
