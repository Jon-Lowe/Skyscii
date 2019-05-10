using System;
using System.Collections.Generic;
using System.Text;

namespace Skyscii.SentientStuff
{
    // A container for a Sentient's stats
    public class Stats
    {
        private StatBar health;
        private ExperienceBar exp;
        private int attack;

        public Stats(int attack, int maxHealth, int expRequiredForNextLevel, int currentLevel)
        {
            health = new StatBar(maxHealth);
            exp = new ExperienceBar(expRequiredForNextLevel, currentLevel);
            this.attack = attack;
        }

        public ExperienceBar Exp { get => exp; }
        public StatBar Health { get => health; }
        public int Attack { get => attack; set => attack = value; }
    }
}
