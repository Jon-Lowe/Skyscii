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
    public class RoomTests{

        //TODO: Add test for Sentient when implemented
        [TestMethod()]
        public void findTargetItemTest()
        {
            Room testRoom = new Room("testRoom", "testRoom", new List<SentientStuff.Sentient>(), new Inventory());
            testRoom.Items.AddItem(new Item("testItem", "testItem", 1, 1, 1));
            Assert.IsNotNull(testRoom.findTarget("testItem"));
            Assert.IsInstanceOfType(testRoom.findTarget("testItem"), typeof(ITargetableObject));
        }

        [TestMethod()]
        public void findTargetSentientTest()
        {
            Room testRoom = new Room("testRoom", "testRoom", new List<SentientStuff.Sentient>(), new Inventory());
            SentientStuff.Sentient player = new SentientStuff.Sentient("player", "it's you!", 20, 30, testRoom);
            testRoom.Creatures.Add(player);
            Assert.IsNotNull(testRoom.findTarget("player"));
            Assert.IsInstanceOfType(testRoom.findTarget("player"), typeof(ITargetableObject));
        }

        [TestMethod()]
        public void findTargetNotExistTest()
        {
            Room testRoom = new Room("testRoom", "testRoom", new List<SentientStuff.Sentient>(), new Inventory());
            Assert.IsNull(testRoom.findTarget("nothing"));
        }
    }
}