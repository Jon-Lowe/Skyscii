using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Skyscii.SentientStuff;

namespace Skyscii
{
    internal class InventoryMenu : Menu
    {
        Sentient player;
        String flavourText = "";

        public InventoryMenu(Sentient player)
        {
            this.player = player;
            validActions.Add("use");
            validActions.Add("equip");
            validActions.Add("examine");
            validActions.Add("quit");
        }

        internal override void Draw()
        {
            Console.WriteLine("Skyscii");
            Console.WriteLine("----------");
            Console.WriteLine("INVENTORY MENU");

            if (player.Inventory.GetBag.Count() == 0)
            {
                Console.WriteLine("You're not carrying anything!");
            }
            else
            {
                foreach (Item item in player.Inventory.GetBag)
                {
                    Console.WriteLine(item.Name);
                }
            }

            Console.WriteLine();
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("(use) an item");
            Console.WriteLine("(equip) an item");
            Console.WriteLine("(examine) an item");
            Console.WriteLine("(quit) to the previous menu");
            Console.WriteLine();
            if (error != "")
            {
                Console.WriteLine(error);
                Console.WriteLine();
                error = "";
            }
            if (flavourText != "")
            {
                Console.WriteLine(flavourText);
            }
        }

        internal override Menu ProcessCommand(Command command, Log log)
        {
            String examineCheck;
            flavourText = "";
            Menu result = this;
            switch (command.GetAction())
            {
                case "use":
                    player.UseItem(command.GetTarget());
                    break;
                case "equip":
                    player.UseItem(command.GetTarget());
                    break;
                case "examine":
                    if (player.Inventory.GetBag.Count == 0)
                    {
                        flavourText = "You have nothing to examine!";
                    }
                    else
                    {
                        examineCheck = player.Inventory.findTarget(command.GetTarget()).GetDescription();
                        if (examineCheck == null)
                        {
                            flavourText = "You try to find the " + examineCheck + " but can't!";
                        }
                        else
                        {
                            flavourText = examineCheck;
                        }
                    }
                    result = this;
                    break;
                case "quit":
                    result = new BattleMenu(player);
                    break;
                default:
                    error = "Please type a valid command";
                    break;
            }
            return result;
        }
    }
}
