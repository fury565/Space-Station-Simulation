using System;
using System.Collections.Generic;
using System.Text;

namespace JustSomeRandomRPGMechanics
{
    interface IInventory
    {
        public void Pickup(IEntity item);
        public void Pickup(List<IEntity> items);
        public void Drop(IEntity item);
        public void SelectItem(bool targetIsEnemy=false);
        public string GetContents();
        public List<IEntity> GetInventoryList();
    }
}
