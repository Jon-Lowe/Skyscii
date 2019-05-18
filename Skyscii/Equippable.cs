using System;
using System.Collections.Generic;
using System.Text;
using Skyscii.SentientStuff;

namespace Skyscii
{
    public class Equippable : Item
    {
        bool equipped = false;
        Sentient equippedBy = null;

        // TODO: equippable should be in sentient's inventory for it to be equipped.

        public Equippable(string name, string description, int experience, int health, int attack) : base(name,
            description, experience, health, attack)
        {
            
        }

        /*
         * equips this equippable to the provided sentient
         */
        public string EquipMe(Sentient s)
        {
            // if the same target already has the item equipped
            if (equippedBy!= null && equippedBy.GetName() == s.GetName() && equipped) {
                return s.GetName() + " tried to equip the " + this.GetName() + " but already had it equipped!";
            }
            else{
                // if this was previously equipped by another creature,
                // unequip it from that creature first.
                if (equipped && equippedBy != null && (s != equippedBy)) {
                    UnequipMe(equippedBy);
                }

                String toReturn = s.GetName() + " equipped the " + this.GetName()+"\n";

                // modify sentient stats
                toReturn += s.ApplyModifier(AttackOption, HealthOption, ExperienceOption);

                equippedBy = s;
                equipped = true;

                return toReturn;
            }
        
        }

        public string UnequipMe(Sentient sentient)
        {
            // not equipped
            if (!equipped) {
                return sentient.GetName() + " tried to unequip the " + this.GetName() + " but didn't have it equipped!";
            }
            // equipped, but by another creature
            if (sentient != equippedBy)
            {
                return sentient.GetName() + "tried to unequip the " + this.GetName()
                    + " but it was already equipped by " + equippedBy.GetName();
            }
            if (equipped && sentient == equippedBy) {
                equipped = false;
                sentient.ApplyModifier(-1 * AttackOption, -1 * HealthOption, -1 * ExperienceOption);
                return sentient.GetName() + " unequipped the " + GetName();
            }
            return "corner case not considered: unequip "+GetName()+" by "+sentient.GetName();
        }
    }
}
