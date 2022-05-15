namespace stateview
{
    partial class ConfigForm
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
            if(disposing && (components != null))
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigForm));
            this.label1 = new System.Windows.Forms.Label();
            this.bitmap_width_textBox = new System.Windows.Forms.TextBox();
            this.bitmap_height_textBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.ReadExcelOptimize_checkBox = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cb_save_by_uuid_order = new System.Windows.Forms.CheckBox();
            this.cb_force_disp_out_pin = new System.Windows.Forms.CheckBox();
            this.button_OK = new System.Windows.Forms.Button();
            this.button_CANCEL = new System.Windows.Forms.Button();
            this.label_help_main_panel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 12);
            this.label1.TabIndex = 0;
            this.label1.Tag = "cnff_bmsize";
            this.label1.Text = "Bitmap Size";
            // 
            // bitmap_width_textBox
            // 
            this.bitmap_width_textBox.Location = new System.Drawing.Point(127, 93);
            this.bitmap_width_textBox.Name = "bitmap_width_textBox";
            this.bitmap_width_textBox.Size = new System.Drawing.Size(100, 19);
            this.bitmap_width_textBox.TabIndex = 1;
            this.bitmap_width_textBox.Text = "4000";
            this.bitmap_width_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // bitmap_height_textBox
            // 
            this.bitmap_height_textBox.Location = new System.Drawing.Point(233, 93);
            this.bitmap_height_textBox.Name = "bitmap_height_textBox";
            this.bitmap_height_textBox.Size = new System.Drawing.Size(100, 19);
            this.bitmap_height_textBox.TabIndex = 2;
            this.bitmap_height_textBox.Text = "2000";
            this.bitmap_height_textBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(125, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(33, 12);
            this.label2.TabIndex = 3;
            this.label2.Tag = "cnff_width";
            this.label2.Text = "Width";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(231, 78);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 12);
            this.label3.TabIndex = 4;
            this.label3.Tag = "cnff_height";
            this.label3.Text = "Height";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(21, 16);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(63, 12);
            this.label10.TabIndex = 25;
            this.label10.Text = "Read Excel";
            this.label10.Visible = false;
            // 
            // ReadExcelOptimize_checkBox
            // 
            this.ReadExcelOptimize_checkBox.AutoSize = true;
            this.ReadExcelOptimize_checkBox.Checked = true;
            this.ReadExcelOptimize_checkBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ReadExcelOptimize_checkBox.Location = new System.Drawing.Point(117, 16);
            this.ReadExcelOptimize_checkBox.Name = "ReadExcelOptimize_checkBox";
            this.ReadExcelOptimize_checkBox.Size = new System.Drawing.Size(66, 16);
            this.ReadExcelOptimize_checkBox.TabIndex = 24;
            this.ReadExcelOptimize_checkBox.Text = "optimize";
            this.ReadExcelOptimize_checkBox.UseVisualStyleBackColor = true;
            this.ReadExcelOptimize_checkBox.Visible = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 180);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(30, 12);
            this.label9.TabIndex = 26;
            this.label9.Text = "Save";
            this.label9.Visible = false;
            // 
            // cb_save_by_uuid_order
            // 
            this.cb_save_by_uuid_order.AutoSize = true;
            this.cb_save_by_uuid_order.Checked = true;
            this.cb_save_by_uuid_order.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_save_by_uuid_order.Enabled = false;
            this.cb_save_by_uuid_order.Location = new System.Drawing.Point(78, 180);
            this.cb_save_by_uuid_order.Name = "cb_save_by_uuid_order";
            this.cb_save_by_uuid_order.Size = new System.Drawing.Size(126, 16);
            this.cb_save_by_uuid_order.TabIndex = 27;
            this.cb_save_by_uuid_order.Text = "Save by UUID order";
            this.cb_save_by_uuid_order.UseVisualStyleBackColor = true;
            this.cb_save_by_uuid_order.Visible = false;
            // 
            // cb_force_disp_out_pin
            // 
            this.cb_force_disp_out_pin.AutoSize = true;
            this.cb_force_disp_out_pin.Location = new System.Drawing.Point(23, 152);
            this.cb_force_disp_out_pin.Name = "cb_force_disp_out_pin";
            this.cb_force_disp_out_pin.Size = new System.Drawing.Size(146, 16);
            this.cb_force_disp_out_pin.TabIndex = 28;
            this.cb_force_disp_out_pin.Text = "Force to display out pin";
            this.cb_force_disp_out_pin.UseVisualStyleBackColor = true;
            this.cb_force_disp_out_pin.Visible = false;
            // 
            // button_OK
            // 
            this.button_OK.Location = new System.Drawing.Point(372, 23);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 29;
            this.button_OK.Text = "OK";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // button_CANCEL
            // 
            this.button_CANCEL.Location = new System.Drawing.Point(372, 65);
            this.button_CANCEL.Name = "button_CANCEL";
            this.button_CANCEL.Size = new System.Drawing.Size(75, 23);
            this.button_CANCEL.TabIndex = 30;
            this.button_CANCEL.Text = "CANCEL";
            this.button_CANCEL.UseVisualStyleBackColor = true;
            this.button_CANCEL.Click += new System.EventHandler(this.button_CANCEL_Click);
            // 
            // label_help_main_panel
            // 
            this.label_help_main_panel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_help_main_panel.AutoSize = true;
            this.label_help_main_panel.BackColor = System.Drawing.Color.Silver;
            this.label_help_main_panel.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_help_main_panel.ForeColor = System.Drawing.Color.White;
            this.label_help_main_panel.Location = new System.Drawing.Point(446, 3);
            this.label_help_main_panel.Margin = new System.Windows.Forms.Padding(0);
            this.label_help_main_panel.Name = "label_help_main_panel";
            this.label_help_main_panel.Size = new System.Drawing.Size(13, 14);
            this.label_help_main_panel.TabIndex = 31;
            this.label_help_main_panel.Text = "?";
            this.label_help_main_panel.Click += new System.EventHandler(this.label_help_main_panel_Click);
            // 
            // ConfigForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(463, 215);
            this.Controls.Add(this.label_help_main_panel);
            this.Controls.Add(this.button_CANCEL);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.cb_force_disp_out_pin);
            this.Controls.Add(this.cb_save_by_uuid_order);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.ReadExcelOptimize_checkBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.bitmap_height_textBox);
            this.Controls.Add(this.bitmap_width_textBox);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ConfigForm";
            this.Tag = "cnff_title";
            this.Text = "Configutation Dialog";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ConfigForm_FormClosing);
            this.Load += new System.EventHandler(this.ConfigForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.TextBox bitmap_width_textBox;
        public System.Windows.Forms.TextBox bitmap_height_textBox;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.CheckBox ReadExcelOptimize_checkBox;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.CheckBox cb_save_by_uuid_order;
        public System.Windows.Forms.CheckBox cb_force_disp_out_pin;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.Button button_CANCEL;
        private System.Windows.Forms.Label label_help_main_panel;
    }
}