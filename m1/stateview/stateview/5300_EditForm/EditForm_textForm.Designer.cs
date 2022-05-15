namespace stateview._5300_EditForm
{
    partial class EditForm_textForm
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
            this.ok_button = new System.Windows.Forms.Button();
            this.cancel_button = new System.Windows.Forms.Button();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageComment = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxPageComment = new System.Windows.Forms.TextBox();
            this.groupBoxRef = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxRef = new System.Windows.Forms.TextBox();
            this.tabPageHelp = new System.Windows.Forms.TabPage();
            this.textBoxPageHelp = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pageSetupDialog1 = new System.Windows.Forms.PageSetupDialog();
            this.textBox_method = new System.Windows.Forms.TextBox();
            this.button_next = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label_help_3 = new System.Windows.Forms.Label();
            this.label_use_ext_editor = new System.Windows.Forms.Label();
            this.button_prev = new System.Windows.Forms.Button();
            this.button_select = new System.Windows.Forms.Button();
            this.label_stateexpand = new System.Windows.Forms.Label();
            this.scintillaBox = new ScintillaBox();
            this.tabControl.SuspendLayout();
            this.tabPageComment.SuspendLayout();
            this.groupBoxRef.SuspendLayout();
            this.tabPageHelp.SuspendLayout();
            this.SuspendLayout();
            // 
            // ok_button
            // 
            this.ok_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ok_button.Location = new System.Drawing.Point(596, 4);
            this.ok_button.Margin = new System.Windows.Forms.Padding(4);
            this.ok_button.Name = "ok_button";
            this.ok_button.Size = new System.Drawing.Size(135, 31);
            this.ok_button.TabIndex = 1;
            this.ok_button.Text = "OK";
            this.ok_button.UseVisualStyleBackColor = true;
            this.ok_button.Click += new System.EventHandler(this.ok_button_Click);
            // 
            // cancel_button
            // 
            this.cancel_button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cancel_button.Location = new System.Drawing.Point(596, 43);
            this.cancel_button.Margin = new System.Windows.Forms.Padding(4);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.Size = new System.Drawing.Size(135, 31);
            this.cancel_button.TabIndex = 2;
            this.cancel_button.Text = "CANCEL";
            this.cancel_button.UseVisualStyleBackColor = true;
            this.cancel_button.Click += new System.EventHandler(this.cancel_button_Click);
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPageComment);
            this.tabControl.Controls.Add(this.tabPageHelp);
            this.tabControl.Location = new System.Drawing.Point(3, 169);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(586, 187);
            this.tabControl.TabIndex = 4;
            // 
            // tabPageComment
            // 
            this.tabPageComment.Controls.Add(this.label1);
            this.tabPageComment.Controls.Add(this.textBoxPageComment);
            this.tabPageComment.Controls.Add(this.groupBoxRef);
            this.tabPageComment.Location = new System.Drawing.Point(4, 26);
            this.tabPageComment.Name = "tabPageComment";
            this.tabPageComment.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageComment.Size = new System.Drawing.Size(578, 157);
            this.tabPageComment.TabIndex = 0;
            this.tabPageComment.Tag = "edtf_comment";
            this.tabPageComment.Text = "Comment";
            this.tabPageComment.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 130);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 16);
            this.label1.TabIndex = 2;
            this.label1.Tag = "edtf_ref";
            this.label1.Text = "ref";
            // 
            // textBoxPageComment
            // 
            this.textBoxPageComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPageComment.Enabled = false;
            this.textBoxPageComment.Location = new System.Drawing.Point(4, 6);
            this.textBoxPageComment.Multiline = true;
            this.textBoxPageComment.Name = "textBoxPageComment";
            this.textBoxPageComment.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxPageComment.Size = new System.Drawing.Size(568, 111);
            this.textBoxPageComment.TabIndex = 0;
            // 
            // groupBoxRef
            // 
            this.groupBoxRef.Controls.Add(this.button1);
            this.groupBoxRef.Controls.Add(this.textBoxRef);
            this.groupBoxRef.Enabled = false;
            this.groupBoxRef.Location = new System.Drawing.Point(4, 113);
            this.groupBoxRef.Name = "groupBoxRef";
            this.groupBoxRef.Size = new System.Drawing.Size(544, 43);
            this.groupBoxRef.TabIndex = 4;
            this.groupBoxRef.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(462, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 3;
            this.button1.Tag = "edtf_open";
            this.button1.Text = "OPEN";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxRef
            // 
            this.textBoxRef.Location = new System.Drawing.Point(40, 14);
            this.textBoxRef.Name = "textBoxRef";
            this.textBoxRef.Size = new System.Drawing.Size(416, 23);
            this.textBoxRef.TabIndex = 1;
            // 
            // tabPageHelp
            // 
            this.tabPageHelp.Controls.Add(this.textBoxPageHelp);
            this.tabPageHelp.Location = new System.Drawing.Point(4, 26);
            this.tabPageHelp.Name = "tabPageHelp";
            this.tabPageHelp.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageHelp.Size = new System.Drawing.Size(578, 157);
            this.tabPageHelp.TabIndex = 1;
            this.tabPageHelp.Tag = "edtf_help";
            this.tabPageHelp.Text = "Help";
            this.tabPageHelp.UseVisualStyleBackColor = true;
            // 
            // textBoxPageHelp
            // 
            this.textBoxPageHelp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPageHelp.Location = new System.Drawing.Point(6, 6);
            this.textBoxPageHelp.Multiline = true;
            this.textBoxPageHelp.Name = "textBoxPageHelp";
            this.textBoxPageHelp.ReadOnly = true;
            this.textBoxPageHelp.Size = new System.Drawing.Size(566, 148);
            this.textBoxPageHelp.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // textBox_method
            // 
            this.textBox_method.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_method.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_method.Location = new System.Drawing.Point(595, 159);
            this.textBox_method.Multiline = true;
            this.textBox_method.Name = "textBox_method";
            this.textBox_method.ReadOnly = true;
            this.textBox_method.Size = new System.Drawing.Size(137, 193);
            this.textBox_method.TabIndex = 6;
            // 
            // button_next
            // 
            this.button_next.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_next.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_next.Location = new System.Drawing.Point(596, 82);
            this.button_next.Margin = new System.Windows.Forms.Padding(4);
            this.button_next.Name = "button_next";
            this.button_next.Size = new System.Drawing.Size(64, 31);
            this.button_next.TabIndex = 7;
            this.button_next.Text = "NEXT";
            this.button_next.UseVisualStyleBackColor = true;
            this.button_next.Click += new System.EventHandler(this.button_next_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 4);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 16);
            this.label2.TabIndex = 8;
            this.label2.Tag = "edtf_value";
            this.label2.Text = "Value";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(55, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 16);
            this.label3.TabIndex = 9;
            this.label3.Tag = "edtf_clear";
            this.label3.Text = "clear";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label_help_3
            // 
            this.label_help_3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_help_3.AutoSize = true;
            this.label_help_3.BackColor = System.Drawing.Color.Silver;
            this.label_help_3.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_help_3.ForeColor = System.Drawing.Color.White;
            this.label_help_3.Location = new System.Drawing.Point(731, 0);
            this.label_help_3.Margin = new System.Windows.Forms.Padding(0);
            this.label_help_3.Name = "label_help_3";
            this.label_help_3.Size = new System.Drawing.Size(13, 14);
            this.label_help_3.TabIndex = 39;
            this.label_help_3.Text = "?";
            this.label_help_3.Click += new System.EventHandler(this.label_help_3_Click);
            // 
            // label_use_ext_editor
            // 
            this.label_use_ext_editor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_use_ext_editor.AutoSize = true;
            this.label_use_ext_editor.Font = new System.Drawing.Font("MS UI Gothic", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_use_ext_editor.ForeColor = System.Drawing.Color.Blue;
            this.label_use_ext_editor.Location = new System.Drawing.Point(436, 4);
            this.label_use_ext_editor.Name = "label_use_ext_editor";
            this.label_use_ext_editor.Size = new System.Drawing.Size(156, 16);
            this.label_use_ext_editor.TabIndex = 40;
            this.label_use_ext_editor.Tag = "eftf_use_ext_editor";
            this.label_use_ext_editor.Text = "Edit In External Editor";
            this.label_use_ext_editor.Click += new System.EventHandler(this.label_use_external_editor_Click);
            // 
            // button_prev
            // 
            this.button_prev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_prev.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_prev.Location = new System.Drawing.Point(667, 82);
            this.button_prev.Margin = new System.Windows.Forms.Padding(4);
            this.button_prev.Name = "button_prev";
            this.button_prev.Size = new System.Drawing.Size(64, 31);
            this.button_prev.TabIndex = 41;
            this.button_prev.Text = "PREV";
            this.button_prev.UseVisualStyleBackColor = true;
            this.button_prev.Click += new System.EventHandler(this.button_prev_Click);
            // 
            // button_select
            // 
            this.button_select.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_select.Location = new System.Drawing.Point(597, 121);
            this.button_select.Margin = new System.Windows.Forms.Padding(4);
            this.button_select.Name = "button_select";
            this.button_select.Size = new System.Drawing.Size(135, 31);
            this.button_select.TabIndex = 42;
            this.button_select.Text = "SELECT";
            this.button_select.UseVisualStyleBackColor = true;
            this.button_select.Click += new System.EventHandler(this.button_select_Click);
            // 
            // label_stateexpand
            // 
            this.label_stateexpand.AutoSize = true;
            this.label_stateexpand.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_stateexpand.ForeColor = System.Drawing.Color.Black;
            this.label_stateexpand.Location = new System.Drawing.Point(102, 4);
            this.label_stateexpand.Name = "label_stateexpand";
            this.label_stateexpand.Size = new System.Drawing.Size(181, 16);
            this.label_stateexpand.TabIndex = 43;
            this.label_stateexpand.Tag = "edtf_stateexpand";
            this.label_stateexpand.Text = "expand or revert [[state]]";
            this.label_stateexpand.Click += new System.EventHandler(this.label_stateexpand_Click);
            // 
            // scintillaBox
            // 
            this.scintillaBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scintillaBox.Location = new System.Drawing.Point(7, 23);
            this.scintillaBox.Name = "scintillaBox";
            this.scintillaBox.Size = new System.Drawing.Size(578, 139);
            this.scintillaBox.TabIndex = 44;
            // 
            // EditForm_textForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 359);
            this.Controls.Add(this.scintillaBox);
            this.Controls.Add(this.label_stateexpand);
            this.Controls.Add(this.button_select);
            this.Controls.Add(this.button_prev);
            this.Controls.Add(this.label_use_ext_editor);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button_next);
            this.Controls.Add(this.textBox_method);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.cancel_button);
            this.Controls.Add(this.ok_button);
            this.Controls.Add(this.label_help_3);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "EditForm_textForm";
            this.Text = "Edit Text";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditForm_textForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.EditForm_textForm_FormClosed);
            this.Load += new System.EventHandler(this.EditForm_textForm_Load);
            this.Resize += new System.EventHandler(this.EditForm_textForm_Resize);
            this.tabControl.ResumeLayout(false);
            this.tabPageComment.ResumeLayout(false);
            this.tabPageComment.PerformLayout();
            this.groupBoxRef.ResumeLayout(false);
            this.groupBoxRef.PerformLayout();
            this.tabPageHelp.ResumeLayout(false);
            this.tabPageHelp.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button ok_button;
        private System.Windows.Forms.Button cancel_button;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageComment;
        private System.Windows.Forms.TabPage tabPageHelp;
        private System.Windows.Forms.TextBox textBoxPageComment;
        private System.Windows.Forms.TextBox textBoxPageHelp;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBoxRef;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBoxRef;
        private System.Windows.Forms.PageSetupDialog pageSetupDialog1;
        private System.Windows.Forms.TextBox textBox_method;
        private System.Windows.Forms.Button button_next;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label_help_3;
        private System.Windows.Forms.Label label_use_ext_editor;
        private System.Windows.Forms.Button button_prev;
        private System.Windows.Forms.Button button_select;
        private System.Windows.Forms.Label label_stateexpand;
        private ScintillaBox scintillaBox;
    }
}