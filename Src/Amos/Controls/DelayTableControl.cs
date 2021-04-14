using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Amos.Data.Sequence;
using Amos.Data.Sequence.Tables;
using Amos.Data.Sequence.Tables.DataTables;

namespace Amos.Controls
{
    public partial class DelayTableControl : UserControl
    {
        private DelayAutoTable _delayAuto;

        public DelayTableControl( DelayAutoTable delayAuto = null )
        {
            InitializeComponent();

            if ( delayAuto == null )
                return;

            _delayAuto = delayAuto;
            SetControls( );
        }

        private void OnVisibleChanged( object sender, EventArgs e )
        {
            SetControls( );
        }

        private void SetControls( )
        {
            valueTextBox.Text = _delayAuto.Increment;
            valueTextBox.Enabled = !_delayAuto.UseDwellTime;
            useDwellTimeCheckBox.Checked = _delayAuto.UseDwellTime;
            if ( _delayAuto.UseDwellTime )
                descriptionLabel.Text = "The value in the Dashboard will be user at run time.";
            else
                descriptionLabel.Text = "The initial value is entered in the 1D event.";
            everyComboBox.SelectedIndex = _delayAuto.Every;
            addComboBox.SelectedIndex = _delayAuto.Add > 0 ? 0 : 0 ;
        }

        private void OnLeave( object sender, EventArgs e )
        {
            try
            {
                _delayAuto.Every = everyComboBox.SelectedIndex;
                _delayAuto.Add = addComboBox.SelectedIndex;
                _delayAuto.Increment = valueTextBox.Text;
                Text = Text == "0" ? "1" : "0";
            }
            catch ( Exception ex )
            {
                MessageBox.Show( ex.Message, Program.MainForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning );
                valueTextBox.Focus( );
            }
        }
        
        private void useDwellTimeCheckBox_CheckedChanged( object sender, EventArgs e )
        {
            try
            {
                _delayAuto.UseDwellTime = useDwellTimeCheckBox.Checked;
                SetControls( );
                Text = Text == "0" ? "1" : "0";
            }
            catch ( Exception ex )
            {
                MessageBox.Show( ex.Message, Program.MainForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning );
                valueTextBox.Focus( );
            }
        }

        private void everyComboBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            try
            {
                _delayAuto.Every = everyComboBox.SelectedIndex;
                Text = Text == "0" ? "1" : "0";
            }
            catch ( Exception ex )
            {
                MessageBox.Show( ex.Message, Program.MainForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning );
                valueTextBox.Focus( );
            }
        }

        private void addComboBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            try
            {
                _delayAuto.Add = addComboBox.SelectedIndex;
                Text = Text == "0" ? "1" : "0";
            }
            catch ( Exception ex )
            {
                MessageBox.Show( ex.Message, Program.MainForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning );
                valueTextBox.Focus( );
            }
        }
    }
}
