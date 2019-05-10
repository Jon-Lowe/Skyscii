using System;
using System.Collections.Generic;
using System.Text;

namespace Skyscii
{
    // consumable item logic can go here
    public class Item : ITargetableObject
    {

        // Makes the Property Constant
        public string Name { get; }
        public string Description { get; }

        private int Attack;
        private int Health;
        private int Experience;


        public Item(string name, string description, int experience, int health, int attack)
        {
            Name = name;
            Description = description;
            Attack = attack;
            Experience = experience;
            Health = health;
        }


        /// <summary>
        /// Attack value of the item can only be set to a number greater then 0
        /// </summary>
        public int AttackOption
        {
            get { return Attack; }
            set
            {
                // prevent value from being less the 0
                if (value > 0) 
                    Attack = value;
            }
        }

        /// <summary>
        /// Health Value of Item will only accept numbers
        /// </summary>
        public int HealthOption
        {
            get { return Health;  }
            set
            {
                if (value > 0)
                {
                        Health = value;
                }
            }
        }

        /// <summary>
        /// Experience Value of Item will only accept numbers
        /// </summary>
        public int ExperienceOption
        {
            get { return Experience; }
            set
            {
                if (value > 0)
                {
                    Experience = value;
                }
            }
        }

        /// <summary>
        /// Get Item Description
        /// </summary>
        /// <returns></returns>
        public string GetDescription()
        {
            return Description;
        }


        /// <summary>
        /// Get Item Name
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return Name;
        }






    }
}
