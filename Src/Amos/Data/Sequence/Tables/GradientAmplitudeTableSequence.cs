using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Serialization;
using Amos.Controls;
using Amos.Data.Sequence.Tables.DataTables;
using Calculations.Values;
using Tools.Interfaces;

namespace Amos.Data.Sequence.Tables
{
    [Serializable]
    public class GradientAmplitudeTableSequence : TableSequence
    {
        private GradientAmplitudeControl _control;

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
        [XmlIgnore]
        public override UserControl AutoControl
        {
            get
            {
                if ( _control == null )
                    _control = new GradientAmplitudeControl( GenerateAuto );
                return _control;
            }
        }

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
