using System;
using System.Collections.Generic;

namespace JustSomeRandomRPGMechanics
{
    class Program
    {
        static void Main(string[] args)
        {
            /*SoundManager soundPlayer = new SoundManager();
            soundPlayer.FindAllSoundFiles();
            soundPlayer.ChooseSound(0);
            soundPlayer.Play();*/
            Console.SetWindowSize(GameVariables.WindowWidth, GameVariables.WindowHeight);
            Console.SetBufferSize(GameVariables.WindowWidth, GameVariables.WindowHeight);
            StoryTeller.LoadDemo();
            BaseNonTargettableEntityCollection.ReadLootTableFromTextFile("items.txt");
            Bestiary.ReadCreaturesFromTextFile("npcs.txt");
            NPCCustomizer.LoadCreatureDrops("npcdrops.txt");
            TileCreator.ReadTypesFromFile("tiletypes.txt");
            Player player = Player.GetPlayer(6, 6);
            player.GetInventory().Pickup(BaseNonTargettableEntityCollection.GetLootAtIndex(1));
            Console.Clear();
            Console.SetCursorPosition(0, Console.CursorTop);
            MapLevelTracker.CreateWorld();
            MapLevelTracker.AddMapLevelToWorld(Map.LoadDemoMap("testmap1.txt"));
            Console.SetCursorPosition(0, Console.CursorTop);
            Display.DisplayMessage("i-inventory c-character stats z-search h-help");
            Console.WriteLine("Press enter to start.");
            Console.Read();
            Console.Clear();
            while (true)
            {
                Display.DisplayWorldAroundPlayer();
                ConsoleKeyInfo pressed = Console.ReadKey(true);
                KeyAction.CheckPressedKey(pressed);
                TurnHandler.NPCTurn();
            }
        }
    }
}
