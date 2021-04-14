using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Amos.Data;
using System.Drawing.Drawing2D;

namespace Amos.Controls.Sequence
{
    public class Header
    {
        private SequenceControl _control;
        private int _showColnum;
        private Size _size;
        private Brush _brush;

        public Header()
        {
            IndexColnum = 0;
            _size = new Size();
            _brush = new LinearGradientBrush( new Rectangle( 0, SequencePanel.heightRow * 2, 1, SequencePanel.heightDelimeter ),
                Color.FromArgb( 0xF9, 0xFC, 0xFD ), Color.FromArgb( 0xD3, 0xDC, 0xE9 ), LinearGradientMode.Vertical );
        }

        public void Initialize( SequenceControl control )
        {
            _control = control;
            _showColnum = _control.Columns.Count;
        }

        public int IndexColnum { get; private set; }

        public int ShowColnum { get { return _showColnum; } }

        public Size Size { get { return _size; } }

        public void Draw( Graphics graphics, Dictionary<Glyph, object> glyphHash )
        {
            if ( _control == null || _control.Rows.Count == 0 )
                return;

            _control.Columns[0].Draw( graphics, new PointF( 0, 0 ) );

            Column column;
            int x = _control.Columns[0].Width;
            for ( int i = IndexColnum + 1, count = 0; i < _control.Columns.Count; i++ )
            {
                Glyph glyph = _control.Rows[0][i];
                glyphHash.Add( glyph, glyph.Draw( graphics, new PointF( x, SequencePanel.heightRow ) ) );
                x += _control.Columns[i].Width;
                if ( ++count > _showColnum - 1 )
                    break;
            }
            x = 0;
            for ( int i = 0, count = 0; i < _control.Columns.Count; i++ )
            {
                if ( i == 0 || i > IndexColnum )
                {
                    column = _control.Columns[i];
                    glyphHash.Add( column, column.Draw( graphics, new PointF( x, 0 ) ) );
                    if ( i == 0 )
                    {
                        Glyph glyph = _control.Rows[0];
                        glyphHash.Add( glyph, glyph.Draw( graphics, new PointF( x, SequencePanel.heightRow ) ) );
                    }
                    x += column.Width;
                    if ( ++count > _showColnum )
                        break;
                }
            }

            Rectangle rect = new Rectangle( 0, SequencePanel.heightRow * 2, x, SequencePanel.heightDelimeter );
            graphics.FillRectangle( _brush, rect );
            graphics.DrawRectangle( Column.nonePen, rect );
        }

        public int ChangeSize( int widthShow, int indexColumn )
        {
            int width = _control.Columns[0].Width;
            _showColnum = 0;

            for ( int i = indexColumn + 1; i < _control.Columns.Count; i++ )
            {
                width += _control.Columns[i].Width;
                _showColnum++;
                if ( width >= widthShow )
                    break;
            }
            while ( width < widthShow && indexColumn > 0 )
            {
                width += _control.Columns[indexColumn].Width;
                _showColnum++;
                if ( width >= widthShow )
                    break; 
                indexColumn--;
            }
            IndexColnum = indexColumn;
            return _showColnum;
        }

        public Column GetSplitter( PointF point, float zoom )
        {
            int width = 0;
            int count = 0;
            for ( int i = 0; i < _control.Columns.Count; i++ )
            {
                if ( i == 0 || i > IndexColnum )
                {
                    Column column = _control.Columns[i];
                    width += column.Width;
                    if ( ( new RectangleF( width - SequencePanel.widthSplitter / ( 2 * zoom ), 0,
                        SequencePanel.widthSplitter / zoom, SequencePanel.heightRow ) ).Contains( point ) )
                        return column;
                    if ( ++count > _showColnum )
                        break;
                }
            }
            return null;
        }

        public void CalculationSize()
        {
            if ( _control == null )
                return;

            _size.Width = 0;
            foreach ( Column obj in _control.Columns )
                _size.Width += obj.Width;
            _size.Height = SequencePanel.heightRow * 2 + SequencePanel.heightDelimeter;
        }
    }
}
