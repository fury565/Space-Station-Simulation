using System;
using System.Collections.Generic;
using System.Text;

namespace JustSomeRandomRPGMechanics
{
    class Inventory:IInventory
    {
        List<IEntity> inventory = new List<IEntity>();
        public List<IEntity> GetInventoryList()
        {
            return inventory;
        }
        public void Pickup(IEntity item)
        {
            int flag = 0;
            foreach(IEntity e in inventory)
            {
                if (e.GetName() == item.GetName())
                {
                    ((Item)e).IncreaseCount(item.GetCount());
                    flag = 1;
                    break;
                }
            }
            if (flag == 0)
                inventory.Add(item);
        }
        public void Pickup(List<IEntity> loot)
        {
            foreach (IEntity item in loot)
                Pickup(item);
        }
        public void Drop(IEntity item)
        {
            for(int i=inventory.Count-1;i>=0;i--)
            {
                if (item == inventory[i])
                {
                    ((Item)inventory[i]).DecreaseCount(1);
                    if(inventory[i].GetCount()==0)
                        inventory.Remove(inventory[i]);
                }
            }
            
        }
        public string GetContents()
        {
            StringBuilder sb = new StringBuilder();
            foreach(var e in inventory)
            {
                sb.AppendLine(e.GetName()+" x"+e.GetCount());
            }
            return sb.ToString();
        }
        public void SelectItem(bool targetIsEnemy=false)
        {
            int counter = 0;
            while (true)
            {
                Console.Clear();
                Display.DisplayMessage(GetContents());
                //Display.DisplayMessage(inventory[counter].GetLootDescription());   Need to create method and attribute for actual item description
                Console.SetCursorPosition(30-2, 0);//create method that searches biggest item name
                Display.DisplayMessage("<");
                ConsoleKeyInfo pressed = Console.ReadKey();
                if (pressed.Key == ConsoleKey.DownArrow)
                {
                    counter++;
                    if (counter == inventory.Count)
                        counter = 0;
                }
                    
                else if (pressed.Key == ConsoleKey.UpArrow)
                {
                    counter--;
                    if (counter == -1)
                        counter = inventory.Count - 1;
                }
                else if (pressed.Key == ConsoleKey.Enter)
                {
                    if (inventory[counter] is UsableItem)
                    {
                        ((UsableItem)inventory[counter]).UseItem(Player.GetPlayer(), this);
                        break;
                    }
                    
                }
            }
            
        }
    }
}
