using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using Localize;

namespace Amos.Views
{
    public partial class BaseView : DockContent
    {
        private Document _document;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        protected BaseView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Возвращает или задаёт документ для отображения данных
        /// </summary>
        public virtual Document Document
        {
            get { return _document; }
            set
            {
                _document = value;
                if (_document == null)
                    Hide();
                else
                    Show();
            }
        }

        /// <summary>
        /// Отображение формы на элементе управления
        /// </summary>
        public new void Show()
        {
            base.Show(Program.MainForm.DockPanel);
        }

        /// <summary>
        /// Локализирует элементы управления формы
        /// </summary>
        public void LocalizeForm()
        {
            Local.LocalizeObject(this);
        }

        /// <summary>
        /// Инициализация элементов управления для формы
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout( );
            // 
            // BaseView
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size( 292, 266 );
            this.Font = new System.Drawing.Font( "Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 204 ) ) );
            this.HideOnClose = true;
            this.Name = "BaseView";
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler( this.OnLoad );
            this.TextChanged += new System.EventHandler( this.OnTextChanged );
            this.ResumeLayout( false );

        }

        /// <summary>
        /// Настройка элементов управления перед загрузкой формы
        /// </summary>
        private void OnLoad(object sender, EventArgs e)
        {
            LocalizeForm();
        }

        /// <summary>
        /// Обработчик изменения текста окна формы
        /// </summary>
        private void OnTextChanged(object sender, EventArgs e)
        {
            TabText = Text;
        }

    }
}
