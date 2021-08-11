using System;
using System.Collections.Generic;
using System.Text;

namespace JustSomeRandomRPGMechanics
{
    class TargetNotFoundException:SystemException
    {
        public void DisplayErrorMessage()
        {
            Display.DisplayDebugMessage("Target NPC not found or already dead");
        }
    }
}
