using System;
using System.Xml.Serialization;
using Tools.Interfaces;

namespace Data.DataObject.Sequence.Tables
{
    [Serializable]
    public class AsynchronusObject : IObjectSequence
    {
        public AsynchronusObject() { }

        public AsynchronusObject( string fileName )
        {
            FileName = fileName;
        }

        [XmlAttribute]
        public string FileName { get; set; }

        [XmlIgnore]
        public string ToolTip { get { return "Async Seq = " + FileName; } }

        public object Clone( )
        {
            AsynchronusObject obj = new AsynchronusObject( );
            obj.FileName = FileName;
            return obj;
        }
    }
}
