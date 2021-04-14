using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Calculations.Expressions;

namespace Calculations.Visitors
{
    /// <summary>
    /// Класс для визитёра выражения
    /// </summary>
    public class ExpressionVisitor : Visitor
    {
        private object _result;

        /// <summary>
        /// Возвращает результат выражения
        /// </summary>
        public object Result
        {
            get { return _result; }
        }

        /// <summary>
        /// Вычисление визита однопараметрового выражения
        /// </summary>
        /// <param name="expression">Объект выражения</param>
        public override void Visit( ValueExpression expression )
        {
            _result = expression.Data;
        }

        /// <summary>
        /// Вычисление визита переменной
        /// </summary>
        /// <param name="expression">Объект выражения</param>
        public override void Visit( VariableExpression expression )
        {
            _result = expression.ValueExpression.Data;
        }

        /// <summary>
        /// Вычисление визита значения
        /// </summary>
        /// <param name="expression">Объект выражения</param>
        public override void Visit( UnaryExpression expression )
        {
            expression.Expression.Accept(this);

            switch (expression.Name)
            {
                case Compiler.BRANCE:
                    break;
                case Compiler.UMINUS:
                    _result = -Convert.ToDouble(_result);
                    break;
                case Compiler.UPLUS:
                    _result = Math.Abs(Convert.ToDouble(_result));
                    break;
                case Compiler.NOT1:
                case Compiler.NOT2:
                    _result = !Convert.ToBoolean(_result);
                    break;
                default:
                    throw new ProcessorException("Not processing unary operator [" + expression.Name + "].");
            }
        }

        /// <summary>
        /// Вычисление визита двухпараметрового выражения
        /// </summary>
        /// <param name="expression">Объект выражения</param>
        public override void Visit(BinaryExpresssion expression)
        {
            expression.Expression1.Accept(this);
            object left = _result;

            expression.Expression2.Accept(this);
            object right = _result;

            switch (expression.Name)
            {
                case Compiler.POW:
                    _result = Math.Pow(Convert.ToDouble(left), Convert.ToDouble(right));
                    break;
                case Compiler.DIV:
                    _result = Convert.ToDouble(left) / Convert.ToDouble(right);
                    break;
                case Compiler.MULT:
                    _result = Convert.ToDouble(left) * Convert.ToDouble(right);
                    break;
                case Compiler.MOD:
                    _result = Convert.ToInt32(left) % Convert.ToInt32(right);
                    break;
                case Compiler.MINUS:
                        _result = Convert.ToDouble(left) - Convert.ToDouble(right);
                    break;
                case Compiler.PLUS:
                    _result = Convert.ToDouble(left) + Convert.ToDouble(right);
                    break;
                case Compiler.EQUALS1:
                case Compiler.EQUALS2:
                    _result = Convert.ToDouble(left) == Convert.ToDouble(right);
                    break;
                case Compiler.NOTEQUALS1:
                case Compiler.NOTEQUALS2:
                    _result = Convert.ToDouble(left) != Convert.ToDouble(right);
                    break;
                case Compiler.LT:
                    _result = Convert.ToDouble(left) < Convert.ToDouble(right);
                    break;
                case Compiler.LTEQ:
                    _result = Convert.ToDouble(left) <= Convert.ToDouble(right);
                    break;
                case Compiler.GT:
                    _result = Convert.ToDouble(left) > Convert.ToDouble(right);
                    break;
                case Compiler.GTEQ:
                    _result = Convert.ToDouble(left) >= Convert.ToDouble(right);
                    break;
                case Compiler.OR1:
                case Compiler.OR2:
                    _result = Convert.ToBoolean(left) || Convert.ToBoolean(right);
                    break;
                case Compiler.AND1:
                case Compiler.AND2:
                    _result = Convert.ToBoolean(left) || Convert.ToBoolean(right);
                    break;
                default:
                    throw new ProcessorException("Not processing binary operator [" + expression.Name + "].");
            }
        }

        /// <summary>
        /// Вычисление визита трёхпараметрового выражения
        /// </summary>
        /// <param name="expression">Объект выражения</param>
        public override void Visit(TrenaryExpression expression)
        {
            expression.Expression1.Accept(this);
            object res1 = _result;
            expression.Expression2.Accept(this);
            object res2 = _result;
            expression.Expression3.Accept(this);
            object res3 = _result;

            _result = Convert.ToBoolean(res1) ? Convert.ToDouble(res2) : Convert.ToDouble(res3);
        }

        /// <summary>
        /// Вычисление визита  выражения функции
        /// </summary>
        /// <param name="expression">Объект выражения</param>
        public override void Visit(FunctionExpression expression)
        {
            object[] parameters = new object[expression.Expressions.Length];
            for (int i = 0; i < parameters.Length; i++)
            {
                expression.Expressions[i].Accept(this);
                parameters[i] = _result;
            }

            switch (expression.Name)
            {
                case Compiler.SIN:
                    break;
                case Compiler.COS:
                    break;
                default:
                    throw new ProcessorException("Not processing function [" + expression.Name + "].");
            }
        }
    }
}
