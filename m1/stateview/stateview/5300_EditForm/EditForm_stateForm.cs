//<<<include=using.txt
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
//using Excel = Microsoft.Office.Interop.Excel;
//using Office = Microsoft.Office.Core;
using G=stateview.Globals;
using DStateData=stateview.Draw.DrawStateData;
using EFU=stateview._5300_EditForm.EditFormUtil;
using SS=stateview.StateStyle;
using DS=stateview.DesignSpec;
//>>>

namespace stateview._5300_EditForm
{
    public partial class EditForm_stateForm:Form
    {
        private List<string> m_all_states { get {  return G.state_working_list; } }
        public string m_text;
        public string m_comment;
        public string m_help;
        public bool   m_bRef = false;
        public string m_ref;
        public Form   m_parent;
        bool   m_editable_prefix;

        public EditForm_stateForm()
        {
            InitializeComponent();
        }
        private void update_label_statename()
        {
            label_statename.Text = textBox_prefix.Text + textBox_name_wo_prefix.Text;
        }
        private void set_text_statename(string state)
        {
            if (state.Length<2) state = "S_";
            textBox_prefix.Text = state.Substring(0,2);
            textBox_name_wo_prefix.Text = state.Substring(2);
            textBox_name_wo_prefix.Select(textBox_name_wo_prefix.Text.Length,0);

            update_label_statename();
        }
        private void update_editable_prefix(bool? b = null )
        {
            if (b!=null) m_editable_prefix = (bool)b;
            label_edit_prefix.Text = m_editable_prefix ? "Editing direct the prefix":"Not editing direct the prefix";

            textBox_prefix.ReadOnly = !m_editable_prefix;
        }
        private void EditForm_stateForm_Load(object sender,EventArgs e)
        {
            textBoxPageComment.Text = m_comment;
            textBoxPageHelp.Text = m_help;
            textBoxRef.Text = m_ref;

            groupBoxRef.Enabled = m_bRef;

            if (!FormUtil.SetCenterInForm(this,m_parent)) //this.Location = Cursor.Position;
            {
                this.Location = Cursor.Position;
            }
            //textBox_name_wo_prefix.Text = m_text.Substring(2);
            //textBox_name_wo_prefix.Select(textBox_name_wo_prefix.Text.Length,0);

            //textBox_prefix.Text = m_text.Substring(0,2);
            set_text_statename(m_text);

            update_editable_prefix(false);

            m_oneTimeAction = () => { 
                this.textBox_name_wo_prefix.Select(m_text.Length,0);
                this.textBox_name_wo_prefix.Focus();
            };

            //メニューに種別変更を追加したので、非表示へ
            this.button_prefix_chg.Visible = false;
            this.label_edit_prefix.Visible = G.option_use_custom_prefix;

            WordStorage.Res.ChangeAll(this,G.system_lang);

        }

        private void ok_button_Click(object sender,EventArgs e)
        {
            var newname =  label_statename.Text.Trim();// textBox_name_wo_prefix.Text.Trim();
            if (string.IsNullOrEmpty(newname))
            {
                G.NoticeToUser_warning("The input name is nothing.");
                return;
            }
            if (!StateUtil.IsValidStateName(newname))
            {
                G.NoticeToUser_warning("The input name is invalid. #1");
                return;
            }
            if (newname.Length >= 2 &&  newname.Substring(1,1) != "_")
            {
                G.NoticeToUser_warning("The input name is invalid. The prefix is invalid.");
                return;
            }
            if (Name.Length >= 4 && newname.Contains("____"))
            {
                G.NoticeToUser_warning("The input name is invalid. 4 under-bar characters are reserved.");
                return;
            }

            if (newname!=m_text && G.excel_program.GetStateList().Contains(newname)/* m_all_states.Contains(newname)*/)
            {
                G.NoticeToUser_warning("The input name has already existed. So another name was made.");
                var canditatename = StateUtil.MakeNewName(newname);
                //textBox_name_wo_prefix.Text = canditatename;
                set_text_statename(canditatename);
                return;
            }
            if (G.option_ignore_case_of_state) //大文字小文字を同一視
            {
                var newname_upper = newname.ToUpper();
                if (m_all_states.FindIndex(i=>i.ToUpper()==newname_upper)>=0)
                {
                    G.NoticeToUser_warning("With ignoring case condition, The input name has already existed. So another name was made.");
                    var canditatename = StateUtil.MakeNewName_w_ignore_case(newname);
                    //textBox_name_wo_prefix.Text = canditatename;
                    set_text_statename(canditatename);
                    return;
                }
            }

            m_text = newname;
            m_comment = textBoxPageComment.Text;
            m_ref = textBoxRef.Text;

            DialogResult = DialogResult.OK;
            Close();
        }

