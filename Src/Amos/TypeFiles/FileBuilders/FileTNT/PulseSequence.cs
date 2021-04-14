using System.IO;
using System.Text;
using System.Globalization;
using System;

namespace Amos.TypeFiles.FileBuilders.FileTNT
{
    public class PulseSequence
    {
        public string SequenceID;		// “1.04 BIN”
        public string fileName;             // File Name
        public int rows;                    // Number of Rows
        public int columns;                 // Number of Columns
        public SequenceRow[] sequenceRows;  // Number of Rows * (variable length)
        public SequenceTable[] sequenceTables;
        public SequenceParameterPages sequenceParameterPages;
        public SequenceParameters sequenceParameters;

        public PulseSequence( BinaryReader bReader )
        {
            int length;
            float version;
            NumberFormatInfo numberFormatInfo = new NumberFormatInfo( );
            numberFormatInfo.NumberDecimalSeparator = ".";

            SequenceID = Encoding.ASCII.GetString(bReader.ReadBytes(8));
            version = float.Parse( SequenceID.Substring( 0, SequenceID.IndexOf( " " ) ), numberFormatInfo );
            if ( version <= 1.129 )
                throw new Exception( "Old version data of pulse sequence." );

            length = bReader.ReadInt32();
            fileName = Encoding.ASCII.GetString(bReader.ReadBytes(length));
            rows = bReader.ReadInt32();
            columns = bReader.ReadInt32();

            sequenceRows = new SequenceRow[rows];
            for (int i = 0; i < rows; i++)
                sequenceRows[i] = new SequenceRow( bReader );

            if ( version >= 1.14f )
            {
                length = bReader.ReadInt32( );
                bReader.ReadInts( length );
            }

            int tables = bReader.ReadInt32( );
            sequenceTables = new SequenceTable[tables];
            for ( int i = 0; i < tables; i++ )
                sequenceTables[i] = new SequenceTable( bReader, version );

            sequenceParameterPages = new SequenceParameterPages( bReader );
            sequenceParameters = new SequenceParameters( bReader );
        }
    }
}
