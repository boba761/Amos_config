using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.Windows.Forms;
using Amos.Interfaces;
using Calculations.Values;

namespace Amos.Controls.Signal
{
    class LargeSignalPanel : Panel
    {
        private static NumberFormatInfo _numberFormatInfo;

        private BufferedGraphicsContext context;
        private BufferedGraphics grafx;
        private int _points;
        private float _fontWidth;
        private float _window;
        private float _pointHeight;
        private float _pointWidth;
        private double _kofNetWidth;
        private double _kofNetHeight;
        private float _offsetTop = 1;
        private float _offsetLeft;
        private float _offsetRight = 1;
        private float _offsetButtom;
        private float _paintWidth;
        private float _paintHeight;
        private string _fornamNumber;
        private Font _font;
        private Pen _realPen;
        private Pen _imagPen;
        private Pen _netPen;
        private StringFormat _sfRightCenter;
        private StringFormat _sfCenterTop;

        static LargeSignalPanel( )
        {
            _numberFormatInfo = new NumberFormatInfo( );
            _numberFormatInfo.NumberDecimalSeparator = ".";
        }

        public LargeSignalPanel( )
        {
            context = BufferedGraphicsManager.Current;
            context.MaximumBuffer = new Size( Width + 1, Height + 1 );
            _realPen = new Pen( SignalControl.colorReal, 2.0f );
            _imagPen = new Pen( SignalControl.colorImag, 2.0f );
            _netPen = new Pen( SignalControl.colorNet, 0.5f );
            _netPen.DashStyle = DashStyle.Dot;
            _sfRightCenter = new StringFormat();
            _sfRightCenter.Alignment = StringAlignment.Far;
            _sfRightCenter.LineAlignment = StringAlignment.Center;
            _sfCenterTop = new StringFormat( );
            _sfCenterTop.Alignment = StringAlignment.Center;
            _sfCenterTop.LineAlignment = StringAlignment.Near;
        }

        public ISignalPaint SignalPaint { get; set; }

        public override void Refresh( )
        {
            if ( SignalPaint == null || SignalPaint.Real == null || SignalPaint.WidthSelection < 2 )
            {
                BorderStyle = BorderStyle.FixedSingle;
                Draw( grafx.Graphics );
                return;
            }

            BorderStyle = SignalPaint.WidthSelection > 1 ? BorderStyle.None : BorderStyle.FixedSingle;

            _fontWidth = GetSizeFont( );
            _font = new Font( FontFamily.GenericSansSerif, _fontWidth );
            _points = SignalPaint.WidthSelection - 1;
            _window = SignalPaint.Max - SignalPaint.Min;

            _kofNetHeight = GetKofNetHeight( );
            _fornamNumber = GetFormatNumber();
            _offsetLeft = GetOffsetLeft( grafx.Graphics );
            _offsetButtom = 2 * _fontWidth;
            _paintWidth = Width - _offsetLeft - _offsetRight;
            _paintHeight = Height - _offsetButtom - _offsetTop;
            _pointWidth = _points / _paintWidth;
            _pointHeight = _window / _paintHeight;
           
            _kofNetWidth = GetKofNetWidth( );

            Draw( grafx.Graphics );
            base.Refresh( );
        }

        public PointF GetPoint( Point point )
        {
            if ( SignalPaint == null || SignalPaint.Real == null || SignalPaint.WidthSelection < 2 )
                return new PointF( 0, 0 );
            RectangleF rect = new RectangleF( _offsetLeft, _offsetTop, _paintWidth, _paintHeight );
            if (!rect.Contains(point))
                return new PointF( 0, 0 );

            return new PointF( (int)Math.Round(SignalPaint.StartSelection + ( point.X - _offsetLeft ) * _pointWidth),
                SignalPaint.Max - ( point.Y - _offsetTop ) * _pointHeight );
        }

        protected override void OnResize( EventArgs eventargs )
        {
            base.OnResize( eventargs );
            context.MaximumBuffer = new Size( Width + 1, Height + 1 );
            if ( grafx != null )
            {
                grafx.Dispose( );
                grafx = null;
            }
            if ( Width > 0 && Height > 0 )
            {
                grafx = context.Allocate( CreateGraphics( ), new Rectangle( 0, 0, Width, Height ) );
                Refresh( );
            }
        }

