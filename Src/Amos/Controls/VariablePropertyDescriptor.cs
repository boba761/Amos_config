using System;
using System.ComponentModel;
using Amos.Controls;
using Amos.Views;
using Calculations.Variables;

namespace Amos.Controls
{
    /// <summary>
    /// Custom PropertyDescriptor
    /// </summary>
    public class VariablePropertyDescriptor : PropertyDescriptor
    {
        private VariableBase _property; 
        private VariableBase _collection;
        private VariableConvertor _variableConvertor;

        public VariablePropertyDescriptor( VariableBase property, VariableBase collection )
            : base(property.Name, null)
        {
            _property = property;
            _collection = collection;
            _variableConvertor = new VariableConvertor();
        }

        #region PropertyDescriptor specific

        public override bool CanResetValue(object component)
        {
            return true;
        }

        public override Type ComponentType
        {
            get 
            {
                return null; 
            }
        }

        public override string Description
        {
            get { return _property.Description; }
        }

        public override string Category
        {
            get 
            {
                if ( _collection == null )
                    return null;
                return _collection.Name;
            }
        }

        public override string DisplayName
        {
            get 
            {
                return _property.Name; 
            }
        }

        public override bool IsReadOnly
        {
            get 
            {
                if ( _property is CollectionVariable )
                    return _property.IsReadOnly;
                else
                {
                    Variable objectVariable = _property as Variable;
                    return objectVariable.IsReadOnly || ( objectVariable.Value.IsExpression && 
                        ( ( Variable.IsShowExpression && objectVariable.IsLookExpression ) ||
                        ( !Variable.IsShowExpression && !objectVariable.IsLookExpression ) ) );
                }
            }
        }
        
        public override Type PropertyType
        {
            get 
            {
                if ( _property is CollectionVariable )
                    return _property.GetType();
                return typeof(string); 
            }
        }

        public override void ResetValue(object component)
        {
        }

        public override bool ShouldSerializeValue(object component)
        {
            return false;
        }

        public override object GetValue(object component)
        {
            if ( _property is CollectionVariable )
                return _property;
            Variable objectVariable = _property as Variable;
            if ( Variable.IsShowExpression && objectVariable.Value.IsExpression )
                return objectVariable.Value.Expression;
            return objectVariable.Value;
        }

        public override void SetValue(object component, object value)
        {
            if ( _property is Variable )
            {
                ( _property as Variable ).SetValue( Program.Document.Dashboard.Compiler, (string)value );
                DashboardView.Instance().Refresh();
            }
        }

        public override object GetEditor(Type editorBaseType)
        {
            return  new VariableTypeEditor();
        }

        public override TypeConverter Converter
        {
            get
            {
                if ( _property is CollectionVariable )
                    return _variableConvertor;
                else
                    return base.Converter;
            }
        }

        #endregion
    }
}
