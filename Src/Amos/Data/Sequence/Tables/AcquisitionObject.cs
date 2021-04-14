using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using Amos.TypeFiles.FileBuilders.FileTNT;
using Calculations;
using Calculations.Values;
using Calculations.Variables;
using Tools.Interfaces;

namespace Amos.Data.Sequence.Tables
{
    [Serializable]
    public class AcquisitionObject : IObjectSequence
    {
        private bool _isLinkDashboard;
        private string _asqPoints;
        private string _sweep;
        private string _dwellTime;
        private Compiler _compiler;
        private Dictionary<string, Variable> _variables;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public AcquisitionObject( )
        {
            _variables = new Dictionary<string, Variable>( );
            _compiler = new Compiler( _variables );
        }

        /// <summary>
        /// Параметризованный конструктор
        /// </summary>
        /// <param name="@object">Объект потока для привязки</param>
        public AcquisitionObject( IObjectStream @object )
            : this()
        {
            Event = @object.IndexEvent + 1;
            IsLinkDashboard = true;
        }

        /// <summary>
        /// Параметризованный конструктор
        /// </summary>
        public AcquisitionObject( SequenceAcquation sequenceAcquation, int index )
            : this( )
        {
            Event = index;
            AsqPoints = sequenceAcquation.asqPoint;
            Sweep = sequenceAcquation.sweep;
            DwellTime = sequenceAcquation.dwellTime;
            IsLinkDashboard = sequenceAcquation.isLinkDashboard;
        }

        /// <summary>
        /// Возвращает или устанавливает номер объекта события последовательности
        /// </summary>
        [XmlAttribute]
        public int Event { get; set; }

        /// <summary>
        /// Возвращает или устанавливает текст значения для переменной Asq Points
        /// </summary>
        [XmlAttribute]
        public string AsqPoints 
        {
            get { return _asqPoints; }
            set
            {
                Variable variable = null;
                if ( !_variables.TryGetValue( "Acq. Points", out variable ) || value == null )
                    return;
                if ( !IntegerValue.IsCheck( value ) )
                    throw new Exception( "Not correct format of value, will repeat the entry of data." );
                _asqPoints = SetVariable( variable, value );
            }
        }

        /// <summary>
        /// Возвращает или устанавливает текст значения для переменной Sweep
        /// </summary>
        [XmlAttribute]
        public string Sweep 
        {
            get { return _sweep; }
            set
            {
                Variable variable = null;
                if ( !_variables.TryGetValue( "SW +/-", out variable ) || value == null )
                    return;
                if ( !FrequencyValue.IsCheck( value ) )
                    throw new Exception( "Not correct format of value, will repeat the entry of data." );
                _sweep = SetVariable( variable, value );
                _dwellTime = _variables["Dwell Time"].ToString();
            }
        }

        /// <summary>
        /// Возвращает или устанавливает текст значения для переменной Dwell Time
        /// </summary>
        [XmlAttribute]
        public string DwellTime 
        {
            get { return _dwellTime; }
            set
            {
                Variable variable = null;
                if ( !_variables.TryGetValue( "Dwell Time", out variable ) || value == null)
                    return;
                if ( !TimeValue.IsCheck( value ) )
                    throw new Exception( "Not correct format of value, will repeat the entry of data." );
                _dwellTime = SetVariable( variable, value );
                _sweep = _variables["SW +/-"].ToString( );
            }
        }

        /// <summary>
        /// Возвращает или устанавливает состояние привязки переменных к Dashboard
        /// </summary>
        [XmlAttribute( "LinkDashboard" )]
        public bool IsLinkDashboard
        {
            get 
            { 
                if ( _variables.Count == 0 )
                    ChangeVariables( );
                return _isLinkDashboard;
            }
            set
            {
                _isLinkDashboard = value;
                ChangeVariables( );
            }
        }

        /// <summary>
        /// Возвращает или устанавливает текст значения для переменной Filter
        /// </summary>
        [XmlIgnore]
        public string Filter { get { return _variables["Filter"].ToString( ); } }

        /// <summary>
        /// Возвращает или устанавливает текст значения для переменной Asq Time
        /// </summary>
        [XmlIgnore]
        public string AsqTime { get { return _variables["Acq. Time"].ToString( ); } }

        /// <summary>
        /// Возвращает текст всплывающей подсказки
        /// </summary>
        [XmlIgnore]
        public string ToolTip { get { return "Acq"; } }

        /// <summary>
        /// Клонирует текущий объект
        /// </summary>
        /// <returns>Клон объекта</returns>
        public object Clone( )
        {
            AcquisitionObject obj = new AcquisitionObject( );
            obj.Event = Event;
            obj.AsqPoints = AsqPoints;
            Sweep = Sweep;
            DwellTime = DwellTime;
            IsLinkDashboard = IsLinkDashboard;
            return obj;
        }

        /// <summary>
        /// Устанавливает выражение в переменную
        /// </summary>
        /// <param name="variable">Объект переменной</param>
        /// <param name="text">текст выражения</param>
        private string SetVariable( Variable variable, string text )
        {
            if ( _isLinkDashboard )
            {
                variable.SetValue( Program.Document.Dashboard.Compiler, text );
                Program.Document.Dashboard.Refresh( );
            }
            else
                variable.SetValue( _compiler, text );
            return variable.ToString( );
        }

        /// <summary>
        /// Вызывает пересчёт и обновление значений привязанных переменных
        /// </summary>
        private void ChangeVariables( )
        {
            _variables.Clear( );
            if ( _isLinkDashboard )
            {
                _variables.Add( "Points 1D", Program.Document.Dashboard["Points 1D"] );
                _variables.Add( "Acq. Points", Program.Document.Dashboard["Acq. Points"] );
                _variables.Add( "SW +/-", Program.Document.Dashboard["SW +/-"] );
                _variables.Add( "Filter", Program.Document.Dashboard["Filter"] );
                _variables.Add( "Dwell Time", Program.Document.Dashboard["Dwell Time"] );
                _variables.Add( "Acq. Time", Program.Document.Dashboard["Acq. Time"] );

                _asqPoints = _variables["Acq. Points"].ToString( );
                _sweep = _variables["SW +/-"].ToString( );
                _dwellTime = _variables["Dwell Time"].ToString( );
            }
            else
            {
                _variables.Add( "Points 1D", Program.Document.Dashboard["Points 1D"] );
                _variables.Add( "Acq. Points", (Variable)Program.Document.Dashboard["Acq. Points"].Clone( ) );
                _variables.Add( "SW +/-", (Variable)Program.Document.Dashboard["SW +/-"].Clone( ) );
                _variables.Add( "Filter", (Variable)Program.Document.Dashboard["Filter"].Clone( ) );
                _variables.Add( "Dwell Time", (Variable)Program.Document.Dashboard["Dwell Time"].Clone( ) );
                _variables.Add( "Acq. Time", (Variable)Program.Document.Dashboard["Acq. Time"].Clone( ) );

                foreach ( Variable variable in _variables.Values )
                    variable.Calculate( _compiler, false );

                _asqPoints = SetVariable( _variables["Acq. Points"], _asqPoints );
                _sweep = SetVariable( _variables["SW +/-"], _sweep );
                _dwellTime = SetVariable( _variables["Dwell Time"], _dwellTime );
            }
        }
    }
}
