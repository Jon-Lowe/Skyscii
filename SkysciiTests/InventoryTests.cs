using Microsoft.VisualStudio.TestTools.UnitTesting;
using Skyscii;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscii.Tests
{
    [TestClass()]
    public class InventoryTests
    {
        // The Mighty Spoon will always be our test item :D, because no every suspects the spoons.
        [TestMethod()]
        public void ShouldReturnCorrectNumberOfItems()
        {
            Inventory bag = new Inventory();

            Item spoon0 = new Item("Mighty Spoon0", "Spoon them to death !!!!!", 0, 0, 0);
            Item spoon1 = new Item("Mighty Spoon1", "Spoon them to death !!!!!", 0, 0, 0);


            bag.AddItem(spoon0);
            bag.AddItem(spoon1);


            Assert.AreEqual(2, bag.GetBag.Count);
        }

        [TestMethod()]
        public void ShouldReturnItem()
        {
            Inventory bag = new Inventory();

            Item spoon0 = new Item("Mighty Spoon0", "Spoon them to death !!!!!", 0, 0, 0);
            Item spoon1 = new Item("Mighty Spoon1", "Spoon them to death !!!!!", 0, 0, 0);

            bag.AddItem(spoon0);
            bag.AddItem(spoon1);

            Assert.AreEqual(spoon0, bag.findTarget("Mighty Spoon0"));
        }

        [TestMethod()]
        public void ShouldRemoveItemFromInventory()
        {
            Inventory bag = new Inventory();

            Item spoon0 = new Item("Mighty Spoon0", "Spoon them to death !!!!!", 0, 0, 0);

            bag.AddItem(spoon0); 
            bag.RemoveItem(spoon0);

            Assert.AreEqual(null, bag.findTarget("Mighty Spoon0"));
        }


    }
}
