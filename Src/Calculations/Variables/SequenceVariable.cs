using System;
using Calculations.Values;

namespace Calculations.Variables
{
    /// <summary>
    /// Класс для переменных последовательности
    /// </summary>
    public class SequenceVariable : Variable
    {
        /// <summary>
        /// Конструктор класса
        /// </summary>
        public SequenceVariable( ) { }

        /// <summary>
        /// Параметризованный конструктор класса
        /// </summary>
        /// <param name="name">Название переменной</param>
        /// <param name="value">Объект значения</param>
        /// <param name="min">Объект минимального значения</param>
        /// <param name="max">Объект максимального значения</param>
        /// <param name="step">Объект шага значения</param>
        public SequenceVariable( string name, Value value, Value min = null, Value max = null, Value step = null )
            : base( name, false, value, min, max, step )
        {
        }

        /// <summary>
        /// Клонирование объекта значения
        /// </summary>
        /// <returns>Клон объекта</returns>
        public override object Clone( )
        {
            SequenceVariable variable = new SequenceVariable( Name, (Value)Value.Clone( ) );
            if ( Min != null )
                variable.Min = (Value)Min.Clone( );
            if ( Max != null )
                variable.Max = (Value)Max.Clone( );
            if ( Step != null )
                variable.Step = (Value)Step.Clone( );
            return variable;
        }
    }
}
