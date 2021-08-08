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

        }
        static Player CreateNewPlayer()
        {
            return new Player(42069,"Bob",30,5,5,5,5,2,0,0);
        }
        static Player CreateNewPlayer(string name,int posx,int posy)
        {
            return new Player(42069,name, 30, 5, 5, 5, 5, 2, posx,posy);
        }
        public override void Move(int xdistance, int ydistance)
        {
            LiveTarget targetNPC = MapLevelTracker.GetNPCTracker().GetNPCatLocation(posx + xdistance, posy + ydistance);
            if (targetNPC is NullTarget)
            {
                posx += xdistance;
                posy += ydistance;
                if (posy < 0)
                    posy = 0;
                else if (posy >= MapLevelTracker.GetMapLevel(0).SizeY)
                    posy = MapLevelTracker.GetMapLevel(0).SizeY - 1;
                if (posx < 0)
                    posx = 0;
                else if (posx >= MapLevelTracker.GetMapLevel(0).SizeX)
                    posx = MapLevelTracker.GetMapLevel(0).SizeX - 1;
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
        public void Die()
        {
            Display.DisplayDebugMessage("Your immortality keeps you alive");
        }
        public string ShowStatus()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(GetName());
            return sb.ToString();
        }
    }
}
