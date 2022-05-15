namespace stateview
{
    partial class ItemEditNewForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItemEditNewForm));
            this.button_cancel = new System.Windows.Forms.Button();
            this.button_ok = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_helpjp = new System.Windows.Forms.TextBox();
            this.textBox_helpen = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_name = new System.Windows.Forms.TextBox();
            this.label_error = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_method = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button_cancel
            // 
            this.button_cancel.Location = new System.Drawing.Point(473, 49);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 15;
            this.button_cancel.Text = "CANCEL";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // button_ok
            // 
            this.button_ok.Location = new System.Drawing.Point(473, 11);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(75, 23);
            this.button_ok.TabIndex = 14;
            this.button_ok.Text = "OK";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 174);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 12);
            this.label3.TabIndex = 13;
            this.label3.Tag = "ief_helpjp";
            this.label3.Text = "Japanese help message";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 12);
            this.label2.TabIndex = 12;
            this.label2.Tag = "ief_helpen";
            this.label2.Text = "English help message";
            // 
            // textBox_helpjp
            // 
            this.textBox_helpjp.Location = new System.Drawing.Point(13, 189);
            this.textBox_helpjp.Multiline = true;
            this.textBox_helpjp.Name = "textBox_helpjp";
            this.textBox_helpjp.Size = new System.Drawing.Size(439, 92);
            this.textBox_helpjp.TabIndex = 11;
            // 
            // textBox_helpen
            // 
            this.textBox_helpen.Location = new System.Drawing.Point(13, 64);
            this.textBox_helpen.Multiline = true;
            this.textBox_helpen.Name = "textBox_helpen";
            this.textBox_helpen.Size = new System.Drawing.Size(439, 94);
            this.textBox_helpen.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 12);
            this.label1.TabIndex = 9;
            this.label1.Tag = "ief_name";
            this.label1.Text = "Item name";
            // 
            // textBox_name
            // 
            this.textBox_name.Location = new System.Drawing.Point(75, 13);
            this.textBox_name.Name = "textBox_name";
            this.textBox_name.Size = new System.Drawing.Size(165, 19);
            this.textBox_name.TabIndex = 8;
            // 
            // label_error
            // 
            this.label_error.AutoSize = true;
            this.label_error.ForeColor = System.Drawing.Color.Red;
            this.label_error.Location = new System.Drawing.Point(259, 16);
            this.label_error.Name = "label_error";
            this.label_error.Size = new System.Drawing.Size(57, 12);
            this.label_error.TabIndex = 16;
            this.label_error.Text = "label_error";
            this.label_error.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 299);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 12);
            this.label4.TabIndex = 18;
            this.label4.Tag = "ief_method";
            this.label4.Text = "Input Method";
            // 
            // textBox_method
            // 
            this.textBox_method.Location = new System.Drawing.Point(13, 314);
            this.textBox_method.Multiline = true;
            this.textBox_method.Name = "textBox_method";
            this.textBox_method.Size = new System.Drawing.Size(439, 92);
            this.textBox_method.TabIndex = 17;
            // 
            // ItemEditNewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 420);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_method);
            this.Controls.Add(this.label_error);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_helpjp);
            this.Controls.Add(this.textBox_helpen);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_name);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ItemEditNewForm";
            this.Tag = "ienf_title";
            this.Text = "ItemEditNewForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ItemEditNewForm_FormClosing);
            this.Load += new System.EventHandler(this.ItemEditNewForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_helpjp;
        private System.Windows.Forms.TextBox textBox_helpen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_name;
        private System.Windows.Forms.Label label_error;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_method;
    }
}