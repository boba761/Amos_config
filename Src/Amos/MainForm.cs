using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using Amos.Views;
using Amos.Commands;
using Amos.Forms;
using System.IO;
using Tools;


namespace Amos
{
    public partial class MainForm : BaseForm
    {
        private DeserializeDockContent _deserializeDockContent;
        private List<ToolStripItem> _recentFileMehuItems;

        public MainForm()
        {
            InitializeComponent();
            _deserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);
            SetMenu( );
        }

        public override Document Document
        {
            get { return base.Document; }
            set
            {
                if ( Document != null )
                    Document.onChangeData -= OnChangeData;
                base.Document = value;
                if ( Document != null )
                    base.Document.onChangeData += OnChangeData;
            }
        }

        public DashboardView DashboardView { get { return DashboardView.Instance( ); } }

        public SequenceView SequenceView { get { return SequenceView.Instance( ); } }

        public SignalView SignalView { get { return SignalView.Instance( ); } }

        public DockPanel DockPanel
        {
            get { return dockPanel; }
        } 
        
        private void SetMenu( )
        {
            if ( _recentFileMehuItems == null )
                _recentFileMehuItems = new List<ToolStripItem>( );
            else
            {
                foreach ( ToolStripItem obj in _recentFileMehuItems )
                    fileMenuItem.DropDownItems.Remove( obj );
                _recentFileMehuItems.Clear( );
            }

            if ( Program.RecentFileList.Count == 0 )
                return;
            ToolStripItem item;
            int index = 0;
            int indexExit = fileMenuItem.DropDownItems.IndexOf( exitMenuItem );
            foreach ( FileInfo file in Program.RecentFileList.RecentFiles )
            {
                item = new ToolStripMenuItem( string.Format( "{0} {1}", index + 1, StringEllipsis.PathEllipsis( file, 35 ) ), null, OnOpenFileMenuItem );
                item.Tag = file.FullName;
                fileMenuItem.DropDownItems.Insert( indexExit + index, item );
                _recentFileMehuItems.Add( item );
                index++;
            }
            item = new ToolStripSeparator();
            fileMenuItem.DropDownItems.Insert( indexExit + index, item );
            _recentFileMehuItems.Add( item );
        }

        private void OnChangeData( )
        {
            DashboardView.Refresh( );
            SequenceView.Refresh( );
            SignalView.Refresh( );
            SetControls( );
        }

        private void OnLoad( object sender, EventArgs e )
        {
            if ( File.Exists( Program.DockPanelConfig ) )
            {
                try
                {
                    dockPanel.LoadFromXml( Program.DockPanelConfig, _deserializeDockContent ); 
                    DashboardView.Hide( );
                    SequenceView.Hide( );
                    SignalView.Hide( );
                }
                catch
                {
                    File.Delete( Program.DockPanelConfig );
                }
            }
            SetControls( );
        }

        private void OnClosing( object sender, FormClosingEventArgs e )
        {
            try
            {
                dockPanel.SaveAsXml( Program.DockPanelConfig );
                if ( Program.Document != null )
                    Program.Document.Close( );
            }
            catch
            {
            }
        }

        private IDockContent GetContentFromPersistString(string persistString)
        {
            if (persistString == typeof(DashboardView).ToString())
                return DashboardView;
            if (persistString == typeof(SequenceView).ToString())
                return SequenceView;
            if (persistString == typeof(SignalView).ToString())
                return SignalView;
            else
                return null;
        }

        private void OnNewMenuItem(object sender, EventArgs e)
        {
            OnCloseMenuItem(sender, e);
            Program.Document = new Document( );
            Program.Document.Create( );
            DashboardView.Document = Program.Document;
            SequenceView.Document = Program.Document;
            SignalView.Document = Program.Document;
            SetMenu( );
            SetControls( );
        }

        private void OnOpenMenuItem(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Open file...";
            openFileDialog.Filter = "Natix Data File (*.mri)|*.mri||";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ( Program.Document != null )
                        Program.Document.Close( );
                    Program.Document = new Document( );
                    Program.Document.Open( openFileDialog.FileName );
                    DashboardView.Document = Program.Document;
                    SequenceView.Document = Program.Document;
                    SignalView.Document = Program.Document;
                    SetMenu( );
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, Name, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            SetControls( );
        }

