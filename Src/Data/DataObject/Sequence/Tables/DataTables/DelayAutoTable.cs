using System;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Calculations.Variables;
using Calculations.Values;

namespace Data.DataObject.Sequence.Tables.DataTables
{
    /// <summary>
    /// Класс дополнительных параметров автоматической таблицы для временных промежутков
    /// </summary>
    [Serializable]
    public class DelayAutoTable : ICloneable
    {
        private TableSequence _table;
        private bool _useDwellTime;
        private object _increment;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public DelayAutoTable( ) 
        {
            Increment = "1u";
        }

        /// <summary>
        /// Возвращает или устанавливает текущию таблицу
        /// </summary>
        [XmlIgnore]
        public TableSequence Table
        {
            get { return _table; }
            set 
            { 
                _table = value;
                if ( _useDwellTime )
                {
                    if ( Table.TypeTable == TableSequence.typeTable2D )
                        _increment = Document.LoadDocument.Dashboard["Dwell 2D"];
                    if ( Table.TypeTable == TableSequence.typeTable3D )
                        _increment = Document.LoadDocument.Dashboard["Dwell 3D"];
                    if ( Table.TypeTable == TableSequence.typeTable4D )
                        _increment = Document.LoadDocument.Dashboard["Dwell 4D"];
                    else
                        _increment = Document.LoadDocument.Dashboard["Dwell Time"];
                }
            }
        }

        /// <summary>
        /// Возвращает или устанавливает привязку к переменноу Dwell Time
        /// </summary>
        [XmlAttribute]
        public bool UseDwellTime 
        {
            get { return _useDwellTime; } 
            set 
            {
                _useDwellTime = value;
                if ( _useDwellTime && Table != null )
                {
                    if ( Table.TypeTable == TableSequence.typeTable2D )
                        _increment = Document.LoadDocument.Dashboard["Dwell 2D"];
                    if ( Table.TypeTable == TableSequence.typeTable3D )
                        _increment = Document.LoadDocument.Dashboard["Dwell 3D"];
                    if ( Table.TypeTable == TableSequence.typeTable4D )
                        _increment = Document.LoadDocument.Dashboard["Dwell 4D"];
                    else
                        _increment = Document.LoadDocument.Dashboard["Dwell Time"];
                }
            }
        }

        /// <summary>
        /// Возвращает или устанавливает строку с выражением шага изменения значения
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
                if ( _useDwellTime )
                    return;
                if ( TimeValue.IsCheck( text ) )
                    _increment = new TimeValue( text );
                else if ( Regex.IsMatch( text, @"^=\s*[a-z][a-z0-9\s\.,]*$", RegexOptions.IgnoreCase ) )
                {
                    if ( Table == null )
                        _increment = Document.LoadDocument.Sequence.GetTableVariable( text.Substring( 1 ).Trim( ) );
                    else
                        _increment = Document.LoadDocument.Sequence.CheckTableVariable( new TableVariable( text.Substring( 1 ).Trim( ), new TimeValue( 0 ),
                            new TimeValue( "100n" ), new TimeValue( "3600s" ) ) );
                }
                else
                    throw new Exception( "In the field Increment must be or variable or value." );
            }
        }

        /// <summary>
        /// Возвращает или устанавливает объект переменной шага  изменения значения
        /// </summary>
        [XmlIgnore]
        public Variable IncrementVariable 
        { 
            get { return _increment as Variable; }
            set { _increment = value; }
        }

        /// <summary>
        /// </summary>
        [XmlAttribute]
        public int Every { get; set; }

        /// <summary>
        /// Возвращает или устанавливает тип добавления
        /// </summary>
        [XmlAttribute]
        public int Add { get; set; }

        /// <summary>
        /// Клонирует текущий объект
        /// </summary>
        public object Clone( )
        {
            DelayAutoTable obj = new DelayAutoTable();
            obj.Table = Table;
            obj._increment = ( _increment as ICloneable ).Clone( );
            obj._useDwellTime = _useDwellTime;
            obj.Every = Every;
            obj.Add = Add;
            return obj;
        }
    }
}
