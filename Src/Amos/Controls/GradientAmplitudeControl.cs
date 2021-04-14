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
    public partial class GradientAmplitudeControl : UserControl
    {
        private GenerateAutoTable _tableData;

        public GradientAmplitudeControl( GenerateAutoTable tableData = null )
        {
            InitializeComponent();

            if ( tableData == null )
                return;

            _tableData = tableData;
            SetControls( );
        }

        private void OnVisibleChanged( object sender, EventArgs e )
        {
            SetControls( );
        }

        private void SetControls( )
        {
            startTextBox.Text = _tableData.Start;
            incrementTextBox.Text = _tableData.Increment;
            numberTextBox.Text = _tableData.Number;
        }

        private void OnLeave( object sender, EventArgs e )
        {
            try 
            { 
                _tableData.Start = startTextBox.Text;
                startTextBox.Text = _tableData.Start;
            }
            catch ( Exception ex )
            {
                MessageBox.Show( ex.Message, Program.MainForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning );
                startTextBox.Focus( );
                return;
            }
            try 
            { 
                _tableData.Increment = incrementTextBox.Text;
                incrementTextBox.Text = _tableData.Increment;
            }
            catch ( Exception ex )
            {
                MessageBox.Show( ex.Message, Program.MainForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning );
                incrementTextBox.Focus( );
                return;
            }
            try 
            { 
                _tableData.Number = numberTextBox.Text;
                numberTextBox.Text = _tableData.Number;
            }
            catch ( Exception ex )
            {
                MessageBox.Show( ex.Message, Program.MainForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning );
                numberTextBox.Focus( );
                return;
            }
            _tableData.Table.Data.Auto = false;
            _tableData.Table.Text = _tableData.Generate( );
            Text = Text == "0" ? "1" : "0";
        }
    }
}
