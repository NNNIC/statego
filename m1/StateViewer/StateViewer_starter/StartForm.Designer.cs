namespace StateViewer_starter
{
    partial class StartForm
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
            this.buttonCreate = new System.Windows.Forms.Button();
            this.comboBoxHistory = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.checkBoxSkipCopyStateManager = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.textBoxGenerateFolder = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxExcelFolder = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxPrefix = new System.Windows.Forms.TextBox();
            this.textBoxDetail = new System.Windows.Forms.TextBox();
            this.listBox_title = new System.Windows.Forms.ListBox();
            this.radioButton_cpp = new System.Windows.Forms.RadioButton();
            this.radioButton_tyranoscript = new System.Windows.Forms.RadioButton();
            this.radioButton_typescriptangular = new System.Windows.Forms.RadioButton();
            this.radioButton_vbaexcel = new System.Windows.Forms.RadioButton();
            this.radioButton_csharp = new System.Windows.Forms.RadioButton();
            this.radioButton_csharpunity = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelFileNameOnHistory = new System.Windows.Forms.Label();
            this.labelSelectOnHistory = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.labelVer = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCreate
            // 
            this.buttonCreate.Location = new System.Drawing.Point(28, 609);
            this.buttonCreate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonCreate.Name = "buttonCreate";
            this.buttonCreate.Size = new System.Drawing.Size(463, 34);
            this.buttonCreate.TabIndex = 0;
            this.buttonCreate.Text = "Create";
            this.buttonCreate.UseVisualStyleBackColor = true;
            this.buttonCreate.Click += new System.EventHandler(this.buttonCreate_Click);
            // 
            // comboBoxHistory
            // 
            this.comboBoxHistory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxHistory.FormattingEnabled = true;
            this.comboBoxHistory.Location = new System.Drawing.Point(26, 75);
            this.comboBoxHistory.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.comboBoxHistory.Name = "comboBoxHistory";
            this.comboBoxHistory.Size = new System.Drawing.Size(313, 26);
            this.comboBoxHistory.TabIndex = 1;
            this.comboBoxHistory.SelectedIndexChanged += new System.EventHandler(this.comboBoxHistory_SelectedIndexChanged);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(358, 70);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(141, 34);
            this.button2.TabIndex = 2;
            this.button2.Text = "Open";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.buttonOpenInHistroy_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(29, 40);
            this.button3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(474, 34);
            this.button3.TabIndex = 3;
            this.button3.Text = "Open";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.buttonOpenExisting_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.checkBoxSkipCopyStateManager);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.button4);
            this.groupBox1.Controls.Add(this.textBoxGenerateFolder);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.textBoxExcelFolder);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBoxPrefix);
            this.groupBox1.Controls.Add(this.textBoxDetail);
            this.groupBox1.Controls.Add(this.listBox_title);
            this.groupBox1.Controls.Add(this.radioButton_cpp);
            this.groupBox1.Controls.Add(this.radioButton_tyranoscript);
            this.groupBox1.Controls.Add(this.radioButton_typescriptangular);
            this.groupBox1.Controls.Add(this.radioButton_vbaexcel);
            this.groupBox1.Controls.Add(this.radioButton_csharp);
            this.groupBox1.Controls.Add(this.radioButton_csharpunity);
            this.groupBox1.Controls.Add(this.buttonCreate);
            this.groupBox1.Location = new System.Drawing.Point(13, 243);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(517, 686);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Create a new state-chart excel file";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Tomato;
            this.label5.Location = new System.Drawing.Point(131, 584);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(306, 18);
            this.label5.TabIndex = 19;
            this.label5.Text = "※ You may skip to copy, if you have own manager.";
            // 
            // checkBoxSkipCopyStateManager
            // 
            this.checkBoxSkipCopyStateManager.AutoSize = true;
            this.checkBoxSkipCopyStateManager.Location = new System.Drawing.Point(98, 559);
            this.checkBoxSkipCopyStateManager.Name = "checkBoxSkipCopyStateManager";
            this.checkBoxSkipCopyStateManager.Size = new System.Drawing.Size(206, 22);
            this.checkBoxSkipCopyStateManager.TabIndex = 18;
            this.checkBoxSkipCopyStateManager.Text = "Skip to copy StateManager file";
            this.checkBoxSkipCopyStateManager.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Tomato;
            this.label1.Location = new System.Drawing.Point(131, 655);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(258, 18);
            this.label1.TabIndex = 17;
            this.label1.Text = "※ If the files exist, they will not be copied.";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(5, 504);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(354, 18);
            this.label4.TabIndex = 16;
            this.label4.Text = "Folder for sources generated from the state-chart excel file";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(402, 524);
            this.button4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(87, 34);
            this.button4.TabIndex = 15;
            this.button4.Text = "FOLDER";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.buttonGenerateFolder_Click);
            // 
            // textBoxGenerateFolder
            // 
            this.textBoxGenerateFolder.Location = new System.Drawing.Point(145, 526);
            this.textBoxGenerateFolder.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxGenerateFolder.Name = "textBoxGenerateFolder";
            this.textBoxGenerateFolder.Size = new System.Drawing.Size(250, 25);
            this.textBoxGenerateFolder.TabIndex = 14;
            this.textBoxGenerateFolder.TextChanged += new System.EventHandler(this.textBoxGenerateFolder_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 435);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(328, 18);
            this.label3.TabIndex = 13;
            this.label3.Text = "Destination folder for the initial state-chart of excel file";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(402, 452);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(87, 34);
            this.button1.TabIndex = 12;
            this.button1.Text = "FOLDER";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.buttonXlsFolder_Click);
            // 
            // textBoxExcelFolder
            // 
            this.textBoxExcelFolder.Location = new System.Drawing.Point(145, 458);
            this.textBoxExcelFolder.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxExcelFolder.Name = "textBoxExcelFolder";
            this.textBoxExcelFolder.Size = new System.Drawing.Size(250, 25);
            this.textBoxExcelFolder.TabIndex = 11;
            this.textBoxExcelFolder.TextChanged += new System.EventHandler(this.textBoxExcelFolder_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 393);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(177, 18);
            this.label2.TabIndex = 10;
            this.label2.Text = "State Controller Name Prefix";
            // 
            // textBoxPrefix
            // 
            this.textBoxPrefix.Location = new System.Drawing.Point(212, 388);
            this.textBoxPrefix.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxPrefix.Name = "textBoxPrefix";
            this.textBoxPrefix.Size = new System.Drawing.Size(287, 25);
            this.textBoxPrefix.TabIndex = 9;
            this.textBoxPrefix.TextChanged += new System.EventHandler(this.textBoxPrefix_TextChanged);
            // 
            // textBoxDetail
            // 
            this.textBoxDetail.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.textBoxDetail.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxDetail.Location = new System.Drawing.Point(7, 177);
            this.textBoxDetail.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.textBoxDetail.Multiline = true;
            this.textBoxDetail.Name = "textBoxDetail";
            this.textBoxDetail.ReadOnly = true;
            this.textBoxDetail.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxDetail.Size = new System.Drawing.Size(493, 200);
            this.textBoxDetail.TabIndex = 8;
            // 
            // listBox_title
            // 
            this.listBox_title.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.listBox_title.FormattingEnabled = true;
            this.listBox_title.ItemHeight = 18;
            this.listBox_title.Items.AddRange(new object[] {
            "　"});
            this.listBox_title.Location = new System.Drawing.Point(7, 108);
            this.listBox_title.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.listBox_title.Name = "listBox_title";
            this.listBox_title.Size = new System.Drawing.Size(493, 58);
            this.listBox_title.TabIndex = 7;
            this.listBox_title.SelectedIndexChanged += new System.EventHandler(this.listBox_title_SelectedIndexChanged);
            // 
            // radioButton_cpp
            // 
            this.radioButton_cpp.AutoSize = true;
            this.radioButton_cpp.Location = new System.Drawing.Point(279, 75);
            this.radioButton_cpp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioButton_cpp.Name = "radioButton_cpp";
            this.radioButton_cpp.Size = new System.Drawing.Size(54, 22);
            this.radioButton_cpp.TabIndex = 6;
            this.radioButton_cpp.TabStop = true;
            this.radioButton_cpp.Text = "C++";
            this.radioButton_cpp.UseVisualStyleBackColor = true;
            // 
            // radioButton_tyranoscript
            // 
            this.radioButton_tyranoscript.AutoSize = true;
            this.radioButton_tyranoscript.Location = new System.Drawing.Point(161, 75);
            this.radioButton_tyranoscript.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioButton_tyranoscript.Name = "radioButton_tyranoscript";
            this.radioButton_tyranoscript.Size = new System.Drawing.Size(96, 22);
            this.radioButton_tyranoscript.TabIndex = 5;
            this.radioButton_tyranoscript.TabStop = true;
            this.radioButton_tyranoscript.Text = "tyranoscript";
            this.radioButton_tyranoscript.UseVisualStyleBackColor = true;
            // 
            // radioButton_typescriptangular
            // 
            this.radioButton_typescriptangular.AutoSize = true;
            this.radioButton_typescriptangular.Location = new System.Drawing.Point(279, 42);
            this.radioButton_typescriptangular.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioButton_typescriptangular.Name = "radioButton_typescriptangular";
            this.radioButton_typescriptangular.Size = new System.Drawing.Size(134, 22);
            this.radioButton_typescriptangular.TabIndex = 4;
            this.radioButton_typescriptangular.TabStop = true;
            this.radioButton_typescriptangular.Text = "Angular Typescript";
            this.radioButton_typescriptangular.UseVisualStyleBackColor = true;
            // 
            // radioButton_vbaexcel
            // 
            this.radioButton_vbaexcel.AutoSize = true;
            this.radioButton_vbaexcel.Location = new System.Drawing.Point(27, 75);
            this.radioButton_vbaexcel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioButton_vbaexcel.Name = "radioButton_vbaexcel";
            this.radioButton_vbaexcel.Size = new System.Drawing.Size(84, 22);
            this.radioButton_vbaexcel.TabIndex = 3;
            this.radioButton_vbaexcel.TabStop = true;
            this.radioButton_vbaexcel.Text = "Excel VBA";
            this.radioButton_vbaexcel.UseVisualStyleBackColor = true;
            // 
            // radioButton_csharp
            // 
            this.radioButton_csharp.AutoSize = true;
            this.radioButton_csharp.Location = new System.Drawing.Point(27, 42);
            this.radioButton_csharp.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioButton_csharp.Name = "radioButton_csharp";
            this.radioButton_csharp.Size = new System.Drawing.Size(44, 22);
            this.radioButton_csharp.TabIndex = 2;
            this.radioButton_csharp.TabStop = true;
            this.radioButton_csharp.Text = "C#";
            this.radioButton_csharp.UseVisualStyleBackColor = true;
            // 
            // radioButton_csharpunity
            // 
            this.radioButton_csharpunity.AutoSize = true;
            this.radioButton_csharpunity.Location = new System.Drawing.Point(161, 42);
            this.radioButton_csharpunity.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioButton_csharpunity.Name = "radioButton_csharpunity";
            this.radioButton_csharpunity.Size = new System.Drawing.Size(79, 22);
            this.radioButton_csharpunity.TabIndex = 1;
            this.radioButton_csharpunity.TabStop = true;
            this.radioButton_csharpunity.Text = "Unity C#";
            this.radioButton_csharpunity.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox2.Controls.Add(this.labelFileNameOnHistory);
            this.groupBox2.Controls.Add(this.labelSelectOnHistory);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.comboBoxHistory);
            this.groupBox2.Location = new System.Drawing.Point(14, 16);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(517, 123);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Open a state-chart excel file in history";
            // 
            // labelFileNameOnHistory
            // 
            this.labelFileNameOnHistory.AutoSize = true;
            this.labelFileNameOnHistory.Font = new System.Drawing.Font("Meiryo UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelFileNameOnHistory.Location = new System.Drawing.Point(16, 26);
            this.labelFileNameOnHistory.Name = "labelFileNameOnHistory";
            this.labelFileNameOnHistory.Size = new System.Drawing.Size(11, 15);
            this.labelFileNameOnHistory.TabIndex = 4;
            this.labelFileNameOnHistory.Text = " ";
            // 
            // labelSelectOnHistory
            // 
            this.labelSelectOnHistory.AutoSize = true;
            this.labelSelectOnHistory.Font = new System.Drawing.Font("Meiryo UI", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelSelectOnHistory.Location = new System.Drawing.Point(13, 45);
            this.labelSelectOnHistory.Name = "labelSelectOnHistory";
            this.labelSelectOnHistory.Size = new System.Drawing.Size(8, 12);
            this.labelSelectOnHistory.TabIndex = 3;
            this.labelSelectOnHistory.Text = " ";
            this.labelSelectOnHistory.Click += new System.EventHandler(this.labelSelectOnHistory_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.button3);
            this.groupBox3.Location = new System.Drawing.Point(12, 148);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox3.Size = new System.Drawing.Size(518, 87);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Open a existing state-chart excel file";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // labelVer
            // 
            this.labelVer.AutoSize = true;
            this.labelVer.Font = new System.Drawing.Font("メイリオ", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelVer.ForeColor = System.Drawing.Color.Maroon;
            this.labelVer.Location = new System.Drawing.Point(332, 943);
            this.labelVer.Name = "labelVer";
            this.labelVer.Size = new System.Drawing.Size(40, 17);
            this.labelVer.TabIndex = 7;
            this.labelVer.Text = "label5";
            // 
            // StartForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(553, 969);
            this.Controls.Add(this.labelVer);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "StartForm";
            this.Text = "Start dialog";
            this.Load += new System.EventHandler(this.StartForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonCreate;
        private System.Windows.Forms.ComboBox comboBoxHistory;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        public System.Windows.Forms.RadioButton radioButton_tyranoscript;
        public System.Windows.Forms.RadioButton radioButton_typescriptangular;
        public System.Windows.Forms.RadioButton radioButton_vbaexcel;
        public System.Windows.Forms.RadioButton radioButton_csharp;
        public System.Windows.Forms.RadioButton radioButton_csharpunity;
        public System.Windows.Forms.RadioButton radioButton_cpp;
        public System.Windows.Forms.ListBox listBox_title;
        public System.Windows.Forms.TextBox textBoxDetail;
        private System.Windows.Forms.Label labelSelectOnHistory;
        public System.Windows.Forms.TextBox textBoxPrefix;
        public System.Windows.Forms.TextBox textBoxExcelFolder;
        public System.Windows.Forms.TextBox textBoxGenerateFolder;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelVer;
        private System.Windows.Forms.Label labelFileNameOnHistory;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.CheckBox checkBoxSkipCopyStateManager;
    }
}