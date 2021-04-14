using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Amos.Commands;
using Amos.Data.Sequence.Objects;
using Amos.Data.Sequence.Tables;
using Calculations.Values;
using Calculations.Variables;
using Tools.Interfaces;

namespace Amos.Data.Sequence
{
    /// <summary>
    /// Базовый клас для объектов последовательности
    /// </summary>
    [Serializable]
    [XmlInclude( typeof( ACQ_ObjectSequence ) )]
    [XmlInclude( typeof( AT_ObjectSequence ) )]
    [XmlInclude( typeof( Delay_ObjectSequence ) )]
    [XmlInclude( typeof( ET_ObjectSequence ) )]
    [XmlInclude( typeof( GA_ObjectSequence ) )]
    [XmlInclude( typeof( GR_ObjectSequence ) )]
    [XmlInclude( typeof( LD_ObjectSequence ) )]
    [XmlInclude( typeof( Loop_ObjectSequence ) )]
    [XmlInclude( typeof( Name_ObjectSequence ) )]
    [XmlInclude( typeof( O1_ObjectSequence ) )]
    [XmlInclude( typeof( PH_ObjectSequence ) )]
    [XmlInclude( typeof( PS_ObjectSequence ) )]
    [XmlInclude( typeof( RM_ObjectSequence ) )]
    [XmlInclude( typeof( SH_ObjectSequence ) )]
    [XmlInclude( typeof( TX_ObjectSequence ) )]
    [XmlType( "Object" )]
    public abstract class ObjectSequence : IObjectStream, IObjectSequence
    {
        private eTypeObjectStream _type;
        private IObjectSequence _object;

        /// <summary>
        /// Базовый конструктор класса
        /// </summary>
        public ObjectSequence( ) 
        {
            Type = eTypeObjectStream.Default;
        }

        /// <summary>
        /// Перегруженый конструктор класса
        /// </summary>
        /// <param name="element">Поток последовательности</param>
        /// <param name="indexEvent">Индекс колонки</param>
        /// <param name="typeCollection">Тип колекции</param>
        public ObjectSequence( IStream element, int indexEvent, eTypeObjectCollection typeCollection )
        {
            Parent = element;
            IndexEvent = indexEvent;
            TypeCollection = typeCollection;
        }

        /// <summary>
        /// Возврашает индекс колонки объекта
        /// </summary>
        [XmlAttribute]
        public int IndexEvent { get; set; }

        /// <summary>
        /// Устанавливает или возвращает тип объекта
        /// </summary>
        [XmlAttribute]
        public eTypeObjectStream Type 
        {
            get { return _type; }
            set 
            {
                _type = value;
                if ( _type == eTypeObjectStream.Default )
                    _object = Default;
            } 
        }

        /// <summary>
        /// Устанавливает или возвращает объект значения
        /// </summary>
        [XmlElement]
        public virtual Value Value 
        {
            get 
            { return _object as Value; }
            set 
            {
                Type = eTypeObjectStream.Value;
                _object = value; 
            }
        }

        /// <summary>
        /// Устанавливает или возвращает назание переменной
        /// </summary>
        [XmlAttribute( "Variable" )]
        public string VariableName 
        {
            get 
            {
                if ( _object is Variable == false )
                    return null;
                return ( _object as Variable ).Name;
            }
            set { _object = Program.Document.Sequence.GetSequenceVariable( this, value ); } 
        }

        /// <summary>
        /// Устанавливает или возвращает объект переменной
        /// </summary>
        [XmlIgnore]
        public virtual Variable Variable 
        {
            get { return _object as Variable; }
            set 
            {
                _object = Program.Document.Sequence.CheckVariable( value );
                Type = eTypeObjectStream.Variable;
            } 
        }

        /// <summary>
        /// Устанавливает или возвращает название Table
        /// </summary>
        [XmlAttribute( "Table" )]
        public string TableName
        {
            get
            {
                if ( _object is TableSequence == false )
                    return null;
                return ( _object as TableSequence ).Name;
            }
            set 
            {
                _object = Program.Document.Sequence.GetTable( this, value ); 
            }
        }

