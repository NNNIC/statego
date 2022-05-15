using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace StateViewer_starter2
{
    public partial class Start2Form:Form
    {
        public static Start2Form m_form;

        public CreateNewForm           m_createform;
        public CreateNewDetailTextFrom m_detailform;

        public NEW2019.NewForm         m_newform;

        public Starter2 m_root;
        public string m_version     { get { return m_root.m_version;  } }
        public string m_buildtime   { get {return m_root.m_buildtime; } }
        public string m_githash     { get { return m_root.m_githash; } }
        public string m_target_xlsx { set { m_root.m_target_xls  = value; } get { return m_root.m_target_xls;  } }
        public string m_target_psgg { set { m_root.m_target_psgg = value; } get { return m_root.m_target_psgg; } }
        public bool   m_bNewFiles   { set { m_root.m_bNewFiles = value;   } get { return m_bNewFiles;          } }

        //2019 11 version
        public bool   m_bNew2019Files { get; private set; }
        public string m_new_target_xlsx;
        public string m_new_target_psgg;

        public Start2Form(Starter2 root)
        {
            m_bNew2019Files = false;

            m_root = root;
            m_form = this;
            InitializeComponent();
        }

        private int size_y_short = 340;
        private int size_y_long  = 607;
        private void Start2Form_Load(object sender,EventArgs e)
        {

            m_createform = new CreateNewForm();
            m_detailform = new CreateNewDetailTextFrom();

            //m_newform = new NEW2019.NewForm();

            labelVersion.Text = m_version + " " + m_githash;
            labelBuildTime.Text = m_buildtime;
            //labelgithash.Text = "";//m_githash;

            DialogResult = DialogResult.None;
            m_target_xlsx = null;

            comboBoxHistory.Items.Clear();
            var historylist = Starter2.GetHistory();
            if (historylist!=null)
            {
                foreach(var i in historylist)
                {
                    var i2 = PathUtil.ShortenPath(i,48);
                    comboBoxHistory.Items.Add(i2);
                    if (string.IsNullOrEmpty(comboBoxHistory.Text))
                    {
                        comboBoxHistory.Text = i;
                    }
                }
            }
            m_seleted = 0;
            try {  comboBoxHistory.SelectedIndex = 0; } catch { }

            WORK.InitSection();

            WORK.SetupCreateSection();

			syslang_preset();
			syslang_set();

            set_lang_label_color();

            WORK.Setup_for_lang(m_syslang);

            FindFile_init();

            this.Size = new Size(554, size_y_long);   //  607 
            change_size_height_from();
        }

        //private string program_lang = null;
        private void listBoxLanguage_SelectedIndexChanged(object sender,EventArgs e)
        {
            //var index = this.listBoxLanguage.SelectedIndex;
            //if (index >=0 && index < this.listBoxLanguage.Items.Count)
            //{
            //    program_lang = this.listBoxLanguage.Items[index].ToString();
            //    WORK.Setup_for_lang(program_lang, m_syslang);
            //}
        }

        private void listBox_title_SelectedIndexChanged(object sender,EventArgs e)
        {
            WORK.setup_setting(this.listBox_title.SelectedIndex);
        }

        private void textBoxPrefix_TextChanged(object sender,EventArgs e)
        {
            WORK.UpdateByInputText();
        }

        private void textBoxExcelFolder_TextChanged(object sender,EventArgs e)
        {
            WORK.UpdateByInputText();
        }

        private void textBoxGenerateFolder_TextChanged(object sender,EventArgs e)
        {
            WORK.UpdateByInputText();
        }

        int m_seleted;
        private void comboBoxHistory_SelectedIndexChanged(object sender,EventArgs e)
        {
            var list = Starter2.GetHistory();
            var index = comboBoxHistory.SelectedIndex;
            var text = (list != null && index >=0 && index < list.Length) ? list[index] : string.Empty;
            if (!string.IsNullOrEmpty(text))
            { 
                labelFileNameOnHistory.Text = Path.GetFileName( text );
                labelSelectOnHistory.Text = TextUtil.BreakUpText(text,90);
            }
            m_seleted = index;
        }

        private void buttonOpenDialog_Click(object sender,EventArgs e)
        {
            openFileDialog1.Filter = @"StateGo File(*.psgg)|*.psgg|Excel Files(*.xlsx;*.xlsm)|*.xlsx;*.xlsm|All Files(*.*)|*.*";
            //openFileDialog1.InitialDirectory = 
            {
                var historylist = Starter2.GetHistory();
                if (historylist!=null && historylist.Length>0)
                {
                    openFileDialog1.InitialDirectory = Path.GetDirectoryName( historylist[0] );
                }
            }

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var file = openFileDialog1.FileName;
                if (_setFile(file))
                { 
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
        }
        private bool _setFile(string file)
        {
            string psgg;
            string xlsx;
            //var b  = _setFile2(file, out psgg, out xlsx, true);
            var b= PSGGFileUtil.FindPsggUsingClue(file, out psgg, out xlsx, true);
            if (b)
            {
                m_target_xlsx = xlsx;
                m_target_psgg = psgg;
                return true;
            }
            return false;
        }
#if obs
        private bool _setFile2(string file, out string psgg, out string xlsx, bool bAskMsgBox)
        {
            var ext = Path.GetExtension(file).ToLower();
            if(ext == ".psgg")
            {
                return _setFile_psgg(file, out psgg,out xlsx);
            }
            else if (ext == ".xlsx" || ext==".elxm") //エクセルファイル or マクロ入りエクセルファイル
            {
                /*m_target_xlsx*/xlsx = file;
                /*m_target_psgg*/psgg = null;
                return true;
            }

            // 1. ファイル内に psgg-file:相対位置がある。
            // 2. ファイル内にある　psggConverterLib.dll converted from ファイル名から、そのファイルを検索する。
            try { 
                var buf = File.ReadAllText(file);
                if (!string.IsNullOrEmpty(buf))
                {
                    var psggfile = "psgg-file:";
                    var find = buf.IndexOf(psggfile);
                    if (find >=0)
                    {
                        var sample = buf.Substring( find + psggfile.Length );
                        var testfile = sample.Split('\x0d','\x0a')[0].Trim();
                        return _setFile_psgg(testfile, out psgg, out xlsx);
                    }

                    var psggconverter = "psggConverterLib.dll converted from";
                    var find2 = buf.IndexOf(psggconverter);
                    if (find2>=0)
                    {
                        var sample = buf.Substring( find2 + psggconverter.Length );
                        var testfile = sample.Split('\x0d','\x0a')[0].Trim().TrimEnd('.');
                        var bTimeout=false;
                        var testfullpath = PathUtil.FindTraverseDownAndUp(Path.GetDirectoryName(file),testfile,1000, out bTimeout );
                        if (!bTimeout)
                        {
                            var bOk = true;
                            if (bAskMsgBox)
                            {
                                bOk = MessageBox.Show("The following file will be opened.\n" + testfullpath,"Confirmation", MessageBoxButtons.OKCancel) == DialogResult.OK;
                            }
                            if (bOk)
                            {
                                /*m_target_xlsx*/xlsx = testfullpath;
                                /*m_target_psgg*/psgg = null;
                                return true;
                            }
                        }
                    }
                }
                {
                    var cfile = Path.GetFileNameWithoutExtension(file);
                    if (cfile.EndsWith("_created")) cfile = cfile.Substring(0,cfile.Length - "_created".Length);
                    var cfile_w_ext = cfile + ".psgg";
                    
                    var bTimeOut = false;
                    var testfullpath = PathUtil.FindTraverseDownAndUp(Path.GetDirectoryName(file), cfile_w_ext,1000, out bTimeOut);
                    if (!bTimeOut)
                    {
                        var bOk = true;
                        if (bAskMsgBox)
                        {
                            bOk = MessageBox.Show("The following file will be opened.\n" + testfullpath,"Confirmation", MessageBoxButtons.OKCancel) == DialogResult.OK;
                        }
                        if (bOk)
                        { 
                            return _setFile_psgg(testfullpath, out psgg, out xlsx);
                        }
                    } 
                }

            } catch (SystemException e)
            {
                Console.WriteLine("{A6C7A169-D9AE-4CBB-8EF5-906603E1E9BF} "  + e.Message);
            }
            psgg = null;
            xlsx = null;
            return false;
        }

        private bool _setFile_psgg(string file, out string psgg, out string xlsx)
        {
            var data = PSGGFileUtil.Read(file);
            if (data != null && !string.IsNullOrEmpty(data.file))
            {
                /*m_target_xlsx*/ xlsx = (new FileInfo(Path.Combine(Path.GetDirectoryName(file), data.file))).FullName;
                /*m_target_psgg*/ psgg = (new FileInfo(file)).FullName;
                return true;
            }
            else
            {
                /*m_target_xlsx*/ xlsx = null;
                /*m_target_psgg*/ psgg = null;
                return false;
            }
        }
#endif

        private void buttonOpenInHistory_Click(object sender,EventArgs e)
        {
            var list = Starter2.GetHistory();
            var index = m_seleted;
            if (list == null || index < 0  || index >= list.Length ) return;

            var file = list[index];
            if (!string.IsNullOrEmpty(file) && File.Exists(file))
            {
                if (_setFile(file))
                {
                    DialogResult = DialogResult.OK;
                    Close();         
                }
            }
        }

        [Obsolete]
        private void buttonOpenDistDirDialog_Click(object sender,EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxExcelFolder.Text))
            {
                folderBrowserDialog1.SelectedPath = textBoxExcelFolder.Text;
            }

            if (string.IsNullOrEmpty(folderBrowserDialog1.SelectedPath))
            {
                var xlsdir = get_xlsdir_candidate();
                if (!string.IsNullOrEmpty(xlsdir))
                {
                    folderBrowserDialog1.SelectedPath = xlsdir;
                }
            }

            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxExcelFolder.Text = folderBrowserDialog1.SelectedPath;
            }
        }
        public string get_xlsdir_candidate()
        {
            var historylist = Starter2.GetHistory();
            if (historylist==null || historylist.Length ==0) return null;
            try {
                var path = historylist[0];
                var di = new DirectoryInfo( Path.GetDirectoryName(path));
                return di.FullName;
            }
            catch { }
            return null;
        }
        public string get_gendir_candidate()
        {
            var historylist = Starter2.GetHistory();
            if (historylist==null || historylist.Length ==0) return null;
            try {
                var path = historylist[0];
                var di = new DirectoryInfo( Path.GetDirectoryName(path));
                if (RegexUtil.IsMatch(@"\\doc$",di.FullName))
                {
                    return di.FullName.Substring(0,di.FullName.Length-4);
                }
                return di.FullName;
            }
            catch { }
            return null;
        }


        [Obsolete]
        private void buttonOpenGenDirDialog_Click(object sender,EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxGenerateFolder.Text))
            {
                folderBrowserDialog1.SelectedPath = textBoxGenerateFolder.Text;
            }
            if (string.IsNullOrEmpty(folderBrowserDialog1.SelectedPath))
            {
                var xlsdir = get_xlsdir_candidate();
                if (!string.IsNullOrEmpty(xlsdir))
                {
                    folderBrowserDialog1.SelectedPath = xlsdir;
                }
            }
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxGenerateFolder.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void buttonCreate_Click(object sender,EventArgs e)
        {
            WORK.CreateFiles();

            RegistryWork.Set_templatedir(WORK.TEMPLATEPATH);
            RegistryWork.Set_prefix(this.textBoxPrefix.Text);
            RegistryWork.Set_xlsdir(this.textBoxExcelFolder.Text);
            RegistryWork.Set_gendir(this.textBoxGenerateFolder.Text);

            //test
            //var xlsfile = @"C:\Users\gea01\Documents\jx\project-jx-opt3\9000_tools\state_managemet_tools-master\state-chart2\m1\test.xlsx";
            //ExcelUtil.AddSheetWithValue(xlsfile,"hogesheet","hoge\nhoge\nhoge\nhoge\n0915");
            //var list = new Dictionary<string,string>();
            //list.Add("test-add-#1","#1value");
            //list.Add("test-add-#2","#2value");
            //list.Add("test-add-#3","#3value");
            //list.Add("test-add-#4","#4value");
            //ExcelUtil.AddSheetWithValue(xlsfile,list);
        }

		public bool m_eng_or_jpn {
            get { 
                if (_eng_or_jpn == null)
                {
                    var regst_lang = RegistryWork.Get_lang();
                    if (regst_lang == "en") {  _eng_or_jpn = true; }
                    else if (regst_lang == "jp") { _eng_or_jpn = false; }
                    else { 
                        _eng_or_jpn = !(System.Globalization.CultureInfo.CurrentCulture.Name.StartsWith("ja-"));
                    }
                }
                return (bool)_eng_or_jpn;
            }
            set
            {
                _eng_or_jpn = value;
            }
        }
        bool? _eng_or_jpn = null;

		private Color lang_focus_front= Color.FromArgb(255,250,250);
		private Color lang_focus_back = Color.FromArgb(95,158,160);
		private Color lang_front      = Color.FromArgb(255,255,255);
		private Color lang_back       = Color.FromArgb(211,211,211);
		private void labelJP_Click(object sender,EventArgs e)
		{
			m_eng_or_jpn = false;
			//labelJP.BackColor = lang_focus_back;
			//labelEN.BackColor = lang_back;

			//labelJP.ForeColor = lang_focus_front;
			//labelEN.ForeColor = lang_front;

            set_lang_label_color();
			syslang_set();
            WORK.Setup_for_lang(m_syslang);

            RegistryWork.Set_lang("jp");

            update_label_collapse();

		}

        private void set_lang_label_color()
        {
            if (m_eng_or_jpn)
            {
			    labelJP.BackColor = lang_back;
			    labelEN.BackColor = lang_focus_back;

			    labelJP.ForeColor = lang_front;
			    labelEN.ForeColor = lang_focus_front;

            }
            else
            {
			    labelJP.BackColor = lang_focus_back;
			    labelEN.BackColor = lang_back;

			    labelJP.ForeColor = lang_focus_front;
			    labelEN.ForeColor = lang_front;
            }
        }


		private void labelEN_Click(object sender,EventArgs e)
		{
			m_eng_or_jpn = true;
			//labelJP.BackColor = lang_back;
			//labelEN.BackColor = lang_focus_back;

			//labelJP.ForeColor = lang_front;
			//labelEN.ForeColor = lang_focus_front;

            set_lang_label_color();
			syslang_set();
            WORK.Setup_for_lang(m_syslang);
            RegistryWork.Set_lang("en");
            update_label_collapse();
		}

		public string m_syslang = "jpn";
        List<Control> m_syslanobjs = new List<Control>();
        private void syslang_preset()
        {
            //難読化させると機能しない
            //var type = this.GetType();
            //var fields = type.GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            //foreach(var f in fields)
            //{
            //    var obj = f.GetValue(this);
            //    if (obj is Control)
            //    {
            //        try { 
            //            dynamic v = obj;
            //            var tagstr = get_text_id(v.Text);
            //            if (!string.IsNullOrEmpty(tagstr))
            //            {
            //                v.Tag = tagstr;
            //                m_syslanobjs.Add(v);
            //            }
            //        } catch { }
            //    }
            //}
            Action<Control> add = (c) => {
                var tagstr = get_text_id(c.Text);
                c.Tag = tagstr;
                m_syslanobjs.Add(c);
            };

            add((Control)this.groupBox2);
            add((Control)this.groupBox3);
            add((Control)this.groupBox4);

            add((Control)this.buttonCreateNew);
            add((Control)this.buttonOpenDialog);
            add((Control)this.buttonOpenInHistory);

            add((Control)this.groupBox6);
            add((Control)this.textBox_drop);
            add((Control)this.textBox_dropAnalize);

            add((Control)this.groupBox5);
            add((Control)this.label10);
            add((Control)this.textBox_findfile_exp);
            add((Control)this.button_search);
            add((Control)this.label11);

            var type = m_createform.GetType();
            var fields = type.GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
            
            foreach(var f in fields)
            {
                var obj = f.GetValue(m_createform);
                if (obj is Control)
                {
                    try { 
                        Control v = obj as Control;
                        var tagstr = get_text_id(v.Text);
                        if (!string.IsNullOrEmpty(tagstr))
                        {
                            v.Tag = tagstr;
                            m_syslanobjs.Add(v);
                        }
                    } catch { }
                }
            }
        }

		private string get_text_id(string s)
		{
            var match = RegexUtil.Get1stMatch(@"^\[.+\]",s);
            if (string.IsNullOrEmpty(match)) return string.Empty;

			var id = match.Trim('[',']');

			return id;
		}

        private void syslang_set()
        {
			m_syslang = m_eng_or_jpn ? "en" : "jpn";
            foreach(var c in m_syslanobjs)
            {
                try { 
                    if (c is Control)
                    { 
                        var d = c as Control;
                        c.Text = WordStorage.Res.Get(c.Tag.ToString(),m_syslang);
                    }
                } catch { }
            }
        }

        private void labelReadFrom_Click(object sender,EventArgs e)
        {
            folderBrowserDialogTemplate.SelectedPath = WORK.TEMPLATEPATH;
            try {
                if (folderBrowserDialogTemplate.ShowDialog(this) == DialogResult.OK)
                {
                    WORK.TEMPLATEPATH = folderBrowserDialogTemplate.SelectedPath;
                    WORK.SetupCreateSection();
                    WORK.Setup_for_lang(m_syslang);

                    RegistryWork.Set_templatedir(WORK.TEMPLATEPATH);
                }
                else
                {
                    if (MessageBox.Show("Reset path ?","Question",MessageBoxButtons.YesNo)== DialogResult.Yes)
                    {
                        WORK.TEMPLATEPATH_Reset();
                        WORK.SetupCreateSection();
                        WORK.Setup_for_lang(m_syslang);
                    }
                }
            } catch
            { }
        }

        private void button1_Click(object sender,EventArgs e)
        {
            //var form = new CreateNewForm();
            //form.ShowDialog();
            //m_createform.ShowDialog();

            buttonNew_Click(sender,e);
        }
        private void buttonNew_Click(object sender, EventArgs e)
        {
            m_bNew2019Files = false;

            m_newform = new NEW2019.NewForm();
            DialogResult rc = DialogResult.None;
            try { 
                rc = m_newform.ShowDialog();
            } catch (SystemException e2) {
                Console.WriteLine("{69A2D8BF-756A-48E3-9E53-A2AB9B260A89} " + e2.Message);
            }
            if (rc == DialogResult.OK)
            {
                //成功
                m_new_target_psgg = m_newform.m_target_psgg;
                m_new_target_xlsx = m_newform.m_target_xlsx;
                if (string.IsNullOrEmpty(m_new_target_xlsx)) //互換性のためにダミー
                {
                    m_new_target_xlsx = Path.Combine(
                        Path.GetDirectoryName(m_new_target_psgg),
                        Path.GetFileNameWithoutExtension(m_new_target_psgg) + ".xlsx"
                        );
                }

                m_bNew2019Files = true;

                m_form.DialogResult = DialogResult.OK;
                m_form.Close();
            }
            else if (rc == DialogResult.Retry)
            {
                //旧バージョンへ
                m_createform.ShowDialog();
                
            }
            m_newform = null;
        }

        private void label_help_Click(object sender, EventArgs e)
        {
            //ExecUtil.execute_start("https://statego.programanic.com/tec3/tec_userguide_j.html#start-dialog","");
            HelpJumpUtil.Jump("tec_userguide","start-dialog",!m_eng_or_jpn);
        }

        private void label_help_new_Click(object sender, EventArgs e)
        {
            //ExecUtil.execute_start("https://statego.programanic.com/tec3/tec_userguide_j.html#start-dialog-new","");
            HelpJumpUtil.Jump("tec_userguide","start-dialog-new",!m_eng_or_jpn);
        }

        private void groupBox5_DragDrop(object sender, DragEventArgs e)
        {
        }

        private void groupBox5_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void textBox_drop_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                foreach(string fileName in (string[])e.Data.GetData(DataFormats.FileDrop))
                {
                    if (_setFile(fileName))
                    {
                        DialogResult = DialogResult.OK;
                        Close();
                        break;
                    }


                }
            }

        }

        private void textBox_drop_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

