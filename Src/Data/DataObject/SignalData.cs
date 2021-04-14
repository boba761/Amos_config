using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace Data.DataObject
{
    [XmlRoot]
    public class SignalData
    {
        private float[] _real;
        private float[] _imag;

        public SignalData( )
        {
            _real = new float[1];
            _imag = new float[1];
        }

        [XmlIgnore]
        internal int Point1D 
        { 
            get 
            {
                if ( Document.LoadDocument == null || Document.LoadDocument.Dashboard.Contains( "Acq. Points" ) == false )
                    return 1;
                return Convert.ToInt32( Document.LoadDocument.Dashboard["Acq. Points"].Value.Data ); 
            } 
        }

        [XmlIgnore]
        internal int Point2D 
        { 
            get 
            {
                if ( Document.LoadDocument == null || Document.LoadDocument.Dashboard.Contains( "Actual Points 2D" ) == false )
                    return 1;
                return Convert.ToInt32( Document.LoadDocument.Dashboard["Actual Points 2D"].Value.Data ); 
            } 
        }

        [XmlIgnore]
        internal int Point3D
        {
            get
            {
                if ( Document.LoadDocument == null || Document.LoadDocument.Dashboard.Contains( "Actual Points 3D" ) == false )
                    return 1;
                return Convert.ToInt32( Document.LoadDocument.Dashboard["Actual Points 3D"].Value.Data );
            }
        }

        [XmlIgnore]
        internal int Point4D
        {
            get
            {
                if ( Document.LoadDocument == null || Document.LoadDocument.Dashboard.Contains( "Actual Points 4D" ) == false )
                    return 1;
                return Convert.ToInt32( Document.LoadDocument.Dashboard["Actual Points 4D"].Value.Data );
            }
        }

        [XmlIgnore]
        internal double AcqTime  
        { 
            get 
            {
                if ( Document.LoadDocument == null || Document.LoadDocument.Dashboard.Contains( "Acq. Time" ) == false )
                    return 1;
                return Convert.ToInt32( Document.LoadDocument.Dashboard["Acq. Time"].Value.Data ); 
            } 
        }

        [XmlElement("RealData")]
        internal byte[] RealBytes 
        {
            get 
            {
                MemoryStream memoryStream = new MemoryStream( _real.Length * sizeof(float) );
                using ( BinaryWriter binaryWriter = new BinaryWriter( memoryStream ) )
                {
                    foreach ( float val in _real )
                        binaryWriter.Write( val );
                }
                return memoryStream.ToArray( );
            }
            set 
            {
                _real = new float[value.Length / 4];
                MemoryStream memoryStream = new MemoryStream( value );
                using ( BinaryReader binaryReader = new BinaryReader( memoryStream ) )
                {
                    for ( int i = 0; i < _real.Length; i++ )
                        _real[i] = binaryReader.ReadSingle( );
                }
            }
        }

        [XmlElement( "ImagData" )]
        internal byte[] ImagBytes
        {
            get
            {
                MemoryStream memoryStream = new MemoryStream( _imag.Length * sizeof( float ) );
                using ( BinaryWriter binaryWriter = new BinaryWriter( memoryStream ) )
                {
                    foreach ( float val in _imag )
                        binaryWriter.Write( val );
                }
                return memoryStream.ToArray( );
            }
            set
            {
                _imag = new float[value.Length / 4];
                MemoryStream memoryStream = new MemoryStream( value );
                using ( BinaryReader binaryReader = new BinaryReader( memoryStream ) )
                {
                    for ( int i = 0; i < _imag.Length; i++ )
                        _imag[i] = binaryReader.ReadSingle( );
                }
            }
        }

        /// <summary>
        /// Выполняем действие по созданию объекта класса
        /// </summary>
        internal void Create( )
        {

        }

        /// <summary>
        /// Выполняем действия по закрытию объекта класса
        /// </summary>
        internal void Close( )
        {
        }

        internal void Refresh( )
        {
            
        }

        internal void SetData( float[] data )
        {
            _real = new float[data.Length / 2];
            _imag = new float[data.Length / 2];
            for ( int i = 0, j = 0; i < data.Length / 2; i++ )
            {
                _real[i] = data[j++];
                _imag[i] = data[j++];
            }
        }

        public float[] GetReal( int pos2D, int pos3D, int pos4D )
        { 
            float[] data = new float[Point1D];
            int pos = Point1D * pos2D + Point1D * Point2D * pos3D + Point1D * Point2D * Point4D * pos4D;
            if ( pos > _real.Length )
            {
                for ( int i = 0; i < Point1D; i++ )
                    data[i] = 1;
                return data;
            }
            for ( int i = 0, j = pos; i < Point1D && j < _real.Length; i++, j++ )
                data[i] = _real[j];
            return data;
        }

        public float[] GetImag( int pos2D, int pos3D, int pos4D )
        {
            float[] data = new float[Point1D];
            int pos = Point1D * pos2D + Point1D * Point2D * pos3D + Point1D * Point2D * Point4D * pos4D;
            if ( pos > _imag.Length )
            {
                for ( int i = 0; i < Point1D; i++ )
                    data[i] = 1;
                return data;
            }
            for ( int i = 0, j = pos; i < Point1D && j < _imag.Length; i++, j++ )
                data[i] = _imag[j];
            return data;
        }
    }
}
