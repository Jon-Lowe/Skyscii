using System;
using System.Collections.Generic;
using System.Text;

namespace Skyscii.SentientStuff
{
    /*
     * A class made to represent all living creatures in the game.
     * Actions include: move, attack, use item, pick up item, level up.
     * 
     * pending TODO: 'MOVE' -> in order to finish implementing this, 'room' must return the 'nextroom'
     * pending TODO: 'Winner' -> 'room' must return the 'nextroom', if null, winner = true.
     */
    public class Sentient : ITargetableObject, ISearchable
    {
        private Room location;
        private Stats stats;
        private Inventory inventory;
        private AI ai;

        private String name;
        private String description;

        // right now, hostile if AI controlled.
        public Sentient(String name, String description, int attack, int health, Room currentLocation) {
            this.name = name;
            this.description = description;

            this.stats = new Stats(attack, health, 20, 1);
            this.location = currentLocation;

            this.inventory = new Inventory();

            this.ai = new AI();
        }

        public Stats Stats { get { return stats; } set { stats = value; } }
        public Inventory Inventory { get { return inventory; } set { inventory = value; } }
        public AI AI { get { return ai; } set { ai = value; } }
        public Room Location { get { return location; } set { location = value; } }
        
        public ITargetableObject findTarget(string name)
        {
            // not searching the current room, as the room logic will search the sentient -> loops.
            if (this.name == name)
                return this;
            else
                return inventory.findTarget(name);
        }

        public string GetDescription()
        {
            return description;
        }

        public string GetName()
        {
            return name;
        }

        /*
         * increments the Sentient's stats based off the provided attack, health, experience
         */
        public void ApplyModifier(int attack, int health, int experience) {
            stats.Health.Increment(health);
            stats.Attack += attack;
            stats.Exp.Increment(experience);

            // perhaps return flavour text?
        }

        /*
         * applies level up logic to sentient, increasing its stats and its level by 1
         * @param attack: true if attack should be increased during level up
         * @param health: true if health should be increased during level up
         */
        public String LevelUp(bool attack, bool health) {
            String toReturn = "you don't have enough experience to level up yet";
            // this logic should be nicer in future.
            if (stats.Exp.GetPendingLevelUps() > 0) {
                toReturn = "You leveled up! ";

                int increaseHealthBy = 10 * stats.Exp.GetLevel();
                int increaseAttackBy = 1 + stats.Exp.GetLevel();
                if (attack){
                    stats.Attack += increaseAttackBy;
                    toReturn += "your attack increased by " + increaseAttackBy;
                }
                if (health){
                    stats.Health.SetMax(stats.Health.GetMax() + increaseHealthBy, true);
                    toReturn += "your health increased by " + increaseHealthBy;
                }
                stats.Exp.LevelUp();
            }
            return toReturn;
        }

        /*
         * searches room for target
         * if target is sentient, will attack them for current Sentient's stat.attack amount
         * if target is killed, will gain experience.
         */
        public String Attack(string targetName)
        {
            ITargetableObject enemy = location.findTarget(targetName);
            if (enemy == null) {
                return "You try to attack a " + targetName + " but couldn't find one!";
            }
            if (enemy is Sentient)
            {
                Sentient enemyCreature = (Sentient)enemy;
                enemyCreature.ApplyModifier(0, -1 * stats.Attack, 0);
                if (enemyCreature.IsAlive())
                    return "You attack " + targetName + " for " + stats.Attack + " points of damage!";
                else {
                    // TODO: lazy expgained, should be more fun
                    int expGained = 15 * enemyCreature.stats.Exp.GetLevel();
                    stats.Exp.Increment(expGained);
                    String toReturn =  "You attack " + targetName + " for " + stats.Attack + " points of damage, and slay them!\n" +
                        "You gain "+expGained+" experience points!";
                    if (stats.Exp.GetPendingLevelUps() > 0)
                        toReturn += " It looks like you can level up!";
                    return toReturn;
                }
            }
            else {
                return "You can't attack a defenseless " + targetName + "!";
            }
        }

        /*
         * searches the sentient's inventory for item 'itemName'
         * applies the item's modifier to the sentient
         * returns flavor text
         */
        public string UseItem(string itemName)
        {
            // search inventory for item
            ITargetableObject retrieved = inventory.findTarget(itemName);
            if (retrieved is Item) {
                Item itemToUse = (Item)retrieved;
                ApplyModifier(itemToUse.AttackOption, itemToUse.HealthOption, itemToUse.ExperienceOption);
                String toReturn = "You use the " + itemName;
                if (itemToUse.AttackOption == 0 && itemToUse.HealthOption == 0 && itemToUse.ExperienceOption == 0) {
                    return toReturn += "but nothing happens!";
                }
                if (itemToUse.AttackOption > 0) 
                    return toReturn += "and feel stronger";
                if (itemToUse.HealthOption > 0)
                    return toReturn += "and feel vitality surge through you";
                if (itemToUse.ExperienceOption > 0)
                    return toReturn += "and feel wiser";
            }
            return "You try to use a " + itemName + " but can't figure out how!";
        }

        /*
         * find the item 'itemName' from the player's surroundings (room)
         * adds it to the players inventory
         * 
         * current issues:
         * can take items directly from enemies
         * need to remove item from room's inventory (inventory.removeitem must be added)
         */
        public string PickUpItem(string itemName)
        {
            // TODO: ADD REMOVEITEM to INVENTORY
            ITargetableObject target = location.findTarget(itemName);
            if (target is Item)
            {
                Item pickingUp = (Item)target;
                // TODO: remove item from room's inventory!!!
                inventory.AddItem(pickingUp);
                return "you gleefully pick up the " + itemName;
            }
            else
                return "you try to pick up the " + itemName + "... it doesn't go very well.";
        }

        /*
         * returns true if Sentient is alive.
         */
        public bool IsAlive()
        {
            return (stats.Health.GetCurrent() > 0);
        }

        public String ExecuteAIAction() {
            if (ai == null) {
                return null;
            }
            return ai.generateResponse(this);
        }
    }
}
