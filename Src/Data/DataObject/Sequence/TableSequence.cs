using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Calculations.Values;
using Calculations.Variables;
using Data.DataObject.Sequence.Tables;
using Data.DataObject.Sequence.Tables.DataTables;
using Tools.Interfaces;

namespace Data.DataObject.Sequence
{
    /// <summary>
    /// Базовый класс для объектов таблиц последовательности
    /// </summary>
    [Serializable]
    [XmlInclude( typeof( AcquisitionTableSequence ) )]
    [XmlInclude( typeof( AttenuationTableSequence ) )]
    [XmlInclude( typeof( DelayTableSequence ) )]
    [XmlInclude( typeof( FrequencyTableSequence ) )]
    [XmlInclude( typeof( GradientAmplitudeTableSequence ) )]
    [XmlInclude( typeof( GradientWaveformTableSequence ) )]
    [XmlInclude( typeof( LoopTableSequence ) )]
    [XmlInclude( typeof( ModulationTableSequence ) )]
    [XmlInclude( typeof( PhaseTableSequence ) )]
    [XmlInclude( typeof( RotationAngleTableSequence ) )]
    public class TableSequence : IObjectSequence
    {
        public const string typeTableShape = "Shape";
        public const string typeTable1D2D = "1/2D";
        public const string typeTable1D = "1D";
        public const string typeTable2D = "2D";
        public const string typeTable3D = "3D";
        public const string typeTable4D = "4D";

        private string _text;
        protected DelayAutoTable _delayAuto;
        protected GenerateAutoTable _generateAuto;
        protected List<Value> _valueList;

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public TableSequence( ) 
        {
            _valueList = new List<Value>( );
        }

        /// <summary>
        /// Параметризованный конструктор
        /// </summary>
        /// <param name="@object">Объект последовательности</param>
        /// <param name="name">Название таблицы</param>
        public TableSequence( IObjectStream @object, string name = null )
            : this()
        { 
            TypeTable = GetTypeTable( @object );
            Name = name == null ? GetTitle( @object, TypeTable) : name;
            Data = new DataTable();
            Text = "0";
        }

        /// <summary>
        /// Устанавливает или возвращает имя таблицы
        /// </summary>
        [XmlAttribute]
        public string Name { get; set; }

        /// <summary>
        /// Возвращает текстовое название типа таблицы
        /// </summary>
        [XmlAttribute]
        public string TypeTable { get; set; }

        /// <summary>
        /// Устанавливает или возвращает объект содержаший дополнительные настройки таблицы
        /// </summary>
        [XmlElement]
        public DataTable Data { get; set; }

        /// <summary>
        /// Устанавливает или возвращает объект для автоматической генерации задержек
        /// </summary>
        [XmlElement]
        public DelayAutoTable DelayAuto
        {
            get { return _delayAuto; }
            set
            {
                _delayAuto = value;
                if ( _delayAuto != null) 
                    _delayAuto.Table = this;
            }
        }

        /// <summary>
        /// Устанавливает или возвращает объект для автоматической генерации значений
        /// </summary>
        [XmlElement]
        public GenerateAutoTable GenerateAuto 
        {
            get { return _generateAuto; }
            set
            {
                _generateAuto = value;
                if ( _generateAuto != null)
                    _generateAuto.Table = this;
            }
        }

        /// <summary>
        /// Устанавливает или возвращает текст для не автоматической таблицы
        /// </summary>
        [XmlElement]
        public string Text 
        { 
            get 
            {
                if ( Data.Auto )
                    return null;
                return _text; 
            }
            set 
            {
                if ( _text == value )
                    return;
                _text = value;
                if ( _text != null )
                    ParseTextValues( _text );
            } 
        }

        /// <summary>
        /// Проверяет будет ли отображаться временная задержка на таблице
        /// </summary>
        [XmlIgnore]
        public bool IsTimePoint { get { return TypeTable == "Shape"; } }

        /// <summary>
        /// Проверяет будет ли отображаться шаг на таблице
        /// </summary>
        [XmlIgnore]
        public virtual bool IsStep { get { return false; } }

        /// <summary>
        /// Возвращает текстовое название класса таблицы
        /// </summary>
        [XmlIgnore]
        public virtual string ClassTable { get { return ""; } }

        /// <summary>
        /// Возвращает текст с количеством значений таблицы
        /// </summary>
        [XmlIgnore]
        public string Entries { get { return string.Format( "{0} {1}", _valueList.Count, _valueList.Count == 1 ? "entry" : "entries" ); } }

