using System;

namespace Skyscii
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu currentMenu = new MainMenu();
            string currentCommand;

            while (!currentMenu.Quit)
            {
                currentMenu.Draw();
                currentCommand = Console.ReadLine();
                currentMenu = currentMenu.ProcessCommand(currentCommand);
                Console.Clear();
            }
        }
    }
}
