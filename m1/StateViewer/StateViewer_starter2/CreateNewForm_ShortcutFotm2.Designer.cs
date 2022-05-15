namespace StateViewer_starter2
{
    partial class CreateNewForm_ShortcutFotm2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateNewForm_ShortcutFotm2));
            this.button_g3clear = new System.Windows.Forms.Button();
            this.buttonOpenSrcFolderDialog = new System.Windows.Forms.Button();
            this.textBoxSrcFolder = new System.Windows.Forms.TextBox();
            this.button_back = new System.Windows.Forms.Button();
            this.button_ok = new System.Windows.Forms.Button();
            this.pictureBox_allinone = new System.Windows.Forms.PictureBox();
            this.pictureBox_srcwithdoc = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox_srcwdoc = new System.Windows.Forms.TextBox();
            this.textBox_allinone = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_allinone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_srcwithdoc)).BeginInit();
            this.SuspendLayout();
            // 
            // button_g3clear
            // 
            this.button_g3clear.Location = new System.Drawing.Point(366, 480);
            this.button_g3clear.Name = "button_g3clear";
            this.button_g3clear.Size = new System.Drawing.Size(75, 23);
            this.button_g3clear.TabIndex = 19;
            this.button_g3clear.Text = "CLEAR";
            this.button_g3clear.UseVisualStyleBackColor = true;
            this.button_g3clear.Click += new System.EventHandler(this.button_g3clear_Click);
            // 
            // buttonOpenSrcFolderDialog
            // 
            this.buttonOpenSrcFolderDialog.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOpenSrcFolderDialog.Location = new System.Drawing.Point(171, 480);
            this.buttonOpenSrcFolderDialog.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.buttonOpenSrcFolderDialog.Name = "buttonOpenSrcFolderDialog";
            this.buttonOpenSrcFolderDialog.Size = new System.Drawing.Size(87, 40);
            this.buttonOpenSrcFolderDialog.TabIndex = 18;
            this.buttonOpenSrcFolderDialog.Text = "FOLDER";
            this.buttonOpenSrcFolderDialog.UseVisualStyleBackColor = true;
            this.buttonOpenSrcFolderDialog.Click += new System.EventHandler(this.buttonOpenSrcFolderDialog_Click);
            // 
            // textBoxSrcFolder
            // 
            this.textBoxSrcFolder.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxSrcFolder.Location = new System.Drawing.Point(12, 351);
            this.textBoxSrcFolder.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.textBoxSrcFolder.Multiline = true;
            this.textBoxSrcFolder.Name = "textBoxSrcFolder";
            this.textBoxSrcFolder.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxSrcFolder.Size = new System.Drawing.Size(627, 119);
            this.textBoxSrcFolder.TabIndex = 17;
            this.textBoxSrcFolder.WordWrap = false;
            // 
            // button_back
            // 
            this.button_back.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_back.Location = new System.Drawing.Point(12, 539);
            this.button_back.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.button_back.Name = "button_back";
            this.button_back.Size = new System.Drawing.Size(87, 40);
            this.button_back.TabIndex = 20;
            this.button_back.Text = "BACK";
            this.button_back.UseVisualStyleBackColor = true;
            this.button_back.Click += new System.EventHandler(this.button_back_Click);
            // 
            // button_ok
            // 
            this.button_ok.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_ok.Location = new System.Drawing.Point(552, 539);
            this.button_ok.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(87, 40);
            this.button_ok.TabIndex = 21;
            this.button_ok.Text = "NEXT";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // pictureBox_allinone
            // 
            this.pictureBox_allinone.BackgroundImage = global::StateViewer_starter2.Properties.Resources.all_on_a_folder;
            this.pictureBox_allinone.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_allinone.Location = new System.Drawing.Point(171, 98);
            this.pictureBox_allinone.Name = "pictureBox_allinone";
            this.pictureBox_allinone.Size = new System.Drawing.Size(322, 170);
            this.pictureBox_allinone.TabIndex = 0;
            this.pictureBox_allinone.TabStop = false;
            // 
            // pictureBox_srcwithdoc
            // 
            this.pictureBox_srcwithdoc.BackgroundImage = global::StateViewer_starter2.Properties.Resources.doc_on_src_folder;
            this.pictureBox_srcwithdoc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox_srcwithdoc.Location = new System.Drawing.Point(171, 98);
            this.pictureBox_srcwithdoc.Name = "pictureBox_srcwithdoc";
            this.pictureBox_srcwithdoc.Size = new System.Drawing.Size(322, 170);
            this.pictureBox_srcwithdoc.TabIndex = 22;
            this.pictureBox_srcwithdoc.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox1.Location = new System.Drawing.Point(61, 309);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(502, 24);
            this.textBox1.TabIndex = 23;
            this.textBox1.Tag = "sc_select_folder";
            this.textBox1.Text = "ソース用のフォルダを指定してください。";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_srcwdoc
            // 
            this.textBox_srcwdoc.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_srcwdoc.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_srcwdoc.Location = new System.Drawing.Point(84, 35);
            this.textBox_srcwdoc.Name = "textBox_srcwdoc";
            this.textBox_srcwdoc.ReadOnly = true;
            this.textBox_srcwdoc.Size = new System.Drawing.Size(502, 24);
            this.textBox_srcwdoc.TabIndex = 24;
            this.textBox_srcwdoc.Tag = "sc_exp_src_w_doc";
            this.textBox_srcwdoc.Text = "ソース用のフォルダを指定してください。";
            this.textBox_srcwdoc.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_allinone
            // 
            this.textBox_allinone.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_allinone.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_allinone.Location = new System.Drawing.Point(12, 35);
            this.textBox_allinone.Name = "textBox_allinone";
            this.textBox_allinone.ReadOnly = true;
            this.textBox_allinone.Size = new System.Drawing.Size(627, 24);
            this.textBox_allinone.TabIndex = 25;
            this.textBox_allinone.Tag = "sc_exp_allinone";
            this.textBox_allinone.Text = "ソース用のフォルダを指定してください。";
            this.textBox_allinone.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // CreateNewForm_ShortcutFotm2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 593);
            this.Controls.Add(this.textBox_allinone);
            this.Controls.Add(this.textBox_srcwdoc);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.pictureBox_srcwithdoc);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.button_back);
            this.Controls.Add(this.button_g3clear);
            this.Controls.Add(this.buttonOpenSrcFolderDialog);
            this.Controls.Add(this.textBoxSrcFolder);
            this.Controls.Add(this.pictureBox_allinone);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CreateNewForm_ShortcutFotm2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "sc_titleselectfolder";
            this.Text = "【簡易指定】フォルダ指定";
            this.Load += new System.EventHandler(this.CreateNewForm_ShortcutFotm2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_allinone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_srcwithdoc)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_allinone;
        public System.Windows.Forms.Button button_g3clear;
        private System.Windows.Forms.Button buttonOpenSrcFolderDialog;
        public System.Windows.Forms.TextBox textBoxSrcFolder;
        private System.Windows.Forms.Button button_back;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.PictureBox pictureBox_srcwithdoc;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox_srcwdoc;
        private System.Windows.Forms.TextBox textBox_allinone;
    }
}