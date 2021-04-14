using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;
using Amos.Controls.Sequence;
using Amos.Data.Sequence.Objects;
using Amos.Data.Sequence.Tables;
using Amos.Interfaces;
using Amos.TypeFiles.FileBuilders.FileTNT;
using Localize;
using Tools.Interfaces;

namespace Amos.Data.Sequence
{
    [XmlType("Element")]
    public class StreamSequence : IStream
    {
        public static Image editCellImage;
        public static Image clearCellImage;
        public static Image table1D2DImage;
        public static Image table1DImage;
        public static Image table2DImage;
        public static Image table3DImage;
        public static Image table4DImage;
        public static Image loopStartImage;
        public static Image loopEndImage;
        public static Image acquisitionImage;
        public static Image continueImage;
        public static Image signalImage;
        public static Image tableShapeImage;
        public static Image plusXImage;
        public static Image plusYImage;
        public static Image minusXImage;
        public static Image minusYImage;
        public static Image propertyImage;
        public static Image asynchronousImage;
        public static Image asynchronousStartImage;
        public static Image asynchronousStopImage;
      
        private sStreamData _elementData;
        private Dictionary<eTypeObjectCollection, ObjectCollection> _collectionHash;

        /// <summary>
        /// Статический конструктор класса
        /// </summary>
        static StreamSequence( )
        {
            editCellImage = Local.GetImage( "EditCell.Sequence.MenuItem" );
            clearCellImage = Local.GetImage( "ClearCell.Sequence.MenuItem" );
            table1D2DImage = Local.GetImage( "Table1D2D.Sequence.MenuItem" );
            table1DImage = Local.GetImage( "Table1D.Sequence.MenuItem" );
            table2DImage = Local.GetImage( "Table2D.Sequence.MenuItem" );
            table3DImage = Local.GetImage( "Table3D.Sequence.MenuItem" );
            table4DImage = Local.GetImage( "Table4D.Sequence.MenuItem" );
            loopStartImage = Local.GetImage( "LoopStart.Sequence.MenuItem" );
            loopEndImage = Local.GetImage( "LoopEnd.Sequence.MenuItem" );
            acquisitionImage = Local.GetImage("Acquisition.Sequence.MenuItem");
            continueImage = Local.GetImage( "Continue.Sequence.MenuItem" );
            signalImage = Local.GetImage( "Signal.Sequence.MenuItem" );
            tableShapeImage = Local.GetImage( "TableShape.Sequence.MenuItem" );
            plusXImage = Local.GetImage( "PlusX.Sequence.MenuItem" );
            plusYImage = Local.GetImage( "PlusY.Sequence.MenuItem" );
            minusXImage = Local.GetImage( "MinusX.Sequence.MenuItem" );
            minusYImage = Local.GetImage( "MinusY.Sequence.MenuItem" );
            propertyImage = Local.GetImage( "Property.Sequence.MenuItem" );
            asynchronousImage = Local.GetImage( "Asynchronous.Sequence.MenuItem" );
            asynchronousStartImage = Local.GetImage( "AsynchronousStart.Sequence.MenuItem" );
            asynchronousStopImage = Local.GetImage( "AsynchronousStop.Sequence.MenuItem" );
        }

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public StreamSequence( )
        {
            _collectionHash = new Dictionary<eTypeObjectCollection, ObjectCollection>( );
        }

        /// <summary>
        /// Параметризованный конструктор
        /// </summary>
        /// <param name="elementData">Объект данных потока из конфигурационного файла</param>
        public StreamSequence( sStreamData elementData )
            : this()
        {
            _elementData = elementData;
            IsMove = IconType != sStreamData.NameIconType && IconType != sStreamData.DelayIconType;
            IsEdit = IconType != sStreamData.NameIconType;

            if ( IconType == sStreamData.NameIconType || IconType == sStreamData.ETIconType || IconType == sStreamData.SHIconType )
                AddCollection( eTypeObjectCollection._1D );
            else
                foreach ( eTypeObjectCollection typeObject in Enum.GetValues( typeof( eTypeObjectCollection ) ) )
                    AddCollection( typeObject );
        }

