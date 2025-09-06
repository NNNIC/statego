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
using System.IO;

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

        private void button2_Click(object sender, EventArgs e)
        {
            test2();
        }

        void test2()
        {
            string prompt = textBox3.Text;
            string pythonExePath =
      @"C:\Users\gea01\Documents\psgg\psgg-editor-public\editor\WinPython\python\python.exe";

            string scriptPath =
      @"C:\Users\gea01\Documents\psgg\psgg-editor-public\editor\WinPython\gemini_image_sys.py";

            string tempDir =
      @"C:\Users\gea01\Documents\psgg\psgg-editor-public\editor\WinPython";

            string outputImagePath = Path.Combine(tempDir, Path.GetFileName( Path.GetTempFileName()) + ".png");
            if (File.Exists(outputImagePath)) File.Delete(outputImagePath);

            var start = new ProcessStartInfo();
            start.FileName = pythonExePath;
            start.Arguments = string.Format("\"{0}\"  -p \"{1}\" -o \"{2}\"", scriptPath, prompt, outputImagePath);
            start.UseShellExecute = false;
            start.RedirectStandardOutput = true;
            start.RedirectStandardError = true;
            
            start.StandardOutputEncoding = System.Text.Encoding.UTF8; // ① 標準出力のエンコーディングをUTF-8に設定
            start.StandardErrorEncoding = System.Text.Encoding.UTF8;  // ② 標準エラー出力のエンコーディングをUTF-8に設定
            start.CreateNoWindow = true;
            start.EnvironmentVariables["PYTHONIOENCODING"] = "UTF-8";

            var result = "";
            var error = "";

            using (var process = Process.Start(start))
            {
                result = process.StandardOutput.ReadToEnd();
                error = process.StandardError.ReadToEnd();
                process.WaitForExit();
            }
            if (!string.IsNullOrEmpty(result)) textBox2.Text = result + "\n\n";
            textBox2.Text += error;

            if (File.Exists(outputImagePath))
            {
                try
                {
                    //表示内に収める
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

                    pictureBox1.Image = Image.FromFile(outputImagePath);
                }
                catch (Exception ex)
                {
                    textBox2.Text += "\n\n" + ex.ToString();
                }
            }
            else
            {
                textBox2.Text += "\n\n" + "Image file not found: " + outputImagePath;
            }
        }
    }
}