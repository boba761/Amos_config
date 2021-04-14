using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Localize;
using Amos.Data;
using Amos.TypeFiles;
using Amos.TypeFiles.FileBuilders;
using Calculations.Variables;

namespace Amos.Forms
{
    public partial class DashboardCustomizeForm : BaseForm
    {
        private const string _dashboardExt = "dashboard";
        private const string _dashboardFilter = "*." + _dashboardExt;
        private ImageList _imageList;
        private CollectionVariable _storeCurrent; 

        protected DashboardCustomizeForm()
        {
            InitializeComponent();
        }

        public DashboardCustomizeForm( Document document )
            : base(document)
        {
            _storeCurrent = Document.Dashboard.CurrentCollectionVariable;
            Document.Dashboard.CurrentCollectionVariable = _storeCurrent.Clone() as CollectionVariable;
            
            InitializeComponent(); 

            _imageList = new ImageList();
            _imageList.ColorDepth = ColorDepth.Depth24Bit;
            _imageList.Images.Add( Local.GetImage( "CustomizeDashboard.Group" ) );
            _imageList.Images.Add( Local.GetImage( "CustomizeDashboard.Variable" ) );
            masterTree.ImageList = _imageList;
            currentTree.ImageList = _imageList;

            setFilesComboBox();
            setTreeView( masterTree.Nodes, Document.Dashboard.MasterCollectionVariable );
            setTreeView( currentTree.Nodes, Document.Dashboard.CurrentCollectionVariable );
        }
        
        private void DashboardCustomizeForm_FormClosing( object sender, FormClosingEventArgs e )
        {
            if ( DialogResult == DialogResult.OK )
                return;
            Document.Dashboard.CurrentCollectionVariable.Clear();
            Document.Dashboard.CurrentCollectionVariable = _storeCurrent;
        }

        private void setFilesComboBox()
        {
            filesComboBox.Items.Clear();
            filesComboBox.Text = Local.GetString("ChooseSetupFile");
            foreach ( FileInfo fileInfo in DashboardData.Directory.GetFiles( _dashboardFilter ) )
                filesComboBox.Items.Add( fileInfo.Name );
        }

        private void filesComboBox_SelectedIndexChanged( object sender, EventArgs e )
        {
            ComboBox comboBox = (ComboBox)sender;
            ( new FileConverter( Document, new ConfigDashboardFileBuilder() ) ).Load( 
                string.Format(@"{0}\{1}", DashboardData.Directory.FullName, (string)comboBox.SelectedItem) );
            currentTree.Nodes.Clear();
            setTreeView( currentTree.Nodes, Document.Dashboard.CurrentCollectionVariable );           
        }

        private void openButton_Click( object sender, EventArgs e )
        {
            openFileDialog.DefaultExt = _dashboardExt;
            openFileDialog.Title = Local.GetString( "OpenDashboard.Title" );
            openFileDialog.Filter = string.Format( "{0} ({1})|{1}||", Local.GetString( "Dashboard.Filter" ), _dashboardFilter );
            openFileDialog.InitialDirectory = DashboardData.Directory.FullName;
            if ( openFileDialog.ShowDialog() == DialogResult.OK )
            {
                ( new FileConverter( Document, new ConfigDashboardFileBuilder() ) ).Load( openFileDialog.FileName );
                currentTree.Nodes.Clear();
                setTreeView( currentTree.Nodes, Document.Dashboard.CurrentCollectionVariable );
            }
        }
        
        private void saveButton_Click( object sender, EventArgs e )
        {
            saveFileDialog.DefaultExt = _dashboardExt;
            saveFileDialog.Title = Local.GetString( "SaveDashboard.Title" );
            saveFileDialog.Filter = string.Format( "{0} ({1})|{1}||", Local.GetString( "Dashboard.Filter" ), _dashboardFilter );
            saveFileDialog.InitialDirectory = DashboardData.Directory.FullName;
            if ( saveFileDialog.ShowDialog() == DialogResult.OK )
            {
                Document.Dashboard.CurrentCollectionVariable.Clear();
                getCollectionVariable( Document.Dashboard.CurrentCollectionVariable, currentTree.Nodes );
                ( new FileConverter( Document, new ConfigDashboardFileBuilder() ) ).Save( saveFileDialog.FileName );
                setFilesComboBox();
            }
        }

