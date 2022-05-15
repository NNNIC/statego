namespace StateViewer_starter2
{
    partial class CreateNewDetailTextFrom
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateNewDetailTextFrom));
            this.textBoxStarterKitDescription = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxStarterKitDescription
            // 
            this.textBoxStarterKitDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxStarterKitDescription.BackColor = System.Drawing.Color.White;
            this.textBoxStarterKitDescription.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxStarterKitDescription.Location = new System.Drawing.Point(12, 12);
            this.textBoxStarterKitDescription.Multiline = true;
            this.textBoxStarterKitDescription.Name = "textBoxStarterKitDescription";
            this.textBoxStarterKitDescription.ReadOnly = true;
            this.textBoxStarterKitDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBoxStarterKitDescription.Size = new System.Drawing.Size(736, 213);
            this.textBoxStarterKitDescription.TabIndex = 0;
            // 
            // CreateNewDetailTextFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 261);
            this.Controls.Add(this.textBoxStarterKitDescription);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "CreateNewDetailTextFrom";
            this.Text = "Description";
            this.Load += new System.EventHandler(this.CreateNewDetailTextFrom_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.TextBox textBoxStarterKitDescription;
    }
}