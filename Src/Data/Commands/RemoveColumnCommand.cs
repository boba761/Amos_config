using Tools;

namespace Data.Commands
{
    class RemoveColumnCommand : Command
    {
        private int _indexColumn;
        private Document _document;

        public RemoveColumnCommand( Document document, int indexColumn )
            : base( document )
        {
            _document = document;
            _indexColumn = indexColumn;
            Add( new ClearColumnCommand( document, _indexColumn ) );
        }

        public override void Execute()
        {
            base.Execute();
            _document.Sequence.RemoveEvent( _indexColumn );
        }

        public override void Receive()
        {
            _document.Sequence.InsertEvent( _indexColumn );
            base.Receive();
        }
    }
}
