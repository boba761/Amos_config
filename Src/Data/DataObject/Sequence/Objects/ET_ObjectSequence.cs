using System;
using System.Drawing;
using Calculations.Values;
using Tools.Interfaces;

namespace Data.DataObject.Sequence.Objects
{
    public class ET_ObjectSequence : ObjectStream
    {
        public ET_ObjectSequence( ) { }

        public ET_ObjectSequence( IStream element, int index, eTypeObjectCollection typeCollection, eTypeObjectStream type, object data = null )
            : base( element, index, typeCollection )
        {
            Type = eTypeObjectStream.Default;
        }

        public ET_ObjectSequence( IStream element, int index, eTypeObjectCollection typeCollection, string text )
            : base( element, index, typeCollection )
        {
            Value = new BooleanValue( true );
        }

        public override bool IsEdit { get { return false; } }

        public override bool IsTable1D2D { get { return false; } }

        public override bool IsTable1D { get { return false; } }

        public override bool IsTable2D { get { return false; } }

        public override bool IsTable3D { get { return false; } }

        public override bool IsTable4D { get { return false; } }

        public override bool IsTableShape { get { return false; } }

        public override bool IsProterty { get { return false; } }

        public override string DrawText { get { return null; } }

        protected override Value Default
        {
            get { return new BooleanValue( false ); }
        }

        public override Value Value
        {
            set
            {
                base.Value = value;
                Type = Convert.ToBoolean( base.Value.Data ) ? eTypeObjectStream.Value : eTypeObjectStream.Default;
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
                default:
                    return null;
                }
            }
        }

        public override object Clone( )
        {
            ET_ObjectSequence obj = new ET_ObjectSequence( );
            obj.CloneData( obj );
            return obj;
        }
    }
}
