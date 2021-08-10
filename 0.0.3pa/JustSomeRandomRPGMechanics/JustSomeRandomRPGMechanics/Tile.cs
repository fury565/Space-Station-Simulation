using System;
using System.Collections.Generic;
using System.Text;

namespace JustSomeRandomRPGMechanics
{
    class Tile
    {
        List<IEntity> contents;
        TileType type;
        public Tile(TileType type)
        {
            contents = new List<IEntity>();
            this.type = type;
        }
        public List<IEntity> ReturnContents()
        {
            return contents;
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
        public TileType GetTileDetails()
        {
            return type;
        }
    }
}
