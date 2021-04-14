using System;
using System.ComponentModel;
using Calculations.Variables;

namespace Amos.Controls
{
    class VariableConvertor : ExpandableObjectConverter
    {
        public override object ConvertTo( ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType )
        {
            if ( value is CollectionVariable )
                return "";
            return base.ConvertTo( context, culture, value, destinationType );
        }
    }
}
