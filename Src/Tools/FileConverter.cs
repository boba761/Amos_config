using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Tools
{
    public class FileConverter
    {
        private FileBuilder _fileBuilder;

        public FileConverter( object @object, FileBuilder fileBuilder )
        {
            _fileBuilder = fileBuilder;
            _fileBuilder.Object = @object;
        }

        public void Load( string fileName )
        {
            FileStream fileStream = null;
            try
            {
                FileInfo fileInfo = new FileInfo( fileName );
                fileInfo.Attributes = FileAttributes.Archive;
                fileStream = fileInfo.Open( FileMode.Open, FileAccess.ReadWrite );
                _fileBuilder.Load( fileStream );
            }
            finally
            {
                if ( fileStream != null )
                    fileStream.Close( );
            }
        }

        public void Save( string fileName )
        {
            FileStream fileStream = null;
            try
            {
                fileStream = File.Open( fileName, FileMode.Create, FileAccess.ReadWrite );
                _fileBuilder.Save( fileStream );
            }
            finally
            {
                if ( fileStream != null )
                    fileStream.Close( );
            }
        }
    }
}
