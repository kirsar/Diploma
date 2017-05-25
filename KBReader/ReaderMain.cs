using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace KBReader
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
        private System.Windows.Forms.MainMenu mainMenu1;
        private System.Windows.Forms.MenuItem menuItem1;
        private System.Windows.Forms.ListBox listBoxRelations;
        private System.Windows.Forms.RichTextBox richTextBoxGiven;
        private System.Windows.Forms.RichTextBox richTextBoxResult;
        private System.Windows.Forms.CheckedListBox listBoxObjects;
        private System.Windows.Forms.Button buttonGetAnswer;
        private System.Windows.Forms.Label labelObjs;
        private System.Windows.Forms.Label labelRels;
        private System.Windows.Forms.Label labelGiven;
        private System.Windows.Forms.Label labelAdditionals;
        private System.Windows.Forms.Label labelResult;
        private System.Windows.Forms.MenuItem menuFileLoad;
        private System.Windows.Forms.MenuItem menuFileExit;
        private System.Windows.Forms.Label labelRequested;
        private System.Windows.Forms.RichTextBox richTextBoxRequested;
        private System.Windows.Forms.RichTextBox richTextBoxAdditionals;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Form1()
		{
			InitializeComponent();
		    
            Init();
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
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.menuFileLoad = new System.Windows.Forms.MenuItem();
            this.menuFileExit = new System.Windows.Forms.MenuItem();
            this.listBoxRelations = new System.Windows.Forms.ListBox();
            this.richTextBoxGiven = new System.Windows.Forms.RichTextBox();
            this.richTextBoxRequested = new System.Windows.Forms.RichTextBox();
            this.richTextBoxResult = new System.Windows.Forms.RichTextBox();
            this.listBoxObjects = new System.Windows.Forms.CheckedListBox();
            this.buttonGetAnswer = new System.Windows.Forms.Button();
            this.labelObjs = new System.Windows.Forms.Label();
            this.labelRels = new System.Windows.Forms.Label();
            this.labelGiven = new System.Windows.Forms.Label();
            this.labelAdditionals = new System.Windows.Forms.Label();
            this.labelResult = new System.Windows.Forms.Label();
            this.richTextBoxAdditionals = new System.Windows.Forms.RichTextBox();
            this.labelRequested = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                      this.menuItem1});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
                                                                                      this.menuFileLoad,
                                                                                      this.menuFileExit});
            this.menuItem1.Text = "File";
            // 
            // menuFileLoad
            // 
            this.menuFileLoad.Index = 0;
            this.menuFileLoad.Text = "Load Knowledge Base";
            this.menuFileLoad.Click += new System.EventHandler(this.menuFileLoad_Click);
            // 
            // menuFileExit
            // 
            this.menuFileExit.Index = 1;
            this.menuFileExit.Text = "Exit";
            this.menuFileExit.Click += new System.EventHandler(this.menuFileExit_Click);
            // 
            // listBoxRelations
            // 
            this.listBoxRelations.Items.AddRange(new object[] {
                                                                  "+",
                                                                  "-",
                                                                  "*",
                                                                  "/",
                                                                  "=",
                                                                  "sqr",
                                                                  "sqrt"});
            this.listBoxRelations.Location = new System.Drawing.Point(8, 144);
            this.listBoxRelations.MultiColumn = true;
            this.listBoxRelations.Name = "listBoxRelations";
            this.listBoxRelations.Size = new System.Drawing.Size(408, 95);
            this.listBoxRelations.TabIndex = 1;
            // 
            // richTextBoxGiven
            // 
            this.richTextBoxGiven.Location = new System.Drawing.Point(8, 264);
            this.richTextBoxGiven.Name = "richTextBoxGiven";
            this.richTextBoxGiven.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBoxGiven.Size = new System.Drawing.Size(112, 96);
            this.richTextBoxGiven.TabIndex = 2;
            this.richTextBoxGiven.Text = "";
            // 
            // richTextBoxRequested
            // 
            this.richTextBoxRequested.Location = new System.Drawing.Point(288, 264);
            this.richTextBoxRequested.Name = "richTextBoxRequested";
            this.richTextBoxRequested.Size = new System.Drawing.Size(128, 96);
            this.richTextBoxRequested.TabIndex = 3;
            this.richTextBoxRequested.Text = "";
            // 
            // richTextBoxResult
            // 
            this.richTextBoxResult.Location = new System.Drawing.Point(8, 384);
            this.richTextBoxResult.Name = "richTextBoxResult";
            this.richTextBoxResult.Size = new System.Drawing.Size(408, 112);
            this.richTextBoxResult.TabIndex = 4;
            this.richTextBoxResult.Text = "";
            // 
            // listBoxObjects
            // 
            this.listBoxObjects.Location = new System.Drawing.Point(8, 24);
            this.listBoxObjects.MultiColumn = true;
            this.listBoxObjects.Name = "listBoxObjects";
            this.listBoxObjects.Size = new System.Drawing.Size(408, 94);
            this.listBoxObjects.TabIndex = 5;
            // 
            // buttonGetAnswer
            // 
            this.buttonGetAnswer.Location = new System.Drawing.Point(8, 504);
            this.buttonGetAnswer.Name = "buttonGetAnswer";
            this.buttonGetAnswer.Size = new System.Drawing.Size(410, 23);
            this.buttonGetAnswer.TabIndex = 7;
            this.buttonGetAnswer.Text = "Get Answer";
            this.buttonGetAnswer.Click += new System.EventHandler(this.buttonGetAnswer_Click);
            // 
            // labelObjs
            // 
            this.labelObjs.Location = new System.Drawing.Point(8, 8);
            this.labelObjs.Name = "labelObjs";
            this.labelObjs.Size = new System.Drawing.Size(100, 16);
            this.labelObjs.TabIndex = 8;
            this.labelObjs.Text = "Objects:";
            // 
            // labelRels
            // 
            this.labelRels.Location = new System.Drawing.Point(8, 128);
            this.labelRels.Name = "labelRels";
            this.labelRels.Size = new System.Drawing.Size(100, 16);
            this.labelRels.TabIndex = 9;
            this.labelRels.Text = "Relations:";
            // 
            // labelGiven
            // 
            this.labelGiven.Location = new System.Drawing.Point(8, 248);
            this.labelGiven.Name = "labelGiven";
            this.labelGiven.Size = new System.Drawing.Size(100, 16);
            this.labelGiven.TabIndex = 10;
            this.labelGiven.Text = "Given Objects:";
            // 
            // labelAdditionals
            // 
            this.labelAdditionals.Location = new System.Drawing.Point(136, 248);
            this.labelAdditionals.Name = "labelAdditionals";
            this.labelAdditionals.Size = new System.Drawing.Size(100, 16);
            this.labelAdditionals.TabIndex = 11;
            this.labelAdditionals.Text = "Additional facts:";
            // 
            // labelResult
            // 
            this.labelResult.Location = new System.Drawing.Point(8, 368);
            this.labelResult.Name = "labelResult";
            this.labelResult.Size = new System.Drawing.Size(100, 16);
            this.labelResult.TabIndex = 12;
            this.labelResult.Text = "Result reasonings:";
            // 
            // richTextBoxAdditionals
            // 
            this.richTextBoxAdditionals.Location = new System.Drawing.Point(128, 264);
            this.richTextBoxAdditionals.Name = "richTextBoxAdditionals";
            this.richTextBoxAdditionals.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.richTextBoxAdditionals.Size = new System.Drawing.Size(152, 96);
            this.richTextBoxAdditionals.TabIndex = 13;
            this.richTextBoxAdditionals.Text = "";
            // 
            // labelRequested
            // 
            this.labelRequested.Location = new System.Drawing.Point(288, 248);
            this.labelRequested.Name = "labelRequested";
            this.labelRequested.Size = new System.Drawing.Size(104, 16);
            this.labelRequested.TabIndex = 14;
            this.labelRequested.Text = "Requested objects:";
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(424, 529);
            this.Controls.Add(this.labelRequested);
            this.Controls.Add(this.richTextBoxAdditionals);
            this.Controls.Add(this.labelResult);
            this.Controls.Add(this.labelAdditionals);
            this.Controls.Add(this.labelGiven);
            this.Controls.Add(this.labelRels);
            this.Controls.Add(this.labelObjs);
            this.Controls.Add(this.buttonGetAnswer);
            this.Controls.Add(this.listBoxObjects);
            this.Controls.Add(this.richTextBoxResult);
            this.Controls.Add(this.richTextBoxRequested);
            this.Controls.Add(this.richTextBoxGiven);
            this.Controls.Add(this.listBoxRelations);
            this.Menu = this.mainMenu1;
            this.Name = "Form1";
            this.Text = "KB Reader";
            this.ResumeLayout(false);

        }
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}

        private void Init()
        {
            InitObjs();
            InitRels();
            this.richTextBoxGiven.Text = "cos2a";
            this.richTextBoxRequested.Text = "ctg2a";
            this.richTextBoxResult.Text += "sin = sqrt(((1 - cos2a ) / 2)); \n";
            this.richTextBoxResult.Text += "cos = sqrt(1 - (sina * sina)); \n";
            this.richTextBoxResult.Text += "tg = (sina / cosa); \n";
            this.richTextBoxResult.Text += "ctg = (1 / tga); \n";
            this.richTextBoxResult.Text += "ctg2 = (((ctga * ctga) - 1) / (2 * ctga)); \n";
        }

        private void InitObjs()
        {
            this.listBoxObjects.Items.Add( "cosa", CheckState.Unchecked );
            this.listBoxObjects.Items.Add( "sina", CheckState.Unchecked );
            this.listBoxObjects.Items.Add( "tga", CheckState.Unchecked );
            this.listBoxObjects.Items.Add( "ctga", CheckState.Unchecked );
            this.listBoxObjects.Items.Add( "cos2a", CheckState.Checked );
            this.listBoxObjects.Items.Add( "sin2a", CheckState.Unchecked );
            this.listBoxObjects.Items.Add( "tg2a", CheckState.Unchecked );
            this.listBoxObjects.Items.Add( "ctg2a", CheckState.Unchecked );
            this.listBoxObjects.Items.Add( "alpha", CheckState.Unchecked );
            this.listBoxObjects.Items.Add( "betta", CheckState.Unchecked );
            this.listBoxObjects.Items.Add( "gamma", CheckState.Unchecked );
            this.listBoxObjects.Items.Add( "a", CheckState.Unchecked );
            this.listBoxObjects.Items.Add( "b", CheckState.Unchecked );
            this.listBoxObjects.Items.Add( "c", CheckState.Unchecked );
        }

        private void InitRels()
        {
        }

        private void ParseObjs()
        {
        }

        private void ParseRels()
        {
        }


        private void AddFactToBase()
        {
        }

        private void GenerateReasoning()
        {
        }
        
        
        private void menuFileExit_Click(object sender, System.EventArgs e)
        {
            Application.Exit();
        }

        private void menuFileLoad_Click(object sender, System.EventArgs e)
        {
            SelectKBase dlg = new SelectKBase();
            if ( dlg.ShowDialog() == DialogResult.OK )
            {

            }
        
        }
        private void buttonGetAnswer_Click(object sender, System.EventArgs e)
        {
            GenerateReasoning();
        }


        
	}
}
