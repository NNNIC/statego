using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace StateViewer_starter2
{
    public partial class CreateNewForm:Form
    {
        public static CreateNewForm V;

        private Start2Form m_sf              { get { return Start2Form.m_form; } }
        private CreateNewDetailTextFrom m_df { get { return m_sf.m_detailform; } }
        private string m_syslang             { get { return m_sf.m_syslang;    } set { m_sf.m_syslang = value;   } }
        private bool m_bNewFiles             { get { return m_sf.m_bNewFiles;  } set { m_sf.m_bNewFiles = value; } }

        public WORK.CreateFileWork     m_cw;

        public CreateNewForm()
        {
            V = this;
            InitializeComponent();
        }

        List<Action> m_update = new List<Action>();
        private void timer1_Tick(object sender,EventArgs e)
        {
            if (m_update!=null) m_update.ForEach(i=>i());
        }

        private void CreateNewForm_Load(object sender,EventArgs e)
        {
            this.DialogResult = DialogResult.None;
            movecontrol_init();
            createcontrol_init();

            var a = label_anchor.Location;

            this.Size = new Size(a.X,a.Y + SystemInformation.CaptionHeight) ;  //new Size(511,296);

            ResetGroupBox();

            textBoxReadFromPath.Text = WORK.TEMPLATEPATH;
        }

        public void ResetGroupBox()
        {
            groupBox1.Location = new Point(12,12);
            groupBox2.Location = 
            groupBox3.Location = 
            groupBox4.Location = 
            groupBox5.Location = 
            groupBox6.Location = new Point(537,12);
        }

        #region movecontrol
        GroupAnimControl m_gac;
        void movecontrol_init()
        {
            m_gac = new GroupAnimControl();
            m_gac.Start();
            m_update.Add(m_gac.update);
        }
        public void movecontrol_req(GroupAnimControl.REQ req)
        {
            m_gac.m_req = req;
        }
        #endregion

        #region createcontrol
        CreateControl m_cc;
        void createcontrol_init()
        {
            m_cc=new CreateControl();
            m_cc.Start();
            m_update.Add(m_cc.update);
        }
        void createcontrol_push(object obj)
        {
            if (m_cc!=null) m_cc.m_inputObject = obj;
        }
        #endregion

        private void button_g1ok_Click(object sender, EventArgs e)
        {
            movecontrol_req(GroupAnimControl.REQ.G1toG2);
            createcontrol_push((Button)sender);

            button_shortcut.Visible = string.IsNullOrEmpty(WORK.SELECT_SETTING.manager_src);

        }

        private void button_g2ok_Click(object sender,EventArgs e)
        {
            //var dlg = new CreateNewForm_ShortcutFotm();
            //if (dlg.ShowDialog(this) == DialogResult.OK)
            //{

            //    movecontrol_req(GroupAnimControl.REQ.G2toG3);
            //    createcontrol_push((Button)sender);
            //}

            //if (!string.IsNullOrEmpty(WORK.SELECT_SETTING.manager_src)) //マネージファイルがある場合は、簡易設定はできない。
            //{
            //    movecontrol_req(GroupAnimControl.REQ.G2toG3);
            //    createcontrol_push((Button)sender);
            //    return;
            //}

            //var sm = new ShortcutControl();
            //sm.m_form = this;
            //sm.Run();
            //if (sm.m_result == ShortcutControl.RESULT.setdetail)
            //{
            //    movecontrol_req(GroupAnimControl.REQ.G2toG3);
            //    createcontrol_push((Button)sender);
            //}
            //else if (sm.m_result == ShortcutControl.RESULT.gocopy)
            //{
            //    movecontrol_req(GroupAnimControl.REQ.G2toG6);
            //    createcontrol_push((Button)sender);
            //}

            movecontrol_req(GroupAnimControl.REQ.G2toG3);
            createcontrol_push((Button)sender);
        }

        private void button_shortcut_Click(object sender, EventArgs e)
        {
            var sm = new ShortcutControl();
            sm.m_form = this;
            sm.Run();
            if (sm.m_result == ShortcutControl.RESULT.setdetail)
            {
                movecontrol_req(GroupAnimControl.REQ.G2toG3);
                createcontrol_push((Button)sender);
            }
            else if (sm.m_result == ShortcutControl.RESULT.gocopy)
            {
                movecontrol_req(GroupAnimControl.REQ.G2toG6);
                createcontrol_push((Button)sender);
            }
        }

        private void button_g3ok_Click(object sender,EventArgs e)
        {
            movecontrol_req(GroupAnimControl.REQ.G3toG4);
            createcontrol_push((Button)sender);
        }

        private void button_g4ok_Click(object sender,EventArgs e)
        {
            if (string.IsNullOrEmpty(WORK.SELECT_SETTING.manager_src))
            {
                m_cw = new WORK.CreateFileWork();
                movecontrol_req(GroupAnimControl.REQ.G4toG6);
            }
            else
            { 
                movecontrol_req(GroupAnimControl.REQ.G4toG5);
            }
            createcontrol_push((Button)sender);
        }

        private void button_g5ok_Click(object sender,EventArgs e)
        {
            m_cw = new WORK.CreateFileWork();
            movecontrol_req(GroupAnimControl.REQ.G5toG6);
            createcontrol_push((Button)sender);
        }

        private void button_g6back_Click(object sender,EventArgs e)
        {
            if (WORK.SrcDocFolderDefineType != SRC_DOC_FOLDER_DFEINE_TYPE.none)
            {
                movecontrol_req(GroupAnimControl.REQ.G6toG2);
            }
            else
            { 
                if (string.IsNullOrEmpty(WORK.SELECT_SETTING.manager_src))
                {
                    movecontrol_req(GroupAnimControl.REQ.G6toG4);
                }
                else
                { 
                    movecontrol_req(GroupAnimControl.REQ.G6toG5);
                }
            }
            createcontrol_push((Button)sender);
        }

        private void button_g5back_Click(object sender,EventArgs e)
        {
            movecontrol_req(GroupAnimControl.REQ.G5toG4);
            createcontrol_push((Button)sender);
        }

        private void button_g4back_Click(object sender,EventArgs e)
        {
            movecontrol_req(GroupAnimControl.REQ.G4toG3);
            createcontrol_push((Button)sender);
        }

        private void button_g3back_Click(object sender,EventArgs e)
        {
            movecontrol_req(GroupAnimControl.REQ.G3toG2);
            createcontrol_push((Button)sender);
        }
        private void button_g3clear_Click(object sender, EventArgs e)
        {
            textBoxDocFolder.Text = string.Empty;
        }
        private void button_g4clear_Click(object sender, EventArgs e)
        {
            textBoxGenFolder.Text = string.Empty;
        }
        private void button_g2back_Click(object sender,EventArgs e)
        {
            movecontrol_req(GroupAnimControl.REQ.G2toG1);
            createcontrol_push((Button)sender);
        }

        private void button_g1cancel_Click(object sender,EventArgs e)
        {
            createcontrol_push((Button)sender);
            Close();
        }

        private void button_g6cancel_Click(object sender,EventArgs e)
        {
            createcontrol_push((Button)sender);
            Close();
        }

        private void textBoxPrefix_TextChanged(object sender,EventArgs e)
        {
            var text = textBoxPrefix.Text;
            text = text.Trim();
            if (!string.IsNullOrEmpty(text)) { 
                var newtext = string.Empty;
                for(var i = 0; i<text.Length; i++)
                {
                    var c = text[i].ToString();
                    var b = false;
                    if (i==0)
                    {
                        if (RegexUtil.IsMatch(@"[a-zA-Z_]",c)) { b = true; }
                    }
                    else
                    {
                        if (RegexUtil.IsMatch(@"[0-9a-zA-Z]",c)) { b= true; }
                    }
                    if (b)
                    {
                        newtext += c;
                    }
                }
                textBoxPrefix.Text = newtext; 
                textBoxPrefix.Select(newtext.Length,0);
            }
            createcontrol_push(sender);
            WORK.UpdateByInputText();
        }

        private void textBoxDocFolder_TextChanged(object sender,EventArgs e)
        {
            createcontrol_push(sender);
            WORK.UpdateByInputText();
        }

        private void textBoxGenFolder_TextChanged(object sender,EventArgs e)
        {
            createcontrol_push(sender);
            WORK.UpdateByInputText();
        }

        private void listBox_title_SelectedIndexChanged(object sender,EventArgs e)
        {
            WORK.setup_setting(this.listBox_title.SelectedIndex);
            this.Text = "Create a new state machine / [" + WORK.SELECT_SETTING.title + "]";
        }

        private void listBox_title_DoubleClick(object sender,EventArgs e)
        {
            m_df.m_bUndefined = true;
            m_df.ShowDialog(this);
        }

        private void label3_Click(object sender,EventArgs e)
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

        private void folderBrowserDialog1_HelpRequest(object sender,EventArgs e)
        {

        }

        private void button1_Click(object sender,EventArgs e)
        {
            m_df.m_bUndefined = false;
            m_df.ShowDialog(this);
        }

        private void buttonOpenDocFolderDialog_Click(object sender,EventArgs e)
        {
#if obs
            var text = this.textBoxDocFolder.Text;
            if (!string.IsNullOrEmpty(text))
            {
                folderBrowserDialog1.SelectedPath  = text;
            }
            if (string.IsNullOrEmpty(folderBrowserDialog1.SelectedPath))
            {
                var xlsdir =  m_sf.get_xlsdir_candidate();
                if (!string.IsNullOrEmpty(xlsdir))
                {
                    folderBrowserDialog1.SelectedPath = xlsdir;
                }
            }
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxDocFolder.Text = folderBrowserDialog1.SelectedPath;
            }
#else
            var text = this.textBoxDocFolder.Text;
            using (var dlg = new CommonOpenFileDialog())
            {
                dlg.IsFolderPicker = true;
                dlg.Title = "Select Folder";
                if (!string.IsNullOrEmpty(text))
                { 
                    dlg.InitialDirectory = text;
                }
                else
                {
                    var xlsdir =  m_sf.get_xlsdir_candidate();
                    if (!string.IsNullOrEmpty(xlsdir))
                    {
                        dlg.InitialDirectory = xlsdir;
                    }
                }
                var result = dlg.ShowDialog();
                if (result== CommonFileDialogResult.Ok)
                {
                    textBoxDocFolder.Text =  dlg.FileName;
                }
            }
#endif


        }

        private void buttonOpenGenFolderDialog_Click(object sender,EventArgs e)
        {
#if obs
            var text = textBoxGenFolder.Text;
            if (!string.IsNullOrEmpty(text))
            {
                folderBrowserDialog1.SelectedPath = text;
            }
            if (string.IsNullOrEmpty(folderBrowserDialog1.SelectedPath))
            {
                var xlsdir = m_sf.get_xlsdir_candidate();
                if (!string.IsNullOrEmpty(xlsdir))
                {
                    folderBrowserDialog1.SelectedPath = xlsdir;
                }
            }
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxGenFolder.Text = folderBrowserDialog1.SelectedPath;
            }
#else
            var text = textBoxGenFolder.Text;
            using (var dlg = new CommonOpenFileDialog())
            {
                dlg.IsFolderPicker = true;
                dlg.Title = "Select Folder";
                if (!string.IsNullOrEmpty(text))
                { 
                    dlg.InitialDirectory = text;
                }
                else
                {
                    var xlsdir =  m_sf.get_xlsdir_candidate();
                    if (!string.IsNullOrEmpty(xlsdir))
                    {
                        dlg.InitialDirectory = xlsdir;
                    }
                }
                var result = dlg.ShowDialog();
                if (result== CommonFileDialogResult.Ok)
                {
                    textBoxGenFolder.Text =  dlg.FileName;
                }
            }
#endif
        }

        private void button_g6ok_Click(object sender,EventArgs e) // Create
        {
            MessageBox.Show(WordStorage.Res.Get("newformatwarn",m_syslang /*"Beta6からの新フォーマット移行のため、Excelファイルは削除されます。"*/));

            m_cw.Save();
         
            RegistryWork.Set_templatedir(WORK.TEMPLATEPATH);

            RegistryWork.Set_xlsdir(WORK.XLSDIR);
            RegistryWork.Set_gendir(WORK.GENDIR);

            RegistryWork.Set_statemchine(this.textBoxStateMachineName.Text);

            m_bNewFiles = true; //新規に作成された。FileDB処理をEditor起動時に依頼する。
        }

        private void textBoxStateMachineName_TextChanged(object sender, EventArgs e)
        {
            WORK.UpdateByInputText();
            createcontrol_push(sender);
        }

        private void textBoxReadFromPath_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (Directory.Exists(textBoxReadFromPath.Text))
                {   
                    WORK.TEMPLATEPATH = textBoxReadFromPath.Text;
                    WORK.SetupCreateSection();
                    WORK.Setup_for_lang(m_syslang);

                    RegistryWork.Set_templatedir(WORK.TEMPLATEPATH);
                }
            }
        }

        private void label_reset_Click(object sender, EventArgs e)
        {
            WORK.TEMPLATEPATH_Reset();
            WORK.SetupCreateSection();
            WORK.Setup_for_lang(m_syslang);
        }

        private void folderBrowserDialog1_HelpRequest_1(object sender, EventArgs e)
        {

        }

        private void folderBrowserDialog2_HelpRequest(object sender, EventArgs e)
        {

        }

        private void label_help_1_Click(object sender, EventArgs e)
        {
            HelpJumpUtil.Jump("tec_userguide","start-dialog-sel-starter",m_syslang=="jpn");
        }

        private void label_help_2_Click(object sender, EventArgs e)
        {
            HelpJumpUtil.Jump("tec_userguide","start-dialog-newname",m_syslang=="jpn");
        }

        private void label_help_3_Click(object sender, EventArgs e)
        {
            HelpJumpUtil.Jump("tec_userguide","start-dialog-new",m_syslang=="jpn");
        }

        private void label_help_4_Click(object sender, EventArgs e)
        {
            HelpJumpUtil.Jump("tec_userguide","start-dialog-new",m_syslang=="jpn");
        }

        private void label_help_5_Click(object sender, EventArgs e)
        {
            HelpJumpUtil.Jump("tec_userguide","start-dialog-new",m_syslang=="jpn");
        }

        private void label_help_6_Click(object sender, EventArgs e)
        {
            HelpJumpUtil.Jump("tec_userguide","start-dialog-new",m_syslang=="jpn");
        }
    }
}
