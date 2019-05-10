using System;
using System.Collections.Generic;
using System.Text;
using Skyscii.SentientStuff;

namespace Skyscii
{
    internal class MainMenu : Menu
    {
        public MainMenu()
        {
            validActions.Add("play");
            validActions.Add("quit");
        }

        internal override void Draw()
        {
            Console.WriteLine("Skyscii");
            Console.WriteLine("----------");
            Console.WriteLine("Please type a command");
            Console.WriteLine();
            Console.WriteLine("(play) Start a new game");
            Console.WriteLine("(quit) Close to desktop");
            Console.WriteLine();
            if (error != "")
            {
                Console.WriteLine(error);
                Console.WriteLine();
                error = "";
            }
        }

        internal override Menu ProcessCommand(Command command, Log log)
        {
            Menu result = this;
            switch (command.GetAction())
            {
                case "play":
                    Room room1 = new Room("room", "it's a room", new List<Sentient>(), new Inventory());
                    room1.Creatures.Add(new Sentient("goblin", "He is green and mean!", 10, 50, room1));
                    Sentient player = new Sentient("player", "it's you!", 10, 50, room1);
                    Item potion = new Item("Green Potion", "As green as the goblin infront of you.", 0, 20, 0);
                    Equippable sword = new Equippable("sword", "take this with you!", 0, 0, 5);
                    player.Inventory.AddItem(potion);
                    player.Inventory.AddItem(sword);
                    room1.Creatures.Add(player);
                    result = new BattleMenu(player);
                    break;
                case "quit":
                    quit = true;
                    break;
                default:
                    error = "Please type a valid command";
                    break;
            }
            return result;
        }
    }
}
