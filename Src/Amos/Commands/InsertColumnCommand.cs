using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Amos.Commands
{
    class InsertColumnCommand : Command
    {
        private int _indexColumn;

        public InsertColumnCommand( Document document, int indexColumn )
            : base( document )
        {
            _indexColumn = indexColumn;
        }

        public override void Execute()
        {
            base.Execute();
            Document.Sequence.InsertEvent( _indexColumn );
        }

        public override void Receive()
        {
            Document.Sequence.RemoveEvent( _indexColumn );
            base.Receive();
        }
    }
}
