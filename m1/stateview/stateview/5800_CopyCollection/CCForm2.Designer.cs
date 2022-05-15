namespace stateview._5800_CopyCollection
{
    partial class CCForm2
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CCForm2));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.DisplayNum = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ListOrder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IconImage = new System.Windows.Forms.DataGridViewImageColumn();
            this.UUID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Comment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastUpdate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CopyCounter = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.textBoxFolderComment = new System.Windows.Forms.TextBox();
            this.labelIconSize = new System.Windows.Forms.Label();
            this.buttonEdit = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStripDg = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bitmapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.titleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.upToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.downToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.explorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.radioButtonCollection = new System.Windows.Forms.RadioButton();
            this.radioButtonCache = new System.Windows.Forms.RadioButton();
            this.radioButtonTrash = new System.Windows.Forms.RadioButton();
            this.button_delall = new System.Windows.Forms.Button();
            this.listBox_collection = new System.Windows.Forms.ListBox();
            this.radioButtonPreset = new System.Windows.Forms.RadioButton();
            this.textBox_readonlyinfo = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStripDg.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.DisplayNum,
            this.ListOrder,
            this.IconImage,
            this.UUID,
            this.Comment,
            this.LastUpdate,
            this.CopyCounter});
            this.dataGridView1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridView1.Location = new System.Drawing.Point(8, 117);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 21;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(353, 423);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseDown);
            this.dataGridView1.MouseLeave += new System.EventHandler(this.dataGridView1_MouseLeave);
            this.dataGridView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseUp);
            // 
            // DisplayNum
            // 
            this.DisplayNum.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DisplayNum.DefaultCellStyle = dataGridViewCellStyle3;
            this.DisplayNum.FillWeight = 40F;
            this.DisplayNum.HeaderText = "no";
            this.DisplayNum.Name = "DisplayNum";
            this.DisplayNum.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DisplayNum.Width = 20;
            // 
            // ListOrder
            // 
            this.ListOrder.HeaderText = "ListOrder";
            this.ListOrder.Name = "ListOrder";
            this.ListOrder.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.ListOrder.Visible = false;
            // 
            // IconImage
            // 
            this.IconImage.HeaderText = "Icon";
            this.IconImage.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.IconImage.Name = "IconImage";
            this.IconImage.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // UUID
            // 
            this.UUID.HeaderText = "UUID";
            this.UUID.Name = "UUID";
            this.UUID.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.UUID.Visible = false;
            // 
            // Comment
            // 
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.Comment.DefaultCellStyle = dataGridViewCellStyle4;
            this.Comment.HeaderText = "Title";
            this.Comment.Name = "Comment";
            this.Comment.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.Comment.Width = 150;
            // 
            // LastUpdate
            // 
            this.LastUpdate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.LastUpdate.HeaderText = "Date";
            this.LastUpdate.Name = "LastUpdate";
            this.LastUpdate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.LastUpdate.Width = 36;
            // 
            // CopyCounter
            // 
            this.CopyCounter.HeaderText = "Count";
            this.CopyCounter.Name = "CopyCounter";
            this.CopyCounter.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.CopyCounter.Width = 40;
            // 
            // textBoxFolderComment
            // 
            this.textBoxFolderComment.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxFolderComment.BackColor = System.Drawing.SystemColors.Info;
            this.textBoxFolderComment.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxFolderComment.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBoxFolderComment.Location = new System.Drawing.Point(152, 12);
            this.textBoxFolderComment.Multiline = true;
            this.textBoxFolderComment.Name = "textBoxFolderComment";
            this.textBoxFolderComment.ReadOnly = true;
            this.textBoxFolderComment.Size = new System.Drawing.Size(204, 80);
            this.textBoxFolderComment.TabIndex = 2;
            // 
            // labelIconSize
            // 
            this.labelIconSize.BackColor = System.Drawing.Color.Teal;
            this.labelIconSize.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelIconSize.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelIconSize.Location = new System.Drawing.Point(9, 101);
            this.labelIconSize.Margin = new System.Windows.Forms.Padding(0);
            this.labelIconSize.Name = "labelIconSize";
            this.labelIconSize.Size = new System.Drawing.Size(15, 15);
            this.labelIconSize.TabIndex = 3;
            this.labelIconSize.Text = "S";
            this.labelIconSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelIconSize.Click += new System.EventHandler(this.labelIconSize_Click);
            // 
            // buttonEdit
            // 
            this.buttonEdit.Location = new System.Drawing.Point(68, 91);
            this.buttonEdit.Name = "buttonEdit";
            this.buttonEdit.Size = new System.Drawing.Size(78, 20);
            this.buttonEdit.TabIndex = 4;
            this.buttonEdit.Tag = "ccwed";
            this.buttonEdit.Text = "Edit";
            this.buttonEdit.UseVisualStyleBackColor = true;
            this.buttonEdit.Click += new System.EventHandler(this.buttonEdit_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // contextMenuStripDg
            // 
            this.contextMenuStripDg.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem,
            this.cutToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.deleteToolStripMenuItem,
            this.editToolStripMenuItem,
            this.upToolStripMenuItem,
            this.downToolStripMenuItem,
            this.explorerToolStripMenuItem,
            this.cancelToolStripMenuItem});
            this.contextMenuStripDg.Name = "contextMenuStripDg";
            this.contextMenuStripDg.ShowImageMargin = false;
            this.contextMenuStripDg.Size = new System.Drawing.Size(120, 256);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(119, 28);
            this.copyToolStripMenuItem.Tag = "cccopy";
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.copyToolStripMenuItem_Click);
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            this.cutToolStripMenuItem.Size = new System.Drawing.Size(119, 28);
            this.cutToolStripMenuItem.Tag = "cccut";
            this.cutToolStripMenuItem.Text = "Cut";
            this.cutToolStripMenuItem.Click += new System.EventHandler(this.cutToolStripMenuItem_Click);
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            this.pasteToolStripMenuItem.Size = new System.Drawing.Size(119, 28);
            this.pasteToolStripMenuItem.Tag = "ccpaste";
            this.pasteToolStripMenuItem.Text = "Paste";
            this.pasteToolStripMenuItem.Click += new System.EventHandler(this.pasteToolStripMenuItem_Click);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(119, 28);
            this.deleteToolStripMenuItem.Tag = "ccdelete";
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bitmapToolStripMenuItem,
            this.titleToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(119, 28);
            this.editToolStripMenuItem.Tag = "ccedit";
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // bitmapToolStripMenuItem
            // 
            this.bitmapToolStripMenuItem.Name = "bitmapToolStripMenuItem";
            this.bitmapToolStripMenuItem.Size = new System.Drawing.Size(134, 28);
            this.bitmapToolStripMenuItem.Tag = "ccebmp";
            this.bitmapToolStripMenuItem.Text = "Bitmap";
            this.bitmapToolStripMenuItem.Click += new System.EventHandler(this.bitmapToolStripMenuItem_Click);
            // 
            // titleToolStripMenuItem
            // 
            this.titleToolStripMenuItem.Name = "titleToolStripMenuItem";
            this.titleToolStripMenuItem.Size = new System.Drawing.Size(134, 28);
            this.titleToolStripMenuItem.Tag = "ccetitle";
            this.titleToolStripMenuItem.Text = "Title";
            this.titleToolStripMenuItem.Click += new System.EventHandler(this.titleToolStripMenuItem_Click);
            // 
            // upToolStripMenuItem
            // 
            this.upToolStripMenuItem.Name = "upToolStripMenuItem";
            this.upToolStripMenuItem.Size = new System.Drawing.Size(119, 28);
            this.upToolStripMenuItem.Tag = "ccup";
            this.upToolStripMenuItem.Text = "Up";
            this.upToolStripMenuItem.Click += new System.EventHandler(this.upToolStripMenuItem_Click);
            // 
            // downToolStripMenuItem
            // 
            this.downToolStripMenuItem.Name = "downToolStripMenuItem";
            this.downToolStripMenuItem.Size = new System.Drawing.Size(119, 28);
            this.downToolStripMenuItem.Tag = "ccdow";
            this.downToolStripMenuItem.Text = "Down";
            this.downToolStripMenuItem.Click += new System.EventHandler(this.downToolStripMenuItem_Click);
            // 
            // explorerToolStripMenuItem
            // 
            this.explorerToolStripMenuItem.Name = "explorerToolStripMenuItem";
            this.explorerToolStripMenuItem.Size = new System.Drawing.Size(119, 28);
            this.explorerToolStripMenuItem.Tag = "ccexplorer";
            this.explorerToolStripMenuItem.Text = "Exprorer";
            this.explorerToolStripMenuItem.Click += new System.EventHandler(this.explorerToolStripMenuItem_Click);
            // 
            // cancelToolStripMenuItem
            // 
            this.cancelToolStripMenuItem.Name = "cancelToolStripMenuItem";
            this.cancelToolStripMenuItem.Size = new System.Drawing.Size(119, 28);
            this.cancelToolStripMenuItem.Tag = "cccnl";
            this.cancelToolStripMenuItem.Text = "Cancel";
            // 
            // radioButtonCollection
            // 
            this.radioButtonCollection.AutoSize = true;
            this.radioButtonCollection.Location = new System.Drawing.Point(72, 12);
            this.radioButtonCollection.Name = "radioButtonCollection";
            this.radioButtonCollection.Size = new System.Drawing.Size(74, 16);
            this.radioButtonCollection.TabIndex = 5;
            this.radioButtonCollection.Tag = "ccwcol";
            this.radioButtonCollection.Text = "Collection";
            this.radioButtonCollection.UseVisualStyleBackColor = true;
            this.radioButtonCollection.CheckedChanged += new System.EventHandler(this.radioButtonCollection_CheckedChanged);
            // 
            // radioButtonCache
            // 
            this.radioButtonCache.AutoSize = true;
            this.radioButtonCache.Checked = true;
            this.radioButtonCache.Location = new System.Drawing.Point(8, 41);
            this.radioButtonCache.Name = "radioButtonCache";
            this.radioButtonCache.Size = new System.Drawing.Size(55, 16);
            this.radioButtonCache.TabIndex = 6;
            this.radioButtonCache.TabStop = true;
            this.radioButtonCache.Tag = "ccwca";
            this.radioButtonCache.Text = "Cache";
            this.radioButtonCache.UseVisualStyleBackColor = true;
            this.radioButtonCache.CheckedChanged += new System.EventHandler(this.radioButtonCollection_CheckedChanged);
            // 
            // radioButtonTrash
            // 
            this.radioButtonTrash.AutoSize = true;
            this.radioButtonTrash.Location = new System.Drawing.Point(8, 70);
            this.radioButtonTrash.Name = "radioButtonTrash";
            this.radioButtonTrash.Size = new System.Drawing.Size(52, 16);
            this.radioButtonTrash.TabIndex = 7;
            this.radioButtonTrash.Tag = "ccwtr";
            this.radioButtonTrash.Text = "Trash";
            this.radioButtonTrash.UseVisualStyleBackColor = true;
            this.radioButtonTrash.CheckedChanged += new System.EventHandler(this.radioButtonCollection_CheckedChanged);
            // 
            // button_delall
            // 
            this.button_delall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_delall.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_delall.Location = new System.Drawing.Point(297, 98);
            this.button_delall.Name = "button_delall";
            this.button_delall.Size = new System.Drawing.Size(59, 18);
            this.button_delall.TabIndex = 10;
            this.button_delall.Tag = "ccwda";
            this.button_delall.Text = "del all";
            this.button_delall.UseVisualStyleBackColor = true;
            this.button_delall.Click += new System.EventHandler(this.button_delall_Click);
            // 
            // listBox_collection
            // 
            this.listBox_collection.FormattingEnabled = true;
            this.listBox_collection.ItemHeight = 12;
            this.listBox_collection.Location = new System.Drawing.Point(69, 34);
            this.listBox_collection.Name = "listBox_collection";
            this.listBox_collection.Size = new System.Drawing.Size(77, 52);
            this.listBox_collection.TabIndex = 11;
            this.listBox_collection.SelectedIndexChanged += new System.EventHandler(this.listBox_collection_SelectedIndexChanged);
            // 
            // radioButtonPreset
            // 
            this.radioButtonPreset.AutoSize = true;
            this.radioButtonPreset.Location = new System.Drawing.Point(8, 12);
            this.radioButtonPreset.Name = "radioButtonPreset";
            this.radioButtonPreset.Size = new System.Drawing.Size(56, 16);
            this.radioButtonPreset.TabIndex = 12;
            this.radioButtonPreset.Tag = "ccwps";
            this.radioButtonPreset.Text = "Preset";
            this.radioButtonPreset.UseVisualStyleBackColor = true;
            this.radioButtonPreset.CheckedChanged += new System.EventHandler(this.radioButtonCollection_CheckedChanged);
            // 
            // textBox_readonlyinfo
            // 
            this.textBox_readonlyinfo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_readonlyinfo.BackColor = System.Drawing.SystemColors.Info;
            this.textBox_readonlyinfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_readonlyinfo.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBox_readonlyinfo.Location = new System.Drawing.Point(66, 31);
            this.textBox_readonlyinfo.Multiline = true;
            this.textBox_readonlyinfo.Name = "textBox_readonlyinfo";
            this.textBox_readonlyinfo.ReadOnly = true;
            this.textBox_readonlyinfo.Size = new System.Drawing.Size(290, 80);
            this.textBox_readonlyinfo.TabIndex = 13;
            this.textBox_readonlyinfo.Tag = "ccpresetdesc";
            this.textBox_readonlyinfo.Text = "スタートキットのプリセットのみを表示中です。\r\n編集したい場合は、ツールよりCCワークフォルダを作成してください。";
            // 
            // CCForm2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 552);
            this.Controls.Add(this.textBox_readonlyinfo);
            this.Controls.Add(this.radioButtonPreset);
            this.Controls.Add(this.listBox_collection);
            this.Controls.Add(this.button_delall);
            this.Controls.Add(this.radioButtonTrash);
            this.Controls.Add(this.radioButtonCache);
            this.Controls.Add(this.radioButtonCollection);
            this.Controls.Add(this.buttonEdit);
            this.Controls.Add(this.labelIconSize);
            this.Controls.Add(this.textBoxFolderComment);
            this.Controls.Add(this.dataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CCForm2";
            this.Tag = "cc";
            this.Text = "CC";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CCForm2_FormClosing);
            this.Load += new System.EventHandler(this.CCForm2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStripDg.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBoxFolderComment;
        private System.Windows.Forms.Label labelIconSize;
        private System.Windows.Forms.Button buttonEdit;
        private System.Windows.Forms.Timer timer1;
        public System.Windows.Forms.DataGridView dataGridView1;
        public System.Windows.Forms.ContextMenuStrip contextMenuStripDg;
        private System.Windows.Forms.RadioButton radioButtonCollection;
        private System.Windows.Forms.RadioButton radioButtonCache;
        private System.Windows.Forms.RadioButton radioButtonTrash;
        public System.Windows.Forms.ToolStripMenuItem bitmapToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem titleToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem cancelToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem upToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem downToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn DisplayNum;
        private System.Windows.Forms.DataGridViewTextBoxColumn ListOrder;
        private System.Windows.Forms.DataGridViewImageColumn IconImage;
        private System.Windows.Forms.DataGridViewTextBoxColumn UUID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comment;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastUpdate;
        private System.Windows.Forms.DataGridViewTextBoxColumn CopyCounter;
        private System.Windows.Forms.Button button_delall;
        private System.Windows.Forms.ToolStripMenuItem explorerToolStripMenuItem;
        private System.Windows.Forms.ListBox listBox_collection;
        private System.Windows.Forms.RadioButton radioButtonPreset;
        private System.Windows.Forms.TextBox textBox_readonlyinfo;
    }
}