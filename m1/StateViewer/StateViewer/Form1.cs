using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using stateview;
using StateViewer_starter2;
using System.IO;
using G=stateview.Globals;
using System.Diagnostics;

namespace PSGGEditor
{
    public partial class Form1:Form
    {
        public string m_target_psgg;
        public string m_target_xlsx;
        public bool   m_bNewFiles { get { return m_ss!=null ? m_ss.m_bNewFiles : false;  }  set { if (m_ss!=null) m_ss.m_bNewFiles = value; }  }
        public string m_version { get { return ver.version; } }
        public string m_buildtime { get { return ver.datetime; } }
        public string m_milestone { get { return ver.milestone; } }
        public string m_milestonetxt { get { return ver.milestonetxt; } }
        public string m_githash
        {
            get
            {
                if (__githash == null)
                {
                    __githash = (new githash()).hash;
                }
                return __githash;
            }
        } private string __githash;

        //新版用
        public bool m_bNew2019Files;
        public string m_new_target_psgg;
        public string m_new_target_xlsx;

        public Form1()
        {
            InitializeComponent();
            //Debugger.Launch();
        }

        StateView m_mc;
        Starter2 m_ss;

        private void StartButton_Click(object sender,EventArgs e)
        {
            //m_mc = new StateView();
            //m_mc.Init("");

            //Hide();
        }

        bool m_bRequestWizard = false;
        bool m_bRequestOpen = false;
        private void Form1_Load(object sender,EventArgs e)
        {
            //System.Diagnostics.Debugger.Launch();

            m_bRequestOpen = false;

            var args = Environment.GetCommandLineArgs();
            
            var _1stArg = args.Length>1 ? args[1] : string.Empty;
            var _2ndArg = args.Length>2 ? args[2] : string.Empty;
            var _3rdArg = args.Length>3 ? args[3] : string.Empty;

            if (!m_bRequestOpen && !string.IsNullOrEmpty(_1stArg) && File.Exists(_1stArg))
            {
                var pfr = new psggFileRead();
                if (pfr.Read(args[1]))
                {
                    m_target_psgg = args[1];
                    m_target_xlsx = pfr.xlsfile;
                    m_bRequestOpen = true;
                }
            }

            /*

                手がかりのファイル名をもとにpsggを探して開く
                -oc {手がかりファイル}
                
            */
            if (!m_bRequestOpen && !string.IsNullOrEmpty(_1stArg) && !string.IsNullOrEmpty(_2ndArg))
            {
                if (_1stArg.ToUpper() == "-OC" )
                {
                    string psgg;
                    string xlsx;
                    var b = PSGGFileUtil.FindPsggUsingClue(_2ndArg,out psgg, out xlsx, false);
                    if (b)
                    {
                        m_target_psgg = psgg;
                        m_target_xlsx =xlsx;
                        m_bRequestOpen = true;
                    }
                }
            }



            if (!m_bRequestOpen)
            {
                bool bHeadless = false;
                
                // Logging for debugging
                var debugLog = Path.Combine(Environment.CurrentDirectory, "headless_debug_startup.txt");
                StringBuilder logContent = new StringBuilder();

                var isNew = args.Any(a => a.Equals("/new", StringComparison.OrdinalIgnoreCase) || a.Equals("--new", StringComparison.OrdinalIgnoreCase));

                if (isNew)
                {
                    var template = GetArg(args, "/src");
                    var id = GetArg(args, "/name");
                    var dir = GetArg(args, "/dir");

                    if (!string.IsNullOrEmpty(template) && !string.IsNullOrEmpty(id))
                    {
                        if (string.IsNullOrEmpty(dir)) dir = Directory.GetCurrentDirectory();

                        try {
                            open_headless(template, id, dir);
                                bHeadless = true;
                                File.AppendAllText(debugLog, "open_headless returned. bHeadless=true\n");
                            } catch (Exception ex) {
                                File.AppendAllText(debugLog, $"open_headless EXCEPTION: {ex}\n");
                            }
                        }
                        else
                        {
                            logContent.AppendLine("Missing template or id. Headless aborted.");
                            File.AppendAllText(debugLog, logContent.ToString());
                        }
                    }
                    else 
                    {
                         File.AppendAllText(debugLog, logContent.ToString());
                    }



                if (!bHeadless)
                {
                    m_bRequestWizard = true;
                }
            }

            Bitmap img = Properties.Resources.statego;   //title; //Properties.Resources.fanfare2;

            pictureBox1.Image = img;
            pictureBox1.Refresh();

            timer1.Interval = string.IsNullOrEmpty( RegistryWork.Get_serial() ) ? 500 : 10;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (m_bRequestWizard)
            {
                m_bRequestWizard = false;
                open_wizard();
            }

            if (m_bRequestOpen)
            {
                m_bRequestOpen = false;

                m_mc = new StateView();
                m_mc.m_version = m_version;
                m_mc.m_githash = m_githash;
                m_mc.m_buildtime = m_buildtime;
                m_mc.m_milestone = m_milestone;
                m_mc.m_milestonetxt = m_milestonetxt;
                //m_mc.m_bNewFiles = m_bNewFiles; //新規作成された。 FileDB処理を受け取り側で

                if (m_bNewFiles) //新規ファイルに対して、1.1への変換を先にかける
                {
                    m_bNewFiles = false;

                    FileDbUtil.read_excel_force(m_target_xlsx);
                    FileDbUtil.write_filedb_all_files();

                    FileDbUtil.set_psgg_header_info(
                        xlsfile                : m_target_xlsx,
                        guid                   : Guid.NewGuid().ToString(),
                        readfrom_excel_or_psgg : false,
                        savemode_withexcel_or_psggonly : false,
                        check_excel_writable: false
                        );
                    G.psgg_file = m_target_psgg;
                    FileDbUtil.create_psgg();
                    File.Delete(m_target_xlsx);

                    m_mc.Init(m_target_xlsx,m_target_psgg);
                }
                else if (m_bNew2019Files)
                {
                    if (string.IsNullOrEmpty(m_new_target_xlsx) && !string.IsNullOrEmpty(m_new_target_psgg))
                    { 
                        m_new_target_xlsx = Path.ChangeExtension(m_new_target_psgg, ".xlsx");
                    }
                    m_mc.Init(m_new_target_xlsx,m_new_target_psgg);
                }
                else
                {
                    m_mc.Init(m_target_xlsx,m_target_psgg);
                }

                Hide();
            }
            if ( m_mc!=null &&  !m_mc.IsAlive())
            {
                this.Close();
                m_mc = null;
            }
        }

