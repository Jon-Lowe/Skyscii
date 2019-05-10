using System;
using System.Collections.Generic;
using System.Text;

namespace Skyscii
{
    /*
     * an interface for any game object that contains other game objects that may be targeted by a command.
     * eg, room, inventory
     */
    public interface ISearchable {

        ITargetableObject findTarget(String name);

    }
}
