using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Win32;
using System.Windows.Forms;

namespace Amos
{
    class RecentFileList
    {
        private const string subKeyeRecentFileList = "Recent File List";
        private static RecentFileList _self;

        private List<FileInfo> _recentFileList;

        public RecentFileList( )
        {
            _recentFileList = new List<FileInfo>( );
            Load( );
        }

        public int Count
        {
            get { return _recentFileList.Count; }
        }

        public FileInfo[] RecentFiles
        {
            get { return _recentFileList.ToArray( ); }
        }

        public static RecentFileList Instance( )
        {
            if ( _self == null )
                _self = new RecentFileList( );
            return _self;
        }

        public void Add( string fileName )
        {
            _recentFileList.Insert( 0, new FileInfo( fileName ) );
            for ( int i = 1; i < _recentFileList.Count; i++ )
            {
                if ( _recentFileList[i].FullName == _recentFileList[0].FullName )
                {
                    _recentFileList.RemoveAt( i );
                    i--;
                }
            }
            using ( RegistryKey registryKey = Application.UserAppDataRegistry.CreateSubKey( subKeyeRecentFileList ) )
            {
                for ( int i=0, j=1; i<_recentFileList.Count && j < 10; i++)
                {
                    if ( _recentFileList[i].Exists )
                    {
                        registryKey.SetValue( "File" + j, _recentFileList[i].FullName, RegistryValueKind.String );
                        j++;
                    }
                }
            }
        }

        private void Load( )
        {
            _recentFileList.Clear( );
            using ( RegistryKey registryKey = Application.UserAppDataRegistry.OpenSubKey( subKeyeRecentFileList ) )
            {
                if ( registryKey == null )
                    return;
                string[] names = registryKey.GetValueNames( );
                foreach ( string name in names )
                {
                    FileInfo fileInfo = new FileInfo( (string)registryKey.GetValue( name ) );
                    if ( fileInfo.Exists )
                        _recentFileList.Add( fileInfo );
                }
            }
        }
     }
}
