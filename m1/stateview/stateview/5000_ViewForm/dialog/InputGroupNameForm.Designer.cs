namespace stateview._5000_ViewForm.dialog
{
    partial class InputGroupNameForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InputGroupNameForm));
            this.groupname_comboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxComment = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label_help_notice = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // groupname_comboBox
            // 
            this.groupname_comboBox.FormattingEnabled = true;
            this.groupname_comboBox.Location = new System.Drawing.Point(12, 24);
            this.groupname_comboBox.Name = "groupname_comboBox";
            this.groupname_comboBox.Size = new System.Drawing.Size(193, 20);
            this.groupname_comboBox.TabIndex = 0;
            this.groupname_comboBox.SelectedIndexChanged += new System.EventHandler(this.groupname_comboBox_SelectedIndexChanged);
            this.groupname_comboBox.SelectedValueChanged += new System.EventHandler(this.groupname_comboBox_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 12);
            this.label1.TabIndex = 1;
            this.label1.Tag = "ignf_groupname";
            this.label1.Text = "Group Name";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(211, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(211, 38);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "CANCEL";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(191, 12);
            this.label2.TabIndex = 4;
            this.label2.Tag = "ignf_ifyouselect";
            this.label2.Text = "※If you select an existing, combine.";
            // 
            // textBoxComment
            // 
            this.textBoxComment.Location = new System.Drawing.Point(12, 117);
            this.textBoxComment.Multiline = true;
            this.textBoxComment.Name = "textBoxComment";
            this.textBoxComment.Size = new System.Drawing.Size(193, 146);
            this.textBoxComment.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 102);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 6;
            this.label3.Tag = "ignf_cmt";
            this.label3.Text = "Comment";
            // 
            // label_help_notice
            // 
            this.label_help_notice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_help_notice.AutoSize = true;
            this.label_help_notice.BackColor = System.Drawing.Color.Silver;
            this.label_help_notice.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_help_notice.ForeColor = System.Drawing.Color.White;
            this.label_help_notice.Location = new System.Drawing.Point(285, 1);
            this.label_help_notice.Margin = new System.Windows.Forms.Padding(0);
            this.label_help_notice.Name = "label_help_notice";
            this.label_help_notice.Size = new System.Drawing.Size(13, 14);
            this.label_help_notice.TabIndex = 19;
            this.label_help_notice.Text = "?";
            this.label_help_notice.Click += new System.EventHandler(this.label_help_notice_Click);
            // 
            // InputGroupNameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(297, 288);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxComment);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupname_comboBox);
            this.Controls.Add(this.label_help_notice);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "InputGroupNameForm";
            this.Tag = "ignf_title";
            this.Text = "Input Group Name";
            this.Load += new System.EventHandler(this.InputGroupNameForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox groupname_comboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxComment;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label_help_notice;
    }
}