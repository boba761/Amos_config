using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Collections;
using Calculations;
using Calculations.Variables;
using Calculations.Values;

namespace Data.DataObject
{
    [XmlRoot]
    public class DashboardData 
    {
        public const string rootVariable = "/";

        private Dictionary<string, Variable> _variables;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public DashboardData( )
        {
            _variables = new Dictionary<string, Variable>( );
            Compiler = new Compiler( _variables );
            MasterCollectionVariable = new CollectionVariable( rootVariable );
            CurrentCollectionVariable = new CollectionVariable( rootVariable );
            InitializeVariables( );
        }

        /// <summary>
        /// Возвращает массив переменных
        /// </summary>
        internal VariableBase[] Variables 
        { 
            get { return CurrentCollectionVariable.VariableChilds; }
            set 
            {
                AddListVariables( value );
                CurrentCollectionVariable = new CollectionVariable( rootVariable );
                CurrentCollectionVariable.VariableChilds = value;
                Compile();
            } 
        }

        /// <summary>
        /// Возврашает или устанавливает перечислитель переменных
        /// </summary>
        [XmlIgnore]
        public IEnumerable<Variable> EnumerableVariables { get { return _variables.Values; } }

        /// <summary>
        /// Возвращает объект компилятора выражений
        /// </summary>
        [XmlIgnore]
        internal Compiler Compiler { get; private set; }

        /// <summary>
        /// Возвращает мастер коллекцию  переменных
        /// </summary>
        [XmlIgnore]
        public CollectionVariable MasterCollectionVariable { get; private set; }

        /// <summary>
        /// Возвращает текущую коллекцию переменных
        /// </summary>
        [XmlIgnore]
        public CollectionVariable CurrentCollectionVariable { get; set; }

        /// <summary>
        /// Возвращает коллекцию глобальных переменных
        /// </summary>
        [XmlIgnore]
        public CollectionVariable GlobalCollectionVariable { get { return CurrentCollectionVariable.FindCollectionVariable( "Global variables" ); } }

        /// <summary>
        /// Возвращает коллекцию локальных переменных
        /// </summary>
        [XmlIgnore]
        public CollectionVariable LocalCollectionVariable { get { return CurrentCollectionVariable.FindCollectionVariable( "Local variables" ); } }

        /// <summary>
        /// Возвращает коллекцию  переменных последовательности
        /// </summary>
        [XmlIgnore]
        public CollectionVariable SequenceCollectionVariable 
        { 
            get 
            {
                if ( Document.LoadDocument.Sequence == null )
                    return null;
                return Document.LoadDocument.Sequence.SequenceCollectionVariable;
            } 
        }

        /// <summary>
        /// Возвращает коллекцию табличных переменных
        /// </summary>
        [XmlIgnore]
        public CollectionVariable TableCollectionVariable
        {
            get
            {
                if ( Document.LoadDocument.Sequence == null )
                    return null;
                return Document.LoadDocument.Sequence.TableCollectionVariable;
            }
        }

        internal Variable this[string name]
        {
            get 
            {
                Variable variable = null;
                _variables.TryGetValue( name, out variable );
                return variable;
            }
        }

        /// <summary>
        /// Производит действия для создания объектов
        /// </summary>
        internal void Create( )
        {
            AddCollection( "Global variables", MasterCollectionVariable, true );
            AddCollection( "Local variables", MasterCollectionVariable, true );
            CurrentCollectionVariable = (CollectionVariable)MasterCollectionVariable.Clone( );
            Compile( );
        }

        /// <summary>
        /// Производит действия при закрытии объекта
        /// </summary>
        internal void Close( )
        {
            _variables.Clear();
        }

        /// <summary>
        /// Обновляет состояние объета
        /// </summary>
        internal void Refresh( )
        {
            foreach ( Variable variable in SequenceCollectionVariable.EnumerableVariables )
            {
                if ( _variables.ContainsKey( variable.Name ) )
                    _variables[variable.Name] = variable;
                else
                    _variables.Add( variable.Name, variable );
            }

            foreach ( Variable variable in TableCollectionVariable.EnumerableVariables )
            {
                if ( _variables.ContainsKey( variable.Name ) )
                    _variables[variable.Name] = variable;
                else
                    _variables.Add( variable.Name, variable );
            }
            Compile( );
        }

