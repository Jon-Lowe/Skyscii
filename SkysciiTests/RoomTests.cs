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
        public void findTargetTest(){
            Room testRoom = new Room("testRoom", "testRoom", new List<SentientStuff.Sentient>(), new Inventory());
            testRoom.Items.AddItem(new Item("testItem", "testItem", 1, 1, 1));
            Assert.IsInstanceOfType(testRoom.findTarget("testItem"), typeof(ITargetableObject));
            Assert.IsNull(testRoom.findTarget("nothing"));
        }
    }
}