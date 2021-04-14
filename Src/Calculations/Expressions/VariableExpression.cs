using System.Xml.Serialization;
using Calculations.Visitors;

namespace Calculations.Expressions
{
    /// <summary>
    /// Класс выражения для переменных
    /// </summary>
    public abstract class VariableExpression : Expression
    {
        /// <summary>
        /// Возвращает объект выражения заначения пременной
        /// </summary>
        [XmlIgnore]
        public abstract ValueExpression ValueExpression { get; }

        /// <summary>
        /// Возвращает название выражения
        /// </summary>
        [XmlAttribute]
        public string Name { get; set; }

        /// <summary>
        /// Исполняет выражение над визитёром
        /// </summary>
        /// <param name="visitor">Визитёр выражения</param>
        public override void Accept( Visitor visitor )
        {
            visitor.Visit( this );
        }

        /// <summary>
        /// Возвращает текст выражения
        /// </summary>
        public override string ToText( )
        {
            return string.Format( "[{0}]", Name );
        }
    }
}