        /// <summary>
        /// Проверяет на существование в списке переменной
        /// </summary>
        /// <param name="nameVariable">Название переменной</param>
        public bool Contains( string nameVariable )
        {
            return _variables.ContainsKey( nameVariable );
        }

        /// <summary>
        /// Устанавливает значение для переменной
        /// </summary>
        /// <param name="name">Название переменной</param>
        /// <param name="val">Новое значение</param>
        public void SetVariable( string name, object val )
        {
            ( (Variable)_variables[name] ).SetValue( Compiler, val );
        }

        /// <summary>
        /// Устанавливает список локальных переменных
        /// </summary>
        internal void SetLocalVariables( IList variables )
        {
            foreach ( LocalVariable variable in LocalCollectionVariable.EnumerableVariables )
                _variables.Remove( variable.Name );
            LocalCollectionVariable.Clear( );
            foreach ( LocalVariable variable in variables )
            {
                _variables.Add( variable.Name, variable );
                LocalCollectionVariable.Add( variable );
            }
        }

        /// <summary>
        /// Устанавливает список глобальных переменных
        /// </summary>
        internal void SetGlobalVariables( IList variables )
        {
            foreach ( GlobalVariable variable in GlobalCollectionVariable.EnumerableVariables )
                _variables.Remove( variable.Name );
            GlobalCollectionVariable.Clear( );
            foreach ( GlobalVariable variable in variables )
            {
                if ( variable.IsAdd )
                {
                    _variables.Add( variable.Name, variable );
                    GlobalCollectionVariable.Add( variable );
                }
            }
            Compile( );
        }

        /// <summary>
        /// Добавляет список переменных
        /// </summary>
        private void AddListVariables( VariableBase[] variables )
        {
            foreach ( VariableBase variable in variables )
            {
                if ( variable is Variable )
                {
                    if ( _variables.ContainsKey( variable.Name ) )
                        _variables[variable.Name] = (Variable)variable;
                    else
                        _variables.Add( variable.Name, (Variable)variable );
                }
                else
                    AddListVariables( ( (CollectionVariable)variable ).VariableChilds );
            }
        }

        /// <summary>
        /// Компилирует выражения переменных
        /// </summary>
        private void Compile()
        {
            foreach ( Variable variable in _variables.Values )
                variable.Compile( Compiler );
        }

        /// <summary>
        /// Инициализирует переменные объекта
        /// </summary>
        private void InitializeVariables()
        {
            InitializeCollection( MasterCollectionVariable );
            InitializeSystemVariables();
        }

        /// <summary>
        /// Добавление в объек колекции переменных
        /// </summary>
        /// <param name="name">Название коллекции</param>
        /// <param name="parentCollection">Владелец коллекции</param>
        private CollectionVariable AddCollection( string name, CollectionVariable parentCollection, bool isReadOnly = false )
        {
            CollectionVariable item = new CollectionVariable( name, isReadOnly );
            parentCollection.Add( item );
            return item;
        }

        /// <summary>
        /// Добавляем переменную
        /// </summary>
        /// <param name="name">Название переменной</param>
        /// <param name="owner">Название коллесции переменных</param>
        /// <param name="isEdit">Редактируемая переменная или нет</param>
        /// <param name="type">Тип переменной</param>
        /// <param name="expression">Строка выражения</param>
        /// <param name="value">Объект значения</param>
        /// <param name="min">Объект минимального  значения</param>
        /// <param name="max">Объект максимального значения</param>
        /// <param name="step">Объект значения шага</param>
        private void AddVariable( string name, string owner, bool isEdit, Type type, string expression, object value, object min = null, object max = null, object step = null )
        {
            Variable item = null;
            if ( _variables.TryGetValue( name, out item ) == false )
            {
                item = new SystemVariable( name, !isEdit, (Value)Activator.CreateInstance( type, new object[] { value, expression } ) );
                if (min != null)
                    item.Min = (Value)Activator.CreateInstance(type, new object[] { min, null });
                if (max != null)
                    item.Max = (Value)Activator.CreateInstance( type, new object[] { max, null } );
                if (step != null)
                    item.Step = (Value)Activator.CreateInstance( type, new object[] { step, null } );
                _variables.Add( item.Name, item );
                item.IsLookExpression = true;
            }
            if ( owner != null )
                MasterCollectionVariable.FindCollectionVariable( owner ).Add( item );
        }

