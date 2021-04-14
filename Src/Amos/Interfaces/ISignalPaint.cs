using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Amos.Interfaces
{
    public interface ISignalPaint
    {
        bool IsReal { get; }
        bool IsImag { get; }
        bool IsSelection { get; }
        int StartSelection { get; }
        int WidthSelection { get; }
        bool IsNewSelection { get; }
        int StartNewSelection { get; }
        int EndNewSelection { get; }
        int WidthNewSelection { get; }
        float Min { get; }
        float Max { get; }
        float[] Real { get; }
        float[] Imag { get; }
        double PointTime { get; }
        string Position { get; }
    }
}
