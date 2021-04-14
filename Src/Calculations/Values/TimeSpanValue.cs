using System;
using System.Xml.Serialization;

namespace Calculations.Values
{
    /// <summary>
    /// Класс для хранения значения разницы во времени
    /// </summary>
    public class TimeSpanValue : Value
    {
        /// <summary>
        /// Конструктор класса
        /// </summary>
        public TimeSpanValue( ) { }

        /// <summary>
        /// Параметризованный конструктор класса
        /// </summary>
        /// <param name="obj">Объект значения</param>
        /// <param name="expression">Текст выражения для вычисления значения</param>
        public TimeSpanValue( object obj, string expression = null )
            : base( ( obj is TimeSpan ? ( (TimeSpan)obj ).Ticks : obj ), expression)
        { }

        /// <summary>
        /// Возврашает приоритет типа значения
        /// </summary>
        [XmlIgnore]
        public override int Priority { get { return 5; } }

        /// <summary>
        /// Утанавливает или возрашает значение
        /// </summary>
        [XmlIgnore]
        public override object Data
        {
            get { return base.Data; }
            set
            {
                if ( value is string )
                    base.Data = double.Parse( (string)value, _numberFormatInfo );
                else if ( value is TimeSpan )
                    base.Data = ( (TimeSpan)value ).Ticks;
                else
                    base.Data = value;
            }
        }

        /// <summary>
        /// Проверяет на корректность текста значения
        /// </summary>
        /// <param name="text">Текст значения</param>
        /// <returns>Возврашает истину если корректно, иначе ложь</returns>
        public static bool IsCheck(string text)
        {
            TimeSpan tmp;
            return TimeSpan.TryParse(text, out tmp);
        }

        /// <summary>
        /// Клонирование объекта значения
        /// </summary>
        /// <returns>Клон объекта</returns>
        public override object Clone( )
        {
            return new TimeSpanValue( Data, Expression );
        }

        /// <summary>
        /// Возвращает текстовое представление объекта
        /// </summary>
        public override string ToString()
        {
            return TimeSpan.FromTicks( Convert.ToInt64( _data ) ).ToString();
        }

        /// <summary>
        /// Сравнивает объекты значений
        /// </summary>
        /// <param name="obj">Объект значения для сравнения</param>
        /// <returns>Результат сравнения если равны истина, иначе ложь</returns>
        protected override bool EqualsValue( object obj )
        {
            return _data != null && obj != null && obj is double && Convert.ToInt64( _data ) == Convert.ToInt64( obj );
        }

        /// <summary>
        /// Парсит значение
        /// </summary>
        /// <param name="text">Строка значения</param>
        protected override object Parse(string text)
        {
            return TimeSpan.Parse(text).Ticks;
        }
    }
}