        /// <summary>
        /// Устанавливает или возвращает объетк Table
        /// </summary>
        [XmlIgnore]
        public virtual TableSequence Table 
        {
            get { return _object as TableSequence; }
            set
            {
                _object = value;
                if ( Type != eTypeObjectStream.Table1D2D && Type != eTypeObjectStream.TableShape )
                    Type = eTypeObjectStream.Table;
            }
        }

        /// <summary>
        /// Устанавливает или возвращает объетк Acquisition
        /// </summary>
        [XmlElement]
        public virtual AcquisitionObject Acquisition
        {
            get { return _object as AcquisitionObject; }
            set { _object = value; }
        }

        /// <summary>
        /// Устанавливает или возвращает объетк Asynchronus
        /// </summary>
        [XmlElement]
        public AsynchronusObject Asynchronus
        {
            get 
            {
                if ( Type != eTypeObjectStream.AsynchronousStart )
                    return null;
                return _object as AsynchronusObject; 
            }
            set 
            { _object = value; }
        }

        /// <summary>
        /// Возврашает может ли объект быть редактирован
        /// </summary>
        [XmlIgnore]
        public virtual bool IsEdit { get { return true; } }

        /// <summary>
        /// Возврашает может ли объект быть  очищенным
        /// </summary>
        [XmlIgnore]
        public virtual bool IsClear { get { return Type != eTypeObjectStream.Default; } }

        /// <summary>
        /// Возврашает может ли объект быть типа Signal
        /// </summary>
        [XmlIgnore]
        public virtual bool IsSignal { get { return Type != eTypeObjectStream.Value && Type != eTypeObjectStream.Variable; } }

        /// <summary>
        /// Возврашает может ли объект быть типа XY
        /// </summary>
        [XmlIgnore]
        public virtual bool IsXY { get { return false; } }

        /// <summary>
        /// Возврашает может ли объект быть типа  AsynchronousStart
        /// </summary>
        [XmlIgnore]
        public virtual bool IsAsynchronousStart { get { return false; } }

        /// <summary>
        /// Возврашает может ли объект быть типа  AsynchronousStop
        /// </summary>
        [XmlIgnore]
        public virtual bool IsAsynchronousStop { get { return false; } }

        /// <summary>
        /// Возврашает может ли объект быть типа LoopStart
        /// </summary>
        [XmlIgnore]
        public virtual bool IsLoopStart { get { return false; } }

        /// <summary>
        /// Возврашает может ли объект быть типа LoopEnd
        /// </summary>
        [XmlIgnore]
        public virtual bool IsLoopEnd { get { return false; } }

        /// <summary>
        /// Возврашает может ли объект быть типа  Acquisition
        /// </summary>
        [XmlIgnore]
        public virtual bool IsAcquisition { get { return false; } }

        /// <summary>
        /// Возврашает может ли объект быть типа  Continue
        /// </summary>
        [XmlIgnore]
        public virtual bool IsContinue
        {
            get
            {
                return IndexEvent > 0 && Old.Type != eTypeObjectStream.Continue && ( Prev.Type == eTypeObjectStream.TableShape || Prev.Type == eTypeObjectStream.Continue );
            }
        }

        /// <summary>
        /// Возврашает может ли объект быть типа Table1D2D
        /// </summary>
        [XmlIgnore]
        public virtual bool IsTable1D2D { get { return Parent[eTypeObjectCollection._1D][IndexEvent].Type != eTypeObjectStream.Table1D2D; } }

        /// <summary>
        /// Возврашает может ли объект быть типа Table1D
        /// </summary>
        [XmlIgnore]
        public virtual bool IsTable1D { get { return Parent[eTypeObjectCollection._1D][IndexEvent].Type != eTypeObjectStream.Table; } }

        /// <summary>
        /// Возврашает может ли объект быть типа Table2D
        /// </summary>
        [XmlIgnore]
        public virtual bool IsTable2D { get { return Parent[eTypeObjectCollection._2D][IndexEvent].Type != eTypeObjectStream.Table; } }

        /// <summary>
        /// Возврашает может ли объект быть типа Table3D
        /// </summary>
        [XmlIgnore]
        public virtual bool IsTable3D { get { return Parent[eTypeObjectCollection._3D][IndexEvent].Type != eTypeObjectStream.Table; } }

