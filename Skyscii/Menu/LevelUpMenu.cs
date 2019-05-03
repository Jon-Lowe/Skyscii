using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Skyscii.SentientStuff;

namespace Skyscii
{
    class LevelUpMenu : Menu
    {
        Sentient player;
        String flavourText = "";

        public LevelUpMenu(Sentient player) {
            this.player = player;
            validActions.Add("attack");
            validActions.Add("health");
            validActions.Add("quit");
        }

        internal override void Draw()
        {
            Console.WriteLine("!!!! Level Up !!!!");
            Console.WriteLine("----------");
            Console.WriteLine("Current level: " + player.Stats.Exp.GetLevel());
            Console.WriteLine("Pending levelUps: " + player.Stats.Exp.GetPendingLevelUps());
            Console.WriteLine("----------");
            Console.WriteLine("Your current stats: ");
            Console.WriteLine("Health: [" + player.Stats.Health.GetCurrent() + "/" + player.Stats.Health.GetMax() + "]");
            Console.WriteLine("Attack: " + player.Stats.Attack);

            Console.WriteLine("Please select which stat you would like to increase:");
            Console.WriteLine();
            Console.WriteLine("(attack)");
            Console.WriteLine("(health)");
            Console.WriteLine("or if you are done, (quit)");
            Console.WriteLine();
            if (error != "")
            {
                Console.WriteLine(error);
                Console.WriteLine();
                error = "";
            }
            if (flavourText != "") {
                Console.WriteLine(flavourText);
            }
        }

        internal override Menu ProcessCommand(Command command, Log log)
        {
            flavourText = "";
            Menu result = this;
            switch (command.GetAction())
            {
                case "attack":
                    flavourText = player.LevelUp(true, false);
                    break;
                case "health":
                    flavourText = player.LevelUp(false, true);
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
