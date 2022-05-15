namespace stateview._5000_ViewForm.dialog
{
    partial class AltStateDistinationForm1
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
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Path = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.State = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StateType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Comment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cancel_button = new System.Windows.Forms.Button();
            this.ok_button = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.checkBox_incsub = new System.Windows.Forms.CheckBox();
            this.radioButton_alltypes = new System.Windows.Forms.RadioButton();
            this.radioButton_pass = new System.Windows.Forms.RadioButton();
            this.radioButton_substart = new System.Windows.Forms.RadioButton();
            this.GroupBox_type = new System.Windows.Forms.GroupBox();
            this.checkBox_noinput = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.GroupBox_type.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 12);
            this.label1.TabIndex = 19;
            this.label1.Text = "Distination State";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Path,
            this.State,
            this.StateType,
            this.Comment});
            this.dataGridView1.Location = new System.Drawing.Point(12, 55);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.Size = new System.Drawing.Size(441, 300);
            this.dataGridView1.TabIndex = 18;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // Path
            // 
            this.Path.FillWeight = 150F;
            this.Path.HeaderText = "Path";
            this.Path.Name = "Path";
            this.Path.ReadOnly = true;
            this.Path.Width = 80;
            // 
            // State
            // 
            this.State.FillWeight = 150F;
            this.State.HeaderText = "State";
            this.State.Name = "State";
            this.State.ReadOnly = true;
            // 
            // StateType
            // 
            this.StateType.HeaderText = "Type";
            this.StateType.Name = "StateType";
            this.StateType.ReadOnly = true;
            // 
            // Comment
            // 
            this.Comment.FillWeight = 500F;
            this.Comment.HeaderText = "Comment";
            this.Comment.Name = "Comment";
            this.Comment.ReadOnly = true;
            this.Comment.Width = 500;
            // 
            // cancel_button
            // 
            this.cancel_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cancel_button.Location = new System.Drawing.Point(459, 45);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.Size = new System.Drawing.Size(75, 23);
            this.cancel_button.TabIndex = 17;
            this.cancel_button.Text = "CANCEL";
            this.cancel_button.UseVisualStyleBackColor = true;
            this.cancel_button.Click += new System.EventHandler(this.cancel_button_Click);
            // 
            // ok_button
            // 
            this.ok_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ok_button.Location = new System.Drawing.Point(459, 9);
            this.ok_button.Name = "ok_button";
            this.ok_button.Size = new System.Drawing.Size(75, 23);
            this.ok_button.TabIndex = 16;
            this.ok_button.Text = "OK";
            this.ok_button.UseVisualStyleBackColor = true;
            this.ok_button.Click += new System.EventHandler(this.ok_button_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Font = new System.Drawing.Font("ＭＳ ゴシック", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox1.Location = new System.Drawing.Point(12, 26);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(441, 23);
            this.textBox1.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(190, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(143, 12);
            this.label2.TabIndex = 20;
            this.label2.Text = "グループ内のステートから選択";
            this.label2.Visible = false;
            // 
            // checkBox_incsub
            // 
            this.checkBox_incsub.AutoSize = true;
            this.checkBox_incsub.Location = new System.Drawing.Point(459, 270);
            this.checkBox_incsub.Name = "checkBox_incsub";
            this.checkBox_incsub.Size = new System.Drawing.Size(80, 28);
            this.checkBox_incsub.TabIndex = 23;
            this.checkBox_incsub.Text = "inculde\r\nsub groups";
            this.checkBox_incsub.UseVisualStyleBackColor = true;
            this.checkBox_incsub.CheckedChanged += new System.EventHandler(this.checkBox_incsub_CheckedChanged);
            // 
            // radioButton_alltypes
            // 
            this.radioButton_alltypes.AutoSize = true;
            this.radioButton_alltypes.Checked = true;
            this.radioButton_alltypes.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.radioButton_alltypes.Location = new System.Drawing.Point(6, 24);
            this.radioButton_alltypes.Name = "radioButton_alltypes";
            this.radioButton_alltypes.Size = new System.Drawing.Size(64, 15);
            this.radioButton_alltypes.TabIndex = 23;
            this.radioButton_alltypes.TabStop = true;
            this.radioButton_alltypes.Text = "All Types";
            this.radioButton_alltypes.UseVisualStyleBackColor = true;
            this.radioButton_alltypes.CheckedChanged += new System.EventHandler(this.radioButton_alltypes_CheckedChanged);
            // 
            // radioButton_pass
            // 
            this.radioButton_pass.AutoSize = true;
            this.radioButton_pass.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.radioButton_pass.Location = new System.Drawing.Point(6, 45);
            this.radioButton_pass.Name = "radioButton_pass";
            this.radioButton_pass.Size = new System.Drawing.Size(45, 15);
            this.radioButton_pass.TabIndex = 23;
            this.radioButton_pass.Text = "Pass";
            this.radioButton_pass.UseVisualStyleBackColor = true;
            this.radioButton_pass.CheckedChanged += new System.EventHandler(this.radioButton_pass_CheckedChanged);
            // 
            // radioButton_substart
            // 
            this.radioButton_substart.AutoSize = true;
            this.radioButton_substart.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.radioButton_substart.Location = new System.Drawing.Point(7, 66);
            this.radioButton_substart.Name = "radioButton_substart";
            this.radioButton_substart.Size = new System.Drawing.Size(68, 15);
            this.radioButton_substart.TabIndex = 25;
            this.radioButton_substart.Text = "Sub Start";
            this.radioButton_substart.UseVisualStyleBackColor = true;
            this.radioButton_substart.CheckedChanged += new System.EventHandler(this.radioButton_substart_CheckedChanged);
            // 
            // GroupBox_type
            // 
            this.GroupBox_type.Controls.Add(this.radioButton_substart);
            this.GroupBox_type.Controls.Add(this.radioButton_pass);
            this.GroupBox_type.Controls.Add(this.radioButton_alltypes);
            this.GroupBox_type.Location = new System.Drawing.Point(459, 104);
            this.GroupBox_type.Name = "GroupBox_type";
            this.GroupBox_type.Size = new System.Drawing.Size(81, 98);
            this.GroupBox_type.TabIndex = 26;
            this.GroupBox_type.TabStop = false;
            this.GroupBox_type.Text = "Type";
            // 
            // checkBox_noinput
            // 
            this.checkBox_noinput.AutoSize = true;
            this.checkBox_noinput.Checked = true;
            this.checkBox_noinput.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_noinput.Location = new System.Drawing.Point(459, 224);
            this.checkBox_noinput.Name = "checkBox_noinput";
            this.checkBox_noinput.Size = new System.Drawing.Size(87, 28);
            this.checkBox_noinput.TabIndex = 27;
            this.checkBox_noinput.Text = "Input source\r\nnot defined";
            this.checkBox_noinput.UseVisualStyleBackColor = true;
            this.checkBox_noinput.CheckedChanged += new System.EventHandler(this.checkBox_noinput_CheckedChanged);
            // 
            // AltStateDistinationForm1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 364);
            this.Controls.Add(this.checkBox_noinput);
            this.Controls.Add(this.GroupBox_type);
            this.Controls.Add(this.checkBox_incsub);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cancel_button);
            this.Controls.Add(this.ok_button);
            this.Controls.Add(this.textBox1);
            this.Name = "AltStateDistinationForm1";
            this.Text = "Select A Distination In The Group";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AltStateDistinationForm1_FormClosing);
            this.Load += new System.EventHandler(this.AltStateDistinationForm1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.GroupBox_type.ResumeLayout(false);
            this.GroupBox_type.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.DataGridView dataGridView1;
        public System.Windows.Forms.Button cancel_button;
        public System.Windows.Forms.Button ok_button;
        public System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox checkBox_incsub;
        public System.Windows.Forms.RadioButton radioButton_alltypes;
        public System.Windows.Forms.RadioButton radioButton_pass;
        public System.Windows.Forms.RadioButton radioButton_substart;
        private System.Windows.Forms.DataGridViewTextBoxColumn Path;
        private System.Windows.Forms.DataGridViewTextBoxColumn State;
        private System.Windows.Forms.DataGridViewTextBoxColumn StateType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comment;
        private System.Windows.Forms.GroupBox GroupBox_type;
        private System.Windows.Forms.CheckBox checkBox_noinput;
    }
}