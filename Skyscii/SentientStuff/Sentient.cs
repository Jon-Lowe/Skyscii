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
         * returns true if sentient is the last one left alive.
         */
        public bool LastOneStanding() {
            if (location.Creatures.Count == 1)
                return true;

            foreach (Sentient s in location.Creatures) {
                if (s != this && s.IsAlive()) {
                    return false;
                }
            }
            return true;
        }

        /*
         * increments the Sentient's stats based off the provided attack, health, experience
         */
        public String ApplyModifier(int attack, int health, int experience) {
            stats.Health.Increment(health);
            stats.Attack += attack;
            stats.Exp.Increment(experience);

            String toReturn = "";
            if (attack > 0)
                toReturn += "You feel stronger.\n";
            if (attack < 0)
                toReturn += "You feel weaker\n";
            if (health > 0)
                toReturn += "You feel vitality surge through you.\n";
            if (health < 0)
                toReturn += "You feel cold...\n";
            if (experience > 0)
                toReturn += "You feel wiser, somehow.\n";
            if (experience < 0)
                toReturn += "You feel some vital knowledge slipping away\n";
            if (experience == 0 && health == 0 && attack == 0)
                toReturn += "But nothing changed.\n";
            return toReturn;
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
                return name + " tries to attack a " + targetName + " but couldn't find one!";
            }
            if (enemy is Sentient)
            {
                Sentient enemyCreature = (Sentient)enemy;
                enemyCreature.ApplyModifier(0, -1 * stats.Attack, 0);
                if (enemyCreature.IsAlive())
                    return name + " attacks " + targetName + " for " + stats.Attack + " points of damage!";
                else {
                    // TODO: lazy expgained, should be more fun
                    int expGained = 20 * enemyCreature.stats.Exp.GetLevel();
                    stats.Exp.Increment(expGained);
                    String toReturn =  name +" attacks " + targetName + " for " + stats.Attack + " points of damage, and slays them!\n" +
                        name + " gains "+expGained+" experience points!";
                    if (stats.Exp.GetPendingLevelUps() > 0)
                        toReturn += " It looks like " + name + " can level up!";
                    return toReturn;
                }
            }
            else {
                return "You can't attack a defenseless " + targetName + "!";
            }
        }

        /*
         * searches the sentient's inventory for an item with itemname 
         * if it exists and is an Equippable, will attempt to equip it
         */
        public String EquipItem(string itemName)
        {
            ITargetableObject retrieved = inventory.findTarget(itemName);
            if (retrieved == null)
                return "You try to equip a "+itemName+" but cannot find one in your inventory!";
            if (retrieved is Equippable)
            {
                Equippable toEquip = (Equippable)retrieved;
                return toEquip.EquipMe(this);
            }
            else
                return "You try to equip a " + itemName + " but can't figure out how!";
        }

        public String UnequipItem(string itemName) {
            ITargetableObject retrieved = inventory.findTarget(itemName);
            if (retrieved == null)
                return "You try to unequip a " + itemName + " but cannot find one in your inventory!";
            if (retrieved is Equippable)
            {
                Equippable toEquip = (Equippable)retrieved;
                return toEquip.UnequipMe(this);
            }
            else
                return "You try to unequip a " + itemName + " but can't figure out how!";
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
            if (retrieved == null) {
                return "You try to use a " + itemName + " but couldn't find one!";
            }
            if (retrieved is Item) {
                Item itemToUse = (Item)retrieved;

                // consume item if it is not equippable
                if (!(itemToUse is Equippable)) {
                    inventory.RemoveItem(itemToUse);

                    String toReturn = "You use the " + itemName+"\n";

                    toReturn += ApplyModifier(itemToUse.AttackOption, itemToUse.HealthOption, itemToUse.ExperienceOption);
                    
                    if (itemToUse.AttackOption == 0 && itemToUse.HealthOption == 0 && itemToUse.ExperienceOption == 0)
                    {
                        return toReturn += "but nothing happens!";
                    }
                    return toReturn;
                }
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

                // add item to play inventory
                inventory.AddItem(pickingUp);

                // remove item from room inventory
                location.Items.RemoveItem(pickingUp);
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

        /*
         * Updates Sentient's location to be the current location's nextRoom, if possible.
         * removes the player from the old location's sentient list
         * adds the player to the new location's sentient list
         */
        public string MoveToNextRoom()
        {
            if (location.NextRoom == null)
                return "You try to go to the next room, but you're at the end of the dungeon!";
            else {
                this.location.Creatures.Remove(this);
                this.location = location.NextRoom;
                this.location.Creatures.Add(this);
                return "You leave your current room, moving forward into the " + location.GetName();
            }
        }

        /*
         * returns true if another room remains to explore
         */
        public bool NextRoomRemains() {
            return location.NextRoom != null;
        }
    }
}
