using System;
using System.Drawing;
using System.Text.RegularExpressions;
using Data.DataObject.Sequence.Tables;
using Calculations.Values;
using Tools.Interfaces;
using Calculations.Variables;

namespace Data.DataObject.Sequence.Objects
{
    public class Loop_ObjectSequence : ObjectStream
    {
        public Loop_ObjectSequence( ) { }

        public Loop_ObjectSequence( IStream element, int index, eTypeObjectCollection typeCollection, eTypeObjectStream type, object data = null )
            : base( element, index, typeCollection )
        {
            Type = type;
            if ( typeCollection == eTypeObjectCollection._1D )
            {
                switch ( Type )
                {
                
                case eTypeObjectStream.Table:
                    Table = new LoopTableSequence( this);
                    break;
                default:
                    Type = eTypeObjectStream.Default;
                    break;
                }
            }
            else if ( Type == eTypeObjectStream.Table )
                Table = new LoopTableSequence( this );
            else
                Type = eTypeObjectStream.Default;
        }

        public Loop_ObjectSequence( IStream element, int index, eTypeObjectCollection typeCollection, string text )
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
                        Variable = new SequenceVariable( @string, new IntegerValue( 1 ) );
                    else
                        Type = eTypeObjectStream.Default;
                }
                else if ( IntegerValue.IsCheck( @string ) )
                    Value = new IntegerValue( @string );
                else if ( CheckedTableName( ref @string ) )
                    Table = new LoopTableSequence( this, @string );
                else
                    Type = eTypeObjectStream.Default;
            }
            else
            {
                if ( CheckedTableName( ref @string ) )
                    Table = new LoopTableSequence( this, @string );
                else
                    Type = eTypeObjectStream.Default;
            }
        }

        public override bool IsSignal { get { return false; } }

        public override bool IsLoopStart { get { return true; } }

        public override bool IsLoopEnd { get { return true; } }

        public override bool IsTable1D2D { get { return false; } }

        public override bool IsTableShape { get { return false; } }

        public override string DrawText
        {
            get
            {
                if ( Type == eTypeObjectStream.Value && Math.Abs(Convert.ToInt32(Value.Data)) == 1)
                    return null;
                return base.DrawText;
            }
        }

        protected override Value Default
        {
            get { return new IntegerValue( 0 ); }
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
                case eTypeObjectStream.Value:
                    if (Convert.ToDouble(Value.Data) > 0)
                        return StreamSequence.loopStartImage;
                    else if ( Convert.ToDouble( Value.Data ) < 0 )
                        return StreamSequence.loopEndImage;
                    else
                        return null;
                case eTypeObjectStream.Variable:
                    if ( Convert.ToDouble( Variable.Value.Data ) > 0 )
                        return StreamSequence.loopStartImage;
                    else if ( Convert.ToDouble( Variable.Value.Data ) < 0 )
                        return StreamSequence.loopEndImage;
                    else
                        return null;
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
            Loop_ObjectSequence obj = new Loop_ObjectSequence( );
            obj.CloneData( obj );
            return obj;
        }
    }
}
