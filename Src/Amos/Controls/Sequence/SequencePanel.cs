using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using Amos.Commands;
using Localize;
using Tools.Interfaces;

namespace Amos.Controls.Sequence
{
    public class SequencePanel : Panel, ISequencePanel
    {
        enum eState { None, Edit, ResizeColumn, MoveRow }

        public const int heightRow = 22;
        public const int heightDelimeter = 3;
        public const int heightLineDelimeter = 5;
        public const int defaultStartWidth = 100;
        public const int defaulWidth = 50;
        public const int widthSplitter = 6;
        public const float heightFont = 8.25f;
        
        private static Brush brushDelimeter;
        private static Cursor cursorSelectColumn;
        private static Cursor cursorSelectRow;
        private static Cursor cursorMoveRow;
        public static Font font;
        public static Font fontBold;
        public static StringFormat sfMidleCenter;
        public static StringFormat sfLeftCenter;
        public static Header header;
        private static eState state;
        public static bool isSelected;
        public static Glyph glyphSelect;
        private static Glyph glyphEdit;
        private static TextBox textBox;
        
        private SequenceControl _control;
        private VScrollBar _vScrollBar;
        private Size _size;
        private float _zoom;
        private int _indexRow;
        private int _showRow;
        private bool _isHeader;
        private bool _isDelimeter;
        private Glyph _glyph;
        private Glyph _glyphToolTip;
        private Dictionary<Glyph, object> _glyphHash;
        private BufferedGraphicsContext context;
        private BufferedGraphics grafx;
        private Timer _timer;
        private MoveRowCommand _moveRowCommand;
        private ToolTip _toolTip;
        private ResizeColumnCommand _resizeColumnCommand;

        static SequencePanel()
        {
            header = new Header();

            textBox = new TextBox();
            textBox.BorderStyle = BorderStyle.None;
            textBox.TabIndex = 0;

            cursorSelectColumn = Local.GetCursor( "SelectColumn" );
            cursorSelectRow = Local.GetCursor( "SelectRow" );
            cursorMoveRow = Local.GetCursor( "MoveRow" );

            font = new Font( FontFamily.GenericSansSerif, heightFont, FontStyle.Regular );
            fontBold = new Font( FontFamily.GenericSansSerif, heightFont, FontStyle.Bold );

            sfMidleCenter = new StringFormat( StringFormatFlags.NoWrap );
            sfMidleCenter.Trimming = StringTrimming.EllipsisCharacter;
            sfMidleCenter.Alignment = StringAlignment.Center;
            sfMidleCenter.LineAlignment = StringAlignment.Center;

            sfLeftCenter = new StringFormat( StringFormatFlags.NoWrap );
            sfLeftCenter.Trimming = StringTrimming.EllipsisCharacter;
            sfLeftCenter.Alignment = StringAlignment.Near;
            sfLeftCenter.LineAlignment = StringAlignment.Center;
            
            brushDelimeter = new HatchBrush( HatchStyle.Percent50, Color.Black, Color.Transparent );
        }

        public SequencePanel()
        {
            _indexRow = 1;
            _zoom = 1.0f;
            _glyphHash = new Dictionary<Glyph, object>(100);
            textBox.KeyDown += OnEditKeyDown;
            context = BufferedGraphicsManager.Current;
            context.MaximumBuffer = new Size( Width + 1, Height + 1 );
            _timer = new Timer();
            _timer.Interval = 200;
            _timer.Tick += OnProperty;
            _toolTip = new ToolTip( (IContainer) new System.ComponentModel.Container( ) );
            _toolTip.AutoPopDelay = 5000;
            _toolTip.InitialDelay = 1000;
            _toolTip.ReshowDelay = 250;
            _toolTip.ShowAlways = true;
            _toolTip.IsBalloon = true;
        }

        public void Initialize( SequenceControl control, VScrollBar vScrollBar, bool isHeader = false )
        {
            _control = control;
            _vScrollBar = vScrollBar;
            _isHeader = isHeader;
            _vScrollBar.Value = 0;
            _vScrollBar.ValueChanged += OnValueChanged;
            if ( _isHeader )
            {
                header.Initialize( control );
                Focus();
            }
            Refresh();
        }

