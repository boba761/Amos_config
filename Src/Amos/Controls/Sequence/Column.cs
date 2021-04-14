using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Localize;

namespace Amos.Controls.Sequence
{
    public class Column : Glyph
    {
        public const int minWidthColumn = 10;
        public const int maxWidthColumn = 500;
        private static Brush noneBrush;
        private static Brush actionBrush;
        private static Brush selectBrush;
        public static Pen nonePen;
        private static Pen actionPen;
        private static Pen selectPen;

        private bool _isBegin;
        private int _width;
        private List<Glyph> _celes;

        static Column()
        {
            noneBrush = new LinearGradientBrush( new Rectangle( 0, 0, 1, SequencePanel.heightRow ), Color.FromArgb( 0xF9, 0xFC, 0xFD ),
                Color.FromArgb( 0xD3, 0xDC, 0xE9 ), LinearGradientMode.Vertical );
            actionBrush = new LinearGradientBrush( new Rectangle( 0, 0, 1, SequencePanel.heightRow ), Color.FromArgb( 0xF9, 0xD9, 0x9F ),
                Color.FromArgb( 0xF1, 0xC1, 0x60 ), LinearGradientMode.Vertical );
            selectBrush = new LinearGradientBrush( new Rectangle( 0, 0, 1, SequencePanel.heightRow ), Color.FromArgb( 0xDF, 0xE2, 0xE4 ),
                Color.FromArgb( 0xBD, 0xC6, 0xD2 ), LinearGradientMode.Vertical );
            nonePen = new Pen( Color.FromArgb( 0x9E, 0xB6, 0xCE ), 1 );
            actionPen = new Pen( Color.FromArgb( 0xF2, 0x95, 0x36 ), 1 );
            selectPen = new Pen( Color.FromArgb( 0x87, 0x9F, 0xB7 ), 1 );
        }

        public Column( SequenceControl parent, string name, int width )
            : base ( parent )
        {
            _celes = new List<Glyph>( );
            _isBegin = true;
            Index = 0;
            Text = name;
            Width = width;
            StateGlyph = eStateGlyph.Normal;
        }

        public Column( SequenceControl parent, int index, int width )
            : base( parent )
        {
            _celes = new List<Glyph>( );
            _isBegin = false;
            Index = index;
            Text = index.ToString( );
            Width = width;
            StateGlyph = eStateGlyph.Normal;
        }

        public int Index { get; set; }

        public int Width 
        { 
            get { return _width; }
            set
            {
                _width = value;
                if ( _width < minWidthColumn )
                    _width = minWidthColumn;
                else if ( _width > maxWidthColumn )
                    _width = maxWidthColumn;
            }
        }

        public override string Text
        {
            get
            {
                if ( Index == 0 )
                    return base.Text;
                return Index.ToString();
            }
        }

        public override eStateGlyph StateGlyph
        {
            get { return base.StateGlyph; }
            set
            {
                if ( _isBegin )
                    return;
                base.StateGlyph = value;
                if ( value != eStateGlyph.Action )
                {
                    foreach ( Glyph cell in _celes )
                        if ( cell.StateGlyph != value )
                            cell.StateGlyph = value;
                }
            }
        }

        public override ContextMenuStrip GetContextMenu( SequencePanel panel )
        {
            ContextMenuStrip contextMenu = new ContextMenuStrip( );
            ToolStripMenuItem item;
            contextMenu.ImageScalingSize = new Size( 22, 22 );

            item = (ToolStripMenuItem)contextMenu.Items.Add( "Clear events", null, OnClearColumn );
            item.ShortcutKeys = Keys.Control | Keys.Delete; 
            
            contextMenu.Items.Add( "-" );
            
            item = (ToolStripMenuItem)contextMenu.Items.Add( "Insert events", null, OnInsertColumn );
            item.ShortcutKeys = Keys.Insert; 

            item = (ToolStripMenuItem)contextMenu.Items.Add( "Remove events", null, OnRemoveColumn );
            item.ShortcutKeys = Keys.Delete;
            item.Enabled = Parent.IsRemoveColumn( Index - 1 );

            contextMenu.Items.Add( "-" );

            item = (ToolStripMenuItem)contextMenu.Items.Add( "Cut", null, OnCutColumn );
            item.ShortcutKeys = Keys.Control | Keys.X;
            item.Enabled = Parent.IsCutColumn( Index - 1 );

            item = (ToolStripMenuItem)contextMenu.Items.Add( "Copy", null, OnCopyColumn );
            item.ShortcutKeys = Keys.Control | Keys.C;
            item.Enabled = Parent.IsCopyColumn( Index - 1 );

            item = (ToolStripMenuItem)contextMenu.Items.Add( "Paste", null, OnPasteColumn );
            item.ShortcutKeys = Keys.Control | Keys.V;
            item.Enabled = Parent.IsPasteColumn( Index - 1 );

            //Clipboard.
            return contextMenu;
        }

        public void Add( Glyph cell )
        {
            _celes.Add( cell );
        }

        public void Remove( Glyph cell )
        {
            _celes.Remove( cell );
        }

        public override RectangleF Draw( Graphics graphics, PointF point, object data = null )
        {
            RectangleF rect = new RectangleF( point.X, point.Y, _width, SequencePanel.heightRow );
            switch ( StateGlyph )
            {
            case eStateGlyph.Normal:
                graphics.FillRectangle( noneBrush, rect );
                graphics.DrawRectangle( nonePen, Rectangle.Ceiling( rect ) );
                break;
            case eStateGlyph.Action:
                graphics.FillRectangle( actionBrush, rect );
                graphics.DrawRectangle( actionPen, Rectangle.Ceiling( rect ) );
                break;
            case eStateGlyph.Select:
                graphics.FillRectangle( selectBrush, rect );
                graphics.DrawRectangle( selectPen, Rectangle.Ceiling( rect ) );
                break;
            }
            int indention = SequencePanel.heightRow / 7;
            graphics.DrawString( Text, SequencePanel.font, Brushes.Black, new RectangleF( rect.Left + indention, rect.Top + 1, 
                rect.Width - indention - 1, rect.Height -2), _isBegin ? SequencePanel.sfLeftCenter : SequencePanel.sfMidleCenter );
            return rect;
        }

        /// <summary>
        /// Возвращает необходимую ширину для нормального отображения содиржимого  
        /// </summary>
        /// <param name="graphics">Объект контекста устройства</param>
        /// <returns>ширину в пикселях</returns>
        public override int GetFillWidth( Graphics graphics )
        {
            int indention = SequencePanel.heightRow / 7;
            int maxWidth = (int)( graphics.MeasureString( Text, SequencePanel.font ).Width + indention * 2 );
            foreach ( Glyph glyph in _celes )
            {
                int width = glyph.GetFillWidth( graphics );
                if ( maxWidth < width )
                    maxWidth = width;
            }
            return maxWidth;
        }

        private void OnClearColumn( object sender, EventArgs e )
        {
            Parent.ClearColumn( Index - 1 );
        }

        private void OnInsertColumn( object sender, EventArgs e )
        {
            Parent.InsertColumn( Index );
        }

        private void OnRemoveColumn( object sender, EventArgs e )
        {
            Parent.RemoveColumn( Index - 1 );
        }

        private void OnCutColumn( object sender, EventArgs e )
        {
            Parent.CutColumn( Index - 1 );
        }

        private void OnCopyColumn( object sender, EventArgs e )
        {
            Parent.CopyColumn( Index - 1 );
        }

        private void OnPasteColumn( object sender, EventArgs e )
        {
            Parent.PasteColumn( Index - 1 );
        }
    }
}
