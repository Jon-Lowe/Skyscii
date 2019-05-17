using System;
using System.Collections.Generic;
using System.Text;

namespace Skyscii
{
    public class Inventory : ISearchable
    {
        // will hold a list of items that the user can collect
        private List<Item> Bag;
        private int drakeCrests;


        public Inventory()
        {
            Bag = new List<Item>();
            drakeCrests = 0;
        }

        public Inventory(int StartingCrests) : this()
        {
            drakeCrests = StartingCrests;
        }


        public List<Item> GetBag
        {
            // returns the item holder
                get => Bag;
        }


        // add item to Item holder
        public void AddItem(Item item)
        {
            Bag.Add(item);
        }


        // remove item from the Item holder
        public void RemoveItem(Item item)
        {
            if (findTarget(item.Name) != null)
            {
                Bag.Remove(item);
            }
        }

        // lets use the interface
        public ITargetableObject findTarget(string name)
        {
            // search items for name? 
            foreach (var item in Bag)
            {
                if (item.GetName() == name)
                {
                    return item;
                }
            }

            // if we cannot find item return null
            return null;

        }

        public int CrestCount
        {
            get => drakeCrests;
        }

        //Function makes sure you're adding a positive number
        public void AddCrests(int amount)
        {
            if (amount > 0)
                drakeCrests += amount;
        }

        //Makes sure you're subtracting a positive number
        //If ammount is positive, subtracts from total drakecrests if you can afford to
        /* 
         * @param: amount: amount to remove from total crests
         * @returns: true if successful, false if cannot afford
         */
        public bool RemoveCrests(int amount)
        {
            if (amount < 0 || ((drakeCrests - amount) < 0))
                return false;

            drakeCrests -= amount;
            return true;
        }
    }
}
