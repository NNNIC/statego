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

namespace stateview
{
    public class SysLangWork
    {
        public static string m_syslang { get { return G.system_lang; } }
        public static List<Action> m_action_list = new List<Action>();

        public static void Setup()
        {
            var form = G.view_form;

            form.button1.Tag = get_id(form.button1.Text);
            form.buttonOpenConvertSrc.Tag = get_id(form.buttonOpenConvertSrc.Text);
            form.buttonOpenImpleSource.Tag = get_id(form.buttonOpenImpleSource.Text);
            form.buttonOpenExcelFolder.Tag = get_id(form.buttonOpenExcelFolder.Text);
            form.buttonOpenSourceFolder.Tag = get_id(form.buttonOpenSourceFolder.Text);
            form.buttonOpenMacro.Tag = get_id(form.buttonOpenMacro.Text);
            form.buttonUserButton.Tag = get_id(form.buttonUserButton.Text);

            m_action_list.Add(()=> { form.button1.Text = get_string(form.button1.Tag);   });
            m_action_list.Add(()=> { form.buttonOpenConvertSrc.Text = get_string(form.buttonOpenConvertSrc.Tag);   });
            m_action_list.Add(()=> { form.buttonOpenImpleSource.Text = get_string(form.buttonOpenImpleSource.Tag);   });
            m_action_list.Add(()=> { form.buttonOpenExcelFolder.Text = get_string(form.buttonOpenExcelFolder.Tag);   });
            m_action_list.Add(()=> { form.buttonOpenSourceFolder.Text = get_string(form.buttonOpenSourceFolder.Tag);   });
            m_action_list.Add(()=> { form.buttonOpenMacro.Text = get_string(form.buttonOpenMacro.Tag); });
            m_action_list.Add(()=> { if (form.buttonUserButton.Tag!=null) form.buttonUserButton.Text = get_string(form.buttonUserButton.Tag); });
        }

        public static void ChangeSysLang()
        {
            m_action_list.ForEach(i=>i());
            G.view_form.change_flowColorLang();
        }

        private static string get_id(string t)
        {
            var match = RegexUtil.Get1stMatch(@"^\[.+\]",t);
            if (string.IsNullOrEmpty(match)) return string.Empty;
            return match.Trim('[',']');
        }
        private static string get_string(object id)
        {
            return WordStorage.Res.Get(id.ToString(),m_syslang);
        }
    }
}
