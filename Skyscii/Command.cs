using System;
using System.Collections.Generic;
using System.Text;

namespace Skyscii
{
    /*  
     *  To subdivide 'command' strings into action and target strings
     *  eg "continue" -> action = continue
     *  "attack goblin" -> action = attack, target = goblin
     *  Supports multi word targets, eg "use red potion"
     *
     *  NOTE: current implementation does not support multi-word commands.
     *  
     *  Author: SV
    */
    public class Command {
        
        // the action of the command - eg use, attack, goto, quit, help
        private string action = "";

        // the target of the command - eg goblin, potion
        private string target = "";

        //@param: commandString - a command string, containing either an action, or an action and target.
        public Command(String commandString){

            commandString = commandString.Trim();
            String[] splitString = commandString.Split(' ');
            action = splitString[0].Trim();

            if (splitString.Length > 1) { // if target exists
                target = commandString.Substring(commandString.IndexOf(' ')+1);
                target = target.Trim();
                // trim in case of 'use    red potion'
            }
            
        }

        public String GetTarget() {
            return target;
        }

        public String GetAction() {
            return action;
        }
    }
}
