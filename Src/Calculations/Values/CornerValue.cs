using System;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Calculations.Values
{
    /// <summary>
    /// Класс для хранения параметров угла поворота
    /// </summary>
    public class CornerValue : DoubleValue
    {
        /// <summary>
        /// Конструктор класса
        /// </summary>
        public CornerValue( ) { }

        /// <summary>
        /// Параметризованный конструктор класса
        /// </summary>
        /// <param name="obj">Объект значения</param>
        /// <param name="expression">Текст выражения для вычисления значения</param>
        public CornerValue( object obj, string expression = null )
            : base( obj, expression )
        { }

        /// <summary>
        /// Возврашает приоритет типа значения
        /// </summary>
        [XmlIgnore]
        public override int Priority { get { return 4; } }

        /// <summary>
        /// Проверяет на корректность текста значения
        /// </summary>
        /// <param name="text">Текст значения</param>
        /// <returns>Возврашает истину если корректно, иначе ложь</returns>
        public static new bool IsCheck( string text )
        {
            if ( Regex.IsMatch( text, @"^-?\d+[\.,]?\d*\s*\xb0?$", RegexOptions.IgnoreCase ) )
            {
                Match vMatch = Regex.Match( text, DoubleValue.stringRegex );
                return vMatch != Match.Empty && DoubleValue.IsCheck( vMatch.Value );
            }
            return false;
        }

        /// <summary>
        /// Клонирование объекта значения
        /// </summary>
        /// <returns>Клон объекта</returns>
        public override object Clone( )
        {
            return new CornerValue( Data, Expression );
        }

        /// <summary>
        /// Возвращает текстовое представление объекта
        /// </summary>
        public override string ToString()
        {
            double value = Convert.ToDouble( _data );
            if ( Math.Abs( value - Math.Round( value, 0 ) ) >= 0.01 )
                return string.Format( _numberFormatInfo, "{0:F2}\xb0", value );
            return string.Format( _numberFormatInfo, "{0:F0}\xb0", value );
        }

        /// <summary>
        /// Парсит значение
        /// </summary>
        /// <param name="text">Строка значения</param>
        protected override object Parse( string text )
        {
            Match vMatch = Regex.Match( text, DoubleValue.stringRegex );
            if ( vMatch != Match.Empty )
                return base.Parse( vMatch.Value );
            else
                throw new Exception( "Error parse value type [{" + this.GetType() + "}]" );
        }
    }
}
