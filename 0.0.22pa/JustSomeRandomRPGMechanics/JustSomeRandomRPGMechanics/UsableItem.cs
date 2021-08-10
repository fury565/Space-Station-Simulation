using System;
using System.Collections.Generic;
using System.Text;

namespace JustSomeRandomRPGMechanics
{
    class UsableItem:Item
    {
        List<ItemEffect> effects;
        public UsableItem(int id, string name, string description, double weight, double volume,List<ItemEffect> effect, int count = 1) : base(id,name, description, weight, volume, count)
        {
            effects = effect;
        }
        public UsableItem(UsableItem original):base(original)
        {
            effects = original.effects;
        }
        public void UseItem(LiveTarget target,IInventory inventory)
        {
            bool consumed = false;
            foreach(ItemEffect effect in effects)
            {
                effect.TriggerEffect(target);
                if (effect.GetName() != "CutDamage" && effect.GetName() != "BashDamage")
                    consumed = true;
            }
            if(consumed)
                inventory.Drop(this);
        }
        public override string GetEntityDescription()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.name+" x"+base.count);
            sb.AppendLine(base.description);
            sb.AppendLine("Effects:");
            foreach (ItemEffect effect in effects)
                sb.AppendLine("    "+effect.ToString());
            return sb.ToString();
        }
    }
}
