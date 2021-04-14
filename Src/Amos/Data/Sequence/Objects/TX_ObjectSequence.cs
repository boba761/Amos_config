using System;
using System.Drawing;
using Amos.Interfaces;
using Calculations.Values;
using Calculations.Variables;
using Tools.Interfaces;

namespace Amos.Data.Sequence.Objects
{
    public class TX_ObjectSequence : ObjectSequence
    {
        public TX_ObjectSequence( ) { }

        public TX_ObjectSequence( IStream element, int index, eTypeObjectCollection typeCollection, eTypeObjectStream type, object data = null )
            : base( element, index, typeCollection )
        {
            Type = type;
            if ( typeCollection == eTypeObjectCollection._1D )
            {
                switch ( Type )
                {
                case eTypeObjectStream.Table:
                    Table = new TableSequence( this );
                    break;
                default:
                    Type = eTypeObjectStream.Default;
                    break;
                }
            }
            else if ( Type == eTypeObjectStream.Table )
                Table = new TableSequence( this );
            else
                Type = eTypeObjectStream.Default;
        }

        public TX_ObjectSequence( IStream element, int index, eTypeObjectCollection typeCollection, string text )
            : base( element, index, typeCollection )
        {
            string @string = text.Trim();
            if ( typeCollection == eTypeObjectCollection._1D )
            {
                if ( @string.StartsWith( "=" ) )
                {
                    @string = @string.Substring( 1 ).Trim();
                    if ( IntegerValue.IsCheck( @string ) )
                        Value = new IntegerValue( @string );
                    else if ( CheckedVariableName( ref @string ) )
                        Variable = new SequenceVariable( @string, new IntegerValue( 0 ), new IntegerValue( 0 ), new IntegerValue( 1 ), null );
                    else
                        Type = eTypeObjectStream.Default;
                }
                else if ( IntegerValue.IsCheck( @string ) )
                    Value = new IntegerValue( @string );
                else if ( CheckedTableName( ref @string ) )
                    Table = new TableSequence(  this, @string );
                else
                    Type = eTypeObjectStream.Default;
            }
            else
            {
                if ( CheckedTableName( ref @string ) )
                    Table = new TableSequence(  this, @string );
                else
                    Type = eTypeObjectStream.Default;
            }
        }

        public override bool IsSignal { get { return true; } }

        public override bool IsTable1D2D { get { return false; } }

        public override bool IsTableShape { get { return false; } }

        protected override Value Default
        {
            get { return new IntegerValue( 0 ); }
        }

        public override Value Value
        {
            set
            {
                base.Value = value;
                if ( Convert.ToInt32( value.Data ) == 0 )
                {
                    base.Value.Data = 0;
                    Type = eTypeObjectStream.Default;
                }
                else
                {
                    base.Value.Data = 1;
                    Type = eTypeObjectStream.Value;
                }
            }
        }

        public override Image Image
        {
            get
            {
                switch ( Type )
                {
                case eTypeObjectStream.Value:
                    return StreamSequence.signalImage;
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
            TX_ObjectSequence obj = new TX_ObjectSequence( );
            obj.CloneData( obj );
            return obj;
        }
    }
}
