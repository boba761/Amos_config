using System;
using System.Xml.Serialization;
using System.Text.RegularExpressions;

namespace Calculations.Values
{
    /// <summary>
    /// Класс для хранения строки
    /// </summary>
    public class StringValue : Value
    {
        /// <summary>
        /// Конструктор класса
        /// </summary>
        public StringValue( ) { }

        /// <summary>
        /// Параметризованный конструктор класса
        /// </summary>
        /// <param name="obj">Объект значения</param>
        /// <param name="expression">Текст выражения для вычисления значения</param>
        public StringValue( object obj, string expression = null )
            : base( obj, expression )
        { }

        /// <summary>
        /// Возврашает приоритет типа значения
        /// </summary>
        [XmlIgnore]
        public override int Priority { get { return 6; } }

        /// <summary>
        /// Утанавливает или возрашает значение
        /// </summary>
        [XmlIgnore]
        public override object Data
        {
            get { return base.Data; }
            set
            {
                if ( value == null )
                {
                    base.Data = "";
                    return;
                }
                int index = ( (string)value ).IndexOf( '\0' );
                base.Data = index >= 0 ? ( (string)value ).Substring( 0, index ) : ( (string)value );
            }
        }

        /// <summary>
        /// Проверяет на корректность текста значения
        /// </summary>
        /// <param name="text">Текст значения</param>
        /// <returns>Возврашает истину если корректно, иначе ложь</returns>
        public static bool IsCheck( string text )
        {
            return true;
        }

        /// <summary>
        /// Клонирование объекта значения
        /// </summary>
        /// <returns>Клон объекта</returns>
        public override object Clone( )
        {
            return new StringValue( Data, Expression );
        }

        /// <summary>
        /// Возвращает текстовое представление объекта
        /// </summary>
        public override string ToString()
        {
            return Convert.ToString( _data );
        }

        /// <summary>
        /// Сравнивает объекты значений
        /// </summary>
        /// <param name="obj">Объект значения для сравнения</param>
        /// <returns>Результат сравнения если равны истина, иначе ложь</returns>
        protected override bool EqualsValue( object obj )
        {
            return _data != null && obj != null && Convert.ToString( _data ) == Convert.ToString( obj );
        }

        /// <summary>
        /// Парсит значение
        /// </summary>
        /// <param name="text">Строка значения</param>
        protected override object Parse( string text )
        {
            return text;
        }
    }
}
