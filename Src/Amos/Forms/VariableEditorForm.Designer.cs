namespace Amos.Forms
{
    partial class VariableEditorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose( );
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent( )
        {
            this.components = new System.ComponentModel.Container( );
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle( );
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle( );
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle( );
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle( );
            this.sequenceSource = new System.Windows.Forms.BindingSource( this.components );
            this.tabControl = new System.Windows.Forms.TabControl( );
            this.sequencePage = new System.Windows.Forms.TabPage( );
            this.sequenceDataGrid = new System.Windows.Forms.DataGridView( );
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn( );
            this.valueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn( );
            this.minDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn( );
            this.maxDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn( );
            this.stepDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn( );
            this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn( );
            this.sequenceCheckBox = new System.Windows.Forms.CheckBox( );
            this.tablePage = new System.Windows.Forms.TabPage( );
            this.tableDataGrid = new System.Windows.Forms.DataGridView( );
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn( );
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn( );
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn( );
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn( );
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn( );
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn( );
            this.tableSource = new System.Windows.Forms.BindingSource( this.components );
            this.tableCheckBox = new System.Windows.Forms.CheckBox( );
            this.localTabPage = new System.Windows.Forms.TabPage( );
            this.localDataGrid = new System.Windows.Forms.DataGridView( );
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn( );
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn( );
            this.dataGridViewTextBoxColumn9 = new System.Windows.Forms.DataGridViewTextBoxColumn( );
            this.dataGridViewTextBoxColumn10 = new System.Windows.Forms.DataGridViewTextBoxColumn( );
            this.dataGridViewTextBoxColumn11 = new System.Windows.Forms.DataGridViewTextBoxColumn( );
            this.dataGridViewTextBoxColumn12 = new System.Windows.Forms.DataGridViewTextBoxColumn( );
            this.localSource = new System.Windows.Forms.BindingSource( this.components );
            this.localCheckBox = new System.Windows.Forms.CheckBox( );
            this.globalTabPage = new System.Windows.Forms.TabPage( );
            this.dataGridView3 = new System.Windows.Forms.DataGridView( );
            this.Add = new System.Windows.Forms.DataGridViewCheckBoxColumn( );
            this.dataGridViewTextBoxColumn13 = new System.Windows.Forms.DataGridViewTextBoxColumn( );
            this.dataGridViewTextBoxColumn14 = new System.Windows.Forms.DataGridViewTextBoxColumn( );
            this.dataGridViewTextBoxColumn15 = new System.Windows.Forms.DataGridViewTextBoxColumn( );
            this.dataGridViewTextBoxColumn16 = new System.Windows.Forms.DataGridViewTextBoxColumn( );
            this.dataGridViewTextBoxColumn17 = new System.Windows.Forms.DataGridViewTextBoxColumn( );
            this.dataGridViewTextBoxColumn18 = new System.Windows.Forms.DataGridViewTextBoxColumn( );
            this.globalSource = new System.Windows.Forms.BindingSource( this.components );
            this.cancelButton = new System.Windows.Forms.Button( );
            this.okButton = new System.Windows.Forms.Button( );
            this.saveButton = new System.Windows.Forms.Button( );
            ( (System.ComponentModel.ISupportInitialize)( this.sequenceSource ) ).BeginInit( );
            this.tabControl.SuspendLayout( );
            this.sequencePage.SuspendLayout( );
            ( (System.ComponentModel.ISupportInitialize)( this.sequenceDataGrid ) ).BeginInit( );
            this.tablePage.SuspendLayout( );
            ( (System.ComponentModel.ISupportInitialize)( this.tableDataGrid ) ).BeginInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.tableSource ) ).BeginInit( );
            this.localTabPage.SuspendLayout( );
            ( (System.ComponentModel.ISupportInitialize)( this.localDataGrid ) ).BeginInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.localSource ) ).BeginInit( );
            this.globalTabPage.SuspendLayout( );
            ( (System.ComponentModel.ISupportInitialize)( this.dataGridView3 ) ).BeginInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.globalSource ) ).BeginInit( );
            this.SuspendLayout( );
            // 
            // sequenceSource
            // 
            this.sequenceSource.AllowNew = false;
            this.sequenceSource.DataSource = typeof( Calculations.Variables.Variable );
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.tabControl.Controls.Add( this.sequencePage );
            this.tabControl.Controls.Add( this.tablePage );
            this.tabControl.Controls.Add( this.localTabPage );
            this.tabControl.Controls.Add( this.globalTabPage );
            this.tabControl.Location = new System.Drawing.Point( 10, 10 );
            this.tabControl.Margin = new System.Windows.Forms.Padding( 0 );
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size( 637, 295 );
            this.tabControl.TabIndex = 0;
            this.tabControl.SelectedIndexChanged += new System.EventHandler( this.OnTabPageChanged );
            // 
            // sequencePage
            // 
            this.sequencePage.Controls.Add( this.sequenceDataGrid );
            this.sequencePage.Controls.Add( this.sequenceCheckBox );
            this.sequencePage.Location = new System.Drawing.Point( 4, 22 );
            this.sequencePage.Name = "sequencePage";
            this.sequencePage.Padding = new System.Windows.Forms.Padding( 5 );
            this.sequencePage.Size = new System.Drawing.Size( 629, 269 );
            this.sequencePage.TabIndex = 0;
            this.sequencePage.Text = "Sequence variables";
            this.sequencePage.UseVisualStyleBackColor = true;
            // 
            // sequenceDataGrid
            // 
            this.sequenceDataGrid.AllowUserToAddRows = false;
            this.sequenceDataGrid.AllowUserToDeleteRows = false;
            this.sequenceDataGrid.AutoGenerateColumns = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 204 ) ) );
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.sequenceDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.sequenceDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.sequenceDataGrid.Columns.AddRange( new System.Windows.Forms.DataGridViewColumn[] {
            this.nameDataGridViewTextBoxColumn,
            this.valueDataGridViewTextBoxColumn,
            this.minDataGridViewTextBoxColumn,
            this.maxDataGridViewTextBoxColumn,
            this.stepDataGridViewTextBoxColumn,
            this.descriptionDataGridViewTextBoxColumn} );
            this.sequenceDataGrid.DataSource = this.sequenceSource;
            this.sequenceDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sequenceDataGrid.Location = new System.Drawing.Point( 5, 5 );
            this.sequenceDataGrid.Name = "sequenceDataGrid";
            this.sequenceDataGrid.RowHeadersVisible = false;
            this.sequenceDataGrid.RowTemplate.Height = 20;
            this.sequenceDataGrid.Size = new System.Drawing.Size( 619, 236 );
            this.sequenceDataGrid.TabIndex = 0;
            this.sequenceDataGrid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler( this.OnCellFormatting );
            this.sequenceDataGrid.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler( this.OnCellParsing );
            this.sequenceDataGrid.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler( this.OnCellValidating );
            this.sequenceDataGrid.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler( this.OnDataError );
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            this.nameDataGridViewTextBoxColumn.Frozen = true;
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            this.nameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // valueDataGridViewTextBoxColumn
            // 
            this.valueDataGridViewTextBoxColumn.DataPropertyName = "Value";
            this.valueDataGridViewTextBoxColumn.HeaderText = "Value";
            this.valueDataGridViewTextBoxColumn.Name = "valueDataGridViewTextBoxColumn";
            this.valueDataGridViewTextBoxColumn.Width = 75;
            // 
            // minDataGridViewTextBoxColumn
            // 
            this.minDataGridViewTextBoxColumn.DataPropertyName = "Min";
            this.minDataGridViewTextBoxColumn.HeaderText = "Min";
            this.minDataGridViewTextBoxColumn.Name = "minDataGridViewTextBoxColumn";
            this.minDataGridViewTextBoxColumn.Width = 75;
            // 
            // maxDataGridViewTextBoxColumn
            // 
            this.maxDataGridViewTextBoxColumn.DataPropertyName = "Max";
            this.maxDataGridViewTextBoxColumn.HeaderText = "Max";
            this.maxDataGridViewTextBoxColumn.Name = "maxDataGridViewTextBoxColumn";
            this.maxDataGridViewTextBoxColumn.Width = 75;
            // 
            // stepDataGridViewTextBoxColumn
            // 
            this.stepDataGridViewTextBoxColumn.DataPropertyName = "Step";
            this.stepDataGridViewTextBoxColumn.HeaderText = "Step";
            this.stepDataGridViewTextBoxColumn.Name = "stepDataGridViewTextBoxColumn";
            this.stepDataGridViewTextBoxColumn.Width = 75;
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            this.descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            this.descriptionDataGridViewTextBoxColumn.HeaderText = "Description";
            this.descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            this.descriptionDataGridViewTextBoxColumn.Width = 200;
            // 
            // sequenceCheckBox
            // 
            this.sequenceCheckBox.AccessibleName = "ShowFormulas";
            this.sequenceCheckBox.AccessibleRole = System.Windows.Forms.AccessibleRole.CheckButton;
            this.sequenceCheckBox.AutoSize = true;
            this.sequenceCheckBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.sequenceCheckBox.Location = new System.Drawing.Point( 5, 241 );
            this.sequenceCheckBox.Name = "sequenceCheckBox";
            this.sequenceCheckBox.Padding = new System.Windows.Forms.Padding( 7, 7, 6, 2 );
            this.sequenceCheckBox.Size = new System.Drawing.Size( 619, 23 );
            this.sequenceCheckBox.TabIndex = 1;
            this.sequenceCheckBox.UseVisualStyleBackColor = true;
            this.sequenceCheckBox.CheckedChanged += new System.EventHandler( this.OnCheckedChanged );
            // 
            // tablePage
            // 
            this.tablePage.Controls.Add( this.tableDataGrid );
            this.tablePage.Controls.Add( this.tableCheckBox );
            this.tablePage.Location = new System.Drawing.Point( 4, 22 );
            this.tablePage.Name = "tablePage";
            this.tablePage.Padding = new System.Windows.Forms.Padding( 5 );
            this.tablePage.Size = new System.Drawing.Size( 629, 269 );
            this.tablePage.TabIndex = 1;
            this.tablePage.Text = "Table variables";
            this.tablePage.UseVisualStyleBackColor = true;
            // 
            // tableDataGrid
            // 
            this.tableDataGrid.AllowUserToAddRows = false;
            this.tableDataGrid.AllowUserToDeleteRows = false;
            this.tableDataGrid.AutoGenerateColumns = false;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 204 ) ) );
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.tableDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.tableDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tableDataGrid.Columns.AddRange( new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6} );
            this.tableDataGrid.DataSource = this.tableSource;
            this.tableDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableDataGrid.Location = new System.Drawing.Point( 5, 5 );
            this.tableDataGrid.Name = "tableDataGrid";
            this.tableDataGrid.RowHeadersVisible = false;
            this.tableDataGrid.RowTemplate.Height = 20;
            this.tableDataGrid.Size = new System.Drawing.Size( 619, 236 );
            this.tableDataGrid.TabIndex = 2;
            this.tableDataGrid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler( this.OnCellFormatting );
            this.tableDataGrid.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler( this.OnCellParsing );
            this.tableDataGrid.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler( this.OnCellValidating );
            this.tableDataGrid.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler( this.OnDataError );
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Name";
            this.dataGridViewTextBoxColumn1.Frozen = true;
            this.dataGridViewTextBoxColumn1.HeaderText = "Name";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Value";
            this.dataGridViewTextBoxColumn2.HeaderText = "Value";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.Width = 75;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Min";
            this.dataGridViewTextBoxColumn3.HeaderText = "Min";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.Width = 75;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "Max";
            this.dataGridViewTextBoxColumn4.HeaderText = "Max";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 75;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "Step";
            this.dataGridViewTextBoxColumn5.HeaderText = "Step";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 75;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "Description";
            this.dataGridViewTextBoxColumn6.HeaderText = "Description";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.Width = 200;
            // 
            // tableSource
            // 
            this.tableSource.AllowNew = false;
            this.tableSource.DataSource = typeof( Calculations.Variables.Variable );
            // 
            // tableCheckBox
            // 
            this.tableCheckBox.AccessibleName = "ShowFormulas";
            this.tableCheckBox.AccessibleRole = System.Windows.Forms.AccessibleRole.CheckButton;
            this.tableCheckBox.AutoSize = true;
            this.tableCheckBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableCheckBox.Location = new System.Drawing.Point( 5, 241 );
            this.tableCheckBox.Name = "tableCheckBox";
            this.tableCheckBox.Padding = new System.Windows.Forms.Padding( 7, 7, 6, 2 );
            this.tableCheckBox.Size = new System.Drawing.Size( 619, 23 );
            this.tableCheckBox.TabIndex = 3;
            this.tableCheckBox.UseVisualStyleBackColor = true;
            this.tableCheckBox.CheckedChanged += new System.EventHandler( this.OnCheckedChanged );
            // 
            // localTabPage
            // 
            this.localTabPage.Controls.Add( this.localDataGrid );
            this.localTabPage.Controls.Add( this.localCheckBox );
            this.localTabPage.Location = new System.Drawing.Point( 4, 22 );
            this.localTabPage.Name = "localTabPage";
            this.localTabPage.Padding = new System.Windows.Forms.Padding( 5 );
            this.localTabPage.Size = new System.Drawing.Size( 629, 269 );
            this.localTabPage.TabIndex = 2;
            this.localTabPage.Text = "Local variables";
            this.localTabPage.UseVisualStyleBackColor = true;
            // 
            // localDataGrid
            // 
            this.localDataGrid.AllowUserToOrderColumns = true;
            this.localDataGrid.AutoGenerateColumns = false;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 204 ) ) );
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.localDataGrid.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.localDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.localDataGrid.Columns.AddRange( new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8,
            this.dataGridViewTextBoxColumn9,
            this.dataGridViewTextBoxColumn10,
            this.dataGridViewTextBoxColumn11,
            this.dataGridViewTextBoxColumn12} );
            this.localDataGrid.DataSource = this.localSource;
            this.localDataGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.localDataGrid.Location = new System.Drawing.Point( 5, 5 );
            this.localDataGrid.Name = "localDataGrid";
            this.localDataGrid.RowHeadersVisible = false;
            this.localDataGrid.RowTemplate.Height = 20;
            this.localDataGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.localDataGrid.Size = new System.Drawing.Size( 619, 236 );
            this.localDataGrid.TabIndex = 2;
            this.localDataGrid.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler( this.OnCellEndEdit );
            this.localDataGrid.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler( this.OnCellFormatting );
            this.localDataGrid.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler( this.OnCellParsing );
            this.localDataGrid.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler( this.OnCellValidating );
            this.localDataGrid.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler( this.OnDataError );
            this.localDataGrid.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler( this.OnRowValidating );
            this.localDataGrid.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler( this.OnUserDeletingRow );
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "Name";
            this.dataGridViewTextBoxColumn7.Frozen = true;
            this.dataGridViewTextBoxColumn7.HeaderText = "Name";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "Value";
            this.dataGridViewTextBoxColumn8.HeaderText = "Value";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.Width = 75;
            // 
            // dataGridViewTextBoxColumn9
            // 
            this.dataGridViewTextBoxColumn9.DataPropertyName = "Min";
            this.dataGridViewTextBoxColumn9.HeaderText = "Min";
            this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
            this.dataGridViewTextBoxColumn9.Width = 75;
            // 
            // dataGridViewTextBoxColumn10
            // 
            this.dataGridViewTextBoxColumn10.DataPropertyName = "Max";
            this.dataGridViewTextBoxColumn10.HeaderText = "Max";
            this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
            this.dataGridViewTextBoxColumn10.Width = 75;
            // 
            // dataGridViewTextBoxColumn11
            // 
            this.dataGridViewTextBoxColumn11.DataPropertyName = "Step";
            this.dataGridViewTextBoxColumn11.HeaderText = "Step";
            this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
            this.dataGridViewTextBoxColumn11.Width = 75;
            // 
            // dataGridViewTextBoxColumn12
            // 
            this.dataGridViewTextBoxColumn12.DataPropertyName = "Description";
            this.dataGridViewTextBoxColumn12.HeaderText = "Description";
            this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
            this.dataGridViewTextBoxColumn12.Width = 200;
            // 
            // localSource
            // 
            this.localSource.AllowNew = true;
            this.localSource.DataSource = typeof( Calculations.Variables.Variable );
            this.localSource.AddingNew += new System.ComponentModel.AddingNewEventHandler( this.OnLocalVariableAddingNew );
            this.localSource.ListChanged += new System.ComponentModel.ListChangedEventHandler( this.OnLocalSourceListChanged );
            // 
            // localCheckBox
            // 
            this.localCheckBox.AccessibleName = "ShowFormulas";
            this.localCheckBox.AccessibleRole = System.Windows.Forms.AccessibleRole.CheckButton;
            this.localCheckBox.AutoSize = true;
            this.localCheckBox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.localCheckBox.Location = new System.Drawing.Point( 5, 241 );
            this.localCheckBox.Name = "localCheckBox";
            this.localCheckBox.Padding = new System.Windows.Forms.Padding( 7, 7, 6, 2 );
            this.localCheckBox.Size = new System.Drawing.Size( 619, 23 );
            this.localCheckBox.TabIndex = 3;
            this.localCheckBox.UseVisualStyleBackColor = true;
            this.localCheckBox.CheckedChanged += new System.EventHandler( this.OnCheckedChanged );
            // 
            // globalTabPage
            // 
            this.globalTabPage.Controls.Add( this.dataGridView3 );
            this.globalTabPage.Location = new System.Drawing.Point( 4, 22 );
            this.globalTabPage.Name = "globalTabPage";
            this.globalTabPage.Padding = new System.Windows.Forms.Padding( 5 );
            this.globalTabPage.Size = new System.Drawing.Size( 629, 269 );
            this.globalTabPage.TabIndex = 3;
            this.globalTabPage.Text = "Global variables";
            this.globalTabPage.UseVisualStyleBackColor = true;
            // 
            // dataGridView3
            // 
            this.dataGridView3.AutoGenerateColumns = false;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 204 ) ) );
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView3.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Columns.AddRange( new System.Windows.Forms.DataGridViewColumn[] {
            this.Add,
            this.dataGridViewTextBoxColumn13,
            this.dataGridViewTextBoxColumn14,
            this.dataGridViewTextBoxColumn15,
            this.dataGridViewTextBoxColumn16,
            this.dataGridViewTextBoxColumn17,
            this.dataGridViewTextBoxColumn18} );
            this.dataGridView3.DataSource = this.globalSource;
            this.dataGridView3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView3.Location = new System.Drawing.Point( 5, 5 );
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.RowHeadersVisible = false;
            this.dataGridView3.RowTemplate.Height = 20;
            this.dataGridView3.Size = new System.Drawing.Size( 619, 259 );
            this.dataGridView3.TabIndex = 2;
            this.dataGridView3.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler( this.OnCellEndEdit );
            this.dataGridView3.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler( this.OnCellFormatting );
            this.dataGridView3.CellParsing += new System.Windows.Forms.DataGridViewCellParsingEventHandler( this.OnCellParsing );
            this.dataGridView3.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler( this.OnCellValidating );
            this.dataGridView3.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler( this.OnDataError );
            this.dataGridView3.RowValidating += new System.Windows.Forms.DataGridViewCellCancelEventHandler( this.OnRowValidating );
            this.dataGridView3.UserDeletingRow += new System.Windows.Forms.DataGridViewRowCancelEventHandler( this.OnUserDeletingRow );
            // 
            // Add
            // 
            this.Add.DataPropertyName = "IsAdd";
            this.Add.Frozen = true;
            this.Add.HeaderText = "Add";
            this.Add.Name = "Add";
            this.Add.Width = 30;
            // 
            // dataGridViewTextBoxColumn13
            // 
            this.dataGridViewTextBoxColumn13.DataPropertyName = "Name";
            this.dataGridViewTextBoxColumn13.Frozen = true;
            this.dataGridViewTextBoxColumn13.HeaderText = "Name";
            this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
            // 
            // dataGridViewTextBoxColumn14
            // 
            this.dataGridViewTextBoxColumn14.DataPropertyName = "Value";
            this.dataGridViewTextBoxColumn14.HeaderText = "Value";
            this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
            this.dataGridViewTextBoxColumn14.Width = 75;
            // 
            // dataGridViewTextBoxColumn15
            // 
            this.dataGridViewTextBoxColumn15.DataPropertyName = "Min";
            this.dataGridViewTextBoxColumn15.HeaderText = "Min";
            this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
            this.dataGridViewTextBoxColumn15.Width = 75;
            // 
            // dataGridViewTextBoxColumn16
            // 
            this.dataGridViewTextBoxColumn16.DataPropertyName = "Max";
            this.dataGridViewTextBoxColumn16.HeaderText = "Max";
            this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
            this.dataGridViewTextBoxColumn16.Width = 75;
            // 
            // dataGridViewTextBoxColumn17
            // 
            this.dataGridViewTextBoxColumn17.DataPropertyName = "Step";
            this.dataGridViewTextBoxColumn17.HeaderText = "Step";
            this.dataGridViewTextBoxColumn17.Name = "dataGridViewTextBoxColumn17";
            this.dataGridViewTextBoxColumn17.Width = 75;
            // 
            // dataGridViewTextBoxColumn18
            // 
            this.dataGridViewTextBoxColumn18.DataPropertyName = "Description";
            this.dataGridViewTextBoxColumn18.HeaderText = "Description";
            this.dataGridViewTextBoxColumn18.Name = "dataGridViewTextBoxColumn18";
            this.dataGridViewTextBoxColumn18.Width = 200;
            // 
            // globalSource
            // 
            this.globalSource.AllowNew = true;
            this.globalSource.DataSource = typeof( Calculations.Variables.GlobalVariable );
            this.globalSource.AddingNew += new System.ComponentModel.AddingNewEventHandler( this.OnGlobalVariableAddingNew );
            // 
            // cancelButton
            // 
            this.cancelButton.AccessibleName = "Cancel";
            this.cancelButton.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cancelButton.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point( 557, 312 );
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size( 90, 26 );
            this.cancelButton.TabIndex = 3;
            this.cancelButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.AccessibleName = "Ok";
            this.okButton.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.okButton.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point( 461, 312 );
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size( 90, 26 );
            this.okButton.TabIndex = 2;
            this.okButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler( this.okButton_Click );
            // 
            // saveButton
            // 
            this.saveButton.AccessibleName = "Save";
            this.saveButton.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.saveButton.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left ) ) );
            this.saveButton.Enabled = false;
            this.saveButton.Location = new System.Drawing.Point( 10, 312 );
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size( 90, 26 );
            this.saveButton.TabIndex = 4;
            this.saveButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler( this.OnSaveGlobalVariables );
            // 
            // VariableEditorForm
            // 
            this.AccessibleName = "VariableEditor";
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size( 657, 348 );
            this.Controls.Add( this.saveButton );
            this.Controls.Add( this.cancelButton );
            this.Controls.Add( this.okButton );
            this.Controls.Add( this.tabControl );
            this.Name = "VariableEditorForm";
            this.Padding = new System.Windows.Forms.Padding( 10 );
            this.Text = "VariableEditorForm";
            this.Load += new System.EventHandler( this.OnLoad );
            ( (System.ComponentModel.ISupportInitialize)( this.sequenceSource ) ).EndInit( );
            this.tabControl.ResumeLayout( false );
            this.sequencePage.ResumeLayout( false );
            this.sequencePage.PerformLayout( );
            ( (System.ComponentModel.ISupportInitialize)( this.sequenceDataGrid ) ).EndInit( );
            this.tablePage.ResumeLayout( false );
            this.tablePage.PerformLayout( );
            ( (System.ComponentModel.ISupportInitialize)( this.tableDataGrid ) ).EndInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.tableSource ) ).EndInit( );
            this.localTabPage.ResumeLayout( false );
            this.localTabPage.PerformLayout( );
            ( (System.ComponentModel.ISupportInitialize)( this.localDataGrid ) ).EndInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.localSource ) ).EndInit( );
            this.globalTabPage.ResumeLayout( false );
            ( (System.ComponentModel.ISupportInitialize)( this.dataGridView3 ) ).EndInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.globalSource ) ).EndInit( );
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.BindingSource sequenceSource;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.DataGridView sequenceDataGrid;
        private System.Windows.Forms.TabPage tablePage;
        private System.Windows.Forms.TabPage sequencePage;
        private System.Windows.Forms.TabPage localTabPage;
        private System.Windows.Forms.TabPage globalTabPage;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn valueDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn minDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn maxDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn stepDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private System.Windows.Forms.CheckBox sequenceCheckBox;
        private System.Windows.Forms.DataGridView tableDataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.CheckBox tableCheckBox;
        private System.Windows.Forms.DataGridView localDataGrid;
        private System.Windows.Forms.CheckBox localCheckBox;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.BindingSource tableSource;
        private System.Windows.Forms.BindingSource localSource;
        private System.Windows.Forms.BindingSource globalSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Add;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn17;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn18;
    }
}