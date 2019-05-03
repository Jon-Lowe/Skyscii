using System;
using System.Collections.Generic;
using System.Text;

namespace Skyscii.SentientStuff
{
    /*
     * Represents a Sentients current experience level
     * as a 'level up' can require modification to external stats, when a level up would occur
     * pendingLevelUps is incremented.
     * 
     * When the stats have been modified, the LevelUp() function may then be called.
     */
    public class ExperienceBar : StatBar
    {
        int currentLevel = 1;

        // how many level ups are pending but have not yet been applied
        int pendingLevelUps =0;

        // how much more experience is required every level to level up.
        private double scaling = 1.2;

        // constructor with default level = 1
        public ExperienceBar(int max) : base(max) {
            this.SetCurrent(0);
        }

        public ExperienceBar(int max, int currentLevel) : base(max) {
            this.currentLevel = currentLevel;
            this.SetCurrent(0);
        }

        public override int Increment(int amount){

            // increment 'amount'
            // subtract max
            // if amount exceeds max, loop
            do{
                base.Increment(amount);
                amount = amount - this.GetMax();

                if (IsFull()) { // if full experience bar
                    pendingLevelUps++;
                    SetCurrent(0);
                    SetMax(Convert.ToInt32(GetMax() * scaling), false);
                }

            } while (amount > 0);
            
            return GetCurrent();
        }

        public double getScaling() {
            return scaling;
        }

        public void setScaling(double scaling) {
            this.scaling = scaling;
        }

        public int GetLevel() {
            return currentLevel;
        }

        public int GetPendingLevelUps() {
            return pendingLevelUps;
        }
        
        // increments level by 1, decrements pending level ups by 1
        // @return - current level
        public void LevelUp() {
            pendingLevelUps--;
            currentLevel++;
        }
    }
}
