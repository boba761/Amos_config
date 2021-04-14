using Calculations.Values;
using Localize;

namespace Calculations.Variables
{
    /// <summary>
    /// Класс для системных переменных
    /// </summary>
    public class SystemVariable : Variable
    {
        /// <summary>
        /// Конструктор класса
        /// </summary>
        public SystemVariable( ) { }

        /// <summary>
        /// Параметризованный конструктор класса
        /// </summary>
        /// <param name="name">Название переменной</param>
        /// <param name="isReadOnly">Флаг указывающий будет ли переменная редактируемой</param>
        /// <param name="value">Объект значения</param>
        /// <param name="min">Объект минимального значения</param>
        /// <param name="max">Объект максимального значения</param>
        /// <param name="step">Объект шага значения</param>
        public SystemVariable( string name, bool isReadOnly, Value value, Value min = null, Value max = null, Value step = null )
            : base( name, isReadOnly, value, min, max, step )
        {
            Description = Local.GetString( string.Format( "[{0}].Description", name ) );
            if ( value.IsExpression )
                IsLookExpression = true;
        }

        /// <summary>
        /// Клонирование объекта значения
        /// </summary>
        /// <returns>Клон объекта</returns>
        public override object Clone( )
        {
            SystemVariable variable = new SystemVariable( Name, IsReadOnly, (Value)Value.Clone( ) );
            if ( Min != null )
                variable.Min = (Value)Min.Clone( );
            if ( Max != null )
                variable.Max = (Value)Max.Clone( );
            if ( Step != null )
                variable.Step = (Value)Step.Clone( );
            variable.IsLookExpression = IsLookExpression;
            return variable;
        }
    }
}