        /// <summary>
        /// Возвращает текст пояснения для таблицы
        /// </summary>
        [XmlIgnore]
        public virtual string Description { get { return ""; } }

        /// <summary>
        /// Возвращает массив связаных переменных с таблицей
        /// </summary>
        [XmlIgnore]
        public Variable[] Variables 
        { 
            get 
            {
                List<Variable> list = new List<Variable>();
                if ( _delayAuto != null && _delayAuto.IncrementVariable != null )
                    list.Add( _delayAuto.IncrementVariable );
                if ( _generateAuto != null )
                {
                    if ( _generateAuto.StartVariable != null )
                        list.Add( _generateAuto.StartVariable );
                    if ( _generateAuto.IncrementVariable != null )
                        list.Add( _generateAuto.IncrementVariable );
                    if ( _generateAuto.NumberVariable != null )
                        list.Add( _generateAuto.NumberVariable );
                }
                return list.ToArray();
            } 
        }

        /// <summary>
        /// Возвращает текст всплывающей подсказки
        /// </summary>
        [XmlIgnore]
        public string ToolTip 
        { 
            get 
            {
                if (Data.Auto)
                    return ToString( );

                StringBuilder stringBuilder = new StringBuilder( string.Format( "{0} = ", Name ) );
                foreach ( Value value in _valueList )
                {
                    if ( stringBuilder.Length > 50 )
                    {
                        stringBuilder.Append( "... " );
                        break;
                    }
                    stringBuilder.Append( value.ToString( ) + " " );
                }
                if (_valueList.Count <= 1)
                    stringBuilder.Append( string.Format( "({0} entry)", _valueList.Count ) );
                else
                    stringBuilder.Append( string.Format( "({0} entries)", _valueList.Count ) );
                return stringBuilder.ToString( );
            } 
        }

        ///// <summary>
        ///// Статический метод для создания экземпляров класса на основе данных импорта
        ///// </summary>
        ///// <param name="table">Объект содержащий данные импорта</param>
        ///// <returns>Объект таблицы</returns>
        //public static TableSequence CreateInstance( SequenceTable table )
        //{
        //    TableSequence objectTable;
        //    switch ( table.typeOfTable )
        //    {
        //    case 0x00000001:
        //        objectTable = new DelayTableSequence( );
        //        break;
        //    case 0x0000000D:
        //        objectTable = new LoopTableSequence( );
        //        break;
        //    case 0x00004154:
        //    case 0x00004132:
        //        objectTable = new AttenuationTableSequence( );
        //        break;
        //    case 0x00004143:
        //        objectTable = new AcquisitionTableSequence( );
        //        break;
        //    case 0x00004741:
        //        objectTable = new RotationAngleTableSequence( );
        //        break;
        //    case 0x00004752:
        //    case 0x00004732:
        //        objectTable = new GradientAmplitudeTableSequence( );
        //        break;
        //    case 0x00004F31:
        //        objectTable = new FrequencyTableSequence( );
        //        break;
        //    case 0x0000524D:
        //    case 0x00005232:
        //       objectTable = new ModulationTableSequence( );
        //        break;
        //    case 0x00005048:
        //    case 0x00005032:
        //    case 0x00005053:
        //        objectTable = new PhaseTableSequence( );
        //        break;
        //    case 0x00005348:
        //        objectTable = new GradientWaveformTableSequence( );
        //        break;
        //    default:
        //        objectTable = new TableSequence( );
        //        break;
        //    }

        //    objectTable.Name = table.name;
        //    objectTable.TypeTable = objectTable.GetTypeTable( table.dimension );
        //    objectTable.Data = new DataTable( );
        //    objectTable.Data.Auto = table.isAuto;
        //    objectTable.Data.Steps = table.stepsPer360Cycle.ToString();
        //    objectTable.Data.TimePoint = table.timePoint;
        //    objectTable.Text = table.entry;
        //    if ( objectTable._delayAuto != null )
        //    {
        //        objectTable._delayAuto.UseDwellTime = table.useIncrementList;
        //        objectTable._delayAuto.Increment = table.incrementValue;
        //        objectTable._delayAuto.Every = table.incrementOperation == "+ Add" ? 0 : 1;
        //        objectTable._delayAuto.Add = table.incrementOperation == "Every pass" ? 0 : 1;
        //        objectTable._delayAuto.Table = objectTable;
        //    }
        //    if ( objectTable._generateAuto != null )
        //    {
        //        objectTable._generateAuto.Start = table.start;
        //        objectTable._generateAuto.Increment = table.increment;
        //        objectTable._generateAuto.Number = table.number;
        //        objectTable._generateAuto.Table = objectTable;
        //    }
        //    return objectTable;
        //}

