namespace stateview._6200_msgboxForm
{
    partial class MsgBox2btnForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MsgBox2btnForm));
            this.textBox_text = new System.Windows.Forms.TextBox();
            this.button_ok = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox_text
            // 
            this.textBox_text.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_text.Location = new System.Drawing.Point(12, 12);
            this.textBox_text.Multiline = true;
            this.textBox_text.Name = "textBox_text";
            this.textBox_text.ReadOnly = true;
            this.textBox_text.Size = new System.Drawing.Size(339, 110);
            this.textBox_text.TabIndex = 3;
            this.textBox_text.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_text.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // button_ok
            // 
            this.button_ok.Location = new System.Drawing.Point(71, 147);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(75, 23);
            this.button_ok.TabIndex = 2;
            this.button_ok.Text = "OK";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.Location = new System.Drawing.Point(229, 147);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 4;
            this.button_cancel.Text = "CANCEL";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // MsgBox2btnForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 182);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.textBox_text);
            this.Controls.Add(this.button_ok);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MsgBox2btnForm";
            this.Text = "MsgBox2btnForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MsgBox2btnForm_FormClosing);
            this.Load += new System.EventHandler(this.MsgBox2btnForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox textBox_text;
        public System.Windows.Forms.Button button_ok;
        public System.Windows.Forms.Button button_cancel;
    }
}