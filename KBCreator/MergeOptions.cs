using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace KBCreator
{
	/// <summary>
	/// Summary description for MergeOptions.
	/// </summary>
	public class MergeOptions : System.Windows.Forms.Form
	{
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public MergeOptions()
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "More specific base:";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(147, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 23);
            this.label2.TabIndex = 1;
            this.label2.Text = "Base to merge with:";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(8, 152);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 23);
            this.label3.TabIndex = 2;
            this.label3.Text = "New object:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(72, 152);
            this.textBox1.Name = "textBox1";
            this.textBox1.TabIndex = 3;
            this.textBox1.Text = "cos_alpha";
            // 
            // listBox1
            // 
            this.listBox1.Items.AddRange(new object[] {
                                                          "sin",
                                                          "cos",
                                                          "tg",
                                                          "ctg",
                                                          "sin2",
                                                          "cos2",
                                                          "tg2",
                                                          "ctg2",
                                                          "arcsin",
                                                          "arccos",
                                                          "arctg",
                                                          "arcctg"});
            this.listBox1.Location = new System.Drawing.Point(8, 32);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(112, 95);
            this.listBox1.TabIndex = 4;
            // 
            // listBox2
            // 
            this.listBox2.Items.AddRange(new object[] {
                                                          "A",
                                                          "B",
                                                          "C",
                                                          "alpha",
                                                          "betta",
                                                          "gamma"});
            this.listBox2.Location = new System.Drawing.Point(147, 32);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(112, 95);
            this.listBox2.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(184, 152);
            this.button1.Name = "button1";
            this.button1.TabIndex = 6;
            this.button1.Text = "Add";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(8, 200);
            this.button2.Name = "button2";
            this.button2.TabIndex = 7;
            this.button2.Text = "OK";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(184, 200);
            this.button3.Name = "button3";
            this.button3.TabIndex = 8;
            this.button3.Text = "Cancel";
            // 
            // MergeOptions
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(267, 230);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "MergeOptions";
            this.Text = "Merge Options";
            this.ResumeLayout(false);

        }
		#endregion
	}
}
