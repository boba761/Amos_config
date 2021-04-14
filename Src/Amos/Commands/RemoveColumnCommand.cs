using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Amos.Data.Sequence;
using Amos.Interfaces;

namespace Amos.Commands
{
    class RemoveColumnCommand : Command
    {
        private int _indexColumn;

        public RemoveColumnCommand( Document document, int indexColumn )
            : base( document )
        {
            _indexColumn = indexColumn;
            Add( new ClearColumnCommand( document, _indexColumn ) );
        }

        public override void Execute()
        {
            base.Execute();
            Document.Sequence.RemoveEvent( _indexColumn );
        }

        public override void Receive()
        {
            Document.Sequence.InsertEvent( _indexColumn );
            base.Receive();
        }
    }
}