        /// <summary>
        /// Параметризованный конструктор
        /// </summary>
        /// <param name="sequenceRow">Импортируемые данные</param>
        public StreamSequence( SequenceRow sequenceRow )
            : this( new sStreamData( sequenceRow ) )
        {
            for ( int i = 0; i < sequenceRow.sequenceEvent.Length; i++ )
            {
                SequenceEvent obj = sequenceRow.sequenceEvent[i];
                if ( i == 0 )
                {
                    Name = obj.dataString;
                    continue;
                }

                ObjectSequence @object = (ObjectSequence)GetNewObject( eTypeObjectCollection._1D, i - 1 );
                if ( obj.isTable0D )
                {
                    if ( i > 1 && (this[eTypeObjectCollection._1D][i - 2].Type == eTypeObjectStream.TableShape 
                        || this[eTypeObjectCollection._1D][i - 2].Type == eTypeObjectStream.Continue ) )
                        @object.Type = eTypeObjectStream.Continue;
                    else
                    {
                        @object.Type = eTypeObjectStream.TableShape;
                        @object.TableName = obj.tableName0D;
                    }
                }
                else if ( obj.isTable1D )
                {
                    @object.Type = eTypeObjectStream.Table;
                    @object.TableName = obj.tableName1D;
                }
                else if ( obj.isTable5D )
                {
                    @object.Type = eTypeObjectStream.Table1D2D;
                    @object.TableName = obj.tableName5D;

                }
                else if ( obj.isAqustion )
                {
                    @object.Type = eTypeObjectStream.Acquisition;
                    @object.Acquisition = new AcquisitionObject( obj.sequenceAcquation, i );
                }
                else if ( obj.fileAsynchronus != null && obj.dataString == "@AS" )
                {
                    @object.Type = eTypeObjectStream.AsynchronousStart;
                    @object.Asynchronus = new AsynchronusObject(obj.fileAsynchronus);
                }
                else if ( sequenceRow.defalut != obj.dataString )
                    @object.LoadText( obj.dataString );

                ( (ObjectCollection)this[eTypeObjectCollection._1D] ).Add( @object );

                if ( _collectionHash.Count == 1 )
                    continue;

                @object = (ObjectSequence)GetNewObject( eTypeObjectCollection._2D, i - 1 );
                if ( obj.isTable2D )
                {
                    @object.Type = eTypeObjectStream.Table;
                    @object.TableName = obj.tableName2D;
                }
                ( (ObjectCollection)this[eTypeObjectCollection._2D] ).Add( @object );

                @object = (ObjectSequence)GetNewObject( eTypeObjectCollection._3D, i - 1 );
                if ( obj.isTable3D )
                {
                    @object.Type = eTypeObjectStream.Table;
                    @object.TableName = obj.tableName3D;
                }
                ( (ObjectCollection)this[eTypeObjectCollection._3D] ).Add( @object );

                @object = (ObjectSequence)GetNewObject( eTypeObjectCollection._4D, i - 1 );
                if ( obj.isTable4D )
                {
                    @object.Type = eTypeObjectStream.Table;
                    @object.TableName = obj.tableName4D;
                }
                ( (ObjectCollection)this[eTypeObjectCollection._4D] ).Add( @object );
            }
        }

        /// <summary>
        /// Возвращает или устанавливает название потока
        /// </summary>
        [XmlAttribute]
        public string Name 
        {
            get { return _elementData.Name; }
            set { _elementData.Name = value; }
        }

        /// <summary>
        /// Возвращает статус перемещения потока на последовательности
        /// </summary>
        [XmlAttribute]
        public bool IsMove { get; set; }

        /// <summary>
        /// Возвращает статус редактирования названия потока
        /// </summary>
        [XmlAttribute]
        public bool IsEdit { get; set; }

        /// <summary>
        /// Возвращает или устанавливает данные  конфигурационного файла
        /// </summary>
        [XmlElement( "Data" )]
        public sStreamData StreamData 
        { 
            get { return _elementData; } 
            set { _elementData = value; } 
        }

        /// <summary>
        /// Возвращает массив колекций потока
        /// </summary>
        [XmlElement( "CollectionItems" )]
        public ObjectCollection[] ObjectCollection 
        { 
            get { return _collectionHash.Values.ToArray(); }
            set 
            {
                foreach ( ObjectCollection collection in value )
                {
                    collection.Stream = this;
                    _collectionHash.Add( collection.TypeCollection, collection );
                }
            }
        }

