using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tools.Interfaces
{
    public interface IObjectSequence : ICloneable
    {
        string ToolTip { get; }
    }
}
