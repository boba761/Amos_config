using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Calculations.Variables;

namespace Amos.TypeFiles.FileBuilders
{
    public class GlobalVariableFileBuilder : FileBuilder
    {
        public override void Load( Stream stream )
        {
            XmlDocument docuument = new XmlDocument( );
            docuument.Load( stream );
            XmlReader reader = new XmlNodeReader( docuument.DocumentElement );
            XmlSerializer serializer = new XmlSerializer( typeof( List<GlobalVariable> ) );
            Document.Global.Variables = (List<GlobalVariable>)serializer.Deserialize( reader );
        }

        public override void Save( Stream stream )
        {
            XmlSerializer serializer = new XmlSerializer( typeof( List<GlobalVariable> ) );
            serializer.Serialize( stream, Document.Global.Variables );
        }
    }
}
