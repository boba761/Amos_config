using System;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Calculations.Values
{
    /// <summary>
    /// Класс для хранения частотных характеристик
    /// </summary>
    public class FrequencyValue : DoubleValue
    {
        private int _kof = 1;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public FrequencyValue( ) { }

        /// <summary>
        /// Параметризованный конструктор класса
        /// </summary>
        /// <param name="obj">Объект значения</param>
        /// <param name="expression">Текст выражения для вычисления значения</param>
        public FrequencyValue( object obj, string expression = null )
            : base( obj, expression )
        { }

        /// <summary>
        /// Возврашает приоритет типа значения
        /// </summary>
        [XmlIgnore]
        public override int Priority { get { return 4; } }

        /// <summary>
        /// Возвращает единицы измерения значения
        /// </summary>
        private string Uint
        {
            get
            {
                if ( _kof == 1000000000 )
                    return "GHz";
                else if ( _kof == 1000000 )
                    return "MHz";
                else if ( _kof == 1000 )
                    return "kHz";
                else
                    return "Hz";
            }
        }

        /// <summary>
        /// Проверяет на корректность текста значения
        /// </summary>
        /// <param name="text">Текст значения</param>
        /// <returns>Возврашает истину если корректно, иначе ложь</returns>
        public static new bool IsCheck(string text)
        {
            if ( Regex.IsMatch( text, @"^-?\d+[\.,]?\d*\s*((GHZ)|(MHZ)|(KHZ)|(HZ))?$", RegexOptions.IgnoreCase ) )
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
            return new FrequencyValue( Data, Expression );
        }

        /// <summary>
        /// Возвращает текстовое представление объекта
        /// </summary>
        public override string ToString()
        {
            double value = Convert.ToDouble( _data ) / _kof;
            if ( Math.Abs( value - Math.Round( value, 0 ) ) >= 0.001 )
                return string.Format( _numberFormatInfo, "{0:F3} {1}", value, Uint );
            return string.Format( _numberFormatInfo, "{0:F0} {1}", value, Uint );
        }

        /// <summary>
        /// Парсит значение
        /// </summary>
        /// <param name="text">Строка значения</param>
        protected override object Parse( string text )
        {
            Match vMatch = Regex.Match( text, DoubleValue.stringRegex );
            _kof = GetKof( Regex.Match( text, @"((GHZ)|(MHZ)|(KHZ)|(HZ))$", RegexOptions.IgnoreCase ) );
            if ( vMatch != Match.Empty )
                return (double)base.Parse( vMatch.Value ) * _kof;
            else
                throw new Exception( "Error parse value type [{" + this.GetType() + "}]" );
        }

        /// <summary>
        /// Округление значения
        /// </summary>
        protected override void Improvement()
        {
            string strVal;
            double val = Math.Abs( Convert.ToDouble( _data ) );
            _kof = 1;

            while ( _kof < 1000000000 )
            {
                strVal = ( val / _kof ).ToString( "F0", _numberFormatInfo );
                if ( strVal.EndsWith( "000" ) )
                    _kof *= 1000;
                else
                    break;
            }
            while ( _kof < 1000000000 && ( val / _kof ) >= 1000 )
                _kof *= 1000;
        }

        /// <summary>
        /// Получаем коэффичиет для отображения частоты
        /// </summary>
        private int GetKof( Match uMatch )
        {
            if ( uMatch == Match.Empty )
                return 1;
            switch ( uMatch.Value.ToUpper() )
            {
            case "GHZ":
                return 1000000000;
            case "MHZ":
                return 1000000;
            case "KHZ":
                return 1000;
            default:
                return 1;
            }
        }
    }
}
