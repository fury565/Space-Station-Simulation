using System;
using System.Collections.Generic;
using System.Text;

namespace JustSomeRandomRPGMechanics
{
    class Player:LiveTarget
    {
        static Player player;
        public static Player GetPlayer(int posx=0,int posy=0)
        {
            if (player == null)
            {
                Display.DisplayMessage("Enter your name:");
                string name = Console.ReadLine();
                player = CreateNewPlayer(name,posx,posy);
            }
            return player;
        }
        private Player(int id, string name, int maxhp, int str, int dex, int intelligence, int per, int nat_armor, int posx,int posy) : base(id,name, maxhp, str, dex, intelligence, per, nat_armor, posx,posy)
        {
            base.needs = new HealthSystem(maxhp, true);
        }
        static Player CreateNewPlayer()
        {
            return new Player(42069,"Bob",30,5,5,5,5,2,0,0);
        }
        static Player CreateNewPlayer(string name,int posx,int posy)
        {
            return new Player(42069,name, 30, 5, 5, 5, 5, 2, posx,posy);
        }
        public void Examine(int xdistance, int ydistance)
        {
            int targetx = posx+ xdistance;
            int targety = posy+ydistance;
            Map currentlevel = MapLevelTracker.GetMapLevel(0);
            if (posy < 0 || posy >= currentlevel.SizeY || posx < 0 || posx >= currentlevel.SizeX)
                return;
            Display.DisplayDebugMessage("Targetable tile");
            if (currentlevel.GetTileAtLocation(targetx,targety).GetTileDetails().Interactable)
            {
                if (currentlevel.GetTileAtLocation(targetx, targety).GetTileDetails().Effect == "Close")
                {
                    MapLevelTracker.GetMapLevel(0).ChangeAtLocation(targetx, targety, TileCreator.ReturnTypeWithName(currentlevel.GetTileAtLocation(targetx, targety).GetTileDetails().Name.Replace("open", "closed")).mapTag);
                    Display.DisplayDebugMessage("Close it");
                }
                else if (currentlevel.GetTileAtLocation(targetx, targety).GetTileDetails().Effect == "Open")
                {
                    MapLevelTracker.GetMapLevel(0).ChangeAtLocation(targetx, targety, TileCreator.ReturnTypeWithName(currentlevel.GetTileAtLocation(targetx, targety).GetTileDetails().Name.Replace("closed", "open")).mapTag);
                }
                MapLevelTracker.displayed = false;
            }
        }
        public override void Move(int xdistance, int ydistance)
        {
            LiveTarget targetNPC = MapLevelTracker.GetNPCTracker().GetNPCatLocation(posx + xdistance, posy + ydistance);
            if (targetNPC is NullTarget)
            {
                PerformMovement(xdistance, ydistance);
            }
            else
            {
                Attack(targetNPC);
            }
        }
        public override void TakeBashHit(int numValue)
        {
            base.TakeBashHit(numValue);
            Display.DisplayDebugMessage("It was actually you");
        }
        public override void TakePierceHit(int numValue)
        {
            base.TakePierceHit(numValue);
            Display.DisplayDebugMessage("It was actually you");
        }
        private void Die()
        {
            if (HP <= 0)
            {
                Display.DisplayDebugMessage("Your immortality keeps you alive");
            }
            
        }
        public bool[] ChooseItems(List<IEntity> items)
        {
            int counter = 0;
            bool[] selected = new bool[items.Count];
            for (int i = 0; i < items.Count; i++)
                selected[i] = false;
            while (true)
            {
                //Display.DisplayMessage(inventory[counter].GetLootDescription());   Need to create method and attribute for actual item description
                Console.SetCursorPosition(GameVariables.screenLenght - 2, 1);
                Display.DisplayMessage("<");
                ConsoleKeyInfo pressed = Console.ReadKey();
                if (pressed.Key == ConsoleKey.DownArrow)
                {
                    counter++;
                    if (counter == items.Count)
                        counter = 0;
                }

                else if (pressed.Key == ConsoleKey.UpArrow)
                {
                    counter--;
                    if (counter == -1)
                        counter = items.Count - 1;
                }
                else if (pressed.Key == ConsoleKey.RightArrow)
                {
                    selected[counter] = true;
                }
                else if (pressed.Key == ConsoleKey.LeftArrow)
                {
                    selected[counter] = false;
                }
                else if (pressed.Key == ConsoleKey.Enter)
                {
                    for(int i = 0; i < items.Count; i++)
                    {
                        if (selected[i] == true)
                            GetInventory().Pickup(items[i]);
                    }
                    return selected;
                }
                else if (pressed.Key == ConsoleKey.Escape)
                {
                    return selected;
                }
            }
        }
        public void Metabolism()
        {
            needs.Metabolism();
        }
        public string ShowStatus()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Health: ");sb.Append(needs.CurrentHealth.ToString());sb.AppendLine(" ");
            sb.Append("Food: ");sb.Append(needs.foodFullness.ToString()); sb.AppendLine(" ");
            sb.Append("Water: ");sb.Append(needs.waterFullness.ToString()); sb.AppendLine(" ");

            return sb.ToString();
        }
    }
}
