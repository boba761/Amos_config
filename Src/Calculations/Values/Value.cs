using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Serialization;
using Calculations.Expressions;
using Tools.Interfaces;

namespace Calculations.Values
{
    /// <summary>
    /// Базовый класс для значений
    /// </summary>
    [XmlInclude( typeof( AttenuationValue ) )]
    [XmlInclude( typeof( BooleanValue ) )]
    [XmlInclude( typeof( CornerValue ) )]
    [XmlInclude( typeof( DateTimeValue ) )] 
    [XmlInclude( typeof( DoubleValue ) )] 
    [XmlInclude( typeof( FrequencyValue ) )] 
    [XmlInclude( typeof( IntegerValue ) )] 
    [XmlInclude( typeof( StringValue ) )] 
    [XmlInclude( typeof( TemperaturaValue ) )] 
    [XmlInclude( typeof( TimeSpanValue ) )] 
    [XmlInclude( typeof( TimeValue ) )]
    public abstract class Value : ValueExpression, IDataValue, IObjectSequence
    {
        protected static NumberFormatInfo _numberFormatInfo;

        private bool _isCalculate;
        private Expression _expression;
        private string _textExpression;
        private List<Value> _inputValues; // кто будет пересчитывать
        private HashSet<Value> _outputValues;  // Пересчитывать переменные

        protected object _data;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        static Value( )
        {
            _numberFormatInfo = new NumberFormatInfo( );
            _numberFormatInfo.NumberDecimalSeparator = ".";
        }

        /// <summary>
        /// Параметризованный конструктор класса
        /// </summary>
        /// <param name="obj">Объект значения</param>
        /// <param name="expression">Текст выражения для вычисления значения</param>
        protected Value( object obj = null, string expression = null )
        {
            _isCalculate = false;
            _inputValues = new List<Value>( );
            _outputValues = new HashSet<Value>( );
            Data = obj;
            if ( expression != null )
                Expression = expression;
        }

        /// <summary>
        /// Возврашает текстовое представление значения
        /// </summary>
        [XmlAttribute( AttributeName = "Data" )]
        public virtual string TextData
        {
            get
            {
                if ( Data == null )
                    return null;
                return this is StringValue ? Data.ToString( ) : Convert.ToDouble( Data ).ToString( _numberFormatInfo );
            }
            set
            {
                if ( this is StringValue )
                    Data = value;
                else
                    Data = double.Parse( value, _numberFormatInfo );
            }
        }
        /// <summary>
        /// Утанавливает или возрашает текст выражения для вычисления его значения
        /// </summary>
        [XmlAttribute]
        public string Expression
        {
            get 
            {
                if ( _expression == null )
                    return _textExpression;
                else
                    return "=" + _expression.ToText( );
            }
            set
            {
                if ( IsLookExpression )
                    SetValue( Parse( value ) );
                else if ( value == null )
                    _textExpression = null;
                else if ( value.StartsWith( "=" ) )
                {
                    _textExpression = value;
                    _expression = null;
                }
                else
                {
                    _textExpression = null;
                    _expression = null;
                    SetValue( Parse( value ) );
                }
            }
        }

        /// <summary>
        /// Утанавливает или возврашает значение блокировки выражения
        /// </summary>
        [XmlAttribute]
        public bool IsLookExpression { get; set; }

        /// <summary>
        /// Возврашает приоритет типа значения
        /// </summary>
        [XmlIgnore]
        public abstract int Priority { get; }

        /// <summary>
        /// Утанавливает или возрашает значение
        /// </summary>
        [XmlIgnore]
        public override object Data
        {
            get { return _data; }
            set
            {
                if ( value != null && value is string )
                    Expression = ( (string)value ).Trim( );
                else
                    SetValue( value );
            }
        }

        /// <summary>
        /// Возврашает является ли значение выражением
        /// </summary>
        [XmlIgnore]
        public bool IsExpression
        {
            get { return _textExpression != null; }
        }

        /// <summary>
        /// Возврашает состояние вычисления значения
        /// </summary>
        [XmlIgnore]
        public bool IsCalculate
        {
            get { return _isCalculate; }
            set
            {
                if ( _isCalculate == false && value == false )
                    return;
                _isCalculate = value;
                if ( _isCalculate == false )
                {
                    foreach ( Value obj in _outputValues )
                        if ( obj != this )
                            obj.IsCalculate = false;
                }
            }
        }

        /// <summary>
        /// Возврашает имеются ли у значения зависитые значения
        /// </summary>
        [XmlIgnore]
        public bool IsOutput { get { return _outputValues.Count > 0; } }

        /// <summary>
        /// Возвращает текст всплывающей подсказки
        /// </summary>
        [XmlIgnore]
        public string ToolTip { get { return ToString(); } }

        public static bool operator >( Value val1, Value val2 )
        {
            return Convert.ToDouble( val1.Data ) > Convert.ToDouble( val2.Data );
        }

        public static bool operator <( Value val1, Value val2 )
        {
            return Convert.ToDouble( val1.Data ) < Convert.ToDouble( val2.Data );
        }

