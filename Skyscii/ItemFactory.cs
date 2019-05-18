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

        public ItemFactory(Random NumberGenerator)
        {
            RNG = NumberGenerator;
            InitLists();
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

            };

            epicItems = new List<Item>()
            {

            };

            commonGear = new List<Equippable>()
            {

            };

            rareGear = new List<Equippable>()
            {

            };

            epicGear = new List<Equippable>()
            {
                new Equippable("Infinity Edge", "A massive, golden blade.", 0, 0, 80),

            };
        }

        public Item GetItem()
        {
            if (RNG.Next(0,100) > 50)
            {
                return GetConsumable();
            }
            return GetEquipment();
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
    }
}
