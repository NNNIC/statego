namespace stateview._5000_ViewForm.dialog_editbranch
{
    partial class EditBranchForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditBranchForm));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.buttonOK = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.MenuStrip_Item = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuItem_Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_ItemSlect = new System.Windows.Forms.ToolStripMenuItem();
            this.brxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Up = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_Down = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStrip_Blank = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuItem_New = new System.Windows.Forms.ToolStripMenuItem();
            this.iFToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.eLSEIFToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.eLSEToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.aPIToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_BlankSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.buttonReset = new System.Windows.Forms.Button();
            this.MenuStrip_Mode = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.iFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eLSEIFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eLSEToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStrip_Cmt = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuItem_EditCmt = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_UpCmt = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_DownCmt = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItem_DeleteCmt = new System.Windows.Forms.ToolStripMenuItem();
            this.checkBox_automode = new System.Windows.Forms.CheckBox();
            this.label_help_notice = new System.Windows.Forms.Label();
            this.Mode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.API = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Comment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.MenuStrip_Item.SuspendLayout();
            this.MenuStrip_Blank.SuspendLayout();
            this.MenuStrip_Mode.SuspendLayout();
            this.MenuStrip_Cmt.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Mode,
            this.API,
            this.Comment});
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(566, 351);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            this.dataGridView1.Click += new System.EventHandler(this.dataGridView1_Click);
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            // 
            // buttonOK
            // 
            this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonOK.Location = new System.Drawing.Point(603, 12);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 1;
            this.buttonOK.Text = "OK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // buttonCancel
            // 
            this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonCancel.Location = new System.Drawing.Point(603, 47);
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.Size = new System.Drawing.Size(75, 23);
            this.buttonCancel.TabIndex = 2;
            this.buttonCancel.Text = "CANCEL";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
            // 
            // MenuStrip_Item
            // 
            this.MenuStrip_Item.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MenuStrip_Item.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_Edit,
            this.MenuItem_ItemSlect,
            this.MenuItem_Up,
            this.MenuItem_Down,
            this.deleteToolStripMenuItem});
            this.MenuStrip_Item.Name = "MenuStrip_Item";
            this.MenuStrip_Item.ShowImageMargin = false;
            this.MenuStrip_Item.Size = new System.Drawing.Size(105, 144);
            // 
            // MenuItem_Edit
            // 
            this.MenuItem_Edit.Name = "MenuItem_Edit";
            this.MenuItem_Edit.Size = new System.Drawing.Size(104, 28);
            this.MenuItem_Edit.Text = "Edit";
            this.MenuItem_Edit.Click += new System.EventHandler(this.MenuItem_Edit_Click);
            // 
            // MenuItem_ItemSlect
            // 
            this.MenuItem_ItemSlect.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.brxToolStripMenuItem});
            this.MenuItem_ItemSlect.Name = "MenuItem_ItemSlect";
            this.MenuItem_ItemSlect.Size = new System.Drawing.Size(104, 28);
            this.MenuItem_ItemSlect.Text = "Select";
            // 
            // brxToolStripMenuItem
            // 
            this.brxToolStripMenuItem.Name = "brxToolStripMenuItem";
            this.brxToolStripMenuItem.Size = new System.Drawing.Size(111, 28);
            this.brxToolStripMenuItem.Text = "br_x";
            // 
            // MenuItem_Up
            // 
            this.MenuItem_Up.Name = "MenuItem_Up";
            this.MenuItem_Up.Size = new System.Drawing.Size(104, 28);
            this.MenuItem_Up.Text = "Up";
            this.MenuItem_Up.Click += new System.EventHandler(this.MenuItem_Up_Click);
            // 
            // MenuItem_Down
            // 
            this.MenuItem_Down.Name = "MenuItem_Down";
            this.MenuItem_Down.Size = new System.Drawing.Size(104, 28);
            this.MenuItem_Down.Text = "Down";
            this.MenuItem_Down.Click += new System.EventHandler(this.MenuItem_Down_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(104, 28);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // MenuStrip_Blank
            // 
            this.MenuStrip_Blank.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MenuStrip_Blank.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_New,
            this.MenuItem_BlankSelect});
            this.MenuStrip_Blank.Name = "contextMenuStrip_Main";
            this.MenuStrip_Blank.ShowImageMargin = false;
            this.MenuStrip_Blank.Size = new System.Drawing.Size(101, 60);
            // 
            // MenuItem_New
            // 
            this.MenuItem_New.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iFToolStripMenuItem1,
            this.eLSEIFToolStripMenuItem1,
            this.eLSEToolStripMenuItem1,
            this.aPIToolStripMenuItem1});
            this.MenuItem_New.Name = "MenuItem_New";
            this.MenuItem_New.Size = new System.Drawing.Size(100, 28);
            this.MenuItem_New.Text = "New";
            // 
            // iFToolStripMenuItem1
            // 
            this.iFToolStripMenuItem1.Name = "iFToolStripMenuItem1";
            this.iFToolStripMenuItem1.Size = new System.Drawing.Size(134, 28);
            this.iFToolStripMenuItem1.Text = "IF";
            this.iFToolStripMenuItem1.Click += new System.EventHandler(this.iFToolStripMenuItem1_Click);
            // 
            // eLSEIFToolStripMenuItem1
            // 
            this.eLSEIFToolStripMenuItem1.Name = "eLSEIFToolStripMenuItem1";
            this.eLSEIFToolStripMenuItem1.Size = new System.Drawing.Size(134, 28);
            this.eLSEIFToolStripMenuItem1.Text = "ELSE IF";
            this.eLSEIFToolStripMenuItem1.Click += new System.EventHandler(this.eLSEIFToolStripMenuItem1_Click);
            // 
            // eLSEToolStripMenuItem1
            // 
            this.eLSEToolStripMenuItem1.Name = "eLSEToolStripMenuItem1";
            this.eLSEToolStripMenuItem1.Size = new System.Drawing.Size(134, 28);
            this.eLSEToolStripMenuItem1.Text = "ELSE";
            this.eLSEToolStripMenuItem1.Click += new System.EventHandler(this.eLSEToolStripMenuItem1_Click);
            // 
            // aPIToolStripMenuItem1
            // 
            this.aPIToolStripMenuItem1.Name = "aPIToolStripMenuItem1";
            this.aPIToolStripMenuItem1.Size = new System.Drawing.Size(134, 28);
            this.aPIToolStripMenuItem1.Text = "API br_";
            this.aPIToolStripMenuItem1.Click += new System.EventHandler(this.aPIToolStripMenuItem1_Click);
            // 
            // MenuItem_BlankSelect
            // 
            this.MenuItem_BlankSelect.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem3});
            this.MenuItem_BlankSelect.Name = "MenuItem_BlankSelect";
            this.MenuItem_BlankSelect.Size = new System.Drawing.Size(100, 28);
            this.MenuItem_BlankSelect.Text = "Select";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(111, 28);
            this.toolStripMenuItem3.Text = "br_x";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 33;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // buttonReset
            // 
            this.buttonReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonReset.Location = new System.Drawing.Point(603, 103);
            this.buttonReset.Name = "buttonReset";
            this.buttonReset.Size = new System.Drawing.Size(75, 23);
            this.buttonReset.TabIndex = 3;
            this.buttonReset.Text = "RESET";
            this.buttonReset.UseVisualStyleBackColor = true;
            this.buttonReset.Click += new System.EventHandler(this.buttonReset_Click);
            // 
            // MenuStrip_Mode
            // 
            this.MenuStrip_Mode.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MenuStrip_Mode.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.iFToolStripMenuItem,
            this.eLSEIFToolStripMenuItem,
            this.eLSEToolStripMenuItem});
            this.MenuStrip_Mode.Name = "MenuStrip_Mode";
            this.MenuStrip_Mode.ShowImageMargin = false;
            this.MenuStrip_Mode.Size = new System.Drawing.Size(109, 88);
            // 
            // iFToolStripMenuItem
            // 
            this.iFToolStripMenuItem.Name = "iFToolStripMenuItem";
            this.iFToolStripMenuItem.Size = new System.Drawing.Size(108, 28);
            this.iFToolStripMenuItem.Text = "IF";
            this.iFToolStripMenuItem.Click += new System.EventHandler(this.iFToolStripMenuItem_Click);
            // 
            // eLSEIFToolStripMenuItem
            // 
            this.eLSEIFToolStripMenuItem.Name = "eLSEIFToolStripMenuItem";
            this.eLSEIFToolStripMenuItem.Size = new System.Drawing.Size(108, 28);
            this.eLSEIFToolStripMenuItem.Text = "ELSE IF";
            this.eLSEIFToolStripMenuItem.Click += new System.EventHandler(this.eLSEIFToolStripMenuItem_Click);
            // 
            // eLSEToolStripMenuItem
            // 
            this.eLSEToolStripMenuItem.Name = "eLSEToolStripMenuItem";
            this.eLSEToolStripMenuItem.Size = new System.Drawing.Size(108, 28);
            this.eLSEToolStripMenuItem.Text = "ELSE";
            this.eLSEToolStripMenuItem.Click += new System.EventHandler(this.eLSEToolStripMenuItem_Click);
            // 
            // MenuStrip_Cmt
            // 
            this.MenuStrip_Cmt.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MenuStrip_Cmt.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItem_EditCmt,
            this.MenuItem_UpCmt,
            this.MenuItem_DownCmt,
            this.MenuItem_DeleteCmt});
            this.MenuStrip_Cmt.Name = "MenuStrip_Item";
            this.MenuStrip_Cmt.ShowImageMargin = false;
            this.MenuStrip_Cmt.Size = new System.Drawing.Size(105, 116);
            // 
            // MenuItem_EditCmt
            // 
            this.MenuItem_EditCmt.Name = "MenuItem_EditCmt";
            this.MenuItem_EditCmt.Size = new System.Drawing.Size(104, 28);
            this.MenuItem_EditCmt.Text = "Edit";
            this.MenuItem_EditCmt.Click += new System.EventHandler(this.MenuItem_EditCmt_Click);
            // 
            // MenuItem_UpCmt
            // 
            this.MenuItem_UpCmt.Name = "MenuItem_UpCmt";
            this.MenuItem_UpCmt.Size = new System.Drawing.Size(104, 28);
            this.MenuItem_UpCmt.Text = "Up";
            this.MenuItem_UpCmt.Click += new System.EventHandler(this.MenuItem_UpCmt_Click);
            // 
            // MenuItem_DownCmt
            // 
            this.MenuItem_DownCmt.Name = "MenuItem_DownCmt";
            this.MenuItem_DownCmt.Size = new System.Drawing.Size(104, 28);
            this.MenuItem_DownCmt.Text = "Down";
            this.MenuItem_DownCmt.Click += new System.EventHandler(this.MenuItem_DownCmt_Click);
            // 
            // MenuItem_DeleteCmt
            // 
            this.MenuItem_DeleteCmt.Name = "MenuItem_DeleteCmt";
            this.MenuItem_DeleteCmt.Size = new System.Drawing.Size(104, 28);
            this.MenuItem_DeleteCmt.Text = "Delete";
            this.MenuItem_DeleteCmt.Click += new System.EventHandler(this.MenuItem_DeleteCmt_Click);
            // 
            // checkBox_automode
            // 
            this.checkBox_automode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBox_automode.AutoSize = true;
            this.checkBox_automode.Checked = true;
            this.checkBox_automode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_automode.Location = new System.Drawing.Point(599, 150);
            this.checkBox_automode.Margin = new System.Windows.Forms.Padding(2);
            this.checkBox_automode.Name = "checkBox_automode";
            this.checkBox_automode.Size = new System.Drawing.Size(79, 16);
            this.checkBox_automode.TabIndex = 4;
            this.checkBox_automode.Tag = "ebf_auto";
            this.checkBox_automode.Text = "Auto Mode";
            this.checkBox_automode.UseVisualStyleBackColor = true;
            this.checkBox_automode.CheckedChanged += new System.EventHandler(this.checkBox_automode_CheckedChanged);
            this.checkBox_automode.Click += new System.EventHandler(this.checkBox_automode_Click);
            // 
            // label_help_notice
            // 
            this.label_help_notice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_help_notice.AutoSize = true;
            this.label_help_notice.BackColor = System.Drawing.Color.Silver;
            this.label_help_notice.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_help_notice.ForeColor = System.Drawing.Color.White;
            this.label_help_notice.Location = new System.Drawing.Point(675, -2);
            this.label_help_notice.Margin = new System.Windows.Forms.Padding(0);
            this.label_help_notice.Name = "label_help_notice";
            this.label_help_notice.Size = new System.Drawing.Size(13, 14);
            this.label_help_notice.TabIndex = 18;
            this.label_help_notice.Text = "?";
            this.label_help_notice.Click += new System.EventHandler(this.label_help_notice_Click);
            // 
            // Mode
            // 
            this.Mode.DividerWidth = 1;
            this.Mode.FillWeight = 50F;
            this.Mode.Frozen = true;
            this.Mode.HeaderText = "Mode";
            this.Mode.Name = "Mode";
            this.Mode.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.Mode.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Mode.Width = 65;
            // 
            // API
            // 
            this.API.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.API.DividerWidth = 1;
            this.API.FillWeight = 50F;
            this.API.Frozen = true;
            this.API.HeaderText = "Condition";
            this.API.MinimumWidth = 260;
            this.API.Name = "API";
            this.API.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.API.Width = 260;
            // 
            // Comment
            // 
            this.Comment.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.Comment.DividerWidth = 1;
            this.Comment.FillWeight = 50F;
            this.Comment.Frozen = true;
            this.Comment.HeaderText = "Comment";
            this.Comment.MinimumWidth = 260;
            this.Comment.Name = "Comment";
            this.Comment.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Comment.Width = 260;
            // 
            // EditBranchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 375);
            this.Controls.Add(this.checkBox_automode);
            this.Controls.Add(this.buttonReset);
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label_help_notice);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditBranchForm";
            this.Tag = "ebf_title";
            this.Text = "Edit Branch";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditBranchForm_FormClosing);
            this.Load += new System.EventHandler(this.EditBranchForm_Load);
            this.Click += new System.EventHandler(this.EditBranchForm_Click);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.MenuStrip_Item.ResumeLayout(false);
            this.MenuStrip_Blank.ResumeLayout(false);
            this.MenuStrip_Mode.ResumeLayout(false);
            this.MenuStrip_Cmt.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Edit;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Up;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_Down;
        private System.Windows.Forms.ToolStripMenuItem brxToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.Timer timer1;
        public System.Windows.Forms.DataGridView dataGridView1;
        public System.Windows.Forms.ContextMenuStrip MenuStrip_Item;
        public System.Windows.Forms.ContextMenuStrip MenuStrip_Blank;
        public System.Windows.Forms.ToolStripMenuItem MenuItem_ItemSlect;
        public System.Windows.Forms.ToolStripMenuItem MenuItem_BlankSelect;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.Button buttonReset;
        private System.Windows.Forms.ToolStripMenuItem iFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eLSEIFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eLSEToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aPIToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem iFToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem eLSEIFToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem eLSEToolStripMenuItem1;
        public System.Windows.Forms.ContextMenuStrip MenuStrip_Mode;
        public System.Windows.Forms.ToolStripMenuItem MenuItem_New;
        public System.Windows.Forms.ContextMenuStrip MenuStrip_Cmt;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_EditCmt;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_UpCmt;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_DownCmt;
        private System.Windows.Forms.ToolStripMenuItem MenuItem_DeleteCmt;
        public System.Windows.Forms.CheckBox checkBox_automode;
        private System.Windows.Forms.Label label_help_notice;
        private System.Windows.Forms.DataGridViewTextBoxColumn Mode;
        private System.Windows.Forms.DataGridViewTextBoxColumn API;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comment;
    }
}