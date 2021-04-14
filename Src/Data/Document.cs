using System;
using System.IO;
using Data.DataObject;
using Data.FileBuilders;
using Tools;
using Localize;

namespace Data
{
    public class Document
    {
        private const string extensionFile = "MRI";

        public Document( )
        {
            Dashboard = new DashboardData( );
            Sequence = new SequenceData( );
            Signal = new SignalData( );
        }

        internal static Document LoadDocument { get; set; }

        public GlobalData Global { get; set; }

        public DashboardData Dashboard { get; set; }

        public SequenceData Sequence { get; set; }
        
        public SignalData Signal { get; set; }

        public void Refresh( )
        {
            Sequence.Refresh( );
            Dashboard.Refresh( );
            Signal.Refresh( );
        }

        public static Document Load( string fileName )
        {
            string ext = ( new FileInfo( fileName ) ).Extension.Remove( 0, 1 ).ToUpper( );
            if ( ext != extensionFile )
                throw new Exception( Local.GetString( "Erorr.NotCorrectFileExtensionMRI" ) );
            LoadDocument = new Document( );
            ( new FileConverter( LoadDocument, new MRIFileBuilder( ) ) ).Load( fileName );
            LoadDocument.Refresh( );
            return LoadDocument;
        }

        public void Save( string fileName )
        {
            string ext = ( new FileInfo( fileName ) ).Extension.Remove( 0, 1 ).ToUpper();
            if ( ext != extensionFile )
                throw new Exception( Local.GetString( "Erorr.NotCorrectFileExtensionMRI" ) );
            ( new FileConverter( this, new MRIFileBuilder( ) ) ).Save( fileName );
        }
    }
}
