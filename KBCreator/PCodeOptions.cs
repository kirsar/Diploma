using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace KBCreator
{
	/// <summary>
	/// Summary description for PCodeOptions.
	/// </summary>
	public class PCodeOptions : System.Windows.Forms.Form
	{
        private System.Windows.Forms.TextBox textBoxFileName;
        private System.Windows.Forms.Label labelPath;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.RadioButton radioButtonNew;
        private System.Windows.Forms.RadioButton radioButtonExpand;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.RadioButton radioButtonApply;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        public KBManager.PGenerationOption PCodeOption = KBManager.PGenerationOption.eCreateNew;
        public string PCodePath = "";

		public PCodeOptions()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.textBoxFileName = new System.Windows.Forms.TextBox();
            this.labelPath = new System.Windows.Forms.Label();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.radioButtonNew = new System.Windows.Forms.RadioButton();
            this.radioButtonExpand = new System.Windows.Forms.RadioButton();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.radioButtonApply = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // textBoxFileName
            // 
            this.textBoxFileName.Location = new System.Drawing.Point(72, 16);
            this.textBoxFileName.Name = "textBoxFileName";
            this.textBoxFileName.Size = new System.Drawing.Size(176, 20);
            this.textBoxFileName.TabIndex = 0;
            this.textBoxFileName.Text = "";
            // 
            // labelPath
            // 
            this.labelPath.Location = new System.Drawing.Point(8, 16);
            this.labelPath.Name = "labelPath";
            this.labelPath.Size = new System.Drawing.Size(64, 23);
            this.labelPath.TabIndex = 1;
            this.labelPath.Text = "Base path:";
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Location = new System.Drawing.Point(256, 16);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.TabIndex = 2;
            this.buttonBrowse.Text = "Browse";
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // radioButtonNew
            // 
            this.radioButtonNew.Location = new System.Drawing.Point(16, 48);
            this.radioButtonNew.Name = "radioButtonNew";
            this.radioButtonNew.Size = new System.Drawing.Size(112, 24);
            this.radioButtonNew.TabIndex = 3;
            this.radioButtonNew.Text = "Create New Base";
            this.radioButtonNew.CheckedChanged += new System.EventHandler(this.radioButtonNew_CheckedChanged);
            // 
            // radioButtonExpand
            // 
            this.radioButtonExpand.Location = new System.Drawing.Point(16, 72);
            this.radioButtonExpand.Name = "radioButtonExpand";
            this.radioButtonExpand.Size = new System.Drawing.Size(168, 24);
            this.radioButtonExpand.TabIndex = 4;
            this.radioButtonExpand.Text = "Add Facts To Existing Base";
            this.radioButtonExpand.CheckedChanged += new System.EventHandler(this.radioButtonExpand_CheckedChanged);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(8, 128);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.TabIndex = 5;
            this.buttonOK.Text = "OK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(88, 128);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.TabIndex = 6;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // radioButtonApply
            // 
            this.radioButtonApply.Location = new System.Drawing.Point(16, 96);
            this.radioButtonApply.Name = "radioButtonApply";
            this.radioButtonApply.Size = new System.Drawing.Size(184, 24);
            this.radioButtonApply.TabIndex = 7;
            this.radioButtonApply.Text = "Merge Base With Applying facts";
            this.radioButtonApply.CheckedChanged += new System.EventHandler(this.radioButtonApply_CheckedChanged);
            // 
            // PCodeOptions
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(344, 158);
            this.Controls.Add(this.radioButtonApply);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.radioButtonExpand);
            this.Controls.Add(this.radioButtonNew);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.labelPath);
            this.Controls.Add(this.textBoxFileName);
            this.Name = "PCodeOptions";
            this.Text = "PCodeOptions";
            this.Load += new System.EventHandler(this.PCodeOptions_Load);
            this.ResumeLayout(false);

        }
		#endregion

        private void radioButtonNew_CheckedChanged(object sender, System.EventArgs e)
        {
            bool bEnable =  radioButtonNew.Checked;
            if ( bEnable )
            {
                labelPath.Enabled = bEnable;
                textBoxFileName.Enabled = bEnable;
                this.PCodeOption = KBManager.PGenerationOption.eCreateNew;
            }
        }

        private void radioButtonExpand_CheckedChanged(object sender, System.EventArgs e)
        {
            bool bEnable =  radioButtonNew.Checked;
            if ( !bEnable )
            {
                labelPath.Enabled = bEnable;
                textBoxFileName.Enabled = bEnable;
                this.PCodeOption = KBManager.PGenerationOption.eAddFacts;
            }
        }

        private void radioButtonApply_CheckedChanged(object sender, System.EventArgs e)
        {
            bool bEnable =  radioButtonNew.Checked;
            if ( !bEnable )
            {
                labelPath.Enabled = bEnable;
                textBoxFileName.Enabled = bEnable;
                this.PCodeOption = KBManager.PGenerationOption.eApplyFactts;
            }
        }

        private void buttonBrowse_Click(object sender, System.EventArgs e)
        {
            if ( this.PCodeOption == KBManager.PGenerationOption.eCreateNew )
            {
                SaveFileDialog saver = new SaveFileDialog();
                saver.InitialDirectory = "c:\\" ;
                saver.Filter = "prolog files (*.pro)|*.pro|All files (*.*)|*.*" ;
                saver.FilterIndex = 2 ;
                saver.RestoreDirectory = true ;
                if ( saver.ShowDialog() == DialogResult.OK )
                {
                    PCodePath = saver.FileName;
                    textBoxFileName.Text = PCodePath;
                }
            }
            else if ( this.PCodeOption == KBManager.PGenerationOption.eApplyFactts || this.PCodeOption == KBManager.PGenerationOption.eAddFacts )
            {
                OpenFileDialog expander = new OpenFileDialog();
                expander.InitialDirectory = "c:\\" ;
                expander.Filter = "prolog files (*.pro)|*.pro|All files (*.*)|*.*" ;
                expander.FilterIndex = 2 ;
                expander.RestoreDirectory = true ;
                if ( expander.ShowDialog() == DialogResult.OK )
                {
                    PCodePath = expander.FileName;
                    textBoxFileName.Text = PCodePath;
                }
            }
        }

        private void buttonOK_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            MergeOptions dlg = new MergeOptions();
            dlg.ShowDialog();
            this.Close();
        }

        private void PCodeOptions_Load(object sender, System.EventArgs e)
        {
            radioButtonNew.Checked = true;
        }
	}
}