        private void cancel_button_Click(object sender,EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        Action m_oneTimeAction = null;
        private void timer1_Tick(object sender,EventArgs e)
        {
            if (m_oneTimeAction!=null)
            {
                m_oneTimeAction();
                m_oneTimeAction = null;
            }
        }

        private void button1_Click(object sender,EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxRef.Text))
            {
                var path = textBoxRef.Text;
                OpenLink.Open(path);
                //if (path.Contains(":") || path.Contains("\\") ||path.Contains("/")  )
                //{
                //    ExecUtil.execute_start(path,"");
                //    return;
                //}

                //var newpath = PathUtil.FindTraverse(G.load_file_dir,path);
                //if (string.IsNullOrEmpty(newpath)) {
                //    var ancesterpath = SettingIniUtil.GetAncestorDir();
                //    if (!string.IsNullOrEmpty(ancesterpath))
                //    {
                //        newpath = PathUtil.FindTraverse(ancesterpath,path);
                //    } 
                //}
                //if (!string.IsNullOrEmpty(newpath))
                //{
                //    ExecUtil.execute_start(newpath,"");
                //}
                //else
                //{
                //    G.NoticeToUser_warning("Cannot open " + path);
                //}
            }
        }

        private void label7_DoubleClick(object sender, EventArgs e)
        {
            textBoxPageComment.Text = string.Empty;
        }
        private void label7_Click(object sender, EventArgs e)
        {
            textBoxPageComment.Text = string.Empty;
        }

        private void label_State_DoubleClick(object sender, EventArgs e)
        {
            //無効化
            //textBox_prefix.Text = "S_";
            //update_label_statename();
            G.NoticeToUser("Obosleted to change category. You can change from state context menu.");
        }
        private void label_State_Click(object sender, EventArgs e)
        {
            //無効化
            //textBox_prefix.Text = "S_";
            //update_label_statename();
            G.NoticeToUser("Obosleted to change category. You can change from state context menu.");
        }

        private void label_Embed_DoubleClick(object sender, EventArgs e)
        {
            //無効化
            //textBox_prefix.Text = "E_";
            //update_label_statename();
            G.NoticeToUser("Obosleted to change category. You can change from state context menu.");
        }
        private void label5_Click(object sender, EventArgs e)
        {
            //textBox_prefix.Text = "E_";
            //update_label_statename();
        }

        private void label_Comment_DubleClick(object sender, EventArgs e)
        {
            //textBox_prefix.Text = "C_";
            //update_label_statename();
        }
        private void label6_Click(object sender, EventArgs e)
        {
            //textBox_prefix.Text = "C_";
            //update_label_statename();
        }

        //private void change_startchar(char c)
        //{
        //    var target = textBox_name_wo_prefix.Text;
        //    var head = RegexUtil.Get1stMatch("._",target);
        //    if (!string.IsNullOrEmpty(head)) {
        //        target = target.Replace(head, c.ToString() + "_");
        //        textBox_name_wo_prefix.Text = target;
        //    }
        //}

        private void label_numberd_Click(object sender, EventArgs e)
        {
            var s = label_statename.Text; //textBox_name_wo_prefix.Text;
            if (RegexUtil.IsMatch(@"^._\d+$",s))
            {
                return;
            }
            var head = RegexUtil.Get1stMatch(@"^._",s);
            //var newname = G.excel_program.Copy(head + "0001");
            //textBox_name_wo_prefix.Text = newname;
            var newname = head + "0001";
            var newname2 = StateUtil.MakeNewName(newname);

            set_text_statename(newname2);
        }

        private void label_help_3_Click(object sender, EventArgs e)
        {
            HelpJumpUtil.Jump("tec_userguide","edit-state-dlg",G.system_lang=="jpn");
        }

        private void textBox_name_wo_prefix_TextChanged(object sender, EventArgs e)
        {
            update_label_statename();
        }

        private void button_prefix_chg_Click(object sender, EventArgs e)
        {
            var text = textBox_prefix.Text;
            if (string.IsNullOrEmpty(text))
            {
                G.NoticeToUser_warning("{0C9483A0-F653-429B-8942-3B9229156B67}");
                text = "C_";
            }
            var c = text[0];
            if (c =='C') c = 'S';
            else if (c=='E') c = 'C';
            else if (c=='S') c = 'E';

            textBox_prefix.Text = c.ToString() + "_";

            update_label_statename();

        }

        private void label_edit_prefix_Click(object sender, EventArgs e)
        {
            update_editable_prefix(!m_editable_prefix);
        }

        private void textBox_prefix_TextChanged(object sender, EventArgs e)
        {
            update_label_statename();
        }

        private void label_statename_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(label_statename.Text);
            G.NoticeToUser("Copied the state name to clipboard : " + label_statename.Text );
        }
    }
}
