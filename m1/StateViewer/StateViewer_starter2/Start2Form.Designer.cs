namespace StateViewer_starter2
{
    partial class Start2Form
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Start2Form));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelFileNameOnHistory = new System.Windows.Forms.Label();
            this.labelSelectOnHistory = new System.Windows.Forms.Label();
            this.buttonOpenInHistory = new System.Windows.Forms.Button();
            this.comboBoxHistory = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.buttonOpenDialog = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelReadFrom = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBoxSkipCopyStateManager = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonOpenGenDirDialog = new System.Windows.Forms.Button();
            this.textBoxGenerateFolder = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonOpenDistDirDialog = new System.Windows.Forms.Button();
            this.textBoxExcelFolder = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxPrefix = new System.Windows.Forms.TextBox();
            this.textBoxDetail = new System.Windows.Forms.TextBox();
            this.listBox_title = new System.Windows.Forms.ListBox();
            this.buttonCreate = new System.Windows.Forms.Button();
            this.labelVersion = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.labelJP = new System.Windows.Forms.Label();
            this.labelEN = new System.Windows.Forms.Label();
            this.labelBuildTime = new System.Windows.Forms.Label();
            this.folderBrowserDialogTemplate = new System.Windows.Forms.FolderBrowserDialog();
            this.buttonCreateNew = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label_help_new = new System.Windows.Forms.Label();
            this.label_help = new System.Windows.Forms.Label();
            this.textBox_drop = new System.Windows.Forms.TextBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.textBox_dropAnalize = new System.Windows.Forms.TextBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.textBox_findfile_exp = new System.Windows.Forms.TextBox();
            this.textBox_findfilename = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.listBox_candidates = new System.Windows.Forms.ListBox();
            this.label10 = new System.Windows.Forms.Label();
            this.button_search = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label_collapse = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.White;
            this.groupBox2.Controls.Add(this.labelFileNameOnHistory);
            this.groupBox2.Controls.Add(this.labelSelectOnHistory);
            this.groupBox2.Controls.Add(this.buttonOpenInHistory);
            this.groupBox2.Controls.Add(this.comboBoxHistory);
            this.groupBox2.Font = new System.Drawing.Font("Calibri", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(11, 182);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBox2.Size = new System.Drawing.Size(517, 108);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "[S00]Open a state-chart excel file in history";
            // 
            // labelFileNameOnHistory
            // 
            this.labelFileNameOnHistory.AutoSize = true;
            this.labelFileNameOnHistory.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelFileNameOnHistory.Location = new System.Drawing.Point(6, 19);
            this.labelFileNameOnHistory.Name = "labelFileNameOnHistory";
            this.labelFileNameOnHistory.Size = new System.Drawing.Size(65, 15);
            this.labelFileNameOnHistory.TabIndex = 4;
            this.labelFileNameOnHistory.Text = " filename";
            // 
            // labelSelectOnHistory
            // 
            this.labelSelectOnHistory.AutoSize = true;
            this.labelSelectOnHistory.Font = new System.Drawing.Font("Meiryo UI", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelSelectOnHistory.Location = new System.Drawing.Point(8, 31);
            this.labelSelectOnHistory.Name = "labelSelectOnHistory";
            this.labelSelectOnHistory.Size = new System.Drawing.Size(40, 12);
            this.labelSelectOnHistory.TabIndex = 3;
            this.labelSelectOnHistory.Text = " fullpath";
            // 
            // buttonOpenInHistory
            // 
            this.buttonOpenInHistory.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOpenInHistory.Location = new System.Drawing.Point(359, 71);
            this.buttonOpenInHistory.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.buttonOpenInHistory.Name = "buttonOpenInHistory";
            this.buttonOpenInHistory.Size = new System.Drawing.Size(141, 34);
            this.buttonOpenInHistory.TabIndex = 2;
            this.buttonOpenInHistory.Text = "[S13]Open File";
            this.buttonOpenInHistory.UseVisualStyleBackColor = true;
            this.buttonOpenInHistory.Click += new System.EventHandler(this.buttonOpenInHistory_Click);
            // 
            // comboBoxHistory
            // 
            this.comboBoxHistory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxHistory.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxHistory.FormattingEnabled = true;
            this.comboBoxHistory.Location = new System.Drawing.Point(27, 76);
            this.comboBoxHistory.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.comboBoxHistory.Name = "comboBoxHistory";
            this.comboBoxHistory.Size = new System.Drawing.Size(313, 22);
            this.comboBoxHistory.TabIndex = 1;
            this.comboBoxHistory.SelectedIndexChanged += new System.EventHandler(this.comboBoxHistory_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.White;
            this.groupBox3.Controls.Add(this.buttonOpenDialog);
            this.groupBox3.Font = new System.Drawing.Font("Calibri", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(10, 100);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBox3.Size = new System.Drawing.Size(517, 77);
            this.groupBox3.TabIndex = 7;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "[S01]Open a existing state-chart excel file";
            // 
            // buttonOpenDialog
            // 
            this.buttonOpenDialog.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOpenDialog.Location = new System.Drawing.Point(29, 23);
            this.buttonOpenDialog.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.buttonOpenDialog.Name = "buttonOpenDialog";
            this.buttonOpenDialog.Size = new System.Drawing.Size(446, 40);
            this.buttonOpenDialog.TabIndex = 3;
            this.buttonOpenDialog.Text = "[S12]Open File Dialog";
            this.buttonOpenDialog.UseVisualStyleBackColor = true;
            this.buttonOpenDialog.Click += new System.EventHandler(this.buttonOpenDialog_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.labelReadFrom);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.checkBoxSkipCopyStateManager);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.buttonOpenGenDirDialog);
            this.groupBox1.Controls.Add(this.textBoxGenerateFolder);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.buttonOpenDistDirDialog);
            this.groupBox1.Controls.Add(this.textBoxExcelFolder);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBoxPrefix);
            this.groupBox1.Controls.Add(this.textBoxDetail);
            this.groupBox1.Controls.Add(this.listBox_title);
            this.groupBox1.Controls.Add(this.buttonCreate);
            this.groupBox1.Font = new System.Drawing.Font("Calibri", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(15, 629);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBox1.Size = new System.Drawing.Size(517, 552);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "[S02]Create a new state-chart excel file";
            // 
            // labelReadFrom
            // 
            this.labelReadFrom.AutoSize = true;
            this.labelReadFrom.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelReadFrom.ForeColor = System.Drawing.Color.Coral;
            this.labelReadFrom.Location = new System.Drawing.Point(7, 18);
            this.labelReadFrom.Name = "labelReadFrom";
            this.labelReadFrom.Size = new System.Drawing.Size(63, 14);
            this.labelReadFrom.TabIndex = 25;
            this.labelReadFrom.Text = "Read from";
            this.labelReadFrom.Click += new System.EventHandler(this.labelReadFrom_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(95, 260);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(320, 14);
            this.label9.TabIndex = 24;
            this.label9.Text = "[S10]\"Undefined\" will be set when you fill out  the blanks.";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label8.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label8.Location = new System.Drawing.Point(93, 400);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 12);
            this.label8.TabIndex = 23;
            this.label8.Text = "GENDIR";
            this.label8.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label7.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label7.Location = new System.Drawing.Point(96, 348);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(44, 12);
            this.label7.TabIndex = 22;
            this.label7.Text = "XLSDIR";
            this.label7.Visible = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label6.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.label6.Location = new System.Drawing.Point(197, 286);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 12);
            this.label6.TabIndex = 21;
            this.label6.Text = "PREFIX";
            this.label6.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Tomato;
            this.label5.Location = new System.Drawing.Point(131, 452);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(299, 14);
            this.label5.TabIndex = 19;
            this.label5.Text = "[S07]※ You may skip to copy, if you have own manager.";
            // 
            // checkBoxSkipCopyStateManager
            // 
            this.checkBoxSkipCopyStateManager.AutoSize = true;
            this.checkBoxSkipCopyStateManager.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBoxSkipCopyStateManager.Location = new System.Drawing.Point(98, 430);
            this.checkBoxSkipCopyStateManager.Name = "checkBoxSkipCopyStateManager";
            this.checkBoxSkipCopyStateManager.Size = new System.Drawing.Size(215, 18);
            this.checkBoxSkipCopyStateManager.TabIndex = 18;
            this.checkBoxSkipCopyStateManager.Text = "[S06]Skip to copy StateManager file";
            this.checkBoxSkipCopyStateManager.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Tomato;
            this.label1.Location = new System.Drawing.Point(131, 526);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(263, 14);
            this.label1.TabIndex = 17;
            this.label1.Text = "[S08]※ If the files exist, they will not be copied.";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(5, 372);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(352, 14);
            this.label4.TabIndex = 16;
            this.label4.Text = "[S05]Folder for sources generated from the state-chart excel file";
            // 
            // buttonOpenGenDirDialog
            // 
            this.buttonOpenGenDirDialog.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOpenGenDirDialog.Location = new System.Drawing.Point(402, 394);
            this.buttonOpenGenDirDialog.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.buttonOpenGenDirDialog.Name = "buttonOpenGenDirDialog";
            this.buttonOpenGenDirDialog.Size = new System.Drawing.Size(87, 40);
            this.buttonOpenGenDirDialog.TabIndex = 15;
            this.buttonOpenGenDirDialog.Text = "FOLDER";
            this.buttonOpenGenDirDialog.UseVisualStyleBackColor = true;
            this.buttonOpenGenDirDialog.Click += new System.EventHandler(this.buttonOpenGenDirDialog_Click);
            // 
            // textBoxGenerateFolder
            // 
            this.textBoxGenerateFolder.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxGenerateFolder.Location = new System.Drawing.Point(145, 397);
            this.textBoxGenerateFolder.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.textBoxGenerateFolder.Name = "textBoxGenerateFolder";
            this.textBoxGenerateFolder.Size = new System.Drawing.Size(250, 22);
            this.textBoxGenerateFolder.TabIndex = 14;
            this.textBoxGenerateFolder.TextChanged += new System.EventHandler(this.textBoxGenerateFolder_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(5, 318);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(334, 14);
            this.label3.TabIndex = 13;
            this.label3.Text = "[S04]Destination folder for the initial state-chart of excel file";
            // 
            // buttonOpenDistDirDialog
            // 
            this.buttonOpenDistDirDialog.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOpenDistDirDialog.Location = new System.Drawing.Point(402, 337);
            this.buttonOpenDistDirDialog.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.buttonOpenDistDirDialog.Name = "buttonOpenDistDirDialog";
            this.buttonOpenDistDirDialog.Size = new System.Drawing.Size(87, 40);
            this.buttonOpenDistDirDialog.TabIndex = 12;
            this.buttonOpenDistDirDialog.Text = "FOLDER";
            this.buttonOpenDistDirDialog.UseVisualStyleBackColor = true;
            this.buttonOpenDistDirDialog.Click += new System.EventHandler(this.buttonOpenDistDirDialog_Click);
            // 
            // textBoxExcelFolder
            // 
            this.textBoxExcelFolder.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxExcelFolder.Location = new System.Drawing.Point(145, 344);
            this.textBoxExcelFolder.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.textBoxExcelFolder.Name = "textBoxExcelFolder";
            this.textBoxExcelFolder.Size = new System.Drawing.Size(250, 22);
            this.textBoxExcelFolder.TabIndex = 11;
            this.textBoxExcelFolder.TextChanged += new System.EventHandler(this.textBoxExcelFolder_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 288);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(184, 14);
            this.label2.TabIndex = 10;
            this.label2.Text = "[S03]State Controller Name Prefix";
            // 
            // textBoxPrefix
            // 
            this.textBoxPrefix.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPrefix.Location = new System.Drawing.Point(249, 282);
            this.textBoxPrefix.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.textBoxPrefix.Name = "textBoxPrefix";
            this.textBoxPrefix.Size = new System.Drawing.Size(250, 22);
            this.textBoxPrefix.TabIndex = 9;
            this.textBoxPrefix.TextChanged += new System.EventHandler(this.textBoxPrefix_TextChanged);
            // 
            // textBoxDetail
            // 
            this.textBoxDetail.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBoxDetail.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxDetail.Location = new System.Drawing.Point(9, 105);
            this.textBoxDetail.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.textBoxDetail.Multiline = true;
            this.textBoxDetail.Name = "textBoxDetail";
            this.textBoxDetail.ReadOnly = true;
            this.textBoxDetail.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxDetail.Size = new System.Drawing.Size(493, 145);
            this.textBoxDetail.TabIndex = 8;
            this.textBoxDetail.WordWrap = false;
            // 
            // listBox_title
            // 
            this.listBox_title.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.listBox_title.FormattingEnabled = true;
            this.listBox_title.ItemHeight = 18;
            this.listBox_title.Items.AddRange(new object[] {
            "　"});
            this.listBox_title.Location = new System.Drawing.Point(10, 37);
            this.listBox_title.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.listBox_title.Name = "listBox_title";
            this.listBox_title.Size = new System.Drawing.Size(492, 58);
            this.listBox_title.TabIndex = 7;
            this.listBox_title.SelectedIndexChanged += new System.EventHandler(this.listBox_title_SelectedIndexChanged);
            // 
            // buttonCreate
            // 
            this.buttonCreate.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCreate.Location = new System.Drawing.Point(28, 477);
            this.buttonCreate.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(463, 40);
            this.buttonCreate.TabIndex = 0;
            this.buttonCreate.Text = "Create";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // labelVersion
            // 
            this.labelVersion.AutoSize = true;
            this.labelVersion.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelVersion.Location = new System.Drawing.Point(12, 2);
            this.labelVersion.Name = "labelVersion";
            this.labelVersion.Size = new System.Drawing.Size(47, 14);
            this.labelVersion.TabIndex = 9;
            this.labelVersion.Text = "version";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // labelJP
            // 
            this.labelJP.AutoSize = true;
            this.labelJP.BackColor = System.Drawing.Color.CadetBlue;
            this.labelJP.Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelJP.ForeColor = System.Drawing.Color.Snow;
            this.labelJP.Location = new System.Drawing.Point(466, 5);
            this.labelJP.Name = "labelJP";
            this.labelJP.Size = new System.Drawing.Size(19, 12);
            this.labelJP.TabIndex = 5;
            this.labelJP.Text = "JP";
            this.labelJP.Click += new System.EventHandler(this.labelJP_Click);
            // 
            // labelEN
            // 
            this.labelEN.AutoSize = true;
            this.labelEN.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelEN.ForeColor = System.Drawing.Color.LightGray;
            this.labelEN.Location = new System.Drawing.Point(493, 4);
            this.labelEN.Name = "labelEN";
            this.labelEN.Size = new System.Drawing.Size(21, 14);
            this.labelEN.TabIndex = 10;
            this.labelEN.Text = "EN";
            this.labelEN.Click += new System.EventHandler(this.labelEN_Click);
            // 
            // labelBuildTime
            // 
            this.labelBuildTime.AutoSize = true;
            this.labelBuildTime.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.labelBuildTime.Location = new System.Drawing.Point(243, 2);
            this.labelBuildTime.Name = "labelBuildTime";
            this.labelBuildTime.Size = new System.Drawing.Size(60, 14);
            this.labelBuildTime.TabIndex = 12;
            this.labelBuildTime.Text = "buildtime";
            // 
            // buttonCreateNew
            // 
            this.buttonCreateNew.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonCreateNew.Location = new System.Drawing.Point(29, 23);
            this.buttonCreateNew.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.buttonCreateNew.Name = "buttonCreateNew";
            this.buttonCreateNew.Size = new System.Drawing.Size(446, 40);
            this.buttonCreateNew.TabIndex = 3;
            this.buttonCreateNew.Text = "[S11]Create new state machine";
            this.buttonCreateNew.UseVisualStyleBackColor = true;
            this.buttonCreateNew.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.White;
            this.groupBox4.Controls.Add(this.label_help_new);
            this.groupBox4.Controls.Add(this.buttonCreateNew);
            this.groupBox4.Font = new System.Drawing.Font("Calibri", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(10, 20);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBox4.Size = new System.Drawing.Size(517, 77);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "[S02]Create a new state-chart excel file";
            // 
            // label_help_new
            // 
            this.label_help_new.AutoSize = true;
            this.label_help_new.BackColor = System.Drawing.Color.Silver;
            this.label_help_new.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_help_new.ForeColor = System.Drawing.Color.White;
            this.label_help_new.Location = new System.Drawing.Point(493, 37);
            this.label_help_new.Margin = new System.Windows.Forms.Padding(0);
            this.label_help_new.Name = "label_help_new";
            this.label_help_new.Size = new System.Drawing.Size(13, 14);
            this.label_help_new.TabIndex = 14;
            this.label_help_new.Text = "?";
            this.label_help_new.Visible = false;
            this.label_help_new.Click += new System.EventHandler(this.label_help_new_Click);
            // 
            // label_help
            // 
            this.label_help.AutoSize = true;
            this.label_help.BackColor = System.Drawing.Color.Silver;
            this.label_help.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_help.ForeColor = System.Drawing.Color.White;
            this.label_help.Location = new System.Drawing.Point(524, 3);
            this.label_help.Margin = new System.Windows.Forms.Padding(0);
            this.label_help.Name = "label_help";
            this.label_help.Size = new System.Drawing.Size(13, 14);
            this.label_help.TabIndex = 13;
            this.label_help.Text = "?";
            this.label_help.Click += new System.EventHandler(this.label_help_Click);
            // 
            // textBox_drop
            // 
            this.textBox_drop.AllowDrop = true;
            this.textBox_drop.BackColor = System.Drawing.SystemColors.Menu;
            this.textBox_drop.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_drop.Font = new System.Drawing.Font("Calibri", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_drop.Location = new System.Drawing.Point(27, 23);
            this.textBox_drop.Multiline = true;
            this.textBox_drop.Name = "textBox_drop";
            this.textBox_drop.Size = new System.Drawing.Size(300, 82);
            this.textBox_drop.TabIndex = 0;
            this.textBox_drop.Text = "[S14]Drop Here";
            this.textBox_drop.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_drop.DragDrop += new System.Windows.Forms.DragEventHandler(this.textBox_drop_DragDrop);
            this.textBox_drop.DragEnter += new System.Windows.Forms.DragEventHandler(this.textBox_drop_DragEnter);
            // 
            // groupBox6
            // 
            this.groupBox6.BackColor = System.Drawing.Color.White;
            this.groupBox6.Controls.Add(this.textBox_dropAnalize);
            this.groupBox6.Controls.Add(this.textBox_drop);
            this.groupBox6.Font = new System.Drawing.Font("Calibri", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox6.Location = new System.Drawing.Point(10, 438);
            this.groupBox6.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBox6.Size = new System.Drawing.Size(517, 115);
            this.groupBox6.TabIndex = 7;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "[S15] Drop and Open File";
            // 
            // textBox_dropAnalize
            // 
            this.textBox_dropAnalize.AllowDrop = true;
            this.textBox_dropAnalize.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBox_dropAnalize.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_dropAnalize.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_dropAnalize.ForeColor = System.Drawing.Color.Red;
            this.textBox_dropAnalize.Location = new System.Drawing.Point(341, 23);
            this.textBox_dropAnalize.Multiline = true;
            this.textBox_dropAnalize.Name = "textBox_dropAnalize";
            this.textBox_dropAnalize.Size = new System.Drawing.Size(159, 82);
            this.textBox_dropAnalize.TabIndex = 1;
            this.textBox_dropAnalize.Text = "[S16]Analize droped file and open StateGo";
            this.textBox_dropAnalize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox5
            // 
            this.groupBox5.BackColor = System.Drawing.Color.White;
            this.groupBox5.Controls.Add(this.textBox_findfile_exp);
            this.groupBox5.Controls.Add(this.textBox_findfilename);
            this.groupBox5.Controls.Add(this.label11);
            this.groupBox5.Controls.Add(this.listBox_candidates);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Controls.Add(this.button_search);
            this.groupBox5.Font = new System.Drawing.Font("Calibri", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox5.Location = new System.Drawing.Point(12, 303);
            this.groupBox5.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Padding = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.groupBox5.Size = new System.Drawing.Size(517, 129);
            this.groupBox5.TabIndex = 14;
            this.groupBox5.TabStop = false;
            this.groupBox5.Tag = "S20";
            this.groupBox5.Text = "[S20]Find File";
            // 
            // textBox_findfile_exp
            // 
            this.textBox_findfile_exp.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_findfile_exp.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_findfile_exp.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.textBox_findfile_exp.Location = new System.Drawing.Point(103, 21);
            this.textBox_findfile_exp.Name = "textBox_findfile_exp";
            this.textBox_findfile_exp.Size = new System.Drawing.Size(300, 15);
            this.textBox_findfile_exp.TabIndex = 8;
            this.textBox_findfile_exp.Tag = "S22";
            this.textBox_findfile_exp.Text = "[S22]Input file name";
            this.textBox_findfile_exp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_findfile_exp.Enter += new System.EventHandler(this.textBox_findfile_exp_Enter);
            // 
            // textBox_findfilename
            // 
            this.textBox_findfilename.BackColor = System.Drawing.SystemColors.Window;
            this.textBox_findfilename.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_findfilename.Location = new System.Drawing.Point(77, 17);
            this.textBox_findfilename.Name = "textBox_findfilename";
            this.textBox_findfilename.Size = new System.Drawing.Size(351, 22);
            this.textBox_findfilename.TabIndex = 5;
            this.textBox_findfilename.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox_findfilename.TextChanged += new System.EventHandler(this.textBox_findfilename_TextChanged);
            this.textBox_findfilename.Leave += new System.EventHandler(this.textBox_findfilename_Leave);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label11.Location = new System.Drawing.Point(7, 78);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(96, 11);
            this.label11.TabIndex = 7;
            this.label11.Tag = "S24";
            this.label11.Text = "[S24]candidates";
            // 
            // listBox_candidates
            // 
            this.listBox_candidates.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBox_candidates.FormattingEnabled = true;
            this.listBox_candidates.HorizontalScrollbar = true;
            this.listBox_candidates.ItemHeight = 14;
            this.listBox_candidates.Location = new System.Drawing.Point(77, 44);
            this.listBox_candidates.Name = "listBox_candidates";
            this.listBox_candidates.Size = new System.Drawing.Size(426, 74);
            this.listBox_candidates.TabIndex = 6;
            this.listBox_candidates.DoubleClick += new System.EventHandler(this.listBox_candidates_DoubleClick);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label10.Location = new System.Drawing.Point(6, 19);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(87, 15);
            this.label10.TabIndex = 4;
            this.label10.Tag = "S21";
            this.label10.Text = " [S21]fname";
            // 
            // button_search
            // 
            this.button_search.Font = new System.Drawing.Font("Calibri", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_search.Location = new System.Drawing.Point(433, 17);
            this.button_search.Margin = new System.Windows.Forms.Padding(0);
            this.button_search.Name = "button_search";
            this.button_search.Size = new System.Drawing.Size(67, 22);
            this.button_search.TabIndex = 2;
            this.button_search.Tag = "S23";
            this.button_search.Text = "[S23]Search";
            this.button_search.UseVisualStyleBackColor = true;
            this.button_search.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label_collapse
            // 
            this.label_collapse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label_collapse.AutoSize = true;
            this.label_collapse.ForeColor = System.Drawing.Color.Brown;
            this.label_collapse.Location = new System.Drawing.Point(475, 554);
            this.label_collapse.Margin = new System.Windows.Forms.Padding(0);
            this.label_collapse.Name = "label_collapse";
            this.label_collapse.Size = new System.Drawing.Size(39, 14);
            this.label_collapse.TabIndex = 15;
            this.label_collapse.Text = "▲Fold";
            this.label_collapse.Click += new System.EventHandler(this.label_collapse_Click);
            // 
            // Start2Form
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(538, 568);
            this.Controls.Add(this.label_collapse);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.label_help);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.labelBuildTime);
            this.Controls.Add(this.labelEN);
            this.Controls.Add(this.labelJP);
            this.Controls.Add(this.labelVersion);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Start2Form";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StateGo Start Dialog";
            this.Load += new System.EventHandler(this.Start2Form_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label labelFileNameOnHistory;
        private System.Windows.Forms.Label labelSelectOnHistory;
        private System.Windows.Forms.ComboBox comboBoxHistory;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.CheckBox checkBoxSkipCopyStateManager;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonOpenGenDirDialog;
        public System.Windows.Forms.TextBox textBoxGenerateFolder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonOpenDistDirDialog;
        public System.Windows.Forms.TextBox textBoxExcelFolder;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox textBoxPrefix;
        public System.Windows.Forms.TextBox textBoxDetail;
        public System.Windows.Forms.ListBox listBox_title;
        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label labelJP;
        private System.Windows.Forms.Label labelEN;
        private System.Windows.Forms.Label labelBuildTime;
        private System.Windows.Forms.Label label9;
        public System.Windows.Forms.Label labelReadFrom;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogTemplate;
        public System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.Button buttonOpenInHistory;
        public System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.Button buttonOpenDialog;
        public System.Windows.Forms.Button buttonCreateNew;
        public System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label_help;
        private System.Windows.Forms.Label label_help_new;
        private System.Windows.Forms.TextBox textBox_drop;
        public System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.TextBox textBox_dropAnalize;
        public System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.Button button_search;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label11;
        public System.Windows.Forms.ListBox listBox_candidates;
        public System.Windows.Forms.TextBox textBox_findfilename;
        public System.Windows.Forms.TextBox textBox_findfile_exp;
        private System.Windows.Forms.Label label_collapse;
    }
}