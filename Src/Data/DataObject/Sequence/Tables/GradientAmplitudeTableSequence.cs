using System;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Calculations.Values;
using Data.DataObject.Sequence.Tables.DataTables;
using Tools.Interfaces;

namespace Data.DataObject.Sequence.Tables
{
    [Serializable]
    public class GradientAmplitudeTableSequence : TableSequence
    {
        public GradientAmplitudeTableSequence( ) 
        {
            _generateAuto = new GenerateAutoTable( ); 
        }

        public GradientAmplitudeTableSequence( IObjectStream @object, string name = null )
            : base( @object )
        {
            if ( name != null )
                Name = name;
             GenerateAuto = new GenerateAutoTable( );
        }

        [XmlIgnore]
        public override string ClassTable { get { return "Gradient Amplitude"; } }

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
        
        public override string ToString( )
        {
            if ( Data.Auto )
                return string.Format( "{0} = Automatic", Name );
            return base.ToString( );
        }

        protected override TableSequence New( )
        {
            return new GradientAmplitudeTableSequence( );
        }
    }
}
