using System;
using System.Collections.Generic;
using System.Text;

namespace Skyscii.SentientStuff
{
    /*
        Represents any variable status a creature has that can increment or decrement, and has a maximum value
        For example, health or mana 

        current implementation: 'current' cannot exceed 'maximum'.
     */
    public class StatBar
    {
        // the maximum boundary of the bar
        private int max;

        // the current value of the bar - cannot exceed maximum
        private int current;

        // initialises a new StatBar with maximum and current set to input parameter maximum
        public StatBar(int max) {
             // do nothing yet
        }

        // increments 'current' by the amount shown
        // @param amount: the amount 'current' will be incremented by. can be negative. will not exceed maximum
        public int Increment(int amount) {
            return 0;
        }

        public int GetCurrent() {
            return 0;
        }

        public int GetMax() {
            return 0;
        }

        // used to set the max.
        // @param resetCurrent: if true, current will be set to equal maximum. Else current will remain the same.
        public void SetMax(int max, bool resetCurrent) {
            
        }
    }
}
