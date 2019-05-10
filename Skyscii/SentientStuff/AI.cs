using System;
using System.Collections.Generic;
using System.Text;

namespace Skyscii.SentientStuff
{
    /*
     * AI to control enemy characters.
     */
    public class AI
    {
        public String generateResponse(Sentient toControl) {
            return toControl.Attack("player");
        }
    }
}
