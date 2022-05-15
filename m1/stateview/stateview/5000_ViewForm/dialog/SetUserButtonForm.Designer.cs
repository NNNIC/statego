namespace stateview._5000_ViewForm.dialog
{
    partial class SetUserButtonForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetUserButtonForm));
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxCommand = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.buttonOpenFile = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxTitle = new System.Windows.Forms.TextBox();
            this.checkBoxCallAfter = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label_help_5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            this.buttonCancel.Location = new System.Drawing.Point(520, 41);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(111, 23);
            this.buttonCancel.TabIndex = 13;
            this.buttonCancel.Text = "CANCEL";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // buttonOK
            // 
            this.buttonOK.Location = new System.Drawing.Point(520, 12);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(111, 23);
            this.buttonOK.TabIndex = 12;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 12);
            this.label1.TabIndex = 11;
            this.label1.Tag = "ucus_command";
            this.label1.Text = "Command";
            // 
            // textBoxCommand
            // 
            this.textBoxCommand.Location = new System.Drawing.Point(70, 54);
            this.textBoxCommand.Name = "textBoxCommand";
            this.textBoxCommand.Size = new System.Drawing.Size(429, 19);
            this.textBoxCommand.TabIndex = 10;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // buttonOpenFile
            // 
            this.buttonOpenFile.Location = new System.Drawing.Point(70, 81);
            this.buttonOpenFile.Name = "buttonOpenFile";
            this.buttonOpenFile.Size = new System.Drawing.Size(75, 23);
            this.buttonOpenFile.TabIndex = 14;
            this.buttonOpenFile.Tag = "ucus_openfile";
            this.buttonOpenFile.Text = "Open File Dialog";
            this.buttonOpenFile.UseVisualStyleBackColor = true;
            this.buttonOpenFile.Click += new System.EventHandler(this.buttonOpenFile_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.DarkRed;
            this.label2.Location = new System.Drawing.Point(68, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(417, 12);
            this.label2.TabIndex = 15;
            this.label2.Tag = "ucus_pathexp";
            this.label2.Text = "※ This tool will save the relative path of the command from the document path.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 17);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 12);
            this.label3.TabIndex = 16;
            this.label3.Tag = "ucus_title";
            this.label3.Text = "Title";
            // 
            // textBoxTitle
            // 
            this.textBoxTitle.Location = new System.Drawing.Point(70, 14);
            this.textBoxTitle.Name = "textBoxTitle";
            this.textBoxTitle.Size = new System.Drawing.Size(153, 19);
            this.textBoxTitle.TabIndex = 17;
            // 
            // checkBoxCallAfter
            // 
            this.checkBoxCallAfter.AutoSize = true;
            this.checkBoxCallAfter.Location = new System.Drawing.Point(70, 160);
            this.checkBoxCallAfter.Name = "checkBoxCallAfter";
            this.checkBoxCallAfter.Size = new System.Drawing.Size(129, 16);
            this.checkBoxCallAfter.TabIndex = 18;
            this.checkBoxCallAfter.Tag = "ucus_callafter";
            this.checkBoxCallAfter.Text = "Call after converting";
            this.checkBoxCallAfter.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.DarkRed;
            this.label4.Location = new System.Drawing.Point(86, 187);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(418, 12);
            this.label4.TabIndex = 19;
            this.label4.Tag = "ucus_callafterexp";
            this.label4.Text = "※ This command will be called right after converting a state machine to source.";
            // 
            // label_help_5
            // 
            this.label_help_5.AutoSize = true;
            this.label_help_5.BackColor = System.Drawing.Color.Silver;
            this.label_help_5.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_help_5.ForeColor = System.Drawing.Color.White;
            this.label_help_5.Location = new System.Drawing.Point(629, 0);
            this.label_help_5.Margin = new System.Windows.Forms.Padding(0);
            this.label_help_5.Name = "label_help_5";
            this.label_help_5.Size = new System.Drawing.Size(13, 14);
            this.label_help_5.TabIndex = 39;
            this.label_help_5.Text = "?";
            this.label_help_5.Click += new System.EventHandler(this.label_help_5_Click);
            // 
            // SetUserButtonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 262);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.checkBoxCallAfter);
            this.Controls.Add(this.textBoxTitle);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.buttonOpenFile);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxCommand);
            this.Controls.Add(this.label_help_5);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SetUserButtonForm";
            this.Tag = "ucus_dlgtitle";
            this.Text = "Set User Customization Button";
            this.Load += new System.EventHandler(this.SetUserButtonForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxCommand;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button buttonOpenFile;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxTitle;
        private System.Windows.Forms.CheckBox checkBoxCallAfter;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label_help_5;
    }
}