using Amos.Data.Sequence;
using Tools.Interfaces;

namespace Amos.Commands
{
    class RenameElementCommand : Command
    {
        private StreamSequence _element;
        private string _oldName;
        private string _newName;

        public RenameElementCommand( Document document, IStream element, string name )
            : base ( document)
        {
            _element = (StreamSequence)element;
            _oldName = _element.Name;
            _newName = name;
        }

        public override void Execute( )
        {
            base.Execute( );
            _element.Name = _newName;
        }

        public override void Receive( )
        {
            _element.Name = _oldName;
            base.Receive( );
        }
    }
}
