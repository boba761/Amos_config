using System;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Calculations.Values
{
    /// <summary>
    /// Класс для хранения вешественных значений
    /// </summary>
    public class DoubleValue : Value
    {
        public const string stringRegex = @"-?((\d+[\.,]?\d*)|([\.,]\d+))";

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public DoubleValue( ) { }

        /// <summary>
        /// Параметризованный конструктор класса
        /// </summary>
        /// <param name="obj">Объект значения</param>
        /// <param name="expression">Текст выражения для вычисления значения</param>
        public DoubleValue( object obj, string expression = null )
            : base( obj, expression )
        { }

        /// <summary>
        /// Возврашает приоритет типа значения
        /// </summary>
        [XmlIgnore]
        public override int Priority { get { return 3; } }

        /// <summary>
        /// Проверяет на корректность текста значения
        /// </summary>
        /// <param name="text">Текст значения</param>
        /// <returns>Возврашает истину если корректно, иначе ложь</returns>
        public static bool IsCheck( string text )
        {
            double result;
            return double.TryParse( ChangeSeparator( text ), out result );
        }

        /// <summary>
        /// Клонирование объекта значения
        /// </summary>
        /// <returns>Клон объекта</returns>
        public override object Clone( )
        {
            return new DoubleValue( Data, Expression );
        }

        /// <summary>
        /// Возвращает текстовое представление объекта
        /// </summary>
        public override string ToString()
        {
            double value = Convert.ToDouble( _data );
            if ( value == 0 )
                return "0";
            if ( Math.Abs( value ) < 0.001 )
                return string.Format( _numberFormatInfo, "{0:F" + Math.Round( Math.Abs( Math.Log10( value ) ) ) + "}", value );
            if ( Math.Abs( value - Math.Round( value, 0 ) ) >= 0.001 )
                return string.Format( _numberFormatInfo, "{0:F3}", value );
            return string.Format( _numberFormatInfo, "{0:F0}", value );
        }

        /// <summary>
        /// Сравнивает объекты значений
        /// </summary>
        /// <param name="obj">Объект значения для сравнения</param>
        /// <returns>Результат сравнения если равны истина, иначе ложь</returns>
        protected override bool EqualsValue( object obj )
        {
            return _data != null && obj != null && Convert.ToDouble( _data ) == Convert.ToDouble( obj );
        }

        /// <summary>
        /// Парсит значение
        /// </summary>
        /// <param name="text">Строка значения</param>
        protected override object Parse( string text )
        {
            return double.Parse( ChangeSeparator( text ) );
        }
    }
}
