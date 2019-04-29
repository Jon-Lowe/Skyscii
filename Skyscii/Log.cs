using System;
using System.Collections.Generic;
using System.Text;

namespace Skyscii
{
    // stores commands and resulting flavour text
    // could be replaced with a standard log?
    internal class Log
    {
        private List<string> log = new List<string>();

        public void Add(string line)
        {
            log.Add(line);
        }

        public List<string> getLines(int number)
        {
            List<string> result = new List<string>();

            if (log.Count > number)
            {
                for (int i = log.Count - number; i < log.Count; i++)
                {
                    result.Add(log[i]);
                }
            }
            else
            {
                result = log;
            }

            return result;
        }
    }
}
