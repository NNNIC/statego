namespace StateViewer_starter2
{
    partial class CreateNewForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateNewForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button_g1ok = new System.Windows.Forms.Button();
            this.label_g1 = new System.Windows.Forms.Label();
            this.label_help_1 = new System.Windows.Forms.Label();
            this.textBoxReadFromPath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label_folderbrowser = new System.Windows.Forms.Label();
            this.listBox_title = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labelReadFrom = new System.Windows.Forms.Label();
            this.button_g1cancel = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label_g2 = new System.Windows.Forms.Label();
            this.button_shortcut = new System.Windows.Forms.Button();
            this.label_help_2 = new System.Windows.Forms.Label();
            this.textBoxStateMachineName = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button_g2back = new System.Windows.Forms.Button();
            this.textBoxPrefix = new System.Windows.Forms.TextBox();
            this.button_g2ok = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label_g3 = new System.Windows.Forms.Label();
            this.label_help_3 = new System.Windows.Forms.Label();
            this.button_g3clear = new System.Windows.Forms.Button();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.buttonOpenDocFolderDialog = new System.Windows.Forms.Button();
            this.textBoxDocFolder = new System.Windows.Forms.TextBox();
            this.button_g3back = new System.Windows.Forms.Button();
            this.button_g3ok = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label_g4 = new System.Windows.Forms.Label();
            this.label_help_4 = new System.Windows.Forms.Label();
            this.button_g4clear = new System.Windows.Forms.Button();
            this.buttonOpenGenFolderDialog = new System.Windows.Forms.Button();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBoxGenFolder = new System.Windows.Forms.TextBox();
            this.button_g4back = new System.Windows.Forms.Button();
            this.button_g4ok = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label_g5 = new System.Windows.Forms.Label();
            this.label_help_5 = new System.Windows.Forms.Label();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.radioButtonNo = new System.Windows.Forms.RadioButton();
            this.radioButtonYes = new System.Windows.Forms.RadioButton();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.button_g5back = new System.Windows.Forms.Button();
            this.button_g5ok = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label_help_6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.button_g6cancel = new System.Windows.Forms.Button();
            this.textBoxCreateFiles = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.button_g6back = new System.Windows.Forms.Button();
            this.button_g6ok = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.folderBrowserDialogTemplate = new System.Windows.Forms.FolderBrowserDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.folderBrowserDialog2 = new System.Windows.Forms.FolderBrowserDialog();
            this.label_anchor = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button_g1ok);
            this.groupBox1.Controls.Add(this.label_g1);
            this.groupBox1.Controls.Add(this.label_help_1);
            this.groupBox1.Controls.Add(this.textBoxReadFromPath);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label_folderbrowser);
            this.groupBox1.Controls.Add(this.listBox_title);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.labelReadFrom);
            this.groupBox1.Controls.Add(this.button_g1cancel);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(464, 231);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Step 1 of 6";
            // 
            // button_g1ok
            // 
            this.button_g1ok.Location = new System.Drawing.Point(368, 24);
            this.button_g1ok.Name = "button_g1ok";
            this.button_g1ok.Size = new System.Drawing.Size(75, 23);
            this.button_g1ok.TabIndex = 1;
            this.button_g1ok.Text = "NEXT";
            this.button_g1ok.UseVisualStyleBackColor = true;
            this.button_g1ok.Click += new System.EventHandler(this.button_g1ok_Click);
            // 
            // label_g1
            // 
            this.label_g1.AutoSize = true;
            this.label_g1.Font = new System.Drawing.Font("MS UI Gothic", 6.25F);
            this.label_g1.Location = new System.Drawing.Point(371, 11);
            this.label_g1.Margin = new System.Windows.Forms.Padding(0);
            this.label_g1.Name = "label_g1";
            this.label_g1.Size = new System.Drawing.Size(27, 9);
            this.label_g1.TabIndex = 36;
            this.label_g1.Text = "[C21]";
            // 
            // label_help_1
            // 
            this.label_help_1.AutoSize = true;
            this.label_help_1.BackColor = System.Drawing.Color.Silver;
            this.label_help_1.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_help_1.ForeColor = System.Drawing.Color.White;
            this.label_help_1.Location = new System.Drawing.Point(345, 18);
            this.label_help_1.Margin = new System.Windows.Forms.Padding(0);
            this.label_help_1.Name = "label_help_1";
            this.label_help_1.Size = new System.Drawing.Size(13, 14);
            this.label_help_1.TabIndex = 35;
            this.label_help_1.Text = "?";
            this.label_help_1.Click += new System.EventHandler(this.label_help_1_Click);
            // 
            // textBoxReadFromPath
            // 
            this.textBoxReadFromPath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxReadFromPath.Location = new System.Drawing.Point(6, 213);
            this.textBoxReadFromPath.Name = "textBoxReadFromPath";
            this.textBoxReadFromPath.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textBoxReadFromPath.Size = new System.Drawing.Size(352, 12);
            this.textBoxReadFromPath.TabIndex = 34;
            this.textBoxReadFromPath.WordWrap = false;
            this.textBoxReadFromPath.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxReadFromPath_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Coral;
            this.label4.Location = new System.Drawing.Point(71, 194);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 16);
            this.label4.TabIndex = 33;
            this.label4.Text = "reset";
            this.label4.Click += new System.EventHandler(this.label_reset_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Coral;
            this.label1.Location = new System.Drawing.Point(7, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(319, 14);
            this.label1.TabIndex = 32;
            this.label1.Text = "[C02]Show the description by Double-clicking on the item.";
            // 
            // label_folderbrowser
            // 
            this.label_folderbrowser.AutoSize = true;
            this.label_folderbrowser.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label_folderbrowser.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_folderbrowser.ForeColor = System.Drawing.Color.Coral;
            this.label_folderbrowser.Location = new System.Drawing.Point(114, 194);
            this.label_folderbrowser.Name = "label_folderbrowser";
            this.label_folderbrowser.Size = new System.Drawing.Size(89, 16);
            this.label_folderbrowser.TabIndex = 31;
            this.label_folderbrowser.Text = "folder browser";
            this.label_folderbrowser.Click += new System.EventHandler(this.label3_Click);
            // 
            // listBox_title
            // 
            this.listBox_title.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.listBox_title.FormattingEnabled = true;
            this.listBox_title.ItemHeight = 18;
            this.listBox_title.Items.AddRange(new object[] {
            "　"});
            this.listBox_title.Location = new System.Drawing.Point(6, 62);
            this.listBox_title.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.listBox_title.Name = "listBox_title";
            this.listBox_title.Size = new System.Drawing.Size(356, 130);
            this.listBox_title.TabIndex = 30;
            this.listBox_title.SelectedIndexChanged += new System.EventHandler(this.listBox_title_SelectedIndexChanged);
            this.listBox_title.DoubleClick += new System.EventHandler(this.listBox_title_DoubleClick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(6, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(247, 23);
            this.label2.TabIndex = 29;
            this.label2.Text = "[C01]Select a language sample.";
            // 
            // labelReadFrom
            // 
            this.labelReadFrom.AutoSize = true;
            this.labelReadFrom.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelReadFrom.ForeColor = System.Drawing.Color.Coral;
            this.labelReadFrom.Location = new System.Drawing.Point(7, 196);
            this.labelReadFrom.Margin = new System.Windows.Forms.Padding(3, 0, 30, 0);
            this.labelReadFrom.Name = "labelReadFrom";
            this.labelReadFrom.Size = new System.Drawing.Size(63, 14);
            this.labelReadFrom.TabIndex = 26;
            this.labelReadFrom.Text = "Read from";
            // 
            // button_g1cancel
            // 
            this.button_g1cancel.Location = new System.Drawing.Point(368, 84);
            this.button_g1cancel.Name = "button_g1cancel";
            this.button_g1cancel.Size = new System.Drawing.Size(75, 23);
            this.button_g1cancel.TabIndex = 2;
            this.button_g1cancel.Text = "CANCEL";
            this.button_g1cancel.UseVisualStyleBackColor = true;
            this.button_g1cancel.Click += new System.EventHandler(this.button_g1cancel_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label_g2);
            this.groupBox2.Controls.Add(this.button_shortcut);
            this.groupBox2.Controls.Add(this.label_help_2);
            this.groupBox2.Controls.Add(this.textBoxStateMachineName);
            this.groupBox2.Controls.Add(this.textBox3);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.button_g2back);
            this.groupBox2.Controls.Add(this.textBoxPrefix);
            this.groupBox2.Controls.Add(this.button_g2ok);
            this.groupBox2.Location = new System.Drawing.Point(537, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(464, 231);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Step 2 of 6";
            // 
            // label_g2
            // 
            this.label_g2.AutoSize = true;
            this.label_g2.Font = new System.Drawing.Font("MS UI Gothic", 6.25F);
            this.label_g2.Location = new System.Drawing.Point(366, 11);
            this.label_g2.Margin = new System.Windows.Forms.Padding(0);
            this.label_g2.Name = "label_g2";
            this.label_g2.Size = new System.Drawing.Size(27, 9);
            this.label_g2.TabIndex = 37;
            this.label_g2.Text = "[C22]";
            // 
            // button_shortcut
            // 
            this.button_shortcut.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_shortcut.Font = new System.Drawing.Font("MS UI Gothic", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.button_shortcut.ForeColor = System.Drawing.Color.Red;
            this.button_shortcut.Location = new System.Drawing.Point(361, 101);
            this.button_shortcut.Name = "button_shortcut";
            this.button_shortcut.Size = new System.Drawing.Size(86, 23);
            this.button_shortcut.TabIndex = 37;
            this.button_shortcut.Text = "[C12]sc";
            this.button_shortcut.UseVisualStyleBackColor = true;
            this.button_shortcut.Click += new System.EventHandler(this.button_shortcut_Click);
            // 
            // label_help_2
            // 
            this.label_help_2.AutoSize = true;
            this.label_help_2.BackColor = System.Drawing.Color.Silver;
            this.label_help_2.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_help_2.ForeColor = System.Drawing.Color.White;
            this.label_help_2.Location = new System.Drawing.Point(341, 18);
            this.label_help_2.Margin = new System.Windows.Forms.Padding(0);
            this.label_help_2.Name = "label_help_2";
            this.label_help_2.Size = new System.Drawing.Size(13, 14);
            this.label_help_2.TabIndex = 36;
            this.label_help_2.Text = "?";
            this.label_help_2.Click += new System.EventHandler(this.label_help_2_Click);
            // 
            // textBoxStateMachineName
            // 
            this.textBoxStateMachineName.Font = new System.Drawing.Font("MS UI Gothic", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxStateMachineName.Location = new System.Drawing.Point(26, 101);
            this.textBoxStateMachineName.Name = "textBoxStateMachineName";
            this.textBoxStateMachineName.Size = new System.Drawing.Size(307, 34);
            this.textBoxStateMachineName.TabIndex = 7;
            this.textBoxStateMachineName.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxStateMachineName.TextChanged += new System.EventHandler(this.textBoxStateMachineName_TextChanged);
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.SystemColors.Control;
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.Font = new System.Drawing.Font("MS UI Gothic", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBox3.Location = new System.Drawing.Point(246, 104);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(93, 27);
            this.textBox3.TabIndex = 6;
            this.textBox3.Text = "Control";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(26, 23);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(307, 55);
            this.textBox1.TabIndex = 5;
            this.textBox1.Text = "[C03]Specify the state machine name.\r\n";
            // 
            // button_g2back
            // 
            this.button_g2back.Location = new System.Drawing.Point(365, 54);
            this.button_g2back.Name = "button_g2back";
            this.button_g2back.Size = new System.Drawing.Size(75, 23);
            this.button_g2back.TabIndex = 4;
            this.button_g2back.Text = "BACK";
            this.button_g2back.UseVisualStyleBackColor = true;
            this.button_g2back.Click += new System.EventHandler(this.button_g2back_Click);
            // 
            // textBoxPrefix
            // 
            this.textBoxPrefix.Font = new System.Drawing.Font("MS UI Gothic", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxPrefix.Location = new System.Drawing.Point(30, 101);
            this.textBoxPrefix.Name = "textBoxPrefix";
            this.textBoxPrefix.Size = new System.Drawing.Size(212, 34);
            this.textBoxPrefix.TabIndex = 1;
            this.textBoxPrefix.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.textBoxPrefix.TextChanged += new System.EventHandler(this.textBoxPrefix_TextChanged);
            // 
            // button_g2ok
            // 
            this.button_g2ok.Location = new System.Drawing.Point(365, 24);
            this.button_g2ok.Name = "button_g2ok";
            this.button_g2ok.Size = new System.Drawing.Size(75, 23);
            this.button_g2ok.TabIndex = 3;
            this.button_g2ok.Text = "NEXT";
            this.button_g2ok.UseVisualStyleBackColor = true;
            this.button_g2ok.Click += new System.EventHandler(this.button_g2ok_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label_g3);
            this.groupBox3.Controls.Add(this.label_help_3);
            this.groupBox3.Controls.Add(this.button_g3clear);
            this.groupBox3.Controls.Add(this.textBox4);
            this.groupBox3.Controls.Add(this.buttonOpenDocFolderDialog);
            this.groupBox3.Controls.Add(this.textBoxDocFolder);
            this.groupBox3.Controls.Add(this.button_g3back);
            this.groupBox3.Controls.Add(this.button_g3ok);
            this.groupBox3.Location = new System.Drawing.Point(12, 259);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(464, 231);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Step 3 of 6";
            // 
            // label_g3
            // 
            this.label_g3.AutoSize = true;
            this.label_g3.Font = new System.Drawing.Font("MS UI Gothic", 6.25F);
            this.label_g3.Location = new System.Drawing.Point(371, 10);
            this.label_g3.Margin = new System.Windows.Forms.Padding(0);
            this.label_g3.Name = "label_g3";
            this.label_g3.Size = new System.Drawing.Size(27, 9);
            this.label_g3.TabIndex = 38;
            this.label_g3.Text = "[C23]";
            // 
            // label_help_3
            // 
            this.label_help_3.AutoSize = true;
            this.label_help_3.BackColor = System.Drawing.Color.Silver;
            this.label_help_3.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_help_3.ForeColor = System.Drawing.Color.White;
            this.label_help_3.Location = new System.Drawing.Point(339, 19);
            this.label_help_3.Margin = new System.Windows.Forms.Padding(0);
            this.label_help_3.Name = "label_help_3";
            this.label_help_3.Size = new System.Drawing.Size(13, 14);
            this.label_help_3.TabIndex = 37;
            this.label_help_3.Text = "?";
            this.label_help_3.Click += new System.EventHandler(this.label_help_3_Click);
            // 
            // button_g3clear
            // 
            this.button_g3clear.Location = new System.Drawing.Point(364, 183);
            this.button_g3clear.Name = "button_g3clear";
            this.button_g3clear.Size = new System.Drawing.Size(75, 23);
            this.button_g3clear.TabIndex = 16;
            this.button_g3clear.Text = "CLEAR";
            this.button_g3clear.UseVisualStyleBackColor = true;
            this.button_g3clear.Click += new System.EventHandler(this.button_g3clear_Click);
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.SystemColors.Control;
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox4.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox4.Location = new System.Drawing.Point(31, 22);
            this.textBox4.Multiline = true;
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(307, 55);
            this.textBox4.TabIndex = 15;
            this.textBox4.Text = "[C04]Specify the document folder.";
            // 
            // buttonOpenDocFolderDialog
            // 
            this.buttonOpenDocFolderDialog.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOpenDocFolderDialog.Location = new System.Drawing.Point(169, 183);
            this.buttonOpenDocFolderDialog.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.buttonOpenDocFolderDialog.Name = "buttonOpenDocFolderDialog";
            this.buttonOpenDocFolderDialog.Size = new System.Drawing.Size(87, 40);
            this.buttonOpenDocFolderDialog.TabIndex = 14;
            this.buttonOpenDocFolderDialog.Text = "FOLDER";
            this.buttonOpenDocFolderDialog.UseVisualStyleBackColor = true;
            this.buttonOpenDocFolderDialog.Click += new System.EventHandler(this.buttonOpenDocFolderDialog_Click);
            // 
            // textBoxDocFolder
            // 
            this.textBoxDocFolder.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDocFolder.Location = new System.Drawing.Point(10, 85);
            this.textBoxDocFolder.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.textBoxDocFolder.Multiline = true;
            this.textBoxDocFolder.Name = "textBoxDocFolder";
            this.textBoxDocFolder.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textBoxDocFolder.Size = new System.Drawing.Size(429, 93);
            this.textBoxDocFolder.TabIndex = 13;
            this.textBoxDocFolder.WordWrap = false;
            this.textBoxDocFolder.TextChanged += new System.EventHandler(this.textBoxDocFolder_TextChanged);
            // 
            // button_g3back
            // 
            this.button_g3back.Location = new System.Drawing.Point(364, 54);
            this.button_g3back.Name = "button_g3back";
            this.button_g3back.Size = new System.Drawing.Size(75, 23);
            this.button_g3back.TabIndex = 8;
            this.button_g3back.Text = "BACK";
            this.button_g3back.UseVisualStyleBackColor = true;
            this.button_g3back.Click += new System.EventHandler(this.button_g3back_Click);
            // 
            // button_g3ok
            // 
            this.button_g3ok.Location = new System.Drawing.Point(364, 24);
            this.button_g3ok.Name = "button_g3ok";
            this.button_g3ok.Size = new System.Drawing.Size(75, 23);
            this.button_g3ok.TabIndex = 7;
            this.button_g3ok.Text = "NEXT";
            this.button_g3ok.UseVisualStyleBackColor = true;
            this.button_g3ok.Click += new System.EventHandler(this.button_g3ok_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label_g4);
            this.groupBox4.Controls.Add(this.label_help_4);
            this.groupBox4.Controls.Add(this.button_g4clear);
            this.groupBox4.Controls.Add(this.buttonOpenGenFolderDialog);
            this.groupBox4.Controls.Add(this.textBox5);
            this.groupBox4.Controls.Add(this.textBoxGenFolder);
            this.groupBox4.Controls.Add(this.button_g4back);
            this.groupBox4.Controls.Add(this.button_g4ok);
            this.groupBox4.Location = new System.Drawing.Point(537, 259);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(464, 231);
            this.groupBox4.TabIndex = 2;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Step 4 of 6";
            // 
            // label_g4
            // 
            this.label_g4.AutoSize = true;
            this.label_g4.Font = new System.Drawing.Font("MS UI Gothic", 6.25F);
            this.label_g4.Location = new System.Drawing.Point(366, 10);
            this.label_g4.Margin = new System.Windows.Forms.Padding(0);
            this.label_g4.Name = "label_g4";
            this.label_g4.Size = new System.Drawing.Size(27, 9);
            this.label_g4.TabIndex = 39;
            this.label_g4.Text = "[C24]";
            // 
            // label_help_4
            // 
            this.label_help_4.AutoSize = true;
            this.label_help_4.BackColor = System.Drawing.Color.Silver;
            this.label_help_4.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_help_4.ForeColor = System.Drawing.Color.White;
            this.label_help_4.Location = new System.Drawing.Point(340, 19);
            this.label_help_4.Margin = new System.Windows.Forms.Padding(0);
            this.label_help_4.Name = "label_help_4";
            this.label_help_4.Size = new System.Drawing.Size(13, 14);
            this.label_help_4.TabIndex = 37;
            this.label_help_4.Text = "?";
            this.label_help_4.Click += new System.EventHandler(this.label_help_4_Click);
            // 
            // button_g4clear
            // 
            this.button_g4clear.Location = new System.Drawing.Point(368, 183);
            this.button_g4clear.Name = "button_g4clear";
            this.button_g4clear.Size = new System.Drawing.Size(75, 23);
            this.button_g4clear.TabIndex = 17;
            this.button_g4clear.Text = "CLEAR";
            this.button_g4clear.UseVisualStyleBackColor = true;
            this.button_g4clear.Click += new System.EventHandler(this.button_g4clear_Click);
            // 
            // buttonOpenGenFolderDialog
            // 
            this.buttonOpenGenFolderDialog.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonOpenGenFolderDialog.Location = new System.Drawing.Point(169, 183);
            this.buttonOpenGenFolderDialog.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.buttonOpenGenFolderDialog.Name = "buttonOpenGenFolderDialog";
            this.buttonOpenGenFolderDialog.Size = new System.Drawing.Size(87, 40);
            this.buttonOpenGenFolderDialog.TabIndex = 17;
            this.buttonOpenGenFolderDialog.Text = "FOLDER";
            this.buttonOpenGenFolderDialog.UseVisualStyleBackColor = true;
            this.buttonOpenGenFolderDialog.Click += new System.EventHandler(this.buttonOpenGenFolderDialog_Click);
            // 
            // textBox5
            // 
            this.textBox5.BackColor = System.Drawing.SystemColors.Control;
            this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox5.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox5.Location = new System.Drawing.Point(30, 22);
            this.textBox5.Multiline = true;
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(307, 55);
            this.textBox5.TabIndex = 16;
            this.textBox5.Text = "[C05]Specify the source code folder.";
            // 
            // textBoxGenFolder
            // 
            this.textBoxGenFolder.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxGenFolder.Location = new System.Drawing.Point(9, 85);
            this.textBoxGenFolder.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.textBoxGenFolder.Multiline = true;
            this.textBoxGenFolder.Name = "textBoxGenFolder";
            this.textBoxGenFolder.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.textBoxGenFolder.Size = new System.Drawing.Size(434, 93);
            this.textBoxGenFolder.TabIndex = 16;
            this.textBoxGenFolder.WordWrap = false;
            this.textBoxGenFolder.TextChanged += new System.EventHandler(this.textBoxGenFolder_TextChanged);
            // 
            // button_g4back
            // 
            this.button_g4back.Location = new System.Drawing.Point(368, 54);
            this.button_g4back.Name = "button_g4back";
            this.button_g4back.Size = new System.Drawing.Size(75, 23);
            this.button_g4back.TabIndex = 6;
            this.button_g4back.Text = "BACK";
            this.button_g4back.UseVisualStyleBackColor = true;
            this.button_g4back.Click += new System.EventHandler(this.button_g4back_Click);
            // 
            // button_g4ok
            // 
            this.button_g4ok.Location = new System.Drawing.Point(368, 24);
            this.button_g4ok.Name = "button_g4ok";
            this.button_g4ok.Size = new System.Drawing.Size(75, 23);
            this.button_g4ok.TabIndex = 5;
            this.button_g4ok.Text = "NEXT";
            this.button_g4ok.UseVisualStyleBackColor = true;
            this.button_g4ok.Click += new System.EventHandler(this.button_g4ok_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label_g5);
            this.groupBox5.Controls.Add(this.label_help_5);
            this.groupBox5.Controls.Add(this.textBox11);
            this.groupBox5.Controls.Add(this.radioButtonNo);
            this.groupBox5.Controls.Add(this.radioButtonYes);
            this.groupBox5.Controls.Add(this.textBox7);
            this.groupBox5.Controls.Add(this.button_g5back);
            this.groupBox5.Controls.Add(this.button_g5ok);
            this.groupBox5.Location = new System.Drawing.Point(12, 509);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(464, 231);
            this.groupBox5.TabIndex = 3;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Step 5 of 6";
            // 
            // label_g5
            // 
            this.label_g5.AutoSize = true;
            this.label_g5.Font = new System.Drawing.Font("MS UI Gothic", 6.25F);
            this.label_g5.Location = new System.Drawing.Point(371, 10);
            this.label_g5.Margin = new System.Windows.Forms.Padding(0);
            this.label_g5.Name = "label_g5";
            this.label_g5.Size = new System.Drawing.Size(27, 9);
            this.label_g5.TabIndex = 40;
            this.label_g5.Text = "[C25]";
            // 
            // label_help_5
            // 
            this.label_help_5.AutoSize = true;
            this.label_help_5.BackColor = System.Drawing.Color.Silver;
            this.label_help_5.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_help_5.ForeColor = System.Drawing.Color.White;
            this.label_help_5.Location = new System.Drawing.Point(339, 18);
            this.label_help_5.Margin = new System.Windows.Forms.Padding(0);
            this.label_help_5.Name = "label_help_5";
            this.label_help_5.Size = new System.Drawing.Size(13, 14);
            this.label_help_5.TabIndex = 37;
            this.label_help_5.Text = "?";
            this.label_help_5.Click += new System.EventHandler(this.label_help_5_Click);
            // 
            // textBox11
            // 
            this.textBox11.BackColor = System.Drawing.SystemColors.Control;
            this.textBox11.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox11.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox11.ForeColor = System.Drawing.Color.Coral;
            this.textBox11.Location = new System.Drawing.Point(31, 158);
            this.textBox11.Multiline = true;
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(307, 57);
            this.textBox11.TabIndex = 21;
            this.textBox11.Text = "[C07]In a case, the manger file exists in another folder because  the manger file" +
    " is A common file of all state machines.";
            // 
            // radioButtonNo
            // 
            this.radioButtonNo.AutoSize = true;
            this.radioButtonNo.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.radioButtonNo.Location = new System.Drawing.Point(130, 113);
            this.radioButtonNo.Name = "radioButtonNo";
            this.radioButtonNo.Size = new System.Drawing.Size(49, 23);
            this.radioButtonNo.TabIndex = 20;
            this.radioButtonNo.Text = "No";
            this.radioButtonNo.UseVisualStyleBackColor = true;
            // 
            // radioButtonYes
            // 
            this.radioButtonYes.AutoSize = true;
            this.radioButtonYes.Checked = true;
            this.radioButtonYes.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.radioButtonYes.Location = new System.Drawing.Point(130, 82);
            this.radioButtonYes.Name = "radioButtonYes";
            this.radioButtonYes.Size = new System.Drawing.Size(60, 23);
            this.radioButtonYes.TabIndex = 19;
            this.radioButtonYes.TabStop = true;
            this.radioButtonYes.Text = "YES";
            this.radioButtonYes.UseVisualStyleBackColor = true;
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.SystemColors.Control;
            this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox7.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox7.Location = new System.Drawing.Point(31, 36);
            this.textBox7.Multiline = true;
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(307, 41);
            this.textBox7.TabIndex = 18;
            this.textBox7.Text = "[C06]Do you need to copy the manager file?";
            // 
            // button_g5back
            // 
            this.button_g5back.Location = new System.Drawing.Point(368, 54);
            this.button_g5back.Name = "button_g5back";
            this.button_g5back.Size = new System.Drawing.Size(75, 23);
            this.button_g5back.TabIndex = 10;
            this.button_g5back.Text = "BACK";
            this.button_g5back.UseVisualStyleBackColor = true;
            this.button_g5back.Click += new System.EventHandler(this.button_g5back_Click);
            // 
            // button_g5ok
            // 
            this.button_g5ok.Location = new System.Drawing.Point(368, 24);
            this.button_g5ok.Name = "button_g5ok";
            this.button_g5ok.Size = new System.Drawing.Size(75, 23);
            this.button_g5ok.TabIndex = 9;
            this.button_g5ok.Text = "NEXT";
            this.button_g5ok.UseVisualStyleBackColor = true;
            this.button_g5ok.Click += new System.EventHandler(this.button_g5ok_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label_help_6);
            this.groupBox6.Controls.Add(this.button1);
            this.groupBox6.Controls.Add(this.textBox12);
            this.groupBox6.Controls.Add(this.button_g6cancel);
            this.groupBox6.Controls.Add(this.textBoxCreateFiles);
            this.groupBox6.Controls.Add(this.textBox8);
            this.groupBox6.Controls.Add(this.button_g6back);
            this.groupBox6.Controls.Add(this.button_g6ok);
            this.groupBox6.Location = new System.Drawing.Point(537, 509);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(464, 231);
            this.groupBox6.TabIndex = 4;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Step 6 of 6";
            // 
            // label_help_6
            // 
            this.label_help_6.AutoSize = true;
            this.label_help_6.BackColor = System.Drawing.Color.Silver;
            this.label_help_6.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_help_6.ForeColor = System.Drawing.Color.White;
            this.label_help_6.Location = new System.Drawing.Point(340, 18);
            this.label_help_6.Margin = new System.Windows.Forms.Padding(0);
            this.label_help_6.Name = "label_help_6";
            this.label_help_6.Size = new System.Drawing.Size(13, 14);
            this.label_help_6.TabIndex = 37;
            this.label_help_6.Text = "?";
            this.label_help_6.Click += new System.EventHandler(this.label_help_6_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(366, 24);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 24;
            this.button1.Text = "[C10]Description";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox12
            // 
            this.textBox12.BackColor = System.Drawing.SystemColors.Control;
            this.textBox12.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox12.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox12.ForeColor = System.Drawing.Color.Coral;
            this.textBox12.Location = new System.Drawing.Point(32, 42);
            this.textBox12.Multiline = true;
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new System.Drawing.Size(307, 18);
            this.textBox12.TabIndex = 22;
            this.textBox12.Text = "[C09] If a file exists, it will not be copied.";
            // 
            // button_g6cancel
            // 
            this.button_g6cancel.Location = new System.Drawing.Point(366, 97);
            this.button_g6cancel.Name = "button_g6cancel";
            this.button_g6cancel.Size = new System.Drawing.Size(75, 23);
            this.button_g6cancel.TabIndex = 23;
            this.button_g6cancel.Text = "CANCEL";
            this.button_g6cancel.UseVisualStyleBackColor = true;
            this.button_g6cancel.Click += new System.EventHandler(this.button_g6cancel_Click);
            // 
            // textBoxCreateFiles
            // 
            this.textBoxCreateFiles.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxCreateFiles.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxCreateFiles.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCreateFiles.Location = new System.Drawing.Point(9, 60);
            this.textBoxCreateFiles.Multiline = true;
            this.textBoxCreateFiles.Name = "textBoxCreateFiles";
            this.textBoxCreateFiles.ReadOnly = true;
            this.textBoxCreateFiles.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxCreateFiles.Size = new System.Drawing.Size(345, 120);
            this.textBoxCreateFiles.TabIndex = 22;
            this.textBoxCreateFiles.Text = "Files\r\n";
            this.textBoxCreateFiles.WordWrap = false;
            // 
            // textBox8
            // 
            this.textBox8.BackColor = System.Drawing.SystemColors.Control;
            this.textBox8.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox8.Font = new System.Drawing.Font("Calibri", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox8.Location = new System.Drawing.Point(32, 22);
            this.textBox8.Multiline = true;
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(307, 32);
            this.textBox8.TabIndex = 21;
            this.textBox8.Text = "[C08]This tool will create the below files.";
            // 
            // button_g6back
            // 
            this.button_g6back.Location = new System.Drawing.Point(366, 54);
            this.button_g6back.Name = "button_g6back";
            this.button_g6back.Size = new System.Drawing.Size(75, 23);
            this.button_g6back.TabIndex = 12;
            this.button_g6back.Text = "BACK";
            this.button_g6back.UseVisualStyleBackColor = true;
            this.button_g6back.Click += new System.EventHandler(this.button_g6back_Click);
            // 
            // button_g6ok
            // 
            this.button_g6ok.Location = new System.Drawing.Point(32, 186);
            this.button_g6ok.Name = "button_g6ok";
            this.button_g6ok.Size = new System.Drawing.Size(411, 39);
            this.button_g6ok.TabIndex = 11;
            this.button_g6ok.Text = "Create";
            this.button_g6ok.UseVisualStyleBackColor = true;
            this.button_g6ok.Click += new System.EventHandler(this.button_g6ok_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // folderBrowserDialogTemplate
            // 
            this.folderBrowserDialogTemplate.HelpRequest += new System.EventHandler(this.folderBrowserDialog1_HelpRequest);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.HelpRequest += new System.EventHandler(this.folderBrowserDialog1_HelpRequest_1);
            // 
            // folderBrowserDialog2
            // 
            this.folderBrowserDialog2.HelpRequest += new System.EventHandler(this.folderBrowserDialog2_HelpRequest);
            // 
            // label_anchor
            // 
            this.label_anchor.AutoSize = true;
            this.label_anchor.Location = new System.Drawing.Point(483, 260);
            this.label_anchor.Margin = new System.Windows.Forms.Padding(0);
            this.label_anchor.Name = "label_anchor";
            this.label_anchor.Size = new System.Drawing.Size(39, 12);
            this.label_anchor.TabIndex = 5;
            this.label_anchor.Text = "anchor";
            this.label_anchor.Visible = false;
            // 
            // CreateNewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1018, 765);
            this.Controls.Add(this.label_anchor);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CreateNewForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Create a new state machine";
            this.Load += new System.EventHandler(this.CreateNewForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        public System.Windows.Forms.GroupBox groupBox1;
        public System.Windows.Forms.GroupBox groupBox2;
        public System.Windows.Forms.GroupBox groupBox3;
        public System.Windows.Forms.GroupBox groupBox4;
        public System.Windows.Forms.GroupBox groupBox5;
        public System.Windows.Forms.GroupBox groupBox6;
        public System.Windows.Forms.Label label2;
        public System.Windows.Forms.Label labelReadFrom;
        public System.Windows.Forms.ListBox listBox_title;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Button buttonOpenDocFolderDialog;
        public System.Windows.Forms.TextBox textBoxDocFolder;
        private System.Windows.Forms.Button buttonOpenGenFolderDialog;
        private System.Windows.Forms.TextBox textBox5;
        public System.Windows.Forms.TextBox textBoxGenFolder;
        private System.Windows.Forms.RadioButton radioButtonNo;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox8;
        public System.Windows.Forms.Label label1;
        public System.Windows.Forms.Label label_folderbrowser;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.Button button_g1ok;
        public System.Windows.Forms.Button button_g1cancel;
        public System.Windows.Forms.Button button_g2back;
        public System.Windows.Forms.Button button_g2ok;
        public System.Windows.Forms.Button button_g3back;
        public System.Windows.Forms.Button button_g3ok;
        public System.Windows.Forms.Button button_g4back;
        public System.Windows.Forms.Button button_g4ok;
        public System.Windows.Forms.Button button_g5back;
        public System.Windows.Forms.Button button_g5ok;
        public System.Windows.Forms.Button button_g6back;
        public System.Windows.Forms.Button button_g6cancel;
        public System.Windows.Forms.Button button_g6ok;
        public System.Windows.Forms.TextBox textBoxPrefix;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialogTemplate;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        public System.Windows.Forms.TextBox textBoxCreateFiles;
        public System.Windows.Forms.RadioButton radioButtonYes;
        public System.Windows.Forms.TextBox textBoxStateMachineName;
        public System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox textBoxReadFromPath;
        public System.Windows.Forms.Button button_g3clear;
        public System.Windows.Forms.Button button_g4clear;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog2;
        private System.Windows.Forms.Label label_anchor;
        private System.Windows.Forms.Label label_help_1;
        private System.Windows.Forms.Label label_help_2;
        private System.Windows.Forms.Label label_help_3;
        private System.Windows.Forms.Label label_help_4;
        private System.Windows.Forms.Label label_help_5;
        private System.Windows.Forms.Label label_help_6;
        public System.Windows.Forms.Button button_shortcut;
        private System.Windows.Forms.Label label_g1;
        private System.Windows.Forms.Label label_g2;
        private System.Windows.Forms.Label label_g3;
        private System.Windows.Forms.Label label_g4;
        private System.Windows.Forms.Label label_g5;
    }
}