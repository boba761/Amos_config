using System;
using System.Xml.Serialization;
using Tools.Interfaces;

namespace Data.DataObject.Sequence.Tables
{
    [Serializable]
    public class AcquisitionTableSequence : TableSequence
    {
        public AcquisitionTableSequence( ) { }

        public AcquisitionTableSequence( IObjectStream @object, string name = null)
            : base( @object )
        {
            if (name != null) 
                Name = name;
        }

        [XmlIgnore]
        public override string ClassTable { get { return "Acquisition Table"; } }
        [XmlIgnore]
        public override string Description { get { return "Use format\nSW1 Filter1"; } }

        protected override TableSequence New( )
        {
            return new AcquisitionTableSequence( );
        }
    }
}
