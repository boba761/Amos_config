using Calculations.Visitors;
using System.Xml.Serialization;

namespace Calculations.Expressions
{
    /// <summary>
    /// Класс выражения значения
    /// </summary>
    public abstract class ValueExpression : Expression
    {
        /// <summary>
        /// Возвращает значение выражения
        /// </summary>
        [XmlIgnore]
        public abstract object Data { get; set; }

        /// <summary>
        /// Исполняет выражение над визитёром
        /// </summary>
        /// <param name="visitor">Визитёр выражения</param>
        public override void Accept(Visitor visitor)
        {
            visitor.Visit(this);
        }

        /// <summary>
        /// Возвращает текст выражения
        /// </summary>
        public override string ToText( )
        {
            return ToString( );
        }
    }
}
