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
            Item spoon1 = new Item("Mighty Spoon1", "Spoon them to death !!!!!", 0, 0, 0);


            // Remove the Item 
            bag.AddItem(spoon0); 
            bag.RemoveItem(spoon0);

            Assert.AreEqual(null, bag.findTarget("Mighty Spoon0"));

            // Validation should allow us to pass an incorrect items without crashing the program
            bag.AddItem(spoon0);
            bag.RemoveItem(spoon1);

            Assert.AreEqual(spoon0, bag.findTarget("Mighty Spoon0"));
        }

        [TestMethod()]
        public void ShouldInitialiseCrestsToZero()
        {
            Inventory wallet = new Inventory();

            Assert.AreEqual(0, wallet.CrestCount());
        }

        [TestMethod()]
        public void ShouldInitialiseCrestsToCount()
        {
            Inventory wallet = new Inventory(10);

            Assert.AreEqual(10, wallet.CrestCount());
        }

        [TestMethod()]
        public void AddCrestsShouldWork()
        {
            Inventory wallet = new Inventory();

            wallet.AddCrests(10);

            Assert.AreEqual(0+10, wallet.CrestCount());
        }

        [TestMethod()]
        public void AddCrestsShouldNeverSubractCrests()
        {
            Inventory wallet = new Inventory(20);

            wallet.AddCrests(-10);

            Assert.AreEqual(20, wallet.CrestCount());
        }

        [TestMethod()]
        public void RemoveCrestsShouldWork()
        {
            Inventory wallet = new Inventory(20);

            wallet.RemoveCrests(10);

            Assert.AreEqual(20-10, wallet.CrestCount());
        }

        [TestMethod()]
        public void RemoveCrestsShouldNeverAddCrests()
        {
            Inventory wallet = new Inventory(20);

            wallet.RemoveCrests(-10);

            Assert.AreEqual(20, wallet.CrestCount());
        }



    }
}
