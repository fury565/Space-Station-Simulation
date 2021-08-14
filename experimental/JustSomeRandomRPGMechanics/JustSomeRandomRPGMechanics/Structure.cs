using System;
using System.Collections.Generic;
using System.Text;

namespace JustSomeRandomRPGMechanics
{
    class Structure
    {
        public List<Tile> designComponents { get; protected set; }
        public List<Distance> componentLocation { get; protected set; }
        public int PosX { get; protected set; }
        public int PosY { get; protected set; }
        
        public Structure(int posX,int posY)
        {
            designComponents = new List<Tile>();
            componentLocation = new List<Distance>();
            PosX = posX;
            PosY = posY;
        }
        public void AddComponent(Tile component,int x,int y)
        {
            designComponents.Add(component);
            componentLocation.Add(new Distance(x, y));
        }
        public void RemoveComponent(int x,int y)
        {

            int counter = ReturnIndexOfComponentAtLocation(x,y);
            designComponents[counter].Destroy();
            if(designComponents[counter].GetTileDetails().Name=="open space")
            {
                componentLocation.Remove(componentLocation[counter]);
                designComponents.Remove(designComponents[counter]);
            }
            
        }
        public int ReturnIndexOfComponentAtLocation(int x,int y)
        {
            int counter = 0;
            foreach (Distance location in componentLocation)
            {
                if (location.X == x && location.Y == y)
                    break;
                counter++;
            }
            return counter;
        }
		public void ChooseComponent(){
			int counter=0;
            List<Tile> activable = new List<Tile>();
            foreach(Tile component in designComponents)
            {
                if (component.GetTileDetails().Interactable &&component.GetTileDetails().Name!="console")
                    activable.Add(component);
            }
			while(true){
				Display.DisplayStructureOptions(activable,counter);
				ConsoleKeyInfo pressed=Console.ReadKey(true);
                if (pressed.Key == ConsoleKey.Enter)
                {
                    ActivateComponent(activable, counter);
                    break;
                }
				else if(pressed.Key==ConsoleKey.DownArrow)
					counter++;
				else if(pressed.Key==ConsoleKey.UpArrow)
					counter--;
			}
			
			
		}
        public virtual void ActivateComponent(List<Tile> activables,int index)
        {
            if(activables[index].GetTileDetails().Effect=="FireLaser"){
				//implement laser firing
			}
        }
    }
}
