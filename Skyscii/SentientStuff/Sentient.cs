using System;
using System.Collections.Generic;
using System.Text;

namespace Skyscii.SentientStuff
{
    public class Sentient : ITargetableObject, ISearchable
    {
        private Room location;
        private Stats stats;
        private Inventory inventory;
        private AI ai;

        String name;
        String description;

        // right now, hostile if AI controlled.
        public Sentient(String name, String description) {
            this.name = name;
            this.description = description;
        }

        public Stats Stats { get { return stats; } set { stats = value; } }
        public Inventory Inventory { get { return inventory; } set { inventory = value; } }
        public AI AI { get { return ai; } set { ai = value; } }
        public Room Location { get { return location; } set { location = value; } }

        public ITargetableObject findTarget(string name)
        {
            throw new NotImplementedException();
        }

        public string GetDescription()
        {
            throw new NotImplementedException();
        }

        public string GetName()
        {
            throw new NotImplementedException();
        }

        public String Attack(string targetName)
        {
            throw new NotImplementedException();
        }

        public string UseItem(string v)
        {
            throw new NotImplementedException();
        }

        public string PickUpItem(string v)
        {
            throw new NotImplementedException();
        }

        public bool IsAlive()
        {
            throw new NotImplementedException();
        }
    }
}
