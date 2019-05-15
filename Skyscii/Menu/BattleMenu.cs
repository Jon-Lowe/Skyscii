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
            validActions.Add("lookat");
            validActions.Add("inventory");
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
                if (s.GetName() != "player")
                    Console.WriteLine(s.GetName()+": [" + s.Stats.Health.GetCurrent() + "/" 
                        + s.Stats.Health.GetMax() + "]");
            }
            Console.WriteLine();
            Console.WriteLine("(attack <enemyname>) attack an enemy");
            Console.WriteLine("(look) around");
            Console.WriteLine("(inventory) open your inventory");
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
                    flavourText = player.Attack(command.GetTarget());
                    foreach (Sentient s in player.Location.Creatures) {
                        if (s.GetName() != "player" && s.IsAlive()) {
                            flavourText += '\n'+ s.ExecuteAIAction();
                        }
                    }

                    if (player.Stats.Exp.GetPendingLevelUps() > 0){
                        flavourText += "\nYou have "+player.Stats.Exp.GetPendingLevelUps()+" pending level ups. " +
                            "(levelup)";
                        validActions.Add("levelup");
                    }

                    if (player.LastOneStanding()) {
                        flavourText += "\nYou have killed absolutely everyone in this room. Time to move on. (moveon)";
                        validActions.Add("moveon");
                        result = this;
                    }

                    result = this;
                    break;
                case "lookat":
                    ITargetableObject target = player.Location.findTarget(command.GetTarget());
                    if (target != null)
                        flavourText = "You look at a "+command.GetTarget()+ ". \n"+ target.GetDescription();
                    else
                        flavourText = "You try to look at a " + command.GetTarget() + " but can't see that far";
                    result = this;
                    break;
                case "quit":
                    result = new MainMenu();
                    quit = true;
                    break;
                case "moveon":
                    // means they have killed everyone in the room -> they should move to the next room, 
                    // or if there is no next room, to the dungeon selection menu

                    // another room remains to be explored, move player to it
                    if (player.NextRoomRemains()) {
                        flavourText = player.MoveToNextRoom();
                        return this;
                    }

                    // if no next room remains, return to mainmenu
                    result = new MainMenu();
                    break;
                case "levelup":
                    result = new LevelUpMenu(player);
                    break;
                case "inventory":
                    result = new InventoryMenu(player);
                    break;
                default:
                    error = "Please type a valid command: (not '"+command.GetAction()+"')";
                    break;
            }
            return result;
        }
    }
}

