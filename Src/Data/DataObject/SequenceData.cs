using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;
using Calculations.Variables;
using Tools.Interfaces;
using Data.DataObject.Sequence;

namespace Data.DataObject
{
    [XmlRoot]
    public class SequenceData
    {
        public delegate void OnInsertColumn( int indexColumn );
        public delegate void OnRemoveColumn( int indexColumn );
        
        private static Dictionary<string, Variable> _loadSequenceVariables;
        private static Dictionary<string, Variable> _loadTableVariables; 
        private static Dictionary<string, TableSequence> _loadTables;
        private static Dictionary<string, List<IObjectStream>> _loadVariableObjects;
        private static Dictionary<string, List<IObjectStream>> _loadTableObjects;

        private int _column;
        private List<StreamSequence> _streamtList;
        private Dictionary<string, Variable> _tableVariables;
        private Dictionary<string, Variable> _sequenceVariables;
        private Dictionary<string, TableSequence> _tables;
        private Dictionary<string, List<IObjectStream>> _variableObjects;
        private Dictionary<string, List<IObjectStream>> _tableObjects;

        public event OnInsertColumn onInsertColumn;
        public event OnRemoveColumn onRemoveColumn;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public SequenceData( )
        {
            _loadSequenceVariables = new Dictionary<string, Variable>( );
            _loadTableVariables = new Dictionary<string, Variable>( );
            _loadTables = new Dictionary<string, TableSequence>( );
            _loadVariableObjects = new Dictionary<string, List<IObjectStream>>( );
            _loadTableObjects = new Dictionary<string, List<IObjectStream>>( );

            _streamtList = new List<StreamSequence>( );
            _tableVariables = new Dictionary<string, Variable>( );
            _sequenceVariables = new Dictionary<string, Variable>( );
            _tables = new Dictionary<string, TableSequence>( );
            _variableObjects = new Dictionary<string, List<IObjectStream>>( );
            _tableObjects = new Dictionary<string, List<IObjectStream>>( );
        }

        [XmlAttribute( "Events" )]
        public int EventCount 
        {
            get { return _streamtList[0].EventCount; }
            set { _column = value; }
        }

        [XmlArray( "SequenceVariableItems" )]
        public Variable[] SequenceVariables
        {
            get { return _sequenceVariables.Values.ToArray( ); }
            set
            {
                _loadSequenceVariables.Clear( );
                _loadVariableObjects.Clear( );
                foreach ( Variable variable in value )
                {
                    if ( Document.LoadDocument.Dashboard.Contains( variable.Name ) )
                        continue;
                    _loadSequenceVariables.Add( variable.Name, variable );
                    _loadVariableObjects.Add( variable.Name, new List<IObjectStream>( ) );
                }
            }
        }

        [XmlArray( "TableVariableItems" )]
        public Variable[] TableVariables
        {
            get { return _tableVariables.Values.ToArray( ); }
            set
            {
                _loadTableVariables.Clear( );
                foreach ( Variable variable in value )
                {
                    if ( Document.LoadDocument.Dashboard.Contains( variable.Name ) )
                        continue;
                    _loadTableVariables.Add( variable.Name, variable );
                }
            }
        }

        [XmlArray( "TableItems" )]
        public TableSequence[] Tables 
        { 
            get { return _tables.Values.ToArray(); } 
            set 
            {
                _loadTables.Clear( );
                _loadTableObjects.Clear( );
                foreach ( TableSequence table in value )
                {
                    _loadTables.Add( table.Name, table );
                    _loadTableObjects.Add( table.Name, new List<IObjectStream>( ) );
                }
            } 
        }

        [XmlArray( "ElementItems" )]
        public StreamSequence[] Streams 
        { 
            get { return _streamtList.ToArray( ); }
            set 
            { 
                _streamtList = value.ToList( );
                foreach ( StreamSequence element in _streamtList )
                    element.SetLoadEvents( _column );

                _sequenceVariables = new Dictionary<string,Variable>( _loadSequenceVariables );
                _tableVariables = new Dictionary<string,Variable>(_loadTableVariables);
                _tables = new Dictionary<string, TableSequence>( _loadTables );
                _variableObjects = new Dictionary<string, List<IObjectStream>>( _loadVariableObjects );
                _tableObjects = new Dictionary<string, List<IObjectStream>>( _loadTableObjects );

                _loadSequenceVariables.Clear( );
                _loadTableVariables.Clear( );
                _loadTables.Clear( );
                _loadVariableObjects.Clear( );
                _loadTableObjects.Clear( );
            }
        }

