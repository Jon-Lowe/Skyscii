using System;
using System.Collections.Generic;
using System.Text;
using Skyscii.SentientStuff;

namespace Skyscii
{
    public class Equippable : Item
    {
        public Equippable(string name, string description, int experience, int health, int attack) : base(name,
            description, experience, health, attack)
        {
            // Values will be pushed up to parent class

        }

        public string EquipMe(Sentient s)
        {
            throw new NotImplementedException();
        }

        public string UnequipMe(Sentient s)
        {
            throw new NotImplementedException();
        }
    }
}
