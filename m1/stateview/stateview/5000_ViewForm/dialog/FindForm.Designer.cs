namespace stateview._5000_ViewForm.dialog
{
    partial class FindForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FindForm));
            this.label1 = new System.Windows.Forms.Label();
            this.combox_text = new System.Windows.Forms.ComboBox();
            this.cb_state = new System.Windows.Forms.CheckBox();
            this.cb_contents = new System.Windows.Forms.CheckBox();
            this.cb_others = new System.Windows.Forms.CheckBox();
            this.cb_all = new System.Windows.Forms.CheckBox();
            this.cb_word = new System.Windows.Forms.CheckBox();
            this.btn_find = new System.Windows.Forms.Button();
            this.tb_result = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cb_case = new System.Windows.Forms.CheckBox();
            this.cb_regex = new System.Windows.Forms.CheckBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label_help_5 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.state = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dir = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Comment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label_result = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 12);
            this.label1.TabIndex = 0;
            this.label1.Tag = "findd_searchtext";
            this.label1.Text = "Search text";
            // 
            // combox_text
            // 
            this.combox_text.FormattingEnabled = true;
            this.combox_text.Location = new System.Drawing.Point(72, 17);
            this.combox_text.Name = "combox_text";
            this.combox_text.Size = new System.Drawing.Size(246, 20);
            this.combox_text.TabIndex = 1;
            this.combox_text.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.combox_text_PreviewKeyDown);
            // 
            // cb_state
            // 
            this.cb_state.AutoSize = true;
            this.cb_state.Checked = true;
            this.cb_state.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_state.Location = new System.Drawing.Point(333, 20);
            this.cb_state.Name = "cb_state";
            this.cb_state.Size = new System.Drawing.Size(50, 16);
            this.cb_state.TabIndex = 2;
            this.cb_state.Tag = "findd_state";
            this.cb_state.Text = "state";
            this.cb_state.UseVisualStyleBackColor = true;
            // 
            // cb_contents
            // 
            this.cb_contents.AutoSize = true;
            this.cb_contents.Location = new System.Drawing.Point(406, 20);
            this.cb_contents.Name = "cb_contents";
            this.cb_contents.Size = new System.Drawing.Size(68, 16);
            this.cb_contents.TabIndex = 3;
            this.cb_contents.Tag = "findd_contents";
            this.cb_contents.Text = "contents";
            this.cb_contents.UseVisualStyleBackColor = true;
            // 
            // cb_others
            // 
            this.cb_others.AutoSize = true;
            this.cb_others.Location = new System.Drawing.Point(480, 20);
            this.cb_others.Name = "cb_others";
            this.cb_others.Size = new System.Drawing.Size(56, 16);
            this.cb_others.TabIndex = 4;
            this.cb_others.Tag = "findd_others";
            this.cb_others.Text = "others";
            this.cb_others.UseVisualStyleBackColor = true;
            // 
            // cb_all
            // 
            this.cb_all.AutoSize = true;
            this.cb_all.Location = new System.Drawing.Point(542, 20);
            this.cb_all.Name = "cb_all";
            this.cb_all.Size = new System.Drawing.Size(36, 16);
            this.cb_all.TabIndex = 5;
            this.cb_all.Tag = "findd_all";
            this.cb_all.Text = "all";
            this.cb_all.UseVisualStyleBackColor = true;
            // 
            // cb_word
            // 
            this.cb_word.AutoSize = true;
            this.cb_word.Location = new System.Drawing.Point(181, 53);
            this.cb_word.Name = "cb_word";
            this.cb_word.Size = new System.Drawing.Size(52, 16);
            this.cb_word.TabIndex = 6;
            this.cb_word.Tag = "findd_word";
            this.cb_word.Text = "word ";
            this.cb_word.UseVisualStyleBackColor = true;
            // 
            // btn_find
            // 
            this.btn_find.Location = new System.Drawing.Point(624, 17);
            this.btn_find.Name = "btn_find";
            this.btn_find.Size = new System.Drawing.Size(75, 23);
            this.btn_find.TabIndex = 7;
            this.btn_find.Tag = "findd_find";
            this.btn_find.Text = "Find";
            this.btn_find.UseVisualStyleBackColor = true;
            this.btn_find.Click += new System.EventHandler(this.btn_find_Click);
            // 
            // tb_result
            // 
            this.tb_result.BackColor = System.Drawing.SystemColors.HighlightText;
            this.tb_result.Location = new System.Drawing.Point(320, 324);
            this.tb_result.Multiline = true;
            this.tb_result.Name = "tb_result";
            this.tb_result.ReadOnly = true;
            this.tb_result.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tb_result.Size = new System.Drawing.Size(361, 62);
            this.tb_result.TabIndex = 8;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 109);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 12);
            this.label2.TabIndex = 9;
            this.label2.Tag = "findd_result";
            this.label2.Text = "Result";
            // 
            // cb_case
            // 
            this.cb_case.AutoSize = true;
            this.cb_case.Location = new System.Drawing.Point(72, 53);
            this.cb_case.Name = "cb_case";
            this.cb_case.Size = new System.Drawing.Size(83, 16);
            this.cb_case.TabIndex = 10;
            this.cb_case.Tag = "findd_caseignore";
            this.cb_case.Text = "case ignore";
            this.cb_case.UseVisualStyleBackColor = true;
            // 
            // cb_regex
            // 
            this.cb_regex.AutoSize = true;
            this.cb_regex.Location = new System.Drawing.Point(260, 53);
            this.cb_regex.Name = "cb_regex";
            this.cb_regex.Size = new System.Drawing.Size(52, 16);
            this.cb_regex.TabIndex = 11;
            this.cb_regex.Tag = "findd_regex";
            this.cb_regex.Text = "regex";
            this.cb_regex.UseVisualStyleBackColor = true;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label_help_5
            // 
            this.label_help_5.AutoSize = true;
            this.label_help_5.BackColor = System.Drawing.Color.Silver;
            this.label_help_5.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_help_5.ForeColor = System.Drawing.Color.White;
            this.label_help_5.Location = new System.Drawing.Point(715, -1);
            this.label_help_5.Margin = new System.Windows.Forms.Padding(0);
            this.label_help_5.Name = "label_help_5";
            this.label_help_5.Size = new System.Drawing.Size(13, 14);
            this.label_help_5.TabIndex = 38;
            this.label_help_5.Text = "?";
            this.label_help_5.Click += new System.EventHandler(this.label_help_5_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.state,
            this.dir,
            this.Comment});
            this.dataGridView1.Location = new System.Drawing.Point(72, 99);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(627, 201);
            this.dataGridView1.TabIndex = 39;
            this.dataGridView1.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseDoubleClick);
            // 
            // state
            // 
            this.state.HeaderText = "State";
            this.state.Name = "state";
            this.state.ReadOnly = true;
            this.state.Width = 130;
            // 
            // dir
            // 
            this.dir.HeaderText = "Path";
            this.dir.Name = "dir";
            this.dir.ReadOnly = true;
            this.dir.Width = 180;
            // 
            // Comment
            // 
            this.Comment.HeaderText = "Comment";
            this.Comment.Name = "Comment";
            this.Comment.ReadOnly = true;
            this.Comment.Width = 500;
            // 
            // label_result
            // 
            this.label_result.AutoSize = true;
            this.label_result.ForeColor = System.Drawing.Color.DarkRed;
            this.label_result.Location = new System.Drawing.Point(72, 84);
            this.label_result.Name = "label_result";
            this.label_result.Size = new System.Drawing.Size(38, 12);
            this.label_result.TabIndex = 40;
            this.label_result.Tag = "findd_result";
            this.label_result.Text = "Result";
            this.label_result.Visible = false;
            // 
            // FindForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 312);
            this.Controls.Add(this.label_result);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cb_regex);
            this.Controls.Add(this.cb_case);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tb_result);
            this.Controls.Add(this.btn_find);
            this.Controls.Add(this.cb_word);
            this.Controls.Add(this.cb_all);
            this.Controls.Add(this.cb_others);
            this.Controls.Add(this.cb_contents);
            this.Controls.Add(this.cb_state);
            this.Controls.Add(this.combox_text);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label_help_5);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FindForm";
            this.Tag = "findd_title";
            this.Text = "Find";
            this.Activated += new System.EventHandler(this.FindForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FindForm_FormClosing);
            this.Load += new System.EventHandler(this.FindForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FindForm_KeyDown);
            this.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.FindForm_PreviewKeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timer1;
        public System.Windows.Forms.ComboBox combox_text;
        public System.Windows.Forms.CheckBox cb_state;
        public System.Windows.Forms.CheckBox cb_contents;
        public System.Windows.Forms.CheckBox cb_others;
        public System.Windows.Forms.CheckBox cb_all;
        public System.Windows.Forms.CheckBox cb_word;
        public System.Windows.Forms.Button btn_find;
        public System.Windows.Forms.CheckBox cb_case;
        public System.Windows.Forms.CheckBox cb_regex;
        public System.Windows.Forms.TextBox tb_result;
        private System.Windows.Forms.Label label_help_5;
        public System.Windows.Forms.DataGridView dataGridView1;
        public System.Windows.Forms.Label label_result;
        private System.Windows.Forms.DataGridViewTextBoxColumn state;
        private System.Windows.Forms.DataGridViewTextBoxColumn dir;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comment;
    }
}