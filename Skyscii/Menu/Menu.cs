using System;
using System.Collections.Generic;
using System.Text;

namespace Skyscii
{
    // could also be implemented as an interface
    abstract class Menu
    {
        protected List<string> validActions = new List<string>();
        protected List<string> globalActions = new List<string>();
        protected bool quit = false;
        protected string error = "";

        // TEMPORARY - (for drawing log) Should end up using a number input or key based menu to page through lines
        protected const int NUM_LINES = 10;

        public bool Quit { get => quit; }

        public Menu()
        {
            globalActions.Add("help");
            globalActions.Add("log");
        }

        internal Menu ProcessGlobalCommand(string command, Log log)
        {
            Command c = new Command(command);

            //TEMPORARY - store every command in log for testing (replace with selective inside child menus)
            if (validActions.Contains(command) || globalActions.Contains(command))
            {
                log.Add(command);
            }

            if (validActions.Contains(c.GetAction()))
            {
                return ProcessCommand(c, log);
            }

            Menu result = this;
            switch (c.GetAction())
            {
                case "help":
                    DrawHelp();
                    break;
                case "log":
                    DrawLog(log);
                    break;
                default:
                    error = "Please type a valid command";
                    break;
            }
            return result;
        }

        internal void DrawHelp()
        {
            Console.Clear();
            Console.WriteLine("Menu actions");
            Console.WriteLine("----------");
            foreach (string s in validActions)
            {
                Console.WriteLine("(" + s + ")");
            }
            Console.WriteLine();
            Console.WriteLine("Global actions");
            Console.WriteLine("----------");
            foreach (string s in globalActions)
            {
                Console.WriteLine("(" + s + ")");
            }
            Console.WriteLine();
            Console.WriteLine("Press any key to return");
            Console.ReadKey();
        }

        internal void DrawLog(Log log)
        {
            Console.Clear();
            List<string> lines = log.getLineOfLog(NUM_LINES);

            Console.WriteLine("Last " + lines.Count + " lines of log");
            Console.WriteLine("----------");
            if (lines.Count > 0)
            {
                foreach (string s in lines)
                {
                    Console.WriteLine(s);
                }
            }

            Console.WriteLine();
            Console.WriteLine("Press any key to return");
            Console.ReadKey();
        }

        internal abstract Menu ProcessCommand(Command command, Log log);

        internal abstract void Draw();
    }
}