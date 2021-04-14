using System;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Calculations.Values
{
    /// <summary>
    /// Класс для хранения температурных значений
    /// </summary>
    public class TemperaturaValue : DoubleValue
    {
        /// <summary>
        /// Конструктор класса
        /// </summary>
        public TemperaturaValue( ) { }

        /// <summary>
        /// Параметризованный конструктор класса
        /// </summary>
        /// <param name="obj">Объект значения</param>
        /// <param name="expression">Текст выражения для вычисления значения</param>
        public TemperaturaValue( object obj, string expression = null )
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
            if ( Regex.IsMatch( text, @"^-?\d+[\.,]?\d*\s*[\xb0]?[CKF]?$", RegexOptions.IgnoreCase ) )
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
            return new TemperaturaValue( Data, Expression );
        }

        /// <summary>
        /// Возвращает текстовое представление объекта
        /// </summary>
        public override string ToString()
        {
            double value = Convert.ToDouble( _data );
            if ( Math.Abs( value - Math.Round( value, 0 ) ) >= 0.01 )
                return string.Format( _numberFormatInfo, "{0:F2} \xb0K", value );
            return string.Format( _numberFormatInfo, "{0:F0} \xb0K", value );
        }

        /// <summary>
        /// Парсит значение
        /// </summary>
        /// <param name="text">Строка значения</param>
        protected override object Parse( string text )
        {
            Match vMatch = Regex.Match( text, DoubleValue.stringRegex );
            if ( vMatch != Match.Empty )
                return Converter( (double)base.Parse( vMatch.Value ), Regex.Match( text, @"[CKF]$", RegexOptions.IgnoreCase ) );
            else
                throw new Exception( "Error parse value type [{" + this.GetType() + "}]" );
        }

        /// <summary>
        /// Конвиртирует температуру из различных систем измерения
        /// </summary>
        /// <param name="value">Значение температуры</param>
        private double Converter( double value, Match uMatch )
        {
            if ( uMatch == Match.Empty )
                return value;
            switch ( uMatch.Value.ToUpper() )
            {
            case "C":
                return value + 273.15;
            case "F":
                return ( value + 459 ) / 1.8;
            default:
                return value;
            }
        }
    }
}
