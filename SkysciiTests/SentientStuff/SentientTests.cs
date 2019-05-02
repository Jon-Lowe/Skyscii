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
        Sentient player = new Sentient("player", "it's you!");
        Sentient goblin = new Sentient("goblin", "he is lean, mean, and very green.");
        List<Sentient> creatures = new List<Sentient>();
        Inventory inv = new Inventory();
        Item roomItem;
        Room room;

        int POTION_HEALTH = 10;

        private void setup() {
            player.Stats.Attack = 2;
            goblin.Stats.Attack = 2;

            Item potion = new Item("potion", "it's red and bubbly", 0, POTION_HEALTH, 0);
            player.Inventory.AddItem(potion);

            Item death = new Item("DEATH", "it smiles at you", 0, -99999999, 0);
            player.Inventory.AddItem(death);

            roomItem = new Item("squid", "it looks at you regretfully", 1000, 0, 12);

            creatures.Add(player);
            creatures.Add(goblin);
            room = new Room("testroom", "this is a test room", creatures, inv);

            room.Items.AddItem(roomItem);
        }

        [TestMethod()]
        public void attackingOtherSentientShouldReduceTheirHealth()
        {
            setup();
            int originalHealth = goblin.Stats.Health.GetCurrent();

            String result = player.Attack("goblin");

            // should damage goblin
            Assert.AreEqual(originalHealth - player.Stats.Attack, goblin.Stats.Health);
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
        public void movingShouldUpdateCurrentRoom()
        {
            setup();
            Room room2 = new Room("new room", "its a new room!", new List<Sentient>(), inv);
            // BLOCKED: cannot implement without nextRoom variable in Room class.
        }

        [TestMethod()]
        public void MovingWhenInLastRoomShouldSetWinnerFlag()
        {
            setup();
            // BLOCKED: cannot implement without nextRoom variable in Room class.
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
            player.Stats.Health.Increment(-999999999);
            Assert.IsFalse(player.IsAlive());
        }
    }
}