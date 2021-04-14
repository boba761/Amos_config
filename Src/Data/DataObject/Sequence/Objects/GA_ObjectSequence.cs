using System.Drawing;
using System.Text.RegularExpressions;
using Data.DataObject.Sequence.Tables;
using Calculations.Values;
using Tools.Interfaces;

namespace Data.DataObject.Sequence.Objects
{
    public class GA_ObjectSequence : ObjectStream
    {
        public GA_ObjectSequence( ) { }

        public GA_ObjectSequence( IStream element, int index, eTypeObjectCollection typeCollection, eTypeObjectStream type, object data = null )
            : base( element, index, typeCollection )
        {
            Type = type;
            if ( typeCollection == eTypeObjectCollection._1D )
            {
                switch ( Type )
                {
                case eTypeObjectStream.Table:
                case eTypeObjectStream.Table1D2D:
                    Table = new RotationAngleTableSequence( this );
                    break;
                default:
                    Type = eTypeObjectStream.Default;
                    break;
                }
            }
            else if ( Type == eTypeObjectStream.Table )
                Table = new RotationAngleTableSequence( this );
            else
                Type = eTypeObjectStream.Default;
        }

        public GA_ObjectSequence( IStream element, int index, eTypeObjectCollection typeCollection, string text )
            : base( element, index, typeCollection )
        {
            string @string = text.Trim();
            if ( typeCollection == eTypeObjectCollection._1D )
            {
                if ( CheckedTableName( ref @string ) )
                    Table = new RotationAngleTableSequence( this, @string );
                else
                    Type = eTypeObjectStream.Default;
            }
            else
            {
                if ( CheckedTableName( ref @string ) )
                    Table = new RotationAngleTableSequence( this, @string );
                else
                    Type = eTypeObjectStream.Default;
            }
        }

        public override bool IsEdit { get { return Type == eTypeObjectStream.Table || Type == eTypeObjectStream.Table1D2D; } }

        public override bool IsSignal { get { return false; } }

        public override bool IsTableShape { get { return false; } }

        protected override Value Default
        {
            get { return new StringValue( "" ); }
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
            GA_ObjectSequence obj = new GA_ObjectSequence( );
            obj.CloneData( obj );
            return obj;
        }
    }
}
