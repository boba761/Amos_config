using Amos.Interfaces;
using Tools.Interfaces;

namespace Amos.Commands
{
    class ClearObjectCommand : Command
    {
        private IObjectStream _oldObject;
        private IObjectStream _newObject;

        public ClearObjectCommand( Document document, IStream element, eTypeObjectCollection typeCollection, int index )
            : base( document )
        {
            _oldObject = element[typeCollection][index];
            _newObject = element.GetNewObject( typeCollection, index );
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
