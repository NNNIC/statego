namespace stateview._5700_ItemEditForm
{
    partial class ItemEditForm_importOptionForm
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
            this.checkBox_overwrite = new System.Windows.Forms.CheckBox();
            this.button_ok = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkBox_overwrite
            // 
            this.checkBox_overwrite.AutoSize = true;
            this.checkBox_overwrite.Location = new System.Drawing.Point(44, 34);
            this.checkBox_overwrite.Name = "checkBox_overwrite";
            this.checkBox_overwrite.Size = new System.Drawing.Size(147, 28);
            this.checkBox_overwrite.TabIndex = 0;
            this.checkBox_overwrite.Tag = "iof_overwrite";
            this.checkBox_overwrite.Text = "同一アイテムがある場合、\r\nインポートの内容で上書き";
            this.checkBox_overwrite.UseVisualStyleBackColor = true;
            // 
            // button_ok
            // 
            this.button_ok.Location = new System.Drawing.Point(35, 103);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(87, 36);
            this.button_ok.TabIndex = 1;
            this.button_ok.Text = "START";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.Location = new System.Drawing.Point(160, 103);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(87, 36);
            this.button_cancel.TabIndex = 2;
            this.button_cancel.Text = "CANCEL";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // ItemEditForm_importOptionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(278, 151);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.checkBox_overwrite);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "ItemEditForm_importOptionForm";
            this.Text = "Import Option";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ItemEditForm_importOptionForm_FormClosing);
            this.Load += new System.EventHandler(this.ItemEditForm_importOptionForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.Button button_cancel;
        public System.Windows.Forms.CheckBox checkBox_overwrite;
    }
}