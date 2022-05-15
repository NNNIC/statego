namespace stateview._5000_ViewForm.dialog
{
    partial class UpgradePsggForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpgradePsggForm));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.checkBox_excelbackup = new System.Windows.Forms.CheckBox();
            this.checkBox_save_withexcel = new System.Windows.Forms.CheckBox();
            this.checkBox_check_excel_writable = new System.Windows.Forms.CheckBox();
            this.button_upgrade = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            this.textBox_caution = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.radioButtonDelExcel = new System.Windows.Forms.RadioButton();
            this.radioButtonUseExcel = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.BackColor = System.Drawing.Color.Azure;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Enabled = false;
            this.textBox1.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.textBox1.Location = new System.Drawing.Point(12, 57);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(908, 278);
            this.textBox1.TabIndex = 0;
            this.textBox1.Tag = "pfud_desc";
            this.textBox1.Text = resources.GetString("textBox1.Text");
            // 
            // checkBox_excelbackup
            // 
            this.checkBox_excelbackup.AutoSize = true;
            this.checkBox_excelbackup.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.checkBox_excelbackup.Checked = true;
            this.checkBox_excelbackup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_excelbackup.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkBox_excelbackup.ForeColor = System.Drawing.Color.RoyalBlue;
            this.checkBox_excelbackup.Location = new System.Drawing.Point(179, 426);
            this.checkBox_excelbackup.Name = "checkBox_excelbackup";
            this.checkBox_excelbackup.Size = new System.Drawing.Size(581, 52);
            this.checkBox_excelbackup.TabIndex = 1;
            this.checkBox_excelbackup.Tag = "pfud_excelbackup";
            this.checkBox_excelbackup.Text = "アップグレード時にExcelファイルのバックアップを作成する。\r\n➡マニュアルで追加したシートは新しいExcelに引き継ぐことが出来ません。";
            this.checkBox_excelbackup.UseVisualStyleBackColor = false;
            this.checkBox_excelbackup.Visible = false;
            // 
            // checkBox_save_withexcel
            // 
            this.checkBox_save_withexcel.AutoSize = true;
            this.checkBox_save_withexcel.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.checkBox_save_withexcel.Checked = true;
            this.checkBox_save_withexcel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_save_withexcel.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkBox_save_withexcel.ForeColor = System.Drawing.Color.RoyalBlue;
            this.checkBox_save_withexcel.Location = new System.Drawing.Point(179, 465);
            this.checkBox_save_withexcel.Name = "checkBox_save_withexcel";
            this.checkBox_save_withexcel.Size = new System.Drawing.Size(533, 52);
            this.checkBox_save_withexcel.TabIndex = 2;
            this.checkBox_save_withexcel.Tag = "pfud_save_withexcel";
            this.checkBox_save_withexcel.Text = "データ保存時、これまでと同様にExcelファイルへデータを記録する。\r\n※本オプションはアップグレード後も変更可能です。";
            this.checkBox_save_withexcel.UseVisualStyleBackColor = false;
            // 
            // checkBox_check_excel_writable
            // 
            this.checkBox_check_excel_writable.AutoSize = true;
            this.checkBox_check_excel_writable.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.checkBox_check_excel_writable.Checked = true;
            this.checkBox_check_excel_writable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_check_excel_writable.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkBox_check_excel_writable.ForeColor = System.Drawing.Color.RoyalBlue;
            this.checkBox_check_excel_writable.Location = new System.Drawing.Point(179, 529);
            this.checkBox_check_excel_writable.Name = "checkBox_check_excel_writable";
            this.checkBox_check_excel_writable.Size = new System.Drawing.Size(670, 76);
            this.checkBox_check_excel_writable.TabIndex = 3;
            this.checkBox_check_excel_writable.Tag = "pfud_checkexcelwr";
            this.checkBox_check_excel_writable.Text = "保存前にExcelファイルのReadOnly属性を確認する。\r\n➡SVN等でファイルロックで保護されていた機能を引き続き利用する場合に設定します。\r\n※本オプショ" +
    "ンはアップグレード後も変更可能です。";
            this.checkBox_check_excel_writable.UseVisualStyleBackColor = false;
            // 
            // button_upgrade
            // 
            this.button_upgrade.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_upgrade.Location = new System.Drawing.Point(473, 667);
            this.button_upgrade.Name = "button_upgrade";
            this.button_upgrade.Size = new System.Drawing.Size(201, 78);
            this.button_upgrade.TabIndex = 4;
            this.button_upgrade.Tag = "pfud_upgrade";
            this.button_upgrade.Text = "アップグレード";
            this.button_upgrade.UseVisualStyleBackColor = true;
            this.button_upgrade.Click += new System.EventHandler(this.button_upgrade_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.Font = new System.Drawing.Font("メイリオ", 14.25F, System.Drawing.FontStyle.Bold);
            this.button_cancel.Location = new System.Drawing.Point(707, 667);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(201, 78);
            this.button_cancel.TabIndex = 5;
            this.button_cancel.Tag = "pfud_cancel";
            this.button_cancel.Text = "キャンセル";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // textBox_caution
            // 
            this.textBox_caution.BackColor = System.Drawing.SystemColors.Control;
            this.textBox_caution.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_caution.Font = new System.Drawing.Font("MS UI Gothic", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_caution.Location = new System.Drawing.Point(12, 744);
            this.textBox_caution.Name = "textBox_caution";
            this.textBox_caution.Size = new System.Drawing.Size(313, 12);
            this.textBox_caution.TabIndex = 6;
            this.textBox_caution.Tag = "pfud_catuion";
            this.textBox_caution.Text = "※本ダイアログはデータバージョンが1.0の場合に表示されます。";
            // 
            // textBox3
            // 
            this.textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox3.BackColor = System.Drawing.SystemColors.Control;
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.Enabled = false;
            this.textBox3.Font = new System.Drawing.Font("メイリオ", 21.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox3.Location = new System.Drawing.Point(12, 12);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(908, 46);
            this.textBox3.TabIndex = 7;
            this.textBox3.Tag = "pfud_title";
            this.textBox3.Text = "StateGo関連のファイルを1.0から1.1へアップグレードします。";
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox4
            // 
            this.textBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox4.BackColor = System.Drawing.Color.White;
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox4.Enabled = false;
            this.textBox4.Font = new System.Drawing.Font("メイリオ", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox4.ForeColor = System.Drawing.SystemColors.Window;
            this.textBox4.Location = new System.Drawing.Point(118, 415);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(759, 234);
            this.textBox4.TabIndex = 8;
            this.textBox4.Tag = " ";
            this.textBox4.Text = resources.GetString("textBox4.Text");
            // 
            // radioButtonDelExcel
            // 
            this.radioButtonDelExcel.AutoSize = true;
            this.radioButtonDelExcel.Checked = true;
            this.radioButtonDelExcel.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.radioButtonDelExcel.Location = new System.Drawing.Point(23, 366);
            this.radioButtonDelExcel.Name = "radioButtonDelExcel";
            this.radioButtonDelExcel.Size = new System.Drawing.Size(174, 23);
            this.radioButtonDelExcel.TabIndex = 9;
            this.radioButtonDelExcel.TabStop = true;
            this.radioButtonDelExcel.Tag = "pfud_exceldel";
            this.radioButtonDelExcel.Text = "Excelファイル削除";
            this.radioButtonDelExcel.UseVisualStyleBackColor = true;
            // 
            // radioButtonUseExcel
            // 
            this.radioButtonUseExcel.AutoSize = true;
            this.radioButtonUseExcel.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.radioButtonUseExcel.Location = new System.Drawing.Point(261, 366);
            this.radioButtonUseExcel.Name = "radioButtonUseExcel";
            this.radioButtonUseExcel.Size = new System.Drawing.Size(236, 23);
            this.radioButtonUseExcel.TabIndex = 10;
            this.radioButtonUseExcel.Tag = "pfud_exceluse";
            this.radioButtonUseExcel.Text = "Excelファイルを今後も使う";
            this.radioButtonUseExcel.UseVisualStyleBackColor = true;
            this.radioButtonUseExcel.CheckedChanged += new System.EventHandler(this.radioButtonUseExcel_CheckedChanged);
            // 
            // UpgradePsggForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(932, 761);
            this.Controls.Add(this.radioButtonUseExcel);
            this.Controls.Add(this.radioButtonDelExcel);
            this.Controls.Add(this.checkBox_check_excel_writable);
            this.Controls.Add(this.checkBox_save_withexcel);
            this.Controls.Add(this.checkBox_excelbackup);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox_caution);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_upgrade);
            this.Controls.Add(this.textBox1);
            this.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UpgradePsggForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "pfud_dlgtitle";
            this.Text = "PSGGファイルアップグレード";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UpgradePsggForm_FormClosing);
            this.Load += new System.EventHandler(this.UpgradePsggForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button_upgrade;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.TextBox textBox_caution;
        public System.Windows.Forms.CheckBox checkBox_excelbackup;
        public System.Windows.Forms.CheckBox checkBox_save_withexcel;
        public System.Windows.Forms.CheckBox checkBox_check_excel_writable;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.RadioButton radioButtonDelExcel;
        private System.Windows.Forms.RadioButton radioButtonUseExcel;
    }
}