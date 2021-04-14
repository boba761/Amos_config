namespace Amos.Controls.Signal
{
    partial class SignalControl
    {
        /// <summary> 
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose( bool disposing )
        {
            if ( disposing && ( components != null ) )
            {
                components.Dispose( );
            }
            base.Dispose( disposing );
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Обязательный метод для поддержки конструктора - не изменяйте 
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent( )
        {
            this.scrollBar = new System.Windows.Forms.HScrollBar( );
            this.point1DLabel = new System.Windows.Forms.Label( );
            this.mouselabel = new System.Windows.Forms.Label( );
            this.panel1 = new System.Windows.Forms.Panel( );
            this.smallBox = new Amos.Controls.Signal.SmallSignalPanel( );
            this.largeBox = new Amos.Controls.Signal.LargeSignalPanel( );
            this.cupsorPointLabel = new System.Windows.Forms.Label( );
            this.panel1.SuspendLayout( );
            this.SuspendLayout( );
            // 
            // scrollBar
            // 
            this.scrollBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.scrollBar.Location = new System.Drawing.Point( 2, 436 );
            this.scrollBar.Name = "scrollBar";
            this.scrollBar.Size = new System.Drawing.Size( 826, 19 );
            this.scrollBar.TabIndex = 1;
            this.scrollBar.ValueChanged += new System.EventHandler( this.scrollBar_ValueChanged );
            // 
            // point1DLabel
            // 
            this.point1DLabel.AutoSize = true;
            this.point1DLabel.BackColor = System.Drawing.Color.Transparent;
            this.point1DLabel.Location = new System.Drawing.Point( 3, 3 );
            this.point1DLabel.Margin = new System.Windows.Forms.Padding( 1 );
            this.point1DLabel.Name = "point1DLabel";
            this.point1DLabel.Size = new System.Drawing.Size( 48, 13 );
            this.point1DLabel.TabIndex = 3;
            this.point1DLabel.Text = "Point 1D";
            // 
            // mouselabel
            // 
            this.mouselabel.AutoSize = true;
            this.mouselabel.BackColor = System.Drawing.Color.Transparent;
            this.mouselabel.Location = new System.Drawing.Point( 130, 3 );
            this.mouselabel.Name = "mouselabel";
            this.mouselabel.Size = new System.Drawing.Size( 42, 13 );
            this.mouselabel.TabIndex = 9;
            this.mouselabel.Text = "Mouse:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add( this.smallBox );
            this.panel1.Controls.Add( this.cupsorPointLabel );
            this.panel1.Controls.Add( this.largeBox );
            this.panel1.Controls.Add( this.scrollBar );
            this.panel1.Controls.Add( this.mouselabel );
            this.panel1.Controls.Add( this.point1DLabel );
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point( 0, 0 );
            this.panel1.Margin = new System.Windows.Forms.Padding( 0 );
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding( 2 );
            this.panel1.Size = new System.Drawing.Size( 830, 457 );
            this.panel1.TabIndex = 10;
            // 
            // smallBox
            // 
            this.smallBox.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.smallBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.smallBox.Location = new System.Drawing.Point( 578, 2 );
            this.smallBox.Margin = new System.Windows.Forms.Padding( 0, 0, 0, 2 );
            this.smallBox.Name = "smallBox";
            this.smallBox.SignalPaint = null;
            this.smallBox.Size = new System.Drawing.Size( 250, 62 );
            this.smallBox.TabIndex = 7;
            // 
            // largeBox
            // 
            this.largeBox.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.largeBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.largeBox.Location = new System.Drawing.Point( 2, 66 );
            this.largeBox.Margin = new System.Windows.Forms.Padding( 0 );
            this.largeBox.Name = "largeBox";
            this.largeBox.SignalPaint = null;
            this.largeBox.Size = new System.Drawing.Size( 826, 370 );
            this.largeBox.TabIndex = 8;
            this.largeBox.MouseDown += new System.Windows.Forms.MouseEventHandler( this.largeBox_MouseDown );
            this.largeBox.MouseLeave += new System.EventHandler( this.largeBox_MouseLeave );
            this.largeBox.MouseMove += new System.Windows.Forms.MouseEventHandler( this.largeBox_MouseMove );
            this.largeBox.MouseUp += new System.Windows.Forms.MouseEventHandler( this.largeBox_MouseUp );
            // 
            // cupsorPointlabel
            // 
            this.cupsorPointLabel.AutoSize = true;
            this.cupsorPointLabel.Location = new System.Drawing.Point( 260, 3 );
            this.cupsorPointLabel.Name = "cupsorPointlabel";
            this.cupsorPointLabel.Size = new System.Drawing.Size( 63, 13 );
            this.cupsorPointLabel.TabIndex = 10;
            this.cupsorPointLabel.Text = "Cursor point";
            // 
            // SignalControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add( this.panel1 );
            this.Margin = new System.Windows.Forms.Padding( 0 );
            this.Name = "SignalControl";
            this.Size = new System.Drawing.Size( 830, 457 );
            this.panel1.ResumeLayout( false );
            this.panel1.PerformLayout( );
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.HScrollBar scrollBar;
        private System.Windows.Forms.Label point1DLabel;
        private SmallSignalPanel smallBox;
        private LargeSignalPanel largeBox;
        private System.Windows.Forms.Label mouselabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label cupsorPointLabel;
    }
}