        /// <summary>
        /// Инициализирует коллекцию переменных
        /// </summary>
        private void InitializeCollection( CollectionVariable collection )
        {
            AddCollection( "Acquisition", collection );
            AddCollection( "Frequency", collection );
            AddCollection( "Hardware", collection );
            CollectionVariable temp =  AddCollection( "Multi Rec.", collection ); 
            for ( int i = 1; i <= 16; i++ )
                AddCollection( string.Format( "Channel {0:D2}", i ), temp );
            AddCollection( "Processing", collection);
            AddCollection( "Grad. Preemph.", collection );
            AddCollection( "B0 Compensation", collection );
            AddCollection( "Misc", collection );
        }

        /// <summary>
        /// Инициализируем системные переменные
        /// </summary>
        private void InitializeSystemVariables()
        {
            AddVariable( "Nucleus", "Acquisition", true, typeof( StringValue ), null, "H1" );
            AddVariable( "Acq. Points", "Acquisition", true, typeof( IntegerValue ), null, 1, 1, "=[Points 1D]", 1 );
            AddVariable( "Points 1D", "Acquisition", true, typeof( IntegerValue ), null, 1, 128, 1000000, 1 );
            AddVariable( "SW +/-", "Acquisition", true, typeof( FrequencyValue ), "=1/(2*[Dwell Time])", "50 kHz", "1.5259 kHz", "500 kHz", null );
            AddVariable( "Filter", "Acquisition", false, typeof( FrequencyValue ), "=[SW +/-]", "50 kHz" );
            AddVariable( "Dwell Time", "Acquisition", true, typeof( TimeValue ), "=1/(2*[SW +/-])", "10u", "1n", "327.68u", "1u" );
            AddVariable( "Acq. Time", "Acquisition", false, typeof( TimeValue ), "=[Points 1D]*[Dwell Time]", null );
            AddVariable( "Scans 1D", "Acquisition", true, typeof( IntegerValue ), null, 1, 1, int.MaxValue, 1 );
            AddVariable( "Actual Scans 1D", "Acquisition", false, typeof( IntegerValue ), null, 1 );
            AddVariable( "Dummy Scans", "Acquisition", true, typeof( IntegerValue ), null, 0, 0, int.MaxValue, 1 );
            AddVariable( "Scan Start 1D", "Acquisition", true, typeof( IntegerValue ), null, 1, 1, int.MaxValue, 1 );
            AddVariable( "Last Delay", "Acquisition", true, typeof( TimeValue ), null, "1s", "1n", "3600s", null );
            AddVariable( "Repeat Times", "Acquisition", true, typeof( IntegerValue ), null, 1, 1, int.MaxValue, 1 );
            AddVariable( "Observe Freq.", "Frequency", false, typeof( FrequencyValue ),
                "=([Observe Ch.]=1?[F1 Freq.]:([Observe Ch.]=2?[F2 Freq.]:([Observe Ch.]=3?[F3 Freq.]:[F4 Freq.])))", "40 MHz", "40 MHz", "150 MHz", null );
            AddVariable( "Observe Ch.", "Frequency", true, typeof( IntegerValue ), null, 1, 1, 4, 1 );
            AddVariable( "Trans. Gain", "Hardware", true, typeof( IntegerValue ), null, 0, 0, 100, 1 );
            AddVariable( "Receiver Gain", "Hardware", true, typeof( IntegerValue ), null, 0, 0, 100, 1 );
            AddVariable( "Receiver Phase", "Hardware", true, typeof( CornerValue ), null, 0, 0, 270, 90 );
            AddVariable( "Dec. Attn.", "Hardware", true, typeof( IntegerValue ), null, 0, 0, 100, 1 );
            AddVariable( "Dec. BW", "Hardware", true, typeof( IntegerValue ), null, 0, 0, int.MaxValue, 1 );
            AddVariable( "Set Temp.", "Hardware", true, typeof( TemperaturaValue ), null, "293 K", 0, 4096, 0.1 );
            AddVariable( "Actual Temp.", "Hardware", false, typeof( TemperaturaValue ), null, "293 K", 0, 4096, 0.1 );
            AddVariable( "Lock Solvent", "Hardware", true, typeof( StringValue ), null, "" );
            AddVariable( "Lock Field", "Hardware", true, typeof( IntegerValue ), null, 0 );
            AddVariable( "Lock Power", "Hardware", true, typeof( IntegerValue ), null, 0 );
            AddVariable( "Lock Gain", "Hardware", true, typeof( IntegerValue ), null, 0 );
            AddVariable( "Lock Phase", "Hardware", true, typeof( IntegerValue ), null, 0 );
            AddVariable( "Lock ppm", "Hardware", true, typeof( DoubleValue ), null, 0 );
            AddVariable( "Shim Units", "Hardware", false, typeof( DoubleValue ), null, 0 );
            AddVariable( "Grd. Orientation", "Hardware", true, typeof( StringValue ), null, "XYZ" );
            AddVariable( "Grd. Theta", "Hardware", true, typeof( CornerValue ), null, 0, 0, 360, 1 );
            AddVariable( "Grd. Phi", "Hardware", true, typeof( CornerValue ), null, 0, 0, 360, 1 );
            AddVariable( "Date", "Misc", false, typeof( StringValue ), null, "" );
            AddVariable( "File Name", "Misc", false, typeof( StringValue ), null, "" );
            AddVariable( "Magnet Field", "Misc", true, typeof( DoubleValue ), null, 4, 1, 9, null );
            AddVariable( "Obs. Ref. Freq.", "Misc", false, typeof( FrequencyValue ), null, "0 MHz" );
            AddVariable( "Absolute Freq.", "Misc", false, typeof( FrequencyValue ), null, "0 MHz" );
            AddVariable( "Exp. Start Time", "Misc", false, typeof( DateTimeValue ), null, DateTime.UtcNow );
            AddVariable( "Exp. Finish Time", "Misc", false, typeof( DateTimeValue ), null, DateTime.UtcNow );
            AddVariable( "Exp. Elapsed Time", "Misc", false, typeof( TimeSpanValue ), "=[Exp. Finish Time]-[Exp. Start Time]", 0 );
            AddVariable( "Cycle period", "Misc", true, typeof( DoubleValue ), null, 1, 1, null, null );

            for ( int i = 1; i <= 4; i++ )
            {
                AddVariable( string.Format( "F{0} Freq.", i ), "Frequency", false, typeof( FrequencyValue ), string.Format( "=[F{0} Base Freq.]+[F{0} Offset Freq.]", i ),
                    "40 MHz", "0 MHz", "300 MHz", null );
                AddVariable( string.Format( "F{0} Base Freq.", i ), "Frequency", true, typeof( FrequencyValue ), null, "40 MHz", "0 MHz", "300 MHz", null );
                AddVariable( string.Format( "F{0} Offset Freq.", i ), "Frequency", true, typeof( FrequencyValue ), null, "0 kHz", "-300 MHz", "300 MHz", null );
                if ( i >= 2 )
                {
                    AddVariable( string.Format( "SW {0}D", i ), "Acquisition", true, typeof( FrequencyValue ), string.Format( "=1/(2*[Dwell {0}D])", i ),
                        "50 kHz", "1.5259 kHz", "500 kHz", null );
                    AddVariable( string.Format( "Dwell {0}D", i ), "Acquisition", true, typeof( TimeValue ), string.Format( "=1/(2*[SW {0}D])", i ), "10u", "1n", "327.68u", "1u" );
                    AddVariable( string.Format( "Points {0}D", i ), "Acquisition", true, typeof( IntegerValue ), null, 1, 1, 1024, 1 );
                    AddVariable( string.Format( "Actual Points {0}D", i ), "Acquisition", true, typeof( IntegerValue ), null, 1, 1, string.Format( "=[Points {0}D]", i ), 1 );
                    AddVariable( string.Format( "Points Start {0}D", i ), "Acquisition", true, typeof( IntegerValue ), null, 1, 1, int.MaxValue, 1 );
                }
            }

            for ( int i = 1; i <= 16; i++ )
            {
                AddVariable( string.Format( "Rec. Gain Ch{0}", i ), string.Format( "Channel {0:D2}", i ), true, typeof( IntegerValue ), null, 0, 0, 100, 1 );
                AddVariable( string.Format( "Obs. Freq. Rec{0}", i ), string.Format( "Channel {0:D2}", i ), true, typeof( FrequencyValue ), null, "40 MHz", "40 MHz", "80 MHz", null );
                AddVariable( string.Format( "Obs. Ch. Rec{0}", i ), string.Format( "Channel {0:D2}", i ), true, typeof( IntegerValue ), null, 1, 1, 4, 1 );
                AddVariable( string.Format( "Rec. Phase Ch{0}", i ), string.Format( "Channel {0:D2}", i ), true, typeof( IntegerValue ), null, 0, 0, 360, 90 );
                AddVariable( string.Format( "Points 1D Rec{0}", i ), string.Format( "Channel {0:D2}", i ), true, typeof( IntegerValue ), null, 128, 128, 1024, 1 );
                AddVariable( string.Format( "Acq. Points Rec{0}", i ), string.Format( "Channel {0:D2}", i ), true, typeof( IntegerValue ),
                    null, 128, 1, string.Format( "=[Points 1D Rec{0}]", i ), 1 );
                AddVariable( string.Format( "SW +/- Rec{0}", i ), string.Format( "Channel {0:D2}", i ), true, typeof( FrequencyValue ),
                    string.Format( "=1/(2*[Dwell Time Rec{0}])", i ), "50 kHz", "1.5259 kHz", "500 kHz", null );
                AddVariable( string.Format( "Filter Rec{0}", i ), string.Format( "Channel {0:D2}", i ), false, typeof( FrequencyValue ),
                    string.Format( "=[SW +/- Rec{0}]", i ), "50 kHz" );
                AddVariable( string.Format( "Dwell Time Rec{0}", i ), string.Format( "Channel {0:D2}", i ), true, typeof( TimeValue ),
                    string.Format( "=1/(2*[SW +/- Rec{0}])", i ), "10u", "1u", "327.68u", "1u" );
                AddVariable( string.Format( "Acq. Time Rec{0}", i ), string.Format( "Channel {0:D2}", i ), false, typeof( TimeValue ),
                    string.Format( "=[Points 1D Rec{0}]*[Dwell Time Rec{0}]", i ), "1.28m" );
            }

            char[] leter = new char[] { 'x', 'y', 'z' };
            for ( int i = 0; i < leter.Length; i++ )
            {
                AddVariable( "DC." + leter[i], "Grad. Preemph.", true, typeof( DoubleValue ), null, 0, -100, 100, 0.1 );
                AddVariable( "DC.b" + leter[i], "B0 Compensation", true, typeof( DoubleValue ), null, 0, -100, 100, 0.1 );
                for ( int j = 0; j <= 5; j++ )
                {
                    AddVariable( string.Format( "A{0}.{1}", j, leter[i] ), "Grad. Preemph.", true, typeof( DoubleValue ), null, 0, -100, 100, 0.1 );
                    AddVariable( string.Format( "A{0}.b{1}", j, leter[i] ), "B0 Compensation", true, typeof( DoubleValue ), null, 0, -100, 100, 0.1 );
                    if ( j > 0 )
                    {
                        AddVariable( string.Format( "T{0}.{1}", j, leter[i] ), "Grad. Preemph.", true, typeof( TimeValue ), null, 0.00002 * Math.Pow( 10, j ) );
                        AddVariable( string.Format( "T{0}.b{1}", j, leter[i] ), "B0 Compensation", true, typeof( TimeValue ), null, 0.00002 * Math.Pow( 10, j ) );
                    }
                }
            }
        }
    }
}
