using System.IO;
using System.Text;

namespace Amos.TypeFiles.FileBuilders.FileTNT
{
    public class SequenceParameters
    {
        public int numberOfVariableSequence;
        public int numberOfVariableTable;
        public Parameter[] variableSequence;
        public Parameter[] variableTable;

        public SequenceParameters( BinaryReader bReader )
        {
            numberOfVariableSequence = bReader.ReadInt32( );
            variableSequence = new Parameter[numberOfVariableSequence];
            for ( int i = 0; i < numberOfVariableSequence; i++ )
                variableSequence[i] = new Parameter( bReader );

            if ( Encoding.ASCII.GetString( bReader.ReadBytes( 4 ) ) == "TBLV" )
            {
                numberOfVariableTable = bReader.ReadInt32( );
                variableTable = new Parameter[numberOfVariableTable];
                for ( int i = 0; i < numberOfVariableTable; i++ )
                    variableTable[i] = new Parameter( bReader );
            }
        }

        public class Parameter
        {
            public string name;
            public string value;
            public int type;
            public string minimum;
            public string maximum;
            public bool isReadOnly;

            public Parameter( BinaryReader bReader )
            {
                name = Encoding.ASCII.GetString( bReader.ReadBytes( bReader.ReadInt32( ) ) );
                type = bReader.ReadInt32( );
                value = Encoding.ASCII.GetString( bReader.ReadBytes( bReader.ReadInt32( ) ) );
                type = bReader.ReadInt32( );
                minimum = Encoding.ASCII.GetString( bReader.ReadBytes( bReader.ReadInt32( ) ) );
                maximum = Encoding.ASCII.GetString( bReader.ReadBytes( bReader.ReadInt32( ) ) );
                isReadOnly = bReader.ReadInt32( ) == 1;
                string tmp;
                tmp = Encoding.ASCII.GetString( bReader.ReadBytes( bReader.ReadInt32( ) ) );
                tmp = Encoding.ASCII.GetString( bReader.ReadBytes( bReader.ReadInt32( ) ) );
                tmp = Encoding.ASCII.GetString( bReader.ReadBytes( bReader.ReadInt32( ) ) );
                tmp = Encoding.ASCII.GetString( bReader.ReadBytes( bReader.ReadInt32( ) ) );
                tmp = Encoding.ASCII.GetString( bReader.ReadBytes( bReader.ReadInt32( ) ) );
                bReader.ReadInt32( );
                bReader.ReadInt32( );
                bReader.ReadInt32( );
            }
        }
    }
}
