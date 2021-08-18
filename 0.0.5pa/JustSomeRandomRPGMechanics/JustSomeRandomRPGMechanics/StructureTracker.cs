using System;
using System.Collections.Generic;
using System.Text;

namespace JustSomeRandomRPGMechanics
{
    class StructureTracker
    {
        List<Structure> structures;
        public StructureTracker()
        {
            structures = new List<Structure>();
        }
        public IReadOnlyCollection<Structure> GetStructures()
        {
            return structures;
        }
        public Structure FindStructureWithComponentCoordinates(int x, int y)
        {
            foreach (Structure structure in structures)
            {
                foreach(Distance location in structure.componentLocation)
                {
                    if (location.X == x && location.Y == y)
                        return structure;
                }
                
            }
            return null;
        }
        public void AddStructure(Structure entity)
        {
            structures.Add(entity);
        }
        public void RemoveStructure(Structure entity)
        {
            structures.Remove(entity);
        }
        public void MoveStructure(Structure structure, int xdistance, int ydistance)
        {
            if(structure is Vehicle)
            {
                foreach (Structure structure1 in structures)
                {
                    if (structure == structure1)
                    {
                        ((Vehicle)structure).Move(xdistance, ydistance);
                    }
                }
            }
            
            
        }
        public void DemoLoad()
        {
            Structure dummy = new Structure(3, 3);
            dummy.AddComponent(new Tile(TileCreator.ReturnTypeWithName("console")), 5, 4);
            dummy.AddComponent(new Tile(TileCreator.ReturnTypeWithName("steel door(closed)")), 6, 3);
            dummy.AddComponent(new Tile(TileCreator.ReturnTypeWithName("steel wall")), 4, 5);
            AddStructure(dummy);
        }
    }
}
