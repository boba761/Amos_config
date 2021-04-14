using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using Calculations.Expressions;

namespace Calculations.Variables
{
    /// <summary>
    /// Класс колекции переменных, объединяет их в группу
    /// </summary>
    public class CollectionVariable : VariableBase
    {
        private List<VariableBase> _variableList;

        /// <summary>
        /// Конструктор класса
        /// </summary>
        public CollectionVariable( ) { }

        /// <summary>
        /// Параметризованный конструктор класса
        /// </summary>
        /// <param name="name">Название колекции переменых</param>
        /// <param name="isReadOnly">Флаг указывающий будет ли переменная редактируемой</param>
        public CollectionVariable( string name, bool isReadOnly = false )
            : base( name, isReadOnly )
        {
            IsReadOnly = isReadOnly;
            _variableList = new List<VariableBase>();
        }

        /// <summary>
        /// Возврашаел или устанавливает массив переменных
        /// </summary>
        public VariableBase[] VariableChilds
        {
            get { return _variableList.OrderBy(obj => obj.Name).ToArray( ); }
            set { _variableList = value.ToList( ); }
        }

        /// <summary>
        /// Возвращает перечисление для списка переменных
        /// </summary>
        [XmlIgnore]
        public IEnumerable<VariableBase> EnumerableVariables
        {
            get { return _variableList; }
        }

        /// <summary>
        /// Возвращает объект выражения заначения пременной
        /// </summary>
        [XmlIgnore]
        public override ValueExpression ValueExpression { get { return null; } }

        /// <summary>
        /// Добавляет переменную в колекцию
        /// </summary>
        /// <param name="variable">Обект переменной</param>
        public void Add( VariableBase variable ) 
        {
            if ( _variableList.Contains( variable ) == false )
                _variableList.Add( variable );
        }

        /// <summary>
        /// Удаляет переменную из колекции
        /// </summary>
        /// <param name="variable">Объект переменной</param>
        public void Remove( VariableBase variable ) 
        {
            _variableList.Remove( variable );
        }

        /// <summary>
        /// Очищает список переменных колеккции
        /// </summary>
        public void Clear()
        {
            foreach ( VariableBase variable in _variableList )
                if ( variable is CollectionVariable)
                    ( variable as CollectionVariable).Clear();
            _variableList.Clear();
        }

        /// <summary>
        /// Осуществляет рекурсивный поиск переменной в колекции
        /// </summary>
        /// <param name="name">Название переменной</param>
        /// <returns>Возврашает объект переменной, в случае неудачи null.</returns>
        public Variable FindRecursionVariable( string name )
        {
            foreach ( VariableBase variable in _variableList )
            {
                Variable objectVariable = variable as Variable;
                if ( objectVariable != null )
                {
                    if ( objectVariable.Name == name )
                        return objectVariable;
                }
                else
                {
                    CollectionVariable collectionVariable = variable as CollectionVariable;
                    if ( ( objectVariable = collectionVariable.FindRecursionVariable( name ) ) != null )
                        return objectVariable;
                }
            }
            return null;
        }

        /// <summary>
        /// Осуществляет поиск колекции переменных в колекции
        /// </summary>
        /// <param name="name">Название переменной</param>
        /// <returns>Объект колекции переменных, в случае неудачи null/</returns>
        public CollectionVariable FindCollectionVariable( string name )
        {
            foreach ( VariableBase variable in _variableList )
            {
                CollectionVariable collection = variable as CollectionVariable;
                if ( collection != null && ( collection.Name == name || ( collection = collection.FindCollectionVariable( name ) ) != null ) )
                    return collection;
            }
            return null;
        }

        /// <summary>
        /// Клонирование объекта значения
        /// </summary>
        /// <returns>Клон объекта</returns>
        public override object Clone( )
        {
            CollectionVariable clone = new CollectionVariable( Name, IsReadOnly );
            foreach ( VariableBase variable in _variableList )
            {
                if ( variable is CollectionVariable )
                    clone.Add( (VariableBase)variable.Clone( ) );
                else
                    clone.Add( variable );
            }
            return clone;
        }
    }
}
