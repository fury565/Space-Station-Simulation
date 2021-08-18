using System;
using System.Collections.Generic;
using System.Text;

namespace JustSomeRandomRPGMechanics
{
    class NPCTracker
    {
        List<LiveTarget> npcs;
        public NPCTracker()
        {
            npcs = new List<LiveTarget>();
        }
        public IReadOnlyCollection<LiveTarget> GetNPCS()
        {
            return npcs;
        }
        public LiveTarget GetNPCatLocation(int x, int y)
        {
            foreach(LiveTarget npc in npcs)
            {
                if (npc.PosX == x && npc.PosY == y)
                    return npc;
            }
            return NullTarget.CreateNullTarget();
        }
        public void AddNPC(LiveTarget entity)
        {
            npcs.Add(entity);
        }
        public void RemoveNPC(LiveTarget entity)
        {
            npcs.Remove(entity);
        }
        public void MoveAllNPCS()
        {
            int x=0, y=0;
            for(int i = 0; i < npcs.Count; i++)
            {
                if (npcs[i].GetAggroState() == 1)
                {
                    x = NumberGenerator.Generate(-1, 1);
                    y = NumberGenerator.Generate(-1, 1);
                }
                else if(npcs[i].GetAggroState() == 0)
                {
                    npcs[i].currentPath=PathFinder.FindPath(npcs[i]);
                }
                MoveNPC(i, x, y);
            }
        }
        public void MoveNPC(int index, int xdistance, int ydistance)
        {
            npcs[index].Move(xdistance,ydistance);
        }
    }
}
