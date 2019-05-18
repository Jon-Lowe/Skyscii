using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skyscii.SentientStuff;

namespace Skyscii
{
    class TownMenu : Menu
    {
        Sentient player;
        String flavourText = "";

        public TownMenu(Sentient player)
        {
            this.player = player;
            player.Stats.Health.SetCurrent(player.Stats.Health.GetMax());
            flavourText += "You have been healed to full health by the Spring of Life in town.";
            validActions.Add("adventure");
            validActions.Add("shop");
            validActions.Add("inventory");
            if (player.Stats.Exp.GetPendingLevelUps() > 0)
            {
                validActions.Add("levelup");
            }
            validActions.Add("quit");
        }

        internal override void Draw()
        {
            Console.WriteLine("Skyscii");
            Console.WriteLine("----------");
            Console.WriteLine("TOWN MENU");
            Console.WriteLine();
            Console.WriteLine("(adventure) return to the dungeon");
            Console.WriteLine("(shop) buy and sell items");
            Console.WriteLine("(inventory) open your inventory");
            if (validActions.Contains("levelup"))
            {
                Console.WriteLine("(levelup) allocate stats - (" + player.Stats.Exp.GetPendingLevelUps() + ") remaining");
            }
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
                Console.WriteLine();
            }
        }

        internal override Menu ProcessCommand(Command command, Log log)
        {
            flavourText = "";
            Menu result = this;

            switch (command.GetAction())
            {
                case "adventure":
                    result = new BattleMenu(player);
                    break;
                case "levelup":
                    result = new LevelUpMenu(player, typeof(TownMenu));
                    break;
                case "inventory":
                    result = new InventoryMenu(player, typeof(TownMenu));
                    break;
                case "quit":
                    result = new MainMenu();
                    quit = true;
                    break;
                default:
                    error = "Please type a valid command: (not '" + command.GetAction() + "')";
                    break;
            }
            return result;
        }
    }
}
