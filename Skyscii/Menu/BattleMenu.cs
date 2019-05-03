using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Skyscii.SentientStuff;

namespace Skyscii
{
    class BattleMenu : Menu
    {
        Sentient player;
        String flavourText = "";

        public BattleMenu(Sentient player) {
            this.player = player;
            validActions.Add("attack");
            validActions.Add("quit");
        }

        internal override void Draw()
        {
            Console.WriteLine("Skyscii");
            Console.WriteLine("----------");
            Console.WriteLine("BATTLE MENU");
            Console.WriteLine("Your health: [" + player.Stats.Health.GetCurrent() + "/" 
                + player.Stats.Health.GetMax() + "]");
            Console.WriteLine("Enemies:");
            foreach (Sentient s in player.Location.Creatures) {
                Console.WriteLine(s.GetName()+": [" + s.Stats.Health.GetCurrent() + "/" 
                    + s.Stats.Health.GetMax() + "]");
            }
            Console.WriteLine();
            Console.WriteLine("(attack <enemyname>) attack an enemy");
            Console.WriteLine("(look) around");
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
            Menu result = this;
            switch (command.GetAction())
            {
                case "attack":
                    flavourText = player.Attack(command.GetTarget());
                    result = this;
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

