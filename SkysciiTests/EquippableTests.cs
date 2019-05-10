using Microsoft.VisualStudio.TestTools.UnitTesting;
using Skyscii;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Skyscii.SentientStuff;

namespace Skyscii.Tests
{
    [TestClass()]
    public class EquippableTests
    {
        Room r;
        Sentient s;
        Equippable sword;

        private void setup() {
            r = new Room("room", "it's a room", new List<Sentient>(), new Inventory());
            s = new Sentient("player", "it's you!", 2, 10, r);
            sword = new Equippable("sword", "don't go alone, take this!", 0, 0, 10);
        }

        [TestMethod()]
        public void EquippingShouldIncreaseStats(){
            setup();
            int originalAttack = s.Stats.Attack;

            String result = sword.EquipMe(s);

            Assert.IsTrue(s.Stats.Attack > originalAttack);
            Assert.IsTrue(result.Contains(s.GetName()));
        }

        [TestMethod()]
        public void EquippingAndUnequippingShouldReturnStatsToNormal() {
            setup();
            int originalAttack = s.Stats.Attack;

            sword.EquipMe(s);
            sword.UnequipMe(s);

            Assert.AreEqual(originalAttack, s.Stats.Attack);
        }

        [TestMethod()]
        public void EquippingTwiceShouldDoNothingSecondTime() {
            setup();
            sword.EquipMe(s);
            int attack = s.Stats.Attack;
            sword.EquipMe(s);

            Assert.AreEqual(attack, s.Stats.Attack);
        }

        [TestMethod()]
        public void UnequippingWhenItemAlreadyUnequippedShouldDoNothing() {
            setup();
            int attack = s.Stats.Attack;
            sword.UnequipMe(s);

            Assert.AreEqual(attack, s.Stats.Attack);
        }

        [TestMethod()]
        public void beingEquippedByAnotherSentientWillUnequipFromFormerSentient() {
            setup();

            Sentient s2 = new Sentient("Luigi", "!", 2, 20, r);
            
            int firstSAttack = s.Stats.Attack;
            int secondSAttack = s2.Stats.Attack;
            sword.EquipMe(s);
            sword.EquipMe(s2);

            // first sentient's attack should be unchanged, as the weapon 
            // is now equipped to second sentinet
            Assert.AreEqual(firstSAttack, s.Stats.Attack);

            // second sentient equipped last, so its attack should be greater
            Assert.IsTrue(secondSAttack < s2.Stats.Attack);
        }

    }
}