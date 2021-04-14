using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Amos.Commands;
using Amos.Data;
using Localize;
using Tools.Interfaces;

namespace Amos.Controls.Sequence
{
    public delegate void PropertyEventHandle( IObjectStream @object );
    public delegate void SelectEventHandle( IObjectStream @object );
    
    public partial class SequenceControl : UserControl
    {
        private SequenceData _sequenceData;
        private List<Column> _columnList;
        private List<Row> _rowsList;
        private int _zoom;

        public event PropertyEventHandle onProperty;
        public event SelectEventHandle onSelect;

        public SequenceControl()
        {
            Clear();

            InitializeComponent();

            splitContainer.Panel2Collapsed = true;
            panel1.Initialize( this, vScrollBar1, true );
            panel2.Initialize( this, vScrollBar2 );
            delimeterButton.BackgroundImage = Local.GetImage( "Delimeter.Button" );
            zoomComboBox.SelectedIndex = 3;
            ActionPanel = panel1;
        }

        public SequenceData Sequence 
        {
            get { return _sequenceData; }
            set 
            {
                Clear();
                _sequenceData = value;
                if ( _sequenceData == null )
                    return;
                _sequenceData.onInsertColumn += OnInsertColumn;
                _sequenceData.onRemoveColumn += OnRemoveColumn;
                foreach ( eTypeObjectCollection typeCollection in Enum.GetValues( typeof( eTypeObjectCollection ) ) )
                {
                    foreach ( IStream element in _sequenceData.Streams )
                    {
                        if ( element.IsVisible == false )
                            continue;
                        IObjectCollection collection = element[typeCollection];
                        if ( collection != null )
                            AddRow( collection );
                    }
                }
                for ( int i = 0; i < _sequenceData.EventCount; i++ )
                    OnInsertColumn( i );
                hScrollBar.Value = 0;
                Refresh();
            } 
        }

        public SequencePanel ActionPanel { get; private set; }

        public IList<Column> Columns { get { return _columnList; } }

        public IList<Row> Rows 
        { 
            get { return _rowsList; } 
        }

        public int CountVisibleRows
        {
            get 
            {
                int count = 0;
                foreach(Row row in _rowsList)
                    if (row.IsVisible)
                        count ++;
                return count; 
            }
        }
        
        public override void Refresh()
        {
            if ( _sequenceData == null )
                return;

            int Width = (int)Math.Ceiling( 100.0 * panel1.Width / _zoom );
            if ( hScrollBar.Maximum != Columns.Count - 1 )
                hScrollBar.Maximum = Columns.Count - 1;
            hScrollBar.LargeChange = SequencePanel.header.ChangeSize( Width, hScrollBar.Value );

            if ( hScrollBar.Value != SequencePanel.header.IndexColnum )
                hScrollBar.Value = SequencePanel.header.IndexColnum;

            panel1.Refresh();
            panel2.Refresh();

            hScrollBar.Enabled = Width < SequencePanel.header.Size.Width;

            base.Refresh();
        }

        public void ClearObject( Cell cell )
        {
            Program.Document.Run( new ClearObjectCommand( Program.Document, cell.Stream, cell.TypeCollection, cell.IndexColumn ) );
        }

        public void SetCell( Cell cell, string text )
        {
            try
            {
                Program.Document.Run( new SetObjectCommand( Program.Document, cell.Stream, cell.TypeCollection, cell.IndexColumn, text ) );
            }
            catch ( Exception ex )
            {
                MessageBox.Show( ex.InnerException != null ? ex.InnerException.Message : ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning );
            }
        }

