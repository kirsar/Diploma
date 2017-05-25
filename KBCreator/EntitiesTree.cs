using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace KBCreator
{
	public class EntitiesTree : System.Windows.Forms.UserControl
	{
        private System.Windows.Forms.TreeView treeView;
        private System.Windows.Forms.MenuItem menuItemAddRelation;
        private System.Windows.Forms.MenuItem menuItemRemoveRelation;
        private System.ComponentModel.IContainer components;
        private System.Windows.Forms.ContextMenu treeViewContextMenu;

        private TreeNode nodeUnderContextMenu;
     
		public EntitiesTree()
		{
			InitializeComponent();
            InitializeTreeViewItems();
            nodeUnderContextMenu = null;
    	}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.treeView = new System.Windows.Forms.TreeView();
            this.treeViewContextMenu = new System.Windows.Forms.ContextMenu();
            this.menuItemAddRelation = new System.Windows.Forms.MenuItem();
            this.menuItemRemoveRelation = new System.Windows.Forms.MenuItem();
            this.SuspendLayout();
            // 
            // treeView
            // 
            this.treeView.ContextMenu = this.treeViewContextMenu;
            this.treeView.ImageIndex = -1;
            this.treeView.Location = new System.Drawing.Point(0, 0);
            this.treeView.Name = "treeView";
            this.treeView.SelectedImageIndex = -1;
            this.treeView.Size = new System.Drawing.Size(150, 480);
            this.treeView.TabIndex = 0;
            this.treeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView_MouseDown);
            this.treeView.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.treeView_ItemDrag);
            // 
            // treeViewContextMenu
            // 
            this.treeViewContextMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                                this.menuItemAddRelation,
                                                                                                this.menuItemRemoveRelation});
            this.treeViewContextMenu.Popup += new System.EventHandler(this.treeViewcontextMenu_Popup);
            // 
            // menuItemAddRelation
            // 
            this.menuItemAddRelation.Index = 0;
            this.menuItemAddRelation.Text = "Add Relation Type";
            this.menuItemAddRelation.Click += new System.EventHandler(this.menuItemAddRelation_Click);
            // 
            // menuItemRemoveRelation
            // 
            this.menuItemRemoveRelation.Index = 1;
            this.menuItemRemoveRelation.Text = "Remove Relation Type";
            this.menuItemRemoveRelation.Click += new System.EventHandler(this.menuItemRemoveRelation_Click);
            // 
            // EntitiesTree
            // 
            this.Controls.Add(this.treeView);
            this.Name = "EntitiesTree";
            this.Size = new System.Drawing.Size(224, 344);
            this.ResumeLayout(false);

        }
		#endregion

        private void InitializeTreeViewItems()
        {
            this.treeView.Nodes.Clear();
            
            TreeNode root = new TreeNode( "Universe" );
            root.ImageIndex = 0;
            root.SelectedImageIndex = 0;
            this.treeView.Nodes.Add( root );
            
            TreeNode nodeObject = new TreeNode( "Object" );
            nodeObject.ImageIndex = 1;
            nodeObject.SelectedImageIndex = 1;
            root.Nodes.Add( nodeObject );
          
            TreeNode nodeRelations = new TreeNode( "Relations" );
            nodeRelations.ImageIndex = 0;
            nodeRelations.ImageIndex = 0;
            root.Nodes.Add( nodeRelations );

            foreach ( Relation rel in formMain.Manager.RelationsTypes )
                AddRelation( rel, nodeRelations );
        }

        public void RefreshTreeView()
        {
            InitializeTreeViewItems();
        }

        private void AddRelation( Relation relation, TreeNode parent )
        {
            TreeNode nodeRelation = parent.Nodes.Add( relation.Name );
            nodeRelation.Tag = relation;
            nodeRelation.SelectedImageIndex = 2;
            nodeRelation.ImageIndex = 2;
        }

        #region Handlers
        private void menuItemAddRelation_Click(object sender, System.EventArgs e)
        {
            AddRelation dialog = new AddRelation();
            if ( dialog.ShowDialog() == DialogResult.OK )
            {
                AddRelation( dialog.Relation, nodeUnderContextMenu );
                formMain.Manager.RelationsTypes.Add( dialog.Relation );
                if ( dialog.PairRelation != null )
                {
                    AddRelation( dialog.PairRelation, nodeUnderContextMenu ); 
                    formMain.Manager.RelationsTypes.Add( dialog.PairRelation );
                }
            }
        }

        private void menuItemRemoveRelation_Click(object sender, System.EventArgs e)
        {
            this.treeView.Nodes.Remove( nodeUnderContextMenu );
            formMain.Manager.RelationsTypes.Remove( nodeUnderContextMenu.Tag as Relation );
        }

        private void treeView_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            nodeUnderContextMenu = treeView.GetNodeAt( e.X, e.Y );
        }

        private void treeViewcontextMenu_Popup(object sender, System.EventArgs e)
        {
            treeViewContextMenu.MenuItems.Clear();
            if ( nodeUnderContextMenu == null )
                return;
            if ( nodeUnderContextMenu.Tag is Relation )
            {
                treeViewContextMenu.MenuItems.Add( menuItemRemoveRelation );
            }
            else if ( nodeUnderContextMenu.Text == "Relations")
            {
                treeViewContextMenu.MenuItems.Add( menuItemAddRelation );
            }
        }

        private void treeView_ItemDrag(object sender, System.Windows.Forms.ItemDragEventArgs e)
        {
            TreeNode treeNode = e.Item as TreeNode;

            if ( null == treeNode)
                return;

            // Move the dragged node when the left mouse button is used.
            if ( treeNode.Text == "Object" || treeNode.Tag is Relation )
            {
                if (e.Button == MouseButtons.Left)
                {
                    DoDragDrop(e.Item, DragDropEffects.Move);
                }
            }
        }
        #endregion
	}
}
