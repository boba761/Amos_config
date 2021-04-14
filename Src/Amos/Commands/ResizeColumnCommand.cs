using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Amos.Controls.Sequence;

namespace Amos.Commands
{
    class ResizeColumnCommand : Command
    {
        private Column _column;
        private int _oldWidth;
        private int _newWidth;

        public ResizeColumnCommand( Document document, Column column )
            : base( document )
        {
            _column = column;
            _oldWidth = column.Width;
        }

        public override bool IsExecute { get { return _oldWidth != _newWidth && _newWidth != 0; } }

        public void Resize( int width )
        {
            _newWidth = width;
            _column.Width = width;
        }

        public override void Execute( )
        {
            base.Execute( );
            _column.Width = _newWidth;
        }

        public override void Receive( )
        {
             _column.Width = _oldWidth;
            base.Receive( );
        }
    }
}
