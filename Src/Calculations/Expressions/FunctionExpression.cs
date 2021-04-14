using System.Collections.Generic;
using System.Linq;
using Calculations.Visitors;

namespace Calculations.Expressions
{
    /// <summary>
    /// Класс для выражения функции
    /// </summary>
    public class FunctionExpression : Expression
    {
        /// <summary>
        /// Конструктор класса
        /// </summary>
        public FunctionExpression( string name, ICollection<Expression> expressions )
        {
            Name = name;
            Expressions = expressions.ToArray();
        }

        /// <summary>
        /// Возвращает название выражения
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Возвращает массив параметров  выражения
        /// </summary>
        public Expression[] Expressions { get; private set; }

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
            string text = signateres[0] + signateres[1];
            for ( int i = 0; i < Expressions.Length; i++ )
                text += string.Format( "{0}{1}", Expressions[i].ToText(), signateres[i + 2] );
            return text;
        }
    }
}
