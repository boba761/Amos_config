using System;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Calculations.Values;
using Tools.Interfaces;

namespace Amos.Data.Sequence.Tables
{
    [Serializable]
    public class GradientWaveformTableSequence : TableSequence
    {
        public GradientWaveformTableSequence( ) { }

        public GradientWaveformTableSequence( IObjectStream @object, string name = null )
            : base( @object )
        {
            if ( name != null )
                Name = name;
        }

        [XmlIgnore]
        public override string ClassTable { get { return "Gradient Waveform"; } }
        [XmlIgnore]
        public override string Description { get { return "-100.0 - 100.0\nfloating point"; } }

        public override void SetListValues( string text )
        {
            _valueList.Clear( );
            foreach ( Match match in Regex.Matches( text, DoubleValue.stringRegex, RegexOptions.IgnoreCase ) )
            {
                if ( !DoubleValue.IsCheck( match.Value ) )
                    continue;
                Value valueObject = new DoubleValue( match.Value );
                double value = Convert.ToDouble( valueObject.Data );
                if ( value < -100.0 )
                    valueObject.Data = -100.0;
                if ( value > 100.0 )
                    valueObject.Data = 100.0;
                _valueList.Add( valueObject );
            }
        }

        protected override TableSequence New( )
        {
            return new GradientWaveformTableSequence( );
        }
    }
}
