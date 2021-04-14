namespace Amos.Controls
{
    partial class GradientAmplitudeControl
    {
        /// <summary> 
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Обязательный метод для поддержки конструктора - не изменяйте 
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label( );
            this.startTextBox = new System.Windows.Forms.TextBox( );
            this.incrementTextBox = new System.Windows.Forms.TextBox( );
            this.label2 = new System.Windows.Forms.Label( );
            this.numberTextBox = new System.Windows.Forms.TextBox( );
            this.label3 = new System.Windows.Forms.Label( );
            this.groupBox1 = new System.Windows.Forms.GroupBox( );
            this.groupBox1.SuspendLayout( );
            this.SuspendLayout( );
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point( 65, 35 );
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size( 32, 13 );
            this.label1.TabIndex = 0;
            this.label1.Text = "Start:";
            // 
            // startTextBox
            // 
            this.startTextBox.Location = new System.Drawing.Point( 141, 32 );
            this.startTextBox.Name = "startTextBox";
            this.startTextBox.Size = new System.Drawing.Size( 166, 20 );
            this.startTextBox.TabIndex = 1;
            this.startTextBox.Leave += new System.EventHandler( this.OnLeave );
            // 
            // incrementTextBox
            // 
            this.incrementTextBox.Location = new System.Drawing.Point( 141, 58 );
            this.incrementTextBox.Name = "incrementTextBox";
            this.incrementTextBox.Size = new System.Drawing.Size( 166, 20 );
            this.incrementTextBox.TabIndex = 3;
            this.incrementTextBox.Leave += new System.EventHandler( this.OnLeave );
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point( 65, 61 );
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size( 57, 13 );
            this.label2.TabIndex = 2;
            this.label2.Text = "Increment:";
            // 
            // numberTextBox
            // 
            this.numberTextBox.Location = new System.Drawing.Point( 141, 84 );
            this.numberTextBox.Name = "numberTextBox";
            this.numberTextBox.Size = new System.Drawing.Size( 166, 20 );
            this.numberTextBox.TabIndex = 5;
            this.numberTextBox.Leave += new System.EventHandler( this.OnLeave );
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point( 65, 87 );
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size( 47, 13 );
            this.label3.TabIndex = 4;
            this.label3.Text = "Number:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add( this.startTextBox );
            this.groupBox1.Controls.Add( this.numberTextBox );
            this.groupBox1.Controls.Add( this.label1 );
            this.groupBox1.Controls.Add( this.label3 );
            this.groupBox1.Controls.Add( this.label2 );
            this.groupBox1.Controls.Add( this.incrementTextBox );
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point( 0, 0 );
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size( 370, 130 );
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Auto Generate Table";
            this.groupBox1.VisibleChanged += new System.EventHandler( this.OnVisibleChanged );
            this.groupBox1.Enter += new System.EventHandler( this.OnVisibleChanged );
            // 
            // GradientAmplitudeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF( 6F, 13F );
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add( this.groupBox1 );
            this.Name = "GradientAmplitudeControl";
            this.Size = new System.Drawing.Size( 370, 130 );
            this.groupBox1.ResumeLayout( false );
            this.groupBox1.PerformLayout( );
            this.ResumeLayout( false );

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox startTextBox;
        private System.Windows.Forms.TextBox incrementTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox numberTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
