using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Amos.Views;
using Calculations;
using Calculations.Values;
using Calculations.Variables;

namespace Amos.Forms
{
    public partial class VariableEditorForm : BaseForm
    {
        private Exception _exception;
        private Compiler _compiler;
        private Dictionary<string, Variable> _variables;

        public VariableEditorForm( Document document = null )
            : base( document ) 
        {
            _variables = new Dictionary<string, Variable>( );
            _compiler = new Compiler( _variables );
            InitializeComponent( );
            sequenceDataGrid.Tag = sequenceCheckBox;
            sequenceCheckBox.Tag = sequenceDataGrid;
            tableDataGrid.Tag = tableCheckBox;
            tableCheckBox.Tag = tableDataGrid;
            localDataGrid.Tag = localCheckBox;
            localCheckBox.Tag = localDataGrid;
        }

        private void OnLoad( object sender, EventArgs e )
        {
            _variables.Clear( );
            foreach ( Variable variable in Document.Dashboard.EnumerableVariables )
                _variables.Add( variable.Name, (Variable)variable.Clone( ) );

            foreach ( Variable variable in Document.Dashboard.SequenceCollectionVariable.EnumerableVariables )
                sequenceSource.Add( _variables[variable.Name] );
            foreach ( Variable variable in Document.Dashboard.TableCollectionVariable.EnumerableVariables )
                tableSource.Add( _variables[variable.Name] );
            foreach ( Variable variable in Document.Dashboard.LocalCollectionVariable.EnumerableVariables )
                localSource.Add( _variables[variable.Name] );

            foreach ( GlobalVariable globalVariable in Document.Global.Variables )
            {
                if ( Document.Dashboard.GlobalCollectionVariable.FindRecursionVariable( globalVariable.Name ) != null )
                    globalSource.Add( _variables[globalVariable.Name] );
                else
                    globalSource.Add( globalVariable.Clone( ) );
            }
            foreach ( Variable variable in Document.Dashboard.GlobalCollectionVariable.EnumerableVariables )
            {
                bool isEnable = false;
                foreach ( GlobalVariable globalVariable in globalSource)
                {
                    if ( globalVariable.Name == variable.Name )
                    {
                        isEnable = true;
                        break;
                    }
                }
                if ( isEnable == false)
                    globalSource.Add( _variables[variable.Name] );
            }
            Compile();
        }

        private void OnCellParsing( object sender, DataGridViewCellParsingEventArgs e )
        {
            DataGridView gridView = (DataGridView)sender;
            Variable variable = (Variable)( (DataGridView)sender ).Rows[e.RowIndex].DataBoundItem;
            string name = ( (DataGridView)sender ).Columns[e.ColumnIndex].HeaderText;
            try
            {
                switch ( name.ToUpper( ) )
                {
                case "VALUE":
                    variable.Value.Data = e.Value;
                    e.Value = variable.Value;
                    e.ParsingApplied = true;
                    break;
                case "MIN":
                    if ( string.IsNullOrWhiteSpace( (string)e.Value ) )
                        variable.Min = null;
                    else if ( variable.Min == null )
                        variable.Min = (Value)Activator.CreateInstance( variable.Value.GetType(), new object[] { e.Value, null } );
                    else
                        variable.Min.Data = e.Value;
                    e.Value = variable.Min;
                    e.ParsingApplied = true;
                    break;
                case "MAX":
                    if ( string.IsNullOrWhiteSpace( (string)e.Value ) )
                        variable.Max = null;
                    else if ( variable.Max == null )
                        variable.Max = (Value)Activator.CreateInstance( variable.Value.GetType( ), new object[] { e.Value, null } );
                    else
                        variable.Max.Data = e.Value;
                    e.Value = variable.Max;
                    e.ParsingApplied = true;
                    break;
                case "STEP":
                    if ( string.IsNullOrWhiteSpace( (string)e.Value ) )
                        variable.Step = null;
                    else if ( variable.Step == null )
                        variable.Step = (Value)Activator.CreateInstance( variable.Value.GetType( ), new object[] { e.Value, null } );
                    else
                        variable.Step.Data = e.Value;
                    e.Value = variable.Step;
                    e.ParsingApplied = true;
                    break;
                }
                variable.Calculate( _compiler, true );
                gridView.Refresh( );
            }
            catch ( Exception ex )
            {
                e.ParsingApplied = false;
                _exception = ex;
            }
        }

