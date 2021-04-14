using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using System;
using Tools.Interfaces;

namespace Data.DataObject.Sequence
{
    /// <summary>
    /// Класс коллекции объектов для потока последовательности
    /// </summary>
    [XmlType( "Collection" )]
    public class ObjectCollection : IObjectCollection
    {
        private List<ObjectStream> _objects;

        public event OnSetObject onSetObject;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public ObjectCollection( ) 
        {
            _objects = new List<ObjectStream>( );
        }

        /// <summary>
        /// Параметризованный конструктор
        /// </summary>
        public ObjectCollection( IStream stream, eTypeObjectCollection typeCollection )
            : this()
        {
            Stream = stream;
            TypeCollection = typeCollection;
        }

        /// <summary>
        /// Возвращает тип колекции
        /// </summary>
        [XmlAttribute("Type")]
        public eTypeObjectCollection TypeCollection { get; set; }

        /// <summary>
        /// Возвращает массив объектов последовательности
        /// </summary>
        public ObjectStream[] Objects 
        {
            get { return _objects.Where( obj => obj.Type != eTypeObjectStream.Default ).ToArray( ); }
            set 
            {
                foreach ( ObjectStream @object in value )
                {
                    for ( int i = _objects.Count; i < @object.IndexEvent; i++ )
                        _objects.Add( null );
                    _objects.Add(@object);
                }
            }
        }

        /// <summary>
        /// Возвращает объект потока последовательности
        /// </summary>
        [XmlIgnore]
        public IStream Stream { get; set; }

        /// <summary>
        /// Возвращает или устанавливает состояние видимости потока
        /// </summary>
        [XmlIgnore]
        public bool IsVisible 
        {
            get
            {
                if ( TypeCollection == eTypeObjectCollection._1D )
                    return true;
                foreach ( ObjectStream obj in _objects )
                    if ( obj.Type != eTypeObjectStream.Default )
                        return true;
                return false;
            }
        }

        /// <summary>
        /// Возвращает количество событий
        /// </summary>
        [XmlIgnore]
        public int EventCount { get { return _objects.Count; } }
        
        public IObjectStream this[int index] 
        { 
            get 
            {
                if ( index >= _objects.Count )
                    return null;
                return _objects[index]; 
            }
            set
            {
                _objects[index] = (ObjectStream)value;
                if ( onSetObject != null )
                    onSetObject( index, value );
            }
        }

        /// <summary>
        /// Добавляет объект в коллекцию
        /// </summary>
        /// <param name="@object">Вставляемый объект</param>
        public void Add( ObjectStream @object )
        {
            _objects.Add( @object );
        }

        /// <summary>
        /// Вставляем объект в коллекцию
        /// </summary>
        /// <param name="indexEvent">Индекс события для вставки</param>
        /// <param name="@object">Вставляемый объект</param>
        public void InsertEvent( int indexEvent, IObjectStream @object )
        {
            _objects.Insert( indexEvent, (ObjectStream)@object );
            for ( int i = indexEvent + 1; i < _objects.Count; i++ )
                _objects[i].IndexEvent++;
        }

        /// <summary>
        /// Удаление объекта из колекции
        /// </summary>
        /// <param name="indexEvent">Индекс события</param>
        public void Remove( int indexEvent )
        {
            _objects.RemoveAt( indexEvent );
            for ( int i = indexEvent; i < _objects.Count; i++ )
                _objects[i].IndexEvent--;
        }

        /// <summary>
        /// Загрузка пустых событий в колекцию
        /// </summary>
        /// <param name="events">Количество событий</param>
        public void SetLoadEvents( int events )
        {
            for ( int i = 0; i < events; i++ )
            {
                if ( _objects.Count <= i)
                    _objects.Add( (ObjectStream)Stream.GetNewObject( TypeCollection, i ) );
                else if ( _objects[i] == null )
                    _objects[i] = (ObjectStream)Stream.GetNewObject( TypeCollection, i );
                else
                {
                    _objects[i].Parent = Stream;
                    _objects[i].TypeCollection = TypeCollection;
                }
            }  
        }
    }
}
