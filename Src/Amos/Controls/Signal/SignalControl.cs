using System;
using System.Drawing;
using System.Windows.Forms;
using Amos.Data;
using Amos.Interfaces;
using Calculations.Values;

namespace Amos.Controls.Signal
{
    public partial class SignalControl : UserControl, ISignalPaint
    { 
        public delegate void OnChangeZoom( float zoom );
        
        public static Color colorReal = Color.Red;
        public static Color colorImag = Color.Green;
        public static Color colorNet = Color.Gray;

        private int _startX;
        private int _endX;
        private float _zoom;
        private bool _isReal;
        private bool _isImag;
        private int _position2D;
        private int _position3D;
        private int _position4D;
        private float[] _real;
        private float[] _imag;

        private SignalData _signalData;

        public event OnChangeZoom onChangeZoom; 

        public SignalControl( )
        {
            InitializeComponent( );
            _isReal = true;
            smallBox.SignalPaint = this;
            largeBox.SignalPaint = this;
        }

        public SignalData Signal
        {
            set
            {
                _signalData = value;
                if ( _signalData != null )
                {
                    Point1D = _signalData.Point1D;
                    SetInfo( );
                    SetData( );
                    Zoom = 1;
                }
                _position2D = _position3D = _position4D = 0;
                Refresh( );
            }
        }

        public float Zoom
        {
            get { return _zoom; }
            set 
            {
                _zoom = value;
                if ( _zoom < 1 )
                    _zoom = 1;
                if ( _signalData == null )
                    return; 
                scrollBar.Maximum = Point1D - 1;
                scrollBar.LargeChange = (int)Math.Floor(Point1D / _zoom );
                scrollBar.SmallChange = (int)Math.Floor( scrollBar.LargeChange / 10.0 );
                if ( scrollBar.Value >= scrollBar.Maximum - scrollBar.LargeChange + 1 )
                    scrollBar.Value = scrollBar.Maximum - scrollBar.LargeChange + 1;
                scrollBar.Enabled = scrollBar.LargeChange != scrollBar.Maximum + 1;
                if ( onChangeZoom != null )
                    onChangeZoom( _zoom );
                smallBox.Refresh( );
                largeBox.Refresh( );
            }
        }

        public bool IsReal
        {
            get { return _isReal; }
            set 
            {
                _isReal = value;
                smallBox.Refresh( );
                Refresh( );
            }
        }

        public bool IsImag
        {
            get { return _isImag; }
            set
            {
                _isImag = value;
                smallBox.Refresh( );
                largeBox.Refresh( );
            }
        }

        public double PointTime 
        { 
            get 
            {
                if ( _signalData == null )
                    return 0;
                return _signalData.AcqTime / Point1D; 
            } 
        }

        public bool IsSelection { get { return scrollBar.LargeChange < scrollBar.Maximum; } }

        public int StartSelection { get { return scrollBar.Value; } }

        public int WidthSelection { get { return scrollBar.LargeChange; } } 
        
        public bool IsNewSelection { get; private set; }

        public int StartNewSelection { get { return _startX - scrollBar.Value; } }

        public int EndNewSelection { get { return _endX - scrollBar.Value; } }

        public int WidthNewSelection { get { return _endX - _startX; } } 

        public float[] Real { get { return _real; } }

        public float[] Imag { get { return _imag; } }

        public float Min { get; private set; }

        public float Max { get; private set; }

        public int Point1D { get; private set; }

        public int Position2D
        {
            get { return _position2D + 1; }
            set
            {
                _position2D = value - 1;
                SetData( );
                smallBox.Refresh( );
                largeBox.Refresh( );
            }
        }

        public int Position3D
        {
            get { return _position3D + 1; }
            set
            {
                _position3D = value - 1;
                SetData( );
                smallBox.Refresh( );
                largeBox.Refresh( );
            }
        }

        public int Position4D
        {
            get { return _position4D + 1; }
            set
            {
                _position4D = value - 1;
                SetData( );
                smallBox.Refresh( );
                largeBox.Refresh( );
            }
        }

        public string Position
        {
            get 
            {
                string text = string.Empty ;
                if ( _signalData.Point2D > 1 )
                    text = Position2D.ToString();
                if ( _signalData.Point3D > 1 )
                    text += ":" + Position3D.ToString( );
                if ( _signalData.Point4D > 1 )
                    text += ":" + Position4D.ToString( );
                return text;
            }
        }

