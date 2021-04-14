namespace Amos.Forms
{
    partial class TableEditForm
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
            this.okButton = new System.Windows.Forms.Button( );
            this.cancelButton = new System.Windows.Forms.Button( );
            this.valuesTextBox = new System.Windows.Forms.TextBox( );
            this.allTablesListBox = new System.Windows.Forms.ListBox( );
            this.listAllTablesLabel = new System.Windows.Forms.Label( );
            this.showAllTablesCheckBox = new System.Windows.Forms.CheckBox( );
            this.autoCheckBox = new System.Windows.Forms.CheckBox( );
            this.discriptionLabel = new System.Windows.Forms.Label( );
            this.stepTextBox = new System.Windows.Forms.TextBox( );
            this.stepRadio = new System.Windows.Forms.RadioButton( );
            this.digreesRadio = new System.Windows.Forms.RadioButton( );
            this.timePointTextBox = new System.Windows.Forms.TextBox( );
            this.timePointLabel = new System.Windows.Forms.Label( );
            this.typeCollectionLabel = new System.Windows.Forms.Label( );
            this.label1 = new System.Windows.Forms.Label( );
            this.nameTextBox = new System.Windows.Forms.TextBox( );
            this.editPanel = new System.Windows.Forms.Panel( );
            this.editPanel.SuspendLayout( );
            this.SuspendLayout( );
            // 
            // okButton
            // 
            this.okButton.AccessibleName = "Ok";
            this.okButton.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.okButton.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point( 197, 205 );
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size( 90, 26 );
            this.okButton.TabIndex = 0;
            this.okButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.AccessibleName = "Cancel";
            this.cancelButton.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cancelButton.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point( 293, 205 );
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size( 90, 26 );
            this.cancelButton.TabIndex = 1;
            this.cancelButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // valuesTextBox
            // 
            this.valuesTextBox.AcceptsReturn = true;
            this.valuesTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.valuesTextBox.Location = new System.Drawing.Point( 0, 0 );
            this.valuesTextBox.Multiline = true;
            this.valuesTextBox.Name = "valuesTextBox";
            this.valuesTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.valuesTextBox.Size = new System.Drawing.Size( 370, 131 );
            this.valuesTextBox.TabIndex = 11;
            this.valuesTextBox.TextChanged += new System.EventHandler( this.OnTextChanged );
            // 
            // allTablesListBox
            // 
            this.allTablesListBox.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.allTablesListBox.FormattingEnabled = true;
            this.allTablesListBox.IntegralHeight = false;
            this.allTablesListBox.Location = new System.Drawing.Point( 13, 266 );
            this.allTablesListBox.Name = "allTablesListBox";
            this.allTablesListBox.ScrollAlwaysVisible = true;
            this.allTablesListBox.Size = new System.Drawing.Size( 370, 163 );
            this.allTablesListBox.Sorted = true;
            this.allTablesListBox.TabIndex = 15;
            this.allTablesListBox.SelectedIndexChanged += new System.EventHandler( this.allTablesListBox_SelectedIndexChanged );
            // 
            // listAllTablesLabel
            // 
            this.listAllTablesLabel.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.listAllTablesLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listAllTablesLabel.Location = new System.Drawing.Point( 13, 244 );
            this.listAllTablesLabel.Margin = new System.Windows.Forms.Padding( 3, 10, 3, 0 );
            this.listAllTablesLabel.Name = "listAllTablesLabel";
            this.listAllTablesLabel.Size = new System.Drawing.Size( 370, 21 );
            this.listAllTablesLabel.TabIndex = 14;
            this.listAllTablesLabel.Text = "List of All Tables";
            this.listAllTablesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // showAllTablesCheckBox
            // 
            this.showAllTablesCheckBox.AutoSize = true;
            this.showAllTablesCheckBox.Location = new System.Drawing.Point( 87, 210 );
            this.showAllTablesCheckBox.Name = "showAllTablesCheckBox";
            this.showAllTablesCheckBox.Size = new System.Drawing.Size( 102, 17 );
            this.showAllTablesCheckBox.TabIndex = 13;
            this.showAllTablesCheckBox.Text = "Show All Tables";
            this.showAllTablesCheckBox.UseVisualStyleBackColor = true;
            this.showAllTablesCheckBox.CheckedChanged += new System.EventHandler( this.showAllTablesCheckBox_CheckedChanged );
            // 
            // autoCheckBox
            // 
            this.autoCheckBox.AutoSize = true;
            this.autoCheckBox.Location = new System.Drawing.Point( 16, 210 );
            this.autoCheckBox.Name = "autoCheckBox";
            this.autoCheckBox.Size = new System.Drawing.Size( 48, 17 );
            this.autoCheckBox.TabIndex = 12;
            this.autoCheckBox.Text = "Auto";
            this.autoCheckBox.UseVisualStyleBackColor = true;
            this.autoCheckBox.CheckedChanged += new System.EventHandler( this.autoCheckBox_CheckedChanged );
            // 
            // discriptionLabel
            // 
            this.discriptionLabel.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.discriptionLabel.Location = new System.Drawing.Point( 289, 13 );
            this.discriptionLabel.Name = "discriptionLabel";
            this.discriptionLabel.Size = new System.Drawing.Size( 94, 45 );
            this.discriptionLabel.TabIndex = 10;
            this.discriptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // stepTextBox
            // 
            this.stepTextBox.Location = new System.Drawing.Point( 243, 38 );
            this.stepTextBox.Name = "stepTextBox";
            this.stepTextBox.Size = new System.Drawing.Size( 40, 20 );
            this.stepTextBox.TabIndex = 9;
            // 
            // stepRadio
            // 
            this.stepRadio.AutoSize = true;
            this.stepRadio.Location = new System.Drawing.Point( 190, 40 );
            this.stepRadio.Name = "stepRadio";
            this.stepRadio.Size = new System.Drawing.Size( 47, 17 );
            this.stepRadio.TabIndex = 8;
            this.stepRadio.TabStop = true;
            this.stepRadio.Text = "Step";
            this.stepRadio.UseVisualStyleBackColor = true;
            this.stepRadio.CheckedChanged += new System.EventHandler( this.stepRadio_CheckedChanged );
            // 
            // digreesRadio
            // 
            this.digreesRadio.AutoSize = true;
            this.digreesRadio.Location = new System.Drawing.Point( 190, 15 );
            this.digreesRadio.Name = "digreesRadio";
            this.digreesRadio.Size = new System.Drawing.Size( 65, 17 );
            this.digreesRadio.TabIndex = 7;
            this.digreesRadio.TabStop = true;
            this.digreesRadio.Text = "Degrees";
            this.digreesRadio.UseVisualStyleBackColor = true;
            this.digreesRadio.CheckedChanged += new System.EventHandler( this.digreesRadio_CheckedChanged );
            // 
            // timePointTextBox
            // 
            this.timePointTextBox.Location = new System.Drawing.Point( 87, 38 );
            this.timePointTextBox.Name = "timePointTextBox";
            this.timePointTextBox.Size = new System.Drawing.Size( 45, 20 );
            this.timePointTextBox.TabIndex = 5;
            // 
            // timePointLabel
            // 
            this.timePointLabel.AutoSize = true;
            this.timePointLabel.Location = new System.Drawing.Point( 13, 41 );
            this.timePointLabel.Name = "timePointLabel";
            this.timePointLabel.Size = new System.Drawing.Size( 62, 13 );
            this.timePointLabel.TabIndex = 4;
            this.timePointLabel.Text = "Time/Point:";
            // 
            // typeCollectionLabel
            // 
            this.typeCollectionLabel.Location = new System.Drawing.Point( 135, 13 );
            this.typeCollectionLabel.Name = "typeCollectionLabel";
            this.typeCollectionLabel.Size = new System.Drawing.Size( 51, 20 );
            this.typeCollectionLabel.TabIndex = 6;
            this.typeCollectionLabel.Text = "1D";
            this.typeCollectionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point( 13, 16 );
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size( 38, 13 );
            this.label1.TabIndex = 2;
            this.label1.Text = "Name:";
            // 
            // nameTextBox
            // 
            this.nameTextBox.Location = new System.Drawing.Point( 57, 13 );
            this.nameTextBox.Name = "nameTextBox";
            this.nameTextBox.ReadOnly = true;
            this.nameTextBox.Size = new System.Drawing.Size( 75, 20 );
            this.nameTextBox.TabIndex = 3;
            this.nameTextBox.TabStop = false;
            // 
            // editPanel
            // 
            this.editPanel.Controls.Add( this.valuesTextBox );
            this.editPanel.Location = new System.Drawing.Point( 13, 64 );
            this.editPanel.Margin = new System.Windows.Forms.Padding( 3, 3, 3, 7 );
            this.editPanel.Name = "editPanel";
            this.editPanel.Size = new System.Drawing.Size( 370, 131 );
            this.editPanel.TabIndex = 16;
            // 
            // TableEditForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size( 396, 442 );
            this.Controls.Add( this.editPanel );
            this.Controls.Add( this.allTablesListBox );
            this.Controls.Add( this.listAllTablesLabel );
            this.Controls.Add( this.showAllTablesCheckBox );
            this.Controls.Add( this.autoCheckBox );
            this.Controls.Add( this.discriptionLabel );
            this.Controls.Add( this.stepTextBox );
            this.Controls.Add( this.stepRadio );
            this.Controls.Add( this.digreesRadio );
            this.Controls.Add( this.timePointTextBox );
            this.Controls.Add( this.timePointLabel );
            this.Controls.Add( this.typeCollectionLabel );
            this.Controls.Add( this.cancelButton );
            this.Controls.Add( this.okButton );
            this.Controls.Add( this.label1 );
            this.Controls.Add( this.nameTextBox );
            this.Name = "TableEditForm";
            this.Padding = new System.Windows.Forms.Padding( 10 );
            this.Text = "TableEditForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler( this.TableEditForm_FormClosing );
            this.editPanel.ResumeLayout( false );
            this.editPanel.PerformLayout( );
            this.ResumeLayout( false );
            this.PerformLayout( );

        }

        #endregion

        private System.Windows.Forms.TextBox nameTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label typeCollectionLabel;
        private System.Windows.Forms.Label timePointLabel;
        private System.Windows.Forms.TextBox timePointTextBox;
        private System.Windows.Forms.RadioButton digreesRadio;
        private System.Windows.Forms.RadioButton stepRadio;
        private System.Windows.Forms.TextBox stepTextBox;
        private System.Windows.Forms.Label discriptionLabel;
        private System.Windows.Forms.CheckBox autoCheckBox;
        private System.Windows.Forms.CheckBox showAllTablesCheckBox;
        private System.Windows.Forms.Label listAllTablesLabel;
        private System.Windows.Forms.ListBox allTablesListBox;
        private System.Windows.Forms.TextBox valuesTextBox;
        private System.Windows.Forms.Panel editPanel;
    }
}