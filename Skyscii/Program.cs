using System;

namespace Skyscii
{
    class Program
    {
        static readonly Random random = new Random();

        static void Main(string[] args)
        {
            Menu currentMenu = new MainMenu();
            Log log = new Log();
            string currentCommand;

            while (!currentMenu.Quit)
            {
                currentMenu.Draw();
                currentCommand = Console.ReadLine();
                currentMenu = currentMenu.ProcessGlobalCommand(currentCommand, log);
                Console.Clear();
            }
        }
        public static Random RandomNumberGenerator { get => random; }            
    }
}
