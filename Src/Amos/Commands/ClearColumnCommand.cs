using Amos.Data.Sequence;
using Tools.Interfaces;

namespace Amos.Commands
{
    class ClearColumnCommand: Command
    {
        private int _indexColumn;

        public ClearColumnCommand( Document document, int indexColumn )
            : base( document )
        {
            _indexColumn = indexColumn;
            foreach ( StreamSequence element in document.Sequence.Streams )
            {
                foreach ( ObjectCollection collection in element.ObjectCollection )
                {
                    if ( collection[_indexColumn].Type != eTypeObjectStream.Default )
                        Add( new ClearObjectCommand( document, element, collection.TypeCollection, _indexColumn ) );
                }
            }
        }

        public override void Execute()
        {
            base.Execute();
        }

        public override void Receive()
        {
            base.Receive();
        }
    }
}
