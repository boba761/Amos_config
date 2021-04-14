using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Globalization;
using System.IO;
using System.Windows.Forms.VisualStyles;
using Microsoft.Win32;
using System.Collections.ObjectModel;

namespace Amos
{
    static class Program
    {
        private const string directoryDashboard = @"\Dashboards\";
        private const string directoryConfig = @"\Config\";
        private const string directoryData = @"\Data\";
        private const string sequenseConfig = "config.con";
        private const string dockPanelConfig = @"\DockPanel.config";
        private const string globalVariableConfig = @"\GlobalVariable.config";

        private static Document document;

        public static MainForm MainForm { get; private set; }

        public static Document Document 
        {
            get { return document;  } 
            set 
            {
                if ( document != null )
                    document.Dispose();
                document = value;
                MainForm.Document = value;
            }
        }

        public static string DirectoryApplication
        {
            get { return ( new FileInfo( Application.ExecutablePath ) ).DirectoryName; }
        }

        public static string DirectoryDashboard
        {
            get { return DirectoryApplication + directoryDashboard; }
        }

        public static string DirectoryConfig
        {
            get { return DirectoryApplication + directoryConfig; }
        }

        public static string DirectoryData
        {
            get { return DirectoryApplication + directoryData; }
        }

        public static string SequenseConfig
        {
            get { return DirectoryConfig + sequenseConfig; }
        }

        public static string DockPanelConfig
        {
            get { return Application.UserAppDataPath + dockPanelConfig; }
        }

        public static string GlobalVariableConfig
        {
            get { return Application.UserAppDataPath + globalVariableConfig; }
        }

        public static RecentFileList RecentFileList
        {
            get { return RecentFileList.Instance( ); }
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            try
            {
                Application.EnableVisualStyles( );
                Application.SetCompatibleTextRenderingDefault( false );
                CreateDirectories( );
                MainForm = new MainForm( );
                Application.Run( MainForm );
            }
            catch ( Exception ex )
            {
                MessageBox.Show( ex.ToString( ), MainForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop );
            }
        }

        static void CreateDirectories()
        {
            if ( !Directory.Exists( DirectoryDashboard ) )
                Directory.CreateDirectory( DirectoryDashboard );
            if ( !Directory.Exists( DirectoryConfig ) )
                Directory.CreateDirectory( DirectoryConfig );
            if ( !Directory.Exists( DirectoryData ) )
                Directory.CreateDirectory( DirectoryData );
        }
    }
}