        private void okButton_Click( object sender, EventArgs e )
        {
            Document.Dashboard.CurrentCollectionVariable.Clear();
            getCollectionVariable( Document.Dashboard.CurrentCollectionVariable, currentTree.Nodes );
        }

        private void setTreeView(TreeNodeCollection nodes, VariableBase variable)
        {
            TreeNode node;
            if ( variable is Variable )
            {
                node = nodes.Add( variable.Name, variable.Name, 1, 1 ); 
                node.Tag = variable;
            }
            if (variable is CollectionVariable)
            {
                CollectionVariable collectionVariable = variable as CollectionVariable;
                if ( collectionVariable.Name != DashboardData.rootVariable && !collectionVariable.IsReadOnly )
                {
                    node = nodes.Add( variable.Name, variable.Name, 0, 0 ); 
                    node.Tag = variable;
                    nodes = node.Nodes;
                }
                foreach ( VariableBase var in collectionVariable.VariableChilds )
                    setTreeView( nodes, var );
            }
        }

        private void getCollectionVariable( CollectionVariable collection, TreeNodeCollection nodes )
        {
            foreach ( TreeNode node in nodes )
            {
                if ( node.Tag is CollectionVariable )
                {
                    CollectionVariable variable = new CollectionVariable( node.Text );
                    collection.Add( variable );
                    getCollectionVariable( variable, node.Nodes );
                }
                else
                    collection.Add( node.Tag as VariableBase );

            }
        }

        private void masterTree_ItemDrag( object sender, ItemDragEventArgs e )
        {
            DoDragDrop( e.Item, DragDropEffects.Copy );
        }
        
        private void currentTree_ItemDrag( object sender, ItemDragEventArgs e )
        {
            DoDragDrop( e.Item, DragDropEffects.Move );
        }

        private void currentTree_DragEnter( object sender, DragEventArgs e )
        {
            TreeNode newNode = (TreeNode)e.Data.GetData( typeof( TreeNode ) );
            if ( newNode != null )
            {
                if ( newNode.TreeView == sender )
                    e.Effect = DragDropEffects.Move;
                else
                    e.Effect = DragDropEffects.Copy;
            }
        }

        private void currentTree_DragDrop( object sender, DragEventArgs e )
        {
            TreeView treeView = (TreeView)sender;
            TreeNode newNode = (TreeNode)e.Data.GetData( typeof( TreeNode ) );
            if ( newNode != null && e.Effect == DragDropEffects.Copy )
            {
                TreeNode destinationNode = treeView.GetNodeAt( treeView.PointToClient( new Point( e.X, e.Y ) ) );
                if ( destinationNode == null )
                {
                    if ( newNode.Tag is CollectionVariable && !checkName( treeView.Nodes, newNode.Name ) )
                        treeView.Nodes.Add( (TreeNode)newNode.Clone() );
                }
                else
                {
                    if ( destinationNode.Tag is Variable )
                        destinationNode = destinationNode.Parent;
                    if ( !checkName( destinationNode.Nodes, newNode.Name ) )
                    {
                        destinationNode.Nodes.Add( (TreeNode)newNode.Clone() );
                        destinationNode.Expand();
                    }
                }
            }
        }

