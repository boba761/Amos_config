using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Amos.Data;

namespace Amos.TypeFiles.FileBuilders
{
    public class SignalFileBuilder : FileBuilder
    {
        public override void Load( Stream stream )
        {
            XmlDocument docuument = new XmlDocument( );
            docuument.Load( stream );
            XmlReader reader = new XmlNodeReader( docuument.DocumentElement );
            XmlSerializer serializer = new XmlSerializer( typeof( SignalData ) );
            Document.Signal = (SignalData)serializer.Deserialize( reader );
        }

        public override void Save( Stream stream )
        {
            XmlSerializer serializer = new XmlSerializer( typeof( SignalData ) );
            serializer.Serialize( stream, Document.Signal );
        }
    }
}
