using System;
using System.Windows.Forms;
using Tools;
using Tools.Interfaces;

namespace Data.Commands
{
    class SetObjectCommand : Command
    {
        private IObjectStream _newObject;
        private IObjectStream _oldObject;

        public SetObjectCommand( Document document, IStream stream, eTypeObjectCollection typeCollection, int index, eTypeObjectStream typeObject, object data = null )
            : base (document)
        {
            _oldObject = stream[typeCollection][index];
            _newObject = stream.GetObject( typeCollection, index, typeObject, data );
            _newObject.CorrectionSequence( this );
            if ( _newObject.Type != eTypeObjectStream.Default && typeCollection == eTypeObjectCollection._1D && stream.EventCount == index + 1 )
                Add( new InsertColumnCommand( document, index + 1 ) );
        }

        public SetObjectCommand( Document document, IStream stream, eTypeObjectCollection typeCollection, int index, string text )
            : base( document )
        {
            _oldObject = stream[typeCollection][index];
            _newObject = stream.GetObject( typeCollection, index, text );
            _newObject.CorrectionSequence( this );
            if ( _newObject.Type != eTypeObjectStream.Default && typeCollection == eTypeObjectCollection._1D && stream.EventCount == index + 1 )
                Add( new InsertColumnCommand( document, index + 1 ) );
        }

        public override bool IsExecute 
        {
            get { return !( _oldObject.Type == eTypeObjectStream.Default && _newObject.Type == eTypeObjectStream.Default ); }
        }

        public override void Execute()
        {
            try
            {
                base.Execute( );
                Document.Sequence.SetObject( _newObject );
            }
            catch ( Exception ex )
            {
                MessageBox.Show( ex.Message, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Warning );
                Receive( );
            }
        }

        public override void Receive()
        {
            Document.Sequence.SetObject( _oldObject );
            base.Receive();
        }
    }
}
