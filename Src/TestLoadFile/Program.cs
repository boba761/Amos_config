using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data;

namespace TestLoadFile
{
    class Program
    {
        static void Main( string[] args )
        {
            try
            {
                Document document = Document.Load( "test.mri" );
                Console.WriteLine( "Load file. OK" );
            }
            catch ( Exception exc )
            {
                Console.WriteLine( exc.Message );
            }
            Console.ReadLine( );
        }
    }
}
