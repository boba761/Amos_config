using System;
using System.Drawing;
using System.Text.RegularExpressions;
using Data.DataObject.Sequence.Tables;
using Calculations.Values;
using Tools.Interfaces;
using Calculations.Variables;

namespace Data.DataObject.Sequence.Objects
{
    public class RM_ObjectSequence : ObjectStream
    {
        public RM_ObjectSequence( ) { }

        public RM_ObjectSequence( IStream element, int index, eTypeObjectCollection typeCollection, eTypeObjectStream type, object data = null )
            : base( element, index, typeCollection )
        {
            Type = type;
            if ( typeCollection == eTypeObjectCollection._1D )
            {
                switch ( Type )
                {
               
                case eTypeObjectStream.Table:
                case eTypeObjectStream.Table1D2D:
                    Table = new ModulationTableSequence( this );
                    break;
                case eTypeObjectStream.TableShape:
                    if ( data != null )
                        Table = (ModulationTableSequence)data;
                    else
                        Table = new ModulationTableSequence( this );
                    break;
                case eTypeObjectStream.Continue:
                    break;
                default:
                    Type = eTypeObjectStream.Default;
                    break;
                }
            }
            else if ( Type == eTypeObjectStream.Table )
                Table = new ModulationTableSequence( this );
            else
                Type = eTypeObjectStream.Default;
        }

        public RM_ObjectSequence( IStream element, int index, eTypeObjectCollection typeCollection, string text )
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
                        Variable = new SequenceVariable( @string, new DoubleValue( 0 ), new DoubleValue( 0 ), new DoubleValue( 100 ), null );
                    else
                        Type = eTypeObjectStream.Default;
                }
                else if ( DoubleValue.IsCheck( @string ) )
                    Value = new DoubleValue( @string );
                else if ( CheckedTableName( ref @string ) )
                    Table = new ModulationTableSequence( this, @string );
                else
                    Type = eTypeObjectStream.Default;
            }
            else
            {
                if ( CheckedTableName( ref @string ) )
                    Table = new ModulationTableSequence( this, @string );
                else
                    Type = eTypeObjectStream.Default;
            }
        }

        public override bool IsSignal { get { return false; } }

        public override bool IsTable1D2D
        {
            get
            {
                return base.IsTable1D2D && Parent.IconType == sStreamData.R2IconType;
            }
        }

        public override bool IsTableShape
        {
            get
            {
                return base.IsTableShape && Parent.IconType == sStreamData.R2IconType;
            }
        }

        public override string DrawText
        {
            get
            {
                if ( Type == eTypeObjectStream.Value && Math.Abs( Convert.ToInt32( Value.Data ) ) == 1 )
                    return null;
                return base.DrawText;
            }
        }

        protected override Value Default
        {
            get { return new DoubleValue( 0 ); }
        }

        public override Value Value
        {
            set
            {
                base.Value = value;
                double rez = Convert.ToDouble( base.Value.Data );
                if ( rez < 0 )
                    base.Value.Data = 0;
                else if ( rez > 100 )
                    base.Value.Data = 100;
                Type = Convert.ToDouble( base.Value.Data ) == 0 ? eTypeObjectStream.Default : eTypeObjectStream.Value;
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
                case eTypeObjectStream.Table1D2D:
                    return StreamSequence.table1D2DImage;
                case eTypeObjectStream.TableShape:
                    return StreamSequence.tableShapeImage;
                case eTypeObjectStream.Continue:
                    return StreamSequence.continueImage;
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
            RM_ObjectSequence obj = new RM_ObjectSequence( );
            obj.CloneData( obj );
            return obj;
        }
    }
}