        [XmlIgnore]
        internal CollectionVariable SequenceCollectionVariable
        {
            get
            {
                CollectionVariable collection = new CollectionVariable( "Sequence Variables" );
                foreach(Variable variable in _sequenceVariables.Values )
                    collection.Add(variable);
                return collection;
            }
        }
        [XmlIgnore]
        internal CollectionVariable TableCollectionVariable
        {
            get
            {
                ScanTableVariable( );
                CollectionVariable collection = new CollectionVariable( "Table Variables" );
                foreach ( Variable variable in _tableVariables.Values )
                    collection.Add( variable );
                return collection;
            }
        }

        /// <summary>
        /// Возвращает таблицу последовательности
        /// </summary>
        internal TableSequence GetTable( IObjectStream @object, string nameTable )
        {
            TableSequence table = null;
            if ( _loadTables.TryGetValue( nameTable, out table ) )
                _loadTableObjects[table.Name].Add( @object );
            return table;
        }

        /// <summary>
        /// Возвращает переменную последовательности
        /// </summary>
        internal Variable GetSequenceVariable( IObjectStream @object, string nameVariable )
        {
            Variable variable = null;
            if ( Document.LoadDocument.Dashboard.Contains( nameVariable ) )
                return Document.LoadDocument.Dashboard[nameVariable];
            if ( _loadSequenceVariables.TryGetValue( nameVariable, out variable ) == false)
                throw new Exception( "Error of load, a variable [" + nameVariable + "] is not found." );
            _loadVariableObjects[variable.Name].Add( @object );
            return variable;
        }

        /// <summary>
        /// Возвращает табличную переменную последовательности
        /// </summary>
        internal Variable GetTableVariable( string nameVariable )
        {
            Variable variable =  Document.LoadDocument.Dashboard[nameVariable];
            if (variable != null)
                return variable;
            if ( _loadTableVariables.TryGetValue( nameVariable, out variable ) == false )
                throw new Exception( "Error of load, a variable [" + nameVariable + "] is not found." );
            return variable;
        }

        /// <summary>
        /// Выполняем действие по созданию объекта класса
        /// </summary>
        internal void Create( string fileName )
        {
            LoadStreamData( fileName );
            InsertEvent( 0 );
        }

        /// <summary>
        /// Выполняем действия по закрытию объекта класса
        /// </summary>
        internal void Close( )
        {
        }

        /// <summary>
        /// Обновляет состояние объекта
        /// </summary>
        internal void Refresh( )
        {
        }

        /// <summary>
        /// Устанавливаем данные импорта
        /// </summary>
        //public void SetDataImport( PulseSequence pulseSequence )
        //{
        //    List<Variable> variablelist = new List<Variable>();
        //    List<TableSequence> tableList = new List<TableSequence>( );
        //    List<StreamSequence> streamList = new List<StreamSequence>( );

        //    if ( pulseSequence.sequenceParameters.variableSequence != null )
        //    {
        //        foreach ( SequenceParameters.Parameter parametr in pulseSequence.sequenceParameters.variableSequence )
        //            variablelist.Add( new SequenceVariable( parametr ) );
        //    }
        //    SequenceVariables = variablelist.ToArray( );

        //    variablelist.Clear( );
        //    if ( pulseSequence.sequenceParameters.variableTable != null )
        //    {
        //        foreach ( SequenceParameters.Parameter parametr in pulseSequence.sequenceParameters.variableTable )
        //            variablelist.Add( new TableVariable( parametr ) );
        //    }
        //    TableVariables = variablelist.ToArray( );

        //    foreach ( SequenceTable table in pulseSequence.sequenceTables )
        //        tableList.Add( TableSequence.CreateInstance( table ) );
        //    Tables = tableList.ToArray( );

        //    foreach ( SequenceRow row in pulseSequence.sequenceRows )
        //        streamList.Add( new StreamSequence( row ) );
        //    Streams = streamList.ToArray( );
        //}

        /// <summary>
        /// Добавляет пустое событие в последовательность
        /// </summary>
        internal void InsertEvent( int indexColumn )
        {
            foreach ( IStream element in _streamtList )
                element.InsertEvent( indexColumn );
            if ( onInsertColumn != null )
                onInsertColumn( indexColumn );
        }

