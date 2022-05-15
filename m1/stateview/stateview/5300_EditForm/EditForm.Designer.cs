namespace stateview._5300_EditForm
{
    partial class EditForm
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
            if(disposing && (components != null))
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditForm));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Row = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NAME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.help = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEM_VAUE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ok_button = new System.Windows.Forms.Button();
            this.cancel_button = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.ImportButton = new System.Windows.Forms.Button();
            this.import_from_cb_button = new System.Windows.Forms.Button();
            this.export_to_cb_button = new System.Windows.Forms.Button();
            this.radioButton_opt = new System.Windows.Forms.RadioButton();
            this.radioButton_full = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton_optmid = new System.Windows.Forms.RadioButton();
            this.label_help = new System.Windows.Forms.Label();
            this.label_explain = new System.Windows.Forms.Label();
            this.button_clearStateCmt = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Row,
            this.NAME,
            this.help,
            this.ITEM_VAUE});
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.Size = new System.Drawing.Size(439, 410);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // Row
            // 
            this.Row.Frozen = true;
            this.Row.HeaderText = "Row";
            this.Row.Name = "Row";
            this.Row.ReadOnly = true;
            this.Row.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Row.Width = 30;
            // 
            // NAME
            // 
            this.NAME.HeaderText = "NAME";
            this.NAME.Name = "NAME";
            this.NAME.ReadOnly = true;
            this.NAME.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.NAME.Width = 75;
            // 
            // help
            // 
            this.help.HeaderText = "Desc";
            this.help.Name = "help";
            this.help.ReadOnly = true;
            this.help.Width = 150;
            // 
            // ITEM_VAUE
            // 
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.ITEM_VAUE.DefaultCellStyle = dataGridViewCellStyle1;
            this.ITEM_VAUE.FillWeight = 300F;
            this.ITEM_VAUE.HeaderText = "Value";
            this.ITEM_VAUE.Name = "ITEM_VAUE";
            this.ITEM_VAUE.ReadOnly = true;
            this.ITEM_VAUE.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ITEM_VAUE.Width = 800;
            // 
            // ok_button
            // 
            this.ok_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ok_button.Location = new System.Drawing.Point(457, 12);
            this.ok_button.Name = "ok_button";
            this.ok_button.Size = new System.Drawing.Size(75, 23);
            this.ok_button.TabIndex = 1;
            this.ok_button.Text = "OK";
            this.ok_button.UseVisualStyleBackColor = true;
            this.ok_button.Click += new System.EventHandler(this.ok_button_Click);
            // 
            // cancel_button
            // 
            this.cancel_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cancel_button.Location = new System.Drawing.Point(457, 51);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.Size = new System.Drawing.Size(75, 23);
            this.cancel_button.TabIndex = 2;
            this.cancel_button.Text = "CANCEL";
            this.cancel_button.UseVisualStyleBackColor = true;
            this.cancel_button.Click += new System.EventHandler(this.cancel_button_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // ImportButton
            // 
            this.ImportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ImportButton.Enabled = false;
            this.ImportButton.Location = new System.Drawing.Point(457, 331);
            this.ImportButton.Name = "ImportButton";
            this.ImportButton.Size = new System.Drawing.Size(75, 23);
            this.ImportButton.TabIndex = 3;
            this.ImportButton.Text = "Import";
            this.ImportButton.UseVisualStyleBackColor = true;
            this.ImportButton.Visible = false;
            // 
            // import_from_cb_button
            // 
            this.import_from_cb_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.import_from_cb_button.Location = new System.Drawing.Point(457, 358);
            this.import_from_cb_button.Name = "import_from_cb_button";
            this.import_from_cb_button.Size = new System.Drawing.Size(75, 33);
            this.import_from_cb_button.TabIndex = 4;
            this.import_from_cb_button.Text = "Import from clipboard";
            this.import_from_cb_button.UseVisualStyleBackColor = true;
            this.import_from_cb_button.Visible = false;
            this.import_from_cb_button.Click += new System.EventHandler(this.import_from_cb_button_Click);
            // 
            // export_to_cb_button
            // 
            this.export_to_cb_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.export_to_cb_button.Location = new System.Drawing.Point(457, 394);
            this.export_to_cb_button.Name = "export_to_cb_button";
            this.export_to_cb_button.Size = new System.Drawing.Size(75, 33);
            this.export_to_cb_button.TabIndex = 5;
            this.export_to_cb_button.Text = "Export to clipboard";
            this.export_to_cb_button.UseVisualStyleBackColor = true;
            this.export_to_cb_button.Visible = false;
            this.export_to_cb_button.Click += new System.EventHandler(this.export_to_cb_button_Click);
            // 
            // radioButton_opt
            // 
            this.radioButton_opt.AutoSize = true;
            this.radioButton_opt.Checked = true;
            this.radioButton_opt.Location = new System.Drawing.Point(6, 18);
            this.radioButton_opt.Name = "radioButton_opt";
            this.radioButton_opt.Size = new System.Drawing.Size(73, 16);
            this.radioButton_opt.TabIndex = 6;
            this.radioButton_opt.TabStop = true;
            this.radioButton_opt.Tag = "eftf_opt";
            this.radioButton_opt.Text = "Optimized";
            this.radioButton_opt.UseVisualStyleBackColor = true;
            this.radioButton_opt.CheckedChanged += new System.EventHandler(this.radioButton_opt_CheckedChanged);
            // 
            // radioButton_full
            // 
            this.radioButton_full.AutoSize = true;
            this.radioButton_full.Location = new System.Drawing.Point(6, 58);
            this.radioButton_full.Name = "radioButton_full";
            this.radioButton_full.Size = new System.Drawing.Size(42, 16);
            this.radioButton_full.TabIndex = 7;
            this.radioButton_full.Tag = "eftf_full";
            this.radioButton_full.Text = "Full";
            this.radioButton_full.UseVisualStyleBackColor = true;
            this.radioButton_full.CheckedChanged += new System.EventHandler(this.radioButton_opt_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.radioButton_optmid);
            this.groupBox1.Controls.Add(this.radioButton_full);
            this.groupBox1.Controls.Add(this.radioButton_opt);
            this.groupBox1.Location = new System.Drawing.Point(454, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(88, 87);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Tag = "eftf_ditms";
            this.groupBox1.Text = "Disp Items";
            // 
            // radioButton_optmid
            // 
            this.radioButton_optmid.AutoSize = true;
            this.radioButton_optmid.Location = new System.Drawing.Point(6, 38);
            this.radioButton_optmid.Name = "radioButton_optmid";
            this.radioButton_optmid.Size = new System.Drawing.Size(41, 16);
            this.radioButton_optmid.TabIndex = 8;
            this.radioButton_optmid.Tag = "eftf_optmid";
            this.radioButton_optmid.Text = "Mid";
            this.radioButton_optmid.UseVisualStyleBackColor = true;
            this.radioButton_optmid.CheckedChanged += new System.EventHandler(this.radioButton_optmid_CheckedChanged);
            // 
            // label_help
            // 
            this.label_help.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_help.AutoSize = true;
            this.label_help.BackColor = System.Drawing.Color.Silver;
            this.label_help.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_help.ForeColor = System.Drawing.Color.White;
            this.label_help.Location = new System.Drawing.Point(531, 0);
            this.label_help.Margin = new System.Windows.Forms.Padding(0);
            this.label_help.Name = "label_help";
            this.label_help.Size = new System.Drawing.Size(13, 14);
            this.label_help.TabIndex = 18;
            this.label_help.Text = "?";
            this.label_help.Click += new System.EventHandler(this.label_help_Click);
            // 
            // label_explain
            // 
            this.label_explain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_explain.AutoSize = true;
            this.label_explain.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_explain.ForeColor = System.Drawing.Color.DarkRed;
            this.label_explain.Location = new System.Drawing.Point(457, 268);
            this.label_explain.Name = "label_explain";
            this.label_explain.Size = new System.Drawing.Size(45, 12);
            this.label_explain.TabIndex = 19;
            this.label_explain.Text = "説明ON";
            this.label_explain.Click += new System.EventHandler(this.label_explain_Click);
            // 
            // button_clearStateCmt
            // 
            this.button_clearStateCmt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_clearStateCmt.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_clearStateCmt.Location = new System.Drawing.Point(459, 184);
            this.button_clearStateCmt.Name = "button_clearStateCmt";
            this.button_clearStateCmt.Size = new System.Drawing.Size(75, 42);
            this.button_clearStateCmt.TabIndex = 20;
            this.button_clearStateCmt.Tag = "eftf_clrcmt";
            this.button_clearStateCmt.Text = "Clear\r\nState\r\nComment";
            this.button_clearStateCmt.UseVisualStyleBackColor = true;
            this.button_clearStateCmt.Visible = false;
            this.button_clearStateCmt.Click += new System.EventHandler(this.button_clearStateCmt_Click);
            // 
            // EditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 434);
            this.Controls.Add(this.button_clearStateCmt);
            this.Controls.Add(this.label_explain);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.export_to_cb_button);
            this.Controls.Add(this.import_from_cb_button);
            this.Controls.Add(this.ImportButton);
            this.Controls.Add(this.cancel_button);
            this.Controls.Add(this.ok_button);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label_help);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditForm";
            this.Tag = "";
            this.Text = "Edit";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditForm_FormClosing);
            this.Load += new System.EventHandler(this.EditForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button ok_button;
        private System.Windows.Forms.Button cancel_button;
        internal System.Windows.Forms.DataGridView dataGridView1;
        internal System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button ImportButton;
        private System.Windows.Forms.Button import_from_cb_button;
        private System.Windows.Forms.Button export_to_cb_button;
        private System.Windows.Forms.RadioButton radioButton_opt;
        private System.Windows.Forms.RadioButton radioButton_full;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label_help;
        private System.Windows.Forms.RadioButton radioButton_optmid;
        private System.Windows.Forms.Label label_explain;
        private System.Windows.Forms.Button button_clearStateCmt;
        private System.Windows.Forms.DataGridViewTextBoxColumn Row;
        private System.Windows.Forms.DataGridViewTextBoxColumn NAME;
        private System.Windows.Forms.DataGridViewTextBoxColumn help;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEM_VAUE;
    }
}