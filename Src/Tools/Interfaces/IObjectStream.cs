using System.Drawing;

namespace Tools.Interfaces
{
    public enum eTypeObjectStream
    {
        Default,
        Value,
        Variable,
        Table,
        Table1D2D,
        TableShape,
        Acquisition,
        Continue,
        AsynchronousStart,
        AsynchronousStop,
        Asynchronous
    }

    public interface IObjectStream : IObjectSequence
    {
        bool IsEdit { get; }
        bool IsClear { get; }
        bool IsSignal { get; }
        bool IsXY { get; }
        bool IsAsynchronousStart { get; }
        bool IsAsynchronousStop { get; }
        bool IsLoopStart { get; }
        bool IsLoopEnd { get; }
        bool IsAcquisition { get; }
        bool IsContinue { get; }
        bool IsTable1D2D { get; }
        bool IsTable1D { get; }
        bool IsTable2D { get; }
        bool IsTable3D { get; }
        bool IsTable4D { get; }
        bool IsTableShape { get; }
        bool IsProterty { get; }
        bool IsChange { get; set; }

        string Text { get; }
        string DrawText { get; } 
        eTypeObjectStream Type { get; }
        Image Image { get; }
        IStream Parent { get; }
        IObjectStream Prev { get; }
        IObjectStream Next { get; }
        eTypeObjectCollection TypeCollection { get; }
        int IndexEvent { get; }

        void CorrectionSequence( ICommandList command );
    }
}
