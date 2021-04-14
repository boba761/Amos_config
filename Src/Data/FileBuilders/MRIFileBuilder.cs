using System;
using System.IO;
using System.IO.Packaging;
using System.Net.Mime;
using Tools;

namespace Data.FileBuilders
{
    public class MRIFileBuilder : FileBuilder
    {
        public override void Load( Stream stream )
        {
            PackagePart packagePart;
            using ( Package package = ZipPackage.Open( stream, FileMode.Open ) )
            {
                packagePart = package.GetPart( new Uri( @"/Dashboard.xml", UriKind.Relative ) );
                ( new DashboardFileBuilder( Object ) ).Load( packagePart.GetStream( ) );

                packagePart = package.GetPart( new Uri( @"/Sequence.xml", UriKind.Relative ) );
                ( new SequenceFileBuilder( Object ) ).Load( packagePart.GetStream( ) );

                packagePart = package.GetPart( new Uri( @"/Signal.xml", UriKind.Relative ) );
                ( new SignalFileBuilder( Object ) ).Load( packagePart.GetStream( ) );
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
                ( new DashboardFileBuilder( Object ) ).Save( packagePart.GetStream( ) );

                packagePart = package.CreatePart( new Uri( @"/Sequence.xml", UriKind.Relative ), MediaTypeNames.Text.Xml, CompressionOption.Maximum );
                ( new SequenceFileBuilder( Object ) ).Save( packagePart.GetStream( ) );

                packagePart = package.CreatePart( new Uri( @"/Signal.xml", UriKind.Relative ), MediaTypeNames.Text.Xml, CompressionOption.Maximum );
                ( new SignalFileBuilder( Object ) ).Save( packagePart.GetStream( ) );
            }
        }
    }
}
