using System;
using System.IO;
using System.Text;

namespace Amos.TypeFiles.FileBuilders.FileTNT
{
    public class SequenceRow
    {
        public int columns;             // Number of Columns
        public int address;             // Address
        public int bitLength;           // BitLength
        public int iconType;            // Icon Library Type
        public int visible;             // Visible Flag
        public int privateData;         // Private Data
        public int group;               // Group
        public string defalut;          // Defalut String
        public string label;            // Label String
        public SequenceEvent[] sequenceEvent;

        public SequenceRow(BinaryReader bReader)
        {
            columns = bReader.ReadInt32();
            address = bReader.ReadInt32();
            bitLength = bReader.ReadInt32();
            iconType = bReader.ReadInt32();
            visible = bReader.ReadInt32();
            privateData = bReader.ReadInt32();
            group = bReader.ReadInt32();
            defalut = Encoding.ASCII.GetString( bReader.ReadBytes( bReader.ReadInt32( ) ) );
            label = Encoding.ASCII.GetString( bReader.ReadBytes( bReader.ReadInt32( ) ) );

            sequenceEvent = new SequenceEvent[columns];
            for ( int i = 0; i < columns; i++ )
                sequenceEvent[i] = new SequenceEvent( bReader );
        }
    }
}
