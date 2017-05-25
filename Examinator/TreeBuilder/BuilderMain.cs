using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Xml.Serialization;
using System.IO;
using System.Diagnostics;

namespace TreeBuilder
{
	public class BuilderMain : System.Windows.Forms.Form
	{
		private ExamTree m_cTree;
		private int m_iNumber;
		private int m_iExChild;
		private int m_iGoodChild;
		private int m_iSatChild;
		private int m_iUnsatChild;
		private string m_strQuestion;
		private string m_strInput;
		private string m_strOutput;
		private bool m_bMultiExpr;
		private string m_strSavePath;
		private string m_strLoadPath;
		private Mark m_eMark;
		
		private System.Windows.Forms.TextBox NumberTextBox;
		private System.Windows.Forms.TextBox ExChildTextBox;
		private System.Windows.Forms.TextBox GoodChildTextBox;
		private System.Windows.Forms.TextBox SatChildTextBox;
		private System.Windows.Forms.TextBox UnSatChildTextBox;
		private System.Windows.Forms.Label NumberLabel;
		private System.Windows.Forms.Label GoodChildLabel;
		private System.Windows.Forms.Label ExChildLabel;
		private System.Windows.Forms.Label SatChildLabel;
		private System.Windows.Forms.Label UnSatChildLabel;
		private System.Windows.Forms.Button AddButton;
		private System.Windows.Forms.Button CloseButton;
		private System.Windows.Forms.Button BrowseButton;
		private System.Windows.Forms.Label QuestionLabel;
		private System.Windows.Forms.CheckBox checkBoxMulti;
		private System.Windows.Forms.Label labelInput;
		private System.Windows.Forms.TextBox textBoxInput;
		private System.Windows.Forms.Label labelOutput;
		private System.Windows.Forms.TextBox textBoxOutput;
		private System.Windows.Forms.Button buttonHelp;
		private System.Windows.Forms.RichTextBox QuestionTextBox;
		private System.Windows.Forms.CheckBox checkBoxMark;
		private System.Windows.Forms.Label labelMark;
		private System.Windows.Forms.ComboBox comboBoxMark;
		private System.Windows.Forms.Button buttonRemove;
		private System.Windows.Forms.TextBox SaveTextBox;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox LoadTextBox;
		private System.Windows.Forms.Button buttonReplace;
		private System.Windows.Forms.Button buttonSave;
	
		private System.ComponentModel.Container components = null;

