namespace stateview._5300_EditForm
{
    partial class EditForm_stateForm
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
            this.components = new System.ComponentModel.Container();
            this.textBox_name_wo_prefix = new System.Windows.Forms.TextBox();
            this.cancel_button = new System.Windows.Forms.Button();
            this.ok_button = new System.Windows.Forms.Button();
            this.textBoxPageComment = new System.Windows.Forms.TextBox();
            this.textBoxPageHelp = new System.Windows.Forms.TextBox();
            this.textBoxRef = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBoxRef = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label_State = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label_numberd = new System.Windows.Forms.Label();
            this.label_help_3 = new System.Windows.Forms.Label();
            this.label_statename = new System.Windows.Forms.Label();
            this.textBox_prefix = new System.Windows.Forms.TextBox();
            this.textBox_BG = new System.Windows.Forms.TextBox();
            this.button_prefix_chg = new System.Windows.Forms.Button();
            this.label_edit_prefix = new System.Windows.Forms.Label();
            this.label_statename_notice = new System.Windows.Forms.Label();
            this.groupBoxRef.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox_name_wo_prefix
            // 
            this.textBox_name_wo_prefix.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_name_wo_prefix.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_name_wo_prefix.Location = new System.Drawing.Point(36, 31);
            this.textBox_name_wo_prefix.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_name_wo_prefix.Name = "textBox_name_wo_prefix";
            this.textBox_name_wo_prefix.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_name_wo_prefix.Size = new System.Drawing.Size(390, 16);
            this.textBox_name_wo_prefix.TabIndex = 1;
            this.textBox_name_wo_prefix.Text = "HOGE";
            this.textBox_name_wo_prefix.WordWrap = false;
            this.textBox_name_wo_prefix.TextChanged += new System.EventHandler(this.textBox_name_wo_prefix_TextChanged);
            // 
            // cancel_button
            // 
            this.cancel_button.Location = new System.Drawing.Point(446, 52);
            this.cancel_button.Margin = new System.Windows.Forms.Padding(4);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.Size = new System.Drawing.Size(95, 31);
            this.cancel_button.TabIndex = 6;
            this.cancel_button.Text = "CANCEL";
            this.cancel_button.UseVisualStyleBackColor = true;
            this.cancel_button.Click += new System.EventHandler(this.cancel_button_Click);
            // 
            // ok_button
            // 
            this.ok_button.Location = new System.Drawing.Point(446, 13);
            this.ok_button.Margin = new System.Windows.Forms.Padding(4);
            this.ok_button.Name = "ok_button";
            this.ok_button.Size = new System.Drawing.Size(96, 31);
            this.ok_button.TabIndex = 5;
            this.ok_button.Text = "OK";
            this.ok_button.UseVisualStyleBackColor = true;
            this.ok_button.Click += new System.EventHandler(this.ok_button_Click);
            // 
            // textBoxPageComment
            // 
            this.textBoxPageComment.Location = new System.Drawing.Point(5, 111);
            this.textBoxPageComment.Multiline = true;
            this.textBoxPageComment.Name = "textBoxPageComment";
            this.textBoxPageComment.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxPageComment.Size = new System.Drawing.Size(537, 125);
            this.textBoxPageComment.TabIndex = 0;
            // 
            // textBoxPageHelp
            // 
            this.textBoxPageHelp.Location = new System.Drawing.Point(5, 303);
            this.textBoxPageHelp.Multiline = true;
            this.textBoxPageHelp.Name = "textBoxPageHelp";
            this.textBoxPageHelp.ReadOnly = true;
            this.textBoxPageHelp.Size = new System.Drawing.Size(537, 126);
            this.textBoxPageHelp.TabIndex = 0;
            // 
            // textBoxRef
            // 
            this.textBoxRef.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxRef.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxRef.Location = new System.Drawing.Point(43, 13);
            this.textBoxRef.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxRef.Name = "textBoxRef";
            this.textBoxRef.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxRef.Size = new System.Drawing.Size(407, 23);
            this.textBoxRef.TabIndex = 7;
            this.textBoxRef.WordWrap = false;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(456, 14);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(73, 23);
            this.button1.TabIndex = 8;
            this.button1.Tag = "edsf_open";
            this.button1.Text = "Open";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBoxRef
            // 
            this.groupBoxRef.Controls.Add(this.label1);
            this.groupBoxRef.Controls.Add(this.button1);
            this.groupBoxRef.Controls.Add(this.textBoxRef);
            this.groupBoxRef.Location = new System.Drawing.Point(5, 242);
            this.groupBoxRef.Name = "groupBoxRef";
            this.groupBoxRef.Size = new System.Drawing.Size(536, 41);
            this.groupBoxRef.TabIndex = 9;
            this.groupBoxRef.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(19, 12);
            this.label1.TabIndex = 9;
            this.label1.Tag = "edsf_ref";
            this.label1.Text = "ref";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 10;
            this.label2.Tag = "edsf_comment";
            this.label2.Text = "Comment";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 288);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 12);
            this.label3.TabIndex = 11;
            this.label3.Tag = "edsf_help";
            this.label3.Text = "Help";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 12);
            this.label4.TabIndex = 12;
            this.label4.Tag = "edsf_state";
            this.label4.Text = "State";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label_State
            // 
            this.label_State.AutoSize = true;
            this.label_State.Font = new System.Drawing.Font("MS UI Gothic", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_State.ForeColor = System.Drawing.Color.Maroon;
            this.label_State.Location = new System.Drawing.Point(57, 54);
            this.label_State.Name = "label_State";
            this.label_State.Size = new System.Drawing.Size(64, 12);
            this.label_State.TabIndex = 13;
            this.label_State.Tag = "edsf_prfxstate";
            this.label_State.Text = "S_・・State";
            this.label_State.Click += new System.EventHandler(this.label_State_Click);
            this.label_State.DoubleClick += new System.EventHandler(this.label_State_DoubleClick);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("MS UI Gothic", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label5.ForeColor = System.Drawing.Color.Maroon;
            this.label5.Location = new System.Drawing.Point(131, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 12);
            this.label5.TabIndex = 14;
            this.label5.Tag = "edsf_prfxembed";
            this.label5.Text = "E_・・Embed code";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            this.label5.DoubleClick += new System.EventHandler(this.label_Embed_DoubleClick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("MS UI Gothic", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.ForeColor = System.Drawing.Color.Maroon;
            this.label6.Location = new System.Drawing.Point(243, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 12);
            this.label6.TabIndex = 15;
            this.label6.Tag = "edsf_prfxcmt";
            this.label6.Text = "C_・・Comment out";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            this.label6.DoubleClick += new System.EventHandler(this.label_Comment_DubleClick);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("MS UI Gothic", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(62, 94);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 12);
            this.label7.TabIndex = 16;
            this.label7.Tag = "edsf_clear";
            this.label7.Text = "clear";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            this.label7.DoubleClick += new System.EventHandler(this.label7_DoubleClick);
            // 
            // label_numberd
            // 
            this.label_numberd.AutoSize = true;
            this.label_numberd.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_numberd.Location = new System.Drawing.Point(382, 54);
            this.label_numberd.Name = "label_numberd";
            this.label_numberd.Size = new System.Drawing.Size(51, 13);
            this.label_numberd.TabIndex = 17;
            this.label_numberd.Tag = "edsf_prfxnum";
            this.label_numberd.Text = "Numberd";
            this.label_numberd.Click += new System.EventHandler(this.label_numberd_Click);
            // 
            // label_help_3
            // 
            this.label_help_3.AutoSize = true;
            this.label_help_3.BackColor = System.Drawing.Color.Silver;
            this.label_help_3.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_help_3.ForeColor = System.Drawing.Color.White;
            this.label_help_3.Location = new System.Drawing.Point(541, 0);
            this.label_help_3.Margin = new System.Windows.Forms.Padding(0);
            this.label_help_3.Name = "label_help_3";
            this.label_help_3.Size = new System.Drawing.Size(13, 14);
            this.label_help_3.TabIndex = 38;
            this.label_help_3.Text = "?";
            this.label_help_3.Click += new System.EventHandler(this.label_help_3_Click);
            // 
            // label_statename
            // 
            this.label_statename.AutoSize = true;
            this.label_statename.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_statename.Location = new System.Drawing.Point(64, 6);
            this.label_statename.Name = "label_statename";
            this.label_statename.Size = new System.Drawing.Size(52, 16);
            this.label_statename.TabIndex = 39;
            this.label_statename.Text = "label8";
            this.label_statename.Click += new System.EventHandler(this.label_statename_Click);
            // 
            // textBox_prefix
            // 
            this.textBox_prefix.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox_prefix.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_prefix.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_prefix.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.textBox_prefix.Location = new System.Drawing.Point(7, 31);
            this.textBox_prefix.Name = "textBox_prefix";
            this.textBox_prefix.ReadOnly = true;
            this.textBox_prefix.Size = new System.Drawing.Size(25, 16);
            this.textBox_prefix.TabIndex = 40;
            this.textBox_prefix.Text = "S_";
            this.textBox_prefix.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_prefix.TextChanged += new System.EventHandler(this.textBox_prefix_TextChanged);
            // 
            // textBox_BG
            // 
            this.textBox_BG.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox_BG.Font = new System.Drawing.Font("MS UI Gothic", 13F);
            this.textBox_BG.Location = new System.Drawing.Point(5, 27);
            this.textBox_BG.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_BG.Name = "textBox_BG";
            this.textBox_BG.ReadOnly = true;
            this.textBox_BG.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox_BG.Size = new System.Drawing.Size(433, 25);
            this.textBox_BG.TabIndex = 41;
            this.textBox_BG.WordWrap = false;
            // 
            // button_prefix_chg
            // 
            this.button_prefix_chg.BackColor = System.Drawing.Color.DarkRed;
            this.button_prefix_chg.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_prefix_chg.ForeColor = System.Drawing.Color.White;
            this.button_prefix_chg.Location = new System.Drawing.Point(2, 45);
            this.button_prefix_chg.Margin = new System.Windows.Forms.Padding(0);
            this.button_prefix_chg.Name = "button_prefix_chg";
            this.button_prefix_chg.Size = new System.Drawing.Size(40, 25);
            this.button_prefix_chg.TabIndex = 42;
            this.button_prefix_chg.Text = "Chg";
            this.button_prefix_chg.UseVisualStyleBackColor = false;
            this.button_prefix_chg.Click += new System.EventHandler(this.button_prefix_chg_Click);
            // 
            // label_edit_prefix
            // 
            this.label_edit_prefix.AutoSize = true;
            this.label_edit_prefix.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_edit_prefix.Location = new System.Drawing.Point(5, 69);
            this.label_edit_prefix.Name = "label_edit_prefix";
            this.label_edit_prefix.Size = new System.Drawing.Size(46, 12);
            this.label_edit_prefix.TabIndex = 43;
            this.label_edit_prefix.Text = "Not edit";
            this.label_edit_prefix.Click += new System.EventHandler(this.label_edit_prefix_Click);
            // 
            // label_statename_notice
            // 
            this.label_statename_notice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_statename_notice.AutoSize = true;
            this.label_statename_notice.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_statename_notice.ForeColor = System.Drawing.Color.Maroon;
            this.label_statename_notice.Location = new System.Drawing.Point(292, 0);
            this.label_statename_notice.Name = "label_statename_notice";
            this.label_statename_notice.Size = new System.Drawing.Size(146, 11);
            this.label_statename_notice.TabIndex = 44;
            this.label_statename_notice.Tag = "efsf_clipcopy";
            this.label_statename_notice.Text = "左のステート名クリックするとコピー";
            this.label_statename_notice.Visible = false;
            // 
            // EditForm_stateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 441);
            this.Controls.Add(this.label_statename);
            this.Controls.Add(this.label_statename_notice);
            this.Controls.Add(this.label_edit_prefix);
            this.Controls.Add(this.textBox_name_wo_prefix);
            this.Controls.Add(this.textBox_prefix);
            this.Controls.Add(this.label_numberd);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label_State);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBoxRef);
            this.Controls.Add(this.textBoxPageHelp);
            this.Controls.Add(this.textBoxPageComment);
            this.Controls.Add(this.cancel_button);
            this.Controls.Add(this.ok_button);
            this.Controls.Add(this.label_help_3);
            this.Controls.Add(this.textBox_BG);
            this.Controls.Add(this.button_prefix_chg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "EditForm_stateForm";
            this.Tag = "edsf_title";
            this.Text = "Edit State";
            this.Load += new System.EventHandler(this.EditForm_stateForm_Load);
            this.groupBoxRef.ResumeLayout(false);
            this.groupBoxRef.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox textBox_name_wo_prefix;
        private System.Windows.Forms.Button cancel_button;
        private System.Windows.Forms.Button ok_button;
        private System.Windows.Forms.TextBox textBoxPageComment;
        private System.Windows.Forms.TextBox textBoxPageHelp;
        public System.Windows.Forms.TextBox textBoxRef;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBoxRef;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label_State;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label_numberd;
        private System.Windows.Forms.Label label_help_3;
        private System.Windows.Forms.Label label_statename;
        private System.Windows.Forms.TextBox textBox_prefix;
        public System.Windows.Forms.TextBox textBox_BG;
        private System.Windows.Forms.Button button_prefix_chg;
        private System.Windows.Forms.Label label_edit_prefix;
        private System.Windows.Forms.Label label_statename_notice;
    }
}