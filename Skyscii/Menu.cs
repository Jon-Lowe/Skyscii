using System;
using System.Collections.Generic;
using System.Text;

namespace Skyscii
{
    // could also be implemented as an interface
    abstract class Menu
    {
        protected List<string> validActions = new List<string>();
        protected bool quit = false;
        protected string error = "";

        public bool Quit { get => quit; }

        internal abstract Menu ProcessCommand(string command);

        internal abstract void Draw();
    }
}
