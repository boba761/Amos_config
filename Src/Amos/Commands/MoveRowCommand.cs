using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Amos.Controls.Sequence;

namespace Amos.Commands
{
    class MoveRowCommand : Command
    {
        private Row _from;
        private Row _to;

        public MoveRowCommand( Document document, Row from, Row to = null)
            : base( document )
        {
            _from = from;
            _to = to; 
        }

        public void MoveTo( Row to )
        {
            Add( new MoveRowCommand( Document, _from, to ) );
            Program.MainForm.SequenceView.SequenceControl.MoveRow( _from, to );
        }

        public override void Execute( )
        {
            base.Execute( );
            if ( _to != null )
                Program.MainForm.SequenceView.SequenceControl.MoveRow( _from, _to );
        }

        public override void Receive( )
        {
            if ( _to != null )
                Program.MainForm.SequenceView.SequenceControl.MoveRow( _to, _from );
            base.Receive( );
        }
    }
}
