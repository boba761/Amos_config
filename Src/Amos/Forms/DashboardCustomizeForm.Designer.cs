namespace Amos.Forms
{
    partial class DashboardCustomizeForm
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
            System.Windows.Forms.Panel panel1;
            System.Windows.Forms.Panel panel2;
            System.Windows.Forms.Panel panel4;
            System.Windows.Forms.Panel panel3;
            this.saveButton = new System.Windows.Forms.Button( );
            this.openButton = new System.Windows.Forms.Button( );
            this.label3 = new System.Windows.Forms.Label( );
            this.filesComboBox = new System.Windows.Forms.ComboBox( );
            this.insertButton = new System.Windows.Forms.Button( );
            this.addButton = new System.Windows.Forms.Button( );
            this.downButton = new System.Windows.Forms.Button( );
            this.upButton = new System.Windows.Forms.Button( );
            this.moveButton = new System.Windows.Forms.Button( );
            this.deleteButton = new System.Windows.Forms.Button( );
            this.editButton = new System.Windows.Forms.Button( );
            this.currentTree = new System.Windows.Forms.TreeView( );
            this.label2 = new System.Windows.Forms.Label( );
            this.masterTree = new System.Windows.Forms.TreeView( );
            this.label1 = new System.Windows.Forms.Label( );
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog( );
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog( );
            this.okButton = new System.Windows.Forms.Button( );
            this.cancelButton = new System.Windows.Forms.Button( );
            panel1 = new System.Windows.Forms.Panel( );
            panel2 = new System.Windows.Forms.Panel( );
            panel4 = new System.Windows.Forms.Panel( );
            panel3 = new System.Windows.Forms.Panel( );
            panel1.SuspendLayout( );
            panel2.SuspendLayout( );
            panel4.SuspendLayout( );
            panel3.SuspendLayout( );
            this.SuspendLayout( );
            // 
            // panel1
            // 
            panel1.Controls.Add( this.saveButton );
            panel1.Controls.Add( this.openButton );
            panel1.Controls.Add( this.label3 );
            panel1.Controls.Add( this.filesComboBox );
            panel1.Dock = System.Windows.Forms.DockStyle.Top;
            panel1.Location = new System.Drawing.Point( 10, 358 );
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size( 496, 40 );
            panel1.TabIndex = 2;
            // 
            // saveButton
            // 
            this.saveButton.AccessibleName = "Save";
            this.saveButton.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.saveButton.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.saveButton.Location = new System.Drawing.Point( 407, 8 );
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size( 90, 26 );
            this.saveButton.TabIndex = 2;
            this.saveButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler( this.saveButton_Click );
            // 
            // openButton
            // 
            this.openButton.AccessibleName = "Open";
            this.openButton.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.openButton.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.openButton.Location = new System.Drawing.Point( 311, 8 );
            this.openButton.Name = "openButton";
            this.openButton.Size = new System.Drawing.Size( 90, 26 );
            this.openButton.TabIndex = 1;
            this.openButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.openButton.UseVisualStyleBackColor = true;
            this.openButton.Click += new System.EventHandler( this.openButton_Click );
            // 
            // label3
            // 
            this.label3.AccessibleName = "OpenDashboardFile";
            this.label3.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
            this.label3.Location = new System.Drawing.Point( 0, 10 );
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size( 120, 21 );
            this.label3.TabIndex = 3;
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // filesComboBox
            // 
            this.filesComboBox.FormattingEnabled = true;
            this.filesComboBox.Location = new System.Drawing.Point( 125, 10 );
            this.filesComboBox.Name = "filesComboBox";
            this.filesComboBox.Size = new System.Drawing.Size( 175, 21 );
            this.filesComboBox.TabIndex = 0;
            this.filesComboBox.SelectedIndexChanged += new System.EventHandler( this.filesComboBox_SelectedIndexChanged );
            // 
            // panel2
            // 
            panel2.Controls.Add( this.insertButton );
            panel2.Controls.Add( this.addButton );
            panel2.Controls.Add( this.downButton );
            panel2.Controls.Add( this.upButton );
            panel2.Controls.Add( this.moveButton );
            panel2.Controls.Add( this.deleteButton );
            panel2.Controls.Add( this.editButton );
            panel2.Controls.Add( panel4 );
            panel2.Controls.Add( panel3 );
            panel2.Dock = System.Windows.Forms.DockStyle.Top;
            panel2.Location = new System.Drawing.Point( 10, 10 );
            panel2.Name = "panel2";
            panel2.Size = new System.Drawing.Size( 496, 348 );
            panel2.TabIndex = 3;
            // 
            // insertButton
            // 
            this.insertButton.AccessibleName = "InsertDashboard";
            this.insertButton.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.insertButton.Enabled = false;
            this.insertButton.FlatAppearance.BorderSize = 0;
            this.insertButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.insertButton.Location = new System.Drawing.Point( 235, 86 );
            this.insertButton.Name = "insertButton";
            this.insertButton.Size = new System.Drawing.Size( 26, 26 );
            this.insertButton.TabIndex = 8;
            this.insertButton.UseVisualStyleBackColor = true;
            this.insertButton.Click += new System.EventHandler( this.insertButton_Click );
            // 
            // addButton
            // 
            this.addButton.AccessibleName = "AddDashboard";
            this.addButton.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.addButton.FlatAppearance.BorderSize = 0;
            this.addButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addButton.Location = new System.Drawing.Point( 235, 54 );
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size( 26, 26 );
            this.addButton.TabIndex = 7;
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler( this.addButton_Click );
            // 
            // downButton
            // 
            this.downButton.AccessibleName = "DownDashboard";
            this.downButton.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.downButton.Enabled = false;
            this.downButton.FlatAppearance.BorderSize = 0;
            this.downButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.downButton.Location = new System.Drawing.Point( 235, 214 );
            this.downButton.Name = "downButton";
            this.downButton.Size = new System.Drawing.Size( 26, 26 );
            this.downButton.TabIndex = 6;
            this.downButton.UseVisualStyleBackColor = true;
            this.downButton.Click += new System.EventHandler( this.downButton_Click );
            // 
            // upButton
            // 
            this.upButton.AccessibleName = "UpDashboard";
            this.upButton.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.upButton.Enabled = false;
            this.upButton.FlatAppearance.BorderSize = 0;
            this.upButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.upButton.Location = new System.Drawing.Point( 235, 182 );
            this.upButton.Name = "upButton";
            this.upButton.Size = new System.Drawing.Size( 26, 26 );
            this.upButton.TabIndex = 5;
            this.upButton.UseVisualStyleBackColor = true;
            this.upButton.Click += new System.EventHandler( this.upButton_Click );
            // 
            // moveButton
            // 
            this.moveButton.AccessibleName = "MoveDashboard";
            this.moveButton.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.moveButton.Enabled = false;
            this.moveButton.FlatAppearance.BorderSize = 0;
            this.moveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.moveButton.Location = new System.Drawing.Point( 235, 150 );
            this.moveButton.Name = "moveButton";
            this.moveButton.Size = new System.Drawing.Size( 26, 26 );
            this.moveButton.TabIndex = 4;
            this.moveButton.UseVisualStyleBackColor = true;
            this.moveButton.Click += new System.EventHandler( this.moveButton_Click );
            // 
            // deleteButton
            // 
            this.deleteButton.AccessibleName = "DeleteDashboard";
            this.deleteButton.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.deleteButton.Enabled = false;
            this.deleteButton.FlatAppearance.BorderSize = 0;
            this.deleteButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.deleteButton.Location = new System.Drawing.Point( 235, 118 );
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size( 26, 26 );
            this.deleteButton.TabIndex = 3;
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler( this.deleteButton_Click );
            // 
            // editButton
            // 
            this.editButton.AccessibleName = "EditDashboard";
            this.editButton.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.editButton.Enabled = false;
            this.editButton.FlatAppearance.BorderSize = 0;
            this.editButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.editButton.Location = new System.Drawing.Point( 235, 22 );
            this.editButton.Name = "editButton";
            this.editButton.Size = new System.Drawing.Size( 26, 26 );
            this.editButton.TabIndex = 2;
            this.editButton.UseVisualStyleBackColor = true;
            this.editButton.Click += new System.EventHandler( this.editButton_Click );
            // 
            // panel4
            // 
            panel4.Controls.Add( this.currentTree );
            panel4.Controls.Add( this.label2 );
            panel4.Dock = System.Windows.Forms.DockStyle.Right;
            panel4.Location = new System.Drawing.Point( 266, 0 );
            panel4.Name = "panel4";
            panel4.Size = new System.Drawing.Size( 230, 348 );
            panel4.TabIndex = 1;
            // 
            // currentTree
            // 
            this.currentTree.AllowDrop = true;
            this.currentTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.currentTree.HideSelection = false;
            this.currentTree.LabelEdit = true;
            this.currentTree.Location = new System.Drawing.Point( 0, 22 );
            this.currentTree.Name = "currentTree";
            this.currentTree.Size = new System.Drawing.Size( 230, 326 );
            this.currentTree.TabIndex = 1;
            this.currentTree.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler( this.currentTree_BeforeLabelEdit );
            this.currentTree.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler( this.currentTree_AfterLabelEdit );
            this.currentTree.ItemDrag += new System.Windows.Forms.ItemDragEventHandler( this.currentTree_ItemDrag );
            this.currentTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler( this.currentTree_AfterSelect );
            this.currentTree.DragDrop += new System.Windows.Forms.DragEventHandler( this.currentTree_DragDrop );
            this.currentTree.DragEnter += new System.Windows.Forms.DragEventHandler( this.currentTree_DragEnter );
            this.currentTree.DragOver += new System.Windows.Forms.DragEventHandler( this.currentTree_DragOver );
            this.currentTree.KeyUp += new System.Windows.Forms.KeyEventHandler( this.currentTree_KeyUp );
            // 
            // label2
            // 
            this.label2.AccessibleName = "CurrentNewDashboard";
            this.label2.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Dock = System.Windows.Forms.DockStyle.Top;
            this.label2.Location = new System.Drawing.Point( 0, 0 );
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size( 230, 22 );
            this.label2.TabIndex = 2;
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            panel3.Controls.Add( this.masterTree );
            panel3.Controls.Add( this.label1 );
            panel3.Dock = System.Windows.Forms.DockStyle.Left;
            panel3.Location = new System.Drawing.Point( 0, 0 );
            panel3.Name = "panel3";
            panel3.Size = new System.Drawing.Size( 230, 348 );
            panel3.TabIndex = 0;
            // 
            // masterTree
            // 
            this.masterTree.AllowDrop = true;
            this.masterTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.masterTree.HideSelection = false;
            this.masterTree.Location = new System.Drawing.Point( 0, 22 );
            this.masterTree.Name = "masterTree";
            this.masterTree.Size = new System.Drawing.Size( 230, 326 );
            this.masterTree.TabIndex = 0;
            this.masterTree.ItemDrag += new System.Windows.Forms.ItemDragEventHandler( this.masterTree_ItemDrag );
            this.masterTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler( this.masterTree_AfterSelect );
            // 
            // label1
            // 
            this.label1.AccessibleName = "MasterDashboard";
            this.label1.AccessibleRole = System.Windows.Forms.AccessibleRole.Text;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point( 0, 0 );
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size( 230, 22 );
            this.label1.TabIndex = 1;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.InitialDirectory = "./Dashboard";
            this.saveFileDialog.RestoreDirectory = true;
            // 
            // openFileDialog
            // 
            this.openFileDialog.RestoreDirectory = true;
            // 
            // okButton
            // 
            this.okButton.AccessibleName = "Ok";
            this.okButton.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.okButton.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point( 321, 403 );
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size( 90, 26 );
            this.okButton.TabIndex = 0;
            this.okButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler( this.okButton_Click );
            // 
            // cancelButton
            // 
            this.cancelButton.AccessibleName = "Cancel";
            this.cancelButton.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cancelButton.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point( 417, 403 );
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size( 90, 26 );
            this.cancelButton.TabIndex = 1;
            this.cancelButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // DashboardCustomizeForm
            // 
            this.AcceptButton = this.okButton;
            this.AccessibleName = "DashboardCustomize";
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size( 516, 442 );
            this.Controls.Add( panel1 );
            this.Controls.Add( this.cancelButton );
            this.Controls.Add( this.okButton );
            this.Controls.Add( panel2 );
            this.Name = "DashboardCustomizeForm";
            this.Padding = new System.Windows.Forms.Padding( 10 );
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler( this.DashboardCustomizeForm_FormClosing );
            panel1.ResumeLayout( false );
            panel2.ResumeLayout( false );
            panel4.ResumeLayout( false );
            panel3.ResumeLayout( false );
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.TreeView masterTree;
        private System.Windows.Forms.TreeView currentTree;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox filesComboBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Button openButton;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Button editButton;
        private System.Windows.Forms.Button downButton;
        private System.Windows.Forms.Button upButton;
        private System.Windows.Forms.Button moveButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button insertButton;
    }
}