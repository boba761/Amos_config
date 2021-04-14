using System;
using Amos.Controls.Sequence;
using Amos.Forms;
using Tools.Interfaces;
using Amos.Data.Sequence;

namespace Amos.Views
{
    public partial class SequenceView : BaseView
    {
        private static SequenceView _self;

        private SequenceView()
        {
            InitializeComponent();

            sequenceControl.onProperty += OnProperty;
            sequenceControl.onSelect += OnSelect;
        }

        public override Document Document
        {
            set
            {
                base.Document = value;
                if ( value == null )
                    sequenceControl.Sequence = null;
                else
                    sequenceControl.Sequence = value.Sequence;
            }
        }

        public SequenceControl SequenceControl { get { return sequenceControl; } }

        public static SequenceView Instance()
        {
            if (_self == null)
                _self = new SequenceView();
            return _self;
        }

        public override void Refresh( )
        {
            sequenceControl.Refresh( );
            base.Refresh( );
        }

        public void OnProperty( IObjectStream @object )
        {
            while ( @object != null && @object.Type == eTypeObjectStream.Continue )
                @object = @object.Prev;

            if ( @object == null )
                return;
            BaseForm form = null;
            if ( ( (ObjectSequence)@object).Acquisition != null )
                form = new AcquisitionPropertieForm( Document, @object );
            else if ( ( (ObjectSequence)@object).Table != null )
                form = new TableEditForm( Document, ( (ObjectSequence)@object).Table );
            if ( form != null )
                form.ShowDialog( );
        }

        public void OnSelect( IObjectStream @object )
        {
            if ( @object == null )
            {
                propertyToolStrip.Enabled = false;
                editCellToolStrip.Enabled = false;
                clearCellToolStrip.Enabled = false;
                signalToolStrip.Enabled = false;
                acquisitionToolStrip.Enabled = false;
                continueToolStrip.Enabled = false;
                table1D2DToolStrip.Enabled = false;
                tableShapeToolStrip.Enabled = false;
                table1DToolStrip.Enabled = false;
                table2DToolStrip.Enabled = false;
                table3DToolStrip.Enabled = false;
                table4DToolStrip.Enabled = false;
                plusXToolStrip.Enabled = false;
                plusYToolStrip.Enabled = false;
                minusXToolStrip.Enabled = false;
                minusYToolStrip.Enabled = false;
                loopStartToolStrip.Enabled = false;
                loopEndToolStrip.Enabled = false;
                asynchronousStartToolStrip.Enabled = false;
                asynchronousStopToolStrip.Enabled = false;
                return;
            }
            propertyToolStrip.Enabled = @object.IsProterty;
            editCellToolStrip.Enabled = @object.IsEdit;
            clearCellToolStrip.Enabled = @object.IsClear;
            signalToolStrip.Enabled = @object.IsSignal;
            acquisitionToolStrip.Enabled = @object.IsAcquisition;
            continueToolStrip.Enabled = @object.IsContinue;
            table1D2DToolStrip.Enabled = @object.IsTable1D2D;
            tableShapeToolStrip.Enabled = @object.IsTableShape;
            table1DToolStrip.Enabled = @object.IsTable1D;
            table2DToolStrip.Enabled = @object.IsTable2D;
            table3DToolStrip.Enabled = @object.IsTable3D;
            table4DToolStrip.Enabled = @object.IsTable4D;
            plusXToolStrip.Enabled = @object.IsXY;
            plusYToolStrip.Enabled = @object.IsXY;
            minusXToolStrip.Enabled = @object.IsXY;
            minusYToolStrip.Enabled = @object.IsXY;
            loopStartToolStrip.Enabled = @object.IsLoopStart;
            loopEndToolStrip.Enabled = @object.IsLoopEnd;
            asynchronousStartToolStrip.Enabled = @object.IsAsynchronousStart;
            asynchronousStopToolStrip.Enabled = @object.IsAsynchronousStop;
        }

        private void propertyToolStrip_Click( object sender, EventArgs e )
        {
            sequenceControl.ActionPanel.OnProperty( sender, e );
        }

        private void editCellToolStrip_Click( object sender, EventArgs e )
        {
            sequenceControl.ActionPanel.OnEdit( sender, e );
        }

        private void clearCellToolStrip_Click( object sender, EventArgs e )
        {
            sequenceControl.ActionPanel.OnClear( sender, e );
        }

        private void signalToolStrip_Click( object sender, EventArgs e )
        {
            sequenceControl.ActionPanel.OnSignal( sender, e );
        }

        private void impulsToolStrip_Click( object sender, EventArgs e )
        {
            sequenceControl.ActionPanel.OnAcquisition( sender, e );
        }

        private void continueToolStrip_Click( object sender, EventArgs e )
        {
            sequenceControl.ActionPanel.OnContinue( sender, e );
        }

        private void table1D2DToolStrip_Click( object sender, EventArgs e )
        {
            sequenceControl.ActionPanel.OnTable1D2D( sender, e );
        }

        private void tableShapeToolStrip_Click( object sender, EventArgs e )
        {
            sequenceControl.ActionPanel.OnTableShape( sender, e );
        }

        private void table1DToolStrip_Click( object sender, EventArgs e )
        {
            sequenceControl.ActionPanel.OnTable1D( sender, e );
        }

        private void table2DToolStrip_Click( object sender, EventArgs e )
        {
            sequenceControl.ActionPanel.OnTable2D( sender, e );
        }

        private void table3DToolStrip_Click( object sender, EventArgs e )
        {
            sequenceControl.ActionPanel.OnTable3D( sender, e );
        }

        private void table4DToolStrip_Click( object sender, EventArgs e )
        {
            sequenceControl.ActionPanel.OnTable4D( sender, e );
        }

        private void plusXToolStrip_Click( object sender, EventArgs e )
        {
            sequenceControl.ActionPanel.OnPlusX( sender, e );
        }

        private void plusYToolStrip_Click( object sender, EventArgs e )
        {
            sequenceControl.ActionPanel.OnPlusY( sender, e );
        }

        private void minusXToolStrip_Click( object sender, EventArgs e )
        {
            sequenceControl.ActionPanel.OnMinusX( sender, e );
        }

        private void minusYToolStrip_Click( object sender, EventArgs e )
        {
            sequenceControl.ActionPanel.OnMinusY( sender, e );
        }

        private void loopStartToolStrip_Click( object sender, EventArgs e )
        {
            sequenceControl.ActionPanel.OnLoopStart( sender, e );
        }

        private void loopEndToolStrip_Click( object sender, EventArgs e )
        {
            sequenceControl.ActionPanel.OnLoopEnd( sender, e );
        }

        private void asynchronousStartToolStrip_Click( object sender, EventArgs e )
        {
            sequenceControl.ActionPanel.OnAsynchronousStart( sender, e );
        }

        private void asynchronousStopToolStrip_Click( object sender, EventArgs e )
        {
            sequenceControl.ActionPanel.OnAsynchronousStop( sender, e );
        }
    }
}
