using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using G = stateview.Globals;

namespace stateview._5300_EditForm
{
    public partial class Editor_bmpForm_aiControl
    {
        public EditForm_bmpForm m_form;

        #region manager
        Action<bool> m_curfunc;
        Action<bool> m_nextfunc;

        bool         m_noWait;

        public void Update()
        {
            while (true)
            {
                var bFirst = false;
                if (m_nextfunc != null)
                {
                    m_curfunc = m_nextfunc;
                    m_nextfunc = null;
                    bFirst = true;
                }
                m_noWait = false;
                if (m_curfunc != null)
                {
                    m_curfunc(bFirst);
                }
                if (!m_noWait) break;
            }
        }
        void Goto(Action<bool> func)
        {
            m_nextfunc = func;
        }
        bool CheckState(Action<bool> func)
        {
            return m_curfunc == func;
        }
        bool HasNextState()
        {
            return m_nextfunc != null;
        }
        void NoWait()
        {
            m_noWait = true;
        }
        #endregion
        #region gosub
        List<Action<bool>> m_callstack = new List<Action<bool>>();
        void GoSubState(Action<bool> nextstate, Action<bool> returnstate)
        {
            m_callstack.Insert(0, returnstate);
            Goto(nextstate);
        }
        void ReturnState()
        {
            var nextstate = m_callstack[0];
            m_callstack.RemoveAt(0);
            Goto(nextstate);
        }
        #endregion

        public void Start()
        {
            Goto(S_START);
        }
        public bool IsEnd()
        {
            return CheckState(S_END);
        }

        public void Run()
        {
            int LOOPMAX = (int)(1E+5);
            Start();
            for (var loop = 0; loop <= LOOPMAX; loop++)
            {
                if (loop >= LOOPMAX) throw new SystemException("Unexpected.");
                Update();
                if (IsEnd()) break;
            }
        }