        public int Zoom 
        {
            set { _zoom = value / 100.0f; } 
        }

        public bool isDelimeter 
        {
            get { return _isDelimeter; }
            set
            {
                _isDelimeter = value;
                if ( _isDelimeter )
                    IndexDelimeter = IndexRow;
                Refresh();
            }
        }

        public int IndexDelimeter { get; set; }

        public int IndexRow 
        {
            get { return _indexRow; }
            set
            {
                if ( _indexRow != value && value >= 0 )
                {
                    _indexRow = value;
                    _vScrollBar.Value = _indexRow;
                }
            }
        } 
        
        public int ShowRow { get { return _showRow; } }

        public int MinHeight { get { return (int)( ( _isHeader ? header.Size.Height : heightRow) * _zoom ); } }

        public int HeightDelemiter
        {
            get { return (int)( ( ( _isHeader ? header.Size.Height : 0 ) + ( IndexDelimeter - IndexRow ) * heightRow ) * _zoom ); }
        }
        
        public override void Refresh()
        {
            if ( _control == null )
                return;

            CalculationSize();

            int height = (int)Math.Ceiling( this.Height / _zoom );
            _vScrollBar.Enabled = height < _size.Height;
            if ( _vScrollBar.Maximum != _control.CountVisibleRows - 1 )
                _vScrollBar.Maximum = _control.CountVisibleRows > 0 ? _control.CountVisibleRows - 1 : 0;
            _vScrollBar.LargeChange = ChangeSize( height, _vScrollBar.Value );
            if ( _vScrollBar.Value != IndexRow )
                _vScrollBar.Value = IndexRow;

            Draw( grafx.Graphics );
            
            if ( state != eState.Edit && textBox.Visible )
                Controls.Remove( textBox );
            else if ( state == eState.Edit )
                MoveEdit( glyphEdit );

            base.Refresh();
        }
        
        public void MoveDelemiter( Point point )
        {
            int index = IndexRow + ( point.Y - header.Size.Height ) / heightRow;
            if ( index < IndexRow )
                index = IndexRow;
            if ( index > IndexRow + _showRow - 2 )
                index = IndexRow + _showRow - 2;
            if (index != IndexDelimeter)
            {
                IndexDelimeter = index;
                Refresh();
            }
        }

        public void OnEdit( object sender, EventArgs e )
        {
            ShowEdit( glyphSelect );
        }

        public void OnClear( object sender, EventArgs e )
        {
            _control.ClearObject( glyphSelect as Cell );
        }

        public void OnTable1D2D( object sender, EventArgs e )
        {
            _control.SetCell( glyphSelect as Cell, eTypeObjectCollection._1D, eTypeObjectStream.Table1D2D );
        }

        public void OnTableShape( object sender, EventArgs e )
        {
            _control.SetCell( glyphSelect as Cell, eTypeObjectCollection._1D, eTypeObjectStream.TableShape );
        }

        public void OnTable1D( object sender, EventArgs e )
        {
            _control.SetCell( glyphSelect as Cell, eTypeObjectCollection._1D, eTypeObjectStream.Table );
        }

        public void OnTable2D( object sender, EventArgs e )
        {
            _control.SetCell( glyphSelect as Cell, eTypeObjectCollection._2D, eTypeObjectStream.Table );
        }

        public void OnTable3D( object sender, EventArgs e )
        {
            _control.SetCell( glyphSelect as Cell, eTypeObjectCollection._3D, eTypeObjectStream.Table );
        }

        public void OnTable4D( object sender, EventArgs e )
        {
            _control.SetCell( glyphSelect as Cell, eTypeObjectCollection._4D, eTypeObjectStream.Table );
        }

        public void OnLoopStart( object sender, EventArgs e )
        {
            _control.SetCell( glyphSelect as Cell, "1" );
        }

        public void OnLoopEnd( object sender, EventArgs e )
        {
            _control.SetCell( glyphSelect as Cell, "-1" );
        }

