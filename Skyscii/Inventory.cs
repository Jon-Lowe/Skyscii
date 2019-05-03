using System;
using System.Collections.Generic;
using System.Text;

namespace Skyscii
{
    public class Inventory : ISearchable
    {
        // will hold a list of items that the user can collect
        private List<Item> Bag;


        public Inventory()
        {
            Bag = new List<Item>();
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
            Bag.Remove(item);
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
    

    }
}
