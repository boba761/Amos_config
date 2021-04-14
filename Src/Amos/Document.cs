using System;
using System.IO;
using Amos.Commands;
using Amos.Data;
using Amos.Interfaces;
using Amos.TypeFiles;
using Amos.TypeFiles.FileBuilders;

namespace Amos
{
    public class Document : Receiver, IDisposable
    {
        public delegate void OnChangeData( );

        private string _fileName;
        private string _ImportFileName;
        public event OnChangeData onChangeData;

        static Document( )
        {
            Global = new GlobalData( );
            Global.Create( );
        }

        public Document( )
        {
            Dashboard = new DashboardData( );
            Sequence = new SequenceData( ); 
            Signal = new SignalData( );
        }

        public string FileName 
        {
            get { return _fileName; }
            private set
            {
                _fileName = value;
                if ( !string.IsNullOrEmpty( _fileName ) )
                    Program.RecentFileList.Add( _fileName );
            }
        }

        public string ImportFileName
        {
            get { return _ImportFileName; }
            private set
            {
                _ImportFileName = value;
                if ( !string.IsNullOrEmpty( _ImportFileName ) )
                    Program.RecentFileList.Add( _ImportFileName );
            }
        }

        public static GlobalData Global { get; private set; }

        public DashboardData Dashboard { get; set; }

        public SequenceData Sequence { get; set; }
        
        public SignalData Signal { get; set; }

        public bool IsModify { get; set; }
        
        #region Члены IDisposable

        public void Dispose()
        {
            
        }

        #endregion

        public void Create()
        {
            Dashboard.Create();
            Sequence.Create();
            Signal.Create();
            FileName = string.Empty;
            ImportFileName = string.Empty;
        }
        
        public void Close()
        {
            Dashboard.Close( );
            Sequence.Close( );
            Signal.Close( );
            FileName = string.Empty;
            ImportFileName = string.Empty;
        }

        public void Refresh( )
        {
            Sequence.Refresh( );
            Dashboard.Refresh( );
            Signal.Refresh( );
        }

        public void Open( string fileName )
        {
            string ext = ( new FileInfo( fileName ) ).Extension.Remove( 0, 1 ).ToUpper( );
            switch ( ext )
            {
            case "TNT":
                Create( );
                ( new FileConverter( this, new TNTFileBuilder( ) ) ).Load( fileName );
                ImportFileName = fileName;
                break;
            case "MRI":
                ( new FileConverter( this, new MRIFileBuilder( ) ) ).Load( fileName );
                FileName = fileName;
                ImportFileName = string.Empty;
                break;
            }
            Refresh( );
        }

        public void Save( string fileName )
        {
            string ext = ( new FileInfo( fileName ) ).Extension.Remove( 0, 1 ).ToUpper();
            switch ( ext )
            {
            case "MRI":
                ( new FileConverter( this, new MRIFileBuilder( ) ) ).Save( fileName );
                FileName = fileName;
                break;
            }
        }

        public void Cut()
        {
            throw new System.NotImplementedException();
        }

        public void Copy()
        {
            throw new System.NotImplementedException();
        }

        public void Paste()
        {
            throw new System.NotImplementedException();
        }

        public void Save()
        {
            throw new System.NotImplementedException();
        }

        protected override void OnCommandExecute( )
        {
            Refresh( );
            if ( onChangeData != null )
                onChangeData( );
        }
    }
}
