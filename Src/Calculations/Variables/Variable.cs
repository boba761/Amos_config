using System.Xml.Serialization;
using System;
using Calculations.Values;
using Calculations.Expressions;
using Tools.Interfaces;

namespace Calculations.Variables
{
    /// <summary>
    /// Базовый клас переменных
    /// </summary>
    public abstract class Variable : VariableBase, IDataValue, IObjectSequence
    {
        /// <summary>
        /// Параметризованный конструктор класса
        /// </summary>
        /// <param name="name">Название переменной</param>
        /// <param name="isReadOnly">Флаг указывающий будет ли переменная редактируемой</param>
        /// <param name="value">Объект значения</param>
        /// <param name="min">Объект минимального значения</param>
        /// <param name="max">Объект максимального значения</param>
        /// <param name="step">Объект шага значения</param>
        public Variable( string name = null, bool isReadOnly = false, Value value = null, Value min = null, Value max = null, Value step = null )
            : base( name, isReadOnly )
        {
            Value = value;
            Min = min;
            Max = max;
            Step = step;
        }

        /// <summary>
        /// Возрашает или устанавливает значение разришаюшее отображение выражения
        /// </summary>
        public static bool IsShowExpression { get; set; }

        /// <summary>
        /// Возвращает или устанавливает объект значения
        /// </summary>
        [XmlElement]
        public Value Value { get; set; }

        /// <summary>
        /// Возвращает или устанавливает объект минимального значения
        /// </summary>
        [XmlElement]
        public Value Min { get; set; }

        /// <summary>
        /// Возвращает или устанавливает объект максимального значения
        /// </summary>
        [XmlElement]
        public Value Max { get; set; }

        /// <summary>
        /// Возвращает или устанавливает объект шага значения
        /// </summary>
        [XmlElement]
        public Value Step { get; set; }

        /// <summary>
        /// Возвращает значение переменной
        /// </summary>
        [XmlIgnore]
        public object Data 
        { 
            get { return Value.Data; } 
            set { Value.Data = value; } 
        }
        /// <summary>
        /// Возврашает текст ошибки
        /// </summary>
        [XmlIgnore]
        public string Errors { get; private set; }
        /// <summary>
        /// Возврашает или устанавливает блокировку выражения переменной
        /// </summary>
        [XmlIgnore]
        public bool IsLookExpression
        {
            get { return Value.IsLookExpression; }
            set 
            { 
                Value.IsLookExpression = value;
                if ( Min != null )
                    Min.IsLookExpression = value;
                if ( Max != null )
                    Max.IsLookExpression = value;
                if ( Step != null )
                    Step.IsLookExpression = value;
            }
        }
        /// <summary>
        /// Возврашает есть ли у переменной зависимые переменные
        /// </summary>
        [XmlIgnore]
        public bool IsOutput { get { return Value.IsOutput; } }
        /// <summary>
        /// Возвращает текст всплывающей подсказки
        /// </summary>
        [XmlIgnore]
        public string ToolTip { get { return string.Format( "{0} = {1}", Name, Value ); } }

        /// <summary>
        /// Возвращает выражение для переменной
        /// </summary>
        public override ValueExpression ValueExpression { get { return Value; } }

        /// <summary>
        /// Компилируем выражение переменной
        /// </summary>
        /// <param name="compiler">Объект компилятора выражений</param>
        public void Compile( Compiler compiler )
        {
            Value.Compile( compiler );
            if ( Min != null )
                Min.Compile( compiler );
            if ( Max != null )
                Max.Compile( compiler );
            if ( Step != null )
                Step.Compile( compiler );
        }

        /// <summary>
        /// Вычисление значения переменной
        /// </summary>
        /// <param name="compiler">Объект компилятора выражений</param>
        /// <param name="isEvaluate">Производить вычисление зависимых переменных</param>
        /// <returns>Возврашает истину если вычисление прошло удачно иначе ложь</returns>
        public bool Calculate( Compiler compiler, bool isEvaluate )
        {
            if ( Min != null )
                Min.Calculate( compiler, true );
            if ( Max != null )
                Max.Calculate( compiler, true );
            if ( Step != null )
                Step.Calculate( compiler, true );

            if ( isEvaluate == false )
            {
                if ( Min != null && Min > Value )
                    Value.Data = Min.Data;
                else if ( Max != null && Max < Value )
                    Value.Data = Max.Data;
                else if ( Step != null && Min != null )
                {
                    double min = Convert.ToDouble( Min.Data );
                    double step = Convert.ToDouble( Step.Data );
                    Value.Data = ( min + Math.Round( ( Convert.ToDouble( Value.Data ) - min ) / step ) ) * step;
                }
            }
           
            Value.Calculate( compiler, isEvaluate );

            if ( isEvaluate == true )
            {
                if ( Min != null && Min > Value )
                {
                    Value.Data = Min.Data;
                    Value.Calculate( compiler, false );
                }
                else if ( Max != null && Max < Value )
                {
                    Value.Data = Max.Data;
                    Value.Calculate( compiler, false );
                }
                else if ( Step != null && Min != null )
                {
                    double min = Convert.ToDouble( Min.Data );
                    double step = Convert.ToDouble( Step.Data );
                    Value.Data = ( min + Math.Round( ( Convert.ToDouble( Value.Data ) - min ) / step ) ) * step;
                    Value.Calculate( compiler, false );
                }
            }
            return true;
        }

        /// <summary>
        /// Устанавливает значение переменной
        /// </summary>
        /// <param name="compiler">Объект компилятора выражений</param>
        /// <param name="obj">Объект значения</param>
        public void SetValue(Compiler compiler, object obj)
        {
            Value.Data = obj;
            Calculate( compiler, false );
        }

        /// <summary>
        /// Копирование значения переменной
        /// </summary>
        public virtual void CopyData( Variable variable )
        {
            Name = variable.Name; 
            IsReadOnly = variable.IsReadOnly;
            Value = variable.Value;
            Min = variable.Min;
            Max = variable.Max;
            Step = variable.Step;
            IsLookExpression = variable.IsLookExpression;
        }

        /// <summary>
        /// Возвращает текстовое представление объекта
        /// </summary>
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
