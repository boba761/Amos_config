using System.Xml.Serialization;

namespace Data.DataObject.Sequence
{
    public struct sStreamData
    {
        public const string NameIconType = "NAME"; 
        public const string DelayIconType = "DE";
        public const string LoopIconType = "LP";
        public const string ATIconType = "AT";
        public const string A2IconType = "A2";
        public const string ACQIconType = "ACQ";
        public const string ETIconType = "ET";
        public const string GAIconType = "GA";
        public const string GRIconType = "GR";
        public const string G2IconType = "G2";
        public const string LDIconType = "LD";
        public const string O1IconType = "O1";
        public const string TXIconType = "TX";
        public const string RMIconType = "RM";
        public const string R2IconType = "R2";
        public const string PHIconType = "PH";
        public const string P2IconType = "P2";
        public const string PSIconType = "PS";
        public const string SHIconType = "SH";

        [XmlAttribute]
        public string Name;         // <1> = Name

        [XmlIgnore]
        public int Address;         // <3> = Start Address

        [XmlAttribute]
        public int BitLength;       // <4> BitLength

        [XmlAttribute]
        public string IconType;     // <8> = Icon type

        [XmlAttribute]
        public bool Visible;        // <9> = Visible

        [XmlAttribute]
        public int PrivateData;     // <10> = Private Data

        [XmlAttribute]
        public int Group;           // <11> = Group Number

        public sStreamData( string name, string iconType )
        {
            Name = name;
            Address = 0;
            BitLength = 1;
            IconType = iconType;
            Visible = true;
            PrivateData = 0;
            Group = 0;
        }

        //public sStreamData( SequenceRow sequenceRow )
        //{
        //    Name = sequenceRow.label;
        //    Address = sequenceRow.address;
        //    BitLength = sequenceRow.bitLength;
        //    Visible = sequenceRow.visible == 1;
        //    PrivateData = sequenceRow.privateData;
        //    Group = sequenceRow.group;
        //    IconType = IntToIconType( sequenceRow.iconType );
        //}

        public sStreamData( string[] strings )
        {
            Name = strings[0];
            Address = int.Parse(strings[2]);
            BitLength = int.Parse( strings[3] );
            IconType = strings[7];
            Visible = int.Parse( strings[8] ) == 1;
            PrivateData = int.Parse( strings[9] );
            Group = int.Parse( strings[10] );
        }

        private static string IntToIconType( int type )
        {
            switch ( type )
            {
            case 0x00000000:
                return NameIconType;
            case 0x00000001:
                return DelayIconType;
            case 0x0000000D:
                return LoopIconType;
            case 0x00004154:
                return ATIconType;
            case 0x00004132:
                return A2IconType;
            case 0x00004143:
                return ACQIconType;
            case 0x00004554:
                return ETIconType;
            case 0x00004741:
                return GAIconType;
            case 0x00004752:
                return GRIconType;
            case 0x00004732:
                return G2IconType;
            case 0x00004C44:
                return LDIconType;
            case 0x00004F31:
                return O1IconType;
            case 0x00005458:
                return TXIconType;
            case 0x0000524D:
                return RMIconType;
            case 0x00005232:
                return R2IconType;
            case 0x00005048:
                return PHIconType;
            case 0x00005032:
                return P2IconType;
            case 0x00005053:
                return PSIconType;
            case 0x00005348:
                return SHIconType;
            default:
                return "";
            }
        }
    }
}
