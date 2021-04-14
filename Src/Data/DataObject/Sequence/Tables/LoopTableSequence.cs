using System;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Calculations.Values;
using Tools.Interfaces;

namespace Data.DataObject.Sequence.Tables
{
    [Serializable]
    public class LoopTableSequence : TableSequence
    {
        public LoopTableSequence( ) { }

        public LoopTableSequence( IObjectStream @object, string name = null )
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
                return "Loop Table";
            }
        }

        public override void SetListValues( string text )
        {
            _valueList.Clear( );
            foreach ( Match match in Regex.Matches( text, IntegerValue.stringRegex, RegexOptions.IgnoreCase ) )
            {
                if ( !IntegerValue.IsCheck( match.Value ) )
                    continue;
                Value valueObject = new IntegerValue( match.Value );
                _valueList.Add( valueObject );
            }
        }

        protected override TableSequence New( )
        {
            return new LoopTableSequence( );
        }
    }
}