        protected override void OnPaint( PaintEventArgs e )
        {
            grafx.Render( e.Graphics );
            if ( SignalPaint == null || SignalPaint.StartNewSelection == SignalPaint.StartSelection )
                return;
            e.Graphics.ResetTransform();
            e.Graphics.DrawLine( Pens.Black, _offsetLeft + SignalPaint.StartNewSelection / _pointWidth,
                _offsetTop, _offsetLeft + SignalPaint.StartNewSelection / _pointWidth, _offsetTop + _paintHeight );
            if ( Capture && !SignalPaint.IsNewSelection )
                e.Graphics.DrawLine( Pens.Black, _offsetLeft + SignalPaint.EndNewSelection / _pointWidth,
                _offsetTop, _offsetLeft + SignalPaint.EndNewSelection / _pointWidth, _offsetTop + _paintHeight );
        }

        protected override void OnPaintBackground( PaintEventArgs e )
        {
        }

        private void Draw( Graphics graphics )
        {
            graphics.Clear( Color.White );
            if ( SignalPaint == null || _points < 1 || SignalPaint.Real.Length <= _points )
                return;

            InitGraphics( graphics );

            if ( SignalPaint.IsNewSelection )
                graphics.FillRectangle( Brushes.Black, SignalPaint.StartNewSelection / _pointWidth, -SignalPaint.Max / _pointHeight,
                    SignalPaint.WidthNewSelection / _pointWidth, _paintHeight );

            DrawNetVertical( graphics );
            DrawNetHorizontal( graphics );
            graphics.DrawRectangle( Pens.Black, 0, -SignalPaint.Max / _pointHeight, _paintWidth - 1, _paintHeight );

            if ( _pointWidth >= 4 )
            {
                float maxReal = 0;
                float minReal = 0;
                float maxImag = 0;
                float minImag = 0;
                float end = 0;

                end += _pointWidth / 2;
                maxReal = minReal = SignalPaint.Real[0];
                maxImag = minImag = SignalPaint.Imag[0];

                for ( int i = 0, j = SignalPaint.StartSelection; i < _points; i++, j++ )
                {
                    if ( i >= Math.Floor( end ) )
                    {
                        end += _pointWidth / 2;
                        if ( SignalPaint.IsReal )
                        {
                            if ( maxReal - minReal < 1 )
                                maxReal += _pointHeight;
                            graphics.DrawLine( _realPen, end / _pointWidth - 0.25f, -minReal / _pointHeight, end / _pointWidth - 0.25f, -maxReal / _pointHeight );
                            maxReal = minReal = SignalPaint.Real[j - 1];
                        }
                        if ( SignalPaint.IsImag )
                        {
                            if ( maxImag - minImag < 1 )
                                maxImag += _pointHeight / 2;
                            graphics.DrawLine( _imagPen, end / _pointWidth - 0.25f, -minImag / _pointHeight, end / _pointWidth - 0.25f, -maxImag / _pointHeight );
                            maxImag = minImag = SignalPaint.Imag[j - 1];
                        }
                    }
                    else
                    {
                        if ( SignalPaint.IsReal )
                        {
                            if ( maxReal < SignalPaint.Real[j] )
                                maxReal = SignalPaint.Real[j];
                            if ( minReal > SignalPaint.Real[j] )
                                minReal = SignalPaint.Real[j];
                        }
                        if ( SignalPaint.IsImag )
                        {
                            if ( maxImag < SignalPaint.Imag[j] )
                                maxImag = SignalPaint.Imag[j];
                            if ( minImag > SignalPaint.Imag[j] )
                                minImag = SignalPaint.Imag[j];
                        }
                    }
                }
            }
            else
            {
                for ( int i = 0, j = SignalPaint.StartSelection; i < _points; i++, j++ )
                {
                    if ( SignalPaint.IsReal )
                        graphics.DrawLine( _realPen, i / _pointWidth, -SignalPaint.Real[j] / _pointHeight, ( i + 1 ) / _pointWidth, -SignalPaint.Real[j + 1] / _pointHeight );
                    if ( SignalPaint.IsImag )
                        graphics.DrawLine( _imagPen, i / _pointWidth, -SignalPaint.Imag[j] / _pointHeight, ( i + 1 ) / _pointWidth, -SignalPaint.Imag[j + 1] / _pointHeight );
                }
            }
        }

