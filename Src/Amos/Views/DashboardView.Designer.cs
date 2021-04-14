namespace Amos.Views
{
    partial class DashboardView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( DashboardView ) );
            this.toolStrip1 = new System.Windows.Forms.ToolStrip( );
            this.propertyGrid = new System.Windows.Forms.PropertyGrid( );
            this.toolStrip = new System.Windows.Forms.ToolStrip( );
            this.categorized = new System.Windows.Forms.ToolStripButton( );
            this.alphabetical = new System.Windows.Forms.ToolStripButton( );
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator( );
            this.customize = new System.Windows.Forms.ToolStripButton( );
            this.variableEditor = new System.Windows.Forms.ToolStripButton( );
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator( );
            this.valueMode = new System.Windows.Forms.ToolStripButton( );
            this.formulaMode = new System.Windows.Forms.ToolStripButton( );
            this.toolStrip.SuspendLayout( );
            this.SuspendLayout( );
            // 
            // toolStrip1
            // 
            this.toolStrip1.Location = new System.Drawing.Point( 0, 0 );
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size( 292, 25 );
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // propertyGrid
            // 
            this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid.Location = new System.Drawing.Point( 0, 29 );
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
            this.propertyGrid.Size = new System.Drawing.Size( 293, 392 );
            this.propertyGrid.TabIndex = 0;
            this.propertyGrid.ToolbarVisible = false;
            // 
            // toolStrip
            // 
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.ImageScalingSize = new System.Drawing.Size( 22, 22 );
            this.toolStrip.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.categorized,
            this.alphabetical,
            this.toolStripSeparator1,
            this.customize,
            this.variableEditor,
            this.toolStripSeparator2,
            this.valueMode,
            this.formulaMode} );
            this.toolStrip.Location = new System.Drawing.Point( 0, 0 );
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip.Size = new System.Drawing.Size( 293, 29 );
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip2";
            // 
            // categorized
            // 
            this.categorized.AccessibleName = "Dashboard.Categorized";
            this.categorized.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuItem;
            this.categorized.Checked = true;
            this.categorized.CheckState = System.Windows.Forms.CheckState.Checked;
            this.categorized.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.categorized.Image = ( (System.Drawing.Image)( resources.GetObject( "categorized.Image" ) ) );
            this.categorized.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.categorized.Name = "categorized";
            this.categorized.Size = new System.Drawing.Size( 26, 26 );
            this.categorized.Text = "toolStripButton1";
            this.categorized.Click += new System.EventHandler( this.OnCategorized );
            // 
            // alphabetical
            // 
            this.alphabetical.AccessibleName = "Dashboard.Alphabetical";
            this.alphabetical.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuItem;
            this.alphabetical.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.alphabetical.Image = ( (System.Drawing.Image)( resources.GetObject( "alphabetical.Image" ) ) );
            this.alphabetical.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.alphabetical.Name = "alphabetical";
            this.alphabetical.Size = new System.Drawing.Size( 26, 26 );
            this.alphabetical.Text = "toolStripButton1";
            this.alphabetical.Click += new System.EventHandler( this.OnAlphabetical );
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size( 6, 29 );
            // 
            // customize
            // 
            this.customize.AccessibleName = "Dashboard.Customize";
            this.customize.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuItem;
            this.customize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.customize.Image = ( (System.Drawing.Image)( resources.GetObject( "customize.Image" ) ) );
            this.customize.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.customize.Name = "customize";
            this.customize.Size = new System.Drawing.Size( 26, 26 );
            this.customize.Text = "toolStripButton1";
            this.customize.Click += new System.EventHandler( this.OnCustomize );
            // 
            // variableEditor
            // 
            this.variableEditor.AccessibleName = "Dashboard.VariableEditor";
            this.variableEditor.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuItem;
            this.variableEditor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.variableEditor.Image = ( (System.Drawing.Image)( resources.GetObject( "variableEditor.Image" ) ) );
            this.variableEditor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.variableEditor.Name = "variableEditor";
            this.variableEditor.Size = new System.Drawing.Size( 26, 26 );
            this.variableEditor.Text = "toolStripButton1";
            this.variableEditor.Click += new System.EventHandler( this.OnVariableEditor );
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size( 6, 29 );
            // 
            // valueMode
            // 
            this.valueMode.AccessibleName = "Dashboard.ValueMode";
            this.valueMode.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuItem;
            this.valueMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.valueMode.Image = ( (System.Drawing.Image)( resources.GetObject( "valueMode.Image" ) ) );
            this.valueMode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.valueMode.Name = "valueMode";
            this.valueMode.Size = new System.Drawing.Size( 26, 26 );
            this.valueMode.Text = "toolStripButton1";
            this.valueMode.Click += new System.EventHandler( this.OnValueMode );
            // 
            // formulaMode
            // 
            this.formulaMode.AccessibleName = "Dashboard.FormulaMode";
            this.formulaMode.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuItem;
            this.formulaMode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.formulaMode.Image = ( (System.Drawing.Image)( resources.GetObject( "formulaMode.Image" ) ) );
            this.formulaMode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.formulaMode.Name = "formulaMode";
            this.formulaMode.Size = new System.Drawing.Size( 26, 26 );
            this.formulaMode.Text = "toolStripButton2";
            this.formulaMode.Click += new System.EventHandler( this.OnFormulaMode );
            // 
            // DashboardView
            // 
            this.AccessibleName = "Dashboard";
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.ClientSize = new System.Drawing.Size( 293, 421 );
            this.Controls.Add( this.propertyGrid );
            this.Controls.Add( this.toolStrip );
            this.DockAreas = ( (WeifenLuo.WinFormsUI.Docking.DockAreas)( ( ( WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight )
                        | WeifenLuo.WinFormsUI.Docking.DockAreas.Document ) ) );
            this.Name = "DashboardView";
            this.ShowHint = WeifenLuo.WinFormsUI.Docking.DockState.DockRight;
            this.toolStrip.ResumeLayout( false );
            this.toolStrip.PerformLayout( );
            this.ResumeLayout( false );
            this.PerformLayout( );

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton categorized;
        private System.Windows.Forms.ToolStripButton alphabetical;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton customize;
        private System.Windows.Forms.ToolStripButton variableEditor;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton valueMode;
        private System.Windows.Forms.ToolStripButton formulaMode;
    }
}