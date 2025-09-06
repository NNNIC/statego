using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GeminiTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Test();
        }

        private void Test()
        {
            string prompt = "Hello, World!";
            string pythonExePath =
      @"C:\Users\gea01\Documents\psgg\psgg-editor-public\editor\WinPython\python\python.exe";

            string scriptPath =
      @"C:\Users\gea01\Documents\psgg\psgg-editor-public\editor\WinPython\gemini_api_script.py";

            var start = new ProcessStartInfo();
            start.FileName = pythonExePath;
            start.Arguments = string.Format("\"{0}\" \"{1}\"", scriptPath, prompt);
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            start.RedirectStandardError = true;
            start.CreateNoWindow = true;

            var result = "";
            var error = "";

            using (var process = Process.Start(start))
            {
                result = process.StandardOutput.ReadToEnd();
                error = process.StandardError.ReadToEnd();
                process.WaitForExit();
            }
            if (!string.IsNullOrEmpty(result)) textBox1.Text = result + "\n\n";
            textBox1.Text += error;
        }
    }
}
