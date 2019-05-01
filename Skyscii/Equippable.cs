using System;
using System.Collections.Generic;
using System.Text;

namespace Skyscii
{
    public class Equippable : Item
    {
        public Equippable(string name, string description, int experience, int health, int attack) : base(name,
            description, experience, health, attack)
        {
            // Values will be pushed up to parent class
        }
    }
}
