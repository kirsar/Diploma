using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;


namespace KBCreator
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class formMain : System.Windows.Forms.Form
	{
		private System.Windows.Forms.MainMenu mainMenu;
		private KBCreator.ModelAreaControl modelAreaControl1;
		private System.Windows.Forms.TextBox textBoxName;
		private System.Windows.Forms.Label labelName;
        private KBCreator.EntitiesTree entitiesTree1;
        private System.ComponentModel.Container components = null;
        private System.Windows.Forms.Label labelPropertry;
        private System.Windows.Forms.ComboBox comboBoxProperty;
        private System.Windows.Forms.MenuItem menuBaseClear;
        private System.Windows.Forms.MenuItem menuUniEnlarge;
        private System.Windows.Forms.MenuItem menuUniClear;
        private System.Windows.Forms.MenuItem menuUniSave;
        private System.Windows.Forms.MenuItem menuBaseSave;
        private System.Windows.Forms.MenuItem menuBaseLoad;
        private System.Windows.Forms.MenuItem menuUniLoad;
        private System.Windows.Forms.MenuItem menuFileExit;
        private System.Windows.Forms.MenuItem menuFile;
        private System.Windows.Forms.MenuItem menuUni;
        private System.Windows.Forms.MenuItem menuBase;
        private System.Windows.Forms.MenuItem menuGeneration;
        private System.Windows.Forms.MenuItem menuGenerateFacts;
        private System.Windows.Forms.ListBox listBoxFacts;
        private System.Windows.Forms.MenuItem menuGeneratePCodeFromGraph;
        private System.Windows.Forms.MenuItem menuGeneratePCodeFromText;
        private System.Windows.Forms.TextBox textBoxLogicalName;
        private System.Windows.Forms.Label labelLogicalName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox richTextBoxFact;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBoxSyncWithName;
        private System.Windows.Forms.ContextMenu textFactContextMenu;
        public static KBManager Manager = new KBManager();
        
        public formMain()
        {
            try
            {
                InitializeComponent();
                InitializeCombo();
                modelAreaControl1.objectSelectedEvent += new ModelAreaControl.ObjectSelectedEventHandler( this.ObjectSelected_Handler );
                //Manager.LoadBase();
                //Manager.LoadUniverse();
                //Manager.LoadBase();
                entitiesTree1.RefreshTreeView();
                //listBoxFacts.Items.Add( "(cos2a equals (1 subt (2 mult sqr(sina))) )" );
                //listBoxFacts.Items.Add( "(sina equals sqrt(((1 subt cos2a) divide 2)))" );
                //listBoxFacts.Items.Add( "(1 equals (cos2a sum ( 2 mult (sqr(sina)))))" );
                //listBoxFacts.Items.Add( "(2 equals (( 1 subt (cos2a)) divide sqr(sina)))" );
            }
            catch( Exception ex )
            {
                MessageBox.Show( ex.Message );
            }
        
        }
        private void InitializeCombo()
        {
            this.comboBoxProperty.Items.Clear();
            foreach( Relation rel in Manager.RelationsTypes )
            {
                int index = this.comboBoxProperty.Items.Add( rel.Name );
            }
        }

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.mainMenu = new System.Windows.Forms.MainMenu();
            this.menuFile = new System.Windows.Forms.MenuItem();
            this.menuFileExit = new System.Windows.Forms.MenuItem();
            this.menuUni = new System.Windows.Forms.MenuItem();
            this.menuUniLoad = new System.Windows.Forms.MenuItem();
            this.menuUniEnlarge = new System.Windows.Forms.MenuItem();
            this.menuUniSave = new System.Windows.Forms.MenuItem();
            this.menuUniClear = new System.Windows.Forms.MenuItem();
            this.menuBase = new System.Windows.Forms.MenuItem();
            this.menuBaseSave = new System.Windows.Forms.MenuItem();
            this.menuBaseLoad = new System.Windows.Forms.MenuItem();
            this.menuBaseClear = new System.Windows.Forms.MenuItem();
            this.menuGeneration = new System.Windows.Forms.MenuItem();
            this.menuGenerateFacts = new System.Windows.Forms.MenuItem();
            this.menuGeneratePCodeFromGraph = new System.Windows.Forms.MenuItem();
            this.menuGeneratePCodeFromText = new System.Windows.Forms.MenuItem();
            this.modelAreaControl1 = new KBCreator.ModelAreaControl();
            this.entitiesTree1 = new KBCreator.EntitiesTree();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelName = new System.Windows.Forms.Label();
            this.labelPropertry = new System.Windows.Forms.Label();
            this.comboBoxProperty = new System.Windows.Forms.ComboBox();
            this.listBoxFacts = new System.Windows.Forms.ListBox();
            this.textBoxLogicalName = new System.Windows.Forms.TextBox();
            this.labelLogicalName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.richTextBoxFact = new System.Windows.Forms.RichTextBox();
            this.textFactContextMenu = new System.Windows.Forms.ContextMenu();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBoxSyncWithName = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                     this.menuFile,
                                                                                     this.menuUni,
                                                                                     this.menuBase,
                                                                                     this.menuGeneration});
            // 
            // menuFile
            // 
            this.menuFile.Index = 0;
            this.menuFile.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                     this.menuFileExit});
            this.menuFile.Text = "File";
            // 
            // menuFileExit
            // 
            this.menuFileExit.Index = 0;
            this.menuFileExit.Text = "Exit";
            this.menuFileExit.Click += new System.EventHandler(this.menuFileExit_Click);
            // 
            // menuUni
            // 
            this.menuUni.Index = 1;
            this.menuUni.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                    this.menuUniLoad,
                                                                                    this.menuUniEnlarge,
                                                                                    this.menuUniSave,
                                                                                    this.menuUniClear});
            this.menuUni.Text = "Universe";
            // 
            // menuUniLoad
            // 
            this.menuUniLoad.Index = 0;
            this.menuUniLoad.Text = "Load";
            this.menuUniLoad.Click += new System.EventHandler(this.menuUniLoad_Click);
            // 
            // menuUniEnlarge
            // 
            this.menuUniEnlarge.Index = 1;
            this.menuUniEnlarge.Text = "Enlarge";
            this.menuUniEnlarge.Click += new System.EventHandler(this.menuUniEnlarge_Click);
            // 
            // menuUniSave
            // 
            this.menuUniSave.Index = 2;
            this.menuUniSave.Text = "Save";
            this.menuUniSave.Click += new System.EventHandler(this.menuUniSave_Click);
            // 
            // menuUniClear
            // 
            this.menuUniClear.Index = 3;
            this.menuUniClear.Text = "Clear";
            this.menuUniClear.Click += new System.EventHandler(this.menuUniClear_Click);
            // 
            // menuBase
            // 
            this.menuBase.Index = 2;
            this.menuBase.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                     this.menuBaseSave,
                                                                                     this.menuBaseLoad,
                                                                                     this.menuBaseClear});
            this.menuBase.Text = "Knowledge Base";
            // 
            // menuBaseSave
            // 
            this.menuBaseSave.Index = 0;
            this.menuBaseSave.Text = "Save";
            this.menuBaseSave.Click += new System.EventHandler(this.menuBaseSave_Click);
            // 
            // menuBaseLoad
            // 
            this.menuBaseLoad.Index = 1;
            this.menuBaseLoad.Text = "Load";
            this.menuBaseLoad.Click += new System.EventHandler(this.menuBaseLoad_Click);
            // 
            // menuBaseClear
            // 
            this.menuBaseClear.Index = 2;
            this.menuBaseClear.Text = "Clear";
            this.menuBaseClear.Click += new System.EventHandler(this.menuBaseClear_Click);
            // 
            // menuGeneration
            // 
            this.menuGeneration.Index = 3;
            this.menuGeneration.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                           this.menuGenerateFacts,
                                                                                           this.menuGeneratePCodeFromGraph,
                                                                                           this.menuGeneratePCodeFromText});
            this.menuGeneration.Text = "Generation";
            // 
            // menuGenerateFacts
            // 
            this.menuGenerateFacts.Index = 0;
            this.menuGenerateFacts.Text = "Generate Facts From Graph";
            this.menuGenerateFacts.Click += new System.EventHandler(this.menuGenerateFacts_Click);
            // 
            // menuGeneratePCodeFromGraph
            // 
            this.menuGeneratePCodeFromGraph.Index = 1;
            this.menuGeneratePCodeFromGraph.Text = "Generate Facts From Text";
            this.menuGeneratePCodeFromGraph.Click += new System.EventHandler(this.menuGeneratePCode_Click);
            // 
            // menuGeneratePCodeFromText
            // 
            this.menuGeneratePCodeFromText.Index = 2;
            this.menuGeneratePCodeFromText.Text = "Generate Prolog Code From Facts";
            this.menuGeneratePCodeFromText.Click += new System.EventHandler(this.menuGeneratePCodeFromText_Click);
            // 
            // modelAreaControl1
            // 
            this.modelAreaControl1.AllowDrop = true;
            this.modelAreaControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.modelAreaControl1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.modelAreaControl1.Location = new System.Drawing.Point(152, 0);
            this.modelAreaControl1.Name = "modelAreaControl1";
            this.modelAreaControl1.Size = new System.Drawing.Size(544, 392);
            this.modelAreaControl1.TabIndex = 0;
            //
            // entitiesTree1
            // 
            this.entitiesTree1.AllowDrop = false;
            this.entitiesTree1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
                | System.Windows.Forms.AnchorStyles.Left) 
                | System.Windows.Forms.AnchorStyles.Right)));
            this.entitiesTree1.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.entitiesTree1.Location = new System.Drawing.Point(0, 0);
            this.entitiesTree1.Name = "entitiesTree1";
            this.entitiesTree1.Size = new System.Drawing.Size(152, 387);
            this.entitiesTree1.TabIndex = 8;
            // 
            // textBoxName
            // 
            this.textBoxName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxName.Location = new System.Drawing.Point(328, 456);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(360, 20);
            this.textBoxName.TabIndex = 4;
            this.textBoxName.Text = "";
            this.textBoxName.Visible = false;
            this.textBoxName.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // labelName
            // 
            this.labelName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelName.Location = new System.Drawing.Point(256, 456);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(72, 23);
            this.labelName.TabIndex = 5;
            this.labelName.Text = "Object Name";
            this.labelName.Visible = false;
            // 
            // labelPropertry
            // 
            this.labelPropertry.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelPropertry.Location = new System.Drawing.Point(256, 528);
            this.labelPropertry.Name = "labelPropertry";
            this.labelPropertry.Size = new System.Drawing.Size(72, 23);
            this.labelPropertry.TabIndex = 6;
            this.labelPropertry.Text = "Property";
            this.labelPropertry.Visible = false;
            // 
            // comboBoxProperty
            // 
            this.comboBoxProperty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBoxProperty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProperty.Location = new System.Drawing.Point(328, 528);
            this.comboBoxProperty.Name = "comboBoxProperty";
            this.comboBoxProperty.Size = new System.Drawing.Size(360, 21);
            this.comboBoxProperty.TabIndex = 7;
            this.comboBoxProperty.Visible = false;
            this.comboBoxProperty.SelectedIndexChanged += new System.EventHandler(this.comboBoxProperty_SelectedIndexChanged);
            // 
            // listBoxFacts
            // 
            this.listBoxFacts.Location = new System.Drawing.Point(8, 424);
            this.listBoxFacts.Name = "listBoxFacts";
            this.listBoxFacts.Size = new System.Drawing.Size(240, 134);
            this.listBoxFacts.TabIndex = 8;
            // 
            // textBoxLogicalName
            // 
            this.textBoxLogicalName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLogicalName.Location = new System.Drawing.Point(328, 504);
            this.textBoxLogicalName.Name = "textBoxLogicalName";
            this.textBoxLogicalName.Size = new System.Drawing.Size(360, 20);
            this.textBoxLogicalName.TabIndex = 9;
            this.textBoxLogicalName.Text = "";
            this.textBoxLogicalName.Visible = false;
            this.textBoxLogicalName.TextChanged += new System.EventHandler(this.textBoxLogicalName_TextChanged);
            // 
            // labelLogicalName
            // 
            this.labelLogicalName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelLogicalName.Location = new System.Drawing.Point(256, 504);
            this.labelLogicalName.Name = "labelLogicalName";
            this.labelLogicalName.Size = new System.Drawing.Size(73, 23);
            this.labelLogicalName.TabIndex = 10;
            this.labelLogicalName.Text = "Logical Name";
            this.labelLogicalName.Visible = false;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 400);
            this.label1.Name = "label1";
            this.label1.TabIndex = 11;
            this.label1.Text = "Facts:";
            // 
            // richTextBoxFact
            // 
            this.richTextBoxFact.ContextMenu = this.textFactContextMenu;
            this.richTextBoxFact.DetectUrls = false;
            this.richTextBoxFact.Location = new System.Drawing.Point(256, 424);
            this.richTextBoxFact.Name = "richTextBoxFact";
            this.richTextBoxFact.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Horizontal;
            this.richTextBoxFact.Size = new System.Drawing.Size(432, 24);
            this.richTextBoxFact.TabIndex = 12;
            this.richTextBoxFact.Text = "";
            // 
            // textFactContextMenu
            // 
            this.textFactContextMenu.Popup += new System.EventHandler(this.textFactContextMenu_Popup);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(256, 400);
            this.label2.Name = "label2";
            this.label2.TabIndex = 13;
            this.label2.Text = "Text fact";
            // 
            // checkBoxSyncWithName
            // 
            this.checkBoxSyncWithName.Checked = true;
            this.checkBoxSyncWithName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSyncWithName.Location = new System.Drawing.Point(256, 480);
            this.checkBoxSyncWithName.Name = "checkBoxSyncWithName";
            this.checkBoxSyncWithName.Size = new System.Drawing.Size(160, 24);
            this.checkBoxSyncWithName.TabIndex = 14;
            this.checkBoxSyncWithName.Text = "Synchronize with name";
            this.checkBoxSyncWithName.Visible = false;
            // 
            // formMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(696, 561);
            this.Controls.Add(this.checkBoxSyncWithName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.richTextBoxFact);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelLogicalName);
            this.Controls.Add(this.textBoxLogicalName);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.modelAreaControl1);
            this.Controls.Add(this.entitiesTree1);
            this.Controls.Add(this.listBoxFacts);
            this.Controls.Add(this.comboBoxProperty);
            this.Controls.Add(this.labelPropertry);
            this.Controls.Add(this.labelName);
            this.Menu = this.mainMenu;
            this.Name = "formMain";
            this.Text = "KB Creator";
            this.ResumeLayout(false);

        }
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new formMain());
		}

        private void ObjectSelected_Handler( object selectedObject )
        {
            try
            {
                if ( selectedObject == null )
                {
                    this.labelPropertry.Visible = false;
                    this.comboBoxProperty.Visible = false;
                    this.textBoxName.Visible = false;
                    this.labelName.Visible = false;
                    this.labelLogicalName.Visible = false;
                    this.textBoxLogicalName.Visible = false;
                    this.checkBoxSyncWithName.Visible = false;
                }
                if ( selectedObject is KBObject )
                {
                    this.labelPropertry.Visible = true;
                    this.comboBoxProperty.Visible = true;
                    this.textBoxName.Visible = true;
                    this.labelName.Visible = true;
                    this.labelLogicalName.Visible = true;
                    this.textBoxLogicalName.Visible = true;
                    this.checkBoxSyncWithName.Visible = true;
                    InitializeCombo();
                    if ( comboBoxProperty.Items.Count == 0 )
                        comboBoxProperty.Enabled = false;
                    else
                    {
                        comboBoxProperty.Enabled = true;
                        int index = ((selectedObject as KBObject).Property != null ) ? this.comboBoxProperty.Items.IndexOf( (selectedObject as KBObject).Property.Name ) : -1;
                        this.comboBoxProperty.SelectedIndex = (index != - 1) ? index : 0;
                    }
                    this.textBoxName.Text = (selectedObject as KBObject).Name;
                    this.textBoxLogicalName.Text = (selectedObject as KBObject).PCodeName;
                }
                else if ( selectedObject is Relation )
                {
                    this.labelPropertry.Visible = false;
                    this.textBoxName.Visible = false;
                    this.labelName.Visible = false;
                    this.comboBoxProperty.Visible = false;
                    this.labelLogicalName.Visible = false;
                    this.textBoxLogicalName.Visible = false;
                    this.checkBoxSyncWithName.Visible = false;
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show ( ex.Message );
            }

        }

        private void comboBoxProperty_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            (modelAreaControl1.SelectedObject as KBObject).Property = Relation.StringToRelation( comboBoxProperty.SelectedItem.ToString() );
            modelAreaControl1.Redraw( null );
        }

        private void textBoxName_TextChanged(object sender, System.EventArgs e)
        {
            (modelAreaControl1.SelectedObject as KBObject).Name = this.textBoxName.Text;
            modelAreaControl1.Redraw( null );
            if ( this.checkBoxSyncWithName.Checked )
            {
                if ( !formMain.Manager.FilterName( this.textBoxName.Text) )
                    this.textBoxLogicalName.Text = this.textBoxName.Text;
                else
                    this.textBoxLogicalName.Text = "";
            }
        }

        private void textBoxLogicalName_TextChanged(object sender, System.EventArgs e)
        {
            (modelAreaControl1.SelectedObject as KBObject).PCodeName = this.textBoxLogicalName.Text;
        }

        private void menuFileExit_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void menuUniSave_Click(object sender, System.EventArgs e)
        {
            SaveFileDialog saver = new SaveFileDialog();
            saver.InitialDirectory = "c:\\" ;
            saver.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*" ;
            saver.FilterIndex = 2 ;
            saver.RestoreDirectory = true ;
            if ( saver.ShowDialog() == DialogResult.OK )
            {
                formMain.Manager.UniversePath = saver.FileName;
                formMain.Manager.SaveUniverse();
            }
        }

        private void menuUniLoad_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog loader = new OpenFileDialog();
            loader.InitialDirectory = "c:\\" ;
            loader.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*" ;
            loader.FilterIndex = 2 ;
            loader.RestoreDirectory = true ;
            if ( loader.ShowDialog() == DialogResult.OK )
            {
                formMain.Manager.UniversePath = loader.FileName;
                formMain.Manager.LoadUniverse();
                entitiesTree1.RefreshTreeView();
            }
        }

        private void menuUniEnlarge_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog loader = new OpenFileDialog();
            loader.InitialDirectory = "c:\\" ;
            loader.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*" ;
            loader.FilterIndex = 2 ;
            loader.RestoreDirectory = true ;
            if ( loader.ShowDialog() == DialogResult.OK )
            {
                formMain.Manager.UniversePath = loader.FileName;
                formMain.Manager.EnlargeUniverse();
                entitiesTree1.RefreshTreeView();
            }
        }

        private void menuUniClear_Click(object sender, System.EventArgs e)
        {
            formMain.Manager.ClearUniverse();
            entitiesTree1.RefreshTreeView();
            modelAreaControl1.Redraw( null );
        }

        private void menuBaseSave_Click(object sender, System.EventArgs e)
        {
            SaveFileDialog saver = new SaveFileDialog();
            saver.InitialDirectory = "c:\\" ;
            saver.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*" ;
            saver.FilterIndex = 2 ;
            saver.RestoreDirectory = true ;
            if ( saver.ShowDialog() == DialogResult.OK )
            {
                formMain.Manager.KBasePath = saver.FileName;
                formMain.Manager.SaveBase();
            }
        }

        private void menuBaseLoad_Click(object sender, System.EventArgs e)
        {
            formMain.Manager.ClearBase();
            this.listBoxFacts.Items.Clear();
            modelAreaControl1.Redraw( null );
                  
            OpenFileDialog loader = new OpenFileDialog();
            loader.InitialDirectory = "c:\\" ;
            loader.Filter = "xml files (*.xml)|*.xml|All files (*.*)|*.*" ;
            loader.FilterIndex = 2 ;
            loader.RestoreDirectory = true ;
            if ( loader.ShowDialog() == DialogResult.OK )
            {
                formMain.Manager.KBasePath = loader.FileName;
                formMain.Manager.LoadBase();
                this.modelAreaControl1.Redraw( null );
            }
        }

        private void menuBaseClear_Click(object sender, System.EventArgs e)
        {
            formMain.Manager.ClearBase();
            this.listBoxFacts.Items.Clear();
            modelAreaControl1.Redraw( null );
        }

        private void menuGenerateFacts_Click(object sender, System.EventArgs e)
        {
            this.richTextBoxFact.Text = "";
            formMain.Manager.GenerateFacts();

            this.listBoxFacts.Items.Clear();
            foreach( RelationsList fact in formMain.Manager.Facts )
                this.listBoxFacts.Items.Add( fact.Fact );
        }

        private void menuGeneratePCode_Click(object sender, System.EventArgs e)
        {
            this.modelAreaControl1.Clear();

            formMain.Manager.GeneratePCodeFromText( this.richTextBoxFact.Text );
            this.listBoxFacts.Items.Clear();
            foreach( RelationsList fact in formMain.Manager.Facts )
                this.listBoxFacts.Items.Add( fact.Fact );
        }

        private void menuGeneratePCodeFromText_Click(object sender, System.EventArgs e)
        {
            PCodeOptions optionsDialog = new PCodeOptions();
            if ( optionsDialog.ShowDialog() == DialogResult.OK )
            {
                formMain.Manager.PCodePath = optionsDialog.PCodePath;
                formMain.Manager.PCodeOption = optionsDialog.PCodeOption;
                formMain.Manager.GeneratePCode();
            }
        }

        private void textFactContextMenu_Popup(object sender, System.EventArgs e)
        {
            textFactContextMenu.MenuItems.Clear();
            foreach ( Relation rel in formMain.Manager.RelationsTypes )
            {
                System.EventHandler onClick = new EventHandler( textFactContextMenuItem_Click );
                MenuItem relItem = new MenuItem( rel.Name, onClick );
                textFactContextMenu.MenuItems.Add( relItem );
            }
        }

        private void textFactContextMenuItem_Click( object sender, System.EventArgs e )
        {
            if ( sender is MenuItem )
            this.richTextBoxFact.AppendText( (sender as MenuItem).Text );
        }
   	}
}
