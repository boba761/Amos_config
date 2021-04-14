using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NMR.Interfaces;

namespace NMR.Commands
{
    class SetObjectCellCommand : Command
    {
        public SetObjectCellCommand(Document document, IObject newObject )
            : base(document)
        {
            if ( newObject.Parent.ColumnCount == newObject.Index + 1 )
                Add( new InsertColumnCommand( document, newObject.Index ) );
            Add( new SetObjectCommand( document, newObject ) );
        }
    }
}
