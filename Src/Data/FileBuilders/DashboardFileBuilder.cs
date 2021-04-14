using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Tools;
using Data.DataObject;

namespace Data.FileBuilders
{
    public class DashboardFileBuilder : FileBuilder
    {
        public DashboardFileBuilder( object @object )
        {
            Object = @object;
        }

        public override void Load( Stream stream )
        {
            XmlDocument docuument = new XmlDocument( );
            docuument.Load( stream );
            XmlReader reader = new XmlNodeReader( docuument.DocumentElement );
            XmlSerializer serializer = new XmlSerializer( typeof( DashboardData ) );
            ( (Document)Object ).Dashboard = (DashboardData)serializer.Deserialize( reader );
        }

        public override void Save( Stream stream )
        {
            XmlSerializer serializer = new XmlSerializer( typeof( DashboardData ) );
            serializer.Serialize( stream, ( (Document)Object ).Dashboard );
        }
    }
}
