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

namespace stateview._5000_ViewForm.dialog
{
    public partial class SetSourceEditor_langPfChangeForm2 : Form
    {
        public SetSourceEditor_langPfChangeForm2()
        {
            InitializeComponent();
        }

        private void button_OK_Click(object sender, EventArgs e)
        {
            if (check_langname() && check_platfromname())
            {
                if (SettingIniUtil.GetLang() != textBox_lang.Text)
                {
                    SettingIniUtil.SetLangForce(textBox_lang.Text);
                }

                if (SettingIniUtil.GetFramework() != textBox_framework.Text)
                {
                    SettingIniUtil.SetFrameworkForce(textBox_framework.Text);
                }
                
                if (G.psgg_file_w_data)
                {
                    FileDbUtil.WriteSettings(); //新PSGGファイル
                }
                else
                { 
                    ExcelSaveOneSheet.WriteSettings(); //従来式
                }

                DialogResult = DialogResult.OK;

                Close();
            }
        }

        private void button_CANCEL_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool check_langname()
        {
            var s = textBox_lang.Text;
            if (string.IsNullOrEmpty(s))
            {
                return false;
            }
            s = s.Trim();
            textBox_lang.Text = s;
           　
            return true;
        }
        private bool check_platfromname()
        {
            var s = textBox_framework.Text;
            if (string.IsNullOrEmpty(s))
            {
                textBox_framework.Text = string.Empty;
                return true;
            }
            s = s.Trim();
            textBox_framework.Text = s;

            return true;
        }

        private void SetSourceEditor_langPfChangeForm2_Load(object sender, EventArgs e)
        {
            WordStorage.Res.ChangeAll(this,G.system_lang);

            DialogResult = DialogResult.None;
            textBox_lang.Text = SettingIniUtil.GetLang();
            textBox_framework.Text = SettingIniUtil.GetFramework();

            var alllangfw = RegistryWork.Get_srcedit_all_langfw();

            if (alllangfw==null || alllangfw.Length == 0)
            {
                listBox1.Enabled = false;
            }
            if (alllangfw!=null) foreach(var s in alllangfw)
            {
                listBox1.Items.Add(s);
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            var index = listBox1.SelectedIndex;
            if (index >=0 && index < listBox1.Items.Count)
            {
                var val = listBox1.Items[index].ToString();
                var lang = RegexUtil.Get1stMatch(@"[^(]+",val);
                var fw = RegexUtil.Get1stMatch(@"\(.+?\)",val);
                if (!string.IsNullOrEmpty(lang))
                {
                    textBox_lang.Text      =lang;
                    textBox_framework.Text = string.Empty;
                    if (!string.IsNullOrEmpty(fw))
                    {
                        fw = fw.Trim('(',')');
                        fw = fw.Trim();
                        textBox_framework.Text = fw;
                    }
                }
            }
        }
    }
}
