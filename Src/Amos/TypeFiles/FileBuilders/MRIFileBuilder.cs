using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Packaging;
using System.Net.Mime;
using System.Xml.Linq;

namespace Amos.TypeFiles.FileBuilders
{
    public class MRIFileBuilder : FileBuilder
    {
        public override void Load( Stream stream )
        {
            PackagePart packagePart;
            using ( Package package = ZipPackage.Open( stream, FileMode.Open ) )
            {
                packagePart = package.GetPart( new Uri( @"/Dashboard.xml", UriKind.Relative ) );
                ( new DashboardFileBuilder( ) ).Load( packagePart.GetStream( ) );

                packagePart = package.GetPart( new Uri( @"/Sequence.xml", UriKind.Relative ) );
                ( new SequenceFileBuilder( ) ).Load( packagePart.GetStream( ) );

                packagePart = package.GetPart( new Uri( @"/Signal.xml", UriKind.Relative ) );
                ( new SignalFileBuilder( ) ).Load( packagePart.GetStream( ) );
            }
        }

        public override void Save( Stream stream )
        {
            PackagePart packagePart;
            using ( Package package = ZipPackage.Open( stream, FileMode.Create ) )
            {
                package.PackageProperties.Version = "1.0";
                package.PackageProperties.ContentType = "Natix Data File";

                packagePart = package.CreatePart( new Uri( @"/Dashboard.xml", UriKind.Relative ), MediaTypeNames.Text.Xml, CompressionOption.Maximum );
                ( new DashboardFileBuilder( ) ).Save( packagePart.GetStream( ) );

                packagePart = package.CreatePart( new Uri( @"/Sequence.xml", UriKind.Relative ), MediaTypeNames.Text.Xml, CompressionOption.Maximum );
                ( new SequenceFileBuilder( ) ).Save( packagePart.GetStream( ) );

                packagePart = package.CreatePart( new Uri( @"/Signal.xml", UriKind.Relative ), MediaTypeNames.Text.Xml, CompressionOption.Maximum );
                ( new SignalFileBuilder( ) ).Save( packagePart.GetStream( ) );
            }
        }
    }
}