        /// <summary>
        /// Возвращает строку с именем иконки потока
        /// </summary>
        [XmlIgnore]
        public string IconType { get { return _elementData.IconType; } }

        /// <summary>
        /// Возвращает номер группы для потока
        /// </summary>
        [XmlIgnore]
        public int Group { get { return _elementData.Group; } }

        /// <summary>
        /// Возвращает статус видимостия потока на последовательности
        /// </summary>
        [XmlIgnore]
        public bool IsVisible { get { return _elementData.Visible; } }

        /// <summary>
        /// Возвращает количество событий для потока
        /// </summary>
        [XmlIgnore]
        public int EventCount { get { return _collectionHash[eTypeObjectCollection._1D].EventCount; } }
        
        public IObjectCollection this[eTypeObjectCollection typeCollection]
        {
            get
            {
                ObjectCollection collection = null;
                _collectionHash.TryGetValue(typeCollection, out collection);
                return collection;
            }
        }

        /// <summary>
        /// Возвращает тип потока
        /// </summary>
        private Type TypeObject
        {
            get
            {
                switch ( _elementData.IconType )
                {
                case sStreamData.NameIconType:
                    return typeof( Name_ObjectSequence );
                case sStreamData.DelayIconType:
                    return typeof( Delay_ObjectSequence );
                case sStreamData.LoopIconType:
                    return typeof( Loop_ObjectSequence );
                case sStreamData.ATIconType:
                case sStreamData.A2IconType:
                    return typeof( AT_ObjectSequence );
                case sStreamData.ACQIconType:
                    return typeof( ACQ_ObjectSequence );
                case sStreamData.ETIconType:
                    return typeof( ET_ObjectSequence );
                case sStreamData.GAIconType:
                    return typeof( GA_ObjectSequence );
                case sStreamData.GRIconType:
                case sStreamData.G2IconType:
                    return typeof( GR_ObjectSequence );
                case sStreamData.LDIconType:
                    return typeof( LD_ObjectSequence );
                case sStreamData.O1IconType:
                    return typeof( O1_ObjectSequence );
                case sStreamData.TXIconType:
                    return typeof( TX_ObjectSequence );
                case sStreamData.RMIconType:
                case sStreamData.R2IconType:
                    return typeof( RM_ObjectSequence );
                case sStreamData.PHIconType:
                case sStreamData.P2IconType:
                    return typeof( PH_ObjectSequence );
                case sStreamData.PSIconType:
                    return typeof( PS_ObjectSequence );
                case sStreamData.SHIconType:
                    return typeof( SH_ObjectSequence );
                default:
                    return null;
                }
            }
        }

        /// <summary>
        /// Возвращает новый объект потока
        /// </summary>
        /// <param name="typeCollection">Тип коллекции объектов</param>
        /// <param name="indexEvent">Индекс события</param>
        public IObjectStream GetNewObject( eTypeObjectCollection typeCollection, int indexEvent )
        {
            return (IObjectStream)Activator.CreateInstance( TypeObject, new object[] { this, indexEvent, typeCollection, eTypeObjectStream.Default, null } );
        }

        /// <summary>
        /// Возвращает объект для потока
        /// </summary>
        /// <param name="typeCollection">Тип коллекции объектов</param>
        /// <param name="indexEvent">Индекс события</param>
        /// <param name="text">Текст объекта</param>
        public IObjectStream GetObject( eTypeObjectCollection typeCollection, int indexEvent, string text)
        {
            return (IObjectStream)Activator.CreateInstance( TypeObject, new object[] { this, indexEvent, typeCollection, text } );
        }

        /// <summary>
        /// Возвращает объект для потока
        /// </summary>
        /// <param name="typeCollection">Тип коллекции объектов</param>
        /// <param name="indexEvent">Индекс события</param>
        /// <param name="type">Тип объекта</param>
        /// <param name="data">Дополнительные данные объекта</param>
        public IObjectStream GetObject( eTypeObjectCollection typeCollection, int indexEvent, eTypeObjectStream type, object data = null )
        {
            return (IObjectStream)Activator.CreateInstance( TypeObject, new object[] { this, indexEvent, typeCollection, type, data } );
        }

