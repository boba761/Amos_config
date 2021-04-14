using Calculations.Visitors;

namespace Calculations.Expressions
{
    /// <summary>
    /// Класс для трёхпараметрового выражения
    /// </summary>
    public class TrenaryExpression : Expression
    {
        /// <summary>
        /// Конструктор класса
        /// </summary>
        public TrenaryExpression( string name, Expression expression1, Expression expression2, Expression expression3 )
        {
            Name = name;
            Expression1 = expression1;
            Expression2 = expression2;
            Expression3 = expression3;
        }

        /// <summary>
        /// Возвращает название выражения
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Возвращает первый параметр  выражения
        /// </summary>
        public Expression Expression1 { get; private set; }

        /// <summary>
        /// Возвращает второй параметр  выражения
        /// </summary>
        public Expression Expression2 { get; private set; }

        /// <summary>
        /// Возвращает третий  параметр  выражения
        /// </summary>
        public Expression Expression3 { get; private set; }

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
            return string.Format( "{0}{1}{2}{3}{4}", Expression1.ToText( ), Compiler.GetOperator( Name ).Signatures[0],
                Expression2.ToText( ), Compiler.GetOperator( Name ).Signatures[1], Expression3.ToText( ) );
        }
    }
}