        private void currentTree_DragOver( object sender, DragEventArgs e )
        {
            TreeView treeView = (TreeView)sender;
            TreeNode newNode = (TreeNode)e.Data.GetData( typeof( TreeNode ) );

            if ( newNode != null )
            {
                Point point = treeView.PointToClient( new Point( e.X, e.Y ) );
                TreeNode node = treeView.GetNodeAt( point );
                if ( e.Effect == DragDropEffects.Copy )
                {
                    if ( node != null )
                    {
                        if ( node.Tag is Variable )
                            node = node.Parent;
                        if ( checkName( node.Nodes, newNode.Name ) )
                            node = null;
                    }
                    treeView.SelectedNode = node;
                }
                else if ( e.Effect == DragDropEffects.Move && node != newNode )
                {
                    if ( node == null && ( newNode.Tag is Variable || checkName( treeView.Nodes, newNode.Name ) ) )
                        return;
                    if ( node != null && node.Parent != null && node.Parent != newNode.Parent && checkName( node.Parent.Nodes, newNode.Name ) )
                        return;
                    if ( newNode.IsExpanded )
                        newNode.Collapse();
                    newNode.Remove();
                    node = treeView.GetNodeAt( point );
                    if ( node != null )
                    {
                        if ( node.Parent != null )
                            node.Parent.Nodes.Insert( node.Index, newNode );
                        else
                            treeView.Nodes.Insert( node.Index, newNode );
                    }
                    else
                        treeView.Nodes.Add( newNode );
                    treeView.SelectedNode = newNode;
                }
            }
        }

        private void currentTree_DragLeave( object sender, EventArgs e )
        {
            ( (TreeView)sender ).SelectedNode = null;
        }

        private void currentTree_KeyUp( object sender, KeyEventArgs e )
        {
            if ( e.KeyValue == (int)Keys.Delete && currentTree.SelectedNode != null )
                currentTree.SelectedNode.Remove();
            else if ( e.KeyValue == (int)Keys.Insert )
            {
                if ( e.Control )
                    InsertNewGroup( );
                else
                    AddNewGroup( );
            }
        }

        private void currentTree_BeforeLabelEdit( object sender, NodeLabelEditEventArgs e )
        {
            if ( e.Node.Tag is Variable )
                e.CancelEdit = true;
        }

        private void currentTree_AfterLabelEdit( object sender, NodeLabelEditEventArgs e )
        {
            TreeView treeView = (TreeView)sender;
            TreeNodeCollection nodes = ( e.Node != null && e.Node.Parent != null ) ? e.Node.Parent.Nodes : treeView.Nodes;
            e.CancelEdit = checkName( nodes, e.Label );
        }

        private string getNewName( TreeView treeView, TreeNode treeNode, string name )
        {
            int index = 1;
            string newName = name;
            TreeNodeCollection nodes = ( treeNode != null ) ? treeNode.Nodes : treeView.Nodes;
            while ( checkName( nodes, newName ) )
            {
                newName = string.Format( "{0} {1}", name, index );
                index++;
            }
            return newName;
        }

        private bool checkName(TreeNodeCollection nodeCollection, string name )
        {
            foreach ( TreeNode node in nodeCollection )
                if (node.Name == name)
                    return true;
            return false;
        }

        private void currentTree_AfterSelect( object sender, TreeViewEventArgs e )
        {
            editButton.Enabled = !( e.Node.Tag is Variable );
            insertButton.Enabled = !( e.Node.Tag is Variable );
            deleteButton.Enabled = e.Node != null;
            upButton.Enabled = e.Node != null && e.Node.PrevNode != null;
            downButton.Enabled = e.Node != null && e.Node.NextNode != null;
        }

        private void masterTree_AfterSelect( object sender, TreeViewEventArgs e )
        {
            moveButton.Enabled = e.Node != null;
        }

        private void editButton_Click( object sender, EventArgs e )
        {
            if ( currentTree.SelectedNode == null )
                return;
            currentTree.SelectedNode.BeginEdit();
        }

        private void addButton_Click( object sender, EventArgs e )
        {
            AddNewGroup( );
        }

        private void insertButton_Click( object sender, EventArgs e )
        {
            if ( currentTree.SelectedNode == null )
                return;
            InsertNewGroup( );
        }