        public void OnAcquisition( object sender, EventArgs e )
        {
            _control.SetCell( glyphSelect as Cell, eTypeObjectCollection._1D, eTypeObjectStream.Acquisition );
        }

        public void OnContinue( object sender, EventArgs e )
        {
            _control.SetCell( glyphSelect as Cell, eTypeObjectCollection._1D, eTypeObjectStream.Continue );
        }

        public void OnSignal( object sender, EventArgs e )
        {
            _control.SetCell( glyphSelect as Cell, "1" );
        }

        public void OnPlusX( object sender, EventArgs e )
        {
            _control.SetCell( glyphSelect as Cell, "0" );
        }       
        
        public void OnPlusY( object sender, EventArgs e )
        {
            _control.SetCell( glyphSelect as Cell, "1" );
        }

        public void OnMinusX( object sender, EventArgs e )
        {
            _control.SetCell( glyphSelect as Cell, "2" );
        }

        public void OnMinusY( object sender, EventArgs e )
        {
            _control.SetCell( glyphSelect as Cell, "3" );
        }

        public void OnAsynchronousStart( object sender, EventArgs e )
        {
            _control.SetCell( glyphSelect as Cell, eTypeObjectCollection._1D, eTypeObjectStream.AsynchronousStart );
        }

        public void OnAsynchronousStop( object sender, EventArgs e )
        {
            _control.SetCell( glyphSelect as Cell, eTypeObjectCollection._1D, eTypeObjectStream.AsynchronousStop );
        }

        public void OnProperty( object sender, EventArgs e )
        {
            _timer.Stop( );
            if ( glyphSelect is Cell )
                _control.ShowProperty( ( glyphSelect as Cell ).Object );
        }

        protected override void  OnPaint(PaintEventArgs e)
        {
 	        grafx.Render( e.Graphics );
        }

        protected override void OnPaintBackground( PaintEventArgs e ) { }

        protected override void OnResize( EventArgs eventargs )
        {
            base.OnResize( eventargs );
            context.MaximumBuffer = new Size( Width + 1, Height + 1 );
            if ( grafx != null )
            {
                grafx.Dispose();
                grafx = null;
            }
            grafx = context.Allocate( CreateGraphics(), new Rectangle( 0, 0, Width, Height ) );
            Refresh();
        }

        protected override void OnMouseMove( MouseEventArgs e )
        {
            PointF point = new PointF( e.X / _zoom, e.Y / _zoom );

            if ( state == eState.None )
            {
                if ( _isHeader && ( _glyph = header.GetSplitter( point, _zoom ) ) != null )
                    Cursor = Cursors.VSplit;
                else
                {
                    _glyph = Find( point );
                    if ( _glyph is Column && _glyph.StateGlyph != eStateGlyph.Select )
                        Cursor = cursorSelectColumn;
                    else if ( _glyph is Row )
                    {
                        if ( _glyph.StateGlyph == eStateGlyph.Select )
                        {
                            if ( ( _glyph as Row ).IsMove )
                                Cursor = cursorMoveRow;
                            else
                                Cursor = Cursors.Arrow;
                        }
                        else
                            Cursor = cursorSelectRow;
                    }
                    else
                        Cursor = Cursors.Arrow;
                    if ( _glyph is Cell )
                    {
                        if ( _glyphToolTip != _glyph )
                        {
                            _toolTip.RemoveAll( );
                            _toolTip.SetToolTip( this, _glyph.ToolTip );
                        }
                        _glyphToolTip = _glyph;
                    }
                    else
                        _toolTip.SetToolTip( this, null );
                }
            }
            else if ( state == eState.ResizeColumn )
            {
                _resizeColumnCommand.Resize( (int)( point.X - ( (RectangleF)_glyphHash[_glyph] ).Left ) );
                _control.Refresh();
            }
            else if ( state == eState.MoveRow )
            {
                int index = (int)( ( _indexRow + 1 ) + ( point.Y - ( _isHeader ? header.Size.Height : 0 ) ) / heightRow );
                if ( index < IndexRow )
                    index = IndexRow;
                if ( index > IndexRow + _showRow - 1 )
                    index = IndexRow + _showRow - 1;
                Row rowMove = null;
                foreach ( Row row in _control.Rows )
                {
                    if ( !row.IsVisible )
                        continue;
                    if ( index == 0 )
                    {
                        rowMove = row;
                        break;
                    }
                    index--;
                }

                if ( rowMove != null && rowMove.IsMove && rowMove != glyphSelect && glyphSelect is Row &&
                    rowMove.ObjectCollection.TypeCollection == ( glyphSelect as Row ).ObjectCollection.TypeCollection )
                {
                    _moveRowCommand.MoveTo( rowMove );
                    _control.Refresh();
                }
            }
        }

