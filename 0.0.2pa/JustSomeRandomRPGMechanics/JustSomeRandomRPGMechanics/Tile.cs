using System;
using System.Collections.Generic;
using System.Text;

namespace JustSomeRandomRPGMechanics
{
    class Tile
    {
        List<IEntity> contents;
        char type;
        public Tile(char type)
        {
            contents = new List<IEntity>();
            this.type = type;
        }
        public IReadOnlyCollection<IEntity> ReturnContents()
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
        public void ChangeType(char type)
        {
            this.type = type;
        }
        public char GetTileType()
        {
            return type;
        }
    }
}