        private void deleteButton_Click( object sender, EventArgs e )
        {
            if ( currentTree.SelectedNode == null )
                return;
            deleteButton.Enabled = false;
            upButton.Enabled = false;
            downButton.Enabled = false;
            currentTree.SelectedNode.Remove( );
        }

        private void moveButton_Click( object sender, EventArgs e )
        {
            if ( masterTree.SelectedNode == null )
                return;

            TreeNode newNode = masterTree.SelectedNode;
            TreeNode destinationNode = currentTree.SelectedNode;
            if ( destinationNode == null || currentTree.SelectedNode.Parent == null )
            {
                if ( newNode.Tag is CollectionVariable && !checkName( currentTree.Nodes, newNode.Name ) )
                {
                    if ( destinationNode == null )
                        currentTree.Nodes.Add( (TreeNode)newNode.Clone( ) );
                    else
                        currentTree.Nodes.Insert( destinationNode.Index + 1, (TreeNode)newNode.Clone( ) );
                }
            }
            else
            {
                if ( destinationNode.Tag is Variable )
                    destinationNode = destinationNode.Parent;
                if ( !checkName( destinationNode.Nodes, newNode.Name ) )
                {
                    destinationNode.Nodes.Insert( currentTree.SelectedNode.Index + 1, (TreeNode)newNode.Clone( ) );
                    destinationNode.Expand( );
                }
            }
        }

        private void upButton_Click( object sender, EventArgs e )
        {
            if ( currentTree.SelectedNode == null || currentTree.SelectedNode.PrevNode == null )
                return;
            TreeNode nodeSel = currentTree.SelectedNode;
            TreeNode node = currentTree.SelectedNode.PrevNode;
            nodeSel.Remove( );
            if ( node.Parent != null )
                node.Parent.Nodes.Insert( node.Index, nodeSel );
            else
                currentTree.Nodes.Insert( node.Index, nodeSel );
            currentTree.SelectedNode = nodeSel;
        }

        private void downButton_Click( object sender, EventArgs e )
        {
            if ( currentTree.SelectedNode == null || currentTree.SelectedNode.NextNode == null )
                return;
            TreeNode nodeSel = currentTree.SelectedNode;
            TreeNode node = currentTree.SelectedNode.NextNode;
            nodeSel.Remove( );
            if ( node.Parent != null )
                node.Parent.Nodes.Insert( node.Index + 1, nodeSel );
            else
                currentTree.Nodes.Insert( node.Index + 1, nodeSel );
            currentTree.SelectedNode = nodeSel;
        }

        private void AddNewGroup( )
        {
            TreeNode node;
            string name;
            if ( currentTree.SelectedNode != null )
            {
                if ( currentTree.SelectedNode.Parent == null )
                {
                    name = getNewName( currentTree, currentTree.SelectedNode, "New Group" );
                    node = currentTree.Nodes.Insert( currentTree.SelectedNode.Index, name, name, 0, 0 );
                }
                else
                {
                    name = getNewName( currentTree, currentTree.SelectedNode.Parent, "New Group" );
                    node = currentTree.SelectedNode.Parent.Nodes.Insert( currentTree.SelectedNode.Index, name, name, 0, 0 );
                }
            }
            else
            {
                name = getNewName( currentTree, null, "New Group" );
                node = currentTree.Nodes.Add( name, name, 0, 0 );
            }
            node.Tag = new CollectionVariable( name );
        }

        private void InsertNewGroup( )
        {
            if ( currentTree.SelectedNode != null && currentTree.SelectedNode.Tag is CollectionVariable )
            { 
                TreeNode node;
                string name;
                name = getNewName( currentTree, currentTree.SelectedNode, "New Group" );
                node = currentTree.SelectedNode.Nodes.Insert( 0, name, name, 0, 0 );
                currentTree.SelectedNode.Expand( );
                node.Tag = new CollectionVariable( name );
            }
        }
    }
}
