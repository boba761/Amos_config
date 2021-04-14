using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Calculations.Values;
using Calculations.Variables;
using Tools.Interfaces;

namespace Amos.Data.Sequence.Tables.DataTables
{
    /// <summary>
    /// Класс дополнительных параметров автоматической таблицы
    /// </summary>
    [Serializable]
    public class GenerateAutoTable : ICloneable
    {
        private TableSequence _table;
        private IDataValue _start;
        private IDataValue _increment;
        private IDataValue _number;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public GenerateAutoTable( )
        {
            Start = "0";
            Increment = "1";
            Number = "1";
        }

        /// <summary>
        /// Возвращает привязанную таблицу
        /// </summary>
        [XmlIgnore]
        public TableSequence Table 
        { 
            get { return _table; } 
            set { _table = value; } 
        }

        /// <summary>
        /// Возвращает или задаёт строку с выражением для начального значения  приращения
        /// </summary>
        [XmlAttribute]
        public string Start
        {
            get
            {
                if ( _start is Value )
                    return ( _start as Value ).ToString( );
                else if ( _start is Variable )
                    return "="  + ( _start as Variable ).Name;
                else
                    return null;
            }
            set
            {
                string text = value.Trim( );
                if ( string.IsNullOrWhiteSpace( text ) )
                    text = "0";
                if ( DoubleValue.IsCheck( text ) )
                    _start = new DoubleValue( text );
                else if ( Regex.IsMatch( text, @"^=[a-z][a-z0-9\s\.,]*$", RegexOptions.IgnoreCase ) )
                {
                    if ( Table == null )
                        _start = Program.Document.Sequence.GetTableVariable( text.Substring( 1 ).Trim( ) );
                    else
                        _start = Program.Document.Sequence.CheckTableVariable( new TableVariable( text.Substring( 1 ).Trim( ), new DoubleValue( 0 ),
                            new DoubleValue( -100 ), new DoubleValue( 100 ) ) );
                }
                else
                    throw new Exception( "In the field Start must be or variable or value." );
            }
        }

        /// <summary>
        /// Возвращает или задаёт строку с выражением для шага приращения
        /// </summary>
        [XmlAttribute]
        public string Increment
        {
            get
            {
                if ( _increment is Value )
                    return ( _increment as Value ).ToString( );
                else if ( _increment is Variable )
                    return "=" + ( _increment as Variable ).Name;
                else
                    return null;
            }
            set
            {
                string text = value.Trim( );
                if ( string.IsNullOrWhiteSpace( text ) )
                    text = "1";
                if ( DoubleValue.IsCheck( text ) )
                    _increment = new DoubleValue( text );
                else if ( Regex.IsMatch( text, @"^=[a-z][a-z0-9\s\.,]*$", RegexOptions.IgnoreCase ) )
                {
                    if ( Table == null )
                        _increment = Program.Document.Sequence.GetTableVariable( text.Substring( 1 ).Trim( ) );
                    else
                        _increment = Program.Document.Sequence.CheckTableVariable( new TableVariable( text.Substring( 1 ).Trim( ), new DoubleValue( 0 ),
                            new DoubleValue( -100 ), new DoubleValue( 100 ) ) );
                }
                else
                    throw new Exception( "In the field Start must be or variable or value." );
            }
        }

        /// <summary>
        /// Возвращает или задаёт строку с выражением для количества итераций приращения
        /// </summary>
        [XmlAttribute]
        public string Number
        {
            get
            {
                if ( _number is Value )
                    return ( _number as Value ).ToString( );
                else if ( _number is Variable )
                    return "=" + ( _number as Variable ).Name;
                else
                    return null;
            }
            set
            {
                string text = value.Trim( );
                if ( string.IsNullOrWhiteSpace( text ) )
                    text = "1";
                if ( IntegerValue.IsCheck( text ) )
                    _number = new IntegerValue( text );
                else if ( Regex.IsMatch( text, @"^=[a-z][a-z0-9\s\.,]*$", RegexOptions.IgnoreCase ) )
                {
                    if ( Table == null )
                        _number = Program.Document.Sequence.GetTableVariable( text.Substring( 1 ).Trim( ) );
                    else
                        _number = Program.Document.Sequence.CheckTableVariable( new TableVariable( text.Substring( 1 ).Trim( ), new IntegerValue( 0 ),
                            new IntegerValue( 0 ) ) );
                }
                else
                    throw new Exception( "In the field Start must be or variable or value." );
            }
        }

        /// <summary>
        /// Возвращает или задаёт переменную для начального значения  приращения
        /// </summary>
        [XmlIgnore]
        public Variable StartVariable
        {
            get { return _start as Variable; }
            set { _start = value; }
        }

        /// <summary>
        /// Возвращает или задаёт переменную для шага приращения
        /// </summary>
        [XmlIgnore]
        public Variable IncrementVariable
        {
            get { return _increment as Variable; }
            set { _increment = value; }
        }

        /// <summary>
        /// Возвращает или задаёт переменную для количества итераций приращения
        /// </summary>
        [XmlIgnore]
        public Variable NumberVariable
        {
            get { return _number as Variable; }
            set { _number = value; }
        }

        /// <summary>
        /// Генерирует текст со значениями таблицы
        /// </summary>
        public string Generate( )
        {
            NumberFormatInfo numberFormatInfo = new NumberFormatInfo( );
            numberFormatInfo.NumberDecimalSeparator = ".";
            StringBuilder stringBuilder = new StringBuilder( "; Automatically generated table:" );
            double start = Convert.ToDouble( _start.Data );
            double increment = Convert.ToDouble( _increment.Data );
            int number = Convert.ToInt32( _number.Data );
            if ( number > 10000 )
                number = 10000; 
            for ( int i = 0; i < number; i++, start += increment)
            { 
                if ( i % 5 == 0 )
                    stringBuilder.Append( "\r\n" + start.ToString( numberFormatInfo ) );
                else
                    stringBuilder.Append( " " + start.ToString( numberFormatInfo ) );
            }
            return stringBuilder.ToString( );
        }

        /// <summary>
        /// Клонирует текущий объект
        /// </summary>
        /// <returns>Клон объекта</returns>
        public object Clone( )
        {
            GenerateAutoTable obj = new GenerateAutoTable( );
            obj.Table = Table;
            obj._start = (IDataValue)( _start as ICloneable ).Clone( );
            obj._increment = (IDataValue)( _increment as ICloneable ).Clone( );
            obj._number = (IDataValue)( _number as ICloneable ).Clone( );
            return obj;
        }
    }
}
