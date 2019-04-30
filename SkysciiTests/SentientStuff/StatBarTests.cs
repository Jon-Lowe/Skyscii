using Microsoft.VisualStudio.TestTools.UnitTesting;
using Skyscii.SentientStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscii.SentientStuff.Tests
{
    [TestClass()]
    public class StatBarTests
    {
        [TestMethod()]
        public void IncrementingShouldAffectCurrent(){
            StatBar s = new StatBar(10);
            s.Increment(-5);

            int expectedCurrent = 10 - 5;
            Assert.AreEqual(expectedCurrent, s.GetCurrent());
        }

        [TestMethod()]
        public void IncrementingShouldNotIncreaseMaximum(){
            StatBar s = new StatBar(10);
            s.Increment(-5);

            int expectedMax = 10;
            Assert.AreEqual(expectedMax, s.GetMax());
        }

        [TestMethod()]
        public void CurrentShouldNotExceedMaxIfIncremented() {
            StatBar s = new StatBar(10);
            s.Increment(5);

            int expectedCurrent = 10;
            Assert.AreEqual(expectedCurrent, s.GetCurrent());
        }

        [TestMethod()]
        public void SettingMaxWithTrueShouldResetCurrentAndMax() {
            StatBar s = new StatBar(10);
            s.SetMax(20, true);
            // current should be updated to equal new max, 10
            Assert.AreEqual(20, s.GetCurrent());
        }

        [TestMethod()]
        public void SettingMaxWithFalseShouldNotChangeCurrent() {
            StatBar s = new StatBar(10);
            s.SetMax(20, false);
            // current should remain as its former value, 10
            Assert.AreEqual(10, s.GetCurrent());
        }
    }
}