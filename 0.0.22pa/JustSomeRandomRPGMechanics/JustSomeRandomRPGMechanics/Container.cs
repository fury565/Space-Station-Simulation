using System;
using System.Collections.Generic;
using System.Text;

namespace JustSomeRandomRPGMechanics
{
    class Container:Item
    {
        double weightCapacity;
        double volumeCapacity;
        List<Item> items;
        public Container(int id, string name, string description, double weight, double volume,double weightcap,double volumecap,int count = 1) : base(id, name, description, weight, volume, count)
        {
            weightCapacity = weightcap;
            volumeCapacity = volumecap;
            items = new List<Item>();
        }
        public void AddItem(Item item)
        {
            items.Add(item);
        }
        public void RemoveItem(Item item)
        {
            items.Remove(item);
        }
        public IReadOnlyCollection<Item> GetContents()
        {
            return items;
        }
        public override string GetEntityDescription()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.name + " x" + base.count);
            sb.AppendLine("Capacity:");
            sb.AppendLine("    Weight: " + weightCapacity + " Volume: " + volumeCapacity);
            return sb.ToString();
        }
    }
}
