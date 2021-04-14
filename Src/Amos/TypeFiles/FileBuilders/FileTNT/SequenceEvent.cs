using System.IO;
using System.Text;

namespace Amos.TypeFiles.FileBuilders.FileTNT
{
    public class SequenceEvent
    {
        public string dataString;       // 0D Data String Length
        public string tableName0D;        // 0DTable Name Length
        public bool isTable0D;          // 0D Table Flag
        public string tableName1D;        // 1D Table Name Length
        public bool isTable1D;          // 1D Table Flag
        public string tableName2D;        // 2D Table Name Length
        public bool isTable2D;          // 2D Table Flag			
        public string tableName3D;        // 3D Table Name Length
        public bool isTable3D;          // 3D Table Flag
        public string tableName4D;        // 4D Table Name Length
        public bool isTable4D;          // 4D Table Flag
        public string tableName5D;        // 4D Table Name Length
        public bool isTable5D;          // 4D Table Flag
        public string fileAsynchronus;
        public bool isAqustion;
        public SequenceAcquation sequenceAcquation; 

        public SequenceEvent( BinaryReader bReader )
        {
            dataString = Encoding.ASCII.GetString( bReader.ReadBytes( bReader.ReadInt32( ) ) );

            tableName0D = Encoding.ASCII.GetString( bReader.ReadBytes( bReader.ReadInt32( ) ) );
            isTable0D = bReader.ReadInt32() == 1;

            tableName1D = Encoding.ASCII.GetString( bReader.ReadBytes( bReader.ReadInt32( ) ) );
            isTable1D = bReader.ReadInt32() == 1;

            tableName2D = Encoding.ASCII.GetString( bReader.ReadBytes( bReader.ReadInt32( ) ) );
            isTable2D = bReader.ReadInt32() == 1;

            tableName3D = Encoding.ASCII.GetString( bReader.ReadBytes( bReader.ReadInt32( ) ) );
            isTable3D = bReader.ReadInt32() == 1;

            tableName4D = Encoding.ASCII.GetString( bReader.ReadBytes( bReader.ReadInt32( ) ) );
            isTable4D = bReader.ReadInt32() == 1;
       
            tableName5D = Encoding.ASCII.GetString( bReader.ReadBytes( bReader.ReadInt32( ) ) );
            isTable5D = bReader.ReadInt32( ) == 1;

            fileAsynchronus = Encoding.ASCII.GetString( bReader.ReadBytes( bReader.ReadInt32( ) ) );

            isAqustion = bReader.ReadInt32( ) == 1;
            if ( isAqustion )
                sequenceAcquation = new SequenceAcquation( bReader );
        }
    }
}