        /// <summary>
        /// Удасяет событие из схемы
        /// </summary>
        internal void RemoveEvent( int indexColumn )
        {
            foreach ( IStream element in _streamtList )
                element.RemoveEvent( indexColumn );
            if ( onRemoveColumn != null )
                onRemoveColumn( indexColumn );
        }

        /// <summary>
        /// Устанавливаем объек в последовательность
        /// </summary>
        internal void SetObject( IObjectStream @object )
        {
            ObjectStream oldObject = (ObjectStream)@object.Parent[@object.TypeCollection][@object.IndexEvent];
            if ( oldObject.Type == @object.Type )
                SetLinkObject( @object, oldObject );
            else
            {
                if ( ((ObjectStream)@object).Variable != null )
                {
                    //if ( Document.ActiveDocument.Dashboard[@object.Variable.Name] != null )
                    //    throw new Exception( "For the variables of sequence it is impossible to use the names of system variables." );
                    if ( _sequenceVariables.ContainsKey( ( (ObjectStream)@object ).Variable.Name ) &&
                        _sequenceVariables[( (ObjectStream)@object ).Variable.Name].Value.GetType( ) != ( (ObjectStream)@object ).Variable.Value.GetType( ) )
                        throw new Exception( "It is impossible to use this variable for this cell, the types of values do not coincide." );
                }
                RemoveLinkObject( oldObject );
                AddLinkToObject( @object );
            }
            @object.Parent[@object.TypeCollection][@object.IndexEvent] = @object;
        }

        /// <summary>
        /// Проверяет на наличие табличной переменной
        /// </summary>
        internal Variable CheckTableVariable( Variable tableVariable )
        {
            Variable variable =  Document.LoadDocument.Dashboard[tableVariable.Name];
            if ( variable != null )
            {
                if (variable is SystemVariable)
                    throw new Exception( "For tabular variables can not be used the names of system variables." );
                if (variable is GlobalVariable)
                    throw new Exception( "For tabular variables can not be used the names of global variables." );
                if (variable is LocalVariable)
                    throw new Exception( "For tabular variables can not be used the names of local variables." );
                if (variable is SequenceVariable)
                    throw new Exception( "For tabular variables can not be used the names of sequence variables." );
                if ( variable is TableVariable && variable.Value.GetType() != tableVariable.Value.GetType() )
                    throw new Exception( "Type does not coincide with an already existent variable." );
                return variable;
            }
            return tableVariable;
        }

        /// <summary>
        /// Проверка на существование переменной
        /// </summary>
        /// <param name="objVariable">Обект перемеменной</param>
        internal Variable CheckVariable( Variable objVariable )
        {
            Variable variable =  Document.LoadDocument.Dashboard[objVariable.Name];
            if ( variable != null )
            {
                //if ( variable is SystemVariable )
                //    throw new Exception( "For tabular variables can not be used the names of system variables." );
                //if ( variable is GlobalVariable )
                //    throw new Exception( "For tabular variables can not be used the names of global variables." );
                //if ( variable is LocalVariable )
                //    throw new Exception( "For tabular variables can not be used the names of local variables." );
                //if ( variable is TableVariable )
                //    throw new Exception( "For tabular variables can not be used the names of table variables." );
                if ( variable.Value.GetType( ) != objVariable.Value.GetType( ) )
                    throw new Exception( "Type does not coincide with an already existent variable." );
                return variable;
            }
            return objVariable;
        }

        /// <summary>
        /// Создаем новое название для таблицы
        /// </summary>
        internal string GeneratetNameTable( string fist, string last )
        {
            int index = 0;
            string name;
            do
            {
                index++;
                name = string.Format( "{0}{1}{2}", fist, index, last );
            }
            while ( _tables.ContainsKey( name ) );
            return name;
        }

        /// <summary>
        /// Проверяем на возможность изменения названия потока
        /// </summary>
        internal bool CheckRenameStream( IStream element, string name )
        {
            foreach ( IStream obj in _streamtList )
                if ( obj != element && obj.Name == name )
                    return false;
            return true;
        }

        /// <summary>
        /// Сканируем таблицы последовательности на наличие переменных
        /// </summary>
        private void ScanTableVariable( )
        {
            _tableVariables.Clear( );
            foreach ( TableSequence table in _tables.Values )
                foreach ( Variable variable in table.Variables )
                    if ( (variable is TableVariable) && _tableVariables.ContainsKey( variable.Name ) == false )
                        _tableVariables.Add( variable.Name, variable );
        }