        public void SetCell( Cell cell, eTypeObjectCollection typeCollection, eTypeObjectStream typeObject )
        {
            if ( typeObject == eTypeObjectStream.AsynchronousStart )
            {
                OpenFileDialog openFileDialog = new OpenFileDialog( );
                openFileDialog.Filter = "Amos Sequence file (*.sequence)|*.sequence|Tecmag Sequence file (*.tps)|*.tps||";
                if ( openFileDialog.ShowDialog( ) == DialogResult.OK )
                    Program.Document.Run( new SetObjectCommand( Program.Document, cell.Stream, typeCollection, cell.IndexColumn, typeObject, openFileDialog.FileName ) );
            }
            else
            {
                try
                {
                    Program.Document.Run( new SetObjectCommand( Program.Document, cell.Stream, typeCollection, cell.IndexColumn, typeObject ) );
                    if ( cell.ObjectCollection.TypeCollection == typeCollection )
                        return;
                    Row row = _rowsList.Single( obj => obj.Stream == cell.Stream && obj.ObjectCollection.TypeCollection == typeCollection);
                    int index = _rowsList.IndexOf( row );
                    for ( int i = index; i >= 0; i-- )
                        if ( !_rowsList[i].IsVisible )
                            index--;
                    if ( index < ActionPanel.IndexRow )
                        ActionPanel.IndexRow = index - 2 >=0 ?  index - 2 : 0 ;
                    else if (index > ActionPanel.IndexRow + ActionPanel.ShowRow )
                        ActionPanel.IndexRow = index - ActionPanel.ShowRow + 1;
                    Select( ActionPanel, row[cell.IndexColumn + 1] );
                }
                catch ( Exception ex )
                {
                    MessageBox.Show( ex.InnerException != null ? ex.InnerException.Message : ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                }
            }
        }

        public void ClearColumn( int index )
        {
            try
            {
                Program.Document.Run( new ClearColumnCommand( Program.Document, index ) );
            }
            catch ( Exception ex )
            {
                MessageBox.Show( ex.InnerException != null ? ex.InnerException.Message : ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning );
            }
        }

        public void InsertColumn( int index )
        {
            try
            {
                Program.Document.Run( new InsertColumnCommand( Program.Document, index ) );
            }
            catch ( Exception ex )
            {
                MessageBox.Show( ex.InnerException != null ? ex.InnerException.Message : ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Warning );
            }
        }

        public bool IsRemoveColumn( int index )
        {
            return Sequence.EventCount > 1 && index < Sequence.EventCount;
        }

        public void RemoveColumn( int index )
        {
            if ( SequencePanel.glyphSelect is Column && Sequence.EventCount > 1 && ( SequencePanel.glyphSelect as Column ).Index < Sequence.EventCount )
            {
                Program.Document.Run( new RemoveColumnCommand( Program.Document, index ) );
                if ( index < 1 )
                    index = 1;
                Select( ActionPanel, Columns[index] );
            }
        }

        public bool IsCutColumn( int index )
        {
            return Sequence.EventCount > 1 && index < Sequence.EventCount - 1;
        }

        public void CutColumn( int index )
        {
        }

        public bool IsCopyColumn( int index )
        {
            return Sequence.EventCount > 1 && index < Sequence.EventCount;
        }

        public void CopyColumn( int index )
        {

        }

        public bool IsPasteColumn( int index )
        {
            return false;
        }

        public void PasteColumn( int index )
        {
        }

        public void RenameElement( IStream element, string newName )
        {
            if ( Sequence.CheckRenameStream( element, newName ) )
                Program.Document.Run( new RenameElementCommand( Program.Document, element, newName ) );
            else
                MessageBox.Show( "With such name a line exists already.", Program.MainForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning ); 
        }

        public void ShowProperty( IObjectStream @object )
        {
            if ( onProperty != null )
                onProperty( @object );
        }

        public void ChangeSelect( IObjectStream @object )
        {
            if ( onSelect != null )
                onSelect( @object );
        }

        public void MoveRow( Row sourceRow, Row toRow )
        {
            int indexSource = _rowsList.IndexOf( sourceRow );
            int indexTo = _rowsList.IndexOf( toRow );
            _rowsList[indexSource] = toRow;
            _rowsList[indexTo] = sourceRow;
        }

        public void Select( SequencePanel panel, Glyph glyph )
        {
            SequencePanel.isSelected = true;
            if ( SequencePanel.glyphSelect != null )
                SequencePanel.glyphSelect.StateGlyph = eStateGlyph.Normal;
            SequencePanel.glyphSelect = glyph;
            SequencePanel.glyphSelect.StateGlyph = eStateGlyph.Select;
            panel.ContextMenuStrip = glyph.GetContextMenu( panel );
            if ( glyph is Cell )
                ChangeSelect( ( glyph as Cell ).Object );
            else
                ChangeSelect( null );
            Refresh( );
        }

        public void SelectUp(SequencePanel panel, Cell cell )
        {
            int index = _rowsList.IndexOf( cell.Row ) - 1;
            while ( index >= 0 && !_rowsList[index].IsVisible )
                index--;
            if ( index < 0 )
                return;
            int indexShow = -1;
            for ( int i = 0; i <= index; i++ )
                if ( _rowsList[i].IsVisible )
                    indexShow++;
            if ( panel.IndexRow > 0 && panel.IndexRow >= index )
                panel.IndexRow = index - 1;
            Select( panel, _rowsList[index][cell.IndexColumn + 1] );
        }

        public void SelectDown( SequencePanel panel, Cell cell )
        {
            int index = _rowsList.IndexOf( cell.Row ) + 1;
            while ( index < _rowsList.Count && !_rowsList[index].IsVisible )
                index++;
            if ( index >= _rowsList.Count)
                return;
            int indexShow = -1;
            for ( int i = 0; i <= index; i++ )
                if ( _rowsList[i].IsVisible )
                    indexShow++;
            if ( panel.IndexRow + panel.ShowRow <= indexShow )
                panel.IndexRow = indexShow - panel.ShowRow + 1;
            Select( panel, _rowsList[index][cell.IndexColumn + 1] );
        }
        
        public void SelectLeft( SequencePanel panel, Cell cell )
        {
            int index = cell.IndexColumn - 1;
            if ( index < 0 )
                return;
            if ( hScrollBar.Value > index )
                hScrollBar.Value = index;
            Select( panel, cell.Row[index + 1] );
        }

        public void SelectRight( SequencePanel panel, Cell cell )
        {
            int index = cell.IndexColumn + 1;
            if ( index >= hScrollBar.Maximum )
                return;
            if ( hScrollBar.Value + hScrollBar.LargeChange <= index + 1 )
                hScrollBar.Value = index - hScrollBar.LargeChange + 2;
            Select( panel, cell.Row[index + 1] );
        }

        protected override void OnResize( EventArgs e )
        {
            Refresh();
            base.OnResize( e );
        }

        private void OnInsertColumn( int indexColumn )
        {
            Column column = new Column( this, indexColumn + 1, SequencePanel.defaulWidth );
            Columns.Insert( indexColumn + 1, column );
            for ( int i = indexColumn + 2; i < Columns.Count; i++ )
                Columns[i].Index++;
            for ( int i = 0; i < _rowsList.Count; i++ )
            {
                Cell cell = new Cell( this, _rowsList[i].ObjectCollection[indexColumn], _rowsList[i], column );
                _rowsList[i].Cells.Insert( indexColumn, cell );
                column.Add( cell );
            }
        }

        private void OnRemoveColumn( int indexColumn )
        {
            for ( int i = indexColumn + 2; i < Columns.Count; i++ )
                Columns[i].Index--;
            for ( int i = 0; i < _rowsList.Count; i++ )
                _rowsList[i].Cells.RemoveAt( indexColumn );
            Columns.RemoveAt( indexColumn + 1 );
        }

        private void OnChangeData()
        {
            Refresh();
            if (onSelect != null && SequencePanel.glyphSelect is Cell)
                onSelect( (SequencePanel.glyphSelect as Cell).Object );
        }

        private void AddRow( IObjectCollection collection )
        {
            _rowsList.Add( new Row( this, collection, Columns[0], new List<Cell>( ) ) );
        }
        
        private void Clear()
        {
            _columnList = new List<Column>();
            _rowsList = new List<Row>();
            Columns.Add( new Column( this, Local.GetString( "EventNumber" ), SequencePanel.defaultStartWidth ) );
        }

        private void OnRefresh( object sender, EventArgs e )
        {
            Refresh();
        }

        private void zoomComboBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            int value = _zoom;
            Match match = Regex.Match( zoomComboBox.Text, @"\d+" );
            if ( match != Match.Empty )
            {
                value = int.Parse( match.Value );
                if ( value < 25 )
                    value = 25;
                else if ( value > 400 )
                    value = 400;
            }
            if ( _zoom != value )
            {
                _zoom = value;
                panel1.Zoom = _zoom;
                panel2.Zoom = _zoom;
                Refresh();
                splitContainer.Focus();
            }
            zoomComboBox.Text = string.Format( "{0} %", _zoom );
        }

