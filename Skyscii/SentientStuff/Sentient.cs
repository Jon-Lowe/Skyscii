using System;
using System.Collections.Generic;
using System.Text;

namespace Skyscii.SentientStuff
{
    public class Sentient : ITargetableObject, ISearchable
    {
        public ITargetableObject findTarget(string name)
        {
            throw new NotImplementedException();
        }

        public string GetDescription()
        {
            throw new NotImplementedException();
        }

        public string GetName()
        {
            throw new NotImplementedException();
        }
    }
}