        private void OnOpenFileMenuItem( object sender, EventArgs e )
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;
            if ( Program.Document != null )
                Program.Document.Close( );
            Program.Document = new Document( );
            Program.Document.Open( (string)item.Tag );
            DashboardView.Document = Program.Document;
            SequenceView.Document = Program.Document;
            SignalView.Document = Program.Document;
            SetMenu( );
            SetControls( );
        }

        private void OnPaintOpenFileMenuItem( object sender, PaintEventArgs e )
        {
            TextRenderer.DrawText( e.Graphics, "Some text.", this.Font, new Point( 10, 10 ), SystemColors.ControlText, TextFormatFlags.PathEllipsis );
        }

        private void OnImportMenuItem( object sender, EventArgs e )
        {
            OpenFileDialog openFileDialog = new OpenFileDialog( );
            openFileDialog.Title = "Import file...";
            openFileDialog.Filter = "Tecmag Data File (*.tnt)|*.tnt||";
            if ( openFileDialog.ShowDialog( ) == DialogResult.OK )
            {
                try
                {
                    if ( Program.Document != null )
                        Program.Document.Close( );
                    Program.Document = new Document( );
                    Program.Document.Open( openFileDialog.FileName );
                    DashboardView.Document = Program.Document;
                    SequenceView.Document = Program.Document;
                    SignalView.Document = Program.Document;
                    SetMenu( );
                }
                catch ( Exception ex )
                {
                    MessageBox.Show( ex.Message, Name, MessageBoxButtons.OK, MessageBoxIcon.Error );
                }
            }
            SetControls( );
        }

        private void OnCloseMenuItem(object sender, EventArgs e)
        {
            //Document = null;
            DashboardView.Instance( ).Hide( );
            SequenceView.Instance( ).Hide( );
            SignalView.Instance( ).Hide( );
            if ( Program.Document != null )
                Program.Document.Close( );
            Program.Document = null;
            SetControls( );
        }

        private void OnSaveMenuItem(object sender, EventArgs e)
        {
            if ( Program.Document != null && !string.IsNullOrWhiteSpace( Program.Document.FileName ) )
                Document.Save( Program.Document.FileName );
            else
                OnSaveAsMenuItem( sender, e );
            SetControls( );
        }

        private void OnSaveAsMenuItem(object sender, EventArgs e)
        {
            SaveFileDialog openFileDialog = new SaveFileDialog( );
            openFileDialog.Filter = "Natix Data File (*.mri)|*.mri||";
            openFileDialog.InitialDirectory = Program.DirectoryData;
            if ( openFileDialog.ShowDialog( ) == DialogResult.OK )
            {
                try
                {
                    Document.Save( openFileDialog.FileName );
                }
                catch ( Exception ex )
                {
                    MessageBox.Show( ex.Message, Name, MessageBoxButtons.OK, MessageBoxIcon.Error );
                }
            }
            SetControls( );
        }

        private void OnExitMenuItem(object sender, EventArgs e)
        {
            Environment.Exit( 0 );
        }

        private void OnUndoMenuItem( object sender, EventArgs e )
        {
            Document.Undo( );
        }

        private void OnRedoMenuItem( object sender, EventArgs e )
        {
            Document.Redo();
        }

        private void OnCutMenuItem( object sender, EventArgs e )
        {

        }

        private void OnCopyMenuItem( object sender, EventArgs e )
        {

        }

        private void OnPasteMenuItem( object sender, EventArgs e )
        {

        }

        private void OnDashboardMenuItem( object sender, EventArgs e )
        {
            DashboardView.Instance().Show();
        }

        private void OnSequenceMenuItem( object sender, EventArgs e )
        {
            SequenceView.Instance().Show();
        }
        
        private void OnSignalMenuItem( object sender, EventArgs e )
        {
            SignalView.Instance( ).Show( );
        }

        private void SetControls()
        {
            if ( Document != null && (!string.IsNullOrEmpty( Document.FileName ) || !string.IsNullOrEmpty( Document.ImportFileName ) ) )
            {
                FileInfo fileInfo = new FileInfo( !string.IsNullOrEmpty( Document.FileName ) ? Document.FileName : Document.ImportFileName );
                Text = string.Format( "NATIX Amos - [ {0} ]", fileInfo.Name );
            }
            else
                Text = "NATIX Amos";

            closeMenuItem.Enabled = Document != null;
            saveMenuItem.Enabled = Document != null;
            saveToolStrip.Enabled = Document != null;
            saveAsMenuItem.Enabled = Document != null;
            saveAsToolStrip.Enabled = Document != null;
            dashboardMenuItem.Enabled = Document != null;
            sequenceMenuItem.Enabled = Document != null;
            signalMenuItem.Enabled = Document != null;

            if ( Document != null )
            {
                undoMenuItem.Enabled = Document.IsUndo;
                undoToolStrip.Enabled = Document.IsUndo;
                redoMenuItem.Enabled = Document.IsRedo;
                redoToolStrip.Enabled = Document.IsRedo;
            }
        }

        private void newSequenceMenuItem_Click( object sender, EventArgs e )
        {
            Document.Sequence = null;
            Program.Document = Document; 
            SetControls( );
        }

        private void openSequenceMenuItem_Click( object sender, EventArgs e )
        {

        }

        private void closeSequenceMenuItem_Click( object sender, EventArgs e )
        {

        }

        private void saveSequenceMenuItem_Click( object sender, EventArgs e )
        {

        }

        private void saveAsSequenceMenuItem_Click( object sender, EventArgs e )
        {

        }
    }
}
