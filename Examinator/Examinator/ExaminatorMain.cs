/*------------------------------------------------------------------------------------------------
// File:        Form1.cs
// Description: The file contains definition of ExaminatorMain form.
// Author:      Kirill V. Sorokin; saron312@yandex.ru
// Copyright:   (c) SPSU, MM 2005
//------------------------------------------------------------------------------------------------
// Modification history:
//----------------------------------------------------------------------------------------------*/
using System;
using System.Collections;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Diagnostics;

namespace Examinator
{
	public class ExaminatorMain : System.Windows.Forms.Form
	{
		private System.ComponentModel.Container components = null;
		private Settings m_cSettings;
		private Analyzer m_cAnalyzer;
		private XMLManager m_cXMLManager;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItemExit;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItemHowTo;
		private System.Windows.Forms.MenuItem menuItemAbout;
		private System.Windows.Forms.RichTextBox richTextBoxQuestion;
		private System.Windows.Forms.RichTextBox richTextBoxAnswer;
		private System.Windows.Forms.Button buttonPrevios;
		private System.Windows.Forms.Button buttonNext;
		private System.Windows.Forms.Label labelQuestion;
		private System.Windows.Forms.Label labelAnswer;
		private System.Windows.Forms.MainMenu mainMenu;
		private System.Windows.Forms.Button buttonClose;
		private System.Windows.Forms.Button buttonHelp; 
		private string m_strSettingsFile;
		
