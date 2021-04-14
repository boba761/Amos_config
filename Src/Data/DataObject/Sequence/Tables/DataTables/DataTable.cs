using System;
using System.Xml.Serialization;
using Calculations.Values;

namespace Data.DataObject.Sequence.Tables.DataTables
{
    /// <summary>
    /// Клас для дополнительных параметров таблицы
    /// </summary>
    [Serializable]
    public class DataTable : ICloneable
    {
        private bool _digrres;
        private TimeValue _timePoint;
        private DoubleValue _steps;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public DataTable( ) { }

        /// <summary>
        /// Параметризованный конструктор
        /// </summary>
        public DataTable( bool digrres = true )
        {
            Degrees = digrres;
        }

        /// <summary>
        /// Возврашает или устанавливает является ли таблица автоматической
        /// </summary>
        [XmlAttribute]
        public bool Auto { get; set; }
        /// <summary>
        /// Возвращает или устанавливает временной интервал для значений таблицы
        /// </summary>
        [XmlAttribute]
        public string TimePoint
        {
            get
            {
                if ( _timePoint == null )
                    return string.Empty;
                else
                    return _timePoint.ToString( );
            }
            set
            {
                if ( !string.IsNullOrEmpty( value ) )
                {
                    if ( _timePoint == null )
                        _timePoint = new TimeValue( value );
                    _timePoint.Data = value;
                }
                else
                    _timePoint = null;
            }
        }
        /// <summary>
        /// Возвращает или устанавливает включение ли ???????
        /// </summary>
        [XmlAttribute]
        public bool Degrees 
        {
            get { return _digrres; }
            set
            {
                if ( _digrres == value )
                    return;
                _digrres = value;
                if ( _digrres )
                    Steps = "1";
            }
        }
        /// <summary>
        /// Возвращает или устанавливает строковое значение шага значений таблицы
        /// </summary>
        [XmlAttribute]
        public string Steps
        {
            get
            {
                if ( _steps == null )
                    return string.Empty;
                else
                    return _steps.ToString( );
            }
            set
            {
                if ( !string.IsNullOrEmpty( value ) )
                {
                    if ( _steps == null )
                        _steps = new DoubleValue( value );
                    _steps.Data = value;
                }
                else
                    _steps = null;
            }
        }

        /// <summary>
        /// Клонирует текущий объект
        /// </summary>
        /// <returns>Клон объекта</returns>
        public object Clone( )
        {
            DataTable obj = new DataTable( Degrees );
            obj.Auto = Auto;
            if ( _timePoint != null )
                obj._timePoint = (TimeValue)( _timePoint as ICloneable ).Clone( );
            if ( _steps != null )
                obj._steps = (DoubleValue)( _steps as ICloneable ).Clone( );
            return obj;
        }
    }
}
