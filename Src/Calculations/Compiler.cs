using System;
using System.Collections.Generic;
using Calculations.Expressions;
using Calculations.Values;
using Calculations.Variables;

namespace Calculations
{
    /// <summary>
    /// Класс для компиляции текстовых выражений
    /// </summary>
    public class Compiler
    {
        public const string FALSE = "false";
        public const string TRUE = "true";
        public const string VAR = "VAR";
        public const string BRANCE = "BRANCE";
        public const string POW = "POW";
        public const string UMINUS = "UMINUS";
        public const string UPLUS = "UPLUS";
        public const string MULT = "MULT";
        public const string DIV = "DIV";
        public const string MOD = "MOD";
        public const string MINUS = "MINUS";
        public const string PLUS = "PLUS";
        public const string EQUALS1 = "EQUALS1";
        public const string EQUALS2 = "EQUALS2";
        public const string NOTEQUALS1 = "NOTEQUALS1";
        public const string NOTEQUALS2 = "NOTEQUALS2";
        public const string LT = "LT";
        public const string LTEQ = "LTEQ";
        public const string GT = "GT";
        public const string GTEQ = "GTEQ";
        public const string NOT1 = "NOT1";
        public const string NOT2 = "NOT2";
        public const string OR1 = "OR1";
        public const string OR2 = "OR2";
        public const string AND1 = "AND1";
        public const string AND2 = "AND2";
        public const string CONDITION = "CONDITION";
        public const string SIN = "SIN";
        public const string COS = "COS";

        private static Dictionary<string, Operator> _operatorsDictionary;
        private static Dictionary<string, List<Operator>> _signaturesDictionary;

        private Dictionary<string, Variable> _variables;

        /// <summary>
        /// Статический конструктор класса
        /// </summary>
        static Compiler()
        {
            _operatorsDictionary = new Dictionary<string, Operator>();
            _signaturesDictionary = new Dictionary<string, List<Operator>>();

            Add( FALSE, eOperatorKind.Variable, "false", 0 );
            Add( TRUE, eOperatorKind.Variable, "true", 0 );
            Add( VAR, eOperatorKind.Variable, "[@]", 0 );
            Add( BRANCE, eOperatorKind.Operator, "(@)", 1 );
            Add( POW, eOperatorKind.Operator, "@^@", 2 );
            Add( UMINUS, eOperatorKind.Operator, "-@", 3 );
            Add( UPLUS, eOperatorKind.Operator, "+@", 3 );
            Add( MULT, eOperatorKind.Operator, "@*@", 4 );
            Add( DIV, eOperatorKind.Operator, "@/@", 4 );
            Add( MOD, eOperatorKind.Operator, "@%@", 4 );
            Add( MINUS, eOperatorKind.Operator, "@-@", 5 );
            Add( PLUS, eOperatorKind.Operator, "@+@", 5 );
            Add( EQUALS1, eOperatorKind.Operator, "@=@", 6 );
            Add( EQUALS2, eOperatorKind.Operator, "@==@", 6 );
            Add( NOTEQUALS1, eOperatorKind.Operator, "@!=@", 6 );
            Add( NOTEQUALS2, eOperatorKind.Operator, "@<>@", 6 );
            Add( LT, eOperatorKind.Operator, "@<@", 6 );
            Add( LTEQ, eOperatorKind.Operator, "@<=@", 6 );
            Add( GT, eOperatorKind.Operator, "@>@", 6 );
            Add( GTEQ, eOperatorKind.Operator, "@>=@", 6 );
            Add( NOT1, eOperatorKind.Operator, "!@", 7 );
            Add( NOT2, eOperatorKind.Operator, "not@", 7 );
            Add( OR1, eOperatorKind.Operator, "@||@", 8 );
            Add( OR2, eOperatorKind.Operator, "@or@", 8 );
            Add( AND1, eOperatorKind.Operator, "@&&@", 8 );
            Add( AND2, eOperatorKind.Operator, "@and@", 8 );
            Add( CONDITION, eOperatorKind.Operator, "@?@:@", 9 );
            Add( SIN, eOperatorKind.Function, "sin (@)", 10 );
            Add( COS, eOperatorKind.Function, "cos (@)", 0 );
        }

        /// <summary>
        /// Параметризованный конструктор
        /// </summary>
        /// <param name="variables">Хесш списка объектов переменных</param>
        public Compiler( Dictionary<string, Variable> variables )
        {
            _variables = variables;
        }

        /// <summary>
        /// Возращает объект оператора
        /// </summary>
        /// <param name="name">Название оператора</param>
        public static Operator GetOperator(string name)
        {
            return _operatorsDictionary[name];
        }

