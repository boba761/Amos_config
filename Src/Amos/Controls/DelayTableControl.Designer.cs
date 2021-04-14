namespace Amos.Controls
{
    partial class DelayTableControl
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
            this.descriptionLabel = new System.Windows.Forms.Label( );
            this.groupBox1 = new System.Windows.Forms.GroupBox( );
            this.label2 = new System.Windows.Forms.Label( );
            this.useDwellTimeCheckBox = new System.Windows.Forms.CheckBox( );
            this.valueTextBox = new System.Windows.Forms.TextBox( );
            this.groupBox2 = new System.Windows.Forms.GroupBox( );
            this.addComboBox = new System.Windows.Forms.ComboBox( );
            this.everyComboBox = new System.Windows.Forms.ComboBox( );
            this.groupBox1.SuspendLayout( );
            this.groupBox2.SuspendLayout( );
            this.SuspendLayout( );
            // 
            // descriptionLabel
            // 
            this.descriptionLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.descriptionLabel.Location = new System.Drawing.Point( 10, 10 );
            this.descriptionLabel.Margin = new System.Windows.Forms.Padding( 0, 0, 0, 3 );
            this.descriptionLabel.Name = "descriptionLabel";
            this.descriptionLabel.Size = new System.Drawing.Size( 350, 21 );
            this.descriptionLabel.TabIndex = 0;
            this.descriptionLabel.Text = "The initial value is entered in the 1D event";
            this.descriptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.groupBox1.Controls.Add( this.label2 );
            this.groupBox1.Controls.Add( this.useDwellTimeCheckBox );
            this.groupBox1.Controls.Add( this.valueTextBox );
            this.groupBox1.Location = new System.Drawing.Point( 13, 38 );
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding( 10, 5, 10, 5 );
            this.groupBox1.Size = new System.Drawing.Size( 168, 79 );
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Increment Value";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point( 13, 23 );
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size( 37, 13 );
            this.label2.TabIndex = 2;
            this.label2.Text = "Value:";
            // 
            // useDwellTimeCheckBox
            // 
            this.useDwellTimeCheckBox.AutoSize = true;
            this.useDwellTimeCheckBox.Location = new System.Drawing.Point( 16, 49 );
            this.useDwellTimeCheckBox.Name = "useDwellTimeCheckBox";
            this.useDwellTimeCheckBox.Size = new System.Drawing.Size( 100, 17 );
            this.useDwellTimeCheckBox.TabIndex = 1;
            this.useDwellTimeCheckBox.Text = "Use Dwell Time";
            this.useDwellTimeCheckBox.UseVisualStyleBackColor = true;
            this.useDwellTimeCheckBox.CheckedChanged += new System.EventHandler( this.useDwellTimeCheckBox_CheckedChanged );
            this.useDwellTimeCheckBox.Leave += new System.EventHandler( this.OnLeave );
            // 
            // valueTextBox
            // 
            this.valueTextBox.Location = new System.Drawing.Point( 67, 20 );
            this.valueTextBox.Name = "valueTextBox";
            this.valueTextBox.Size = new System.Drawing.Size( 88, 20 );
            this.valueTextBox.TabIndex = 0;
            this.valueTextBox.Leave += new System.EventHandler( this.OnLeave );
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
                        | System.Windows.Forms.AnchorStyles.Left )
                        | System.Windows.Forms.AnchorStyles.Right ) ) );
            this.groupBox2.Controls.Add( this.addComboBox );
            this.groupBox2.Controls.Add( this.everyComboBox );
            this.groupBox2.Location = new System.Drawing.Point( 189, 38 );
            this.groupBox2.Margin = new System.Windows.Forms.Padding( 5 );
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding( 10, 5, 10, 5 );
            this.groupBox2.Size = new System.Drawing.Size( 166, 79 );
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Increment Scheme";
            // 
            // addComboBox
            // 
            this.addComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.addComboBox.FormattingEnabled = true;
            this.addComboBox.Items.AddRange( new object[] {
            "+ Add"} );
            this.addComboBox.Location = new System.Drawing.Point( 13, 47 );
            this.addComboBox.Name = "addComboBox";
            this.addComboBox.Size = new System.Drawing.Size( 140, 21 );
            this.addComboBox.TabIndex = 1;
            this.addComboBox.SelectedIndexChanged += new System.EventHandler( this.addComboBox_SelectedIndexChanged );
            this.addComboBox.Leave += new System.EventHandler( this.OnLeave );
            // 
            // everyComboBox
            // 
            this.everyComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.everyComboBox.FormattingEnabled = true;
            this.everyComboBox.Items.AddRange( new object[] {
            "Every pass",
            "Every 2 passes"} );
            this.everyComboBox.Location = new System.Drawing.Point( 13, 20 );
            this.everyComboBox.Name = "everyComboBox";
            this.everyComboBox.Size = new System.Drawing.Size( 140, 21 );
            this.everyComboBox.TabIndex = 0;
            this.everyComboBox.SelectedIndexChanged += new System.EventHandler( this.everyComboBox_SelectedIndexChanged );
            this.everyComboBox.Leave += new System.EventHandler( this.OnLeave );
            // 
            // DelayTableControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add( this.groupBox2 );
            this.Controls.Add( this.groupBox1 );
            this.Controls.Add( this.descriptionLabel );
            this.Name = "DelayTableControl";
            this.Padding = new System.Windows.Forms.Padding( 10 );
            this.Size = new System.Drawing.Size( 370, 130 );
            this.VisibleChanged += new System.EventHandler( this.OnVisibleChanged );
            this.Enter += new System.EventHandler( this.OnVisibleChanged );
            this.Leave += new System.EventHandler( this.OnLeave );
            this.groupBox1.ResumeLayout( false );
            this.groupBox1.PerformLayout( );
            this.groupBox2.ResumeLayout( false );
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.Label descriptionLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox useDwellTimeCheckBox;
        private System.Windows.Forms.TextBox valueTextBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox addComboBox;
        private System.Windows.Forms.ComboBox everyComboBox;
    }
}
