namespace stateview._5000_ViewForm.dialog
{
    partial class SetSourceEditorForm2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetSourceEditorForm2));
            this.textBox_Lang = new System.Windows.Forms.TextBox();
            this.textBox_SetName = new System.Windows.Forms.TextBox();
            this.comboBox_EditorCandidate = new System.Windows.Forms.ComboBox();
            this.textBox_Command = new System.Windows.Forms.TextBox();
            this.textBox_LabelCommand = new System.Windows.Forms.TextBox();
            this.textBox_LabelSetName = new System.Windows.Forms.TextBox();
            this.textBox_LabelLang = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBox_jumpforVS2015 = new System.Windows.Forms.CheckBox();
            this.textBox_LabelEditorCandidate = new System.Windows.Forms.TextBox();
            this.button_Input = new System.Windows.Forms.Button();
            this.listBox_History = new System.Windows.Forms.ListBox();
            this.button_SaveClose = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.label_help = new System.Windows.Forms.Label();
            this.textBox_LabelHistory = new System.Windows.Forms.TextBox();
            this.textBox_SetNameDefault = new System.Windows.Forms.TextBox();
            this.button_changelangfw = new System.Windows.Forms.Button();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.button_cmdreset = new System.Windows.Forms.Button();
            this.button_editor_cand = new System.Windows.Forms.Button();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.checkBox_use_batch = new System.Windows.Forms.CheckBox();
            this.checkBox_usecmn = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox_Lang
            // 
            this.textBox_Lang.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_Lang.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBox_Lang.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_Lang.Location = new System.Drawing.Point(127, 48);
            this.textBox_Lang.Name = "textBox_Lang";
            this.textBox_Lang.ReadOnly = true;
            this.textBox_Lang.Size = new System.Drawing.Size(172, 12);
            this.textBox_Lang.TabIndex = 2;
            this.textBox_Lang.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_Lang.DoubleClick += new System.EventHandler(this.textBox_Lang_DoubleClick);
            this.textBox_Lang.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_Lang_KeyDown);
            // 
            // textBox_SetName
            // 
            this.textBox_SetName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_SetName.Enabled = false;
            this.textBox_SetName.ForeColor = System.Drawing.SystemColors.WindowFrame;
            this.textBox_SetName.Location = new System.Drawing.Point(127, 68);
            this.textBox_SetName.Name = "textBox_SetName";
            this.textBox_SetName.Size = new System.Drawing.Size(172, 19);
            this.textBox_SetName.TabIndex = 3;
            this.textBox_SetName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_SetName.Visible = false;
            this.textBox_SetName.WordWrap = false;
            this.textBox_SetName.Leave += new System.EventHandler(this.textBox_SetName_Leave);
            // 
            // comboBox_EditorCandidate
            // 
            this.comboBox_EditorCandidate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.comboBox_EditorCandidate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_EditorCandidate.FormattingEnabled = true;
            this.comboBox_EditorCandidate.Location = new System.Drawing.Point(127, 267);
            this.comboBox_EditorCandidate.Name = "comboBox_EditorCandidate";
            this.comboBox_EditorCandidate.Size = new System.Drawing.Size(404, 20);
            this.comboBox_EditorCandidate.TabIndex = 5;
            this.comboBox_EditorCandidate.Visible = false;
            // 
            // textBox_Command
            // 
            this.textBox_Command.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_Command.Location = new System.Drawing.Point(127, 104);
            this.textBox_Command.Multiline = true;
            this.textBox_Command.Name = "textBox_Command";
            this.textBox_Command.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox_Command.Size = new System.Drawing.Size(485, 64);
            this.textBox_Command.TabIndex = 7;
            // 
            // textBox_LabelCommand
            // 
            this.textBox_LabelCommand.BackColor = System.Drawing.SystemColors.Control;
            this.textBox_LabelCommand.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_LabelCommand.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_LabelCommand.Location = new System.Drawing.Point(14, 106);
            this.textBox_LabelCommand.Name = "textBox_LabelCommand";
            this.textBox_LabelCommand.ReadOnly = true;
            this.textBox_LabelCommand.Size = new System.Drawing.Size(107, 13);
            this.textBox_LabelCommand.TabIndex = 8;
            this.textBox_LabelCommand.Tag = "ssed_command";
            this.textBox_LabelCommand.Text = "コマンド";
            this.textBox_LabelCommand.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox_LabelSetName
            // 
            this.textBox_LabelSetName.BackColor = System.Drawing.SystemColors.Control;
            this.textBox_LabelSetName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_LabelSetName.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_LabelSetName.Location = new System.Drawing.Point(14, 70);
            this.textBox_LabelSetName.Name = "textBox_LabelSetName";
            this.textBox_LabelSetName.ReadOnly = true;
            this.textBox_LabelSetName.Size = new System.Drawing.Size(107, 13);
            this.textBox_LabelSetName.TabIndex = 9;
            this.textBox_LabelSetName.Text = "設定名";
            this.textBox_LabelSetName.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox_LabelLang
            // 
            this.textBox_LabelLang.BackColor = System.Drawing.SystemColors.Control;
            this.textBox_LabelLang.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_LabelLang.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_LabelLang.Location = new System.Drawing.Point(5, 47);
            this.textBox_LabelLang.Name = "textBox_LabelLang";
            this.textBox_LabelLang.ReadOnly = true;
            this.textBox_LabelLang.Size = new System.Drawing.Size(120, 13);
            this.textBox_LabelLang.TabIndex = 10;
            this.textBox_LabelLang.Tag = "ssed_lang";
            this.textBox_LabelLang.Text = "言語(フレームワーク)";
            this.textBox_LabelLang.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(146, 191);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(260, 24);
            this.label4.TabIndex = 21;
            this.label4.Tag = "ssed_vsdesc";
            this.label4.Text = "Because Visual Studio \'jump to line number\' \r\ncommand option does not work curren" +
    "tly versions";
            // 
            // checkBox_jumpforVS2015
            // 
            this.checkBox_jumpforVS2015.AutoSize = true;
            this.checkBox_jumpforVS2015.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkBox_jumpforVS2015.ForeColor = System.Drawing.Color.Red;
            this.checkBox_jumpforVS2015.Location = new System.Drawing.Point(129, 174);
            this.checkBox_jumpforVS2015.Name = "checkBox_jumpforVS2015";
            this.checkBox_jumpforVS2015.Size = new System.Drawing.Size(372, 15);
            this.checkBox_jumpforVS2015.TabIndex = 20;
            this.checkBox_jumpforVS2015.Tag = "ssed_vscheck";
            this.checkBox_jumpforVS2015.Text = "For Visual Studio  2015 and 2017, call Jump tool after calling the command.";
            this.checkBox_jumpforVS2015.UseVisualStyleBackColor = true;
            // 
            // textBox_LabelEditorCandidate
            // 
            this.textBox_LabelEditorCandidate.BackColor = System.Drawing.SystemColors.Control;
            this.textBox_LabelEditorCandidate.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_LabelEditorCandidate.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_LabelEditorCandidate.Location = new System.Drawing.Point(14, 269);
            this.textBox_LabelEditorCandidate.Name = "textBox_LabelEditorCandidate";
            this.textBox_LabelEditorCandidate.ReadOnly = true;
            this.textBox_LabelEditorCandidate.Size = new System.Drawing.Size(107, 13);
            this.textBox_LabelEditorCandidate.TabIndex = 22;
            this.textBox_LabelEditorCandidate.Tag = "ssed_cand";
            this.textBox_LabelEditorCandidate.Text = "エディタ候補";
            this.textBox_LabelEditorCandidate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBox_LabelEditorCandidate.Visible = false;
            // 
            // button_Input
            // 
            this.button_Input.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Input.Location = new System.Drawing.Point(537, 265);
            this.button_Input.Name = "button_Input";
            this.button_Input.Size = new System.Drawing.Size(75, 23);
            this.button_Input.TabIndex = 23;
            this.button_Input.Tag = "ssed_input";
            this.button_Input.Text = "入力";
            this.button_Input.UseVisualStyleBackColor = true;
            this.button_Input.Visible = false;
            this.button_Input.Click += new System.EventHandler(this.button_Input_Click);
            // 
            // listBox_History
            // 
            this.listBox_History.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBox_History.FormattingEnabled = true;
            this.listBox_History.ItemHeight = 12;
            this.listBox_History.Items.AddRange(new object[] {
            "\"C:\\Program Files\\Microsoft VS Code\\Code.exe\" %1",
            "\"C:\\Program Files (x86)\\Microsoft Visual Studio 14.0\\Common7\\IDE\\devenv.exe\" /Edi" +
                "t %1"});
            this.listBox_History.Location = new System.Drawing.Point(127, 297);
            this.listBox_History.Name = "listBox_History";
            this.listBox_History.Size = new System.Drawing.Size(485, 172);
            this.listBox_History.TabIndex = 24;
            this.listBox_History.DoubleClick += new System.EventHandler(this.listBox_History_DoubleClick);
            // 
            // button_SaveClose
            // 
            this.button_SaveClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_SaveClose.Location = new System.Drawing.Point(361, 9);
            this.button_SaveClose.Name = "button_SaveClose";
            this.button_SaveClose.Size = new System.Drawing.Size(111, 23);
            this.button_SaveClose.TabIndex = 42;
            this.button_SaveClose.Text = "SAVE && CLOSE";
            this.button_SaveClose.UseVisualStyleBackColor = true;
            this.button_SaveClose.Click += new System.EventHandler(this.button_SaveClose_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_Cancel.Location = new System.Drawing.Point(478, 9);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(111, 23);
            this.button_Cancel.TabIndex = 43;
            this.button_Cancel.Text = "CANCEL";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // label_help
            // 
            this.label_help.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label_help.AutoSize = true;
            this.label_help.BackColor = System.Drawing.Color.Silver;
            this.label_help.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_help.ForeColor = System.Drawing.Color.White;
            this.label_help.Location = new System.Drawing.Point(602, 9);
            this.label_help.Margin = new System.Windows.Forms.Padding(0);
            this.label_help.Name = "label_help";
            this.label_help.Size = new System.Drawing.Size(13, 14);
            this.label_help.TabIndex = 44;
            this.label_help.Text = "?";
            this.label_help.Click += new System.EventHandler(this.label_help_Click);
            // 
            // textBox_LabelHistory
            // 
            this.textBox_LabelHistory.BackColor = System.Drawing.SystemColors.Control;
            this.textBox_LabelHistory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_LabelHistory.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox_LabelHistory.Location = new System.Drawing.Point(5, 297);
            this.textBox_LabelHistory.Name = "textBox_LabelHistory";
            this.textBox_LabelHistory.ReadOnly = true;
            this.textBox_LabelHistory.Size = new System.Drawing.Size(107, 13);
            this.textBox_LabelHistory.TabIndex = 45;
            this.textBox_LabelHistory.Tag = "ssed_hist";
            this.textBox_LabelHistory.Text = "履歴";
            this.textBox_LabelHistory.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // textBox_SetNameDefault
            // 
            this.textBox_SetNameDefault.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox_SetNameDefault.BackColor = System.Drawing.SystemColors.HighlightText;
            this.textBox_SetNameDefault.Cursor = System.Windows.Forms.Cursors.Default;
            this.textBox_SetNameDefault.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.textBox_SetNameDefault.Location = new System.Drawing.Point(127, 68);
            this.textBox_SetNameDefault.Name = "textBox_SetNameDefault";
            this.textBox_SetNameDefault.ReadOnly = true;
            this.textBox_SetNameDefault.Size = new System.Drawing.Size(172, 19);
            this.textBox_SetNameDefault.TabIndex = 46;
            this.textBox_SetNameDefault.Text = "(デフォルト)";
            this.textBox_SetNameDefault.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_SetNameDefault.Click += new System.EventHandler(this.textBox_SetNameDefault_Click);
            // 
            // button_changelangfw
            // 
            this.button_changelangfw.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_changelangfw.Location = new System.Drawing.Point(305, 48);
            this.button_changelangfw.Name = "button_changelangfw";
            this.button_changelangfw.Size = new System.Drawing.Size(15, 15);
            this.button_changelangfw.TabIndex = 47;
            this.button_changelangfw.Text = "-";
            this.button_changelangfw.UseVisualStyleBackColor = true;
            this.button_changelangfw.Click += new System.EventHandler(this.button_changelangfw_Click);
            // 
            // button_cmdreset
            // 
            this.button_cmdreset.FlatAppearance.BorderSize = 0;
            this.button_cmdreset.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_cmdreset.Font = new System.Drawing.Font("MS UI Gothic", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_cmdreset.Location = new System.Drawing.Point(75, 128);
            this.button_cmdreset.Name = "button_cmdreset";
            this.button_cmdreset.Size = new System.Drawing.Size(46, 23);
            this.button_cmdreset.TabIndex = 48;
            this.button_cmdreset.Text = "Reset";
            this.button_cmdreset.UseVisualStyleBackColor = true;
            this.button_cmdreset.Click += new System.EventHandler(this.button_cmdreset_Click);
            // 
            // button_editor_cand
            // 
            this.button_editor_cand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button_editor_cand.Location = new System.Drawing.Point(129, 264);
            this.button_editor_cand.Name = "button_editor_cand";
            this.button_editor_cand.Size = new System.Drawing.Size(109, 23);
            this.button_editor_cand.TabIndex = 49;
            this.button_editor_cand.Tag = "ssed_cand";
            this.button_editor_cand.Text = "エディタ候補";
            this.button_editor_cand.UseVisualStyleBackColor = true;
            this.button_editor_cand.Click += new System.EventHandler(this.button_editor_cand_Click);
            // 
            // checkBox_use_batch
            // 
            this.checkBox_use_batch.AutoSize = true;
            this.checkBox_use_batch.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.checkBox_use_batch.ForeColor = System.Drawing.Color.Red;
            this.checkBox_use_batch.Location = new System.Drawing.Point(129, 233);
            this.checkBox_use_batch.Name = "checkBox_use_batch";
            this.checkBox_use_batch.Size = new System.Drawing.Size(142, 15);
            this.checkBox_use_batch.TabIndex = 50;
            this.checkBox_use_batch.Tag = "ssed_usebatch";
            this.checkBox_use_batch.Text = "Use batch to open editor.";
            this.checkBox_use_batch.UseVisualStyleBackColor = true;
            // 
            // checkBox_usecmn
            // 
            this.checkBox_usecmn.AutoSize = true;
            this.checkBox_usecmn.Location = new System.Drawing.Point(127, 16);
            this.checkBox_usecmn.Name = "checkBox_usecmn";
            this.checkBox_usecmn.Size = new System.Drawing.Size(117, 16);
            this.checkBox_usecmn.TabIndex = 51;
            this.checkBox_usecmn.Tag = "ssed_usecmn";
            this.checkBox_usecmn.Text = "checkBox_usecmn";
            this.checkBox_usecmn.UseVisualStyleBackColor = true;
            this.checkBox_usecmn.CheckedChanged += new System.EventHandler(this.checkBox_usecmn_CheckedChanged);
            // 
            // SetSourceEditorForm2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(624, 484);
            this.Controls.Add(this.checkBox_usecmn);
            this.Controls.Add(this.checkBox_use_batch);
            this.Controls.Add(this.button_editor_cand);
            this.Controls.Add(this.button_cmdreset);
            this.Controls.Add(this.button_changelangfw);
            this.Controls.Add(this.textBox_SetNameDefault);
            this.Controls.Add(this.textBox_LabelHistory);
            this.Controls.Add(this.label_help);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_SaveClose);
            this.Controls.Add(this.listBox_History);
            this.Controls.Add(this.button_Input);
            this.Controls.Add(this.textBox_LabelEditorCandidate);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.checkBox_jumpforVS2015);
            this.Controls.Add(this.textBox_LabelLang);
            this.Controls.Add(this.textBox_LabelSetName);
            this.Controls.Add(this.textBox_LabelCommand);
            this.Controls.Add(this.textBox_Command);
            this.Controls.Add(this.comboBox_EditorCandidate);
            this.Controls.Add(this.textBox_SetName);
            this.Controls.Add(this.textBox_Lang);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SetSourceEditorForm2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Tag = "ssed_title";
            this.Text = "Set a source editor path";
            this.Load += new System.EventHandler(this.SetSourceEditorForm2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBox_Lang;
        private System.Windows.Forms.TextBox textBox_SetName;
        private System.Windows.Forms.ComboBox comboBox_EditorCandidate;
        private System.Windows.Forms.TextBox textBox_Command;
        private System.Windows.Forms.TextBox textBox_LabelCommand;
        private System.Windows.Forms.TextBox textBox_LabelSetName;
        private System.Windows.Forms.TextBox textBox_LabelLang;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBox_jumpforVS2015;
        private System.Windows.Forms.TextBox textBox_LabelEditorCandidate;
        private System.Windows.Forms.Button button_Input;
        private System.Windows.Forms.ListBox listBox_History;
        private System.Windows.Forms.Button button_SaveClose;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Label label_help;
        private System.Windows.Forms.TextBox textBox_LabelHistory;
        private System.Windows.Forms.TextBox textBox_SetNameDefault;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.Button button_changelangfw;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.Button button_cmdreset;
        private System.Windows.Forms.Button button_editor_cand;
        private System.Windows.Forms.CheckBox checkBox_use_batch;
        private System.Windows.Forms.CheckBox checkBox_usecmn;
    }
}