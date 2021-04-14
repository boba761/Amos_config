using System.IO;
using System.Text;

namespace Amos.TypeFiles.FileBuilders.FileTNT
{
    public class SequenceParameterPages
    {
        public int numberOfPages;
        public ParametersPage[] parametersPage;

        public SequenceParameterPages( BinaryReader bReader )
        {
            numberOfPages = bReader.ReadInt32();
            parametersPage = new ParametersPage[numberOfPages];
            for ( int i = 0; i < numberOfPages; i++ )
                parametersPage[i] = new ParametersPage( bReader );
        }

        public class ParametersPage
        {
            public string name;
            public int numberOnPage;
            public string[] parameterNames;

            public ParametersPage( BinaryReader bReader )
            {
                name = Encoding.ASCII.GetString( bReader.ReadBytes( bReader.ReadInt32( ) ) );
                numberOnPage = bReader.ReadInt32( );
                parameterNames = new string[numberOnPage];
                for ( int i = 0; i < numberOnPage; i++ )
                    parameterNames[i] = Encoding.ASCII.GetString( bReader.ReadBytes( bReader.ReadInt32( ) ) );
            }
        }
    }
}
