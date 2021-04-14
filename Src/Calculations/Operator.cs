using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Calculations.Expressions;

namespace Calculations
{
    public enum eOperatorKind
    {
        Variable, 
        Operator,
        Function,
    }

    /// <summary>
    /// Класс для хранения элемента оператора
    /// </summary>
    public class OperatorItem
    {
        /// <summary>
        /// Конструктор класса
        /// </summary>
        public OperatorItem( )
        {
            Kind = eCompiledItemKind.Expression;
        }

        /// <summary>
        /// Параметризованный конструктор
        /// </summary>
        /// <param name="text">Текст элемента оператора</param>
        public OperatorItem(string text)
        {
            Kind = eCompiledItemKind.Signature;
            Text = text;
        }

        /// <summary>
        /// Возвращает тип элемента оператора
        /// </summary>
        public eCompiledItemKind Kind { get; private set; }

        /// <summary>
        /// Возвращает текст элемента оператора
        /// </summary>
        public string Text { get; private set; }

        /// <summary>
        /// Возвращает текстовое представление объекта
        /// </summary>
        public override string ToString()
        {
            return Kind.ToString();
        }
    }

    /// <summary>
    /// Класс оператора компилятора
    /// </summary>
    public class Operator
    {
        private string _text;
        private List<OperatorItem> _operatorItemList;

        /// <summary>
        /// Параметризованный конструктор
        /// </summary>
        /// <param name="name">Название оператора</param>
        /// <param name="kind">Тип оператора</param>
        /// <param name="signature">Строка сигнатуры</param>
        /// <param name="priority">Приоритет оператора</param>
        public Operator(string name, eOperatorKind kind, string signature, int priority) 
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (signature == null)
                throw new ArgumentNullException("signature");
            if (signature.Length == 0)
                throw new ArgumentException("Signature is empty.", "signature");
            Name = name;
            Kind = kind;
            _text = signature;
            Signatures = new List<string>();
            _operatorItemList = GetKingList(signature);
        }

        /// <summary>
        /// Возвращает приоритет оператора
        /// </summary>
        public int Priority { get; private set; }

        /// <summary>
        /// Возвращает тип оператора
        /// </summary>
        public eOperatorKind Kind { get; private set; }

        /// <summary>
        /// Возвращает название оператора
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Возращает список сигнатур компилятора
        /// </summary>
        public List<string> Signatures { get; private set; }

        /// <summary>
        /// Возвращает объект выражения
        /// </summary>
        /// <param name="items">Колекция элементов выражений сомпиляции</param>
        /// <param name="indexEvent">Индек начала проверки</param>
        public Expression GetExpression(List<CompiledItem> items, int index)
        {
            if (_operatorItemList[0].Kind == eCompiledItemKind.Expression)
                index--;
            List<Expression> expressionList = new List<Expression>();
            for (int i = 0; i <_operatorItemList.Count;  i++)
            {
                if (items[index].Kind == eCompiledItemKind.Expression)
                    expressionList.Add(items[index].Expression);
                items.RemoveAt(index);
            }
            Expression expression;
            if (Kind == eOperatorKind.Operator)
            {
                switch (expressionList.Count)
                {
                    case 1:
                        expression = new UnaryExpression(Name, expressionList[0]);
                        break;
                    case 2:
                        expression = new BinaryExpresssion(Name, expressionList[0], expressionList[1]);
                        break;
                    case 3:
                        expression = new TrenaryExpression(Name, expressionList[0], expressionList[1], expressionList[2]);
                        break;
                    default:
                        throw new CompiledException("Not Find class for type expression.");
                }
            }
            else
                expression = new FunctionExpression(Name, expressionList);

            items.Insert(index, new CompiledItem(expression));
            return expression;
        }

        /// <summary>
        /// Проверка входного орератора на совпадение
        /// </summary>
        /// <param name="items">Колекция элементов выражений сомпиляции</param>
        /// <param name="indexEvent">Индек начала проверки</param>
        public bool IsCheck(List<CompiledItem> items, int index)
        {
            if (items[index].Text != Signatures[0])
                return false;
            int startIndex = 0;
            if (_operatorItemList[0].Kind == eCompiledItemKind.Expression)
            {
                if (index == 0)
                    return false;
                else if (items[index-1].Kind != eCompiledItemKind.Expression)
                    return false;
                else
                    startIndex = 1;
            }
            else if (index > 0 && items[index-1].Kind != eCompiledItemKind.Signature)
                return false;
            
            for (int i = startIndex; i <_operatorItemList.Count;  i++)
            {
                if (items.Count <= index)
                    return false;

                OperatorItem operatorItem = _operatorItemList[i];
                CompiledItem compiledItem = items[index];
                if (operatorItem.Kind != compiledItem.Kind)
                    return false;
                if (operatorItem.Kind == eCompiledItemKind.Signature && operatorItem.Text != compiledItem.Text)
                    return false;
                index++;
            }
            return true;
        }

        /// <summary>
        /// Возвращает список элементов оператора
        /// </summary>
        private List<OperatorItem> GetKingList(string text)
        {
            List<OperatorItem> items = new List<OperatorItem>();
            
            StringBuilder signature = null;
            for (int i=0; i<text.Length; i++)
            {
                if (text[i] == ' ')
                {
                    if (signature != null)
                    {
                        items.Add(new OperatorItem(signature.ToString()));
                        Signatures.Add(signature.ToString());
                        signature = null;
                    }
                    continue;
                }
                if (text[i] == '@')
                {
                    if (signature != null)
                    {
                        items.Add(new OperatorItem(signature.ToString()));
                        Signatures.Add(signature.ToString());
                        signature = null;
                    }
                    items.Add(new OperatorItem());
                }
                else 
                {
                    if (signature == null)
                        signature = new StringBuilder();
                    signature.Append(text[i]);
                }
            }
            if (signature != null)
            {
                items.Add(new OperatorItem(signature.ToString()));
                Signatures.Add(signature.ToString());
            }
            return items;
        }
    }
}
