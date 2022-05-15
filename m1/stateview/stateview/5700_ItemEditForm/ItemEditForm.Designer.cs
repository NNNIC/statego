namespace stateview
{
    partial class ItemEditForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ItemEditForm));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button_ok = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip_main = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.contextMenuStrip_index = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.insertToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.upToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.downToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip_cond = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.condChgToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.cancel2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip_checkonoff = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.checkonToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.checkoffToolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelToolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            this.label_help_5 = new System.Windows.Forms.Label();
            this.button_export = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip_main.SuspendLayout();
            this.contextMenuStrip_index.SuspendLayout();
            this.contextMenuStrip_cond.SuspendLayout();
            this.contextMenuStrip_checkonoff.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.Size = new System.Drawing.Size(732, 525);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // button_ok
            // 
            this.button_ok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_ok.Location = new System.Drawing.Point(768, 12);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(75, 23);
            this.button_ok.TabIndex = 1;
            this.button_ok.Text = "OK";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // contextMenuStrip_main
            // 
            this.contextMenuStrip_main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.cancelToolStripMenuItem});
            this.contextMenuStrip_main.Name = "contextMenuStrip_main";
            this.contextMenuStrip_main.Size = new System.Drawing.Size(132, 60);
            this.contextMenuStrip_main.Closing += new System.Windows.Forms.ToolStripDropDownClosingEventHandler(this.contextMenuStrip_main_Closing);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(131, 28);
            this.editToolStripMenuItem.Tag = "iefm_edit";
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // cancelToolStripMenuItem
            // 
            this.cancelToolStripMenuItem.Name = "cancelToolStripMenuItem";
            this.cancelToolStripMenuItem.Size = new System.Drawing.Size(131, 28);
            this.cancelToolStripMenuItem.Tag = "iefm_cancel";
            this.cancelToolStripMenuItem.Text = "Cancel";
            this.cancelToolStripMenuItem.Click += new System.EventHandler(this.cancelToolStripMenuItem_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(768, 51);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "CANCEL";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // contextMenuStrip_index
            // 
            this.contextMenuStrip_index.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.insertToolStripMenuItem1,
            this.removeToolStripMenuItem1,
            this.upToolStripMenuItem1,
            this.downToolStripMenuItem1,
            this.cancelToolStripMenuItem1});
            this.contextMenuStrip_index.Name = "contextMenuStrip_index";
            this.contextMenuStrip_index.Size = new System.Drawing.Size(153, 166);
            this.contextMenuStrip_index.Tag = "iefi_insert";
            // 
            // insertToolStripMenuItem1
            // 
            this.insertToolStripMenuItem1.Name = "insertToolStripMenuItem1";
            this.insertToolStripMenuItem1.Size = new System.Drawing.Size(152, 28);
            this.insertToolStripMenuItem1.Tag = "iefi_insert";
            this.insertToolStripMenuItem1.Text = "Insert";
            this.insertToolStripMenuItem1.Click += new System.EventHandler(this.insertToolStripMenuItem1_Click);
            // 
            // removeToolStripMenuItem1
            // 
            this.removeToolStripMenuItem1.Name = "removeToolStripMenuItem1";
            this.removeToolStripMenuItem1.Size = new System.Drawing.Size(152, 28);
            this.removeToolStripMenuItem1.Tag = "iefi_remove";
            this.removeToolStripMenuItem1.Text = "Remove";
            this.removeToolStripMenuItem1.Click += new System.EventHandler(this.removeToolStripMenuItem1_Click);
            // 
            // upToolStripMenuItem1
            // 
            this.upToolStripMenuItem1.Name = "upToolStripMenuItem1";
            this.upToolStripMenuItem1.Size = new System.Drawing.Size(152, 28);
            this.upToolStripMenuItem1.Tag = "iefi_up";
            this.upToolStripMenuItem1.Text = "Up";
            this.upToolStripMenuItem1.Click += new System.EventHandler(this.upToolStripMenuItem1_Click);
            // 
            // downToolStripMenuItem1
            // 
            this.downToolStripMenuItem1.Name = "downToolStripMenuItem1";
            this.downToolStripMenuItem1.Size = new System.Drawing.Size(152, 28);
            this.downToolStripMenuItem1.Tag = "iefi_down";
            this.downToolStripMenuItem1.Text = "Down";
            this.downToolStripMenuItem1.Click += new System.EventHandler(this.downToolStripMenuItem1_Click);
            // 
            // cancelToolStripMenuItem1
            // 
            this.cancelToolStripMenuItem1.Name = "cancelToolStripMenuItem1";
            this.cancelToolStripMenuItem1.Size = new System.Drawing.Size(152, 28);
            this.cancelToolStripMenuItem1.Tag = "iefi_cancel";
            this.cancelToolStripMenuItem1.Text = "Cancel";
            this.cancelToolStripMenuItem1.Click += new System.EventHandler(this.cancelToolStripMenuItem1_Click);
            // 
            // contextMenuStrip_cond
            // 
            this.contextMenuStrip_cond.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.condChgToolStripMenuItem2,
            this.cancel2ToolStripMenuItem});
            this.contextMenuStrip_cond.Name = "contextMenuStrip_main";
            this.contextMenuStrip_cond.Size = new System.Drawing.Size(219, 60);
            // 
            // condChgToolStripMenuItem2
            // 
            this.condChgToolStripMenuItem2.Name = "condChgToolStripMenuItem2";
            this.condChgToolStripMenuItem2.Size = new System.Drawing.Size(218, 28);
            this.condChgToolStripMenuItem2.Text = "share ⇔ exclusion";
            this.condChgToolStripMenuItem2.Click += new System.EventHandler(this.conditionChange2ToollStripMenuItem2_Click);
            // 
            // cancel2ToolStripMenuItem
            // 
            this.cancel2ToolStripMenuItem.Name = "cancel2ToolStripMenuItem";
            this.cancel2ToolStripMenuItem.Size = new System.Drawing.Size(218, 28);
            this.cancel2ToolStripMenuItem.Text = "Cancel";
            this.cancel2ToolStripMenuItem.Click += new System.EventHandler(this.cancel2ToolStripMenuItem_Click);
            // 
            // contextMenuStrip_checkonoff
            // 
            this.contextMenuStrip_checkonoff.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkonToolStripMenuItem3,
            this.checkoffToolStripMenuItem4,
            this.cancelToolStripMenuItem9});
            this.contextMenuStrip_checkonoff.Name = "contextMenuStrip_main";
            this.contextMenuStrip_checkonoff.Size = new System.Drawing.Size(218, 88);
            // 
            // checkonToolStripMenuItem3
            // 
            this.checkonToolStripMenuItem3.Name = "checkonToolStripMenuItem3";
            this.checkonToolStripMenuItem3.Size = new System.Drawing.Size(217, 28);
            this.checkonToolStripMenuItem3.Text = "Check On □➡☑";
            this.checkonToolStripMenuItem3.Click += new System.EventHandler(this.checkonToolStripMenuItem3_Click);
            // 
            // checkoffToolStripMenuItem4
            // 
            this.checkoffToolStripMenuItem4.Name = "checkoffToolStripMenuItem4";
            this.checkoffToolStripMenuItem4.Size = new System.Drawing.Size(217, 28);
            this.checkoffToolStripMenuItem4.Text = "Check Off ☑➡□";
            this.checkoffToolStripMenuItem4.Click += new System.EventHandler(this.checkoffToolStripMenuItem4_Click);
            // 
            // cancelToolStripMenuItem9
            // 
            this.cancelToolStripMenuItem9.Name = "cancelToolStripMenuItem9";
            this.cancelToolStripMenuItem9.Size = new System.Drawing.Size(217, 28);
            this.cancelToolStripMenuItem9.Text = "Cancel";
            this.cancelToolStripMenuItem9.Click += new System.EventHandler(this.cancelToolStripMenuItem9_Click);
            // 
            // label_help_5
            // 
            this.label_help_5.AutoSize = true;
            this.label_help_5.BackColor = System.Drawing.Color.Silver;
            this.label_help_5.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_help_5.ForeColor = System.Drawing.Color.White;
            this.label_help_5.Location = new System.Drawing.Point(849, 1);
            this.label_help_5.Margin = new System.Windows.Forms.Padding(0);
            this.label_help_5.Name = "label_help_5";
            this.label_help_5.Size = new System.Drawing.Size(13, 14);
            this.label_help_5.TabIndex = 40;
            this.label_help_5.Text = "?";
            this.label_help_5.Click += new System.EventHandler(this.label_help_5_Click);
            // 
            // button_export
            // 
            this.button_export.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_export.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_export.Location = new System.Drawing.Point(768, 223);
            this.button_export.Name = "button_export";
            this.button_export.Size = new System.Drawing.Size(84, 46);
            this.button_export.TabIndex = 41;
            this.button_export.Tag = "ief_export";
            this.button_export.Text = "Export to \r\nclipboard";
            this.button_export.UseVisualStyleBackColor = true;
            this.button_export.Click += new System.EventHandler(this.button_export_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button2.Location = new System.Drawing.Point(768, 277);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(84, 46);
            this.button2.TabIndex = 42;
            this.button2.Tag = "ief_import";
            this.button2.Text = "Import from\r\nclipboard";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button_import_Click);
            // 
            // ItemEditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(864, 549);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button_export);
            this.Controls.Add(this.label_help_5);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.dataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ItemEditForm";
            this.Tag = "ief_title";
            this.Text = "ItemEditForm";
            this.Load += new System.EventHandler(this.ItemEditForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip_main.ResumeLayout(false);
            this.contextMenuStrip_index.ResumeLayout(false);
            this.contextMenuStrip_cond.ResumeLayout(false);
            this.contextMenuStrip_checkonoff.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cancelToolStripMenuItem;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip_main;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ToolStripMenuItem insertToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem upToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem downToolStripMenuItem1;
        public System.Windows.Forms.ToolStripMenuItem cancelToolStripMenuItem1;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip_index;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip_cond;
        private System.Windows.Forms.ToolStripMenuItem condChgToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem cancel2ToolStripMenuItem;
        public System.Windows.Forms.ContextMenuStrip contextMenuStrip_checkonoff;
        private System.Windows.Forms.ToolStripMenuItem checkonToolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem checkoffToolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem cancelToolStripMenuItem9;
        private System.Windows.Forms.Label label_help_5;
        private System.Windows.Forms.Button button_export;
        private System.Windows.Forms.Button button2;
    }
}