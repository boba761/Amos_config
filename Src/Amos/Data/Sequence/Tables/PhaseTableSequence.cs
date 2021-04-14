using System;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Calculations.Values;
using Tools.Interfaces;

namespace Amos.Data.Sequence.Tables
{
    [Serializable]
    public class PhaseTableSequence : TableSequence
    {
        public PhaseTableSequence( ) { }

        public PhaseTableSequence( IObjectStream @object, string name = null )
            : base( @object )
        {
            if ( name != null )
                Name = name;
        }

        [XmlIgnore]
        public override bool IsStep { get { return true; } }

        [XmlIgnore]
        public override string ClassTable { get { return "Phase Table"; } }

        [XmlIgnore]
        public override string Description 
        { 
            get { return (Data.Degrees ? "Digrees\n0 - 360" : "Steps"); } 
        }

        public override void SetListValues( string text )
        {
            _valueList.Clear( );
            foreach ( Match match in Regex.Matches( text, CornerValue.stringRegex, RegexOptions.IgnoreCase ) )
            {
                if ( Data.Degrees == true )
                {
                    if ( !CornerValue.IsCheck( match.Value ) )
                        continue;
                    Value valueObject = new CornerValue( match.Value );
                    int value = Convert.ToInt32( valueObject.Data );
                    if ( value < 0 )
                        valueObject.Data = 0;
                    if ( value > 360 )
                        valueObject.Data = 360;
                    else
                        valueObject.Data = value;
                    _valueList.Add( valueObject );
                }
                else
                {
                    if ( !IntegerValue.IsCheck( match.Value ) )
                        continue;
                    _valueList.Add( new IntegerValue( match.Value ) );
                }
            }
        }

        protected override TableSequence New( )
        {
            return new PhaseTableSequence( );
        }
    }
}
