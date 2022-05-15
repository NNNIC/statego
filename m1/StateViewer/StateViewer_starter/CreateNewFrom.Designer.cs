namespace StateViewer_starter
{
    partial class CreateNewFrom
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
            this.labelLang = new System.Windows.Forms.Label();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.textBoxPrefix = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBoxDetail = new System.Windows.Forms.TextBox();
            this.textBoxExcelFolder = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.textBoxGenerateFolder = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.textBoxStateManager = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelLang
            // 
            this.labelLang.AutoSize = true;
            this.labelLang.Location = new System.Drawing.Point(21, 18);
            this.labelLang.Name = "labelLang";
            this.labelLang.Size = new System.Drawing.Size(53, 12);
            this.labelLang.TabIndex = 0;
            this.labelLang.Text = "Language";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Items.AddRange(new object[] {
            "test"});
            this.listBox1.Location = new System.Drawing.Point(23, 46);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(334, 88);
            this.listBox1.TabIndex = 1;
            // 
            // textBoxPrefix
            // 
            this.textBoxPrefix.Location = new System.Drawing.Point(180, 247);
            this.textBoxPrefix.Name = "textBoxPrefix";
            this.textBoxPrefix.Size = new System.Drawing.Size(177, 19);
            this.textBoxPrefix.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 250);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(153, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "State Controller Name Prefix";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(23, 442);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(119, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "CREATE";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(251, 442);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(106, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "BACK";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // textBoxDetail
            // 
            this.textBoxDetail.Location = new System.Drawing.Point(23, 150);
            this.textBoxDetail.Multiline = true;
            this.textBoxDetail.Name = "textBoxDetail";
            this.textBoxDetail.ReadOnly = true;
            this.textBoxDetail.Size = new System.Drawing.Size(334, 77);
            this.textBoxDetail.TabIndex = 6;
            // 
            // textBoxExcelFolder
            // 
            this.textBoxExcelFolder.Location = new System.Drawing.Point(61, 294);
            this.textBoxExcelFolder.Name = "textBoxExcelFolder";
            this.textBoxExcelFolder.Size = new System.Drawing.Size(215, 19);
            this.textBoxExcelFolder.TabIndex = 7;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(282, 294);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 8;
            this.button3.Text = "FOLDER";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 279);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(293, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "Destination folder for the initial state-chart of excel file";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(21, 324);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(313, 12);
            this.label4.TabIndex = 12;
            this.label4.Text = "Folder for sources generated from the state-chart excel file";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(282, 339);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 11;
            this.button4.Text = "FOLDER";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // textBoxGenerateFolder
            // 
            this.textBoxGenerateFolder.Location = new System.Drawing.Point(61, 341);
            this.textBoxGenerateFolder.Name = "textBoxGenerateFolder";
            this.textBoxGenerateFolder.Size = new System.Drawing.Size(215, 19);
            this.textBoxGenerateFolder.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(21, 374);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(249, 12);
            this.label5.TabIndex = 15;
            this.label5.Text = "Destination folder for the state manager source";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(282, 387);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 14;
            this.button5.Text = "FOLDER";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // textBoxStateManager
            // 
            this.textBoxStateManager.Location = new System.Drawing.Point(61, 389);
            this.textBoxStateManager.Name = "textBoxStateManager";
            this.textBoxStateManager.Size = new System.Drawing.Size(215, 19);
            this.textBoxStateManager.TabIndex = 13;
            // 
            // CreateNewFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 499);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.textBoxStateManager);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.textBoxGenerateFolder);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.textBoxExcelFolder);
            this.Controls.Add(this.textBoxDetail);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxPrefix);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.labelLang);
            this.Name = "CreateNewFrom";
            this.Text = "Create a new  state chart";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelLang;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.TextBox textBoxPrefix;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBoxDetail;
        private System.Windows.Forms.TextBox textBoxExcelFolder;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.TextBox textBoxGenerateFolder;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox textBoxStateManager;
    }
}