        private void buttonWizard_Click(object sender,EventArgs e)
        {
            //m_ss = new Starter();
            //m_ss.Init();
            //m_target_xlsx = m_ss.m_target_xls;
            //if (!string.IsNullOrEmpty(m_target_xlsx)) {
            //    m_bRequestOpen = true;
            //}
            open_wizard();
        }

        string GetArg(string[] args, string key)
        {
            for(int i = 0; i < args.Length; i++)
            {
                if (args[i].Equals(key, StringComparison.OrdinalIgnoreCase))
                {
                    if (i + 1 < args.Length)
                    {
                        var val = args[i + 1];
                        if (!val.StartsWith("/") && !val.StartsWith("-"))
                        {
                            return val;
                        }
                    }
                }
            }
            return null;
        }

        void open_headless(string template, string id, string dir)
        {
            m_ss = new Starter2();
            m_ss.m_version = m_version;
            m_ss.m_githash = m_githash;
            m_ss.m_buildtime = m_buildtime;

            m_ss.InitHeadless(template, id, dir);

            m_bNew2019Files = m_ss.m_bNew2019Files;
            m_new_target_psgg = m_ss.m_new_target_psgg;
            m_new_target_xlsx = m_ss.m_new_target_xls;
            
            if (m_bNew2019Files)
            {
                m_bRequestOpen = true; 
                return;
            }
            Close();
        }

        void open_wizard()
        {
            m_ss = new Starter2();
            m_ss.m_version = m_version;
            m_ss.m_githash = m_githash;
            m_ss.m_buildtime = m_buildtime;

            m_ss.Init();

            m_bNew2019Files = m_ss.m_bNew2019Files;
            m_new_target_psgg = m_ss.m_new_target_psgg;
            m_new_target_xlsx = m_ss.m_new_target_xls;
            
            if (m_bNew2019Files)
            {
                m_bRequestOpen = true;
                return;
            }

            m_target_psgg = m_ss.m_target_psgg;
            m_target_xlsx = m_ss.m_target_xls;
            if (!string.IsNullOrEmpty(m_target_xlsx)) {
                m_bRequestOpen = true;
            }
            else
            {
                Close();
            }
        }
    }
}
