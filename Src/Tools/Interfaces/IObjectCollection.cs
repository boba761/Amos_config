using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tools.Interfaces
{
    public enum eTypeObjectCollection
    {
        _1D,
        _2D,
        _3D,
        _4D
    }

    public delegate void OnSetObject( int index, IObjectStream @object );

    public interface IObjectCollection
    {
        bool IsVisible { get; }
        IStream Stream { get; }
        eTypeObjectCollection TypeCollection { get; }
        IObjectStream this[int index] { get; set; }

        event OnSetObject onSetObject;
    }
}
