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
            Console.WriteLine("");
            Console.WriteLine(@"
                        ________     ____       _   _              _          _ _ 
                       /  ____  \   |  _ \     | | | |            | |        (_|_)
                      /  / ___|  \  | |_) | ___| |_| |__   ___  __| |___  ___ _ _ 
                     |  | |       | |  _ < / _ \ __| '_ \ / _ \/ _` / __|/ __| | |
                     |  | |___    | | |_) |  __/ |_| | | |  __/ (_| \__ \ (__| | |
                      \  \____|  /  |____/ \___|\__|_| |_|\___|\__,_|___/\___|_|_|
                       \________/   Presents                       
                                                                                  



             _______    _                     _______    _______   _________  _________   _  _ 
            (  ____ \  | \    /\  |\     /|  (  ____ \  (  ____ \  \__   __/  \__   __/  ( )( )
            | (    \/  |  \  / /  ( \   / )  | (    \/  | (    \/     ) (        ) (     | || |
            | (_____   |  (_/ /    \ (_) /   | (_____   | |           | |        | |     | || |
            (_____  )  |   _ (      \   /    (_____  )  | |           | |        | |     | || |
                  ) |  |  ( \ \      ) (           ) |  | |           | |        | |     (_)(_)
            /\____) |  |  /  \ \     | |     /\____) |  | (____/\  ___) (___  ___) (___   _  _ 
            \_______)  |_/    \/     \_/     \_______)  (_______/  \_______/  \_______/  (_)(_)
                                                                                               

                     
            ");
            Console.WriteLine("----------");
            Console.WriteLine("");
            Console.WriteLine("Please type a command");
            Console.WriteLine();
            Console.WriteLine("(play) Start a new game");
            Console.WriteLine("(quit) Close to desktop");
            Console.WriteLine();
            Console.WriteLine("----------");
            Console.WriteLine("");
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

                    // room2 setup
                    Room room2 = new Room("Boss Room", "It looks a little scary!", new List<Sentient>(), new Inventory());
                    room2.Creatures.Add(new Sentient("Goblin Boss", "This one looks a little overweight", 100, 100, room2));
                    Item veryGoodSword = new Equippable("MEGASWORD", "The devs really didn't think this through", 10000, 10000, 10000);
                    room2.Items.AddItem(veryGoodSword);
                    room1.NextRoom = room2;

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
