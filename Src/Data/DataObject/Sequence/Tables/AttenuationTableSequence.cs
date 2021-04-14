using System;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Calculations.Values;
using Tools.Interfaces;

namespace Data.DataObject.Sequence.Tables
{
    [Serializable]
    public class AttenuationTableSequence : TableSequence
    {
        public AttenuationTableSequence( ) { }

        public AttenuationTableSequence( IObjectStream @object, string name = null )
            : base(@object)
        {
            if (name != null)
                Name = name;
        }

        [XmlIgnore]
        public override string ClassTable { get { return "Attenuation Table"; } }
        [XmlIgnore]
        public override string Description { get { return "Decibels\nin 0.5 dB Steps"; } }

        public override void SetListValues( string text )
        {
            _valueList.Clear( );
            foreach ( Match match in Regex.Matches( text, AttenuationValue.stringRegex, RegexOptions.IgnoreCase ) )
            {
                if ( !AttenuationValue.IsCheck( match.Value ) )
                    continue;
                Value valueObject = new AttenuationValue( match.Value );
                valueObject.Data = ( (int)( Convert.ToDouble( valueObject.Data ) * 2 ) ) / 2;
                _valueList.Add( valueObject );
            }
        }

        protected override TableSequence New( )
        {
            return new AttenuationTableSequence( );
        }
    }
}
