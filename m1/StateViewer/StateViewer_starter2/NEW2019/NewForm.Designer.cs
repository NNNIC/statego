namespace StateViewer_starter2.NEW2019
{
    partial class NewForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewForm));
            this.treeView_starterkit = new System.Windows.Forms.TreeView();
            this.label_starterkitdir = new System.Windows.Forms.Label();
            this.label_starterkitdir_path = new System.Windows.Forms.Label();
            this.textBox_desc = new System.Windows.Forms.TextBox();
            this.button_starterkitdir = new System.Windows.Forms.Button();
            this.button_starterkit_reset = new System.Windows.Forms.Button();
            this.textBox_statemachine = new System.Windows.Forms.TextBox();
            this.textBox_gendir_path = new System.Windows.Forms.TextBox();
            this.button_gendir = new System.Windows.Forms.Button();
            this.checkBox_xlsdir = new System.Windows.Forms.CheckBox();
            this.button_xlsdir = new System.Windows.Forms.Button();
            this.textBox_xlsdir_path = new System.Windows.Forms.TextBox();
            this.button_create = new System.Windows.Forms.Button();
            this.button_old = new System.Windows.Forms.Button();
            this.label_select_starterkit = new System.Windows.Forms.Label();
            this.label_statemachine = new System.Windows.Forms.Label();
            this.label_gendir = new System.Windows.Forms.Label();
            this.textBox_selectstarterkit = new System.Windows.Forms.TextBox();
            this.checkBox_control_name = new System.Windows.Forms.CheckBox();
            this.checkBox_specifydoc = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_xlsdir_eq_srcdir = new System.Windows.Forms.TextBox();
            this.textBox_nothing = new System.Windows.Forms.TextBox();
            this.label_help_win = new System.Windows.Forms.Label();
            this.comboBox_docpath = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // treeView_starterkit
            // 
            this.treeView_starterkit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.treeView_starterkit.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.treeView_starterkit.Location = new System.Drawing.Point(12, 68);
            this.treeView_starterkit.Name = "treeView_starterkit";
            this.treeView_starterkit.Size = new System.Drawing.Size(228, 425);
            this.treeView_starterkit.TabIndex = 0;
            this.treeView_starterkit.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeView_starterkit_AfterSelect);
            // 
            // label_starterkitdir
            // 
            this.label_starterkitdir.AutoSize = true;
            this.label_starterkitdir.Location = new System.Drawing.Point(12, 9);
            this.label_starterkitdir.Name = "label_starterkitdir";
            this.label_starterkitdir.Size = new System.Drawing.Size(100, 12);
            this.label_starterkitdir.TabIndex = 1;
            this.label_starterkitdir.Text = "Starter Kit Folder :";
            // 
            // label_starterkitdir_path
            // 
            this.label_starterkitdir_path.AutoSize = true;
            this.label_starterkitdir_path.ForeColor = System.Drawing.Color.Red;
            this.label_starterkitdir_path.Location = new System.Drawing.Point(118, 9);
            this.label_starterkitdir_path.Name = "label_starterkitdir_path";
            this.label_starterkitdir_path.Size = new System.Drawing.Size(35, 12);
            this.label_starterkitdir_path.TabIndex = 2;
            this.label_starterkitdir_path.Text = "label2";
            // 
            // textBox_desc
            // 
            this.textBox_desc.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_desc.BackColor = System.Drawing.SystemColors.Info;
            this.textBox_desc.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_desc.Location = new System.Drawing.Point(246, 68);
            this.textBox_desc.Multiline = true;
            this.textBox_desc.Name = "textBox_desc";
            this.textBox_desc.ReadOnly = true;
            this.textBox_desc.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_desc.Size = new System.Drawing.Size(546, 180);
            this.textBox_desc.TabIndex = 5;
            // 
            // button_starterkitdir
            // 
            this.button_starterkitdir.Location = new System.Drawing.Point(13, 28);
            this.button_starterkitdir.Margin = new System.Windows.Forms.Padding(0);
            this.button_starterkitdir.Name = "button_starterkitdir";
            this.button_starterkitdir.Size = new System.Drawing.Size(62, 19);
            this.button_starterkitdir.TabIndex = 6;
            this.button_starterkitdir.Text = "Open";
            this.button_starterkitdir.UseVisualStyleBackColor = true;
            this.button_starterkitdir.Click += new System.EventHandler(this.button_starterkitdir_Click);
            // 
            // button_starterkit_reset
            // 
            this.button_starterkit_reset.Location = new System.Drawing.Point(81, 28);
            this.button_starterkit_reset.Margin = new System.Windows.Forms.Padding(0);
            this.button_starterkit_reset.Name = "button_starterkit_reset";
            this.button_starterkit_reset.Size = new System.Drawing.Size(62, 19);
            this.button_starterkit_reset.TabIndex = 7;
            this.button_starterkit_reset.Text = "Reset";
            this.button_starterkit_reset.UseVisualStyleBackColor = true;
            this.button_starterkit_reset.Click += new System.EventHandler(this.button_starterkit_reset_Click);
            // 
            // textBox_statemachine
            // 
            this.textBox_statemachine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_statemachine.Font = new System.Drawing.Font("MS UI Gothic", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_statemachine.Location = new System.Drawing.Point(246, 275);
            this.textBox_statemachine.Name = "textBox_statemachine";
            this.textBox_statemachine.Size = new System.Drawing.Size(546, 44);
            this.textBox_statemachine.TabIndex = 8;
            this.textBox_statemachine.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_statemachine.TextChanged += new System.EventHandler(this.textBox_statemachine_TextChanged);
            // 
            // textBox_gendir_path
            // 
            this.textBox_gendir_path.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_gendir_path.Location = new System.Drawing.Point(246, 345);
            this.textBox_gendir_path.Name = "textBox_gendir_path";
            this.textBox_gendir_path.Size = new System.Drawing.Size(465, 19);
            this.textBox_gendir_path.TabIndex = 9;
            this.textBox_gendir_path.TextChanged += new System.EventHandler(this.textBox_gendir_path_TextChanged);
            // 
            // button_gendir
            // 
            this.button_gendir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_gendir.Location = new System.Drawing.Point(717, 344);
            this.button_gendir.Margin = new System.Windows.Forms.Padding(0);
            this.button_gendir.Name = "button_gendir";
            this.button_gendir.Size = new System.Drawing.Size(75, 20);
            this.button_gendir.TabIndex = 10;
            this.button_gendir.Text = "Open";
            this.button_gendir.UseVisualStyleBackColor = true;
            this.button_gendir.Click += new System.EventHandler(this.button_gendir_Click);
            // 
            // checkBox_xlsdir
            // 
            this.checkBox_xlsdir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_xlsdir.AutoSize = true;
            this.checkBox_xlsdir.Location = new System.Drawing.Point(539, 391);
            this.checkBox_xlsdir.Name = "checkBox_xlsdir";
            this.checkBox_xlsdir.Size = new System.Drawing.Size(253, 16);
            this.checkBox_xlsdir.TabIndex = 11;
            this.checkBox_xlsdir.Tag = "cns_free_xls";
            this.checkBox_xlsdir.Text = "Specify an another folder to save PSGG file ";
            this.checkBox_xlsdir.UseVisualStyleBackColor = true;
            this.checkBox_xlsdir.Visible = false;
            this.checkBox_xlsdir.CheckedChanged += new System.EventHandler(this.checkBox_xlsdir_CheckedChanged);
            // 
            // button_xlsdir
            // 
            this.button_xlsdir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_xlsdir.Location = new System.Drawing.Point(717, 413);
            this.button_xlsdir.Margin = new System.Windows.Forms.Padding(0);
            this.button_xlsdir.Name = "button_xlsdir";
            this.button_xlsdir.Size = new System.Drawing.Size(75, 20);
            this.button_xlsdir.TabIndex = 13;
            this.button_xlsdir.Text = "Open";
            this.button_xlsdir.UseVisualStyleBackColor = true;
            this.button_xlsdir.Visible = false;
            this.button_xlsdir.Click += new System.EventHandler(this.button_xlsdir_Click);
            // 
            // textBox_xlsdir_path
            // 
            this.textBox_xlsdir_path.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_xlsdir_path.Location = new System.Drawing.Point(246, 414);
            this.textBox_xlsdir_path.Name = "textBox_xlsdir_path";
            this.textBox_xlsdir_path.Size = new System.Drawing.Size(465, 19);
            this.textBox_xlsdir_path.TabIndex = 12;
            this.textBox_xlsdir_path.Visible = false;
            // 
            // button_create
            // 
            this.button_create.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button_create.Location = new System.Drawing.Point(602, 445);
            this.button_create.Name = "button_create";
            this.button_create.Size = new System.Drawing.Size(190, 53);
            this.button_create.TabIndex = 14;
            this.button_create.Tag = "cns_create";
            this.button_create.Text = "作成";
            this.button_create.UseVisualStyleBackColor = true;
            this.button_create.Click += new System.EventHandler(this.button_create_Click);
            // 
            // button_old
            // 
            this.button_old.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button_old.Enabled = false;
            this.button_old.Location = new System.Drawing.Point(246, 445);
            this.button_old.Name = "button_old";
            this.button_old.Size = new System.Drawing.Size(190, 53);
            this.button_old.TabIndex = 15;
            this.button_old.Tag = "cns_toold";
            this.button_old.Text = "旧スタートキット用\r\n新規作成フォームへ";
            this.button_old.UseVisualStyleBackColor = true;
            this.button_old.Visible = false;
            this.button_old.Click += new System.EventHandler(this.button_old_Click);
            // 
            // label_select_starterkit
            // 
            this.label_select_starterkit.AutoSize = true;
            this.label_select_starterkit.Location = new System.Drawing.Point(12, 53);
            this.label_select_starterkit.Name = "label_select_starterkit";
            this.label_select_starterkit.Size = new System.Drawing.Size(94, 12);
            this.label_select_starterkit.TabIndex = 17;
            this.label_select_starterkit.Tag = "cns_select";
            this.label_select_starterkit.Text = "Select Starter Kit";
            // 
            // label_statemachine
            // 
            this.label_statemachine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_statemachine.AutoSize = true;
            this.label_statemachine.Location = new System.Drawing.Point(246, 260);
            this.label_statemachine.Name = "label_statemachine";
            this.label_statemachine.Size = new System.Drawing.Size(109, 12);
            this.label_statemachine.TabIndex = 18;
            this.label_statemachine.Tag = "cns_statemachine";
            this.label_statemachine.Text = "State machine name";
            // 
            // label_gendir
            // 
            this.label_gendir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label_gendir.AutoSize = true;
            this.label_gendir.Location = new System.Drawing.Point(246, 330);
            this.label_gendir.Name = "label_gendir";
            this.label_gendir.Size = new System.Drawing.Size(96, 12);
            this.label_gendir.TabIndex = 19;
            this.label_gendir.Tag = "cns_src_path";
            this.label_gendir.Text = "Source Save Path";
            // 
            // textBox_selectstarterkit
            // 
            this.textBox_selectstarterkit.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_selectstarterkit.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_selectstarterkit.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_selectstarterkit.Location = new System.Drawing.Point(246, 36);
            this.textBox_selectstarterkit.Name = "textBox_selectstarterkit";
            this.textBox_selectstarterkit.ReadOnly = true;
            this.textBox_selectstarterkit.Size = new System.Drawing.Size(546, 21);
            this.textBox_selectstarterkit.TabIndex = 20;
            this.textBox_selectstarterkit.Text = "SELECTED STARTER KIT";
            // 
            // checkBox_control_name
            // 
            this.checkBox_control_name.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_control_name.AutoSize = true;
            this.checkBox_control_name.Checked = true;
            this.checkBox_control_name.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_control_name.Location = new System.Drawing.Point(450, 259);
            this.checkBox_control_name.Name = "checkBox_control_name";
            this.checkBox_control_name.Size = new System.Drawing.Size(131, 16);
            this.checkBox_control_name.TabIndex = 21;
            this.checkBox_control_name.Tag = "cns_use_control";
            this.checkBox_control_name.Text = "Use \'Control\' for end.";
            this.checkBox_control_name.UseVisualStyleBackColor = true;
            this.checkBox_control_name.CheckedChanged += new System.EventHandler(this.checkBox_control_name_CheckedChanged);
            // 
            // checkBox_specifydoc
            // 
            this.checkBox_specifydoc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.checkBox_specifydoc.AutoSize = true;
            this.checkBox_specifydoc.Location = new System.Drawing.Point(246, 391);
            this.checkBox_specifydoc.Name = "checkBox_specifydoc";
            this.checkBox_specifydoc.Size = new System.Drawing.Size(235, 16);
            this.checkBox_specifydoc.TabIndex = 22;
            this.checkBox_specifydoc.Tag = "cns_doc_under_src";
            this.checkBox_specifydoc.Text = "Specify doc folder under sorce save path";
            this.checkBox_specifydoc.UseVisualStyleBackColor = true;
            this.checkBox_specifydoc.Visible = false;
            this.checkBox_specifydoc.CheckedChanged += new System.EventHandler(this.checkBox_specifydoc_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(757, 260);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 12);
            this.label1.TabIndex = 23;
            this.label1.Text = "reset";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.Location = new System.Drawing.Point(246, 380);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 15);
            this.label2.TabIndex = 24;
            this.label2.Tag = "cns_xls_path";
            this.label2.Text = "Document Save Path";
            // 
            // textBox_xlsdir_eq_srcdir
            // 
            this.textBox_xlsdir_eq_srcdir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_xlsdir_eq_srcdir.BackColor = System.Drawing.SystemColors.Info;
            this.textBox_xlsdir_eq_srcdir.Cursor = System.Windows.Forms.Cursors.No;
            this.textBox_xlsdir_eq_srcdir.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.textBox_xlsdir_eq_srcdir.Location = new System.Drawing.Point(246, 413);
            this.textBox_xlsdir_eq_srcdir.Name = "textBox_xlsdir_eq_srcdir";
            this.textBox_xlsdir_eq_srcdir.Size = new System.Drawing.Size(465, 19);
            this.textBox_xlsdir_eq_srcdir.TabIndex = 25;
            this.textBox_xlsdir_eq_srcdir.Tag = "cns_xlsdir_eq_srcdir";
            this.textBox_xlsdir_eq_srcdir.Text = "As same as source save path";
            this.textBox_xlsdir_eq_srcdir.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_xlsdir_eq_srcdir.Visible = false;
            // 
            // textBox_nothing
            // 
            this.textBox_nothing.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBox_nothing.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_nothing.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_nothing.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.textBox_nothing.Location = new System.Drawing.Point(31, 141);
            this.textBox_nothing.Multiline = true;
            this.textBox_nothing.Name = "textBox_nothing";
            this.textBox_nothing.ReadOnly = true;
            this.textBox_nothing.Size = new System.Drawing.Size(192, 201);
            this.textBox_nothing.TabIndex = 26;
            this.textBox_nothing.Tag = "cns_nothing";
            this.textBox_nothing.Text = "If nothing is shown here and you are not familiar with StateGo, please click \"Res" +
    "et\" button above this panel.";
            // 
            // label_help_win
            // 
            this.label_help_win.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_help_win.AutoSize = true;
            this.label_help_win.BackColor = System.Drawing.Color.Silver;
            this.label_help_win.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_help_win.ForeColor = System.Drawing.Color.White;
            this.label_help_win.Location = new System.Drawing.Point(782, 7);
            this.label_help_win.Margin = new System.Windows.Forms.Padding(0);
            this.label_help_win.Name = "label_help_win";
            this.label_help_win.Size = new System.Drawing.Size(13, 14);
            this.label_help_win.TabIndex = 27;
            this.label_help_win.Text = "?";
            this.label_help_win.Click += new System.EventHandler(this.label_help_win_Click);
            // 
            // comboBox_docpath
            // 
            this.comboBox_docpath.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_docpath.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboBox_docpath.FormattingEnabled = true;
            this.comboBox_docpath.Items.AddRange(new object[] {
            "Same as the source path.",
            "To save StateGo file, Specify \'doc\' folder  under the source path.",
            "Spefify an another folder to save StateGo file."});
            this.comboBox_docpath.Location = new System.Drawing.Point(384, 377);
            this.comboBox_docpath.Name = "comboBox_docpath";
            this.comboBox_docpath.Size = new System.Drawing.Size(327, 20);
            this.comboBox_docpath.TabIndex = 28;
            this.comboBox_docpath.SelectedIndexChanged += new System.EventHandler(this.comboBox_docpath_SelectedIndexChanged);
            // 
            // NewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 505);
            this.Controls.Add(this.comboBox_docpath);
            this.Controls.Add(this.label_help_win);
            this.Controls.Add(this.textBox_nothing);
            this.Controls.Add(this.textBox_xlsdir_eq_srcdir);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.checkBox_specifydoc);
            this.Controls.Add(this.checkBox_control_name);
            this.Controls.Add(this.textBox_selectstarterkit);
            this.Controls.Add(this.label_gendir);
            this.Controls.Add(this.label_statemachine);
            this.Controls.Add(this.label_select_starterkit);
            this.Controls.Add(this.button_old);
            this.Controls.Add(this.button_create);
            this.Controls.Add(this.button_xlsdir);
            this.Controls.Add(this.textBox_xlsdir_path);
            this.Controls.Add(this.checkBox_xlsdir);
            this.Controls.Add(this.button_gendir);
            this.Controls.Add(this.textBox_gendir_path);
            this.Controls.Add(this.textBox_statemachine);
            this.Controls.Add(this.button_starterkit_reset);
            this.Controls.Add(this.button_starterkitdir);
            this.Controls.Add(this.textBox_desc);
            this.Controls.Add(this.label_starterkitdir_path);
            this.Controls.Add(this.label_starterkitdir);
            this.Controls.Add(this.treeView_starterkit);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NewForm";
            this.Text = "Create New State Machine";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NewForm_FormClosing);
            this.Load += new System.EventHandler(this.NewForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label_select_starterkit;
        private System.Windows.Forms.Label label_statemachine;
        private System.Windows.Forms.Label label_gendir;
        internal System.Windows.Forms.TreeView treeView_starterkit;
        internal System.Windows.Forms.Label label_starterkitdir;
        internal System.Windows.Forms.Label label_starterkitdir_path;
        internal System.Windows.Forms.TextBox textBox_desc;
        internal System.Windows.Forms.Button button_starterkitdir;
        internal System.Windows.Forms.Button button_starterkit_reset;
        internal System.Windows.Forms.TextBox textBox_statemachine;
        internal System.Windows.Forms.TextBox textBox_gendir_path;
        internal System.Windows.Forms.Button button_gendir;
        internal System.Windows.Forms.Button button_xlsdir;
        internal System.Windows.Forms.TextBox textBox_xlsdir_path;
        internal System.Windows.Forms.Button button_create;
        internal System.Windows.Forms.Button button_old;
        internal System.Windows.Forms.TextBox textBox_selectstarterkit;
        public System.Windows.Forms.CheckBox checkBox_xlsdir;
        internal System.Windows.Forms.CheckBox checkBox_control_name;
        internal System.Windows.Forms.CheckBox checkBox_specifydoc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        internal System.Windows.Forms.TextBox textBox_xlsdir_eq_srcdir;
        public System.Windows.Forms.TextBox textBox_nothing;
        private System.Windows.Forms.Label label_help_win;
        public System.Windows.Forms.ComboBox comboBox_docpath;
    }
}