        /// <summary>
        /// Возвращает объект контекстного меню для объекта потока в активную панель
        /// </summary>
        /// <param name="panel">Активная панель</param>
        /// <param name="typeCollection">Тип коллекции объектов</param>
        /// <param name="indexEvent">Индекс события</param>
        public ContextMenuStrip GetContextMenu( ISequencePanel panel, eTypeObjectCollection typeCollection, int indexEvent )
        {
            ContextMenuStrip contextMenu = new ContextMenuStrip();
            contextMenu.ImageScalingSize = new Size( 22, 22 );
            IObjectStream @object = this[typeCollection][indexEvent];

            if ( @object.IsEdit )
                contextMenu.Items.Add( "Edit...", editCellImage, panel.OnEdit );
            if ( @object.IsClear )
                contextMenu.Items.Add( "Clear", clearCellImage, panel.OnClear );
            if ( @object.IsSignal )
                contextMenu.Items.Add( "Signal", signalImage, panel.OnSignal );
            if ( @object.IsXY )
            {
                contextMenu.Items.Add( "+X", plusXImage, panel.OnPlusX );
                contextMenu.Items.Add( "+Y", plusYImage, panel.OnPlusY );
                contextMenu.Items.Add( "-X", minusXImage, panel.OnMinusX );
                contextMenu.Items.Add( "-Y", minusYImage, panel.OnMinusY );
            }
            if ( @object.IsAsynchronousStart )
                contextMenu.Items.Add( "Asynchronous Start", asynchronousStartImage, panel.OnAsynchronousStart );
            if ( @object.IsAsynchronousStop )
                contextMenu.Items.Add( "Asynchronous Stop", asynchronousStopImage, panel.OnAsynchronousStop );
            if ( @object.IsLoopStart )
                contextMenu.Items.Add( "Loop Start", loopStartImage, panel.OnLoopStart );
            if ( @object.IsLoopEnd )
                contextMenu.Items.Add( "Loop End", loopEndImage, panel.OnLoopEnd );
            if ( @object.IsAcquisition )
                contextMenu.Items.Add( "Acquisition", acquisitionImage, panel.OnAcquisition );
            if ( @object.IsContinue )
                contextMenu.Items.Add( "Continue", continueImage, panel.OnContinue );
            if ( @object.IsTable1D2D )
                 contextMenu.Items.Add( "1/2D Table", table1D2DImage, panel.OnTable1D2D );
            if ( @object.IsTable1D )
                contextMenu.Items.Add( "1D Table", table1DImage, panel.OnTable1D );
            if ( @object.IsTable2D )
                contextMenu.Items.Add( "2D Table", table1DImage, panel.OnTable2D );
            if ( @object.IsTable3D )
                contextMenu.Items.Add( "3D Table", table1DImage, panel.OnTable3D );
            if ( @object.IsTable4D )
                contextMenu.Items.Add( "4D Table", table1DImage, panel.OnTable4D );
            if ( @object.IsTableShape )
                contextMenu.Items.Add( "Shape Table", tableShapeImage, panel.OnTableShape );

            if ( @object.IsProterty )
            {
                contextMenu.Items.Add( "-" );
                contextMenu.Items.Add( "Properties", propertyImage, panel.OnProperty );
            }
            return contextMenu;
        }

        /// <summary>
        /// Вставляет событие в поток
        /// </summary>
        /// <param name="indexEvent">Индекс события</param>
        public void InsertEvent( int indexEvent )
        {
            foreach ( ObjectCollection collection in _collectionHash.Values )
                collection.InsertEvent( indexEvent, GetNewObject( collection.TypeCollection, indexEvent ) );
        }

        /// <summary>
        /// Удаляет событие из потока
        /// </summary>
        /// <param name="indexEvent">Индекс события</param>
        public void RemoveEvent( int indexEvent )
        {
            foreach ( ObjectCollection collection in _collectionHash.Values )
                collection.Remove( indexEvent );
        }

        /// <summary>
        /// Устанавливает количество событий для загрузки объектов
        /// </summary>
        public void SetLoadEvents( int events )
        {
            foreach ( ObjectCollection collection in _collectionHash.Values )
                collection.SetLoadEvents( events );
        }

        /// <summary>
        /// Добавляет колекции объектов последовательности в поток
        /// </summary>
        /// <param name="type">Тип колекции объектов</param>
        private void AddCollection(eTypeObjectCollection type)
        {
            _collectionHash.Add( type, new ObjectCollection( this, type ) );
        }
    }
}