        private void OnCellFormatting( object sender, DataGridViewCellFormattingEventArgs e )
        {
            DataGridView gridView = (DataGridView)sender;
            if ( gridView.Tag == null )
                return;
            DataGridViewCell cell = gridView.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if ( e.Value is Value )
            {
                if ( ( (CheckBox)gridView.Tag ).Checked )
                {
                    if ( ( e.Value as Value ).IsExpression )
                    {
                        e.Value = ( (Value)e.Value ).Expression;
                        e.FormattingApplied = true;
                    }
                    if ( cell.IsInEditMode == false )
                    {
                        e.CellStyle.ForeColor = Color.Black;
                        cell.ReadOnly = false;
                    }
                }
                else if ( cell.IsInEditMode == false )
                {
                    if ( ( e.Value as Value ).IsExpression )
                    {
                        e.CellStyle.ForeColor = Color.DarkGray;
                        cell.ToolTipText = ( e.Value as Value ).Expression;
                        cell.ReadOnly = true;
                    }
                    else
                    {
                        e.CellStyle.ForeColor = Color.Black;
                        cell.ReadOnly = false;
                    }
                }
            }
        }
        
        private void OnCellValidating( object sender, DataGridViewCellValidatingEventArgs e )
        {
            Value value;
            Variable variable = (Variable)( (DataGridView)sender ).Rows[e.RowIndex].DataBoundItem;
            DataGridViewCell cell = ( (DataGridView)sender ).Rows[e.RowIndex].Cells[e.ColumnIndex];
            string name = ( (DataGridView)sender ).Columns[e.ColumnIndex].HeaderText;
            switch ( name.ToUpper( ) )
            {
            case "ADD":
                if ( variable != null && ( variable as GlobalVariable ).IsAdd != bool.Parse( e.FormattedValue.ToString( ) ) )
                {
                    if ( bool.Parse( e.FormattedValue.ToString( ) ) )
                    {
                        if ( _variables.ContainsKey( variable.Name ) )
                        {
                            cell.ErrorText = "Variable with such name already sushestvuet in the list of variables.";
                            e.Cancel = true;
                        }
                        else
                            _variables.Add( variable.Name, variable );
                    }
                    else
                    {
                        if ( variable.IsOutput )
                        {
                            cell.ErrorText = "Deleting a variable is impossible it participates in calculation of other variables.";
                            e.Cancel = true;
                        }
                        else
                            _variables.Remove( variable.Name );
                    }
                }
                break;
            case "NAME":
                if ( variable != null) 
                {
                    if ( string.IsNullOrWhiteSpace( (string)e.FormattedValue ) == true )
                    {
                        cell.ErrorText = "Name a variable must not be null string.";
                        e.Cancel = true;
                    }
                    else if ( variable.Name != e.FormattedValue.ToString( ) && _variables.ContainsKey( e.FormattedValue.ToString( ) ) )
                    {
                        cell.ErrorText = "A variable with such name exists already.";
                        e.Cancel = true;
                    }
                    else if ( variable is LocalVariable )
                    {
                        _variables.Remove( variable.Name );
                        _variables.Add( e.FormattedValue.ToString( ), variable );
                    }
                }
                break;
            case "VALUE":
                if ( variable != null && string.IsNullOrWhiteSpace( (string)e.FormattedValue ) )
                {
                    cell.ErrorText = "Value a variable must not be null.";
                    e.Cancel = true;
                }
                break;
            case "MIN":
                if (variable != null && variable.Max != null && string.IsNullOrWhiteSpace( (string)e.FormattedValue ) == false )
                {
                    value = (Value)Activator.CreateInstance( variable.Value.GetType( ), new object[] { e.FormattedValue, null } );
                    if (variable.Max < value)
                    {
                        cell.ErrorText = "Value minimum must be less value of maximum.";
                        e.Cancel = true;
                    }
                }
                break;
            case "MAX":
                if ( variable != null && variable.Min != null && string.IsNullOrWhiteSpace( (string)e.FormattedValue ) == false )
                {
                    value = (Value)Activator.CreateInstance( variable.Value.GetType( ), new object[] { e.FormattedValue, null } );
                    if ( variable.Min > value )
                    {
                        cell.ErrorText = "Value of maximum must be more value of minimum.";
                        e.Cancel = true;
                    }
                }
                break;
            case "STEP":
                if ( variable != null && variable.Min != null && variable.Max != null && string.IsNullOrWhiteSpace( (string)e.FormattedValue ) == false )
                {
                    value = (Value)Activator.CreateInstance( variable.Value.GetType( ), new object[] { e.FormattedValue, null } );
                    double rez = Convert.ToDouble( variable.Max.Data ) - Convert.ToDouble( variable.Min.Data );
                    if ( Convert.ToDouble( value.Data ) <= 0 )
                    {
                        cell.ErrorText = "Step must be or empty or to have a positive value different from a zero.";
                        e.Cancel = true;
                    }
                    else if ( Convert.ToDouble( value.Data ) > rez )
                    {
                        cell.ErrorText = "Too stride for a variable.";
                        e.Cancel = true;
                    }
                }
                break;
            }

            if ( e.Cancel )
                MessageBox.Show( cell.ErrorText, Program.MainForm.Name, MessageBoxButtons.OK, MessageBoxIcon.Warning );
            else
                cell.ErrorText = string.Empty;
        }

