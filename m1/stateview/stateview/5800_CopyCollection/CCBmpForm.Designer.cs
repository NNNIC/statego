namespace stateview._5800_CopyCollection
{
    partial class CCBmpForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CCBmpForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button_OK = new System.Windows.Forms.Button();
            this.button_LOAD = new System.Windows.Forms.Button();
            this.button_PASTE = new System.Windows.Forms.Button();
            this.button_CANCEL = new System.Windows.Forms.Button();
            this.button_USECAP = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(458, 413);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(488, 12);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 1;
            this.button_OK.Text = "OK";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // button_LOAD
            // 
            this.button_LOAD.Location = new System.Drawing.Point(488, 41);
            this.button_LOAD.Name = "button_LOAD";
            this.button_LOAD.Size = new System.Drawing.Size(75, 23);
            this.button_LOAD.TabIndex = 2;
            this.button_LOAD.Text = "LOAD";
            this.button_LOAD.UseVisualStyleBackColor = true;
            this.button_LOAD.Click += new System.EventHandler(this.button_LOAD_Click);
            // 
            // button_PASTE
            // 
            this.button_PASTE.Location = new System.Drawing.Point(488, 70);
            this.button_PASTE.Name = "button_PASTE";
            this.button_PASTE.Size = new System.Drawing.Size(75, 23);
            this.button_PASTE.TabIndex = 3;
            this.button_PASTE.Text = "PASTE";
            this.button_PASTE.UseVisualStyleBackColor = true;
            this.button_PASTE.Click += new System.EventHandler(this.button_PASTE_Click);
            // 
            // button_CANCEL
            // 
            this.button_CANCEL.Location = new System.Drawing.Point(488, 160);
            this.button_CANCEL.Name = "button_CANCEL";
            this.button_CANCEL.Size = new System.Drawing.Size(75, 23);
            this.button_CANCEL.TabIndex = 4;
            this.button_CANCEL.Text = "CANCEL";
            this.button_CANCEL.UseVisualStyleBackColor = true;
            this.button_CANCEL.Click += new System.EventHandler(this.button_CANCEL_Click);
            // 
            // button_USECAP
            // 
            this.button_USECAP.Location = new System.Drawing.Point(488, 99);
            this.button_USECAP.Name = "button_USECAP";
            this.button_USECAP.Size = new System.Drawing.Size(75, 45);
            this.button_USECAP.TabIndex = 5;
            this.button_USECAP.Tag = "ccbcap";
            this.button_USECAP.Text = "Use Capture image";
            this.button_USECAP.UseVisualStyleBackColor = true;
            this.button_USECAP.Click += new System.EventHandler(this.button_USECAP_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "icon.png";
            this.openFileDialog1.Filter = "イメージファイル|*.png;*.bmp;*.jpg;*.gif|全てのファイル|*.*";
            // 
            // CCBmpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 437);
            this.Controls.Add(this.button_USECAP);
            this.Controls.Add(this.button_CANCEL);
            this.Controls.Add(this.button_PASTE);
            this.Controls.Add(this.button_LOAD);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CCBmpForm";
            this.Tag = "ccbtitle";
            this.Text = "CCBmpForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CCBmpForm_FormClosing);
            this.Load += new System.EventHandler(this.CCBmpForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Button button_LOAD;
        private System.Windows.Forms.Button button_PASTE;
        private System.Windows.Forms.Button button_CANCEL;
        public System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.Button button_USECAP;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}