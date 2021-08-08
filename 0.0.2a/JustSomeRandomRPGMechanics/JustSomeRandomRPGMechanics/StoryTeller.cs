using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace JustSomeRandomRPGMechanics
{
    static class StoryTeller
    {
        static List<string> StoryText = new List<string>();
        static List<string> CombatText = new List<string>();
        static List<string> TownText = new List<string>();
        static List<string> AmbienceText = new List<string>();
        public static void LoadStoryTextFromFile(string filename)
        {
            using (StreamReader file = new StreamReader(filename))
            {
                string line = file.ReadLine();

                while (line != null)
                {
                    StoryText.Add(line);
                    line = file.ReadLine();
                }
            }
        }
        public static void LoadCombatTextFromFile(string filename)
        {
            using (StreamReader file = new StreamReader(filename))
            {
                string line = file.ReadLine();

                while (line != null)
                {
                    CombatText.Add(line);
                    line = file.ReadLine();
                }
            }
        }
        public static void LoadTownTextFromFile(string filename)
        {
            using (StreamReader file = new StreamReader(filename))
            {
                string line = file.ReadLine();

                while (line != null)
                {
                    TownText.Add(line);
                    line = file.ReadLine();
                }
            }
        }
        public static void LoadAmbienceTextFromFile(string filename)
        {
            using (StreamReader file = new StreamReader(filename))
            {
                string line = file.ReadLine();

                while (line != null)
                {
                    AmbienceText.Add(line);
                    line = file.ReadLine();
                }
            }
        }
        public static string GetStoryTextByIndex(int index)
        {
            try
            {
                return StoryText[index];
            }
            catch (Exception)
            {
                return "NULL";
            }
            
        }
        public static string GetCombatTextByIndex(int index)
        {
            try
            {
                return CombatText[index];
            }
            catch (Exception)
            {
                return "NULL";
            }

        }
        public static string GetAmbienceTextByIndex(int index)
        {
            try
            {
                return AmbienceText[index];
            }
            catch (Exception)
            {
                return "NULL";
            }

        }
        public static string GetTownTextByIndex(int index)
        {
            try
            {
                return TownText[index];
            }
            catch (Exception)
            {
                return "NULL";
            }

        }
        public static void LoadDemo()
        {
            CombatText.Add("1-Attack    2-Spell     3-Run");
            CombatText.Add("L-Load Game    Q-Quit Game");
        }
    }
}