        #region    // [PSGG OUTPUT START] indent(8) $/./$
        //             psggConverterLib.dll converted from psgg-file:Editor_bmpForm_aiControl.psgg                  // *DoNotEdit*
                                                                            // *DoNotEdit*
        /*                                                                  // *DoNotEdit*
            S_0000                                                          // *DoNotEdit*
        */                                                                  // *DoNotEdit*
        void S_0000(bool bFirst)                                            // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (bFirst)                                                     // *DoNotEdit*
            {                                                               // *DoNotEdit*
                Log("Call python script");                                  // *DoNotEdit*
                start_python();                                             // *DoNotEdit*
                start_process();                                            // *DoNotEdit*
            }                                                               // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (!HasNextState())                                            // *DoNotEdit*
            {                                                               // *DoNotEdit*
                Goto(S_0005);                                               // *DoNotEdit*
            }                                                               // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        /*                                                                  // *DoNotEdit*
            S_0003                                                          // *DoNotEdit*
        */                                                                  // *DoNotEdit*
        void S_0003(bool bFirst)                                            // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (bFirst)                                                     // *DoNotEdit*
            {                                                               // *DoNotEdit*
                Log("\nTIMEOUT!");                                          // *DoNotEdit*
            }                                                               // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (!HasNextState())                                            // *DoNotEdit*
            {                                                               // *DoNotEdit*
                Goto(S_FINALIZE);                                           // *DoNotEdit*
            }                                                               // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        /*                                                                  // *DoNotEdit*
            S_0005                                                          // *DoNotEdit*
            待ち！                                                          // *DoNotEdit*
        */                                                                  // *DoNotEdit*
        DateTime m_starttime;                                               // *DoNotEdit*
        bool m_bTimeout;                                                    // *DoNotEdit*
        void S_0005(bool bFirst)                                            // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (bFirst)                                                     // *DoNotEdit*
            {                                                               // *DoNotEdit*
                m_starttime = DateTime.Now;                                 // *DoNotEdit*
                m_bTimeout = false;                                         // *DoNotEdit*
            }                                                               // *DoNotEdit*
            m_bTimeout = (DateTime.Now - m_starttime).TotalSeconds > 180;   // *DoNotEdit*
            if (!(is_process_done() || m_bTimeout)) return;                 // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (!HasNextState())                                            // *DoNotEdit*
            {                                                               // *DoNotEdit*
                Goto(S_0006);                                               // *DoNotEdit*
            }                                                               // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        /*                                                                  // *DoNotEdit*
            S_0006                                                          // *DoNotEdit*
            プロセスエンド                                                  // *DoNotEdit*
        */                                                                  // *DoNotEdit*
        void S_0006(bool bFirst)                                            // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (bFirst)                                                     // *DoNotEdit*
            {                                                               // *DoNotEdit*
                end_process();                                              // *DoNotEdit*
            }                                                               // *DoNotEdit*
            // branch                                                       // *DoNotEdit*
            if (m_bTimeout) { Goto( S_0003 ); }                             // *DoNotEdit*
            else { Goto( S_0007 ); }                                        // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        /*                                                                  // *DoNotEdit*
            S_0007                                                          // *DoNotEdit*
            成功していればイメージを表示                                    // *DoNotEdit*
        */                                                                  // *DoNotEdit*
        void S_0007(bool bFirst)                                            // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (bFirst)                                                     // *DoNotEdit*
            {                                                               // *DoNotEdit*
                copy_and_draw();                                            // *DoNotEdit*
            }                                                               // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (!HasNextState())                                            // *DoNotEdit*
            {                                                               // *DoNotEdit*
                Goto(S_FINALIZE);                                           // *DoNotEdit*
            }                                                               // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        /*                                                                  // *DoNotEdit*
            S_END                                                           // *DoNotEdit*
        */                                                                  // *DoNotEdit*
        void S_END(bool bFirst)                                             // *DoNotEdit*
        {                                                                   // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        /*                                                                  // *DoNotEdit*
            S_FINALIZE                                                      // *DoNotEdit*
        */                                                                  // *DoNotEdit*
        void S_FINALIZE(bool bFirst)                                        // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (bFirst)                                                     // *DoNotEdit*
            {                                                               // *DoNotEdit*
                m_form.enable_allbuttons(true);                             // *DoNotEdit*
                waiting_end();                                              // *DoNotEdit*
            }                                                               // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (!HasNextState())                                            // *DoNotEdit*
            {                                                               // *DoNotEdit*
                Goto(S_END);                                                // *DoNotEdit*
            }                                                               // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        /*                                                                  // *DoNotEdit*
            S_INIT                                                          // *DoNotEdit*
        */                                                                  // *DoNotEdit*
        void S_INIT(bool bFirst)                                            // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (bFirst)                                                     // *DoNotEdit*
            {                                                               // *DoNotEdit*
                m_form.enable_allbuttons(false);                            // *DoNotEdit*
                waiting_start();                                            // *DoNotEdit*
            }                                                               // *DoNotEdit*
            //                                                              // *DoNotEdit*
            if (!HasNextState())                                            // *DoNotEdit*
            {                                                               // *DoNotEdit*
                Goto(S_0000);                                               // *DoNotEdit*
            }                                                               // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        /*                                                                  // *DoNotEdit*
            S_START                                                         // *DoNotEdit*
        */                                                                  // *DoNotEdit*
        void S_START(bool bFirst)                                           // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            Goto(S_INIT);                                                   // *DoNotEdit*
            NoWait();                                                       // *DoNotEdit*
        }                                                                   // *DoNotEdit*
                                                                            // *DoNotEdit*
                                                                            // *DoNotEdit*
        #endregion // [PSGG OUTPUT END]

