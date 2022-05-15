namespace StateViewer_starter2
{
    partial class CreateNewForm_ShortcutFotm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateNewForm_ShortcutFotm));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label_setdetail = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button_back = new System.Windows.Forms.Button();
            this.button_src_with_doc = new System.Windows.Forms.Button();
            this.button_all_in_one = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Yu Gothic UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox1.Location = new System.Drawing.Point(19, 59);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(190, 137);
            this.textBox1.TabIndex = 2;
            this.textBox1.Tag = "sc_exp_allinone";
            this.textBox1.Text = "ソースと同じフォルダにStateGoのデータファイルを配置します。";
            // 
            // textBox2
            // 
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Font = new System.Drawing.Font("Yu Gothic UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox2.Location = new System.Drawing.Point(19, 53);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(190, 149);
            this.textBox2.TabIndex = 5;
            this.textBox2.Tag = "sc_exp_src_w_doc";
            this.textBox2.Text = "ソースフォルダ内のdocフォルダにStateGoのデータファイルを配置します。";
            // 
            // label_setdetail
            // 
            this.label_setdetail.AutoSize = true;
            this.label_setdetail.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label_setdetail.ForeColor = System.Drawing.Color.Red;
            this.label_setdetail.Location = new System.Drawing.Point(461, 547);
            this.label_setdetail.Name = "label_setdetail";
            this.label_setdetail.Size = new System.Drawing.Size(111, 21);
            this.label_setdetail.TabIndex = 6;
            this.label_setdetail.Tag = "sc_godetailset";
            this.label_setdetail.Text = "詳細指定へ";
            this.label_setdetail.Click += new System.EventHandler(this.label_setdetail_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label2.Location = new System.Drawing.Point(9, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(643, 28);
            this.label2.TabIndex = 7;
            this.label2.Tag = "sc_ask";
            this.label2.Text = "出力ソース及びStateGoデータファイルの配置方法を指定してください。";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Location = new System.Drawing.Point(11, 45);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(627, 229);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "All in one folder";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBox2);
            this.groupBox2.Location = new System.Drawing.Point(11, 285);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(626, 242);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Src folder with doc folder";
            // 
            // button_back
            // 
            this.button_back.Location = new System.Drawing.Point(12, 539);
            this.button_back.Name = "button_back";
            this.button_back.Size = new System.Drawing.Size(87, 40);
            this.button_back.TabIndex = 10;
            this.button_back.Text = "BACK";
            this.button_back.UseVisualStyleBackColor = true;
            this.button_back.Click += new System.EventHandler(this.button_back_Click);
            // 
            // button_src_with_doc
            // 
            this.button_src_with_doc.BackgroundImage = global::StateViewer_starter2.Properties.Resources.doc_on_src_folder;
            this.button_src_with_doc.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_src_with_doc.Location = new System.Drawing.Point(228, 297);
            this.button_src_with_doc.Name = "button_src_with_doc";
            this.button_src_with_doc.Size = new System.Drawing.Size(403, 221);
            this.button_src_with_doc.TabIndex = 3;
            this.button_src_with_doc.UseVisualStyleBackColor = true;
            this.button_src_with_doc.Click += new System.EventHandler(this.button_src_with_doc_Click);
            // 
            // button_all_in_one
            // 
            this.button_all_in_one.BackgroundImage = global::StateViewer_starter2.Properties.Resources.all_on_a_folder;
            this.button_all_in_one.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button_all_in_one.Location = new System.Drawing.Point(226, 54);
            this.button_all_in_one.Name = "button_all_in_one";
            this.button_all_in_one.Size = new System.Drawing.Size(403, 221);
            this.button_all_in_one.TabIndex = 0;
            this.button_all_in_one.UseVisualStyleBackColor = true;
            this.button_all_in_one.Click += new System.EventHandler(this.button_all_in_one_Click);
            // 
            // CreateNewForm_ShortcutFotm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 593);
            this.Controls.Add(this.button_back);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label_setdetail);
            this.Controls.Add(this.button_src_with_doc);
            this.Controls.Add(this.button_all_in_one);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CreateNewForm_ShortcutFotm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "sc_titlehowto";
            this.Text = "【簡易指定】ファイル配置方法の指定";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CreateNewForm_ShortcutFotm_FormClosing);
            this.Load += new System.EventHandler(this.CreateNewForm_ShortcutFotm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_all_in_one;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button_src_with_doc;
        private System.Windows.Forms.Label label_setdetail;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button_back;
    }
}