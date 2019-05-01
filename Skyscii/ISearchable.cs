using System;
using System.Collections.Generic;
using System.Text;

namespace Skyscii
{
    public interface ISearchable {

        ITargetableObject findTarget(String name);

    }
}