        private void zoomComboBox_Leave( object sender, EventArgs e )
        {
            zoomComboBox.Text = string.Format( "{0} %", _zoom );
        }

        private void zoomComboBox_KeyUp( object sender, KeyEventArgs e )
        {
            if ( e.KeyData == Keys.Enter )
                zoomComboBox_SelectedIndexChanged( sender, e );
        }

        private void zoomComboBox_KeyDown( object sender, KeyEventArgs e )
        {
            if ( e.KeyData == Keys.Enter )
                e.SuppressKeyPress = true;
        }

        private void delimeterButton_MouseDown( object sender, MouseEventArgs e )
        {
            delimeterButton.Capture = true;
            delimeterButton.Cursor = Cursors.HSplit;
            panel1.isDelimeter = true;
        }

        private void delimeterButton_MouseMove( object sender, MouseEventArgs e )
        {
            if ( !panel1.isDelimeter )
                return;
            Point point = panel1.PointToClient( delimeterButton.PointToScreen( new Point( e.X, e.Y ) ) );
            point = new Point( (int)(100.0f * point.X / _zoom), (int)(100.0f * point.Y / _zoom) );
            panel1.MoveDelemiter( point );
        }

        private void delimeterButton_MouseUp( object sender, MouseEventArgs e )
        {
            delimeterButton.Capture = false;
            delimeterButton.Cursor = Cursors.Arrow;
            if ( !panel1.isDelimeter )
                return;
            panel1.isDelimeter = false;
            if ( panel1.IndexRow != panel1.IndexDelimeter )
            {
                splitContainer.SplitterDistance = panel1.HeightDelemiter;
                splitContainer.Panel2Collapsed = false;
                delimeterButton.Visible = false;
                panel2.IndexRow = panel1.IndexDelimeter;
            }
        }

        private void splitContainer_SplitterMoved( object sender, SplitterEventArgs e )
        {
            if ( panel1.MinHeight > e.SplitY )
            {
                splitContainer.Panel2Collapsed = true;
                delimeterButton.Visible = true;
            }
        }

        private void splitContainer_Paint( object sender, PaintEventArgs e )
        {
            if ( splitContainer.Panel2Collapsed == false)
                e.Graphics.FillRectangle( Brushes.LightSteelBlue, 0, splitContainer.SplitterDistance, 
                    splitContainer.Width, splitContainer.SplitterWidth );
        }

        private void panel_Enter( object sender, EventArgs e )
        {
            ActionPanel = (SequencePanel)sender;
        }
    }
}
