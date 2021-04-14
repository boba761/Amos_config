using System.Drawing;
using System.Windows.Forms;

namespace Amos.Controls.Sequence
{
    public enum eStateGlyph
    {
        Normal,
        Action,
        Select
    }

    /// <summary>
    /// Базовый клас для элементов последовательности
    /// </summary>
    public abstract class Glyph
    {

        /// <summary>
        /// Конструктор класса
        /// </summary>
        /// <param name="parent">Владелиц элемента</param>
        public Glyph( SequenceControl parent )
        {
            Parent = parent;
        }

        /// <summary>
        /// Возвращает владелиц
        /// </summary>
        public SequenceControl Parent { get; private set; }

        /// <summary>
        /// Возвращает или устанавливает ссостояние
        /// </summary>
        public virtual eStateGlyph StateGlyph { get; set; }

        /// <summary>
        /// Возвращает или устанавливает текст
        /// </summary>
        public virtual string Text { get; set; }

        /// <summary>
        /// Возвращает текст подсказки
        /// </summary>
        public virtual string ToolTip { get { return null; } }

        /// <summary>
        /// Отрисовка элемента последовательности
        /// </summary>
        /// <param name="graphics">Объект контекста устройства</param>
        /// <param name="point">Точка отрисовки</param>
        /// <param name="data">Дополнительные данные</param>
        /// <returns>Координаты отрисовки</returns>
        public abstract RectangleF Draw( Graphics graphics, PointF point, object data = null );

        /// <summary>
        /// Возвращает контекстное меню для элемента последовательности
        /// </summary>
        /// <param name="panel">Панель для отображения меню</param>
        /// <returns>Объект контекстного меню</returns>
        public virtual ContextMenuStrip GetContextMenu( SequencePanel panel ) 
        { 
            return null; 
        }

        /// <summary>
        /// Отрисовка выделенного элемента последовательности
        /// </summary>
        /// <param name="graphics">Объект контекста устройства</param>
        /// <param name="rect">Координаты отрисовки</param>
        public virtual void DrawSelect( Graphics graphics, RectangleF rect ) 
        { 
        }

        /// <summary>
        /// Отрисовка элемента последовательности в режиме редактирования
        /// </summary>
        /// <param name="graphics">Объект контекста устройства</param>
        /// <param name="rect">Координаты отрисовки</param>
        public virtual void DrawEdit( Graphics graphics, RectangleF rect ) 
        { 
        }

        /// <summary>
        /// Возвращает необходимую ширину для нормального отображения содиржимого  
        /// </summary>
        /// <param name="graphics">Объект контекста устройства</param>
        /// <returns>ширину в пикселях</returns>
        public abstract int GetFillWidth( Graphics graphics );
    }
}
