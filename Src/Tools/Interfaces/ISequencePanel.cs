using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tools.Interfaces
{
    public interface ISequencePanel
    {
        void OnEdit( object sender, EventArgs e );
        void OnClear( object sender, EventArgs e );
        void OnTable1D2D( object sender, EventArgs e );
        void OnTableShape( object sender, EventArgs e );
        void OnTable1D( object sender, EventArgs e );
        void OnTable2D( object sender, EventArgs e );
        void OnTable3D( object sender, EventArgs e );
        void OnTable4D( object sender, EventArgs e );
        void OnLoopStart( object sender, EventArgs e );
        void OnLoopEnd( object sender, EventArgs e );
        void OnAcquisition( object sender, EventArgs e );
        void OnContinue( object sender, EventArgs e );
        void OnSignal( object sender, EventArgs e );
        void OnPlusX( object sender, EventArgs e );
        void OnPlusY( object sender, EventArgs e );
        void OnMinusX( object sender, EventArgs e );
        void OnMinusY( object sender, EventArgs e );
        void OnAsynchronousStart( object sender, EventArgs e );
        void OnAsynchronousStop( object sender, EventArgs e );
        void OnProperty( object sender, EventArgs e );
    }
}
