using System.Windows.Forms;

namespace Tools.Interfaces
{
    public interface IStream
    {
        string Name { get; }
        string IconType { get; }
        int Group { get; }
        bool IsVisible { get; }
        bool IsMove { get; }
        bool IsEdit { get; }
        int EventCount { get; }
        //sStreamData StreamData { get;  }
        IObjectCollection this[eTypeObjectCollection typeCollection] { get; }

        IObjectStream GetNewObject( eTypeObjectCollection typeCollection, int index );
        IObjectStream GetObject( eTypeObjectCollection typeCollection, int index, string text );
        IObjectStream GetObject( eTypeObjectCollection typeCollection, int index, eTypeObjectStream type, object data = null );
        ContextMenuStrip GetContextMenu( ISequencePanel panel, eTypeObjectCollection typeCollection, int index );

        void InsertEvent( int indexColumn );
        void RemoveEvent( int indexColumn );
    }
}
