using System;
using System.Collections.Generic;
using System.Text;

namespace JustSomeRandomRPGMechanics
{
    class Tile
    {
        List<IEntity> contents;
        TileType type;
        TileFurniture furniture;
        
        public Tile(TileType type)
        {
            contents = new List<IEntity>();
            this.type = type;
            furniture = null;
            
        }
        public List<IEntity> ReturnContents()
        {
            return contents;
        }
        public TileType GetTileDetails()
        {
            return type;
        }
        public void ChangeTileType(TileType type)
        {
            this.type = type;
        }
        public TileFurniture GetFurniture()
        {
            return furniture;
        }
        public void AddContent(List<IEntity> entity)
        {
            foreach (IEntity something in entity)
                AddContent(something);
        }
        public void AddContent(IEntity entity)
        {
            contents.Add(entity);
        }
        public void RemoveContent(IEntity entity)
        {
            contents.Remove(entity);
        }
        public void RemoveSelected(bool[] selected)
        {
            for(int i = contents.Count - 1; i > -1; i--)
            {
                if (selected[i] == true)
                    RemoveContent(contents[i]);
            }
        }
        public void AddFurniture(TileFurniture furniture)
        {
            if (this.furniture != null)
            {
                this.furniture = furniture;
            }
            else
            {
                Display.DisplayDebugMessage("Furniture already exists at this location");
            }
        }
        public void RemoveFurniture()
        {
            furniture = null;
        }
        public void Destroy()
        {
            type=TileCreator.ReturnTypeWithName(type.degradeName);
        }
    }
}
