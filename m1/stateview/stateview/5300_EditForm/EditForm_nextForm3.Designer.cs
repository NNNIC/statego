namespace stateview._5300_EditForm
{
    partial class EditForm_nextForm3
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditForm_nextForm3));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Path = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.State = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Comment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cancel_button = new System.Windows.Forms.Button();
            this.ok_button = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label_centerfocus = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.radioButtonNone = new System.Windows.Forms.RadioButton();
            this.radioButtonSubStart = new System.Windows.Forms.RadioButton();
            this.radioButtonSubReturn = new System.Windows.Forms.RadioButton();
            this.radioButtonBaseState = new System.Windows.Forms.RadioButton();
            this.radioButtonGosub = new System.Windows.Forms.RadioButton();
            this.focus_button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
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
            this.Comment});
            this.dataGridView1.Location = new System.Drawing.Point(12, 52);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.Size = new System.Drawing.Size(441, 300);
            this.dataGridView1.TabIndex = 11;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
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
            this.State.Width = 150;
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
            this.cancel_button.Location = new System.Drawing.Point(459, 42);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.Size = new System.Drawing.Size(75, 23);
            this.cancel_button.TabIndex = 10;
            this.cancel_button.Text = "CANCEL";
            this.cancel_button.UseVisualStyleBackColor = true;
            this.cancel_button.Click += new System.EventHandler(this.cancel_button_Click);
            // 
            // ok_button
            // 
            this.ok_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ok_button.Location = new System.Drawing.Point(459, 6);
            this.ok_button.Name = "ok_button";
            this.ok_button.Size = new System.Drawing.Size(75, 23);
            this.ok_button.TabIndex = 9;
            this.ok_button.Text = "OK";
            this.ok_button.UseVisualStyleBackColor = true;
            this.ok_button.Click += new System.EventHandler(this.ok_button_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox1.Location = new System.Drawing.Point(12, 23);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(441, 23);
            this.textBox1.TabIndex = 8;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(459, 106);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 12;
            this.button1.Tag = "sns_clear";
            this.button1.Text = "CLEAR";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label_centerfocus
            // 
            this.label_centerfocus.AutoSize = true;
            this.label_centerfocus.Font = new System.Drawing.Font("MS UI Gothic", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_centerfocus.ForeColor = System.Drawing.Color.Red;
            this.label_centerfocus.Location = new System.Drawing.Point(219, 6);
            this.label_centerfocus.Name = "label_centerfocus";
            this.label_centerfocus.Size = new System.Drawing.Size(167, 12);
            this.label_centerfocus.TabIndex = 13;
            this.label_centerfocus.Tag = "sns_centerfocus";
            this.label_centerfocus.Text = "View and focus state and close";
            this.label_centerfocus.Visible = false;
            this.label_centerfocus.Click += new System.EventHandler(this.label_centerfocus_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(91, 12);
            this.label1.TabIndex = 14;
            this.label1.Tag = "sns_diststate";
            this.label1.Text = "Distination State";
            // 
            // radioButtonNone
            // 
            this.radioButtonNone.AutoSize = true;
            this.radioButtonNone.Checked = true;
            this.radioButtonNone.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.radioButtonNone.Location = new System.Drawing.Point(459, 246);
            this.radioButtonNone.Name = "radioButtonNone";
            this.radioButtonNone.Size = new System.Drawing.Size(47, 15);
            this.radioButtonNone.TabIndex = 15;
            this.radioButtonNone.TabStop = true;
            this.radioButtonNone.Text = "none";
            this.radioButtonNone.UseVisualStyleBackColor = true;
            this.radioButtonNone.CheckedChanged += new System.EventHandler(this.radioButtonNone_CheckedChanged);
            // 
            // radioButtonSubStart
            // 
            this.radioButtonSubStart.AutoSize = true;
            this.radioButtonSubStart.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.radioButtonSubStart.Location = new System.Drawing.Point(459, 288);
            this.radioButtonSubStart.Name = "radioButtonSubStart";
            this.radioButtonSubStart.Size = new System.Drawing.Size(83, 15);
            this.radioButtonSubStart.TabIndex = 16;
            this.radioButtonSubStart.Text = "SUB-START";
            this.radioButtonSubStart.UseVisualStyleBackColor = true;
            this.radioButtonSubStart.CheckedChanged += new System.EventHandler(this.radioButtonSubStart_CheckedChanged);
            // 
            // radioButtonSubReturn
            // 
            this.radioButtonSubReturn.AutoSize = true;
            this.radioButtonSubReturn.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.radioButtonSubReturn.Location = new System.Drawing.Point(459, 310);
            this.radioButtonSubReturn.Name = "radioButtonSubReturn";
            this.radioButtonSubReturn.Size = new System.Drawing.Size(90, 15);
            this.radioButtonSubReturn.TabIndex = 17;
            this.radioButtonSubReturn.Text = "SUB-RETURN";
            this.radioButtonSubReturn.UseVisualStyleBackColor = true;
            this.radioButtonSubReturn.CheckedChanged += new System.EventHandler(this.radioButtonSubReturn_CheckedChanged);
            // 
            // radioButtonBaseState
            // 
            this.radioButtonBaseState.AutoSize = true;
            this.radioButtonBaseState.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.radioButtonBaseState.Location = new System.Drawing.Point(459, 331);
            this.radioButtonBaseState.Name = "radioButtonBaseState";
            this.radioButtonBaseState.Size = new System.Drawing.Size(78, 15);
            this.radioButtonBaseState.TabIndex = 18;
            this.radioButtonBaseState.Text = "Base-State";
            this.radioButtonBaseState.UseVisualStyleBackColor = true;
            this.radioButtonBaseState.CheckedChanged += new System.EventHandler(this.radioButtonBaseState_CheckedChanged);
            // 
            // radioButtonGosub
            // 
            this.radioButtonGosub.AutoSize = true;
            this.radioButtonGosub.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.radioButtonGosub.Location = new System.Drawing.Point(459, 267);
            this.radioButtonGosub.Name = "radioButtonGosub";
            this.radioButtonGosub.Size = new System.Drawing.Size(59, 15);
            this.radioButtonGosub.TabIndex = 19;
            this.radioButtonGosub.Text = "GOSUB";
            this.radioButtonGosub.UseVisualStyleBackColor = true;
            this.radioButtonGosub.CheckedChanged += new System.EventHandler(this.radioButtonGosub_CheckedChanged);
            // 
            // focus_button
            // 
            this.focus_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.focus_button.Location = new System.Drawing.Point(459, 165);
            this.focus_button.Name = "focus_button";
            this.focus_button.Size = new System.Drawing.Size(75, 46);
            this.focus_button.TabIndex = 20;
            this.focus_button.Tag = "sns_centerfocus";
            this.focus_button.Text = "Focus\r\nand\r\nClose";
            this.focus_button.UseVisualStyleBackColor = true;
            this.focus_button.Visible = false;
            this.focus_button.Click += new System.EventHandler(this.focus_button_Click);
            // 
            // EditForm_nextForm3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 364);
            this.Controls.Add(this.focus_button);
            this.Controls.Add(this.radioButtonGosub);
            this.Controls.Add(this.radioButtonBaseState);
            this.Controls.Add(this.radioButtonSubReturn);
            this.Controls.Add(this.radioButtonSubStart);
            this.Controls.Add(this.radioButtonNone);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label_centerfocus);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.cancel_button);
            this.Controls.Add(this.ok_button);
            this.Controls.Add(this.textBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditForm_nextForm3";
            this.Tag = "sns_selnextstate";
            this.Text = "Select Next State";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditForm_nextForm3_FormClosing);
            this.Load += new System.EventHandler(this.EditForm_nextForm3_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridViewTextBoxColumn Path;
        private System.Windows.Forms.DataGridViewTextBoxColumn State;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comment;
        public System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label_centerfocus;
        public System.Windows.Forms.DataGridView dataGridView1;
        public System.Windows.Forms.Button cancel_button;
        public System.Windows.Forms.Button ok_button;
        public System.Windows.Forms.Button button1;
        public System.Windows.Forms.RadioButton radioButtonNone;
        public System.Windows.Forms.RadioButton radioButtonSubStart;
        public System.Windows.Forms.RadioButton radioButtonSubReturn;
        public System.Windows.Forms.RadioButton radioButtonBaseState;
        public System.Windows.Forms.RadioButton radioButtonGosub;
        public System.Windows.Forms.Button focus_button;
    }
}