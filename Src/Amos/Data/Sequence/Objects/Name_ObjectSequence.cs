using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Amos.Interfaces;
using Calculations.Values;
using Tools.Interfaces;

namespace Amos.Data.Sequence.Objects
{
    public class Name_ObjectSequence : ObjectSequence
    {
        public Name_ObjectSequence( ) { }

        public Name_ObjectSequence( IStream element, int index, eTypeObjectCollection typeCollection, eTypeObjectStream type, object data = null )
            : base( element, index, typeCollection )
        {
            Type = eTypeObjectStream.Default;
        }

        public Name_ObjectSequence( IStream element, int index, eTypeObjectCollection typeCollection, string text )
            : base( element, index, typeCollection )
        {
            if ( string.IsNullOrWhiteSpace( text ) )
                Type = eTypeObjectStream.Default;
            else
                Value = new StringValue( text );
        }

        public override bool IsSignal { get { return false; } }

        public override bool IsTable1D2D { get { return false; } }

        public override bool IsTable1D { get { return false; } }

        public override bool IsTable2D { get { return false; } }

        public override bool IsTable3D { get { return false; } }

        public override bool IsTable4D { get { return false; } }

        public override bool IsTableShape { get { return false; } }

        public override bool IsProterty { get { return false; } }

        protected override Value Default
        {
            get { return new StringValue( "" ); }
        }

        public override Value Value
        {
            set
            {
                base.Value = value;
                Type = value.ToString() == "" ? eTypeObjectStream.Default : eTypeObjectStream.Value;
            }
        }

        public override object Clone( )
        {
            Name_ObjectSequence obj = new Name_ObjectSequence( );
            obj.CloneData( obj );
            return obj;
        }
    }
}
