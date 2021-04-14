using System;
using System.Drawing;
using System.Text.RegularExpressions;
using Data.DataObject.Sequence.Tables;
using Calculations.Values;
using Tools.Interfaces;
using Calculations.Variables;

namespace Data.DataObject.Sequence.Objects
{
    public class SH_ObjectSequence : ObjectStream
    {
        public SH_ObjectSequence( ) { }

        public SH_ObjectSequence( IStream element, int index, eTypeObjectCollection typeCollection, eTypeObjectStream type, object data = null )
            : base( element, index, typeCollection )
         {
            Type = type;
            switch ( Type )
            {
            case eTypeObjectStream.TableShape:
                if ( data != null )
                    Table = (GradientWaveformTableSequence)data;
                else
                    Table = new GradientWaveformTableSequence( this );
                break;
            case eTypeObjectStream.Continue:
                break;
            default:
                Type = eTypeObjectStream.Default;
                break;
            }
        }

        public SH_ObjectSequence( IStream element, int index, eTypeObjectCollection typeCollection, string text)
            : base( element, index, typeCollection )
        {
            string @string = text.Trim();
            if ( @string.StartsWith( "=" ) )
            {
                @string = @string.Substring( 1 ).Trim();
                if ( DoubleValue.IsCheck( @string ) )
                    Value = new DoubleValue( @string );
                else if ( CheckedVariableName( ref @string ) )
                    Variable = new SequenceVariable( @string, new DoubleValue( 0 ), new DoubleValue( -100 ), new DoubleValue( 100 ), null );
                else
                    Type = eTypeObjectStream.Default;
            }
            else if ( DoubleValue.IsCheck( @string ) )
                Value = new DoubleValue( @string );
            else if ( CheckedTableName( ref @string ) )
                Table = new GradientWaveformTableSequence( this, @string );
            else
                Type = eTypeObjectStream.Default;
        }

        public override bool IsEdit { get { return Type != eTypeObjectStream.Continue; } }

        public override bool IsSignal { get { return false; } }

        public override bool IsTableShape { get { return base.IsTableShape; } }

        public override bool IsTable1D2D { get { return false; } }

        public override bool IsTable1D { get { return false; } }

        public override bool IsTable2D { get { return false; } }

        public override bool IsTable3D { get { return false; } }

        public override bool IsTable4D { get { return false; } }

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
                if ( rez < -100 ) 
                    base.Value.Data = -100;
                else if ( rez > 100 )
                    base.Value.Data = 100;
                Type = Convert.ToDouble( base.Value.Data ) == 0 ? eTypeObjectStream.Default : eTypeObjectStream.Value;
            }
        }

        public override TableSequence Table
        {
            set
            {
                base.Table = value;
                Type = eTypeObjectStream.TableShape;
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
                case eTypeObjectStream.Continue:
                    return StreamSequence.continueImage;
                case eTypeObjectStream.TableShape:
                    return StreamSequence.tableShapeImage;
                default:
                    return null;
                }
            }
        }

        public override object Clone( )
        {
            SH_ObjectSequence obj = new SH_ObjectSequence( );
            obj.CloneData( obj );
            return obj;
        }
    }
}
