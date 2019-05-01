using System;
using System.Collections.Generic;
using System.Text;

namespace Skyscii
{
    /*
     An interface for any game object that can be targeted by a command
     eg room, Sentient, Item.   
     */
    public interface ITargetableObject{

        String GetName();

        String GetDescription();

    }
}
