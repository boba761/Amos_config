using System;
using System.Drawing;
using System.Windows.Forms;
using Tools.Interfaces;

namespace Amos.Controls.Sequence
{
    public class Cell : Glyph
    {
        private static Pen pen;
        private static Pen penSelect;
        private static Brush brush1;
        private static Brush brush2;
        private static Brush brushSelect;
        
        static Cell()
        {
            pen = new Pen( Color.FromArgb( 0xD0, 0xD7, 0xE5 ) );
            brush1 = new SolidBrush( Color.White );
            brush2 = new SolidBrush( Color.FromArgb( 252, 252, 252 ) );
            penSelect = new Pen( Color.Black, 3 );
            brushSelect = new SolidBrush( Color.FromArgb( 0xF0, 0xF0, 0xF0 ) );
        }

        public Cell( SequenceControl parent, IObjectStream objectSequence, Row row, Column column )
            : base ( parent )
        {
            Row = row;
            Column = column;
            Object = objectSequence;
        }

        public bool IsEdit { get { return Object.IsEdit; } }

        public Row Row { get; private set; }
        
        public Column Column { get; private set; }

        public IObjectStream Object { get; set; }

        public int IndexColumn { get { return Column.Index - 1; } }

        public IStream Stream { get { return Row.Stream; } }

        public eTypeObjectCollection TypeCollection { get { return Row.ObjectCollection.TypeCollection; } }

        public IObjectCollection ObjectCollection { get { return Row.ObjectCollection; } }

        public override eStateGlyph StateGlyph
        {
            get { return base.StateGlyph; }
            set
            {
                base.StateGlyph = value;
                if ( Row.StateGlyph != eStateGlyph.Select)
                    Row.StateGlyph = value == eStateGlyph.Select ? eStateGlyph.Action : value;
                if ( Column.StateGlyph != eStateGlyph.Select )
                    Column.StateGlyph = value == eStateGlyph.Select ? eStateGlyph.Action : value;
            }
        }

        public override string Text
        {
            get 
            {
                return Object.Text; 
            }
            set
            {
                if ( Object.Text == value )
                    return;
                Parent.SetCell( this, value );
            }
        }

        public override string ToolTip
        {
            get { return string.Format( "Event {0}: {1}", Column.Index, Object.ToolTip ); }
        }

        public override ContextMenuStrip GetContextMenu( SequencePanel panel )
        {
            return Row.Stream.GetContextMenu( panel, Row.ObjectCollection.TypeCollection, Column.Index - 1 );
        }

        public override RectangleF Draw( Graphics graphics, PointF point, object data = null )
        {
            RectangleF rect = new RectangleF( point.X, point.Y, Column.Width, SequencePanel.heightRow );
            if ( StateGlyph == eStateGlyph.Select )
                graphics.FillRectangle( brushSelect, rect );
            else if ( data != null )
            {
                if ( (int)data % 2 == 0 )
                    graphics.FillRectangle( brush1, rect );
                else
                    graphics.FillRectangle( brush2, rect );
            }
            graphics.DrawRectangle( pen, Rectangle.Ceiling( rect ) );
            if ( IndexColumn == Stream.EventCount - 1 && TypeCollection == eTypeObjectCollection._1D )
                return rect;
            if ( Object.Type != eTypeObjectStream.Default )
            {
                Image image = Object.Image;
                string text = Object.DrawText;
                RectangleF rectImage;
                RectangleF rectText;
                if ( image != null && text != null && rect.Width > image.Width )
                {
                    SizeF sizeText = graphics.MeasureString( text, SequencePanel.font );
                    if ( rect.Width >= sizeText.Width + image.Width )
                    {
                        rectImage = new RectangleF( new PointF( rect.Left + ( rect.Width - ( image.Size.Width + sizeText.Width )) / 2,
                            rect.Top + ( rect.Height - image.Size.Height ) / 2 ), image.Size );
                        rectText = new RectangleF( rectImage.Right, rectImage.Top, sizeText.Width, rectImage.Height );
                    }
                    else
                    {
                        rectImage = new RectangleF( new PointF( rect.Left, rect.Top + ( rect.Height - image.Size.Height ) / 2 ), image.Size );
                        rectText = new RectangleF( rectImage.Right, rectImage.Top, rect.Width - rectImage.Width, rectImage.Height );
                    }
                    graphics.DrawImage( image, rectImage );
                    graphics.DrawString( text, SequencePanel.font, Brushes.Black, rectText, SequencePanel.sfLeftCenter ); 
                }
                else if ( image != null )
                {
                    rectImage = new RectangleF( new PointF( rect.Left + ( rect.Width - image.Size.Width ) / 2,
                        rect.Top + ( rect.Height - image.Size.Height ) / 2 ), image.Size );
                    graphics.DrawImage( image, rectImage );
                }
                else if ( text != null )
                {
                    rectText = new RectangleF( rect.Left + 1, rect.Top + 1, rect.Width - 2, rect.Height - 2 );
                    graphics.DrawString( text, SequencePanel.font, Brushes.Black, rectText, SequencePanel.sfMidleCenter );
                }
            }
            return rect;
        }

        public override void DrawSelect( Graphics graphics, RectangleF rect )
        {
            graphics.DrawRectangle( penSelect, Rectangle.Ceiling( rect ) );
        }

        public override void DrawEdit( Graphics graphics, RectangleF rect )
        {
            rect.Inflate( 1, 1 );
            graphics.FillRectangle( brush1, rect );
            graphics.DrawRectangle( penSelect, Rectangle.Ceiling( rect ) );
        }

        /// <summary>
        /// Возвращает необходимую ширину для нормального отображения содиржимого  
        /// </summary>
        /// <param name="graphics">Объект контекста устройства</param>
        /// <returns>ширину в пикселях</returns>
        public override int GetFillWidth( Graphics graphics )
        {
            if ( Object.Type != eTypeObjectStream.Default )
            {
                int indention = SequencePanel.heightRow / 7;
                Image image = Object.Image;
                string text = Object.DrawText;
                if ( image != null && text != null )
                    return (int)( image.Size.Width + graphics.MeasureString( text, SequencePanel.font ).Width + indention * 2);
                else if ( image != null )
                    return (int)( image.Size.Width + indention * 2 );
                else if ( text != null )
                    return (int)( graphics.MeasureString( text, SequencePanel.font ).Width + 4 + indention * 2 );
                return Column.minWidthColumn;
            }
            return Column.minWidthColumn;
        }
    }
}
