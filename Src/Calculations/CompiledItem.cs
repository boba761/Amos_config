using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Calculations.Expressions;

namespace Calculations
{
    public enum eCompiledItemKind
    {
        Expression,
        Signature
    }

    /// <summary>
    /// Класс для хранения промежуточного результата компиляции выражения
    /// </summary>
    public sealed class CompiledItem
    {
        /// <summary>
        /// Параметризованный конструктор
        /// </summary>
        /// <param name="expression">Объект выражения</param>
        public CompiledItem(Expression expression)
        {
            Kind = eCompiledItemKind.Expression;
            Expression = expression;
        }

        /// <summary>
        /// Параметризованный конструктор
        /// </summary>
        /// <param name="text">Текст выражения</param>
        /// <param name="operators">Коллекция операторов выражения</param>
        public CompiledItem(string text, ICollection<Operator> operators)
        {
            Kind = eCompiledItemKind.Signature;
            Text = text;
            Operators = operators;
        }

        /// <summary>
        /// Возвращает какой тип содержит объект
        /// </summary>
        public eCompiledItemKind Kind { get; private set; }

        /// <summary>
        /// Возвращает объект выражения
        /// </summary>
        public Expression Expression { get; private set; }

        /// <summary>
        /// Возвращает текст выражения
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// Возвращает колекцию операторов
        /// </summary>
        public ICollection<Operator> Operators { get; private set; }

        /// <summary>
        /// Возвращает текстовое представление объекта
        /// </summary>
        public override string ToString()
        {
            return Kind.ToString();
        }
    }
}