        /// <summary>
        /// Возврашает может ли объект быть типа Table4D
        /// </summary>
        [XmlIgnore]
        public virtual bool IsTable4D { get { return Parent[eTypeObjectCollection._4D][IndexEvent].Type != eTypeObjectStream.Table; } }

        /// <summary>
        /// Возврашает может ли объект быть типа TableShape
        /// </summary>
        [XmlIgnore]
        public virtual bool IsTableShape 
        { 
            get 
            { 
                return (IndexEvent == 0 && Parent[eTypeObjectCollection._1D][IndexEvent].Type != eTypeObjectStream.TableShape) ||
                    ( IndexEvent > 0  && Parent[eTypeObjectCollection._1D][IndexEvent].Type != eTypeObjectStream.TableShape && 
                    Parent[eTypeObjectCollection._1D][IndexEvent - 1].Type != eTypeObjectStream.TableShape &&
                    Parent[eTypeObjectCollection._1D][IndexEvent - 1].Type != eTypeObjectStream.Continue ); 
            } 
        }

        /// <summary>
        /// Возврашает можно ли для объекта вызывать окно свойств
        /// </summary>
        [XmlIgnore]
        public virtual bool IsProterty 
        { 
            get 
            { 
                return Type == eTypeObjectStream.Table || Type == eTypeObjectStream.Table1D2D || Type == eTypeObjectStream.TableShape || Type == eTypeObjectStream.Continue; 
            } 
        }

        /// <summary>
        /// Возвращает текст объекта
        /// </summary>
        [XmlIgnore]
        public string Text 
        {
            get
            {
                if ( TypeCollection == eTypeObjectCollection._1D )
                {
                    switch ( Type )
                    {
                    case eTypeObjectStream.Default:
                    case eTypeObjectStream.Value:
                        return Value.ToString();
                    case eTypeObjectStream.Variable:
                        return "=" + Variable.Name;
                    case eTypeObjectStream.Table:
                    case eTypeObjectStream.Table1D2D:
                    case eTypeObjectStream.TableShape:
                        return Table.Name;
                    }
                }
                else if ( Type != eTypeObjectStream.Default )
                    return Table.Name;
                return "";
            } 
        }

        /// <summary>
        /// Возвращает объект значения по умолчанию
        /// </summary>
        [XmlIgnore]
        protected abstract Value Default { get; }

        /// <summary>
        /// Возвращает текст для отрисовки на объекте
        /// </summary>
        [XmlIgnore]
        public virtual string DrawText 
        {
            get
            {
                if ( Type == eTypeObjectStream.Value )
                    return Value.ToString();
                if ( Type == eTypeObjectStream.Variable )
                    return Variable.Name;
                return null;
            } 
        }

        /// <summary>
        /// Возврашает обект картинки для объекта
        /// </summary>
        [XmlIgnore]
        public virtual Image Image { get { return null; } }

        /// <summary>
        /// Возврашает поток владеющий объектом
        /// </summary>
        [XmlIgnore]
        public IStream Parent { get; set; }

        /// <summary>
        /// Возвращает пркведушее состояние объекта
        /// </summary>
        [XmlIgnore]
        public IObjectStream Old { get { return Parent[TypeCollection][IndexEvent]; } }

        /// <summary>
        /// Возврашает преведущий объект
        /// </summary>
        [XmlIgnore]
        public IObjectStream Prev { get { return IndexEvent == 0 ? null : Parent[TypeCollection][IndexEvent - 1]; } }

        /// <summary>
        /// Возврашает следующий объект
        /// </summary>
        [XmlIgnore]
        public IObjectStream Next { get { return IndexEvent >= Parent.EventCount-2 ? null : Parent[TypeCollection][IndexEvent + 1]; } }

        /// <summary>
        /// Возвращает тип колекции к которой пренадлежит объект
        /// </summary>
        [XmlIgnore]
        public eTypeObjectCollection TypeCollection { get; set; }

        /// <summary>
        /// Возврашает может ли объект быть  изменен
        /// </summary>
        [XmlIgnore]
        public bool IsChange { get; set; }

