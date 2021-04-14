using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Calculations.Values;
using Data.DataObject.Sequence.Tables.DataTables;
using Tools.Interfaces;

namespace Data.DataObject.Sequence.Tables
{
    [Serializable]
    public class DelayTableSequence : TableSequence
    {
        //private DelayTableControl _control;

        public DelayTableSequence( ) 
        {
            _delayAuto = new DelayAutoTable( );
        }

        public DelayTableSequence( IObjectStream @object, string name = null )
            : base( @object )
        {
            if ( name != null )
                Name = name;
            DelayAuto = new DelayAutoTable( );
        }

        [XmlIgnore]
        public override string ClassTable { get { return "Delay Table"; } }

        //[XmlIgnore]
        //public override UserControl AutoControl 
        //{ 
        //    get 
        //    {
        //        if ( _control == null )
        //            _control = new DelayTableControl( DelayAuto );
        //        return _control; 
        //    } 
        //}

        public override void SetListValues( string text )
        {
            _valueList.Clear( );
            foreach ( Match match in Regex.Matches( text, TimeValue.stringRegex, RegexOptions.IgnoreCase ) )
            {
                if ( !TimeValue.IsCheck( match.Value ) )
                    continue;
                Value valueObject = new TimeValue( match.Value );
                _valueList.Add( valueObject );
            }
        }

        public override string ToString( )
        {
            if ( Data.Auto )
                return string.Format( "{0} = {1}", Name, GetText( ) ); 
            return base.ToString( );
        }

        protected override TableSequence New( )
        {
            return new DelayTableSequence( );
        }

        private string GetText( )
        {
            StringBuilder stringBuilder = new StringBuilder( "{" );
            //if ( DelayAuto.Add == 0 )
            stringBuilder.Append( "+:" );
            switch ( DelayAuto.Every )
            {
            case 0:
                stringBuilder.Append( "1" );
                break;
            case 1:
                stringBuilder.Append( "0,1" );
                break;
            }
            stringBuilder.Append( "} " );
            stringBuilder.Append( DelayAuto.Increment.StartsWith( "=" ) ? DelayAuto.Increment.Substring(1) : DelayAuto.Increment );
            return stringBuilder.ToString();
        }
    }
}