        /// <summary>
        /// Статический метод создания значения с автоопределением его типа
        /// </summary>
        /// <param name="text">Текст значения</param>
        /// <returns>Объект значения</returns>
        public static Value CreateInstance( string text )
        {
            if ( BooleanValue.IsCheck( text ) )
                return new BooleanValue( text );
            if ( IntegerValue.IsCheck( text ) )
                return new IntegerValue( text );
            if ( DoubleValue.IsCheck( text ) )
                return new DoubleValue( text );
            if ( AttenuationValue.IsCheck( text ) )
                return new AttenuationValue( text );
            if ( CornerValue.IsCheck( text ) )
                return new CornerValue( text );
            if ( FrequencyValue.IsCheck( text ) )
                return new FrequencyValue( text );
            if ( TimeValue.IsCheck( text ) )
                return new TimeValue( text );
            if ( TemperaturaValue.IsCheck( text ) )
                return new TemperaturaValue( text );
            if ( TimeSpanValue.IsCheck( text ) )
                return new TimeSpanValue( text );
            if ( DateTimeValue.IsCheck( text ) )
                return new DateTimeValue( text );
            //if ( StringValue.IsCheck( text ) )
            //    return new StringValue( text );
            return null;
        }

        /// <summary>
        /// Статисеский метод автоопределения типов значений
        /// </summary>
        /// <param name="texts">Строковый массив значений</param>
        /// <returns>Тип значения</returns>
        public static Type GetAutoType( params string[] texts )
        {
            Value valueBase = null;
            foreach(string text in texts)
            {
                if ( string.IsNullOrWhiteSpace(text) )
                    continue;
                Value tmpValueBase = CreateInstance( text );
                if ( valueBase == null )
                    valueBase = tmpValueBase;
                else if ( tmpValueBase != null && valueBase.Priority < tmpValueBase.Priority )
                    valueBase = tmpValueBase;
            }
            return valueBase != null ? valueBase.GetType() : null;
        }

        /// <summary>
        /// Добавление зависимых значений
        /// </summary>
        /// <param name="value">Объект значения</param>
        public void AddOutput( Value value )
        {
            if ( value == this || _outputValues.Contains( value ) )
                return;
            _outputValues.Add( value );
            value.AddInput( this );
        }

        /// <summary>
        /// Удаление зависимых значений
        /// </summary>
        /// <param name="value">Объект значения</param>
        public void RemoveOutput( Value value )
        {
            _outputValues.Remove( value );
        }

        /// <summary>
        /// Компилируем выражение
        /// </summary>
        /// <param name="compiler">Объект компилятора выражений</param>
        public void Compile( Compiler compiler )
        {
            if ( IsExpression == false )
                return;
            foreach ( Value value in _inputValues )
                value.RemoveOutput( this );
            _inputValues.Clear( );
            _expression = compiler.Compile( _textExpression.Substring( 1 ), this );
        }

        /// <summary>
        /// Вычисление значения
        /// </summary>
        /// <param name="compiler">Объект компилятора выражений</param>
        /// <param name="isEvaluate">Производить вычисление зависимых значений</param>
        public void Calculate( Compiler compiler, bool isEvaluate )
        {
            if ( IsExpression )
            {
                if ( _expression == null )
                    Compile( compiler );
                IsCalculate = false;
                if ( isEvaluate )
                    SetValue( _expression.Evaluate( ) );
                IsCalculate = true;
            }
            else
            {
                IsCalculate = false;
                IsCalculate = true;
            }

            foreach ( Value obj in _outputValues )
                if ( obj.IsCalculate == false )
                    obj.Calculate( compiler, true );
        }

        /// <summary>
        /// Клонирование объекта значения
        /// </summary>
        /// <returns>Клон объекта</returns>
        public abstract object Clone( );

        /// <summary>
        /// Парсит значение
        /// </summary>
        /// <param name="text">Строка значения</param>
        protected abstract object Parse( string text );

        /// <summary>
        /// Сравнивает объекты значений
        /// </summary>
        /// <param name="obj">Объект значения для сравнения</param>
        /// <returns>Результат сравнения если равны истина, иначе ложь</returns>
        protected abstract bool EqualsValue( object obj );

        /// <summary>
        /// Округление значения
        /// </summary>
        protected virtual void Improvement( ) { }

        /// <summary>
        /// Изменение раздилителя целой и вещественной части для разных стран
        /// </summary>
        /// <param name="text">Текст значения</param>
        /// <returns>Текст приведённого значения</returns>
        protected static string ChangeSeparator( string text )
        {
            if ( CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator == "." )
                return text.Replace( ",", "." );
            return text.Replace( ".", "," );
        }

        /// <summary>
        /// Добавление входящих значений
        /// </summary>
        /// <param name="value">Объект значения</param>
        private void AddInput( Value value )
        {
            _inputValues.Add( value );
            value.AddOutput( this );
        }

        /// <summary>
        /// Устанавливает значение в объект
        /// </summary>
        /// <param name="value">Объект значения</param>
        private void SetValue( object value )
        {
            if ( !EqualsValue( value ) )
            {
                _data = value;
                Improvement( );
            }
        }
    }
}
