using System.Collections.Generic;
using System.Drawing;
using Tools.Interfaces;

namespace Amos.Controls.Sequence
{
    public class Row : Glyph
    {
        private static Brush brushNone;
        private static Brush brushAction;
        private static Brush brushSelect;
        private static Pen penNone;
        private static Pen penAction;
        private static Pen penSelect;

        private Column _column;

        static Row()
        {
            brushNone = new SolidBrush( Color.FromArgb( 0xE4, 0xEC, 0xF7 ) );
            brushAction = new SolidBrush( Color.FromArgb( 0xFF, 0xD5, 0x8D ) );
            brushSelect = new SolidBrush( Color.FromArgb( 0xBB, 0xC4, 0xD1 ) );
            penNone = new Pen( Color.FromArgb( 0x9E, 0xB6, 0xCE ), 1 );
            penAction = new Pen( Color.FromArgb( 0xF2, 0x95, 0x36 ), 1 );
            penSelect = new Pen( Color.FromArgb( 0x87, 0x9F, 0xB7), 1 );
        }

        public Row( SequenceControl parent, IObjectCollection objectColection, Column column, List<Cell> cellList )
            : base ( parent )
        {
            ObjectCollection = objectColection;
            _column = column;
            Cells = cellList;
            ObjectCollection.onSetObject += OnSetObject;
            _column.Add( this );
        }

        public override eStateGlyph StateGlyph
        {
            get { return base.StateGlyph; }
            set
            {
                base.StateGlyph = value;
                if ( value != eStateGlyph.Action )
                {
                    foreach ( Cell cell in Cells )
                        if ( cell.StateGlyph != value )
                            cell.StateGlyph = value;
                }
            }
        }

        public IStream Stream { get { return ObjectCollection.Stream; } }

        public IObjectCollection ObjectCollection { get; private set; }

        public bool IsEditName { get { return ObjectCollection.Stream.IsEdit; } }

        public bool IsMove { get { return ObjectCollection.Stream.IsMove; } }

        public bool IsVisible { get { return ObjectCollection.IsVisible; } }

        public override string Text
        {
            get { return ObjectCollection.Stream.Name; }
            set { Parent.RenameElement( Stream, value ); }
        }

        public List<Cell> Cells { get; private set; }

        public Glyph this[int index]
        {
            get 
            {
                if ( index == 0 )
                    return this;
                return Cells[index - 1]; 
            }
        }

        public override RectangleF Draw( Graphics graphics, PointF point, object data = null ) 
        {
            RectangleF rect = new RectangleF( point.X, point.Y, _column.Width, SequencePanel.heightRow );
            switch ( StateGlyph )
            {
            case eStateGlyph.Normal:
                graphics.FillRectangle( brushNone, rect );
                graphics.DrawRectangle( penNone, Rectangle.Ceiling( rect ) );
                break;
            case eStateGlyph.Action:
                graphics.FillRectangle( brushAction, rect );
                graphics.DrawRectangle( penAction, Rectangle.Ceiling( rect ) );
                break;
            case eStateGlyph.Select:
                graphics.FillRectangle( brushSelect, rect );
                graphics.DrawRectangle( penSelect, Rectangle.Ceiling( rect ) );
                break;
            }
            int indention = SequencePanel.heightRow / 7;
            RectangleF rectText = new RectangleF( indention, rect.Top + 1, rect.Width - indention - 1, rect.Height - 2 );
            if ( ObjectCollection.TypeCollection != eTypeObjectCollection._1D )
            {
                rectText = new RectangleF(rect.Width - SequencePanel.heightRow, rect.Top, SequencePanel.heightRow, rect.Height);
                graphics.DrawRectangle( penSelect, Rectangle.Ceiling( rectText ) );
                switch (ObjectCollection.TypeCollection)
                {
                case eTypeObjectCollection._2D:
                    graphics.DrawString( "2D", SequencePanel.fontBold, Brushes.Black, rectText, SequencePanel.sfMidleCenter );
                    break;
                case eTypeObjectCollection._3D:
                    graphics.DrawString( "3D", SequencePanel.fontBold, Brushes.Black, rectText, SequencePanel.sfMidleCenter );
                    break;
                case eTypeObjectCollection._4D:
                    graphics.DrawString( "4D", SequencePanel.fontBold, Brushes.Black, rectText, SequencePanel.sfMidleCenter );
                    break;
                }
                rectText = new RectangleF(indention, rect.Top + 1, rect.Width - indention - SequencePanel.heightRow - 1, rect.Height - 2);
            }
            graphics.DrawString( Text, SequencePanel.font, Brushes.Black, rectText, SequencePanel.sfLeftCenter );

            return rect;
        }

        public override void DrawEdit( Graphics graphics, RectangleF rect )
        {
            rect.Inflate( 1, 1 );
            graphics.FillRectangle( Brushes.White, rect );
            graphics.DrawRectangle( new Pen(Color.Black, 3), Rectangle.Ceiling( rect ) );
        }

        /// <summary>
        /// Возвращает необходимую ширину для нормального отображения содиржимого  
        /// </summary>
        /// <param name="graphics">Объект контекста устройства</param>
        /// <returns>ширину в пикселях</returns>
        public override int GetFillWidth( Graphics graphics )
        {
            if ( IsVisible == false )
                return Column.minWidthColumn;
            int indention = SequencePanel.heightRow / 7;
            if ( ObjectCollection.TypeCollection == eTypeObjectCollection._1D )
                return (int)( graphics.MeasureString( Text, SequencePanel.font ).Width + indention * 2 );
            else
                return (int)( graphics.MeasureString( Text, SequencePanel.font ).Width + indention * 2 + SequencePanel.heightRow );
        }

        private void OnSetObject( int index, IObjectStream @object )
        {
            Cells[index].Object = @object;
            if ( Cells[index].StateGlyph == eStateGlyph.Select)
                Parent.ChangeSelect( @object );
        }
    }
}
