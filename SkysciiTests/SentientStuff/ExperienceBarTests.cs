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
    public class ExperienceBarTests
    {
        [TestMethod()]
        public void ShouldAddPendingLevelUpIfExperienceHitsMax(){
            ExperienceBar e = new ExperienceBar(20, 1);
            e.Increment(20);
            Assert.AreEqual(1, e.GetPendingLevelUps());
        }

        [TestMethod()]
        public void ShouldEmptyExperienceBarOnReachingLevelUp() {
            ExperienceBar e = new ExperienceBar(20, 1);
            e.Increment(20);
            Assert.AreEqual(0, e.GetCurrent());
        }

        [TestMethod()]
        public void LevellingUpShouldReducePendingLevelUps() {
            ExperienceBar e = new ExperienceBar(20, 1);
            e.Increment(20);
            e.LevelUp();
            Assert.AreEqual(0, e.GetPendingLevelUps());
        }

        // levelling up should increase the amount of experience required to level up again, based off scaling.
        [TestMethod()]
        public void LevellingUpShouldIncreaseTheAmountRequiredForNextLevel() {
            ExperienceBar e = new ExperienceBar(20, 1);
            e.setScaling(1.2);
            e.Increment(20);
            Assert.IsTrue(20 < e.GetMax());
        }

        [TestMethod()]
        public void ExcessExperienceShouldOverflowToNextLevel() {
            ExperienceBar e = new ExperienceBar(20, 1);
            e.Increment(25);
            Assert.AreEqual(5, e.GetCurrent());
        }

        // eg if provided experience could cause multiple level ups
        [TestMethod()]
        public void ExcessExperienceShouldOverflowNumerousTimes() {
            ExperienceBar e = new ExperienceBar(20, 1);
            e.setScaling(2);
            e.Increment(20 + (20 * 2)); // should level up twice
            Assert.AreEqual(2, e.GetPendingLevelUps());
            Assert.AreEqual(0, e.GetCurrent());
        }
    }
}