        // write your code below
        void Log(string s)
        {
            if (m_form.textBox1.InvokeRequired)
            {
                m_form.textBox1.Invoke(new Action(() => m_form.textBox1.AppendText(s + Environment.NewLine)));
            }
            else
            {
                m_form.textBox1.AppendText(s + Environment.NewLine);
            }
        }
        //
        ProcessStartInfo m_start;
        string m_tempfile;
        string m_filepath;
        void start_python()
        {
            var winpython = Path.Combine( Path.GetDirectoryName(Application.ExecutablePath),"winPython");
            var pythonexe = Path.Combine( winpython, "python", "python.exe");
            var script = Path.Combine( winpython,"gemini_image_sys.py" );
            var state = G.multiedit_control.m_state;

            Converter.Prepare();
            var code = Converter.GetFuncSrc(state);

            var picpath = Path.Combine( Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "StateGo");
            if (!Directory.Exists(picpath))
            {
                Directory.CreateDirectory(picpath);
            }

            //picpathの中で、 ファイル名を "{state}.png" とする。存在する場合は連番を付与する。
            var filename = state;
            m_filepath = Path.Combine(picpath, filename + ".png");
            if (File.Exists(m_filepath))
            {
                var cnt = 1;
                while (File.Exists(m_filepath))
                {
                    filename = $"{state}_{cnt}.png";
                    m_filepath = Path.Combine(picpath, filename + ".png");
                    cnt++;
                }
            }
            var prompt = "source code:\n" + code;
            prompt = EscapeCommandLineArgument(prompt);

            m_tempfile = Path.Combine(Path.GetTempPath(), "statego_aiimage.png");
            if (File.Exists(m_tempfile))
            {
                File.Delete(m_tempfile);
            }

            m_start = new ProcessStartInfo();
            m_start.FileName = pythonexe;
            m_start.Arguments = string.Format("\"{0}\" -p {1} -o {2} ", script, prompt, m_tempfile);
            m_start.UseShellExecute = false;
            m_start.RedirectStandardOutput = true;
            m_start.RedirectStandardError = true;
            m_start.CreateNoWindow = true;

            m_start.StandardOutputEncoding = System.Text.Encoding.UTF8; // ① 標準出力のエンコーディングをUTF-8に設定
            m_start.StandardErrorEncoding = System.Text.Encoding.UTF8; // ② 標準エラー出力のエンコーディングをUTF-8に設定
            m_start.CreateNoWindow = true;

            m_start.EnvironmentVariables["PYTHONIOENCODING"] = "UTF-8";
        }
        Process m_process;
        bool m_bProcessEnded = false;
        void start_process()
        {
            m_bProcessEnded = false;
            try
            {
                m_process = new Process();
                {
                    m_process.StartInfo = m_start;
                    m_process.EnableRaisingEvents = true;
                    m_process.Exited += (sender, e) =>
                    {
                        Log("Process exited.");
                        m_bProcessEnded = true;
                    };
                    // 非同期で出力を読み取る
                    m_process.OutputDataReceived += (sender, e) =>
                    {
                        if (!string.IsNullOrEmpty(e.Data))
                        {
                            Log(e.Data);
                        }
                    };
                    m_process.ErrorDataReceived += (sender, e) =>
                    {
                        if (!string.IsNullOrEmpty(e.Data))
                        {
                            Log("ERR: " + e.Data);
                        }
                    };
                    m_process.Start();
                    m_process.BeginOutputReadLine();
                    m_process.BeginErrorReadLine();
                }
            }
            catch (Exception ex)
            {
                Log("Failed to start process: " + ex.Message);
            }
        }
        private bool is_process_done()
        {
            if (m_process == null) return true;
            return m_bProcessEnded;
        }
        private void end_process()
        {
            if (m_process != null)
            {
                try
                {
                    if (!m_process.HasExited)
                    {
                        m_process.Kill();
                    }
                }
                catch (Exception ex)
                {
                    Log("Failed to kill process: " + ex.Message);
                }
                finally
                {
                    m_process.Dispose();
                    m_process = null;
                }
            }
        }
        private void copy_and_draw()
        {
            if (File.Exists(m_tempfile))
            {
                File.Copy(m_tempfile, m_filepath, true);
                //表示内に収める
                m_form.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
                var bmp = (Bitmap)Image.FromFile(m_filepath);
                if (m_form.m_changebmp != null)
                {
                    m_form.m_changebmp.Dispose();
                    m_form.m_changebmp = null;
                }
                int w, h;
                m_form.calc(bmp.Width, bmp.Height, out w, out h);
                m_form.m_changebmp = new Bitmap(w, h);
                using (var g = Graphics.FromImage(m_form.m_changebmp))
                {
                    g.DrawImage(bmp, 0, 0, m_form.m_changebmp.Width, m_form.m_changebmp.Height);
                }
                try { bmp.Dispose(); } catch { }
                m_form.draw();
            }
            else
            {
                Log("\n\n" + "Image file not found: " + m_filepath);
            }
        }


        private static string EscapeCommandLineArgument(string arg)
        {
            if (string.IsNullOrEmpty(arg)) return "\"\"";
            // ダブルクォートをエスケープ
            arg = arg.Replace("\\", "/").Replace("\"", "`");
            return $"\"{arg}\"";
        }

        void waiting_start()
        {
            m_form.waiting_textBox.Visible = true;
        }
        void waiting_end()
        {
            m_form.waiting_textBox.Visible = false;
        }
    }
}

/*  :::: PSGG MACRO ::::
:psgg-macro-start

commentline=// {%0}

@branch=@@@
<<<?"{%0}"/^brifc{0,1}$/
if ([[brcond:{%N}]]) { Goto( {%1} ); }
>>>
<<<?"{%0}"/^brelseifc{0,1}$/
else if ([[brcond:{%N}]]) { Goto( {%1} ); }
>>>
<<<?"{%0}"/^brelse$/
else { Goto( {%1} ); }
>>>
<<<?"{%0}"/^br_/
{%0}({%1});
>>>
@@@

:psgg-macro-end
*/