        protected override void OnMouseDown( MouseEventArgs e )
        {
            if ( !Focused )
            {
                if ( state == eState.Edit )
                {
                    HideEdit( glyphEdit );
                    _control.Refresh();
                }
                Focus();
            }
            if ( state == eState.None && _glyph != null )
            {
                if ( Cursor == Cursors.VSplit )
                {
                    state = eState.ResizeColumn;
                    _resizeColumnCommand = new ResizeColumnCommand( Program.Document, (Column)_glyph );
                    Capture = true;
                }
                else if ( Cursor == cursorMoveRow )
                {
                    state = eState.MoveRow;
                    _moveRowCommand = new MoveRowCommand( Program.Document, (Row)glyphSelect );
                    Capture = true;
                }
                else
                {
                    PointF point = new PointF( e.X / _zoom, e.Y / _zoom );
                    _glyph = Find( point );
                    if (_glyph != null && glyphSelect != _glyph )
                        _control.Select( this, _glyph );
                    else if ( glyphSelect != null )
                    {
                        ContextMenuStrip = glyphSelect.GetContextMenu( this );
                        if ( glyphSelect is Cell )
                            _control.ChangeSelect( ( glyphSelect as Cell ).Object );
                        else
                            _control.ChangeSelect( null );
                    }
                }
            }
            base.OnMouseDown( e );
        }

        protected override void OnMouseUp( MouseEventArgs e )
        {

            if ( state == eState.ResizeColumn || state == eState.MoveRow )
            {
                state = eState.None;
                Capture = false;
                Cursor = Cursors.Arrow;
                if ( _moveRowCommand != null )
                {
                    Program.Document.Add( _moveRowCommand );
                    _moveRowCommand = null;
                }
                else if ( _resizeColumnCommand != null )
                {
                    Program.Document.Add( _resizeColumnCommand );
                    _resizeColumnCommand = null;
                }
            }
            base.OnMouseUp( e );
        }

        protected override void OnMouseClick( MouseEventArgs e )
        {
            PointF point = new PointF( e.X / _zoom, e.Y / _zoom );
            _glyph = Find( point );

            if ( e.Button == MouseButtons.Left && !isSelected && glyphSelect == _glyph && glyphSelect is Cell && ( glyphSelect as Cell ).Object.IsProterty )
                _timer.Start( );
            else if ( e.Button == MouseButtons.Middle )
                OnProperty( null, new EventArgs() );
            isSelected = false;
            base.OnMouseClick( e );
        }

        protected override void OnMouseDoubleClick( MouseEventArgs e )
        {
            PointF point = new PointF( e.X / _zoom, e.Y / _zoom );
            if ( Cursor == Cursors.VSplit )
            {
                Graphics graphics = Graphics.FromHwnd( this.Handle );
                InitGraphics( graphics );
                _resizeColumnCommand = new ResizeColumnCommand( Program.Document, (Column)_glyph );
                _resizeColumnCommand.Resize( _glyph.GetFillWidth( graphics ) );
                Program.Document.Add( _resizeColumnCommand );
                _resizeColumnCommand = null;
                _control.Refresh( );
            }
            else
            {
                _glyph = Find( point );
                if ( _glyph is Cell && ( _glyph as Cell ).IsEdit || ( _glyph is Row && ( _glyph as Row ).IsEditName ) )
                {
                    _timer.Stop( );
                    Capture = false;
                    ShowEdit( _glyph );
                }
            }
            base.OnMouseDoubleClick( e );
        }