		public ExaminatorMain()
		{
			m_cXMLManager = new XMLManager();
			InitializeComponent();
			InitDefaultSettings();
			//InitUserSettings();
			m_cAnalyzer = new Analyzer( m_cSettings );
			TreeItem cFirstItem = m_cSettings.m_cTree.GetNextItem();
			LoadTreeItem( cFirstItem );
			richTextBoxQuestion.BackColor = Color.White;
			richTextBoxQuestion.ForeColor = Color.Black;
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
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItemExit = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItemHowTo = new System.Windows.Forms.MenuItem();
			this.menuItemAbout = new System.Windows.Forms.MenuItem();
			this.richTextBoxQuestion = new System.Windows.Forms.RichTextBox();
			this.richTextBoxAnswer = new System.Windows.Forms.RichTextBox();
			this.buttonPrevios = new System.Windows.Forms.Button();
			this.buttonNext = new System.Windows.Forms.Button();
			this.labelQuestion = new System.Windows.Forms.Label();
			this.labelAnswer = new System.Windows.Forms.Label();
			this.buttonClose = new System.Windows.Forms.Button();
			this.buttonHelp = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// mainMenu
			// 
			this.mainMenu.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					 this.menuItem1,
																					 this.menuItem2});
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItemExit});
			this.menuItem1.Text = "&File";
			// 
			// menuItemExit
			// 
			this.menuItemExit.Index = 0;
			this.menuItemExit.Text = "&Exit";
			this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 1;
			this.menuItem2.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItemHowTo,
																					  this.menuItemAbout});
			this.menuItem2.Text = "&Help";
			// 
			// menuItemHowTo
			// 
			this.menuItemHowTo.Index = 0;
			this.menuItemHowTo.Text = "&How to...";
			// 
			// menuItemAbout
			// 
			this.menuItemAbout.Index = 1;
			this.menuItemAbout.Text = "&About";
			// 
			// richTextBoxQuestion
			// 
			this.richTextBoxQuestion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.richTextBoxQuestion.Enabled = false;
			this.richTextBoxQuestion.Location = new System.Drawing.Point(8, 30);
			this.richTextBoxQuestion.Name = "richTextBoxQuestion";
			this.richTextBoxQuestion.Size = new System.Drawing.Size(352, 112);
			this.richTextBoxQuestion.TabIndex = 0;
			this.richTextBoxQuestion.Text = "";
			// 
			// richTextBoxAnswer
			// 
			this.richTextBoxAnswer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.richTextBoxAnswer.Location = new System.Drawing.Point(8, 206);
			this.richTextBoxAnswer.Name = "richTextBoxAnswer";
			this.richTextBoxAnswer.Size = new System.Drawing.Size(352, 112);
			this.richTextBoxAnswer.TabIndex = 1;
			this.richTextBoxAnswer.Text = "";
			// 
			// buttonPrevios
			// 
			this.buttonPrevios.Enabled = false;
			this.buttonPrevios.Location = new System.Drawing.Point(8, 152);
			this.buttonPrevios.Name = "buttonPrevios";
			this.buttonPrevios.Size = new System.Drawing.Size(76, 23);
			this.buttonPrevios.TabIndex = 2;
			this.buttonPrevios.Text = "<< Previous      ";
			// 
			// buttonNext
			// 
			this.buttonNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonNext.Location = new System.Drawing.Point(285, 152);
			this.buttonNext.Name = "buttonNext";
			this.buttonNext.Size = new System.Drawing.Size(76, 23);
			this.buttonNext.TabIndex = 3;
			this.buttonNext.Text = "  Next >>";
			this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
			// 
			// labelQuestion
			// 
			this.labelQuestion.Location = new System.Drawing.Point(8, 8);
			this.labelQuestion.Name = "labelQuestion";
			this.labelQuestion.Size = new System.Drawing.Size(100, 14);
			this.labelQuestion.TabIndex = 4;
			this.labelQuestion.Text = "Question:";
			// 
			// labelAnswer
			// 
			this.labelAnswer.Location = new System.Drawing.Point(8, 184);
			this.labelAnswer.Name = "labelAnswer";
			this.labelAnswer.Size = new System.Drawing.Size(100, 14);
			this.labelAnswer.TabIndex = 5;
			this.labelAnswer.Text = "Answer:";
			// 
			// buttonClose
			// 
			this.buttonClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonClose.Location = new System.Drawing.Point(288, 368);
			this.buttonClose.Name = "buttonClose";
			this.buttonClose.TabIndex = 6;
			this.buttonClose.Text = "Close";
			this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
			// 
			// buttonHelp
			// 
			this.buttonHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonHelp.Location = new System.Drawing.Point(208, 368);
			this.buttonHelp.Name = "buttonHelp";
			this.buttonHelp.TabIndex = 7;
			this.buttonHelp.Text = "Help";
			// 
			// ExaminatorMain
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(368, 398);
			this.Controls.Add(this.buttonHelp);
			this.Controls.Add(this.buttonClose);
			this.Controls.Add(this.labelAnswer);
			this.Controls.Add(this.labelQuestion);
			this.Controls.Add(this.buttonNext);
			this.Controls.Add(this.buttonPrevios);
			this.Controls.Add(this.richTextBoxAnswer);
			this.Controls.Add(this.richTextBoxQuestion);
			this.Menu = this.mainMenu;
			this.Name = "ExaminatorMain";
			this.Text = "Form1";
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new ExaminatorMain());
		}

		private void InitDefaultSettings()
		{
			m_strSettingsFile = "settings.xml";
			m_cSettings.strTreeFile   = "tree.xml";
			m_cSettings.m_cTree       = new ExamTree();
			m_cSettings.m_cTree = m_cXMLManager.GetTreeFromXML( m_cSettings.strTreeFile );
			m_cSettings.strBaseBufferInFile = "buffin.txt";
			m_cSettings.strBaseBufferOutFile = "buffout.txt";
			m_cSettings.strBaseExecuteFile = "trigbase.exe";
		}

		private void InitUserSettings()
		{
			try
			{
			
			}
			catch ( Exception ex )
			{
				Debug.Assert( false, ex.Message );
			}
		}

		private void LoadTreeItem( TreeItem cItem )
		{
			richTextBoxQuestion.Text = cItem.QuestionItem;
		}

		private void menuItemExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void buttonNext_Click(object sender, System.EventArgs e)
		{
			Mark cCurrMark = new Mark();
			TreeItem cCurrItem = new TreeItem();
			string strAnswer;
			
			cCurrItem = m_cSettings.m_cTree.GetCurrItem();
			strAnswer = richTextBoxAnswer.Text;
			if ( cCurrItem.MultiExpr )
				cCurrMark =  m_cAnalyzer.AnalyzeMultiExpression( cCurrItem.Input, cCurrItem.Output, strAnswer );
			else
				cCurrMark =  m_cAnalyzer.AnalyzeSingleExpression( cCurrItem.Input, cCurrItem.Output, strAnswer );
			m_cSettings.m_cTree.m_cMarks.Add( cCurrMark );
			m_cSettings.m_cTree.SetItemChild( cCurrMark );
			cCurrItem = m_cSettings.m_cTree.GetNextItem();
			LoadTreeItem( cCurrItem );
			richTextBoxAnswer.Clear();
			if ( cCurrItem.Mark != Mark.eUnknown )
			{
				buttonNext.Enabled = false;
			}
		}

		private void buttonClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}


	
	}
}