        private void DrawNetHorizontal( Graphics graphics )
        {
            double y = Math.Ceiling( SignalPaint.Min / _kofNetHeight ) * _kofNetHeight;
            while( y <= SignalPaint.Max )
            {
                float yPos = (float)( -y / _pointHeight );
                graphics.DrawLine( _netPen, 0, yPos, _paintWidth, yPos );
                if ( yPos > -SignalPaint.Max / _pointHeight + _fontWidth / 2 )
                    graphics.DrawString( y.ToString( _fornamNumber, _numberFormatInfo ), _font, Brushes.Black, -_fontWidth / 4, yPos, _sfRightCenter );
                y += _kofNetHeight;
            }
        }

        private void DrawNetVertical( Graphics graphics )
        {
            double xTime = Math.Ceiling( SignalPaint.StartSelection * SignalPaint.PointTime / _kofNetWidth ) * _kofNetWidth;
            double x = ( xTime / SignalPaint.PointTime - SignalPaint.StartSelection ) / _pointWidth;
            double deltaX = ( _kofNetWidth / SignalPaint.PointTime ) / _pointWidth;
            while ( x < _paintWidth )
            {
                TimeValue time = new TimeValue( xTime );
                graphics.DrawLine( _netPen, (float)x, -SignalPaint.Min / _pointHeight, (float)x, -SignalPaint.Max / _pointHeight );
                graphics.DrawString( time.ToString( ), _font, Brushes.Black, (float)x , - SignalPaint.Min / _pointHeight + _fontWidth / 4, _sfCenterTop );
                x += deltaX;
                xTime += _kofNetWidth;
            }
        }

        private double GetKofNetHeight( )
        {
            double kof = Math.Pow( 10, Math.Floor( Math.Log10( _window ) ) - 1 );
            if ( _window / kof > 50 )
                kof *= 5;
            else if ( _window / kof > 20 )
                kof *= 2;
            return kof;
        }

        private double GetKofNetWidth( )
        {
            double timeWindow = _points * SignalPaint.PointTime;
            double kof = Math.Pow( 10, Math.Floor( Math.Log10( timeWindow ) ) - 1 );
            double countDivision = ( _points / _pointWidth ) / ( _font.Size * 8 );
            if ( timeWindow / kof <= countDivision )
                return kof;
            if ( timeWindow / (kof * 2) <= countDivision )
                return kof * 2;
            return kof * 5;
        }

        private string GetFormatNumber( )
        {
            int log10 = (int)Math.Floor( Math.Log10( _kofNetHeight ) );
            if ( log10 >= 0 )
                return "F0";
            return "F" + ( (int)Math.Abs( log10 ) ).ToString( );
        }

        private float GetOffsetLeft( Graphics graphics )
        {
            float width = 0;
            double y = Math.Ceiling( SignalPaint.Min / _kofNetHeight ) * _kofNetHeight;
            while ( y < SignalPaint.Max )
            {
                SizeF sizeText = graphics.MeasureString( y.ToString( _fornamNumber, _numberFormatInfo ), _font );
                if ( width < sizeText.Width )
                    width = sizeText.Width;
                y += _kofNetHeight;
            }
            return width;
        }

        private float GetSizeFont()
        {
            float widthVertical = Height < 80 ? 1 : Height / 80.0f;
            float widthHorizontal = Width < 125 ? 1 : Width / 125.0f;
            return widthVertical > widthHorizontal ? widthVertical : widthHorizontal;
        }

        private void InitGraphics( Graphics graphics )
        {
            graphics.ResetTransform( );
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            //graphics.TextRenderingHint = TextRenderingHint.SystemDefault;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.TranslateTransform( _offsetLeft, SignalPaint.Max / _pointHeight );
        }
    }
}
