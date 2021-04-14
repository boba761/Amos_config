using System;
using System.Drawing;
using Amos.Commands;
using Amos.Data.Sequence.Tables;
using Calculations.Values;
using Tools.Interfaces;

namespace Amos.Data.Sequence.Objects
{
    public class ACQ_ObjectSequence : ObjectSequence
    {
        public ACQ_ObjectSequence( ) { }

        public ACQ_ObjectSequence( IStream element, int index, eTypeObjectCollection typeCollection, eTypeObjectStream type, object data = null )
            : base( element, index, typeCollection )
        {
            Type = type;
            if ( typeCollection == eTypeObjectCollection._1D )
            {
                switch ( Type )
                {
                case eTypeObjectStream.Acquisition:
                    if ( data != null )
                        Acquisition = (AcquisitionObject)data;
                    else
                        Acquisition = new AcquisitionObject( this );
                    break;
                case eTypeObjectStream.Continue:
                    break;
                case eTypeObjectStream.Table:
                    Table = new AcquisitionTableSequence( this );
                    break;
                default:
                    Type = eTypeObjectStream.Default;
                    break;
                }
            }
            else if ( Type == eTypeObjectStream.Table )
                Table = new AcquisitionTableSequence( this );
            else
                Type = eTypeObjectStream.Default;
        }

        public ACQ_ObjectSequence( IStream element, int index, eTypeObjectCollection typeCollection, string text )
            : base( element, index, typeCollection )
        {
            string @string = text.Trim();
            if ( CheckedTableName( ref @string ) )
                Table = new AcquisitionTableSequence( this, @string );
            else
                Type = eTypeObjectStream.Default;
        }

        public override bool IsEdit { get { return Type == eTypeObjectStream.Table; } }

        public override bool IsSignal { get { return false; } }

        public override bool IsAcquisition
        {
            get
            {
                return ( Prev != null && Prev.Type != eTypeObjectStream.Acquisition && Prev.Type != eTypeObjectStream.Continue && 
                    Old.Type != eTypeObjectStream.Acquisition ) || ( IndexEvent == 0 && Old.Type != eTypeObjectStream.Acquisition );
            }
        }

        public override bool IsContinue
        {
            get
            {
                return Prev != null && Old.Type != eTypeObjectStream.Continue && (Prev.Type == eTypeObjectStream.Acquisition || Prev.Type == eTypeObjectStream.Continue);
            }
        }

        public override bool IsTable1D2D { get { return false; } }

        public override bool IsTableShape { get { return false; } }

        public override bool IsProterty { get { return base.IsProterty || Type == eTypeObjectStream.Acquisition || Type == eTypeObjectStream.Continue; } }
       
        public override string DrawText
        {
            get { return null; }
        }

        protected override Value Default
        {
            get { return new IntegerValue(0); }
        }

        public override Value Value
        {
            set
            {
                base.Value = value;
                Type = Convert.ToInt32( base.Value.Data ) == 0 ? eTypeObjectStream.Default : eTypeObjectStream.Value;
            }
        }

        public override Image Image
        {
            get
            {
                switch ( Type )
                {
                case eTypeObjectStream.Acquisition:
                    return StreamSequence.acquisitionImage;
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

        protected override void LoadValue( string text )
        {
            int result = 0;
            int.TryParse( text, out result );
            if ( result > 0 )
                Type = eTypeObjectStream.Continue;
            else
                base.LoadValue( text );
        }

        public override void CorrectionSequence( ICommandList command )
        {
            Old.IsChange = true;
            if ( Next != null && Next.IsChange == false )
            {
                if ( Old.Type == eTypeObjectStream.Continue && Next.Type == eTypeObjectStream.Continue && Type != eTypeObjectStream.Acquisition )
                {
                    Next.IsChange = true;
                    command.Add( new ClearObjectCommand( ( (Command)command ).Document, Parent, TypeCollection, Next.IndexEvent ) );
                }
                else if ( Old.Type == eTypeObjectStream.Acquisition && Next.Type == eTypeObjectStream.Continue )
                {
                    Next.IsChange = true;
                    command.Add( new SetObjectCommand( ( (Command)command ).Document, Parent, TypeCollection, Next.IndexEvent, eTypeObjectStream.Acquisition, ( (ObjectSequence)Old ).Acquisition ) );
                }
            }
        }

        public override object Clone( )
        {
            ACQ_ObjectSequence obj = new ACQ_ObjectSequence( );
            obj.CloneData( obj );
            return obj;
        }
    }
}
