using System.IO;
using System.Text;

namespace Amos.TypeFiles.FileBuilders.FileTNT
{
    public class SequenceAcquation
    {
        public string asqPoint;
        public string sweep;
        public string filter;
        public string dwellTime;
        public string asqTime;
        public bool isLinkDashboard;

        public SequenceAcquation( BinaryReader bReader )
        {
            int length = bReader.ReadInt32( );
            asqPoint = Encoding.ASCII.GetString( bReader.ReadBytes( length ) );
            length = bReader.ReadInt32( );
            sweep = Encoding.ASCII.GetString( bReader.ReadBytes( length ) );
            length = bReader.ReadInt32( );
            filter = Encoding.ASCII.GetString( bReader.ReadBytes( length ) );
            length = bReader.ReadInt32( );
            dwellTime = Encoding.ASCII.GetString( bReader.ReadBytes( length ) );
            length = bReader.ReadInt32( );
            asqTime = Encoding.ASCII.GetString( bReader.ReadBytes( length ) );
            isLinkDashboard = bReader.ReadInt32( ) == 1;
            bReader.ReadInt16( );
        }
    }
}
