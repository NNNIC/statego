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
    public partial class EditForm_textForm:Form
    {
        public Form m_parent;

        public string m_text;
        public string m_comment;
        public string m_help;
        public string m_ref;
        public bool   m_bEnableCommentTab { get { return m_bEnableComment || m_bEnableRef;  } }
        public bool   m_bEnableComment;
        public bool   m_bEnableRef;

        public string m_method;

        public string m_state;
        public string m_item;

        methodproc m_mp;

        public EditForm_textForm()
        {
            InitializeComponent();
            this.DialogResult = DialogResult.None;
        }

        private void ok_button_Click(object sender,EventArgs e)
        {
            if (!m_mp.is_oktext(this.scintillaBox.Text))
            {
                if (MessageBox.Show("the text is not mutch to Regex string.\nForce to save?", "CAUTION", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
            }

            ok_process();
        }

        private void ok_process()
        {
            this.DialogResult = DialogResult.OK;
            m_text = this.scintillaBox.Text;
            m_comment = this.textBoxPageComment.Text;
            m_ref = this.textBoxRef.Text;
            this.Close();
        }

        private void cancel_button_Click(object sender,EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void EditForm_textForm_SaveAndClose()
        {
            ok_process();
        }

        private void EditForm_textForm_Load(object sender,EventArgs e)
        {
            var zoom = RegistryWork.Get_texteditor_zoom();
            this.scintillaBox.Init(G.view_form,zoom, false);
            this.scintillaBox.TextArea.Click += ScintillaBox_Click;
            this.scintillaBox.SaveAction = EditForm_textForm_SaveAndClose;

            this.textBoxPageHelp.Text    = m_help;
            this.textBoxPageComment.Text = m_comment;
            this.textBoxRef.Text         = m_ref;
            this.textBox_method.Text     = m_method;
            

            m_mp = new methodproc();
            m_mp.init(m_method);
            if (m_mp.is_textfield_readonly())
            {
                this.scintillaBox.ReadOnly = true;
            }

            if (m_mp.is_shulink_width())
            {
                Width = 330;
                label_stateexpand.Visible = false;
            }

            this.button_next.Visible   = m_mp.is_valid_select();
            this.button_prev.Visible   = m_mp.is_valid_select();
            this.button_select.Visible = m_mp.is_select();

            if (m_bEnableComment)
            {
                this.textBoxPageComment.Enabled = true;
            }
            if (m_bEnableRef)
            {
                this.groupBoxRef.Enabled = true;
            }

            if (!m_bEnableCommentTab)
            {
                this.textBoxPageComment.Hide();
                this.tabControl.SelectedTab = tabPageHelp;
            }

            if (!FormUtil.SetCenterInForm(this,m_parent))
            {
                this.Location = Cursor.Position;
            }

            this.scintillaBox.Text =  StringUtil.TrimLines(m_text);

            // Use External EditorのON/OFF
            var bUse_ext_editor = false;
            if (
                !m_item.Contains("-") // アイテム名に '-'がない。 
                &&
                G.excel_convertsettings.m_template_func.Contains("[[" +  m_item + "]]") //テンプレート関数に [[item]]がある
                &&
                string.IsNullOrEmpty( G.macro_ini.GetValue("@" + m_item) ) //ラインマクロなし
                )
            {
                bUse_ext_editor=true;
            }

            this.label_use_ext_editor.Visible = bUse_ext_editor;

            WordStorage.Res.ChangeAll(this,G.system_lang);

            // Focusを確実に実行
            m_oneTimeAction = () => { 
                //this.textBox1.Select(m_text.Length,0);
                //this.textBox1.Focus();

                this.scintillaBox.Focus();
            };

            //サイズの反映
            if (G.itemeditform_size_list!=null && G.itemeditform_size_list.ContainsKey(m_item))
            {
                var size = G.itemeditform_size_list[m_item];
                this.Size = size;
            }
        }

        private void ScintillaBox_Click(object sender, EventArgs e)
        {
            textBox1_Click(sender, e);
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
            if (!string.IsNullOrEmpty(this.textBoxRef.Text))
            {
                //ExecUtil.execute_start(this.textBoxRef.Text,"");
                OpenLink.Open(this.textBoxRef.Text);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            scintillaBox.Text = string.Empty;
        }

        private void button_next_Click(object sender, EventArgs e)
        {
            scintillaBox.Text = m_mp.get_next(scintillaBox.Text);
        }
        private void button_prev_Click(object sender, EventArgs e)
        {
            scintillaBox.Text = m_mp.get_prev(scintillaBox.Text);
        }

        #region for method proc 
        public class methodproc
        {
            public enum mode {
                none,
                free,
                fix,
                select,
                regex,
                regex_by_line
            }

            mode m_mode;
            public List<string> m_params;
            string m_regex;

            public void init(string s)
            {
                m_mode = mode.none;
                m_params = null;

                if (string.IsNullOrEmpty(s) )
                {
                    return;
                }
                var newchar = StringUtil.FindNewLineChar(s);
                if (newchar == null || newchar.Length==0) {
                    G.NoticeToUser_warning("input method is invalid. #1");
                    return;
                }
                var lines = StringUtil.SplitTrim(s,newchar[0]);
                if (lines == null || lines.Length<2)
                {
                    G.NoticeToUser_warning("input method is invalid. #2");
                    return;
                }

                lines = StringUtil.TrimLines(lines);
                if (lines == null || lines.Length<2)
                {
                    G.NoticeToUser_warning("input method is invalid. #2a");
                    return;
                }

                var line0 = lines[0];
                if (string.IsNullOrEmpty(line0) || line0.Length ==0)
                {
                    G.NoticeToUser_warning("input method is invalid. #3");
                    return;
                }
                if (line0[0]!='*')
                {
                    G.NoticeToUser_warning("input method is invalid. #4");
                    return;
                }
                m_mode = EnumUtil.Parse(line0.Substring(1),mode.none);
                if (m_mode== mode.none)
                {
                    G.NoticeToUser_warning("input method is invalid. #5");
                    return;
                }
                if (m_mode== mode.fix)
                {
                    if (lines.Length<2)
                    {
                        G.NoticeToUser_warning("input method is invalid. #6");
                        return;
                    }
                    m_params = new List<string>();
                    m_params.Add(lines[1]);
                    m_params.Add(string.Empty); // ボタン押下時に空白も表示
                    return; 
                }
                if (m_mode==mode.select)
                {
                    if (lines.Length<2)
                    {
                        G.NoticeToUser_warning("input method is invalid. #7");
                        return;
                    }
                    m_params = new List<string>();
                    for(var n = 1; n < lines.Length; n++)
                    {
                        m_params.Add(lines[n]);
                    }
                    m_params.Add(string.Empty);// ボタン押下時に空白も表示
                    return;
                }
                if (m_mode == mode.regex || m_mode == mode.regex_by_line)
                {
                    if (lines.Length<2)
                    {
                        G.NoticeToUser_warning("input method is invalid. #8");
                        return;
                    }
                    m_regex = lines[1];
                    return;
                }
            }

            public bool is_textfield_readonly()
            {
                return (m_mode == mode.fix || m_mode == mode.select);
            }

            public bool is_valid_select()
            {
                return (m_mode == mode.fix || m_mode == mode.select);
            }
            public bool is_select()
            {
                return m_mode == mode.select;
            }
            public bool is_shulink_width()
            {
                return (m_mode == mode.fix || m_mode == mode.select);
            }

            public string get_next(string s)
            {
                if (m_mode== mode.fix || m_mode == mode.select)
                {
                    if (string.IsNullOrEmpty(s)) s= string.Empty;
                    return ListUtil.GetNextValue(m_params,s);
                }
                G.NoticeToUser_warning("{95B4EF2B-0C28-4512-9031-2B89E30F8496}");
                return string.Empty;
            }

            public string get_prev(string s)
            {
                if (m_mode== mode.fix || m_mode == mode.select)
                {
                    if (string.IsNullOrEmpty(s)) s= string.Empty;
                    return ListUtil.GetPrevValue(m_params,s);
                }
                G.NoticeToUser_warning("{95B4EF2B-0C28-4512-9031-2B89E30F8496}");
                return string.Empty;
            }

            public bool is_oktext(string s)
            {
                if (string.IsNullOrEmpty(s)) return true;
                if (m_mode == mode.regex)
                {
                    if (string.IsNullOrEmpty(m_regex))
                    {
                        G.NoticeToUser_warning("{6894D1D1-C354-426D-8E77-EE4735934934}");
                        return true;
                    }
                    s = s.Trim();
                    
                    return RegexUtil.Get1stMatch(m_regex,s)==s;
                }
                else if (m_mode == mode.regex_by_line)
                {
                    if (string.IsNullOrEmpty(m_regex))
                    {
                        G.NoticeToUser_warning("{87768281-4D2B-40A9-9549-5DC329707FDE}");
                        return true;
                    }
                    var newchar = StringUtil.FindNewLineChar(s);
                    if (newchar==null || newchar.Length == 0)
                    {
                        s = s.Trim();
                        return RegexUtil.Get1stMatch(m_regex,s) == s;
                    }
                    var lines = StringUtil.SplitTrim(s,newchar[0]);
                    lines = StringUtil.TrimLines(lines);
                    foreach(var l in lines)
                    {
                        if (RegexUtil.Get1stMatch(m_regex,l) != l)
                        {
                            return false;
                        }
                    }
                    return true;
                }
                else
                {
                    return true;
                }
            }

        }

        #endregion

        private void label3_Click(object sender, EventArgs e)
        {
            scintillaBox.Text = string.Empty;
        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            if (m_mp!=null && m_mp.is_textfield_readonly())
            {
                scintillaBox.Text = m_mp.get_next(scintillaBox.Text);
            }
        }

        private void label_help_3_Click(object sender, EventArgs e)
        {
            HelpJumpUtil.Jump("tec_userguide","edit-item-text",G.system_lang=="jpn");
        }

        private void label_use_external_editor_Click(object sender, EventArgs e)
        {
            var sm = new UseExtEditorControl();
            sm.m_state = m_state;
            sm.m_item  = m_item;
            sm.m_val = scintillaBox.Text;//textBox1.Text;
            sm.m_parent = this;
            sm.Run();

            if (sm.m_bOk)
            {
                scintillaBox.Text = sm.m_output;
                //ok_process();
            }
        }

        private void EditForm_textForm_Resize(object sender, EventArgs e)
        {
            if (G.itemeditform_size_list==null) G.itemeditform_size_list = new Dictionary<string, Size>();
            DictionaryUtil.Set(G.itemeditform_size_list,m_item,this.Size);
        }

        private void button_select_Click(object sender, EventArgs e)
        {
            var form = new EditForm_textForm_selectForm();
            form.textBox1.Text = scintillaBox.Text;// textBox1.Text;
            form.m_mp = m_mp;
            form.m_startlocation = this.Location;
            if (form.ShowDialog() == DialogResult.OK)
            {
                scintillaBox.Text = form.textBox1.Text;
            }
        }

        bool bRevertOrExpand;
        private void label_stateexpand_Click(object sender, EventArgs e)
        {
            var lowerCamel = StringUtil.convert_to_camel_word(m_state,false);
            var upperCamel = StringUtil.convert_to_camel_word(m_state,true);

            if (bRevertOrExpand)
            {
                bRevertOrExpand = false;

                var text = scintillaBox.Text.Replace(m_state,"[[state]]");
                
                var text2 = text.Replace(lowerCamel, "[[state>>lc]]");
                var text3 = text2.Replace(upperCamel,"[[state>>uc]]");

                scintillaBox.Text = text3;
            }
            else
            {
                bRevertOrExpand = true;

                var text  = scintillaBox.Text.Replace("[[state]]",m_state);
                var text2 = text.Replace("[[state>>lc]]", lowerCamel);
                var text3 = text2.Replace("[[state>>uc]]",upperCamel); 

                scintillaBox.Text = text3;
            }
        }

        private void EditForm_textForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void EditForm_textForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            RegistryWork.Set_texteditor_zoom(scintillaBox.GetZoom());

            scintillaBox.UnInit();
        }
    }
}
