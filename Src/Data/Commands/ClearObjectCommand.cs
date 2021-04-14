using Tools;
using Tools.Interfaces;

namespace Data.Commands
{
    class ClearObjectCommand : Command
    {
        private IObjectStream _oldObject;
        private IObjectStream _newObject;

        public ClearObjectCommand( Document document, IStream stream, eTypeObjectCollection typeCollection, int index )
            : base( document )
        {
            _oldObject = stream[typeCollection][index];
            _newObject = stream.GetNewObject( typeCollection, index );
            _newObject.CorrectionSequence( this );
        }

        public override void Execute()
        {
            base.Execute();
            _newObject.Parent[_newObject.TypeCollection][_newObject.IndexEvent] = _newObject;
        }

        public override void Receive()
        {
            _newObject.Parent[_oldObject.TypeCollection][_oldObject.IndexEvent] = _oldObject;
            base.Receive();
        }
    }
}