        /// <summary>
        /// Загрузка данных по потокам последовательности из конфигурации
        /// </summary>
        private void LoadStreamData(string fileName)
        {
            _streamtList.Add( new StreamSequence( new sStreamData( "Name:", sStreamData.NameIconType )) );
            _streamtList.Add( new StreamSequence( new sStreamData( "Delay", sStreamData.DelayIconType ) ) );

            using ( StreamReader streamReader = new StreamReader( fileName, true ) )
            {
                try
                {
                    string str;
                    while ( ( str = streamReader.ReadLine() ) != null )
                    {
                        int indexComment = str.IndexOf( ';' );
                        if ( indexComment >= 0 )
                            str = str.Remove( indexComment );
                        string[] strings = str.Split( new char[] { ' ', '\t' }, 11, StringSplitOptions.RemoveEmptyEntries );
                        if ( strings.Length == 11 )
                            _streamtList.Add( new StreamSequence( new sStreamData( strings ) ) );
                    }
                }
                catch ( Exception )
                {
                    MessageBox.Show( "Error load file config sequence." );
                }
            }

            _streamtList.Add( new StreamSequence( new sStreamData( "Loop", sStreamData.LoopIconType ) ) );
        }

        /// <summary>
        /// Устанавливаем ссылку на объект последовательности
        /// </summary>
        private void SetLinkObject( IObjectStream newObject, IObjectStream oldObject )
        {
            if ( ( (ObjectStream)newObject ).Table != null )
            {
                if ( _tables.ContainsKey( ( (ObjectStream)newObject ).Table.Name ) &&
                    _tables[( (ObjectStream)newObject ).Table.Name].ClassTable != ( (ObjectStream)newObject ).Table.ClassTable )
                    throw new Exception( "Types do not coincide at of the same name tables." );

                RemoveLinkObject( oldObject );
                if ( !_tables.ContainsKey( ( (ObjectStream)newObject ).Table.Name ) )
                    ( (ObjectStream)newObject ).Table = ( (ObjectStream)oldObject ).Table;
                AddLinkToObject( newObject );
            }
            else if ( ( (ObjectStream)newObject ).Variable != null )
            {
                RemoveLinkObject( oldObject );
                AddLinkToObject( newObject );
            }
        }

        /// <summary>
        /// Добавление ссылки на объкт
        /// </summary>
        private void AddLinkToObject( IObjectStream @object )
        {
            if ( ( (ObjectStream)@object ).Table != null )
            {
                TableSequence table = ( (ObjectStream)@object ).Table;
                if ( !_tables.ContainsKey( table.Name ) )
                {
                    _tables.Add( table.Name, table );
                    _tableObjects.Add( table.Name, new List<IObjectStream>( ) );
                }
                ( (ObjectStream)@object ).Table = _tables[table.Name];
                _tableObjects[table.Name].Add( @object );
            }
            else if ( ( (ObjectStream)@object ).Variable != null )
            {
                Variable variable = ( (ObjectStream)@object ).Variable;
                if ( !_sequenceVariables.ContainsKey( variable.Name ) )
                {
                    _sequenceVariables.Add( variable.Name, variable );
                    _variableObjects.Add( variable.Name, new List<IObjectStream>( ) );
                }
                ( (ObjectStream)@object ).Variable = _sequenceVariables[variable.Name];
                _variableObjects[variable.Name].Add( @object );
            }
        }

        /// <summary>
        /// Удаляем ссылку на объект
        /// </summary>
        private void RemoveLinkObject( IObjectStream @object )
        {
            if ( ( (ObjectStream)@object ).Table != null )
            {
                TableSequence table = ( (ObjectStream)@object ).Table;
                _tableObjects[table.Name].Remove( @object );
                if ( _tableObjects[table.Name].Count == 0 )
                {
                    _tableObjects.Remove( table.Name );
                    _tables.Remove( table.Name );
                }
            }
            else if ( ( (ObjectStream)@object ).Variable != null )
            {
                Variable variable = ( (ObjectStream)@object ).Variable;
                _variableObjects[variable.Name].Remove( @object );
                if ( _variableObjects[variable.Name].Count == 0 )
                {
                    _variableObjects.Remove( variable.Name );
                    _sequenceVariables.Remove( variable.Name );
                }
            }
        }
    }
}
