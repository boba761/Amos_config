using System.Xml.Serialization;
using System;
using Calculations.Expressions;

namespace Calculations.Variables
{
    [XmlInclude( typeof( CollectionVariable ) )] 
    [XmlInclude( typeof( SystemVariable ) )]
    [XmlInclude( typeof( SequenceVariable ) )] 
    [XmlInclude( typeof( LocalVariable ) )]  
    [XmlInclude( typeof( GlobalVariable ) )]
    [XmlInclude( typeof( TableVariable ) )]
    public abstract class VariableBase : VariableExpression
    {
        /// <summary>
        /// Параметризованный конструктор класса
        /// </summary>
        /// <param name="name">Название переменной</param>
        /// <param name="isReadOnly">Флаг указывающий будет ли переменная редактируемой</param>
        public VariableBase( string name = null, bool isReadOnly = false )
        {
            Name = name;
            IsReadOnly = isReadOnly;
        }

        /// <summary>
        /// Возврашает статус на изменение переменной
        /// </summary>
        [XmlAttribute]
        public bool IsReadOnly { get; set; }
        /// <summary>
        /// Возвращает пояснение к переменной
        /// </summary>
        [XmlAttribute]
        public string Description { get; set; }

        /// <summary>
        /// Клонирование объекта значения
        /// </summary>
        /// <returns>Клон объекта</returns>
        public abstract object Clone( );
    }
}