        /// <summary>
        /// Клонирует текущий объект
        /// </summary>
        /// <returns>Клон объекта</returns>
        public object Clone( )
        {
            TableSequence obj = New( );
            obj.Name = Name;
            obj.TypeTable = TypeTable;
            obj.Text = Text;
            obj.Data = (DataTable)Data.Clone( );
            obj.DelayAuto = (DelayAutoTable)DelayAuto.Clone( );
            obj.GenerateAuto = (GenerateAutoTable)GenerateAuto.Clone( );
            return obj;
        }

        /// <summary>
        /// Устанавливает спискок значений для таблицы
        /// </summary>
        /// <param name="text">Текст содержаший значения</param>
        public virtual void SetListValues(string text)
        {
            _valueList.Clear( );
            foreach ( Match match in Regex.Matches( text, DoubleValue.stringRegex, RegexOptions.IgnoreCase ) )
            {
                if ( !DoubleValue.IsCheck( match.Value ) )
                    continue;
                Value valueObject = new DoubleValue( match.Value );
                _valueList.Add( valueObject );
            }
        }

        /// <summary>
        /// Перегруженный метод для преобразования класса в строку
        /// </summary>
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder( string.Format( "{0} = ", Name) );
            foreach ( Value value in _valueList )
                stringBuilder.Append( value.ToString( ) + " " );
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Создаёт новый объект класса
        /// </summary>
        protected virtual TableSequence New( ) 
        {
            return new TableSequence( );
        }

        /// <summary>
        /// Парсит текст со значениями переменных и сутанавливает их в список
        /// </summary>
        /// <param name="text">Текст содержаший значения</param>
        private void ParseTextValues( string text )
        { 
            int index;
            StringBuilder stringBuilder = new StringBuilder();
            string[] lines = text.Split( new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries );

            foreach ( string line in lines )
            {
                index = line.IndexOf( ';' );
                if ( index > 0 )
                    stringBuilder.Append( line.Substring( 0, index ) + " " );
                else if ( index < 0 )
                    stringBuilder.Append( line + " " );
            }
            SetListValues( stringBuilder.ToString( ) );
        }

        /// <summary>
        /// Возвращает название типа таблицы
        /// </summary>
        /// <param name="dimension">Индекс типа</param>
        /// <returns>Строка с названием типа таблицы</returns>
        private string GetTypeTable( int dimension )
        {
            switch ( dimension )
            {
            case 0:
                return typeTableShape;
            case 2:
                return typeTable2D;
            case 3:
                return typeTable3D;
            case 4:
                return typeTable4D;
            case 5:
                return typeTable1D2D;
            default:
                return typeTable1D;
            }
        }

        /// <summary>
        /// Возвращает название типа таблицы
        /// </summary>
        /// <param name="@object">Объект последовательности</param>
        /// <returns>Строка с названием типа таблицы</returns>
        private string GetTypeTable( IObjectStream @object )
        {
            switch ( @object.TypeCollection )
            {
            case eTypeObjectCollection._1D:
                if ( @object.Type == eTypeObjectStream.TableShape )
                    return typeTableShape;
                if ( @object.Type == eTypeObjectStream.Table1D2D )
                    return typeTable1D2D;
                else
                    return typeTable1D;
            case eTypeObjectCollection._2D:
                return typeTable2D;
            case eTypeObjectCollection._3D:
                return typeTable3D;
            case eTypeObjectCollection._4D:
                return typeTable4D;
            default:
                return typeTable1D;
            }
        }

        /// <summary>
        /// Возвращает заголовок формы для текущего объекта таблицы
        /// </summary>
        /// <param name="@object">Обект последовательности</param>
        /// <param name="typeTable">Тип таблицы</param>
        /// <returns>Строка заголовка формы</returns>
        private string GetTitle( IObjectStream @object, string typeTable )
        {
            string prefix = @object.Parent.IconType.ToLower( );
            if ( Regex.IsMatch( prefix, @"\d$" ) )
                prefix += "_";
            switch ( typeTable )
            {
            case typeTableShape:
                return Document.LoadDocument.Sequence.GeneratetNameTable( typeTable, "" );
            case typeTable1D:
                return Document.LoadDocument.Sequence.GeneratetNameTable( prefix, "" );
            default:
                return Document.LoadDocument.Sequence.GeneratetNameTable( prefix, ":" + typeTable );
            }
        }
    }
}