        /// <summary>
        /// Компиляция текстового выражения
        /// </summary>
        /// <param name="sourceString">Строка с выражением</param>
        /// <param name="value">Объект занчения объекта переменной</param>
        /// <returns>Скомпилированный объект выражения</returns>
        public Expression Compile( string sourceString, Value value )
        {
            if ( sourceString == null )
                throw new ArgumentNullException( "sourceString" );
            if ( sourceString.Length == 0 )
                throw new ArgumentException( "String is empty.", "sourceString" );

            List<CompiledItem> res = new List<CompiledItem>();

            int length;
            string operandString;
            CompiledItem addItem;
            string signature;
            List<Operator> collection = null;
            bool operandStarted = false;
            int operandStartIndex = 0;

            for ( int i = 0; i < sourceString.Length; i++ )
            {
                if ( sourceString[i] == ' ' )
                    continue;
                length = 0;
                addItem = null;
                signature = null;
                operandString = sourceString.Substring( i );
                foreach ( KeyValuePair<string, List<Operator>> pair in _signaturesDictionary )
                {
                    if ( operandString.StartsWith( pair.Key ) && pair.Key.Length > length )
                    {
                        signature = pair.Key;
                        collection = pair.Value;
                    }
                }
                if ( signature != null )
                {
                    if ( collection[0].Kind == eOperatorKind.Variable )
                    {
                        if ( collection[0].Signatures.Count == 1 )
                            res.Add( GetVariableOrValue( signature, value ) );
                        else
                        {
                            signature = operandString.Substring( 0, operandString.IndexOf( collection[0].Signatures[1] ) + collection[0].Signatures[1].Length );
                            res.Add( GetVariableOrValue( signature.Substring( collection[0].Signatures[0].Length,
                                signature.Length - ( collection[0].Signatures[0].Length + collection[0].Signatures[1].Length ) ), value ) );
                        }
                        i += signature.Length - 1;
                        continue;
                    }

                    addItem = new CompiledItem( signature, collection );
                    if ( operandStarted )
                    {
                        res.Add( GetVariableOrValue( sourceString.Substring( operandStartIndex, i - operandStartIndex ), value ) );
                        operandStarted = false;
                    }
                    res.Add( addItem );
                    if ( addItem.Kind == eCompiledItemKind.Signature )
                        i += addItem.Text.Length - 1;
                }
                else if ( !operandStarted )
                {
                    operandStarted = true;
                    operandStartIndex = i;
                }
            }
            if ( operandStarted )
                res.Add( GetVariableOrValue( sourceString.Substring( operandStartIndex ), value ) );
            return LinkExpression( res );
        }

        /// <summary>
        /// Сборка выражения из списка элементов сомппиляции
        /// </summary>
        /// <param name="items">Список элементов коприляции</param>
        /// <returns>Скомпилированный объект выражения</returns>
        private Expression LinkExpression( List<CompiledItem> items )
        {
            if ( items.Count == 0 )
                throw new CompiledException( "Null expression." );
            if ( items.Count == 1 )
            {
                if ( items[0].Kind != eCompiledItemKind.Expression )
                    throw new CompiledException( "Not expression." );
                return items[0].Expression;
            }
            int currentIndex = 0;
            Operator currentOperator = null;
            for ( int i = 0; i < items.Count; i++ )
            {
                if ( items[i].Kind == eCompiledItemKind.Signature )
                {
                    foreach ( Operator obj in items[i].Operators )
                    {
                        if ( currentOperator != null && currentOperator.Priority <= obj.Priority )
                            continue;
                        if ( obj.IsCheck( items, i ) )
                        {
                            currentOperator = obj;
                            currentIndex = i;
                        }
                    }
                }
            }
            if ( currentOperator == null )
                throw new CompiledException( "Error expression." );
            Expression expression = currentOperator.GetExpression( items, currentIndex );
            if ( items.Count == 1 )
                return expression;
            return LinkExpression( items );
        }

        /// <summary>
        /// Добавление оператора в хаш компилятора
        /// </summary>
        /// <param name="name">Название оператора</param>
        /// <param name="kind">Тип оператора</param>
        /// <param name="signature">Строка с сигнатурой оператора</param>
        /// <param name="priority">Приоритет оператора</param>
        private static void Add( string name, eOperatorKind kind, string signature, int priority )
        {
            Operator Operator = new Operator( name, kind, signature, priority );
            _operatorsDictionary.Add( name, Operator );
            foreach ( string str in Operator.Signatures )
            {
                List<Operator> collection = null;
                if ( _signaturesDictionary.TryGetValue( str, out collection ) == false )
                {
                    collection = new List<Operator>();
                    _signaturesDictionary.Add( str, collection );
                }
                collection.Add( Operator );
            }
        }

        /// <summary>
        /// Возвращает элемет компиляции переменной или значения
        /// </summary>
        /// <param name="textVariable">Обрабатываемый текст</param>
        /// <param name="iValue">Объект занчения объекта переменной</param>
        private CompiledItem GetVariableOrValue( string textVariable, Value iValue )
        {
            string text = textVariable.Trim();
            Variable variable = null;
            if ( _variables.TryGetValue( text, out variable ) )
            {
                variable.Value.AddOutput( iValue );
                return new CompiledItem( variable );
            }
            else
            {
                Value value = Value.CreateInstance( text );
                if ( value != null )
                    return new CompiledItem( value );
                else
                    throw new CompiledException( String.Format( "{0} is not valid variable identifier.", text ) );
            }
        }
    }
}
