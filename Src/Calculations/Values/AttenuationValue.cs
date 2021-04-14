using System;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace Calculations.Values
{
    /// <summary>
    /// Класс для хранения параметров звукового давления значений
    /// </summary>
    public class AttenuationValue : DoubleValue
    {
        /// <summary>
        /// Kонструктор класса
        /// </summary>
        public AttenuationValue( ) { }

        /// <summary>
        /// Параметризованный конструктор класса
        /// </summary>
        /// <param name="obj">Объект значения</param>
        /// <param name="expression">Текст выражения для вычисления значения</param>
        public AttenuationValue( object obj, string expression = null )
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
        public static new bool IsCheck(string text)
        {
            if ( Regex.IsMatch( text, @"^-?\d+[\.,]?\d*\s*(dB)?$", RegexOptions.IgnoreCase ) )
            {
                Match vMatch = Regex.Match( text, DoubleValue.stringRegex );
                return vMatch != Match.Empty && DoubleValue.IsCheck( vMatch.Value );
            }
            return false;
        }

        /// <summary>
        /// Возвращает текстовое представление объекта
        /// </summary>
        public override string ToString()
        {
            return base.ToString() + " dB";
        }

        /// <summary>
        /// Клонирование объекта значения
        /// </summary>
        /// <returns>Клон объекта</returns>
        public override object Clone( )
        {
            return new AttenuationValue( Data, Expression );
        }

        /// <summary>
        /// Парсит значение
        /// </summary>
        /// <param name="text">Строка значения</param>
        protected override object Parse(string text)
        {
            Match vMatch = Regex.Match(text, DoubleValue.stringRegex);
            if ( vMatch != Match.Empty )
                return base.Parse( vMatch.Value );
            else
                throw new Exception( "Error parse value type [{" + this.GetType( ) + "}]" );
        }
    }
}
