using System;
using System.Collections.Generic;
using System.Text;

namespace JustSomeRandomRPGMechanics
{
    class Vehicle:Structure
    {
		public int Rotation{get;private set;}
        public Vehicle(int posx,int posy) : base(posx, posy)
        {

        }
        public void Move(int xDistance,int yDistance)
        {
            base.PosX += xDistance;
            base.PosY += yDistance;
            foreach(Distance Location in componentLocation)
            {
                Location.Change(Location.X+xDistance,Location.Y+yDistance);
            }
        }
		public void RotateRight(){
			Rotation+=90;
			if(Rotation==360)
				Rotation=0;
		}
		public void RotateLeft(){
			Rotation-=90;
			if(Rotation<0)
				Rotation=270;
		}
		/*public override void ActivateComponent(int x,int y)
        {
            int index = ReturnIndexOfComponentAtLocation(x, y);
            if(designComponents[index].GetTileDetails().Effect=="FireLaser"){
				//implement laser firing
			}
        }*/
    }
}
