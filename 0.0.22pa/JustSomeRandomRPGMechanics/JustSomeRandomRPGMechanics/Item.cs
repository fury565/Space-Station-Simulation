using System;
using System.Collections.Generic;
using System.Text;

namespace JustSomeRandomRPGMechanics
{
    class Item:IEntity
    {
        protected string name;
        protected string description;
        protected int count;
        double weight;
        double volume;
        int id;
        List<ItemAttribute> attributes = new List<ItemAttribute>();
        public Item(int id, string name, string description,double weight,double volume, int count=1)
        {
            this.name = name;
            this.description = description;
            this.count = count;
            this.id = id;
            this.weight = weight;
            this.volume = volume;
        }
        public Item(Item original)
        {
            name = original.name;
            description = original.description;
            count = original.count;
            weight = original.weight;
            volume = original.volume;
            id = original.id;
            attributes = original.attributes;
        }
        public virtual string GetEntityDescription()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(name+" x"+count);
            sb.AppendLine(description);
            sb.Append("Mass: ");sb.Append(weight.ToString());sb.Append(" Volume: "); sb.AppendLine(volume.ToString());
            if (attributes.Count > 0)
            {
                sb.AppendLine("Bonuses:");
                foreach (ItemAttribute attribute in attributes)
                    sb.AppendLine(attribute.ToString());
            }
            return sb.ToString();
        }
        public string GetName()
        {
            return name;
        }
        public int GetCount()
        {
            return count;
        }
        public int ReturnID()
        {
            return id;
        }
        public void IncreaseCount(int value)
        {
            count += value;
        }
        public void DecreaseCount(int value)
        {
            count -= value;
        }
        public int ReturnAbilityValue(int ID)
        {
            foreach(ItemAttribute ability in attributes)
            {
                if (ability.ID == ID)
                {
                    return ability.Value;
                }
            }
            return 0;
        }
        public void AddAbility(int ID,int value)
        {
            attributes.Add(new ItemAttribute(ID,value));
        }
    }
}