#region Find File

        FileCandidateControl m_fcc;

        private void FindFile_init()
        {
            //this.textBox_findfilename.Parent = this.textBox_findfile_exp;
            //this.textBox_findfilename.back

            this.listBox_candidates.Items.Clear();
            this.listBox_candidates.Items.Add("");
            this.listBox_candidates.Items.Add("");
            this.listBox_candidates.Items.Add("");
            this.listBox_candidates.Items.Add("");
            this.listBox_candidates.Items.Add("");

            m_fcc = new FileCandidateControl();
            m_fcc.m_form = this;
            m_fcc.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try { 
            if (m_fcc!=null) m_fcc.Update();
            } catch (SystemException e2) {
                System.Diagnostics.Debug.WriteLine(e2.Message);
            }
        }

        private void textBox_findfilename_TextChanged(object sender, EventArgs e)
        {
            m_fcc.m_bTxtChg = true;   
        }
#endregion

        private void textBox_findfile_exp_Enter(object sender, EventArgs e)
        {
            textBox_findfile_exp.Visible = false;
            textBox_findfile_exp.Enabled = false;
            textBox_findfilename.Focus();
        }

        private void textBox_findfilename_Leave(object sender, EventArgs e)
        {
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            m_fcc.m_bBtnPush = true;
        }

        private void label_collapse_Click(object sender, EventArgs e)
        {
           change_size_height_from();
        }

        private void change_size_height_from(int? iy=null)
        {
            var y =  iy!=null ? (int)iy : this.Size.Height;

            if (y == size_y_short)
            {
                y = size_y_long;
                //this.label_collapse.Text = WordStorage.Res.Get("S25",m_syslang);//  "▲ Fold";
                this.label_collapse.Tag = "S25";
            }
            else
            {
                y = size_y_short;
                //this.label_collapse.Text = WordStorage.Res.Get("S26",m_syslang);//"▼ Collapse";
                this.label_collapse.Tag = "S26";
            }
            this.Size = new Size(this.Size.Width, y);

            update_label_collapse();
        }

        private void update_label_collapse()
        {
            var tag = this.label_collapse.Tag?.ToString();
            if (!string.IsNullOrEmpty(tag))
            {
                this.label_collapse.Text = WordStorage.Res.Get(tag,m_syslang);//"▼ Collapse";
            }
        }


        private void listBox_candidates_DoubleClick(object sender, EventArgs e)
        {
            var s = this.listBox_candidates.SelectedItem.ToString();
            if (_setFile(s))
            {
                Close();
            }
        }

    }
}
