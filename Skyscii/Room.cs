using System;
using System.Collections.Generic;
using System.Text;
using Skyscii.SentientStuff;

namespace Skyscii
{
    public class Room : ISearchable, ITargetableObject
    {
        private string name;
        private string description;
        private Room nextRoom;
        private List<Sentient> creatures;
        private Inventory items;

        public Room(string name, string description, List<Sentient> creatures, Inventory items)
        {
            this.name = name;
            this.description = description;
            this.creatures = creatures;
            this.items = items;
        }

        public Inventory Items { get => items; }
        public List<Sentient> Creatures { get => creatures; }
        public Room NextRoom { get => nextRoom; set => nextRoom = value; }

        public ITargetableObject findTarget(string name)
        {
            ITargetableObject result = items.findTarget(name);
            if (result == null)
            {
                foreach (Sentient s in creatures)
                {
                    if (!s.IsAlive())
                    {
                        creatures.Remove(s);
                    }
                }

                foreach (Sentient s in creatures)
                {
                    if (s.GetName() == name)
                    {
                        result = s;
                    }
                }
            }
            return result;
        }

        public string GetDescription()
        {
            return name;
        }

        public string GetName()
        {
            return description;
        }
    }
}
