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
    public class FrequencyTableSequence : TableSequence
    {
        private GradientAmplitudeControl _control;

        public FrequencyTableSequence( ) 
        {
            _generateAuto = new GenerateAutoTable( ); 
        }

        public FrequencyTableSequence( IObjectStream @object, string name = null )
            : base( @object )
        {
            if ( name != null )
                Name = name;
            GenerateAuto = new GenerateAutoTable( );
        }

        [XmlIgnore]
        public override string ClassTable { get { return "Frequency Table"; } }
        [XmlIgnore]
        public override string Description { get { return "Offset (Hz)\nfloating point"; } }

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
            foreach ( Match match in Regex.Matches( text, FrequencyValue.stringRegex, RegexOptions.IgnoreCase ) )
            {
                if ( !FrequencyValue.IsCheck( match.Value ) )
                    continue;
                Value valueObject = new FrequencyValue( match.Value );
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
            return new FrequencyTableSequence( );
        }
    }
}
