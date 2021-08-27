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
        int move_speed;
        int sight_radius;
        string name;
        protected int posx;
        protected int posy;
        int id;
        string description;
        int aggroState;
        public Path currentPath;
        IInventory inventory;
        UsableItem weapon=new UsableItem((UsableItem)(Item)BaseNonTargettableEntityCollection.GetLootAtIndex(13));
        public LiveTarget(int id,string name,int maxhp,int str,int dex,int intelligence,int per,int nat_armor,int move_speed,int sight_radius,int posx=0,int posy=0)
        {
            this.name = name;
            needs = new HealthSystem(maxhp, false);
            this.str = str;
            this.dex = dex;
            this.intelligence = intelligence;
            this.per = per;
            this.nat_armor = nat_armor;
            this.move_speed = move_speed;
            this.sight_radius = sight_radius;
            this.posx = posx;
            this.posy = posy;
            this.id = id;
            aggroState = 0;
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
            move_speed = original.move_speed;
            sight_radius = original.sight_radius;
            name = original.name;
            id = original.id;
            this.posx = posx;
            this.posy = posy;
            aggroState = 0;
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
        public int MoveSpeed
        {
            get
            {
                return move_speed;
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
        public int GetSightRadius()
        {
            return sight_radius;
        }
        public int GetAggroState()
        {
            return aggroState;
        }
        public void SetAgressive()
        {
            aggroState = 1;
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
                currentPath = null;
            }
            else if (currentPath != null)
            {
                if (currentPath.PathLeftToFollow())
                {
                    Distance temp = currentPath.FollowPath();
                    PerformMovement(temp.X, temp.Y);
                }
                else
                    currentPath = null;
            }
            else
            {
                Display.DisplayDebugMessage("Random move");
                PerformMovement(xdistance,ydistance);
            }
        }
        protected void PerformMovement(int xdistance, int ydistance)
        {
            
            Map currentlevel = MapLevelTracker.GetMapLevel(0);
            int targetx = posx+xdistance;
            int targety = posy+ydistance;
            Player player = Player.GetPlayer();
            if (player.PosX == targetx && player.PosY == targety)
            {
                Attack(player);
                return;
            }
            if (targety < 0)
                targety = 0;
            else if (targety >= currentlevel.SizeY)
                targety = currentlevel.SizeY - 1;
            if (targetx < 0)
                targetx = 0;
            else if (targetx >= currentlevel.SizeX)
                targetx = currentlevel.SizeX - 1;
            Structure tempStruct = MapLevelTracker.GetStructureTracker().FindStructureWithComponentCoordinates(targetx, targety);
            if (tempStruct != null)
            {
                if (!tempStruct.designComponents[tempStruct.ReturnIndexOfComponentAtLocation(targetx, targety)].GetTileDetails().Passable)
                {
                    if (tempStruct.designComponents[tempStruct.ReturnIndexOfComponentAtLocation(targetx, targety)].GetTileDetails().Interactable)
                    {
                        tempStruct.ActivateComponent(tempStruct.ReturnIndexOfComponentAtLocation(targetx, targety));
                        MapLevelTracker.displayed = false;
                    }
                }
                else
                {
                    posx = targetx;
                    posy = targety;
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
            else if (currentlevel.GetTileAtLocation(targetx, targety).GetTileDetails().Passable&& MapLevelTracker.GetNPCTracker().GetNPCatLocation(targetx, targety) is NullTarget)
            {

                posx = targetx;
                posy = targety;
            }
        }
        public void Attack(LiveTarget target)
        {
            weapon.UseItem(target, inventory);
        }
        public void CheckSurroundings()
        {
            if (Math.Sqrt(Math.Pow(Math.Abs(Player.GetPlayer().PosX - posx), 2) + Math.Pow(Math.Abs(Player.GetPlayer().PosY - posy), 2)) <= sight_radius)
            {
                aggroState = 1;
            }
            else
                aggroState = 0;
        }
    }
}
