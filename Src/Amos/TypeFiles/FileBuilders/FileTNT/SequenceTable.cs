using System.IO;
using System.Text;

namespace Amos.TypeFiles.FileBuilders.FileTNT
{
    public class SequenceTable
    {
        public string name;
        public string entry;
        public string incrementOperation;
        public string incrementValue;
        public string incrementScheme;
        public int repeatTime;
        public int typeOfTable;
        public int dimension;
        public int stepsPer360Cycle;
        public bool useIncrementList;
        public int valueType;
        public bool isAuto;
        public string start;
        public string increment;
        public string number;
        public string timePoint;
        public int question1;  // Неизвестный параметр

        public SequenceTable( BinaryReader bReader, float version )
        {
            name = Encoding.ASCII.GetString( bReader.ReadBytes( bReader.ReadInt32( ) ) );
            entry = Encoding.ASCII.GetString( bReader.ReadBytes( bReader.ReadInt32( ) ) );
            incrementOperation = Encoding.ASCII.GetString( bReader.ReadBytes( bReader.ReadInt32( ) ) );
            incrementValue = Encoding.ASCII.GetString( bReader.ReadBytes( bReader.ReadInt32( ) ) );
            incrementScheme = Encoding.ASCII.GetString( bReader.ReadBytes( bReader.ReadInt32( ) ) );
            repeatTime = bReader.ReadInt32();
            typeOfTable = bReader.ReadInt32( );
            dimension = bReader.ReadInt32( );
            stepsPer360Cycle = bReader.ReadInt32( );
            useIncrementList = bReader.ReadInt32( ) == 1;
            valueType = bReader.ReadInt32( );
            timePoint = Encoding.ASCII.GetString( bReader.ReadBytes( bReader.ReadInt32( ) ) );
            isAuto = bReader.ReadInt32( ) == 1;
            question1 = bReader.ReadInt32( );
            number = Encoding.ASCII.GetString( bReader.ReadBytes( bReader.ReadInt32( ) ) );
            increment = Encoding.ASCII.GetString( bReader.ReadBytes( bReader.ReadInt32( ) ) );
            start = Encoding.ASCII.GetString( bReader.ReadBytes( bReader.ReadInt32( ) ) );
            if ( version >= 1.15f )
                bReader.ReadInt32( );
        }
    }
}
