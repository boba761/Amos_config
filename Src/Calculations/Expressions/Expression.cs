using Calculations.Visitors;

namespace Calculations.Expressions
{
    /// <summary>
    /// Базовый класс для классов выражений
    /// </summary>
    public abstract class Expression
    {
        /// <summary>
        /// Исполняет выражение над визитёром
        /// </summary>
        /// <param name="visitor">Визитёр выражения</param>
        public abstract void Accept(Visitor visitor);

        /// <summary>
        /// Вычисляет результат выражения
        /// </summary>
        /// <returns>Результат выражения</returns>
        public object Evaluate( )
        {
            ExpressionVisitor visitor = new ExpressionVisitor( );
            Accept(visitor);
            return visitor.Result;
        }

        /// <summary>
        /// Возвращает текст выражения
        /// </summary>
        public abstract string ToText( );
    }
}
