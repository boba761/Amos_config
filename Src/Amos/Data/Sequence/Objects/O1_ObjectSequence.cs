using System;
using System.Drawing;
using System.Text.RegularExpressions;
using Amos.Data.Sequence.Tables;
using Calculations.Values;
using Calculations.Variables;
using Amos.Interfaces;
using Tools.Interfaces;

namespace Amos.Data.Sequence.Objects
{
    public class O1_ObjectSequence  : ObjectSequence
    {
        public O1_ObjectSequence( ) { }

        public O1_ObjectSequence( IStream element, int index, eTypeObjectCollection typeCollection, eTypeObjectStream type, object data = null )
            : base( element, index, typeCollection )
        {
            Type = type;
            if ( typeCollection == eTypeObjectCollection._1D )
            {
                switch ( Type )
                {
                case eTypeObjectStream.Table:
                case eTypeObjectStream.Table1D2D:
                    Table = new FrequencyTableSequence( this );
                    break; 
                default:
                    Type = eTypeObjectStream.Default;
                    break;
                }
            }
            else if ( Type == eTypeObjectStream.Table )
                Table = new FrequencyTableSequence( this );
            else
                Type = eTypeObjectStream.Default;
        }

        public O1_ObjectSequence( IStream element, int index, eTypeObjectCollection typeCollection, string text)
            : base( element, index, typeCollection )
        {
            string @string = text.Trim();
            if ( typeCollection == eTypeObjectCollection._1D )
            {
                if ( @string.StartsWith( "=" ) )
                {
                    @string = @string.Substring( 1 ).Trim();
                    if ( FrequencyValue.IsCheck( @string ) )
                        Value = new FrequencyValue( @string );
                    else if ( CheckedVariableName( ref @string ) )
                        Variable = new SequenceVariable( @string, new FrequencyValue( 0 ) );
                    else
                        Type = eTypeObjectStream.Default;
                }
                else if ( FrequencyValue.IsCheck( @string ) )
                    Value = new FrequencyValue( @string );
                else if ( CheckedTableName( ref @string ) )
                    Table = new FrequencyTableSequence( this, @string );
                else
                    Type = eTypeObjectStream.Default;
            }
            else
            {
                if ( CheckedTableName( ref @string ) )
                    Table = new FrequencyTableSequence( this, @string );
                else
                    Type = eTypeObjectStream.Default;
            }
        }

        public override bool IsSignal { get { return false; } }

        public override bool IsTableShape { get { return false; } }

        protected override Value Default
        {
            get { return new FrequencyValue( 0 ); }
        }

        public override Value Value
        {
            set
            {
                base.Value = value;
                Type = Convert.ToDouble( value.Data ) == 0 ? eTypeObjectStream.Default : eTypeObjectStream.Value;
            }
        }

        public override Image Image
        {
            get
            {
                switch ( Type )
                {
                case eTypeObjectStream.Table1D2D:
                    return StreamSequence.table1D2DImage;
                case eTypeObjectStream.Table:
                    switch ( TypeCollection )
                    {
                    case eTypeObjectCollection._1D:
                        return StreamSequence.table1DImage;
                    case eTypeObjectCollection._2D:
                        return StreamSequence.table2DImage;
                    case eTypeObjectCollection._3D:
                        return StreamSequence.table3DImage;
                    case eTypeObjectCollection._4D:
                        return StreamSequence.table4DImage;
                    default:
                        return null;
                    }
                default:
                    return null;
                }
            }
        }

        public override object Clone( )
        {
            O1_ObjectSequence obj = new O1_ObjectSequence( );
            obj.CloneData( obj );
            return obj;
        }
    }
}