        protected override void OnMouseWheel( MouseEventArgs e )
        {
            if ( e.Delta > 0 )
                IndexRow--;
            else
                IndexRow++;
            base.OnMouseWheel( e );
        }

        protected override void OnPreviewKeyDown( PreviewKeyDownEventArgs e )
        {
            e.IsInputKey = true;
            base.OnPreviewKeyDown( e );
        }

        protected override void OnKeyDown( KeyEventArgs e )
        {
            if (glyphSelect is Cell)
            {
                if ( e.KeyData == Keys.Enter )
                    ShowEdit( glyphSelect );
                if ( e.KeyData == Keys.Up )
                    _control.SelectUp( this, glyphSelect as Cell );
                else if ( e.KeyData == Keys.Down)
                    _control.SelectDown( this, glyphSelect as Cell );
                else if ( e.KeyData == Keys.Left )
                    _control.SelectLeft( this, glyphSelect as Cell );
                else if ( e.KeyData == Keys.Right )
                    _control.SelectRight( this, glyphSelect as Cell );
                else if ( e.KeyData == Keys.Delete )
                    _control.ClearObject( glyphSelect as Cell );
            }
            else if ( e.KeyData == Keys.Enter && glyphSelect is Row && ( glyphSelect as Row ).IsEditName )
                ShowEdit( glyphSelect );
            else if ( glyphSelect is Column )
            {
                if ( e.KeyData == Keys.Delete )
                    _control.RemoveColumn( ( glyphSelect as Column ).Index - 1 );
                else if ( e.KeyData == (Keys.Control | Keys.Delete) )
                    _control.ClearColumn( ( glyphSelect as Column ).Index - 1 );
                else if ( e.KeyData == Keys.Insert )
                    _control.InsertColumn( ( glyphSelect as Column ).Index );
                else if ( e.KeyData == ( Keys.Control | Keys.X ) )
                    _control.CutColumn( ( glyphSelect as Column ).Index - 1 );
                else if ( e.KeyData == ( Keys.Control | Keys.C ) )
                    _control.CopyColumn( ( glyphSelect as Column ).Index - 1 );
                else if ( e.KeyData == ( Keys.Control | Keys.V ) )
                    _control.PasteColumn( ( glyphSelect as Column ).Index - 1 );
            }
            base.OnKeyDown( e );
        }

        private void OnEditKeyDown( object sender, KeyEventArgs e )
        {
            if ( (e.KeyData == Keys.Enter || e.KeyData == Keys.Escape ) && e.SuppressKeyPress == false )
            {
                e.SuppressKeyPress = true;
                if ( glyphEdit != null )
                {
                    HideEdit( glyphEdit, e.KeyData == Keys.Escape );
                    Focus( );
                    _control.Refresh();
                }
            }
        }

        private int ChangeSize(int heightShow, int indexRow)
        {
            int height = _isHeader ? header.Size.Height: 0;
            _showRow = 0;

            for ( int i = indexRow + 1; i < _control.CountVisibleRows; i++ )
            {
                height += heightRow;
                _showRow++;
                if ( height >= heightShow )
                    break;
            }
            while ( height < heightShow && indexRow > 0 )
            {
                height += heightRow;
                _showRow++;
                if ( height >= heightShow )
                    break;
                indexRow--;
            }
            IndexRow = indexRow;
            return _showRow;
        }

        private void InitGraphics( Graphics graphics )
        {
            _glyphHash.Clear();
            graphics.ResetTransform();
            graphics.Clear( Color.White );
            graphics.ScaleTransform( _zoom, _zoom );
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.TextRenderingHint = ( _zoom < 0.9 ) ? TextRenderingHint.AntiAlias : TextRenderingHint.SystemDefault;
        }

        private void OnValueChanged( object sender, EventArgs e )
        {
            Refresh();
        }

