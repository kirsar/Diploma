using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace KBCreator
{
	public class AddRelation : System.Windows.Forms.Form
	{
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ComboBox comboBoxArity;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.CheckBox checkBoxCommutative;
        private System.Windows.Forms.CheckBox checkBoxMutable;
        private System.Windows.Forms.CheckBox checkBoxPrimary;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelArity;
        private System.Windows.Forms.Label labelPair;
        private System.Windows.Forms.TextBox textBoxPair;
        private System.ComponentModel.Container components = null;
        
        public Relation Relation;

        public Relation PairRelation;
        private System.Windows.Forms.TextBox textBoxlogicalName;
        private System.Windows.Forms.Label labelLogicalName;
        private System.Windows.Forms.CheckBox checkBoxSyncWithName;

        private bool bSecondRun;
        
        public AddRelation()
		{
			InitializeComponent();
            InitializeCombos();
     	}

        public AddRelation( bool bSecondRun, Relation pairRelation )
        {
            InitializeComponent();
            InitializeCombos();

            this.bSecondRun = bSecondRun;
            this.PairRelation = pairRelation;
            InitializeSecond();
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.comboBoxArity = new System.Windows.Forms.ComboBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.checkBoxCommutative = new System.Windows.Forms.CheckBox();
            this.checkBoxMutable = new System.Windows.Forms.CheckBox();
            this.checkBoxPrimary = new System.Windows.Forms.CheckBox();
            this.labelName = new System.Windows.Forms.Label();
            this.labelArity = new System.Windows.Forms.Label();
            this.labelPair = new System.Windows.Forms.Label();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.textBoxPair = new System.Windows.Forms.TextBox();
            this.textBoxlogicalName = new System.Windows.Forms.TextBox();
            this.labelLogicalName = new System.Windows.Forms.Label();
            this.checkBoxSyncWithName = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // comboBoxArity
            // 
            this.comboBoxArity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxArity.Location = new System.Drawing.Point(8, 184);
            this.comboBoxArity.Name = "comboBoxArity";
            this.comboBoxArity.Size = new System.Drawing.Size(200, 21);
            this.comboBoxArity.TabIndex = 0;
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(8, 40);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(200, 20);
            this.textBoxName.TabIndex = 1;
            this.textBoxName.Text = "";
            this.textBoxName.TextChanged += new System.EventHandler(this.textBoxName_TextChanged);
            // 
            // checkBoxCommutative
            // 
            this.checkBoxCommutative.Location = new System.Drawing.Point(8, 216);
            this.checkBoxCommutative.Name = "checkBoxCommutative";
            this.checkBoxCommutative.TabIndex = 2;
            this.checkBoxCommutative.Text = "Commutative";
            // 
            // checkBoxMutable
            // 
            this.checkBoxMutable.Location = new System.Drawing.Point(8, 240);
            this.checkBoxMutable.Name = "checkBoxMutable";
            this.checkBoxMutable.TabIndex = 3;
            this.checkBoxMutable.Text = "Mutable";
            this.checkBoxMutable.CheckedChanged += new System.EventHandler(this.checkBoxMutable_CheckedChanged);
            // 
            // checkBoxPrimary
            // 
            this.checkBoxPrimary.Location = new System.Drawing.Point(8, 328);
            this.checkBoxPrimary.Name = "checkBoxPrimary";
            this.checkBoxPrimary.TabIndex = 5;
            this.checkBoxPrimary.Text = "Primary";
            this.checkBoxPrimary.CheckedChanged += new System.EventHandler(this.checkBoxPrimary_CheckedChanged);
            // 
            // labelName
            // 
            this.labelName.Location = new System.Drawing.Point(8, 16);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(40, 23);
            this.labelName.TabIndex = 6;
            this.labelName.Text = "Name:";
            // 
            // labelArity
            // 
            this.labelArity.Location = new System.Drawing.Point(8, 160);
            this.labelArity.Name = "labelArity";
            this.labelArity.Size = new System.Drawing.Size(32, 23);
            this.labelArity.TabIndex = 7;
            this.labelArity.Text = "Arity:";
            // 
            // labelPair
            // 
            this.labelPair.Location = new System.Drawing.Point(8, 272);
            this.labelPair.Name = "labelPair";
            this.labelPair.Size = new System.Drawing.Size(72, 23);
            this.labelPair.TabIndex = 8;
            this.labelPair.Text = "Pair Relation:";
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(8, 376);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.TabIndex = 9;
            this.buttonOK.Text = "OK";
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(96, 376);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.TabIndex = 10;
            this.buttonCancel.Text = "Cancel";
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // textBoxPair
            // 
            this.textBoxPair.Enabled = false;
            this.textBoxPair.Location = new System.Drawing.Point(8, 296);
            this.textBoxPair.Name = "textBoxPair";
            this.textBoxPair.Size = new System.Drawing.Size(200, 20);
            this.textBoxPair.TabIndex = 11;
            this.textBoxPair.Text = "";
            // 
            // textBoxlogicalName
            // 
            this.textBoxlogicalName.Location = new System.Drawing.Point(8, 128);
            this.textBoxlogicalName.Name = "textBoxlogicalName";
            this.textBoxlogicalName.Size = new System.Drawing.Size(200, 20);
            this.textBoxlogicalName.TabIndex = 12;
            this.textBoxlogicalName.Text = "";
            // 
            // labelLogicalName
            // 
            this.labelLogicalName.Location = new System.Drawing.Point(8, 96);
            this.labelLogicalName.Name = "labelLogicalName";
            this.labelLogicalName.Size = new System.Drawing.Size(96, 23);
            this.labelLogicalName.TabIndex = 13;
            this.labelLogicalName.Text = "Logical Name:";
            // 
            // checkBoxSyncWithName
            // 
            this.checkBoxSyncWithName.Checked = true;
            this.checkBoxSyncWithName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSyncWithName.Location = new System.Drawing.Point(8, 72);
            this.checkBoxSyncWithName.Name = "checkBoxSyncWithName";
            this.checkBoxSyncWithName.Size = new System.Drawing.Size(200, 24);
            this.checkBoxSyncWithName.TabIndex = 14;
            this.checkBoxSyncWithName.Text = "Synchronize With Name";
            // 
            // AddRelation
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(216, 406);
            this.Controls.Add(this.checkBoxSyncWithName);
            this.Controls.Add(this.labelLogicalName);
            this.Controls.Add(this.textBoxlogicalName);
            this.Controls.Add(this.textBoxPair);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.labelPair);
            this.Controls.Add(this.labelArity);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.checkBoxPrimary);
            this.Controls.Add(this.checkBoxMutable);
            this.Controls.Add(this.checkBoxCommutative);
            this.Controls.Add(this.comboBoxArity);
            this.MaximumSize = new System.Drawing.Size(224, 440);
            this.MinimumSize = new System.Drawing.Size(224, 440);
            this.Name = "AddRelation";
            this.Text = "Relation Properties";
            this.ResumeLayout(false);

        }
		#endregion

        private void InitializeCombos()
        {
            comboBoxArity.Items.Add( Relation.ArityToString( RelationArity.Unary ) );
            comboBoxArity.Items.Add( Relation.ArityToString( RelationArity.Binary ) );
            comboBoxArity.SelectedIndex = 0;
        }

        private void InitializeSecond()
        {
            int index = comboBoxArity.Items.IndexOf( Relation.ArityToString( this.PairRelation.Arity ) );
            comboBoxArity.SelectedIndex = index;
            comboBoxArity.Enabled = false;

            checkBoxMutable.CheckState = CheckState.Checked;
            checkBoxMutable.Enabled = false;
            
            textBoxPair.Text = this.PairRelation.Name;
        }

        private void SaveRelationSettings()
        {
            this.Relation =  new Relation( textBoxName.Text,
                textBoxlogicalName.Text,
                Relation.StringToArity( comboBoxArity.SelectedItem as string ), 
                checkBoxCommutative.Checked, 
                checkBoxMutable.Checked,
                this.PairRelation,
                checkBoxPrimary.Checked );
        }

        private void checkBoxMutable_CheckedChanged(object sender, System.EventArgs e)
        {
            labelPair.Enabled = checkBoxMutable.Checked;
            checkBoxPrimary.Enabled = !checkBoxMutable.Checked;
            if ( checkBoxMutable.Checked && !bSecondRun )
            {
               SaveRelationSettings();
               AddRelation cPairDialog = new AddRelation( true, this.Relation );
               if ( cPairDialog.ShowDialog() == DialogResult.OK )
               {
                   this.PairRelation = cPairDialog.Relation;
                   this.textBoxPair.Text = this.PairRelation.Name;
               }
               else
               {
                   checkBoxMutable.CheckState = CheckState.Unchecked;
               }

            }
        }

        private void checkBoxPrimary_CheckedChanged(object sender, System.EventArgs e)
        {
            checkBoxMutable.Enabled = !checkBoxPrimary.Checked;
        }
    
        private void buttonOK_Click(object sender, System.EventArgs e)
        {
            if ( textBoxName.Text.Length == 0 )
            {
                MessageBox.Show( "You've entered empty relation name", "Empty Name", MessageBoxButtons.OK );
                return;
            }
            if ( formMain.Manager.RelationsTypes.RelationNameExists( textBoxName.Text ) )
            {
                MessageBox.Show( "Relation with this name already exists", "Existing Name", MessageBoxButtons.OK );
                return;
            }
            
            SaveRelationSettings();

            this.DialogResult = DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void textBoxName_TextChanged(object sender, System.EventArgs e)
        {
            if ( this.checkBoxSyncWithName.Checked )
            {
                if ( !formMain.Manager.FilterName( this.textBoxName.Text ) )
                    this.textBoxlogicalName.Text = this.textBoxName.Text;
                else
                    this.textBoxlogicalName.Text = "";
            }
        }
	}
}
