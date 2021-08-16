using System;
using System.Collections.Generic;
using System.Text;

namespace JustSomeRandomRPGMechanics
{
    class TileFurniture
    {
        public string Name { get; private set; }
        public char mapTag { get; private set; }
        public bool Passable { get; private set; }
        public bool Interactable { get; private set; }
        public string Effect { get; private set; }
        public TileFurniture(string name, char maptag, bool passable, bool leaks, bool interactable, string effect)
        {
            Name = name;
            mapTag = maptag;
            Passable = passable;
            Interactable = interactable;
            Effect = effect;
        }

    }
}
