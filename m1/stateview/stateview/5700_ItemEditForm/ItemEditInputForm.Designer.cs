namespace stateview
{
    partial class ItemEditInputForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItemEditInputForm));
            this.textBox_name = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_helpen = new System.Windows.Forms.TextBox();
            this.textBox_helpjp = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label_error = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_method = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox_name
            // 
            this.textBox_name.Location = new System.Drawing.Point(73, 12);
            this.textBox_name.Name = "textBox_name";
            this.textBox_name.Size = new System.Drawing.Size(165, 19);
            this.textBox_name.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 12);
            this.label1.TabIndex = 1;
            this.label1.Tag = "ief_name";
            this.label1.Text = "Item name";
            // 
            // textBox_helpen
            // 
            this.textBox_helpen.Location = new System.Drawing.Point(11, 82);
            this.textBox_helpen.Multiline = true;
            this.textBox_helpen.Name = "textBox_helpen";
            this.textBox_helpen.Size = new System.Drawing.Size(439, 94);
            this.textBox_helpen.TabIndex = 2;
            // 
            // textBox_helpjp
            // 
            this.textBox_helpjp.Location = new System.Drawing.Point(11, 207);
            this.textBox_helpjp.Multiline = true;
            this.textBox_helpjp.Name = "textBox_helpjp";
            this.textBox_helpjp.Size = new System.Drawing.Size(439, 92);
            this.textBox_helpjp.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 12);
            this.label2.TabIndex = 4;
            this.label2.Tag = "ief_helpen";
            this.label2.Text = "English help message";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 192);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 12);
            this.label3.TabIndex = 5;
            this.label3.Tag = "ief_helpjp";
            this.label3.Text = "Japanese help message";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(471, 10);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 6;
            this.button1.Text = "OK";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(471, 48);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 7;
            this.button2.Text = "CANCEL";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // label_error
            // 
            this.label_error.AutoSize = true;
            this.label_error.ForeColor = System.Drawing.Color.Red;
            this.label_error.Location = new System.Drawing.Point(244, 15);
            this.label_error.Name = "label_error";
            this.label_error.Size = new System.Drawing.Size(57, 12);
            this.label_error.TabIndex = 8;
            this.label_error.Text = "label_error";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 312);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 12);
            this.label4.TabIndex = 10;
            this.label4.Tag = "ief_method";
            this.label4.Text = "Input Method";
            // 
            // textBox_method
            // 
            this.textBox_method.Location = new System.Drawing.Point(11, 327);
            this.textBox_method.Multiline = true;
            this.textBox_method.Name = "textBox_method";
            this.textBox_method.Size = new System.Drawing.Size(439, 92);
            this.textBox_method.TabIndex = 9;
            // 
            // ItemEditInputForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(558, 434);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_method);
            this.Controls.Add(this.label_error);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_helpjp);
            this.Controls.Add(this.textBox_helpen);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_name);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ItemEditInputForm";
            this.Tag = "ieif_title";
            this.Text = "ItemEditInputForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ItemEditInputForm_FormClosing);
            this.Load += new System.EventHandler(this.ItemEditInputForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_name;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_helpen;
        private System.Windows.Forms.TextBox textBox_helpjp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label_error;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_method;
    }
}