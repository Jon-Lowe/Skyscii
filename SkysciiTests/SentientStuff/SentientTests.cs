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
        Sentient personwithcrests;
        Sentient goblin;
        List<Sentient> creatures = new List<Sentient>();
        Inventory inv = new Inventory();
        Item roomItem;
        Equippable sword;
        Room room;

        int POTION_HEALTH = 10;
        int SWORD_ATTACK = 10;
        int PLAYER_STARTING_ATTACK = 20;

        private void setup() {

            roomItem = new Item("squid", "it looks at you regretfully", 1000, 0, 12);
            
            room = new Room("testroom", "this is a test room", creatures, inv);
            player = new Sentient("player", "it's you!", PLAYER_STARTING_ATTACK, 30, room);
            goblin = new Sentient("goblin", "he is lean, mean, and very green.", 2, 30, room);

            personwithcrests = new Sentient("personwithcrests", "sentient with money", PLAYER_STARTING_ATTACK, 30, room, 10);

            creatures.Add(player);
            creatures.Add(goblin);

            // adding items
            Item potion = new Item("potion", "it's red and bubbly", 0, POTION_HEALTH, 0);
            sword = new Equippable("sword", "take this with you!", 0, 0, SWORD_ATTACK);

            player.Inventory.AddItem(potion);
            player.Inventory.AddItem(sword);

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

            // potion should also be consumed.
            Assert.IsNull(player.Inventory.findTarget("potion"));
        }

        [TestMethod()]
        public void PickingUpAnItemInTheRoomShouldAddItToSentientInventory()
        {
            setup();
            String result = player.PickUpItem("squid");

            Assert.AreEqual(roomItem, player.Inventory.findTarget("squid"));
            Assert.IsTrue(result.Contains("squid"));

            // potion should be removed from room's inventory
            Assert.IsNull(player.Location.Items.findTarget("squid"));
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

        [TestMethod()]
        public void BugFixUsingEquippableShouldNotConsumeIt() {
            setup();
            player.EquipItem("sword");
            Assert.AreEqual(sword, player.Inventory.findTarget("sword"));
        }

        [TestMethod()]
        public void EquippingItemShouldIncreaseStats() {
            setup();
            player.EquipItem("sword");
            Assert.AreEqual(PLAYER_STARTING_ATTACK + SWORD_ATTACK, player.Stats.Attack);
        }

        [TestMethod()]
        public void EquippingItemTwiceShouldNotIncreaseStatsTwice() {
            setup();
            player.EquipItem("sword");
            player.EquipItem("sword");
            Assert.AreEqual(PLAYER_STARTING_ATTACK + SWORD_ATTACK, player.Stats.Attack);
        }

        [TestMethod()]
        public void unequippingItemShouldReduceStatsIfEquipped() {
            setup();
            player.EquipItem("sword");
            player.UnequipItem("sword");
            Assert.AreEqual(PLAYER_STARTING_ATTACK, player.Stats.Attack);

            // unequipping twice should not decrease stats further, either.
            player.UnequipItem("sword");
            Assert.AreEqual(PLAYER_STARTING_ATTACK, player.Stats.Attack);
        }
        
        // NOTE: logic check that the current room is cleared is done in the BattleMenu
        // ie, the 'moveon' command will only be available on Sentient.LastOneStanding()
        // therefore, Sentient does not currently check that current room is cleared before moving.
        [TestMethod()]
        public void movingShouldUpdateCurrentRoom()
        {
            setup();
            Room room2 = new Room("new room", "its a new room!", new List<Sentient>(), inv);
            room.NextRoom = room2;
            goblin.Stats.Health.Increment(-99999);
            // initial room
            Assert.AreEqual(player.Location, room);
            // another room should remain, as we are still in the first room
            Assert.IsTrue(player.NextRoomRemains());

            // move to next room
            String result = player.MoveToNextRoom();
            Assert.AreEqual(player.Location, room2);

            // another room doesn't exist, as this is the last room.
            Assert.IsFalse(player.NextRoomRemains());
        }

        [TestMethod()]
        public void ShouldInitialiseCrestsToZeroViaSentient()
        {
            setup();
            Assert.AreEqual(0, player.Inventory.CrestCount);
        }

        [TestMethod()]
        public void ShouldInitialiseCrestsToCountViaSentient()
        {
            setup();
            Assert.AreEqual(10, personwithcrests.Inventory.CrestCount);
        }

        [TestMethod()]
        public void WhenKilledSentientShouldGiveCrestsToTheirKiller() {
            setup();
            goblin.Inventory.AddCrests(10);

            // player starts with no crests
            Assert.AreEqual(0, player.Inventory.CrestCount);

            player.Stats.Attack = 1000; // player should kill goblin in one hit
            player.Attack("goblin");

            // player should be given the goblin's crests
            Assert.AreEqual(10, player.Inventory.CrestCount);
        }

        [TestMethod()]
        public void WhenKilledSentientShouldGiveItemsToTheirKiller() {
            setup();

            Item goblinItem = new Item("goblin treasure", "what does it do?", 0, 0, 0);
            goblin.Inventory.AddItem(goblinItem);
            player.Stats.Attack = 1000;
            player.Attack("goblin");

            Assert.AreEqual(goblinItem, player.Inventory.findTarget("goblin treasure"));
        }


    }
}