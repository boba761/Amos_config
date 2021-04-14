using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Calculations.Expressions;

namespace Calculations.Visitors
{
    /// <summary>
    /// Базовый класс для визитёра выражения
    /// </summary>
    public abstract class Visitor
    {
        /// <summary>
        /// Вычисление визита значения
        /// </summary>
        /// <param name="expression">Объект выражения</param>
        public abstract void Visit( ValueExpression expression );

        /// <summary>
        /// Вычисление визита двухпараметрового выражения
        /// </summary>
        /// <param name="expression">Объект выражения</param>
        public abstract void Visit( VariableExpression expression );

        /// <summary>
        /// Вычисление визита однопараметрового выражения
        /// </summary>
        /// <param name="expression">Объект выражения</param>
        public abstract void Visit( UnaryExpression expression );

        /// <summary>
        /// Вычисление визита переменной
        /// </summary>
        /// <param name="expression">Объект выражения</param>
        public abstract void Visit( BinaryExpresssion expression );

        /// <summary>
        /// Вычисление визита трёхпараметрового выражения
        /// </summary>
        /// <param name="expression">Объект выражения</param>
        public abstract void Visit( TrenaryExpression expression );

        /// <summary>
        /// Вычисление визита  выражения функции
        /// </summary>
        /// <param name="expression">Объект выражения</param>
        public abstract void Visit( FunctionExpression expression );
    }
}
