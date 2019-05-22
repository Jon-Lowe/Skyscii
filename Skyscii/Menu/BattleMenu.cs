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
            validActions.Add("flee");
            validActions.Add("quit");
            if (player.LastOneStanding())
            {
                validActions.Add("moveon");
            }
            if (player.Stats.Exp.GetPendingLevelUps() > 0)
            {
                validActions.Add("levelup");
            }
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
            Console.WriteLine("(lookat) <target> investigate");
            Console.WriteLine("(inventory) open your inventory");
            Console.WriteLine("(flee) flee the battle and return to town");
            if (validActions.Contains("moveon"))
            {
                Console.WriteLine("(moveon) move to the next room");
            }
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
            if (flavourText != "") {
                Console.WriteLine(flavourText);
                Console.WriteLine();
            }
        }

        internal override Menu ProcessCommand(Command command, Log log)
        {
            flavourText = "";
            Menu result = this;

            if (!player.IsAlive())
            {
                return new MainMenu();
            }

            switch (command.GetAction())
            {
                case "attack":
                    flavourText = player.Attack(command.GetTarget());
                    EnemyTurn();

                    if (!player.IsAlive())
                    {
                        flavourText += "\nYou have died! Type quit to return to main menu.";
                        break;
                    }

                    if (player.Stats.Exp.GetPendingLevelUps() > 0){
                        flavourText += "\nYou have "+player.Stats.Exp.GetPendingLevelUps()+" pending level ups. " +
                            "(levelup)";
                        if (!validActions.Contains("levelup"))
                        {
                            validActions.Add("levelup");
                        }
                    }

                    if (player.LastOneStanding()) {
                        flavourText += "\nYou have killed absolutely everyone in this room. Time to move on. (moveon)";
                        if (!validActions.Contains("moveon"))
                        {
                            validActions.Add("moveon");
                        }
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
                    result = new LevelUpMenu(player, typeof(BattleMenu));
                    break;
                case "inventory":
                    result = new InventoryMenu(player, typeof(BattleMenu));
                    break;
                case "flee":
                    Random random = new Random();
                    int roll = random.Next(100);
                    int chance = (((player.Stats.Exp.GetLevel() + player.Stats.Exp.GetPendingLevelUps()) / player.Location.Creatures.First().Stats.Exp.GetLevel()) * 100) / 2;
                    if (roll < chance)
                    {
                        result = new TownMenu(player);
                    }
                    else
                    {
                        flavourText = "You tried to flee but couldnt get away!";
                        EnemyTurn();
                        if (!player.IsAlive())
                        {
                            flavourText += "\nYou have died! Type quit to return to main menu.";
                            break;
                        }
                    }
                    break;
                default:
                    error = "Please type a valid command: (not '"+command.GetAction()+"')";
                    break;
            }
            return result;
        }

        private void EnemyTurn()
        {
            foreach (Sentient s in player.Location.Creatures)
            {
                if (s.GetName() != "player" && s.IsAlive())
                {
                    flavourText += '\n' + s.ExecuteAIAction();
                }
            }
        }
    }
}

