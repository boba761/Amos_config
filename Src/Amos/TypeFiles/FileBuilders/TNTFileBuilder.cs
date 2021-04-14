using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Amos.TypeFiles.FileBuilders.FileTNT;

namespace Amos.TypeFiles.FileBuilders
{
    public class TNTFileBuilder : FileBuilder
    {
        private string version;

        public override void Load(Stream stream)
        {
            int length;
            BinaryReader bReader = new BinaryReader( stream );
            version = Encoding.ASCII.GetString( bReader.ReadBytes( 8 ) );

            string tag = Encoding.ASCII.GetString( bReader.ReadBytes( 4 ) );
            if ( bReader.ReadInt32( ) == 1 )
            {
                length = bReader.ReadInt32( );
                TECMAG TMAG = new TECMAG( bReader, Document );
            }

            tag = Encoding.ASCII.GetString( bReader.ReadBytes( 4 ) );
            if ( bReader.ReadInt32( ) == 1 )
            {
                length = bReader.ReadInt32( );
                Document.Signal.SetData( bReader.ReadFloats( length / 4 ) );
            }

            tag = Encoding.ASCII.GetString( bReader.ReadBytes( 4 ) );
            if ( bReader.ReadInt32( ) == 1 )
            {
                length = bReader.ReadInt32( );
                TECMAG2 TMG2 = new TECMAG2( bReader );
            }

            tag = Encoding.ASCII.GetString( bReader.ReadBytes( 4 ) );
            if ( bReader.ReadInt32( ) == 1 )
                Document.Sequence.SetDataImport( new PulseSequence( bReader ) );
        }

        public override void Save(Stream stream)
        {
            throw new NotImplementedException();
        }
    }
}
