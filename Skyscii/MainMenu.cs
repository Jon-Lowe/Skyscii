using System;
using System.Collections.Generic;
using System.Text;

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
            }
        }

        internal override Menu ProcessCommand(string command, Log log)
        {
            Menu result = this;
            switch (command)
            {
                case "play":
                    result = new MainMenu();
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
