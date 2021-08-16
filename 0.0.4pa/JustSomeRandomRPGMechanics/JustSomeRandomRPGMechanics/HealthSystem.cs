using System;
using System.Collections.Generic;
using System.Text;

namespace JustSomeRandomRPGMechanics
{
    class HealthSystem
    {
        public int CurrentHealth { get; private set; }
        public int Health { get; private set; }
        public int foodFullness { get; private set; }
        public int waterFullness { get; private set; }
        public int turnsTillFoodDrop { get; private set; }
        public int turnsTillWaterDrop { get; private set; }
        public int turnsTillHealed { get; private set; }
        public int healingValue { get; private set; }
        public bool ActiveMetabolism { get; private set; }
        public HealthSystem(int health,bool activeMetabolism)
        {
            CurrentHealth = health;
            Health = health;
            foodFullness = 100;
            waterFullness = 100;
            turnsTillFoodDrop = 50;
            turnsTillWaterDrop = 50;
            ActiveMetabolism = activeMetabolism;
            turnsTillHealed = 0;
            healingValue = 0;
        }
        public void Metabolism()
        {
            if (ActiveMetabolism)
            {
                turnsTillFoodDrop--;
                turnsTillWaterDrop--;
                if (turnsTillFoodDrop <= 0)
                {
                    foodFullness--;
                    turnsTillFoodDrop += 50;
                    if (foodFullness < 0)
                    {
                        CurrentHealth--;
                    }
                    else if (foodFullness > 85)
                    {
                        CurrentHealth=CurrentHealth < Health ? CurrentHealth+1 : Health;
                    }
                }
                if (turnsTillWaterDrop <= 0)
                {
                    waterFullness--;
                    turnsTillWaterDrop += 50;
                    if (waterFullness < 0)
                    {
                        CurrentHealth--;
                    }
                    else if (waterFullness > 85)
                    {
                        CurrentHealth = CurrentHealth < Health ? CurrentHealth+1 : Health;
                    }
                }
                if (turnsTillHealed > 0)
                {
                    RecoverHealth(healingValue);
                    if (CurrentHealth > Health)
                    {
                        CurrentHealth = Health;
                    }
                    turnsTillHealed--;
                }
            }
        }
        public void TakeDamage(int value)
        {
            CurrentHealth -= value;
        }
        public void RecoverHealth(int value)
        {
            CurrentHealth += value;
        }
        public void ApplyHealingItem(int TotalHealthHeal,int turnsTillHealed)
        {
            healingValue = TotalHealthHeal / turnsTillHealed;
            this.turnsTillHealed = turnsTillHealed;
        }
    }
}
