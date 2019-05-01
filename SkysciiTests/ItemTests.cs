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
    public class ItemTests
    {
        // The Mighty Spoon will always be our test item :D, because no every suspects the spoons.


        [TestMethod()]
        public void ShouldReturnItemName()
        {
            Item spoon = new Item("Mighty Spoon", "Spoon them to death !!!!!", 0, 0, 0);
            Assert.AreEqual("Mighty Spoon", spoon.GetName());
        }


        [TestMethod()]
        public void ShouldModifyAndRetunAttackValue()
        {
            Item spoon = new Item("Mighty Spoon", "Spoon them to death !!!!!", 0, 0, 0);

            spoon.AttackOption = 200;

            Assert.AreEqual(200, spoon.AttackOption);
        }

        [TestMethod()]
        public void ShouldModifyAndReturnExperienceValue()
        {
            Item spoon = new Item("Mighty Spoon", "Spoon them to death !!!!!", 0, 0, 0);
            spoon.ExperienceOption = 200;

            Assert.AreEqual(200, spoon.ExperienceOption);
        }

        [TestMethod()]
        public void ShouldModifyAndReturnHealthValue()
        {
            Item spoon = new Item("Mighty Spoon", "Spoon them to death !!!!!", 0, 0, 0);
            spoon.HealthOption = 200;

            Assert.AreEqual(200, spoon.HealthOption);
        }



    }
}
