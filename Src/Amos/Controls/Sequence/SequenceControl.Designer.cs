namespace Amos.Controls.Sequence
{
    partial class SequenceControl
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
                components.Dispose();
            }
            base.Dispose( disposing );
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Обязательный метод для поддержки конструктора - не изменяйте 
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.zoomComboBox = new System.Windows.Forms.ComboBox( );
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar( );
            this.hScrollBar = new System.Windows.Forms.HScrollBar( );
            this.delimeterButton = new System.Windows.Forms.Panel( );
            this.splitContainer = new System.Windows.Forms.SplitContainer( );
            this.rightPanel = new System.Windows.Forms.Panel( );
            this.vScrollBar2 = new System.Windows.Forms.VScrollBar( );
            this.panel1 = new Amos.Controls.Sequence.SequencePanel( );
            this.panel2 = new Amos.Controls.Sequence.SequencePanel( );
            ( (System.ComponentModel.ISupportInitialize)( this.splitContainer ) ).BeginInit( );
            this.splitContainer.Panel1.SuspendLayout( );
            this.splitContainer.Panel2.SuspendLayout( );
            this.splitContainer.SuspendLayout( );
            this.rightPanel.SuspendLayout( );
            this.SuspendLayout( );
            // 
            // zoomComboBox
            // 
            this.zoomComboBox.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left ) ) );
            this.zoomComboBox.BackColor = System.Drawing.SystemColors.Window;
            this.zoomComboBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.zoomComboBox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.zoomComboBox.Items.AddRange( new object[] {
            "25 %",
            "50 %",
            "75 %",
            "100 %",
            "150 %",
            "200 %",
            "400 %"} );
            this.zoomComboBox.Location = new System.Drawing.Point( 0, 352 );
            this.zoomComboBox.Name = "zoomComboBox";
            this.zoomComboBox.Size = new System.Drawing.Size( 60, 21 );
            this.zoomComboBox.TabIndex = 0;
            this.zoomComboBox.TabStop = false;
            this.zoomComboBox.SelectedIndexChanged += new System.EventHandler( this.zoomComboBox_SelectedIndexChanged );
            this.zoomComboBox.KeyDown += new System.Windows.Forms.KeyEventHandler( this.zoomComboBox_KeyDown );
            this.zoomComboBox.KeyUp += new System.Windows.Forms.KeyEventHandler( this.zoomComboBox_KeyUp );
            this.zoomComboBox.Leave += new System.EventHandler( this.zoomComboBox_Leave );
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vScrollBar1.LargeChange = 85;
            this.vScrollBar1.Location = new System.Drawing.Point( 0, 17 );
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size( 17, 156 );
            this.vScrollBar1.TabIndex = 1;
            this.vScrollBar1.Value = 65;
            // 
            // hScrollBar
            // 
            this.hScrollBar.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.hScrollBar.Location = new System.Drawing.Point( 61, 352 );
            this.hScrollBar.Name = "hScrollBar";
            this.hScrollBar.Size = new System.Drawing.Size( 548, 17 );
            this.hScrollBar.TabIndex = 0;
            this.hScrollBar.ValueChanged += new System.EventHandler( this.OnRefresh );
            // 
            // delimeterButton
            // 
            this.delimeterButton.BackColor = System.Drawing.Color.Transparent;
            this.delimeterButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.delimeterButton.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.delimeterButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.delimeterButton.Location = new System.Drawing.Point( 0, 0 );
            this.delimeterButton.Margin = new System.Windows.Forms.Padding( 0 );
            this.delimeterButton.Name = "delimeterButton";
            this.delimeterButton.Size = new System.Drawing.Size( 17, 17 );
            this.delimeterButton.TabIndex = 0;
            this.delimeterButton.MouseDown += new System.Windows.Forms.MouseEventHandler( this.delimeterButton_MouseDown );
            this.delimeterButton.MouseMove += new System.Windows.Forms.MouseEventHandler( this.delimeterButton_MouseMove );
            this.delimeterButton.MouseUp += new System.Windows.Forms.MouseEventHandler( this.delimeterButton_MouseUp );
            // 
            // splitContainer
            // 
            this.splitContainer.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.splitContainer.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer.Location = new System.Drawing.Point( 0, 0 );
            this.splitContainer.Margin = new System.Windows.Forms.Padding( 0 );
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add( this.panel1 );
            this.splitContainer.Panel1.Controls.Add( this.rightPanel );
            this.splitContainer.Panel1MinSize = 2;
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add( this.panel2 );
            this.splitContainer.Panel2.Controls.Add( this.vScrollBar2 );
            this.splitContainer.Panel2MinSize = 50;
            this.splitContainer.Size = new System.Drawing.Size( 626, 352 );
            this.splitContainer.SplitterDistance = 173;
            this.splitContainer.TabIndex = 0;
            this.splitContainer.SplitterMoved += new System.Windows.Forms.SplitterEventHandler( this.splitContainer_SplitterMoved );
            this.splitContainer.Paint += new System.Windows.Forms.PaintEventHandler( this.splitContainer_Paint );
            // 
            // rightPanel
            // 
            this.rightPanel.Controls.Add( this.vScrollBar1 );
            this.rightPanel.Controls.Add( this.delimeterButton );
            this.rightPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.rightPanel.Location = new System.Drawing.Point( 609, 0 );
            this.rightPanel.Margin = new System.Windows.Forms.Padding( 0 );
            this.rightPanel.Name = "rightPanel";
            this.rightPanel.Size = new System.Drawing.Size( 17, 173 );
            this.rightPanel.TabIndex = 4;
            // 
            // vScrollBar2
            // 
            this.vScrollBar2.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.vScrollBar2.LargeChange = 85;
            this.vScrollBar2.Location = new System.Drawing.Point( 609, 0 );
            this.vScrollBar2.Name = "vScrollBar2";
            this.vScrollBar2.Size = new System.Drawing.Size( 17, 175 );
            this.vScrollBar2.TabIndex = 1;
            this.vScrollBar2.Value = 65;
            // 
            // panel1
            // 
            this.panel1.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.IndexDelimeter = 0;
            this.panel1.IndexRow = 1;
            this.panel1.isDelimeter = false;
            this.panel1.Location = new System.Drawing.Point( 0, 0 );
            this.panel1.Margin = new System.Windows.Forms.Padding( 0 );
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size( 609, 173 );
            this.panel1.TabIndex = 0;
            this.panel1.TabStop = true;
            this.panel1.Enter += new System.EventHandler( this.panel_Enter );
            // 
            // panel2
            // 
            this.panel2.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.IndexDelimeter = 0;
            this.panel2.IndexRow = 1;
            this.panel2.isDelimeter = false;
            this.panel2.Location = new System.Drawing.Point( 0, 0 );
            this.panel2.Margin = new System.Windows.Forms.Padding( 0 );
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size( 609, 175 );
            this.panel2.TabIndex = 0;
            this.panel2.TabStop = true;
            this.panel2.Enter += new System.EventHandler( this.panel_Enter );
            // 
            // SequenceControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add( this.splitContainer );
            this.Controls.Add( this.hScrollBar );
            this.Controls.Add( this.zoomComboBox );
            this.DoubleBuffered = true;
            this.Name = "SequenceControl";
            this.Size = new System.Drawing.Size( 626, 372 );
            this.Resize += new System.EventHandler( this.OnRefresh );
            this.splitContainer.Panel1.ResumeLayout( false );
            this.splitContainer.Panel2.ResumeLayout( false );
            ( (System.ComponentModel.ISupportInitialize)( this.splitContainer ) ).EndInit( );
            this.splitContainer.ResumeLayout( false );
            this.rightPanel.ResumeLayout( false );
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.ComboBox zoomComboBox;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.HScrollBar hScrollBar;
        private System.Windows.Forms.Panel delimeterButton;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.VScrollBar vScrollBar2;
        private System.Windows.Forms.Panel rightPanel;
        private SequencePanel panel1;
        private SequencePanel panel2;
    }
}
