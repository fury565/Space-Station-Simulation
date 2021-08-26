using System;
using System.Collections.Generic;
using System.Text;

namespace JustSomeRandomRPGMechanics
{
    class NullTarget:LiveTarget
    {
        public static NullTarget CreateNullTarget()
        {
            return new NullTarget(0, "", 0, 0, 0, 0, 0, 0, 0);
        }
        private NullTarget(int id, string name, int maxhp, int str, int dex, int intelligence, int per, int nat_armor,int sight_radius, int posx = 0, int posy = 0) : base(id, name, maxhp, str, dex, intelligence, per, nat_armor,sight_radius, posx, posy)
        {

        }
    }
}
