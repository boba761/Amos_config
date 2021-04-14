using System;
using System.Windows.Forms;
using Amos.Data.Sequence.Tables;
using Tools.Interfaces;
using Amos.Data.Sequence;

namespace Amos.Forms
{
    public partial class AcquisitionPropertieForm : BaseForm
    {
        private bool _oldIsLinkDashboard;
        private string _oldAsqPoints;
        private string _oldSweep;
        private string _oldDwellTime;

        public AcquisitionPropertieForm( Document document = null, IObjectStream @object = null )
            : base( document )
        {
            InitializeComponent( );

            if ( @object == null )
                return;

            eventNumber.Text = ( @object.IndexEvent + 1 ).ToString( );
            Acquisition = ( (ObjectSequence)@object).Acquisition;
            _oldIsLinkDashboard = Acquisition.IsLinkDashboard;
            _oldAsqPoints = Acquisition.AsqPoints;
            _oldSweep = Acquisition.Sweep;
            _oldDwellTime = Acquisition.DwellTime;
        }

        public AcquisitionObject Acquisition { get; private set; }

        private void AcquisitionPropertieForm_Load( object sender, EventArgs e )
        {
            linkDashboard.Checked = Acquisition.IsLinkDashboard;
            SetText( );
        }

        private void asqPoints_Leave( object sender, EventArgs e )
        {
            try
            {
                Acquisition.AsqPoints = asqPoints.Text;
                SetText( );
            }
            catch ( Exception ex )
            {
                MessageBox.Show( ex.Message, Program.MainForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning );
            }
        }

        private void sweep_Leave( object sender, EventArgs e )
        {
            try
            {
                Acquisition.Sweep = sweep.Text;
                SetText( );
            }
            catch ( Exception ex )
            {
                MessageBox.Show( ex.Message, Program.MainForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning );
            }
        }

        private void dwell_Leave( object sender, EventArgs e )
        {
            try
            {
                Acquisition.DwellTime = dwell.Text;
                SetText( );
            }
            catch ( Exception ex )
            {
                MessageBox.Show( ex.Message, Program.MainForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning );
            }
        }

        private void SetText( )
        {
            asqPoints.Text = Acquisition.AsqPoints;
            sweep.Text = Acquisition.Sweep;
            filter.Text = Acquisition.Filter;
            dwell.Text = Acquisition.DwellTime;
            asqTime.Text = Acquisition.AsqTime;
        }

        private void linkDashboard_CheckedChanged( object sender, EventArgs e )
        {
            Acquisition.IsLinkDashboard = linkDashboard.Checked;
            SetText( );
        }

        private void OnFormClosing( object sender, FormClosingEventArgs e )
        {
            if ( DialogResult != DialogResult.OK )
            {
                Acquisition.IsLinkDashboard = _oldIsLinkDashboard;
                Acquisition.AsqPoints = _oldAsqPoints;
                Acquisition.Sweep = _oldSweep;
                Acquisition.DwellTime = _oldDwellTime;
            }
        }
    }
}