        private void SetInfo( )
        {
            string text = string.Format( "Point 1D: {0}\n", Point1D );
            if ( _signalData.Point2D > 1 || _signalData.Point3D > 1 || _signalData.Point4D > 1 )
                text += string.Format( "Point 2D: {0}\n", _signalData.Point2D );
            if ( _signalData.Point3D > 1 || _signalData.Point4D > 1 )
                text += string.Format( "Point 3D: {0}\n", _signalData.Point3D );
            if ( _signalData.Point4D > 1 )
                text += string.Format( "Point 4D: {0}", _signalData.Point4D );
            point1DLabel.Text = text;
            mouselabel.Text = string.Empty;
            cupsorPointLabel.Text = string.Empty;
        }

        private void SetData( )
        {
            _real = _signalData.GetReal( _position2D, _position3D, _position4D );
            _imag = _signalData.GetImag( _position2D, _position3D, _position4D );

            FindMaxMin( );
        }

        private void FindMaxMin( )
        {
            Max = Min = _real[0];
            foreach ( float val in _real )
            {
                if ( Max < val )
                    Max = val;
                if ( Min > val )
                    Min = val;
            }
            foreach ( float val in _imag )
            {
                if ( Max < val )
                    Max = val;
                if ( Min > val )
                    Min = val;
            }

            float window = Max - Min; 
            if ( window < 1 )
                window = 1;

            Max += window * 0.05f;
            Min -= window * 0.05f;
            window = Max - Min;
        }

        private void scrollBar_ValueChanged( object sender, EventArgs e )
        {
            smallBox.Refresh( );
            largeBox.Refresh( );
        }
        
        private void largeBox_MouseLeave( object sender, EventArgs e )
        {
            mouselabel.Text = string.Empty;
        }

        private void largeBox_MouseMove( object sender, MouseEventArgs e )
        {
            PointF point = largeBox.GetPoint( e.Location );
            mouselabel.Text = string.Format( "Mouse Position:\nX: {0:F0}\nY: {1:F2}", point.X + 1, point.Y );
            if ( largeBox.Capture && !IsNewSelection )
            {
                _endX = (int)point.X;
                largeBox.Invalidate( );
                SetCursorPoint( true );
            }
        }

        private void largeBox_MouseDown( object sender, MouseEventArgs e )
        {
            PointF point = largeBox.GetPoint( e.Location );
            if ( IsNewSelection == false )
            {
                _startX = (int)point.X;
                largeBox.Capture = true;
                largeBox.Invalidate( );
                SetCursorPoint( );
            }
        }

        private void largeBox_MouseUp( object sender, MouseEventArgs e )
        {
            PointF point = largeBox.GetPoint( e.Location );
            if ( largeBox.Capture && IsNewSelection == false)
            {
                largeBox.Capture = false;
                _endX = (int)point.X;
                if ( Math.Abs( _startX - _endX ) < 2 )
                {
                    _startX = _endX;
                    largeBox.Refresh( );
                    SetCursorPoint( );
                    return;
                }
                else if ( _startX > _endX )
                {
                    int tmp = _startX;
                    _startX = _endX;
                    _endX = tmp;
                }
                IsNewSelection = true;
                largeBox.Refresh( );
                SetCursorPoint( true );
            }
            else if ( IsNewSelection )
            {
                largeBox.Capture = false;
                IsNewSelection = false;
                int x = (int)point.X;
                if ( x > _startX && x < _endX )
                {
                    Zoom = (float)scrollBar.Maximum / ( _endX - _startX );
                    scrollBar.Value = _startX;
                    cupsorPointLabel.Text = string.Empty;
                }
                else
                {    
                    _startX = _endX = x;
                    SetCursorPoint( );
                }
                largeBox.Refresh( );
            } 
        }

        private void SetCursorPoint(bool isLeft = false)
        {
            string text = string.Empty;
            int x = _startX;
            if (isLeft)
            {
                 x = _startX > _endX ? _endX : _startX;
                text = string.Format( "Left point {0}\n", x + 1 );
            }
            else
                text = string.Format( "Cursor point {0}\n", x + 1 );
            text += string.Format( "{0}\nReal {1:F3}\nImag {2:F3}", ( new TimeValue( PointTime * x ) ).ToString( ), Real[x], Imag[x] );
            cupsorPointLabel.Text = text;
        }
    }
}
