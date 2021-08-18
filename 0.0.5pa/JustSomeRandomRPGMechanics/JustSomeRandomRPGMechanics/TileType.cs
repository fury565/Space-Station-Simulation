using System;
using System.Collections.Generic;
using System.Text;

namespace JustSomeRandomRPGMechanics
{
    class TileType
    {
        public string Name { get; private set; }
        public char mapTag { get; private set; }
        public bool Passable { get; private set; }
        public bool LeaksGas { get; private set; }
        public bool Interactable { get; private set; }
        public string Effect { get; private set; }
        public string degradeName { get; private set; }
        public TileType(string name, char maptag, bool passable,bool leaks,bool interactable,string effect, string degradeName)
        {
            Name = name;
            mapTag = maptag;
            Passable = passable;
            LeaksGas=leaks;
            Interactable = interactable;
            Effect = effect;
            this.degradeName = degradeName;
        }
    }
}
