using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace KBReader
{
	/// <summary>
	/// Summary description for SelectKBase.
	/// </summary>
	public class SelectKBase : System.Windows.Forms.Form
	{
        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.Label labelPath;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOk;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        private string path;
        public string Path 
        {
            get
            {
                return ( path );
            }
        }

		public SelectKBase()
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
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.labelPath = new System.Windows.Forms.Label();
            this.buttonBrowse = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxPath
            // 
            this.textBoxPath.Location = new System.Drawing.Point(8, 24);
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.Size = new System.Drawing.Size(232, 20);
            this.textBoxPath.TabIndex = 0;
            this.textBoxPath.Text = "";
            // 
            // labelPath
            // 
            this.labelPath.Location = new System.Drawing.Point(8, 8);
            this.labelPath.Name = "labelPath";
            this.labelPath.Size = new System.Drawing.Size(128, 16);
            this.labelPath.TabIndex = 1;
            this.labelPath.Text = "Path to knowledge base:";
            // 
            // buttonBrowse
            // 
            this.buttonBrowse.Location = new System.Drawing.Point(248, 24);
            this.buttonBrowse.Name = "buttonBrowse";
            this.buttonBrowse.TabIndex = 2;
            this.buttonBrowse.Text = "Browse";
            this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(248, 88);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.TabIndex = 4;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOk
            // 
            this.buttonOk.Location = new System.Drawing.Point(168, 88);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.TabIndex = 5;
            this.buttonOk.Text = "OK";
            this.buttonOk.Click += new System.EventHandler(this.buttonOk_Click);
            // 
            // SelectKBase
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(328, 114);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonBrowse);
            this.Controls.Add(this.labelPath);
            this.Controls.Add(this.textBoxPath);
            this.Name = "SelectKBase";
            this.Text = "SelectKBase";
            this.ResumeLayout(false);

        }
		#endregion

        private void buttonBrowse_Click(object sender, System.EventArgs e)
        {
            OpenFileDialog loader = new OpenFileDialog();
            loader.InitialDirectory = "c:\\" ;
            loader.Filter = "pro files (*.pro)|*.pro|All files (*.*)|*.*" ;
            loader.FilterIndex = 2;
            loader.RestoreDirectory = true ;
            if ( loader.ShowDialog() == DialogResult.OK )
            {
                this.textBoxPath.Text = loader.FileName;
            }
        }

        private void buttonOk_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.path = this.textBoxPath.Text;
            this.Close();
        }

        private void buttonCancel_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
	}
}