        /// <summary>
        /// Возвращает текст всплывающейй подсказки
        /// </summary>
        [XmlIgnore]
        public virtual string ToolTip 
        { 
            get 
            {
                if ( Type == eTypeObjectStream.Continue )
                    return Prev.ToolTip;
                return _object.ToolTip; 
            } 
        }

        /// <summary>
        /// Клонирует объект
        /// </summary>
        /// <returns>Копия объекта</returns>
        public abstract object Clone( );

        /// <summary>
        /// Корректирует объекты схемы в зависимости от текущего объекта команды
        /// </summary>
        /// <param name="command">Объект комманды</param>
        public virtual void CorrectionSequence( ICommandList command ) 
        {
            Old.IsChange = true;
            if ( Next != null && Next.IsChange == false )
            {
                if ( Old.Type == eTypeObjectStream.Continue && Next.Type == eTypeObjectStream.Continue && Type != eTypeObjectStream.TableShape )
                {
                    Next.IsChange = true;
                    command.Add( new ClearObjectCommand( ( (Command)command ).Document, Parent, TypeCollection, Next.IndexEvent ) );
                }
                else if ( Old.Type == eTypeObjectStream.TableShape && Next.Type == eTypeObjectStream.Continue )
                {
                    Next.IsChange = true;
                    command.Add( new SetObjectCommand( ( (Command)command ).Document, Parent, TypeCollection, Next.IndexEvent, eTypeObjectStream.TableShape,
                        ( (ObjectSequence)Parent[TypeCollection][IndexEvent]).Table ) );
                }
            }
        }

        /// <summary>
        /// Загружает текст из файла  в объект
        /// </summary>
        /// <param name="text">Загружаемый текст</param>
        public void LoadText( string text )
        {
            if ( text.StartsWith( "=" ) )
            {
                Type = eTypeObjectStream.Variable;
                VariableName = text.Substring( 1 ).Trim( );
            }
            else
                LoadValue( text );
        }

        /// <summary>
        /// Клонирует общие данные объектов
        /// </summary>
        /// <param name="obj">Клонируемый объект</param>
        protected void CloneData( ObjectSequence obj )
        {
            obj._type = _type;
            obj._object = (ObjectSequence)_object.Clone( );
            obj.TypeCollection = TypeCollection;
        }

        /// <summary>
        /// Загружает значение из файла  в объект
        /// </summary>
        /// <param name="text">Tекст значения</param>
        protected virtual void LoadValue(string text)
        {
            Type = eTypeObjectStream.Value;
            Value value = Default; 
            value.Data = text;
            Value = value;
        }

        /// <summary>
        /// Проверяет текст на название переменной
        /// </summary>
        /// <param name="text">Название переменной</param>
        /// <returns>Возврашает истину если название правильное. Иначе ложь</returns>
        protected bool CheckedVariableName( ref string text )
        {
            string @string = RemoveDoubleSpaceInText( text.Trim( ) );
            if ( @string.StartsWith( "[" ) && @string.EndsWith( "]" ) )
                @string = @string.Substring( 1, @string.Length - 2 ).Trim( );
            if ( Regex.IsMatch( @string, @"^[_a-z][ _a-z0-9\.]*$", RegexOptions.IgnoreCase ) )
            {
                text = @string;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Проверяет текст на название таблицы
        /// </summary>
        /// <param name="text">название таблицы</param>
        /// <returns>Возврашает истину если название правильное. Иначе ложь</returns>
        protected bool CheckedTableName( ref string text )
        {
            string @string = RemoveDoubleSpaceInText( text.Trim( ) );
            if ( Regex.IsMatch( @string, @"^[_a-z][_a-z0-9]*$", RegexOptions.IgnoreCase ) )
            {
                text = @string;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Удаляет из текста двой ные пробклы
        /// </summary>
        /// <param name="@string">Строка текста</param>
        /// <returns>Строка без двойных пробелов</returns>
        private string RemoveDoubleSpaceInText( string @string )
        {
            int length;
            @string = @string.Replace( "\t", " " );
            @string = @string.Replace( "\n", " " );
            do
            {
                length = @string.Length;
                @string = @string.Replace( "  ", " " );
            }
            while ( length != @string.Length );
            return @string;
        }
    }
}