        private void Draw( Graphics graphics )
        {
            if ( _control == null )
                return;

            InitGraphics( graphics );
            int y = _isHeader ? header.Size.Height : 0;
            for ( int i = IndexRow + 1, countRow = 0; i < _control.Rows.Count; )
            {
                Row row = _control.Rows[i++];
                if ( row.IsVisible == false )
                    continue;
                for ( int j = header.IndexColnum + 1, x = _control.Columns[0].Width, countColumn = 0; j < _control.Columns.Count; j++ )
                {
                    Glyph glyph = row[j];
                    _glyphHash.Add( glyph, glyph.Draw( graphics, new PointF( x, y ), i ) );
                    x += _control.Columns[j].Width;
                    if ( ++countColumn > header.ShowColnum - 1 )
                        break;
                }
                _glyphHash.Add( row, row.Draw( graphics, new PointF( 0, y ) ) );
                y += heightRow;
                if ( ++countRow > _showRow )
                    break;
            }
            if ( isDelimeter )
            {
                y = header.Size.Height + ( IndexDelimeter - IndexRow ) * heightRow;
                graphics.FillRectangle( brushDelimeter, new RectangleF( 0, y, Width / _zoom, heightLineDelimeter ) );
            }
            if ( _isHeader )
                header.Draw( graphics, _glyphHash );

            if ( glyphEdit != null && _glyphHash.ContainsKey( glyphEdit ) )
                glyphEdit.DrawEdit( graphics, (RectangleF)_glyphHash[glyphEdit] );
            else if ( glyphSelect != null && _glyphHash.ContainsKey( glyphSelect ) )
                glyphSelect.DrawSelect( graphics, (RectangleF)_glyphHash[glyphSelect] );
        }

        private void CalculationSize()
        {
            if ( _control == null )
                return;
            if ( _isHeader )
            {
                header.CalculationSize();
                _size.Height = header.Size.Height + heightRow * ( _control.CountVisibleRows - 1 );
            }
            else
                _size.Height = heightRow * ( _control.CountVisibleRows - 1 );
            _size.Width = header.Size.Width;
        }

        private Glyph Find( PointF point )
        {
            foreach ( KeyValuePair<Glyph, object> pair in _glyphHash )
                if ( ( (RectangleF)pair.Value ).Contains( point ) )
                    return pair.Key;
            return null;
        }

        private void ShowEdit( Glyph glyph )
        {
            Cursor = Cursors.Arrow;
            state = eState.Edit;
            glyphEdit = glyph;
            Rectangle rect = Rectangle.Ceiling( (RectangleF)_glyphHash[glyph] );
            textBox.Font = new Font( FontFamily.GenericSansSerif, heightFont * _zoom, FontStyle.Regular );
            textBox.Size = new Size( (int)( ( rect.Width - 3 ) * _zoom ), (int)( rect.Height * _zoom ) );
            int deltaHeight = (int)( ( ( rect.Height * _zoom ) - textBox.Size.Height ) / 2 );
            textBox.Location = new Point( (int)( ( rect.Left + 2 ) * _zoom ), (int)( rect.Top * _zoom + deltaHeight ) );
            textBox.Text = glyphEdit.Text;
            Controls.Add( textBox );
            textBox.Visible = true;
            textBox.Focus();
            Refresh();
        }

        private void MoveEdit( Glyph glyph )
        {
            if ( Contains( textBox ) == false )
                return;
            if ( _glyphHash.ContainsKey( glyph ) == false )
            {
                textBox.Visible = false;
                return;
            }
            Rectangle rect = Rectangle.Ceiling( (RectangleF)_glyphHash[glyph] );
            textBox.Font = new Font( FontFamily.GenericSansSerif, heightFont * _zoom, FontStyle.Regular );
            textBox.Size = new Size( (int)( ( rect.Width - 4 ) * _zoom ), (int)( rect.Height * _zoom ) );
            int deltaHeight = (int)( ( ( rect.Height * _zoom ) - textBox.Size.Height ) / 2 );
            textBox.Location = new Point( (int)( ( rect.Left + 3 ) * _zoom ), (int)( rect.Top * _zoom + deltaHeight ) );
            textBox.Visible = true;
            textBox.Focus();
         }

        private void HideEdit( Glyph glyph, bool isUndo = false )
        {
            state = eState.None;
            if ( isUndo == false )
                glyph.Text = textBox.Text;
            textBox.Visible = false;
            Controls.Remove( textBox );
            glyphEdit = null;
        }
    }
}
