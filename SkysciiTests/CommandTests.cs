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
    public class CommandTests
    {
        /*
            Considering gameplay flow and resulting commands:
            main menu:
            new, quit

            battle menu:
            attack goblin
            examine goblin
            inventory

            inventory menu:
            use potion
            equip sword
            use fire scroll
            throw bomb

            level up menu:
            (choosing which stat to upgrade)
            strength
            health

            ie, single word command with or without single/multi-word target
            all patterns from above are addressed in the below tests:

             */

        [TestMethod()]
        public void ShouldRetrieveCommandCorrectlyIfNoTarget(){
            Command c = new Command("continue");
            Assert.AreEqual("continue", c.GetAction());
        }

        [TestMethod()]
        public void ShouldDivideCommandCorrectlyFromSingleWordTarget() {
            Command c = new Command("attack goblin");
            Assert.AreEqual("attack", c.GetAction());
            Assert.AreEqual("goblin", c.GetTarget());
        }

        [TestMethod()]
        public void ShouldDivideCommandCorrectlyFromMultiWordTarget() {
            Command c = new Command("use red potion");
            Assert.AreEqual("use", c.GetAction());
            Assert.AreEqual("red potion", c.GetTarget());
        }

        // limitation: 
        // white space in target will cascade into command class. 
        // eg "use fire     scroll" -> target = "fire     scroll"
        [TestMethod()]
        public void WhitespaceShouldNotDisruptCommandLogic() {
            Command c = new Command("   use   fire scroll  ");
            Assert.AreEqual("use", c.GetAction());
            Assert.AreEqual("fire scroll", c.GetTarget());
        }
    }
}