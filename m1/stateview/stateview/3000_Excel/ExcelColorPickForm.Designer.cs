namespace stateview
{
    partial class ExcelColorPickForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExcelColorPickForm));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Index = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.value = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.view = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.hex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBox_index = new System.Windows.Forms.TextBox();
            this.textBox_value = new System.Windows.Forms.TextBox();
            this.textBox_view = new System.Windows.Forms.TextBox();
            this.textBox_hex = new System.Windows.Forms.TextBox();
            this.button_ok = new System.Windows.Forms.Button();
            this.button_cancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Index,
            this.value,
            this.view,
            this.hex});
            this.dataGridView1.Location = new System.Drawing.Point(12, 58);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.Size = new System.Drawing.Size(318, 472);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.Click += new System.EventHandler(this.dataGridView1_Click);
            // 
            // Index
            // 
            this.Index.HeaderText = "Index";
            this.Index.Name = "Index";
            this.Index.ReadOnly = true;
            this.Index.Width = 40;
            // 
            // value
            // 
            this.value.HeaderText = "Color Value";
            this.value.Name = "value";
            this.value.ReadOnly = true;
            // 
            // view
            // 
            this.view.HeaderText = "View";
            this.view.Name = "view";
            this.view.ReadOnly = true;
            // 
            // hex
            // 
            this.hex.HeaderText = "Hex";
            this.hex.Name = "hex";
            this.hex.ReadOnly = true;
            // 
            // textBox_index
            // 
            this.textBox_index.Location = new System.Drawing.Point(12, 22);
            this.textBox_index.Multiline = true;
            this.textBox_index.Name = "textBox_index";
            this.textBox_index.Size = new System.Drawing.Size(31, 19);
            this.textBox_index.TabIndex = 1;
            // 
            // textBox_value
            // 
            this.textBox_value.Location = new System.Drawing.Point(49, 22);
            this.textBox_value.Multiline = true;
            this.textBox_value.Name = "textBox_value";
            this.textBox_value.Size = new System.Drawing.Size(147, 19);
            this.textBox_value.TabIndex = 2;
            // 
            // textBox_view
            // 
            this.textBox_view.Location = new System.Drawing.Point(202, 22);
            this.textBox_view.Multiline = true;
            this.textBox_view.Name = "textBox_view";
            this.textBox_view.Size = new System.Drawing.Size(49, 19);
            this.textBox_view.TabIndex = 3;
            // 
            // textBox_hex
            // 
            this.textBox_hex.Location = new System.Drawing.Point(257, 22);
            this.textBox_hex.Multiline = true;
            this.textBox_hex.Name = "textBox_hex";
            this.textBox_hex.Size = new System.Drawing.Size(73, 19);
            this.textBox_hex.TabIndex = 4;
            // 
            // button_ok
            // 
            this.button_ok.Location = new System.Drawing.Point(345, 22);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(83, 42);
            this.button_ok.TabIndex = 5;
            this.button_ok.Text = "OK";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // button_cancel
            // 
            this.button_cancel.Location = new System.Drawing.Point(345, 70);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(83, 42);
            this.button_cancel.TabIndex = 6;
            this.button_cancel.Text = "CANCEL";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // ExcelColorPickForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 542);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.textBox_hex);
            this.Controls.Add(this.textBox_view);
            this.Controls.Add(this.textBox_value);
            this.Controls.Add(this.textBox_index);
            this.Controls.Add(this.dataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ExcelColorPickForm";
            this.Text = "ExcelColorPickForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ExcelColorPickForm_FormClosing);
            this.Load += new System.EventHandler(this.ExcelColorPickForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox textBox_index;
        private System.Windows.Forms.TextBox textBox_value;
        private System.Windows.Forms.TextBox textBox_view;
        private System.Windows.Forms.TextBox textBox_hex;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.Button button_cancel;
        private System.Windows.Forms.DataGridViewTextBoxColumn Index;
        private System.Windows.Forms.DataGridViewTextBoxColumn value;
        private System.Windows.Forms.DataGridViewTextBoxColumn view;
        private System.Windows.Forms.DataGridViewTextBoxColumn hex;
    }
}