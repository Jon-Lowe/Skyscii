using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skyscii
{
    class ItemFactory
    {
        private List<Item> commonItems;
        private List<Item> rareItems;
        private List<Item> epicItems;

        private List<Equippable> commonGear;
        private List<Equippable> rareGear;
        private List<Equippable> epicGear;

        private Random RNG;

        private enum PotionTeir {Common, Rare, Epic}

        public ItemFactory()
        {
            RNG = Program.RandomNumberGenerator;
            InitLists();
        }

        private Item RandomPotion(PotionTeir teir)
        {
            string name;
            int potionExp = 0;
            int potionHP = 0;
            int potionAtk = 0;
            int multiplier = 0;

            switch (teir)
            {
                case PotionTeir.Common:
                    multiplier = 2;
                    break;
                case PotionTeir.Epic:
                    multiplier = 4;
                    break;
                case PotionTeir.Rare:
                    multiplier = 6;
                    break;
            }

            switch (RNG.Next(1, 10))
            {
                case 1:
                    name = "Red";
                    break;
                case 2:
                    name = "Yellow";
                    break;
                case 3:
                    name = "Green";
                    break;
                case 4:
                    name = "Pink";
                    break;
                case 5:
                    name = "Blue";
                    break;
                case 6:
                    name = "Orange";
                    break;
                case 7:
                    name = "Purple";
                    break;
                case 8:
                    name = "Grey";
                    break;
                default:
                    name = "Cloudy";
                    break;                
            }

            switch (RNG.Next(1, 3))
            {
                case 1:
                    potionExp = RNG.Next(1, 7) * multiplier;
                    break;
                case 2:
                    potionHP = RNG.Next(1, 7) * multiplier;
                    break;
                case 3:
                    potionAtk = RNG.Next(1, 7) * multiplier;
                    break;
            }

            return new Item(name + " Potion", "A mysterious potion...", potionExp, potionHP, potionAtk);
        }

        private void InitLists()
        {
            commonItems = new List<Item>()
            {
                new Item("Bread", "A slice of fresh, soft bread.", 0, RNG.Next(8, 12), 0),
                new Item("Cheese", "A small cheese wheel.", 0, RNG.Next(5, 15), 0),
                new Item("Eggplant", "A large purple vegetable. Uncooked.", 0, RNG.Next(0,20), 0),
                new Item("Apple", "A crunchy green apple!", 0, RNG.Next(10,20), 0),
                new Item("Protien Shake", "Helps you bulk!", 0, 0, RNG.Next(2,8)),                
            };

            rareItems = new List<Item>()
            {
                new Item("Potion of Strength", "A dull red potion.", 0, 0, RNG.Next(20,30)),
                new Item("Potion of Wisdom", "A sickly yellow potion.", RNG.Next(50, 80), 0, 0),
                new Item("Potion of Vitality", "A lukewark green potion.", 0, RNG.Next(25, 40), 0),
            };

            epicItems = new List<Item>()
            {
                new Item("Elixir of Strength", "A blood red potion in an ornate flask.", 0, 0, RNG.Next(60, 90)),
                new Item("Elixir of Wisdom", "A glowing yellow potion in an ornate flask.", RNG.Next(100,150), 0, 0),
                new Item("Elixir of Vitality", "A warm green potion in an ornate flask.", 0, RNG.Next(50, 100), 0),
            };

            commonGear = new List<Equippable>()
            {
                new Equippable("Iron Dagger", "An old iron shard with some leather wrapped around one end.", 0, 0, 5),
                new Equippable("Sickle", "More of a farm tool than a weapon.", 0, 0, 10)
            };

            rareGear = new List<Equippable>()
            {
                new Equippable("Flintlock Pistol", "Packs a punch.", 0, 0, 40),
                new Equippable("Steel Sallet", "A light steel helmet.", 0, 30, 0)
            };

            epicGear = new List<Equippable>()
            {
                new Equippable("Infinity Edge", "A massive, golden blade.", 0, 0, 80),
                new Equippable("Brotherhood", "A sword, seemingly made of water.", 0, 0, 76),
                new Equippable("Dragonblade", "A katana, imbued with the might of a dragon.", 0, 0, 120),
                new Equippable("Excalibur", "Will you be the next king?", 0, 0, 100),
                new Equippable("Mjölner", "Are you worthy enough to lift this hammer?", 0, 0, 130),
            };
        }

        public Item GetConsumable()
        {
            int roll = RNG.Next(1, 100);
            if (roll > 90)
                return epicItems[RNG.Next(epicItems.Count)];
            else if (roll > 60)
                return rareItems[RNG.Next(rareItems.Count)];
            else
                return commonItems[RNG.Next(commonItems.Count)];
        }

        public Equippable GetEquipment()
        {
            int roll = RNG.Next(1, 100);
            if (roll > 90)
                return epicGear[RNG.Next(epicGear.Count)];
            else if (roll > 60)
                return rareGear[RNG.Next(rareGear.Count)];
            else
                return commonGear[RNG.Next(commonGear.Count)];
        }

        public Item GetPotion()
        {
            int roll = RNG.Next(1, 100);
            if (roll > 90)
                return RandomPotion(PotionTeir.Epic);
            else if (roll > 60)
                return RandomPotion(PotionTeir.Rare);
            else
                return RandomPotion(PotionTeir.Common);
        }

        public Item GetItem()
        {
            int roll = RNG.Next(1, 90);
            if (roll > 60)
                return GetPotion();
            else if (roll > 30)
                return GetConsumable();
            else
                return GetEquipment();
        }

    }
}