		public BuilderMain()
		{
			InitializeComponent();
			m_cTree = new ExamTree();
			m_eMark = new Mark();
			m_strSavePath = "tree.xml";
			m_strLoadPath = "tree.xml";
			SaveTextBox.Text = m_strSavePath;
			LoadTextBox.Text = m_strLoadPath;
		}

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
		private void InitializeComponent()
		{
			this.NumberTextBox = new System.Windows.Forms.TextBox();
			this.ExChildTextBox = new System.Windows.Forms.TextBox();
			this.GoodChildTextBox = new System.Windows.Forms.TextBox();
			this.SatChildTextBox = new System.Windows.Forms.TextBox();
			this.UnSatChildTextBox = new System.Windows.Forms.TextBox();
			this.NumberLabel = new System.Windows.Forms.Label();
			this.GoodChildLabel = new System.Windows.Forms.Label();
			this.ExChildLabel = new System.Windows.Forms.Label();
			this.SatChildLabel = new System.Windows.Forms.Label();
			this.UnSatChildLabel = new System.Windows.Forms.Label();
			this.AddButton = new System.Windows.Forms.Button();
			this.CloseButton = new System.Windows.Forms.Button();
			this.SaveTextBox = new System.Windows.Forms.TextBox();
			this.BrowseButton = new System.Windows.Forms.Button();
			this.QuestionLabel = new System.Windows.Forms.Label();
			this.checkBoxMulti = new System.Windows.Forms.CheckBox();
			this.labelInput = new System.Windows.Forms.Label();
			this.textBoxInput = new System.Windows.Forms.TextBox();
			this.labelOutput = new System.Windows.Forms.Label();
			this.textBoxOutput = new System.Windows.Forms.TextBox();
			this.buttonHelp = new System.Windows.Forms.Button();
			this.QuestionTextBox = new System.Windows.Forms.RichTextBox();
			this.checkBoxMark = new System.Windows.Forms.CheckBox();
			this.labelMark = new System.Windows.Forms.Label();
			this.comboBoxMark = new System.Windows.Forms.ComboBox();
			this.buttonRemove = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.LoadTextBox = new System.Windows.Forms.TextBox();
			this.buttonReplace = new System.Windows.Forms.Button();
			this.buttonSave = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// NumberTextBox
			// 
			this.NumberTextBox.Location = new System.Drawing.Point(120, 24);
			this.NumberTextBox.Name = "NumberTextBox";
			this.NumberTextBox.TabIndex = 0;
			this.NumberTextBox.Text = "";
			// 
			// ExChildTextBox
			// 
			this.ExChildTextBox.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.ExChildTextBox.Location = new System.Drawing.Point(400, 24);
			this.ExChildTextBox.Name = "ExChildTextBox";
			this.ExChildTextBox.Size = new System.Drawing.Size(40, 20);
			this.ExChildTextBox.TabIndex = 1;
			this.ExChildTextBox.Text = "";
			// 
			// GoodChildTextBox
			// 
			this.GoodChildTextBox.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.GoodChildTextBox.Location = new System.Drawing.Point(400, 56);
			this.GoodChildTextBox.Name = "GoodChildTextBox";
			this.GoodChildTextBox.Size = new System.Drawing.Size(40, 20);
			this.GoodChildTextBox.TabIndex = 2;
			this.GoodChildTextBox.Text = "";
			// 
			// SatChildTextBox
			// 
			this.SatChildTextBox.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.SatChildTextBox.Location = new System.Drawing.Point(400, 88);
			this.SatChildTextBox.Name = "SatChildTextBox";
			this.SatChildTextBox.Size = new System.Drawing.Size(40, 20);
			this.SatChildTextBox.TabIndex = 3;
			this.SatChildTextBox.Text = "";
			// 
			// UnSatChildTextBox
			// 
			this.UnSatChildTextBox.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.UnSatChildTextBox.Location = new System.Drawing.Point(400, 120);
			this.UnSatChildTextBox.Name = "UnSatChildTextBox";
			this.UnSatChildTextBox.Size = new System.Drawing.Size(40, 20);
			this.UnSatChildTextBox.TabIndex = 4;
			this.UnSatChildTextBox.Text = "";
			// 
			// NumberLabel
			// 
			this.NumberLabel.Location = new System.Drawing.Point(24, 24);
			this.NumberLabel.Name = "NumberLabel";
			this.NumberLabel.Size = new System.Drawing.Size(88, 23);
			this.NumberLabel.TabIndex = 5;
			this.NumberLabel.Text = "Node Number";
			// 
			// GoodChildLabel
			// 
			this.GoodChildLabel.Location = new System.Drawing.Point(232, 56);
			this.GoodChildLabel.Name = "GoodChildLabel";
			this.GoodChildLabel.Size = new System.Drawing.Size(126, 23);
			this.GoodChildLabel.TabIndex = 6;
			this.GoodChildLabel.Text = "Number of \"good\" Child";
			// 
			// ExChildLabel
			// 
			this.ExChildLabel.Location = new System.Drawing.Point(232, 24);
			this.ExChildLabel.Name = "ExChildLabel";
			this.ExChildLabel.Size = new System.Drawing.Size(150, 23);
			this.ExChildLabel.TabIndex = 7;
			this.ExChildLabel.Text = "Number of \"exellent\" Child";
			// 
			// SatChildLabel
			// 
			this.SatChildLabel.Location = new System.Drawing.Point(232, 88);
			this.SatChildLabel.Name = "SatChildLabel";
			this.SatChildLabel.Size = new System.Drawing.Size(160, 23);
			this.SatChildLabel.TabIndex = 8;
			this.SatChildLabel.Text = "Number of \"satisfactory\" Child";
			// 
			// UnSatChildLabel
			// 
			this.UnSatChildLabel.Location = new System.Drawing.Point(232, 120);
			this.UnSatChildLabel.Name = "UnSatChildLabel";
			this.UnSatChildLabel.Size = new System.Drawing.Size(168, 23);
			this.UnSatChildLabel.TabIndex = 9;
			this.UnSatChildLabel.Text = "Number of \"unsatisfactory\" Child";
			// 
			// AddButton
			// 
			this.AddButton.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			this.AddButton.Location = new System.Drawing.Point(24, 400);
			this.AddButton.Name = "AddButton";
			this.AddButton.TabIndex = 10;
			this.AddButton.Text = "Add";
			this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
			// 
			// CloseButton
			// 
			this.CloseButton.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.CloseButton.Location = new System.Drawing.Point(278, 400);
			this.CloseButton.Name = "CloseButton";
			this.CloseButton.TabIndex = 12;
			this.CloseButton.Text = "Close";
			this.CloseButton.Click += new System.EventHandler(this.CloseButton_Click);
			// 
			// SaveTextBox
			// 
			this.SaveTextBox.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.SaveTextBox.Location = new System.Drawing.Point(112, 304);
			this.SaveTextBox.Name = "SaveTextBox";
			this.SaveTextBox.Size = new System.Drawing.Size(104, 20);
			this.SaveTextBox.TabIndex = 14;
			this.SaveTextBox.Text = "";
			// 
			// BrowseButton
			// 
			this.BrowseButton.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			this.BrowseButton.Location = new System.Drawing.Point(24, 304);
			this.BrowseButton.Name = "BrowseButton";
			this.BrowseButton.TabIndex = 15;
			this.BrowseButton.Text = "Save As..";
			this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
			// 
			// QuestionLabel
			// 
			this.QuestionLabel.Location = new System.Drawing.Point(232, 152);
			this.QuestionLabel.Name = "QuestionLabel";
			this.QuestionLabel.Size = new System.Drawing.Size(88, 23);
			this.QuestionLabel.TabIndex = 16;
			this.QuestionLabel.Text = "Question";
			// 
			// checkBoxMulti
			// 
			this.checkBoxMulti.Location = new System.Drawing.Point(8, 176);
			this.checkBoxMulti.Name = "checkBoxMulti";
			this.checkBoxMulti.Size = new System.Drawing.Size(112, 24);
			this.checkBoxMulti.TabIndex = 17;
			this.checkBoxMulti.Text = "Multi Expression";
			// 
			// labelInput
			// 
			this.labelInput.Location = new System.Drawing.Point(24, 56);
			this.labelInput.Name = "labelInput";
			this.labelInput.Size = new System.Drawing.Size(96, 23);
			this.labelInput.TabIndex = 18;
			this.labelInput.Text = "Input Parameters";
			// 
			// textBoxInput
			// 
			this.textBoxInput.Location = new System.Drawing.Point(120, 56);
			this.textBoxInput.Name = "textBoxInput";
			this.textBoxInput.TabIndex = 19;
			this.textBoxInput.Text = "";
			// 
			// labelOutput
			// 
			this.labelOutput.Location = new System.Drawing.Point(24, 88);
			this.labelOutput.Name = "labelOutput";
			this.labelOutput.TabIndex = 20;
			this.labelOutput.Text = "Output Parameter";
			// 
			// textBoxOutput
			// 
			this.textBoxOutput.Location = new System.Drawing.Point(120, 88);
			this.textBoxOutput.Name = "textBoxOutput";
			this.textBoxOutput.TabIndex = 21;
			this.textBoxOutput.Text = "";
			// 
			// buttonHelp
			// 
			this.buttonHelp.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.buttonHelp.Location = new System.Drawing.Point(366, 400);
			this.buttonHelp.Name = "buttonHelp";
			this.buttonHelp.TabIndex = 22;
			this.buttonHelp.Text = "Help";
			// 
			// QuestionTextBox
			// 
			this.QuestionTextBox.Location = new System.Drawing.Point(232, 176);
			this.QuestionTextBox.Name = "QuestionTextBox";
			this.QuestionTextBox.Size = new System.Drawing.Size(208, 184);
			this.QuestionTextBox.TabIndex = 23;
			this.QuestionTextBox.Text = "";
			// 
			// checkBoxMark
			// 
			this.checkBoxMark.Location = new System.Drawing.Point(8, 208);
			this.checkBoxMark.Name = "checkBoxMark";
			this.checkBoxMark.TabIndex = 24;
			this.checkBoxMark.Text = "Node Is Final";
			this.checkBoxMark.CheckedChanged += new System.EventHandler(this.checkBoxMark_CheckedChanged);
			// 
			// labelMark
			// 
			this.labelMark.Enabled = false;
			this.labelMark.Location = new System.Drawing.Point(24, 240);
			this.labelMark.Name = "labelMark";
			this.labelMark.Size = new System.Drawing.Size(40, 23);
			this.labelMark.TabIndex = 25;
			this.labelMark.Text = "Mark:";
			// 
			// comboBoxMark
			// 
			this.comboBoxMark.Enabled = false;
			this.comboBoxMark.Items.AddRange(new object[] {
															  "5",
															  "4",
															  "3",
															  "2"});
			this.comboBoxMark.Location = new System.Drawing.Point(72, 240);
			this.comboBoxMark.Name = "comboBoxMark";
			this.comboBoxMark.Size = new System.Drawing.Size(148, 21);
			this.comboBoxMark.TabIndex = 26;
			this.comboBoxMark.SelectedIndexChanged += new System.EventHandler(this.comboBoxMark_SelectedIndexChanged);
			// 
			// buttonRemove
			// 
			this.buttonRemove.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			this.buttonRemove.Location = new System.Drawing.Point(112, 400);
			this.buttonRemove.Name = "buttonRemove";
			this.buttonRemove.TabIndex = 28;
			this.buttonRemove.Text = "Remove";
			this.buttonRemove.Click += new System.EventHandler(this.buttonRemove_Click);
			// 
			// button1
			// 
			this.button1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			this.button1.Location = new System.Drawing.Point(24, 336);
			this.button1.Name = "button1";
			this.button1.TabIndex = 29;
			this.button1.Text = "Load From...";
			this.button1.Click += new System.EventHandler(this.LoadFrom_Click);
			// 
			// LoadTextBox
			// 
			this.LoadTextBox.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.LoadTextBox.Location = new System.Drawing.Point(112, 336);
			this.LoadTextBox.Name = "LoadTextBox";
			this.LoadTextBox.Size = new System.Drawing.Size(104, 20);
			this.LoadTextBox.TabIndex = 30;
			this.LoadTextBox.Text = "";
			// 
			// buttonReplace
			// 
			this.buttonReplace.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			this.buttonReplace.Location = new System.Drawing.Point(196, 400);
			this.buttonReplace.Name = "buttonReplace";
			this.buttonReplace.TabIndex = 31;
			this.buttonReplace.Text = "Replace";
			this.buttonReplace.Click += new System.EventHandler(this.buttonReplace_Click);
			// 
			// buttonSave
			// 
			this.buttonSave.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			this.buttonSave.Location = new System.Drawing.Point(24, 368);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.TabIndex = 32;
			this.buttonSave.Text = "Save Tree";
			this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
			// 
			// BuilderMain
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(464, 430);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.buttonSave,
																		  this.buttonReplace,
																		  this.LoadTextBox,
																		  this.button1,
																		  this.buttonRemove,
																		  this.comboBoxMark,
																		  this.labelMark,
																		  this.checkBoxMark,
																		  this.QuestionTextBox,
																		  this.buttonHelp,
																		  this.textBoxOutput,
																		  this.labelOutput,
																		  this.textBoxInput,
																		  this.labelInput,
																		  this.checkBoxMulti,
																		  this.QuestionLabel,
																		  this.BrowseButton,
																		  this.SaveTextBox,
																		  this.CloseButton,
																		  this.AddButton,
																		  this.UnSatChildLabel,
																		  this.SatChildLabel,
																		  this.ExChildLabel,
																		  this.GoodChildLabel,
																		  this.NumberLabel,
																		  this.UnSatChildTextBox,
																		  this.SatChildTextBox,
																		  this.GoodChildTextBox,
																		  this.ExChildTextBox,
																		  this.NumberTextBox});
			this.MinimumSize = new System.Drawing.Size(328, 424);
			this.Name = "BuilderMain";
			this.Text = "Tree Builder";
			this.ResumeLayout(false);

		}
		#endregion

		[STAThread]
		static void Main() 
		{
			Application.Run(new BuilderMain());
		}

		private void CloseButton_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void AddButton_Click(object sender, System.EventArgs e)
		{
			try
			{
				if ( checkBoxMark.Checked )
				{
					m_eMark = (Mark)Convert.ToInt32( comboBoxMark.SelectedItem );
					m_iNumber = Convert.ToInt32( NumberTextBox.Text );
					m_iExChild    = 0;
					m_iGoodChild  = 0;
					m_iSatChild   = 0;
					m_iUnsatChild = 0;
					m_strQuestion = QuestionTextBox.Text;
					m_strInput    = "";
					m_strOutput   = "";
					m_bMultiExpr = false;
				}
				else
				{
					m_iNumber     = Convert.ToInt32( NumberTextBox.Text );
					m_iExChild    = Convert.ToInt32( ExChildTextBox.Text );
					m_iGoodChild  = Convert.ToInt32( GoodChildTextBox.Text );
					m_iSatChild   = Convert.ToInt32( SatChildTextBox.Text );
					m_iUnsatChild = Convert.ToInt32( UnSatChildTextBox.Text );
					m_strQuestion = QuestionTextBox.Text;
					m_strInput    = textBoxInput.Text;
					m_strOutput   = textBoxOutput.Text;
					m_bMultiExpr = checkBoxMulti.Checked;
					m_eMark = Mark.eUnknown;
				}
	
				NumberTextBox.Clear();
				checkBoxMulti.Checked = false; 
			}
			catch ( Exception ex )
			{
				Debug.Assert( false, ex.Message );
			}
			finally
			{
				TreeItem cItem = new TreeItem();
				cItem.Number       = m_iNumber;
				cItem.ExChild      = m_iExChild;
				cItem.GoodChild    = m_iGoodChild;
				cItem.SatChild     = m_iSatChild;
				cItem.UnsatChild   = m_iUnsatChild;
				cItem.QuestionItem = m_strQuestion;
				cItem.Input        = m_strInput;
				cItem.Output       = m_strOutput;
				cItem.MultiExpr    = m_bMultiExpr;
				cItem.Mark         = m_eMark;
				m_cTree.m_cTreeItems.Add( cItem ) ;
			}
		}


		private void BrowseButton_Click(object sender, System.EventArgs e)
		{
			try
			{
				string strCurrPath = "";
				Stream sfStream ;
				SaveFileDialog sfDialog = new SaveFileDialog();
				sfDialog.Filter = "xml files (*.xml)|*.xml";
				sfDialog.FilterIndex = 2 ;
				sfDialog.RestoreDirectory = true ;
				if( sfDialog.ShowDialog() == DialogResult.OK )
				{
					if((sfStream = sfDialog.OpenFile()) != null)
					{
						strCurrPath = Path.GetFileName( sfDialog.FileName );
					}
					if ( strCurrPath != "" )
					{
						m_cTree.m_cTreeItems.CollectionName = "Tree Items";
						XmlSerializer Serializer = new XmlSerializer( typeof (TreeItems) );
						Serializer.Serialize( sfStream, m_cTree.m_cTreeItems );
						m_strSavePath = sfDialog.FileName;
						SaveTextBox.Text = m_strSavePath;
						sfStream.Close();
					}
				}
			}
			catch (Exception ex)
			{
				Debug.Assert( false, ex.Message );
			}
		}

		private void checkBoxMark_CheckedChanged(object sender, System.EventArgs e)
		{
			if ( checkBoxMark.Checked )
			{
				ExChildTextBox.Enabled     = false;
				ExChildLabel.Enabled       = false;
				GoodChildTextBox.Enabled   = false;
				GoodChildLabel.Enabled     = false;
				SatChildTextBox.Enabled    = false;
				SatChildLabel.Enabled      = false;
				UnSatChildTextBox.Enabled  = false;
				UnSatChildLabel.Enabled    = false;
				textBoxInput.Enabled       = false;
				labelInput.Enabled         = false;
				textBoxOutput.Enabled      = false;
				labelOutput.Enabled        = false;
				checkBoxMulti.Enabled      = false;
				QuestionLabel.Enabled      = false;
				QuestionTextBox.Enabled    = false;
				QuestionTextBox.Text = " Ваша оценка - " + comboBoxMark.SelectedItem; 
				labelMark.Enabled    = true;
				comboBoxMark.Enabled = true;
			}
			else
			{
				ExChildTextBox.Enabled     = true;
				ExChildLabel.Enabled       = true;
				GoodChildTextBox.Enabled   = true;
				GoodChildLabel.Enabled     = true;
				SatChildTextBox.Enabled    = true;
				SatChildLabel.Enabled      = true;
				UnSatChildTextBox.Enabled  = true;
				UnSatChildLabel.Enabled    = true;
				textBoxInput.Enabled       = true;
				labelInput.Enabled         = true;
				textBoxOutput.Enabled      = true;
				labelOutput.Enabled        = true;
				checkBoxMulti.Enabled      = true;
				QuestionLabel.Enabled      = true;
				QuestionTextBox.Enabled    = true;
				QuestionTextBox.Text = ""; 
				labelMark.Enabled    = false;
				comboBoxMark.Enabled = false;
			}

		}

		private void comboBoxMark_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			QuestionTextBox.Text = " Ваша оценка - " + comboBoxMark.SelectedItem; 
		}

		private void LoadFrom_Click(object sender, System.EventArgs e)
		{
			try
			{
				string strCurrPath = "";
				Stream ofStream ;
				OpenFileDialog ofDialog = new OpenFileDialog();
				ofDialog.Filter = "xml files (*.xml)|*.xml";
				ofDialog.FilterIndex = 2 ;
				ofDialog.RestoreDirectory = true ;
				if( ofDialog.ShowDialog() == DialogResult.OK )
				{
					if((ofStream = ofDialog.OpenFile()) != null)
					{
						strCurrPath = Path.GetFileName( ofDialog.FileName );
					}
					if ( strCurrPath != "" )
					{
						m_strLoadPath = strCurrPath;
						LoadTextBox.Text = m_strLoadPath;
						XmlSerializer Serializer = new XmlSerializer( typeof (TreeItems) );
						m_cTree.m_cTreeItems = (TreeItems)Serializer.Deserialize( ofStream );
						ofStream.Close();
					}
				}
			}
			catch (Exception ex)
			{
				Debug.Assert( false, ex.Message );
			}
		}

		private void buttonLoad_Click(object sender, System.EventArgs e)
		{
			try
			{
				m_strLoadPath = LoadTextBox.Text;
				XmlSerializer Serializer = new XmlSerializer( typeof (TreeItems) );
				TextReader Reader = new StreamReader( m_strLoadPath );
				m_cTree.m_cTreeItems = (TreeItems)Serializer.Deserialize( Reader );
				Reader.Close();
			}
			catch ( Exception ex )
			{
				Debug.Assert( false, ex.Message );
			}
			
		}

		private void buttonRemove_Click(object sender, System.EventArgs e)
		{
			try
			{
				int iCurrNumber = Convert.ToInt32(NumberTextBox.Text);
				int iCurrIndex = m_cTree.GetIndexByNumber( iCurrNumber );
				m_cTree.m_cTreeItems.RemoveAt( iCurrIndex );
			}
			catch ( Exception ex )
			{
				Debug.Assert( false, ex.Message );
				MessageBox.Show( "Programm can't delete this item. Perhaps, it was deleted earlier" );
			}
		}

		private void buttonSave_Click(object sender, System.EventArgs e)
		{
			try
			{
				if ( m_strSavePath ==  "" )
				{	
					BrowseButton_Click( sender, e );
					return;
				}
				m_cTree.m_cTreeItems.CollectionName = "Tree Items";
				XmlSerializer Serializer = new XmlSerializer( typeof (TreeItems) );
				TextWriter Writer = new StreamWriter( m_strSavePath );
				Serializer.Serialize( Writer, m_cTree.m_cTreeItems );
				Writer.Close();
			}
			catch ( Exception ex )
			{
				Debug.Assert( false, ex.Message );
			}
		}

		private void buttonReplace_Click(object sender, System.EventArgs e)
		{
			buttonRemove_Click( sender, e );
			AddButton_Click   ( sender, e );
		}
	}
}
