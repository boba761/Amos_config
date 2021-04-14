using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Amos.Interfaces;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace Amos.Controls.Signal
{
    public class SmallSignalPanel : Panel
    {
        private BufferedGraphicsContext context;
        private BufferedGraphics grafx;
        private int _points;
        private float _window;
        private float _pointHeight;
        private float _pointWidth;
        private Pen _realPen;
        private Pen _imagPen;
        private Font _font;

        public SmallSignalPanel( )
        {
            context = BufferedGraphicsManager.Current;
            context.MaximumBuffer = new Size( Width + 1, Height + 1 );
            _realPen = new Pen( SignalControl.colorReal );
            _imagPen = new Pen( SignalControl.colorImag );
            _font = new Font( FontFamily.GenericSansSerif, 12, FontStyle.Bold );
        }

        public ISignalPaint SignalPaint { get; set; }

        public override void Refresh( )
        {
            if ( SignalPaint == null || SignalPaint.Real == null)
                return;

            _points = SignalPaint.Real.Length - 1;
            _window = SignalPaint.Max - SignalPaint.Min;
            _pointHeight = (float)_window / Height;
            _pointWidth = (float)_points / Width;

            Draw( grafx.Graphics );
            base.Refresh( );
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
            grafx = context.Allocate( CreateGraphics( ), new Rectangle( 0, 0, Width, Height ) );
            Refresh( );
        }

        protected override void OnPaint( PaintEventArgs e )
        {
            grafx.Render( e.Graphics );
        }

        protected override void OnPaintBackground( PaintEventArgs e )
        {
        }

        private void Draw( Graphics graphics )
        {
            graphics.Clear( Color.White );
            if ( SignalPaint == null || _points <= 1 )
                return;

            InitGraphics( graphics );

            if ( SignalPaint.IsSelection )
                graphics.FillRectangle( Brushes.Black, SignalPaint.StartSelection, -SignalPaint.Max, SignalPaint.WidthSelection, _window );

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

                for ( int i = 0; i <= _points; i++ )
                {
                    if ( i >= Math.Floor( end ) )
                    {
                        if ( SignalPaint.IsReal )
                        {
                            if ( maxReal - minReal < 1 )
                                maxReal += _pointHeight;
                            graphics.DrawLine( _realPen, end - _pointWidth / 4, -minReal, end - _pointWidth / 4, -maxReal );
                            maxReal = minReal = SignalPaint.Real[i - 1];
                        }
                        if ( SignalPaint.IsImag )
                        {
                            if ( maxImag - minImag < 1 )
                                maxImag += _pointHeight / 2;
                            graphics.DrawLine( _imagPen, end - _pointWidth / 4, -minImag, end - _pointWidth / 4, -maxImag );
                            maxImag = minImag = SignalPaint.Imag[i - 1];
                        }
                        end += _pointWidth / 2;
                    }
                    else
                    {
                        if ( SignalPaint.IsReal )
                        {
                            if ( maxReal < SignalPaint.Real[i] )
                                maxReal = SignalPaint.Real[i];
                            if ( minReal > SignalPaint.Real[i] )
                                minReal = SignalPaint.Real[i];
                        }
                        if ( SignalPaint.IsImag )
                        {
                            if ( maxImag < SignalPaint.Imag[i] )
                                maxImag = SignalPaint.Imag[i];
                            if ( minImag > SignalPaint.Imag[i] )
                                minImag = SignalPaint.Imag[i];
                        }
                    }
                }
            }
            else
            {
                for ( int i = 0; i < _points; i++ )
                {
                    if ( SignalPaint.IsReal )
                        graphics.DrawLine( _realPen, i, -SignalPaint.Real[i], i + 1, -SignalPaint.Real[i + 1] );
                    if ( SignalPaint.IsImag )
                        graphics.DrawLine( _imagPen, i, -SignalPaint.Imag[i], i + 1, -SignalPaint.Imag[i + 1] );
                }
            }

            DrawPointNumber( graphics );
        }

        private void DrawPointNumber( Graphics graphics )
        {
            if ( SignalPaint.Position == string.Empty )
                return;
            StringFormat sfLeftTop = new StringFormat();
            GraphicsPath clipPath = new GraphicsPath( );
            graphics.ResetTransform( );
            clipPath.AddString(SignalPaint.Position, FontFamily.GenericSansSerif, (int)FontStyle.Bold, 18, new Point( 0, 0) , sfLeftTop);
            graphics.FillPath( Brushes.Black, clipPath );
            graphics.DrawPath( Pens.LightGray, clipPath );
        }

        private void InitGraphics( Graphics graphics )
        {
            graphics.ResetTransform( );
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.TextRenderingHint = TextRenderingHint.SystemDefault;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.ScaleTransform( (float)Width / _points, Height / ( SignalPaint.Max - SignalPaint.Min ) );
            graphics.TranslateTransform( 0, SignalPaint.Max );
        }
    }
}
