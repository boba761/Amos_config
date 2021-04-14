using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Amos.Interfaces;
using Amos.Data.Sequence;
using Amos.Data.Sequence.Tables.DataTables;
using Amos.Controls;

namespace Amos.Forms
{
    public partial class TableEditForm : BaseForm
    {
        private TableSequence _table;
        private int _fullHeight;
        private Dictionary<string, string> _oldText;
        private Dictionary<string, DataTable> _oldData;
        private Dictionary<string, DelayAutoTable> _oldDelayAuto;
        private Dictionary<string, GenerateAutoTable> _oldGenerateAuto;

        public TableEditForm( Document document = null, TableSequence table = null )
            : base (document)
        {
            _oldText = new Dictionary<string, string>( );
            _oldData = new Dictionary<string, DataTable>( );
            _oldDelayAuto = new Dictionary<string, DelayAutoTable>( );
            _oldGenerateAuto = new Dictionary<string, GenerateAutoTable>( );
            
            InitializeComponent();

            _fullHeight = ClientSize.Height;
            ClientSize = new Size( ClientSize.Width, listAllTablesLabel.Location.Y );

            if ( table == null )
                return;

            Table = table;
            SetAllTablesList();
        }

        public TableSequence Table 
        {
            get { return _table; }
            private set
            {
                if ( _table != null && _table != value && _table.AutoControl != null )
                {
                    editPanel.Controls.Remove( _table.AutoControl );
                    _table.AutoControl.TextChanged -= OnTextChanged;
                }
                _table = value;
                Text = string.Format( "{0}: {1} : {2}", _table.ClassTable, _table.Name, _table.Entries );
                nameTextBox.Text = _table.Name;
                typeCollectionLabel.Text = _table.TypeTable;
                discriptionLabel.Text = _table.Description;
                timePointLabel.Enabled = _table.IsTimePoint;
                timePointTextBox.Enabled = _table.IsTimePoint;
                if (_table.IsTimePoint )
                    timePointTextBox.Text = _table.Data.TimePoint;
                digreesRadio.Enabled = _table.IsStep;
                stepRadio.Enabled = _table.IsStep;
                stepTextBox.Enabled = _table.IsStep;
                if ( _table.IsStep )
                {
                    digreesRadio.Checked = _table.Data.Degrees;
                    stepRadio.Checked = !digreesRadio.Checked;
                    stepTextBox.Text = _table.Data.Steps;
                }
                valuesTextBox.Text = _table.Text == null ? "0" : _table.Text;
                if ( autoCheckBox.Checked == _table.Data.Auto )
                {
                    if ( autoCheckBox.Checked )
                    {
                        editPanel.Controls.Add( _table.AutoControl );
                        _table.AutoControl.TextChanged += OnTextChanged;
                        _table.AutoControl.Focus( );
                    }
                    else if ( _table.AutoControl != null )
                    {
                        editPanel.Controls.Remove( _table.AutoControl );
                        _table.AutoControl.TextChanged -= OnTextChanged;
                    }
                }
                else
                    autoCheckBox.Checked = _table.Data.Auto;
                autoCheckBox.Enabled = _table.AutoControl != null;
            }
        }

        private void showAllTablesCheckBox_CheckedChanged( object sender, EventArgs e )
        {
            if (showAllTablesCheckBox.Checked)
                ClientSize = new Size( ClientSize.Width, _fullHeight );
            else
                ClientSize = new Size( ClientSize.Width, listAllTablesLabel.Location.Y );
        }

        private void SetAllTablesList()
        {
            allTablesListBox.Items.Clear( );
            _oldText.Clear( );
            _oldData.Clear( );
            _oldDelayAuto.Clear( );
            _oldGenerateAuto.Clear( );
            foreach ( TableSequence table in Document.Sequence.Tables )
            {
                allTablesListBox.Items.Add( table );
                _oldText.Add( table.Name, table.Text );
                _oldData.Add( table.Name, (DataTable)table.Data.Clone( ) );
                if ( table.DelayAuto != null ) 
                    _oldDelayAuto.Add( table.Name, (DelayAutoTable)table.DelayAuto.Clone( ) );
                else
                    _oldDelayAuto.Add( table.Name, null );
                if ( table.GenerateAuto != null )
                    _oldGenerateAuto.Add( table.Name, (GenerateAutoTable)table.GenerateAuto.Clone( ) );
                else
                    _oldGenerateAuto.Add( table.Name, null );
            }
            if ( allTablesListBox.Items.Count > 0 )
                allTablesListBox.SelectedItem = _table;
        }

        private void OnTextChanged( object sender, EventArgs e )
        {
            if ( autoCheckBox.Checked && !_table.Data.Auto )
                valuesTextBox.Text = _table.Text;
            if ( !autoCheckBox.Checked )
                _table.Text = valuesTextBox.Text;
            allTablesListBox.Items.Remove( _table );
            allTablesListBox.Items.Add( _table );
            allTablesListBox.SelectedItem = _table;
        }

        private void TableEditForm_FormClosing( object sender, FormClosingEventArgs e )
        {
            if ( DialogResult == DialogResult.OK )
            {
                _table.Text = valuesTextBox.Text;
                if ( _table.AutoControl is GradientAmplitudeControl )
                    _table.Data.Auto = false;
                else
                    _table.Data.Auto = autoCheckBox.Checked;
                _table.Data.TimePoint = timePointTextBox.Text;
                _table.Data.Degrees = digreesRadio.Checked;
                _table.Data.Steps = stepTextBox.Text;
            }
            else
            {
                foreach ( TableSequence table in Document.Sequence.Tables )
                {
                    table.Text = _oldText[table.Name];
                    table.Data = _oldData[table.Name];
                    table.DelayAuto = _oldDelayAuto[table.Name];
                    table.GenerateAuto = _oldGenerateAuto[table.Name];
                }
            }
            Program.Document.Sequence.Refresh( );
        }

        private void allTablesListBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            if ( allTablesListBox.SelectedItem != null && allTablesListBox.SelectedItem != Table )
                Table = (TableSequence)allTablesListBox.SelectedItem;
        }

        private void autoCheckBox_CheckedChanged( object sender, EventArgs e )
        {
            valuesTextBox.Visible = !autoCheckBox.Checked;
            _table.Data.Auto = autoCheckBox.Checked;
            if ( autoCheckBox.Checked )
            {
                editPanel.Controls.Add( _table.AutoControl );
                _table.AutoControl.TextChanged += OnTextChanged;
                _table.AutoControl.Focus( );
            }
            else if ( _table.AutoControl != null )
            {
                editPanel.Controls.Remove( _table.AutoControl );
                _table.AutoControl.TextChanged -= OnTextChanged;
            }
            OnTextChanged( sender, e );
        }

        private void digreesRadio_CheckedChanged( object sender, EventArgs e )
        {
            _table.Data.Degrees = digreesRadio.Checked;
            stepTextBox.Enabled = !digreesRadio.Checked;
            stepRadio.Checked = !digreesRadio.Checked;
            discriptionLabel.Text = _table.Description;
        }

        private void stepRadio_CheckedChanged( object sender, EventArgs e )
        {
            stepTextBox.Enabled = stepRadio.Checked;
            digreesRadio.Checked = !stepRadio.Checked;
            discriptionLabel.Text = _table.Description;
        }
    }
}
