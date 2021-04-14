using System;
using System.Drawing;
using System.Text.RegularExpressions;
using Amos.Data.Sequence.Tables;
using Calculations.Values;
using Calculations.Variables;
using Amos.Interfaces;
using Amos.Commands;
using Tools.Interfaces;

namespace Amos.Data.Sequence.Objects
{
    public class PH_ObjectSequence : ObjectSequence
    {
        public PH_ObjectSequence( ) { }

        public PH_ObjectSequence( IStream element, int index, eTypeObjectCollection typeCollection, eTypeObjectStream type, object data = null )
            : base( element, index, typeCollection )
        {
            Type = type;
            if ( typeCollection == eTypeObjectCollection._1D )
            {
                switch ( Type )
                {
                case eTypeObjectStream.Table:
                case eTypeObjectStream.Table1D2D:
                    Table = new PhaseTableSequence( this );
                    break;
                case eTypeObjectStream.TableShape:
                    if ( data != null )
                        Table = (PhaseTableSequence)data;
                    else
                        Table = new PhaseTableSequence( this );
                    break;
                case eTypeObjectStream.Continue:
                    break;
                case eTypeObjectStream.AsynchronousStart:
                    Asynchronus = new AsynchronusObject( (string)data );
                    break;
                case eTypeObjectStream.AsynchronousStop:
                case eTypeObjectStream.Asynchronous:
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

        public PH_ObjectSequence( IStream element, int index, eTypeObjectCollection typeCollection, string text )
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
                        Variable = new SequenceVariable( @string, new IntegerValue( 0 ), new IntegerValue( 0 ), new IntegerValue( 3 ), null );
                    else
                        Type = eTypeObjectStream.Default;
                }
                else if ( IntegerValue.IsCheck( @string ) )
                    Value = new IntegerValue( @string );
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

        public override bool IsXY 
        { 
            get 
            { 
                return true; 
            } 
        }

        public override bool IsAsynchronousStart { get { return Parent.Group == 1; } }

        public override bool IsAsynchronousStop
        {
            get 
            {
                if ( Parent.Group == 1 )
                {
                    int pos = IndexEvent -1;
                    while ( pos >= 0 && Parent[TypeCollection][pos].Type != eTypeObjectStream.AsynchronousStart &&
                        Parent[TypeCollection][pos].Type != eTypeObjectStream.Asynchronous )
                        pos --;
                    return pos >= 0 && Old.Type != eTypeObjectStream.AsynchronousStop;
                }
                return false;
            }
        }

        public override bool IsTable1D2D
        {
            get
            {
                return base.IsTable1D2D && Parent.IconType == sStreamData.P2IconType;
            }
        }

        public override bool IsTableShape
        {
            get
            {
                return base.IsTableShape && Parent.IconType == sStreamData.P2IconType;
            }
        }

        public override string DrawText
        {
            get
            {
                if ( Type == eTypeObjectStream.Value )
                    return null;
                return base.DrawText;
            }
        }

        protected override Value Default
        {
            get { return new IntegerValue( -1 ); }
        }

        public override Value Value
        {
            get 
            {
                if ( Type == eTypeObjectStream.AsynchronousStop || Type == eTypeObjectStream.Asynchronous )
                    return null;
                return base.Value;
            }
            set
            {
                base.Value = value;
                if ( Convert.ToInt32( base.Value.Data ) >= 0 )
                    Type =  eTypeObjectStream.Value;
                if ( Type == eTypeObjectStream.Value )
                    base.Value.Data = Convert.ToInt32( base.Value.Data ) % 4;
            }
        }

        public override Image Image
        {
            get
            {
                switch ( Type )
                {
                case eTypeObjectStream.Value:
                    switch ( Convert.ToInt32( Value.Data ) )
                    {
                    case 0:
                        return StreamSequence.plusXImage;
                    case 1:
                        return StreamSequence.plusYImage;
                    case 2:
                        return StreamSequence.minusXImage;
                    case 3:
                        return StreamSequence.minusYImage;
                    default:
                        return null;
                    }
                case eTypeObjectStream.AsynchronousStart:
                    return StreamSequence.asynchronousStartImage;
                case eTypeObjectStream.Asynchronous:
                    return StreamSequence.asynchronousImage;
                case eTypeObjectStream.AsynchronousStop:
                    return StreamSequence.asynchronousStopImage;
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

        protected override void LoadValue( string text )
        {
            if ( text == "@AI" )
                Type = eTypeObjectStream.Asynchronous;
            else if ( text == "@AE" )
                Type = eTypeObjectStream.AsynchronousStop;
            else
                base.LoadValue( text );
        }

        public override void CorrectionSequence( ICommandList command )
        {
            base.CorrectionSequence( command );
            if ( Type == eTypeObjectStream.AsynchronousStop )
            {
                for ( int i = IndexEvent - 1; i > 0 && Parent[TypeCollection][i].Type != eTypeObjectStream.AsynchronousStart && 
                    Parent[TypeCollection][i].Type != eTypeObjectStream.Asynchronous; i-- )
                {
                    Parent[TypeCollection][i].IsChange = true;
                    command.Add( new SetObjectCommand( ( (Command)command ).Document, Parent, TypeCollection, i, eTypeObjectStream.Asynchronous ) );
                }
            }
            if ( Next != null && Next.IsChange == false )
            {
                if ( Old.Type == eTypeObjectStream.Asynchronous && Type != eTypeObjectStream.AsynchronousStart )
                {
                    Next.IsChange = true;
                    command.Add( new ClearObjectCommand( ( (Command)command ).Document, Parent, TypeCollection, Next.IndexEvent ) );
                }
                else if ( Old.Type == eTypeObjectStream.AsynchronousStart && Type != eTypeObjectStream.AsynchronousStart &&
                    ( Next.Type == eTypeObjectStream.Asynchronous || Next.Type == eTypeObjectStream.AsynchronousStop ) )
                {
                    Next.IsChange = true;
                    command.Add( new ClearObjectCommand( ( (Command)command ).Document, Parent, TypeCollection, Next.IndexEvent ) );
                }
            }
            if ( Prev != null && Prev.IsChange == false )
            {
                if ( (Old.Type == eTypeObjectStream.AsynchronousStop || Old.Type == eTypeObjectStream.Asynchronous) && 
                    Prev.Type == eTypeObjectStream.Asynchronous && Type != eTypeObjectStream.Asynchronous && Type != eTypeObjectStream.AsynchronousStop )
                {
                    Prev.IsChange = true;
                    command.Add( new SetObjectCommand( ( (Command)command ).Document, Parent, TypeCollection, Prev.IndexEvent, eTypeObjectStream.AsynchronousStop ) );
                }
            }
        }

        public override object Clone( )
        {
            PH_ObjectSequence obj = new PH_ObjectSequence( );
            obj.CloneData( obj );
            return obj;
        }
    }
}
