using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace Tools
{
    public class StringEllipsis
    {
        public static readonly string EllipsisChars = "...";

        public static string PathEllipsis( FileInfo fileInfo, int lengthString )
        {
            if ( fileInfo == null )
                return string.Empty;
            return PathEllipsis( fileInfo.FullName, lengthString );
        }

        public static string PathEllipsis( string text, int lengthString )
        {
            if ( string.IsNullOrWhiteSpace( text ) )
                return string.Empty;
            if ( text.Length <= lengthString )
                return text;

            string[] strs = text.Split( '\\' );
            if ( strs.Length == 2 )
                return text;

            string result = null;


            int left = 1;
            int right = 1;

            for ( int i = 2; i <= strs.Length; i++ )
            {
                string leftString = string.Empty;
                string rightString = string.Empty;

                for ( int j = 0; j < left; j++ )
                    leftString += strs[j] + "\\";

                for ( int j = strs.Length - right; j < strs.Length; j++ )
                    rightString += "\\" + strs[j] ;

                string temp = leftString + EllipsisChars + rightString;

                if ( result == null )
                    result = temp;
                if ( temp.Length > lengthString && result != null )
                    return result;
                result = temp;
                left += ( i + 1 ) % 2;
                right += i % 2;
            }
            return text;
        }
    }
}

