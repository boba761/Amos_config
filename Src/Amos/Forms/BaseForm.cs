using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Localize;

namespace Amos.Forms
{
    /// <summary>
    /// Базовый класс для всех форм приложения
    /// </summary>
    public partial class BaseForm : Form
    {
        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        protected BaseForm( )
        {
            LoadLocalize = true;
            InitializeComponent( );
        }

        /// <summary>
        /// Параметризованный конструктор
        /// </summary>
        /// <param name="document">Объект документа</param>
        public BaseForm(Document document = null)
            : this()
        {
            LoadLocalize = true;
            if ( document  != null )
                Document = document;
        }

        /// <summary>
        /// Возвращает или устанавливает флаг локализации
        /// </summary>
        protected bool LoadLocalize { get; set; }

        /// <summary>
        /// Возвращает или устанавливает объект документа
        /// </summary>
        public virtual Document Document { get; set; } 

        //public virtual void Localize()
        //{
        //}

        /// <summary>
        /// Метод локализирующий объекты форты
        /// </summary>
        public void LocalizeForm()
        {
            Local.LocalizeObject(this);
            //Localize();
        }

        /// <summary>
        /// Инициализация визуальных компонентоф формы
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // BaseForm
            // 
            this.ClientSize = new System.Drawing.Size(292, 266);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BaseForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Load += new System.EventHandler(this.OnLoad);
            this.ResumeLayout(false);

        }

        /// <summary>
        /// Выпонение действий при загрузке формы
        /// </summary>
        private void OnLoad(object sender, EventArgs e)
        {
            if (LoadLocalize)
                LocalizeForm();
        }
    }
}
