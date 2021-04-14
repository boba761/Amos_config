using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Tools;
using Calculations.Variables;
using Data.DataObject;

namespace Data.FileBuilders
{
    public class GlobalVariableFileBuilder : FileBuilder
    {
        public override void Load( Stream stream )
        {
            XmlDocument docuument = new XmlDocument( );
            docuument.Load( stream );
            XmlReader reader = new XmlNodeReader( docuument.DocumentElement );
            XmlSerializer serializer = new XmlSerializer( typeof( GlobalData ) );
            ( (Document)Object ).Global = (GlobalData)serializer.Deserialize( reader );
        }

        public override void Save( Stream stream )
        {
            XmlSerializer serializer = new XmlSerializer( typeof( GlobalData ) );
            serializer.Serialize( stream, ( (Document)Object ).Global );
        }
    }
}
