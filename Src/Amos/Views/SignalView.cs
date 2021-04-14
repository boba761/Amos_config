using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Amos.Views
{
    public partial class SignalView : BaseView
    {
        private static SignalView _self;

        private SignalView()
        {
            InitializeComponent();
            signalControl.onChangeZoom += OnChangeZoom;
        }

        public static SignalView Instance()
        {
            if (_self == null)
                _self = new SignalView();
            return _self;
        }

        public override Document Document
        {
            set
            {
                base.Document = value;
                if ( value == null )
                    signalControl.Signal = null;
                else
                {
                    signalControl.Signal = value.Signal;
                    SetControl( );
                }
            }
        }

        private void OnChangeZoom( float zoom )
        {
            zoomInToolStrip.Enabled = zoom < 32 && zoom < signalControl.Point1D;
            zoomOutToolStrip.Enabled = zoom > 1;
        }

        private void SetControl( )
        {
            signalControl.IsReal = realToolStrip.Checked;
            signalControl.IsImag = imagToolStrip.Checked;

            zoomInToolStrip.Enabled = signalControl.Zoom < 32 && signalControl.Zoom < Document.Signal.Point1D;
            zoomOutToolStrip.Enabled = signalControl.Zoom > 1; 

            pos2D.Value = 1;
            pos2D.Maximum = Document.Signal.Point2D;
            pos2D.Enabled = Document.Signal.Point2D > 1;
           
            pos3D.Value = 1;
            pos3D.Maximum = Document.Signal.Point3D;
            pos3D.Enabled = Document.Signal.Point3D > 1;

            pos4D.Value = 1;
            pos4D.Maximum = Document.Signal.Point4D;
            pos4D.Enabled = Document.Signal.Point4D > 1;
        }

        private void pos2D_ValueChanged( object sender, EventArgs e )
        {
            signalControl.Position2D = (int)pos2D.Value;
        }

        private void pos3D_ValueChanged( object sender, EventArgs e )
        {
            signalControl.Position3D = (int)pos3D.Value;
        }

        private void pos4D_ValueChanged( object sender, EventArgs e )
        {
            signalControl.Position4D = (int)pos4D.Value;
        }

        private void OnToolStripClick( object sender, EventArgs e )
        {
            ( (ToolStripButton)sender ).Checked = !( (ToolStripButton)sender ).Checked;
            signalControl.IsReal = realToolStrip.Checked;
            signalControl.IsImag = imagToolStrip.Checked;
        }

        private void zoomInToolStrip_Click( object sender, EventArgs e )
        {
            signalControl.Zoom *= 2;
            zoomInToolStrip.Enabled = signalControl.Zoom < 32 && signalControl.Zoom < Document.Signal.Point1D;
            zoomOutToolStrip.Enabled = signalControl.Zoom > 1; 
        }

        private void zoomOutToolStrip_Click( object sender, EventArgs e )
        {
            signalControl.Zoom /= 2;
            zoomInToolStrip.Enabled = signalControl.Zoom < 32 && signalControl.Zoom < Document.Signal.Point1D;
            zoomOutToolStrip.Enabled = signalControl.Zoom > 1; 
        }
    }
}
