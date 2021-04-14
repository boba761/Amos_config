using System.Drawing;
using System.Text.RegularExpressions;
using Data.DataObject.Sequence.Tables;
using Calculations.Values;
using Tools.Interfaces;
using Calculations.Variables;

namespace Data.DataObject.Sequence.Objects
{
    public class PS_ObjectSequence : ObjectStream
    {
        public PS_ObjectSequence( ) { }

        public PS_ObjectSequence( IStream element, int index, eTypeObjectCollection typeCollection, eTypeObjectStream type, object data = null )
            : base( element, index, typeCollection )
        {
            Type = type;
            if ( typeCollection == eTypeObjectCollection._1D )
            {
                switch ( Type )
                {
                case eTypeObjectStream.Table:
                    Table = new PhaseTableSequence( this );
                    break;
                default:
                    Type = eTypeObjectStream.Default;
                    break;
                }
            }
            else if ( Type == eTypeObjectStream.Table )
                Table = new PhaseTableSequence( this );
            else
                Type = eTypeObjectStream.Default;
        }

        public PS_ObjectSequence( IStream element, int index, eTypeObjectCollection typeCollection, string text )
            : base( element, index, typeCollection )
        {
            string @string = text.Trim();
            if ( typeCollection == eTypeObjectCollection._1D )
            {
                if ( @string.StartsWith( "=" ) )
                {
                    @string = @string.Substring( 1 ).Trim();
                    if ( DoubleValue.IsCheck( @string ) )
                        Value = new DoubleValue( @string );
                    else if ( CheckedVariableName( ref @string ) )
                        Variable = new SequenceVariable( @string, new DoubleValue( 0 ) );
                    else
                        Type = eTypeObjectStream.Default;
                }
                else if ( DoubleValue.IsCheck( @string ) )
                    Value = new DoubleValue( @string );
                else if ( CheckedTableName( ref @string ) )
                    Table = new PhaseTableSequence( this, @string );
                else
                    Type = eTypeObjectStream.Default;
            }
            else
            {
                if ( CheckedTableName( ref @string ) )
                    Table = new PhaseTableSequence( this, @string );
                else
                    Type = eTypeObjectStream.Default;
            }
        }

        public override bool IsSignal { get { return false; } }

        public override bool IsTable1D2D { get { return false; } }

        public override bool IsTableShape { get { return false; } }

        protected override Value Default
        {
            get { return new DoubleValue( 0 ); }
        }

        public override Value Value
        {
            set
            {
                base.Value = value;
                Type = value.ToString() == "0" ? eTypeObjectStream.Default : eTypeObjectStream.Value;
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
            PS_ObjectSequence obj = new PS_ObjectSequence( );
            obj.CloneData( obj );
            return obj;
        }
    }
}
