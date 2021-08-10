using System;
using System.Collections.Generic;
using System.Text;

namespace JustSomeRandomRPGMechanics
{
    class ItemEffect
    {
        string name;
        string codeName;
        int effectValue;
        public ItemEffect(string name,string effectCodeName,int effectValue)
        {
            this.name = name;
            codeName = effectCodeName;
            this.effectValue = effectValue;
        }
        public string GetName()
        {
            return name;
        }
        public override string ToString()
        {
            return name + ": " + effectValue;
        }
        public void TriggerEffect(LiveTarget target)
        {
            try
            {
                switch (codeName)
                {
                    case "BashDamage":
                        target.TakeBashHit(effectValue);
                        break;
                    case "PierceDamage":
                        target.TakePierceHit(effectValue);
                        break;
                    case "HPGain":
                        target.RecoverHP(effectValue);
                        break;

                }
            }
            catch (NullReferenceException)
            {
                if (GameVariables.UseDebug)
                {
                    Display.DisplayDebugMessage("Target not found or dead");
                }
            }
        }
    }
}