        private void OnCellEndEdit( object sender, DataGridViewCellEventArgs e )
        {
            DataGridView gridView = (DataGridView)sender;
            if ( gridView.Columns[e.ColumnIndex].HeaderText.ToUpper( ) == "NAME" )
                gridView.Refresh( );
        }

        private void OnDataError( object sender, DataGridViewDataErrorEventArgs e )
        {
            if ( _exception != null)
                MessageBox.Show( _exception.Message, Program.MainForm.Name, MessageBoxButtons.OK, MessageBoxIcon.Warning );
            else
                MessageBox.Show( e.Exception.Message, Program.MainForm.Name, MessageBoxButtons.OK, MessageBoxIcon.Warning );
        }

        private void OnCheckedChanged( object sender, EventArgs e )
        {
            CheckBox checkBox = (CheckBox)sender;
            if ( checkBox.Tag != null )
                ( (DataGridView)checkBox.Tag ).Refresh( );
        }
        
        private void OnLocalVariableAddingNew( object sender, AddingNewEventArgs e )
        {
            e.NewObject = new LocalVariable( "", new DoubleValue( 0 ) );
        }

        private void OnGlobalVariableAddingNew( object sender, AddingNewEventArgs e )
        {
            e.NewObject = new GlobalVariable( "", new DoubleValue( 0 ) );
        }

        private void OnUserDeletingRow( object sender, DataGridViewRowCancelEventArgs e )
        {
            Variable variable = (Variable)e.Row.DataBoundItem;
            if ( variable.IsOutput )
            {
                MessageBox.Show( "Deleting a variable is impossible it participates in calculation of other variables.", 
                    Program.MainForm.Name, MessageBoxButtons.OK, MessageBoxIcon.Warning );
                e.Cancel = true;
            }
            else
                _variables.Remove( variable.Name );
        }

        private void OnRowValidating( object sender, DataGridViewCellCancelEventArgs e )
        {
            Variable variable = (Variable)( (DataGridView)sender ).Rows[e.RowIndex].DataBoundItem; 
            DataGridViewCell cell = ( (DataGridView)sender ).Rows[e.RowIndex].Cells[0];
            if ( variable != null && string.IsNullOrWhiteSpace( variable.Name ) )
            {
                cell.ErrorText = "Name a variable must not be null string.";
                e.Cancel = true;
            }
            if ( e.Cancel )
                MessageBox.Show( cell.ErrorText, Program.MainForm.Name, MessageBoxButtons.OK, MessageBoxIcon.Warning );
            else
                cell.ErrorText = string.Empty;
        }

        private void OnLocalSourceListChanged( object sender, ListChangedEventArgs e )
        {
            Variable variable;
            BindingSource bindingSource = (BindingSource)sender;
            switch(e.ListChangedType)
            {
            case ListChangedType.ItemAdded:
                variable = (Variable)bindingSource.List[e.NewIndex];
                if ( string.IsNullOrWhiteSpace(variable.Name))
                    _variables.Add( variable.Name, variable );
                break;
            case ListChangedType.ItemDeleted:
                _variables.Remove( "" );
                break;
            }
        }
        
        private void Compile()
        {
            foreach ( Variable variable in _variables.Values )
                variable.Compile( _compiler );
        }

        private void OnSaveGlobalVariables( object sender, EventArgs e )
        {
            Document.Global.Variables.Clear( );
            foreach ( GlobalVariable variable in globalSource )
                Document.Global.Variables.Add( variable );
            Document.Global.SaveVariable( );
        }

        private void OnTabPageChanged( object sender, EventArgs e )
        {
            saveButton.Enabled = tabControl.SelectedTab == globalTabPage;
        }

        private void okButton_Click( object sender, EventArgs e )
        {
            OnSaveGlobalVariables( sender, e );
            foreach ( Variable variable in Document.Dashboard.EnumerableVariables )
            {
                if ( variable is GlobalVariable || variable is LocalVariable )
                    continue;
                variable.CopyData( _variables[ variable.Name ] );
            }
            Document.Dashboard.SetLocalVariables( localSource );
            Document.Dashboard.SetGlobalVariables( globalSource );
            DashboardView.Instance( ).Refresh( );
        }
    }
}
