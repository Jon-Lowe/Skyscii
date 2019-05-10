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
        public void IncrementingShouldIncreaseCurrent(){
            StatBar s = new StatBar(10);
            s.Increment(-5); // incrementing negative will subtract

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
            Assert.AreEqual(20, s.GetMax());
        }

        [TestMethod()]
        public void SettingMaxWithFalseShouldNotChangeCurrent() {
            StatBar s = new StatBar(10);
            s.SetMax(20, false);
            // current should remain as its former value, 10
            Assert.AreEqual(10, s.GetCurrent());
            Assert.AreEqual(20, s.GetMax());
        }

        // current should never be greater than max, so if max is reduced to be smaller than current
        // current should be reduced to equal max.
        [TestMethod()]
        public void SettingMaxToBeSmallerThanCurrentWillReduceCurrent() {
            StatBar s = new StatBar(10);
            s.SetMax(5, false);

            Assert.AreEqual(5, s.GetMax());
            Assert.AreEqual(5, s.GetCurrent(), "current should not be greater than max.");
        }

        [TestMethod()]
        public void canSetCurrentCorrectly() {
            StatBar s = new StatBar(10);
            s.SetCurrent(8);

            Assert.AreEqual(8, s.GetCurrent());
            Assert.IsFalse(s.IsFull());
        }

        [TestMethod()]
        public void IsFullReturnsFalseIfCurrentIsLessThanMax() {
            StatBar s = new StatBar(10);
            s.Increment(-1);
            Assert.IsFalse(s.IsFull());
        }

        [TestMethod()]
        public void IsFullReturnsTrueIfCurrentIsEqualToMax() {
            StatBar s = new StatBar(10);
            Assert.IsTrue(s.IsFull());
        }
    }
}