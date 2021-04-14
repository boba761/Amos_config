using System;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Calculations.Values;
using Tools.Interfaces;

namespace Amos.Data.Sequence.Tables
{
    [Serializable]
    public class RotationAngleTableSequence : TableSequence
    {
        public RotationAngleTableSequence( ) { }

        public RotationAngleTableSequence( IObjectStream @object, string name = null )
            : base( @object )
        {
            if ( name != null )
                Name = name;
        }

        [XmlIgnore]
        public override string ClassTable
        {
            get
            {
                return "Rotation Angle";
            }
        }

        [XmlIgnore]
        public override string Description
        {
            get
            {
                return "-360.0 - 360.0\nFloating point";
            }
        }

        public override void SetListValues( string text )
        {
            _valueList.Clear( );
            foreach ( Match match in Regex.Matches( text, CornerValue.stringRegex, RegexOptions.IgnoreCase ) )
            {
                if ( !CornerValue.IsCheck( match.Value ) )
                    continue;
                Value valueObject = new CornerValue( match.Value );
                double value = Convert.ToDouble( valueObject.Data );
                if ( value < -360.0 )
                    valueObject.Data = -360.0;
                if ( value > 360.0 )
                    valueObject.Data = 360.0;
                _valueList.Add( valueObject );
            }
        }

        protected override TableSequence New( )
        {
            return new RotationAngleTableSequence( );
        }
    }
}
