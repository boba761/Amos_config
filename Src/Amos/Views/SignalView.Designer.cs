namespace Amos.Views
{
    partial class SignalView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager( typeof( SignalView ) );
            this.toolStripPanel1 = new System.Windows.Forms.ToolStripPanel( );
            this.panel1 = new System.Windows.Forms.Panel( );
            this.panel2 = new System.Windows.Forms.Panel( );
            this.pos4D = new System.Windows.Forms.NumericUpDown( );
            this.label4D = new System.Windows.Forms.Label( );
            this.pos3D = new System.Windows.Forms.NumericUpDown( );
            this.label3D = new System.Windows.Forms.Label( );
            this.pos2D = new System.Windows.Forms.NumericUpDown( );
            this.label2D = new System.Windows.Forms.Label( );
            this.panel3 = new System.Windows.Forms.Panel( );
            this.toolStrip = new System.Windows.Forms.ToolStrip( );
            this.realToolStrip = new System.Windows.Forms.ToolStripButton( );
            this.imagToolStrip = new System.Windows.Forms.ToolStripButton( );
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator( );
            this.zoomInToolStrip = new System.Windows.Forms.ToolStripButton( );
            this.zoomOutToolStrip = new System.Windows.Forms.ToolStripButton( );
            this.signalControl = new Amos.Controls.Signal.SignalControl( );
            this.panel1.SuspendLayout( );
            this.panel2.SuspendLayout( );
            ( (System.ComponentModel.ISupportInitialize)( this.pos4D ) ).BeginInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.pos3D ) ).BeginInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.pos2D ) ).BeginInit( );
            this.panel3.SuspendLayout( );
            this.toolStrip.SuspendLayout( );
            this.SuspendLayout( );
            // 
            // toolStripPanel1
            // 
            this.toolStripPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStripPanel1.Location = new System.Drawing.Point( 0, 31 );
            this.toolStripPanel1.Name = "toolStripPanel1";
            this.toolStripPanel1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.toolStripPanel1.Padding = new System.Windows.Forms.Padding( 1 );
            this.toolStripPanel1.RowMargin = new System.Windows.Forms.Padding( 0, 3, 0, 0 );
            this.toolStripPanel1.Size = new System.Drawing.Size( 0, 424 );
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb( ( (int)( ( (byte)( 188 ) ) ) ), ( (int)( ( (byte)( 199 ) ) ) ), ( (int)( ( (byte)( 216 ) ) ) ) );
            this.panel1.Controls.Add( this.panel2 );
            this.panel1.Controls.Add( this.panel3 );
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point( 0, 0 );
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size( 786, 31 );
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb( ( (int)( ( (byte)( 188 ) ) ) ), ( (int)( ( (byte)( 199 ) ) ) ), ( (int)( ( (byte)( 216 ) ) ) ) );
            this.panel2.Controls.Add( this.pos4D );
            this.panel2.Controls.Add( this.label4D );
            this.panel2.Controls.Add( this.pos3D );
            this.panel2.Controls.Add( this.label3D );
            this.panel2.Controls.Add( this.pos2D );
            this.panel2.Controls.Add( this.label2D );
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point( 121, 0 );
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size( 665, 31 );
            this.panel2.TabIndex = 4;
            // 
            // pos4D
            // 
            this.pos4D.Location = new System.Drawing.Point( 182, 5 );
            this.pos4D.Minimum = new decimal( new int[] {
            1,
            0,
            0,
            0} );
            this.pos4D.Name = "pos4D";
            this.pos4D.Size = new System.Drawing.Size( 45, 20 );
            this.pos4D.TabIndex = 5;
            this.pos4D.Value = new decimal( new int[] {
            1,
            0,
            0,
            0} );
            this.pos4D.ValueChanged += new System.EventHandler( this.pos4D_ValueChanged );
            // 
            // label4D
            // 
            this.label4D.AutoSize = true;
            this.label4D.Location = new System.Drawing.Point( 155, 9 );
            this.label4D.Margin = new System.Windows.Forms.Padding( 0 );
            this.label4D.Name = "label4D";
            this.label4D.Size = new System.Drawing.Size( 24, 13 );
            this.label4D.TabIndex = 4;
            this.label4D.Text = "4D:";
            this.label4D.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pos3D
            // 
            this.pos3D.Location = new System.Drawing.Point( 107, 5 );
            this.pos3D.Minimum = new decimal( new int[] {
            1,
            0,
            0,
            0} );
            this.pos3D.Name = "pos3D";
            this.pos3D.Size = new System.Drawing.Size( 45, 20 );
            this.pos3D.TabIndex = 3;
            this.pos3D.Value = new decimal( new int[] {
            1,
            0,
            0,
            0} );
            this.pos3D.ValueChanged += new System.EventHandler( this.pos3D_ValueChanged );
            // 
            // label3D
            // 
            this.label3D.AutoSize = true;
            this.label3D.Location = new System.Drawing.Point( 80, 9 );
            this.label3D.Margin = new System.Windows.Forms.Padding( 0 );
            this.label3D.Name = "label3D";
            this.label3D.Size = new System.Drawing.Size( 24, 13 );
            this.label3D.TabIndex = 2;
            this.label3D.Text = "3D:";
            this.label3D.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pos2D
            // 
            this.pos2D.Location = new System.Drawing.Point( 32, 5 );
            this.pos2D.Minimum = new decimal( new int[] {
            1,
            0,
            0,
            0} );
            this.pos2D.Name = "pos2D";
            this.pos2D.Size = new System.Drawing.Size( 45, 20 );
            this.pos2D.TabIndex = 1;
            this.pos2D.Value = new decimal( new int[] {
            1,
            0,
            0,
            0} );
            this.pos2D.ValueChanged += new System.EventHandler( this.pos2D_ValueChanged );
            // 
            // label2D
            // 
            this.label2D.AutoSize = true;
            this.label2D.BackColor = System.Drawing.Color.Transparent;
            this.label2D.Location = new System.Drawing.Point( 5, 9 );
            this.label2D.Margin = new System.Windows.Forms.Padding( 0 );
            this.label2D.Name = "label2D";
            this.label2D.Size = new System.Drawing.Size( 24, 13 );
            this.label2D.TabIndex = 0;
            this.label2D.Text = "2D:";
            this.label2D.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel3
            // 
            this.panel3.AutoSize = true;
            this.panel3.BackColor = System.Drawing.Color.FromArgb( ( (int)( ( (byte)( 188 ) ) ) ), ( (int)( ( (byte)( 199 ) ) ) ), ( (int)( ( (byte)( 216 ) ) ) ) );
            this.panel3.Controls.Add( this.toolStrip );
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point( 0, 0 );
            this.panel3.Margin = new System.Windows.Forms.Padding( 0 );
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding( 1 );
            this.panel3.Size = new System.Drawing.Size( 121, 31 );
            this.panel3.TabIndex = 5;
            // 
            // toolStrip
            // 
            this.toolStrip.BackColor = System.Drawing.Color.FromArgb( ( (int)( ( (byte)( 188 ) ) ) ), ( (int)( ( (byte)( 199 ) ) ) ), ( (int)( ( (byte)( 216 ) ) ) ) );
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip.ImageScalingSize = new System.Drawing.Size( 22, 22 );
            this.toolStrip.Items.AddRange( new System.Windows.Forms.ToolStripItem[] {
            this.realToolStrip,
            this.imagToolStrip,
            this.toolStripSeparator1,
            this.zoomInToolStrip,
            this.zoomOutToolStrip} );
            this.toolStrip.Location = new System.Drawing.Point( 2, 2 );
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Padding = new System.Windows.Forms.Padding( 3, 0, 3, 0 );
            this.toolStrip.Size = new System.Drawing.Size( 118, 29 );
            this.toolStrip.TabIndex = 0;
            // 
            // realToolStrip
            // 
            this.realToolStrip.AccessibleName = "Real";
            this.realToolStrip.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuItem;
            this.realToolStrip.Checked = true;
            this.realToolStrip.CheckState = System.Windows.Forms.CheckState.Checked;
            this.realToolStrip.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.realToolStrip.Image = ( (System.Drawing.Image)( resources.GetObject( "realToolStrip.Image" ) ) );
            this.realToolStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.realToolStrip.Name = "realToolStrip";
            this.realToolStrip.Size = new System.Drawing.Size( 26, 26 );
            this.realToolStrip.Text = "toolStripButton1";
            this.realToolStrip.Click += new System.EventHandler( this.OnToolStripClick );
            // 
            // imagToolStrip
            // 
            this.imagToolStrip.AccessibleName = "Imaginary";
            this.imagToolStrip.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuItem;
            this.imagToolStrip.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.imagToolStrip.Image = ( (System.Drawing.Image)( resources.GetObject( "imagToolStrip.Image" ) ) );
            this.imagToolStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.imagToolStrip.Name = "imagToolStrip";
            this.imagToolStrip.Size = new System.Drawing.Size( 26, 26 );
            this.imagToolStrip.Text = "toolStripButton2";
            this.imagToolStrip.Click += new System.EventHandler( this.OnToolStripClick );
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size( 6, 29 );
            // 
            // zoomInToolStrip
            // 
            this.zoomInToolStrip.AccessibleName = "ZoomIn";
            this.zoomInToolStrip.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuItem;
            this.zoomInToolStrip.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.zoomInToolStrip.Image = ( (System.Drawing.Image)( resources.GetObject( "zoomInToolStrip.Image" ) ) );
            this.zoomInToolStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.zoomInToolStrip.Name = "zoomInToolStrip";
            this.zoomInToolStrip.Size = new System.Drawing.Size( 26, 26 );
            this.zoomInToolStrip.Text = "toolStripButton1";
            this.zoomInToolStrip.Click += new System.EventHandler( this.zoomInToolStrip_Click );
            // 
            // zoomOutToolStrip
            // 
            this.zoomOutToolStrip.AccessibleName = "ZoomOut";
            this.zoomOutToolStrip.AccessibleRole = System.Windows.Forms.AccessibleRole.MenuItem;
            this.zoomOutToolStrip.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.zoomOutToolStrip.Image = ( (System.Drawing.Image)( resources.GetObject( "zoomOutToolStrip.Image" ) ) );
            this.zoomOutToolStrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.zoomOutToolStrip.Name = "zoomOutToolStrip";
            this.zoomOutToolStrip.Size = new System.Drawing.Size( 26, 26 );
            this.zoomOutToolStrip.Text = "toolStripButton2";
            this.zoomOutToolStrip.Click += new System.EventHandler( this.zoomOutToolStrip_Click );
            // 
            // signalControl
            // 
            this.signalControl.BackColor = System.Drawing.Color.White;
            this.signalControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.signalControl.IsImag = false;
            this.signalControl.IsReal = true;
            this.signalControl.Location = new System.Drawing.Point( 0, 31 );
            this.signalControl.Margin = new System.Windows.Forms.Padding( 0 );
            this.signalControl.Name = "signalControl";
            this.signalControl.Size = new System.Drawing.Size( 786, 424 );
            this.signalControl.TabIndex = 0;
            this.signalControl.Zoom = 1F;
            // 
            // SignalView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb( ( (int)( ( (byte)( 188 ) ) ) ), ( (int)( ( (byte)( 199 ) ) ) ), ( (int)( ( (byte)( 216 ) ) ) ) );
            this.ClientSize = new System.Drawing.Size( 786, 455 );
            this.Controls.Add( this.toolStripPanel1 );
            this.Controls.Add( this.signalControl );
            this.Controls.Add( this.panel1 );
            this.DoubleBuffered = true;
            this.Name = "SignalView";
            this.TabText = "Signal";
            this.Text = "Signal";
            this.panel1.ResumeLayout( false );
            this.panel1.PerformLayout( );
            this.panel2.ResumeLayout( false );
            this.panel2.PerformLayout( );
            ( (System.ComponentModel.ISupportInitialize)( this.pos4D ) ).EndInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.pos3D ) ).EndInit( );
            ( (System.ComponentModel.ISupportInitialize)( this.pos2D ) ).EndInit( );
            this.panel3.ResumeLayout( false );
            this.panel3.PerformLayout( );
            this.toolStrip.ResumeLayout( false );
            this.toolStrip.PerformLayout( );
            this.ResumeLayout( false );
            this.PerformLayout( );

        }

        #endregion

        private Controls.Signal.SignalControl signalControl;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.ToolStripButton realToolStrip;
        private System.Windows.Forms.ToolStripButton imagToolStrip;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.NumericUpDown pos4D;
        private System.Windows.Forms.Label label4D;
        private System.Windows.Forms.NumericUpDown pos3D;
        private System.Windows.Forms.Label label3D;
        private System.Windows.Forms.NumericUpDown pos2D;
        private System.Windows.Forms.Label label2D;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton zoomInToolStrip;
        private System.Windows.Forms.ToolStripButton zoomOutToolStrip;
        private System.Windows.Forms.ToolStripPanel toolStripPanel1;

    }
}