using System;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Calculations.Values
{
    /// <summary>
    /// Класс для хранения логических значений
    /// </summary>
    public class BooleanValue : Value
    {
        public const string stringRegex = @"(true)|(false)|(0)|(1)";

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public BooleanValue( ) { }

        /// <summary>
        /// Параметризованный конструктор класса
        /// </summary>
        /// <param name="obj">Объект значения</param>
        /// <param name="expression">Текст выражения для вычисления значения</param>
        /// <returns></returns>
        public BooleanValue( object obj, string expression = null )
            : base( obj, expression )
        { }

        /// <summary>
        /// Возврашает приоритет типа значения
        /// </summary>
        [XmlIgnore]
        public override int Priority { get { return 1; } }

        /// <summary>
        /// Проверяет на корректность текста значения
        /// </summary>
        /// <param name="text">Текст значения</param>
        /// <returns>Возврашает истину если корректно, иначе ложь</returns>
        public static bool IsCheck( string text )
        {
            bool result;
            return bool.TryParse( text, out result ); 
        }

        /// <summary>
        /// Клонирование объекта значения
        /// </summary>
        public override object Clone( )
        {
            return new BooleanValue( Data, Expression );
        }

        /// <summary>
        /// Возвращает текстовое представление объекта
        /// </summary>
        public override string ToString()
        {
            return Convert.ToBoolean( _data ).ToString();
        }

        /// <summary>
        /// Сравнивает объекты значений
        /// </summary>
        /// <param name="obj">Объект значения для сравнения</param>
        /// <returns>Результат сравнения если равны истина, иначе ложь</returns>
        protected override bool EqualsValue( object obj )
        {
            return _data != null && obj != null && Convert.ToBoolean( _data ) == Convert.ToBoolean( obj );
        }

        /// <summary>
        /// Парсит значение
        /// </summary>
        /// <param name="text">Строка значения</param>
        protected override object Parse( string text )
        {
            return bool.Parse( text );
        }
    }
}
