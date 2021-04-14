namespace Amos.Forms
{
    partial class AcquisitionPropertieForm
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
            System.Windows.Forms.Label eventNumberLabel;
            System.Windows.Forms.Label asqPointsLabel;
            System.Windows.Forms.Label sweepLabel;
            System.Windows.Forms.Label filterLabel;
            System.Windows.Forms.Label dwellLabel;
            System.Windows.Forms.Label label6;
            this.eventNumber = new System.Windows.Forms.TextBox( );
            this.asqPoints = new System.Windows.Forms.TextBox( );
            this.sweep = new System.Windows.Forms.TextBox( );
            this.filter = new System.Windows.Forms.TextBox( );
            this.linkDashboard = new System.Windows.Forms.CheckBox( );
            this.dwell = new System.Windows.Forms.TextBox( );
            this.asqTime = new System.Windows.Forms.TextBox( );
            this.cancelButton = new System.Windows.Forms.Button( );
            this.okButton = new System.Windows.Forms.Button( );
            eventNumberLabel = new System.Windows.Forms.Label( );
            asqPointsLabel = new System.Windows.Forms.Label( );
            sweepLabel = new System.Windows.Forms.Label( );
            filterLabel = new System.Windows.Forms.Label( );
            dwellLabel = new System.Windows.Forms.Label( );
            label6 = new System.Windows.Forms.Label( );
            this.SuspendLayout( );
            // 
            // eventNumberLabel
            // 
            eventNumberLabel.AutoSize = true;
            eventNumberLabel.Location = new System.Drawing.Point( 13, 15 );
            eventNumberLabel.Name = "eventNumberLabel";
            eventNumberLabel.Size = new System.Drawing.Size( 76, 13 );
            eventNumberLabel.TabIndex = 2;
            eventNumberLabel.Text = "Event number:";
            // 
            // asqPointsLabel
            // 
            asqPointsLabel.AutoSize = true;
            asqPointsLabel.Location = new System.Drawing.Point( 13, 41 );
            asqPointsLabel.Name = "asqPointsLabel";
            asqPointsLabel.Size = new System.Drawing.Size( 63, 13 );
            asqPointsLabel.TabIndex = 4;
            asqPointsLabel.Text = "Acq. points:";
            // 
            // sweepLabel
            // 
            sweepLabel.AutoSize = true;
            sweepLabel.Location = new System.Drawing.Point( 13, 67 );
            sweepLabel.Name = "sweepLabel";
            sweepLabel.Size = new System.Drawing.Size( 45, 13 );
            sweepLabel.TabIndex = 6;
            sweepLabel.Text = "SW +/-:";
            // 
            // filterLabel
            // 
            filterLabel.AutoSize = true;
            filterLabel.Location = new System.Drawing.Point( 13, 94 );
            filterLabel.Name = "filterLabel";
            filterLabel.Size = new System.Drawing.Size( 32, 13 );
            filterLabel.TabIndex = 8;
            filterLabel.Text = "Filter:";
            // 
            // dwellLabel
            // 
            dwellLabel.AutoSize = true;
            dwellLabel.Location = new System.Drawing.Point( 13, 121 );
            dwellLabel.Name = "dwellLabel";
            dwellLabel.Size = new System.Drawing.Size( 62, 13 );
            dwellLabel.TabIndex = 10;
            dwellLabel.Text = "Dwell Time:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point( 13, 147 );
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size( 58, 13 );
            label6.TabIndex = 12;
            label6.Text = "Acq. Time:";
            // 
            // eventNumber
            // 
            this.eventNumber.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.eventNumber.Location = new System.Drawing.Point( 96, 12 );
            this.eventNumber.Name = "eventNumber";
            this.eventNumber.ReadOnly = true;
            this.eventNumber.Size = new System.Drawing.Size( 100, 20 );
            this.eventNumber.TabIndex = 3;
            this.eventNumber.TabStop = false;
            // 
            // asqPoints
            // 
            this.asqPoints.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.asqPoints.Location = new System.Drawing.Point( 96, 38 );
            this.asqPoints.Name = "asqPoints";
            this.asqPoints.Size = new System.Drawing.Size( 100, 20 );
            this.asqPoints.TabIndex = 5;
            this.asqPoints.Leave += new System.EventHandler( this.asqPoints_Leave );
            // 
            // sweep
            // 
            this.sweep.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.sweep.Location = new System.Drawing.Point( 96, 64 );
            this.sweep.Name = "sweep";
            this.sweep.Size = new System.Drawing.Size( 100, 20 );
            this.sweep.TabIndex = 7;
            this.sweep.Leave += new System.EventHandler( this.sweep_Leave );
            // 
            // filter
            // 
            this.filter.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.filter.Location = new System.Drawing.Point( 96, 90 );
            this.filter.Name = "filter";
            this.filter.ReadOnly = true;
            this.filter.Size = new System.Drawing.Size( 100, 20 );
            this.filter.TabIndex = 9;
            // 
            // linkDashboard
            // 
            this.linkDashboard.AutoSize = true;
            this.linkDashboard.Checked = true;
            this.linkDashboard.CheckState = System.Windows.Forms.CheckState.Checked;
            this.linkDashboard.Location = new System.Drawing.Point( 23, 175 );
            this.linkDashboard.Name = "linkDashboard";
            this.linkDashboard.Size = new System.Drawing.Size( 113, 17 );
            this.linkDashboard.TabIndex = 14;
            this.linkDashboard.Text = "Link to Dashboard";
            this.linkDashboard.UseVisualStyleBackColor = true;
            this.linkDashboard.CheckedChanged += new System.EventHandler( this.linkDashboard_CheckedChanged );
            // 
            // dwell
            // 
            this.dwell.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.dwell.Location = new System.Drawing.Point( 96, 118 );
            this.dwell.Name = "dwell";
            this.dwell.Size = new System.Drawing.Size( 100, 20 );
            this.dwell.TabIndex = 11;
            this.dwell.Leave += new System.EventHandler( this.dwell_Leave );
            // 
            // asqTime
            // 
            this.asqTime.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.asqTime.Location = new System.Drawing.Point( 96, 144 );
            this.asqTime.Name = "asqTime";
            this.asqTime.ReadOnly = true;
            this.asqTime.Size = new System.Drawing.Size( 100, 20 );
            this.asqTime.TabIndex = 13;
            this.asqTime.TabStop = false;
            // 
            // cancelButton
            // 
            this.cancelButton.AccessibleName = "Cancel";
            this.cancelButton.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.cancelButton.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point( 106, 204 );
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size( 90, 26 );
            this.cancelButton.TabIndex = 1;
            this.cancelButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.AccessibleName = "Ok";
            this.okButton.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.okButton.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.Location = new System.Drawing.Point( 10, 204 );
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size( 90, 26 );
            this.okButton.TabIndex = 0;
            this.okButton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.okButton.UseVisualStyleBackColor = true;
            // 
            // AcquisitionPropertieForm
            // 
            this.AcceptButton = this.okButton;
            this.AccessibleName = "AcqustionProperties";
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size( 209, 243 );
            this.Controls.Add( this.cancelButton );
            this.Controls.Add( this.okButton );
            this.Controls.Add( this.asqTime );
            this.Controls.Add( this.dwell );
            this.Controls.Add( this.linkDashboard );
            this.Controls.Add( label6 );
            this.Controls.Add( this.filter );
            this.Controls.Add( this.sweep );
            this.Controls.Add( this.asqPoints );
            this.Controls.Add( dwellLabel );
            this.Controls.Add( filterLabel );
            this.Controls.Add( sweepLabel );
            this.Controls.Add( asqPointsLabel );
            this.Controls.Add( this.eventNumber );
            this.Controls.Add( eventNumberLabel );
            this.Name = "AcquisitionPropertieForm";
            this.Padding = new System.Windows.Forms.Padding( 10, 15, 10, 10 );
            this.ShowIcon = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler( this.OnFormClosing );
            this.Load += new System.EventHandler( this.AcquisitionPropertieForm_Load );
            this.ResumeLayout( false );
            this.PerformLayout( );

        }

        #endregion

        private System.Windows.Forms.TextBox eventNumber;
        private System.Windows.Forms.TextBox asqPoints;
        private System.Windows.Forms.TextBox sweep;
        private System.Windows.Forms.TextBox filter;
        private System.Windows.Forms.CheckBox linkDashboard;
        private System.Windows.Forms.TextBox dwell;
        private System.Windows.Forms.TextBox asqTime;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
    }
}