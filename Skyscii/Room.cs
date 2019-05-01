using System;
using System.Collections.Generic;
using System.Text;

namespace Skyscii
{
    public class Room : ISearchable, ITargetableObject
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
