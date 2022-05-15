namespace stateview._5000_ViewForm.dialog
{
    partial class ExternalCommendForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExternalCommendForm));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonExec = new System.Windows.Forms.Button();
            this.buttonSaveClose = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.checkBoxUseExternal = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(65, 21);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(349, 19);
            this.textBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Command";
            // 
            // buttonExec
            // 
            this.buttonExec.Location = new System.Drawing.Point(420, 19);
            this.buttonExec.Name = "buttonExec";
            this.buttonExec.Size = new System.Drawing.Size(75, 23);
            this.buttonExec.TabIndex = 2;
            this.buttonExec.Text = "Execute";
            this.buttonExec.UseVisualStyleBackColor = true;
            this.buttonExec.Click += new System.EventHandler(this.buttonExec_Click);
            // 
            // buttonSaveClose
            // 
            this.buttonSaveClose.Location = new System.Drawing.Point(515, 19);
            this.buttonSaveClose.Name = "buttonSaveClose";
            this.buttonSaveClose.Size = new System.Drawing.Size(111, 23);
            this.buttonSaveClose.TabIndex = 3;
            this.buttonSaveClose.Text = "SAVE && CLOSE";
            this.buttonSaveClose.UseVisualStyleBackColor = true;
            this.buttonSaveClose.Click += new System.EventHandler(this.buttonSaveClose_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(515, 48);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(111, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "CANCEL";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checkBoxUseExternal
            // 
            this.checkBoxUseExternal.AutoSize = true;
            this.checkBoxUseExternal.Location = new System.Drawing.Point(65, 57);
            this.checkBoxUseExternal.Name = "checkBoxUseExternal";
            this.checkBoxUseExternal.Size = new System.Drawing.Size(141, 16);
            this.checkBoxUseExternal.TabIndex = 5;
            this.checkBoxUseExternal.Text = "Use external command";
            this.checkBoxUseExternal.UseVisualStyleBackColor = true;
            this.checkBoxUseExternal.CheckedChanged += new System.EventHandler(this.checkBoxUseExternal_CheckedChanged);
            // 
            // ExternalCommendForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(643, 85);
            this.Controls.Add(this.checkBoxUseExternal);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.buttonSaveClose);
            this.Controls.Add(this.buttonExec);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ExternalCommendForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Execute external command";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExternalCommendForm_FormClosing);
            this.Load += new System.EventHandler(this.ExternalCommendForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonExec;
        private System.Windows.Forms.Button buttonSaveClose;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checkBoxUseExternal;
    }
}