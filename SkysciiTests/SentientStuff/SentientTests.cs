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
    public class SentientTests
    {
        Sentient player;
        Sentient goblin;
        List<Sentient> creatures = new List<Sentient>();
        Inventory inv = new Inventory();
        Item roomItem;
        Room room;

        int POTION_HEALTH = 10;

        private void setup() {

            roomItem = new Item("squid", "it looks at you regretfully", 1000, 0, 12);

            room = new Room("testroom", "this is a test room", creatures, inv, null);
            player = new Sentient("player", "it's you!", 20, 30, room);
            goblin = new Sentient("goblin", "he is lean, mean, and very green.", 2, 30, room);

            creatures.Add(player);
            creatures.Add(goblin);

            // adding items
            Item potion = new Item("potion", "it's red and bubbly", 0, POTION_HEALTH, 0);
            player.Inventory.AddItem(potion);

            Item death = new Item("DEATH", "it smiles at you", 0, -99999999, 0);
            player.Inventory.AddItem(death);

            room.Items.AddItem(roomItem);
        }

        [TestMethod()]
        public void attackingOtherSentientShouldReduceTheirHealth()
        {
            setup();
            int originalHealth = goblin.Stats.Health.GetCurrent();

            String result = player.Attack("goblin");

            // should damage goblin
            Assert.AreEqual(originalHealth - player.Stats.Attack, goblin.Stats.Health.GetCurrent());
            // should return flavour text
            Assert.IsTrue(result.Contains("goblin"));

        }

        [TestMethod()]
        public void usingPotionShouldIncreaseHealth()
        {
            setup();
            player.Stats.Health.Increment(-12);
            int originalHealth = player.Stats.Health.GetCurrent();
            String result = player.UseItem("potion");

            Assert.AreEqual(originalHealth + POTION_HEALTH, player.Stats.Health.GetCurrent());
            Assert.IsTrue(result.Contains("potion"));
        }

        [TestMethod()]
        public void PickingUpAnItemInTheRoomShouldAddItToSentientInventory()
        {
            setup();
            String result = player.PickUpItem("squid");

            Assert.AreEqual(roomItem, player.Inventory.findTarget("squid"));
            Assert.IsTrue(result.Contains("squid"));
        }

        [TestMethod()]
        public void PickingUpAnItemNotInTheRoomShouldNotAddItToSentientInventory()
        {
            setup();
            String result = player.PickUpItem("I dun exist");
            Assert.IsNull(player.Inventory.findTarget("I dun exist"));
        }

        [TestMethod()]
        public void WhenHealthIsAtZeroShouldBeDead()
        {
            setup();
            player.Stats.Health.Increment(-999999999);
            Assert.IsFalse(player.IsAlive());
        }

        [TestMethod()]
        public void ApplyingLevelUpShouldModifyStats() {
            // applying a level up should increase the selected stat,
            // reduce pending level ups, and increase level.

            setup();
            player.Stats.Exp.Increment(100);
            int originalPendingLevelUps = player.Stats.Exp.GetPendingLevelUps();
            int originalHealth = player.Stats.Health.GetMax();
            int originalAttack = player.Stats.Attack;
            String result = player.LevelUp(true, true);

            // stats should be modified
            Assert.IsTrue(originalHealth < player.Stats.Health.GetMax());
            Assert.IsTrue(originalAttack < player.Stats.Attack);

            // level should increase
            Assert.AreEqual(2, player.Stats.Exp.GetLevel());

            // pending level up should decrease by 1
            Assert.AreEqual(originalPendingLevelUps - 1, player.Stats.Exp.GetPendingLevelUps());
        }

        // integration test with AI, really.
        // AI is very basic right now, this will need to be updated
        [TestMethod()]
        public void BasicAIShouldAttackPlayer() {
            setup();
            int originalPlayerHealth = player.Stats.Health.GetCurrent();
            goblin.ExecuteAIAction();
            Assert.IsTrue(originalPlayerHealth > player.Stats.Health.GetCurrent());
        }


        // TODO:
        /*
        [TestMethod()]
        public void movingShouldUpdateCurrentRoom()
        {
            setup();
            Room room2 = new Room("new room", "its a new room!", new List<Sentient>(), inv);
            // BLOCKED: cannot implement without nextRoom variable in Room class.
            Assert.Fail("unimplemented");
        }

        [TestMethod()]
        public void MovingWhenInLastRoomShouldSetWinnerFlag()
        {
            setup();
            // BLOCKED: cannot implement without nextRoom variable in Room class.
            Assert.Fail("unimplemented");
        }
        */
    }
}