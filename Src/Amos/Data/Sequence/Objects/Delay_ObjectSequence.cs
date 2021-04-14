using System;
using System.Drawing;
using Amos.Data.Sequence.Tables;
using Calculations.Values;
using Calculations.Variables;
using Tools.Interfaces;

namespace Amos.Data.Sequence.Objects
{
    public class Delay_ObjectSequence : ObjectSequence
    {
        public Delay_ObjectSequence( ) 
        {
        }

        public Delay_ObjectSequence( IStream element, int index, eTypeObjectCollection typeCollection, eTypeObjectStream type, object data = null )
            : base( element, index, typeCollection )
        {
            Type = type;
            if ( typeCollection == eTypeObjectCollection._1D )
            {
                switch ( Type )
                {
                case eTypeObjectStream.Table:
                case eTypeObjectStream.Table1D2D:
                    Table = new DelayTableSequence( this );
                    break;
                default:
                    Value = Default;
                    break;
                }
            }
            else if ( Type == eTypeObjectStream.Table )
                Table = new DelayTableSequence( this );
            else
                Type = eTypeObjectStream.Default;
        }

        public Delay_ObjectSequence( IStream element, int index, eTypeObjectCollection typeCollection, string text )
            : base( element, index, typeCollection )
        {
            string @string = text.Trim();
            if ( typeCollection == eTypeObjectCollection._1D )
            {
                if ( @string.StartsWith( "=" ) )
                {
                    @string = @string.Substring( 1 ).Trim();
                    if ( TimeValue.IsCheck( @string ) )
                        Value = new TimeValue( @string );
                    else if ( CheckedVariableName( ref @string ) )
                        Variable = new SequenceVariable( @string, new TimeValue( "1u" ), new TimeValue( "100n" ), null, null );
                    else
                        Value = Default;
                }
                else if ( TimeValue.IsCheck( @string ) )
                    Value = new TimeValue( @string );
                else if ( CheckedTableName( ref @string ) )
                    Table = new DelayTableSequence(this, @string );
                else
                    Value = Default;
            }
            else
            {
                if ( CheckedTableName( ref @string ) )
                    Table = new DelayTableSequence( this, @string );
                else
                    Type = eTypeObjectStream.Default;
            }
        }

        public override bool IsSignal { get { return false; } }

        public override bool IsTableShape { get { return false; } }

        protected override Value Default
        {
            get { return new TimeValue( "1u" ); }
        }

        public override Value Value
        {
            set
            {
                base.Value = value;
                if ( Convert.ToDouble( base.Value.Data ) < 0.0000001 )
                    base.Value.Data = 0.0000001;
                if ( TypeCollection != eTypeObjectCollection._1D )
                    Type = eTypeObjectStream.Default;
            }
        }

        public override Image Image
        {
            get
            {
                switch (Type)
                {
                case eTypeObjectStream.Table1D2D:
                    return StreamSequence.table1D2DImage;
                case eTypeObjectStream.Table:
                    switch (TypeCollection)
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
            Delay_ObjectSequence obj = new Delay_ObjectSequence( );
            obj.CloneData( obj );
            return obj;
        }
    }
}
