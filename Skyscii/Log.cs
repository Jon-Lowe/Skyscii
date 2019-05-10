using System;
using System.Collections.Generic;
using System.Text;

namespace Skyscii
{
    // stores commands and resulting flavour text
    // could be replaced with a standard log?
    public class Log
    {

        private List<string> mainLog { get; set; }

        public Log()
        {
            mainLog = new List<string>();
        }

        
        public List<string> GetLog
        {
            get { return mainLog; }
        }


        public void Add(string action)
        {
            mainLog.Add(action);
        }

        public List<string> getLineOfLog(int number)
        {
            List<string> result = new List<string>();
        

            if (mainLog.Count > number)
            {
                for (int i = mainLog.Count - number; i < mainLog.Count; i++)
                {
                    result.Add(mainLog[i]);
                }
            }
            else
            {
                result = mainLog;
            }
        
            return result;
        }

    }
}
