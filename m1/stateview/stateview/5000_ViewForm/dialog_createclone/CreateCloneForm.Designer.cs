namespace stateview._5000_ViewForm.dialog_createclone
{
    partial class CreateCloneForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateCloneForm));
            this.textBox_this = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_new = new System.Windows.Forms.TextBox();
            this.textBox_docdir = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_srcdir = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_notice = new System.Windows.Forms.TextBox();
            this.button_create = new System.Windows.Forms.Button();
            this.button_docdir = new System.Windows.Forms.Button();
            this.button_srcdir = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label_help_5 = new System.Windows.Forms.Label();
            this.label_copyabove = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox_this
            // 
            this.textBox_this.Location = new System.Drawing.Point(162, 12);
            this.textBox_this.Name = "textBox_this";
            this.textBox_this.ReadOnly = true;
            this.textBox_this.Size = new System.Drawing.Size(281, 19);
            this.textBox_this.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(133, 12);
            this.label1.TabIndex = 1;
            this.label1.Tag = "ccf_statemashinename";
            this.label1.Text = "This Statemachine Name";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 12);
            this.label2.TabIndex = 2;
            this.label2.Tag = "ccf_newstatename";
            this.label2.Text = "New Statemachine Name";
            // 
            // textBox_new
            // 
            this.textBox_new.Location = new System.Drawing.Point(162, 51);
            this.textBox_new.Name = "textBox_new";
            this.textBox_new.Size = new System.Drawing.Size(281, 19);
            this.textBox_new.TabIndex = 3;
            // 
            // textBox_docdir
            // 
            this.textBox_docdir.Location = new System.Drawing.Point(12, 99);
            this.textBox_docdir.Name = "textBox_docdir";
            this.textBox_docdir.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textBox_docdir.Size = new System.Drawing.Size(431, 19);
            this.textBox_docdir.TabIndex = 5;
            this.textBox_docdir.WordWrap = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 12);
            this.label3.TabIndex = 4;
            this.label3.Tag = "ccf_docfolder";
            this.label3.Text = "Document folder";
            // 
            // textBox_srcdir
            // 
            this.textBox_srcdir.Location = new System.Drawing.Point(14, 150);
            this.textBox_srcdir.Name = "textBox_srcdir";
            this.textBox_srcdir.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textBox_srcdir.Size = new System.Drawing.Size(429, 19);
            this.textBox_srcdir.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 131);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(128, 12);
            this.label4.TabIndex = 6;
            this.label4.Tag = "ccf_genfolder";
            this.label4.Text = "Generated source folder";
            // 
            // textBox_notice
            // 
            this.textBox_notice.BackColor = System.Drawing.SystemColors.Control;
            this.textBox_notice.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox_notice.Location = new System.Drawing.Point(14, 194);
            this.textBox_notice.Multiline = true;
            this.textBox_notice.Name = "textBox_notice";
            this.textBox_notice.Size = new System.Drawing.Size(429, 78);
            this.textBox_notice.TabIndex = 8;
            this.textBox_notice.Tag = "ccf_desc";
            this.textBox_notice.Text = "1. Manager file will not be copied.\r\n2. If statemachine name is used at unexpecte" +
    "d places in documents and  sources, the result will not be able to be used as it" +
    "s clone. \r\n";
            // 
            // button_create
            // 
            this.button_create.Location = new System.Drawing.Point(14, 289);
            this.button_create.Name = "button_create";
            this.button_create.Size = new System.Drawing.Size(429, 53);
            this.button_create.TabIndex = 9;
            this.button_create.Text = "Create Clone";
            this.button_create.UseVisualStyleBackColor = true;
            this.button_create.Click += new System.EventHandler(this.button_create_Click);
            // 
            // button_docdir
            // 
            this.button_docdir.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.button_docdir.Font = new System.Drawing.Font("MS UI Gothic", 5F);
            this.button_docdir.Location = new System.Drawing.Point(422, 84);
            this.button_docdir.Name = "button_docdir";
            this.button_docdir.Size = new System.Drawing.Size(21, 12);
            this.button_docdir.TabIndex = 10;
            this.button_docdir.Text = "...";
            this.button_docdir.UseVisualStyleBackColor = false;
            this.button_docdir.Click += new System.EventHandler(this.button_docdir_Click);
            // 
            // button_srcdir
            // 
            this.button_srcdir.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.button_srcdir.Font = new System.Drawing.Font("MS UI Gothic", 5F);
            this.button_srcdir.Location = new System.Drawing.Point(422, 134);
            this.button_srcdir.Name = "button_srcdir";
            this.button_srcdir.Size = new System.Drawing.Size(21, 12);
            this.button_srcdir.TabIndex = 11;
            this.button_srcdir.Text = "...";
            this.button_srcdir.UseVisualStyleBackColor = false;
            this.button_srcdir.Click += new System.EventHandler(this.button_srcdir_Click);
            // 
            // label_help_5
            // 
            this.label_help_5.AutoSize = true;
            this.label_help_5.BackColor = System.Drawing.Color.Silver;
            this.label_help_5.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_help_5.ForeColor = System.Drawing.Color.White;
            this.label_help_5.Location = new System.Drawing.Point(442, -1);
            this.label_help_5.Margin = new System.Windows.Forms.Padding(0);
            this.label_help_5.Name = "label_help_5";
            this.label_help_5.Size = new System.Drawing.Size(13, 14);
            this.label_help_5.TabIndex = 40;
            this.label_help_5.Text = "?";
            this.label_help_5.Click += new System.EventHandler(this.label_help_5_Click);
            // 
            // label_copyabove
            // 
            this.label_copyabove.AutoSize = true;
            this.label_copyabove.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_copyabove.ForeColor = System.Drawing.Color.Red;
            this.label_copyabove.Location = new System.Drawing.Point(228, 134);
            this.label_copyabove.Name = "label_copyabove";
            this.label_copyabove.Size = new System.Drawing.Size(35, 12);
            this.label_copyabove.TabIndex = 41;
            this.label_copyabove.Tag = "ccf_copy_above";
            this.label_copyabove.Text = "label5";
            this.label_copyabove.Click += new System.EventHandler(this.label_copyabove_Click);
            // 
            // CreateCloneForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(457, 359);
            this.Controls.Add(this.label_copyabove);
            this.Controls.Add(this.button_srcdir);
            this.Controls.Add(this.button_docdir);
            this.Controls.Add(this.button_create);
            this.Controls.Add(this.textBox_notice);
            this.Controls.Add(this.textBox_srcdir);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_docdir);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_new);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_this);
            this.Controls.Add(this.label_help_5);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CreateCloneForm";
            this.Tag = "CreateCloneForm";
            this.Text = "CreateCloneForm";
            this.Load += new System.EventHandler(this.CreateCloneForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_notice;
        private System.Windows.Forms.Button button_create;
        private System.Windows.Forms.Button button_docdir;
        private System.Windows.Forms.Button button_srcdir;
        public System.Windows.Forms.TextBox textBox_this;
        public System.Windows.Forms.TextBox textBox_new;
        public System.Windows.Forms.TextBox textBox_docdir;
        public System.Windows.Forms.TextBox textBox_srcdir;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label_help_5;
        private System.Windows.Forms.Label label_copyabove;
    }
}