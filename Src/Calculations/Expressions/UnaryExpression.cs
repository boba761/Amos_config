using System.Collections.Generic;
using Calculations.Visitors;

namespace Calculations.Expressions
{
    /// <summary>
    /// Класс для однопараметрового выражения
    /// </summary>
    public class UnaryExpression : Expression
    {
        /// <summary>
        /// Конструктор класса
        /// </summary>
        public UnaryExpression( string name, Expression expression )
        {
            Name = name;
            Expression = expression;
        }

        /// <summary>
        /// Возвращает название выражения
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Возвращает параметр  выражения
        /// </summary>
        public Expression Expression { get; private set; }

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
            List<string> signateres = Compiler.GetOperator( Name ).Signatures;
            if ( signateres.Count > 1 )
                return string.Format( "{0}{1}{2}", signateres[0], Expression.ToText( ), signateres[1] );
            else
                return string.Format( "{0}{1}", signateres[0], Expression.ToText( ) );
        }
    }
}
