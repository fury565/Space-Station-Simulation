using System;
using System.Collections.Generic;
using System.Text;

namespace JustSomeRandomRPGMechanics
{
    class LiveTarget:Targettable,IEntity
    {
        protected HealthSystem needs;
        int str;
        int dex;
        int intelligence;
        int per;
        int nat_armor;
        string name;
        protected int posx;
        protected int posy;
        int id;
        string description;
        int aggroState;
        IInventory inventory;
        UsableItem weapon=new UsableItem((UsableItem)(Item)BaseNonTargettableEntityCollection.GetLootAtIndex(13));
        public LiveTarget(int id,string name,int maxhp,int str,int dex,int intelligence,int per,int nat_armor,int posx=0,int posy=0)
        {
            this.name = name;
            needs = new HealthSystem(maxhp, false);
            this.str = str;
            this.dex = dex;
            this.intelligence = intelligence;
            this.per = per;
            this.nat_armor = nat_armor;
            this.posx = posx;
            this.posy = posy;
            this.id = id;
            aggroState = 1;
            inventory = new Inventory();
        }
        public LiveTarget(LiveTarget original, int posx=0, int posy=0)
        {
            needs = new HealthSystem(original.MaxHP, false);
            str = original.str;
            dex = original.dex;
            intelligence = original.intelligence;
            per = original.per;
            nat_armor = original.nat_armor;
            name = original.name;
            id = original.id;
            this.posx = posx;
            this.posy = posy;
            aggroState = 1;
            inventory = new Inventory();
        }
        public int HP
        {
            get{
                return needs.CurrentHealth;
            }
        }
        public int MaxHP
        {
            get
            {
                return needs.Health;
            }
        }
        public string GetName()
        {
            return name;
        }
        public int PosX
        {
            get
            {
                return posx;
            }
        }
        public int PosY
        {
            get
            {
                return posy;
            }
        }
        public int ReturnID()
        {
            return id;
        }
        public string GetEntityDescription()
        {
            return description;
        }
        public int GetCount()
        {
            return 1;
        }
        public int GetAggroState()
        {
            return aggroState;
        }
        public IInventory GetInventory()
        {
            return inventory;
        }
        public virtual void TakeBashHit(int numValue)
        {
            int finalDamage = numValue;
            finalDamage -= nat_armor + Convert.ToInt32(finalDamage * 0.3);
            needs.TakeDamage(finalDamage);
            Display.DisplayDebugMessage("Entity took damage: "+finalDamage.ToString());
            Die();
        }
        public virtual void TakePierceHit(int numValue)
        {
            int finalDamage = numValue;
            finalDamage -= Convert.ToInt32(nat_armor /2) + Convert.ToInt32(finalDamage * 0.1);
            needs.TakeDamage(finalDamage);
            Display.DisplayDebugMessage("Entity took damage: " + finalDamage.ToString());
            Die();
        }
        private void Die()
        {
            if (needs.CurrentHealth <= 0)
            {
                MapLevelTracker.GetMapLevel(0).GetTileAtLocation(PosX, PosY).AddContent(inventory.GetInventoryList());
                inventory = null;
                MapLevelTracker.GetNPCTracker().RemoveNPC(this);
            }
        }
        public void RecoverHealth(int healingValue,int turnsToHeal)
        {
            needs.ApplyHealingItem(healingValue, turnsToHeal);
        }
        public virtual void Move(int xdistance, int ydistance)
        {
            Player player = Player.GetPlayer();
            if (player.PosX==posx+xdistance && player.PosY==posy+ydistance)
            {
                Attack(player);
            }
            else
            {
                PerformMovement(xdistance,ydistance);
            }
        }
        protected void PerformMovement(int xdistance, int ydistance)
        {
            Map currentlevel = MapLevelTracker.GetMapLevel(0);
            int oldx = posx;
            int oldy = posy;
            posx += xdistance;
            posy += ydistance;
            if (posy < 0)
                posy = 0;
            else if (posy >= currentlevel.SizeY)
                posy = currentlevel.SizeY - 1;
            if (posx < 0)
                posx = 0;
            else if (posx >= currentlevel.SizeX)
                posx = currentlevel.SizeX - 1;
            if (!currentlevel.GetTileAtLocation(posx, posy).GetTileDetails().Passable)
            {
                if(currentlevel.GetTileAtLocation(posx, posy).GetTileDetails().Interactable)
                {
                    if(currentlevel.GetTileAtLocation(posx, posy).GetTileDetails().Effect == "Close")
                    {
                        MapLevelTracker.GetMapLevel(0).ChangeAtLocation(posx, posy, TileCreator.ReturnTypeWithName(currentlevel.GetTileAtLocation(posx, posy).GetTileDetails().Name.Replace("open", "closed")).mapTag);
                    }
                    else if (currentlevel.GetTileAtLocation(posx, posy).GetTileDetails().Effect == "Open")
                    {
                        MapLevelTracker.GetMapLevel(0).ChangeAtLocation(posx, posy, TileCreator.ReturnTypeWithName(currentlevel.GetTileAtLocation(posx, posy).GetTileDetails().Name.Replace("closed", "open")).mapTag);
                    }
                    MapLevelTracker.displayed = false;
                }
                posx = oldx;
                posy = oldy;
                
            }
            /*else//effects when entity steps on a tile
            {
                if (currentlevel.GetTileAtLocation(posx, posy).GetTileDetails().Interactable)
                {
                    if (currentlevel.GetTileAtLocation(posx, posy).GetTileDetails().Effect == "Close")
                    {
                        MapLevelTracker.GetMapLevel(0).ChangeAtLocation(posx, posy, TileCreator.ReturnTypeWithName(currentlevel.GetTileAtLocation(posx, posy).GetTileDetails().Name.Replace("open", "closed")).mapTag);
                    }
                    else if (currentlevel.GetTileAtLocation(posx, posy).GetTileDetails().Effect == "Open")
                    {
                        MapLevelTracker.GetMapLevel(0).ChangeAtLocation(posx, posy, TileCreator.ReturnTypeWithName(currentlevel.GetTileAtLocation(posx, posy).GetTileDetails().Name.Replace("closed", "open")).mapTag);
                    }
                }
            }*/
        }
        public void Attack(LiveTarget target)
        {
            weapon.UseItem(target, inventory);
        }
        
    }
}
