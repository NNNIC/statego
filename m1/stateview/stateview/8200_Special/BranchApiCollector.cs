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
    public class BranchApiCollector
    {
        string MATCH_BR { get { return BranchUtil.MATCH_BR; } }//= @"br_[a-zA-Z0-9_]+";

        //List<string> m_apiList = new List<string>();
        Dictionary<string,int> m_apiDic = new Dictionary<string, int>(); //APIの使用頻度
        List<string>           m_apiList;

        public void Refresh()
        {
            m_apiDic.Clear();
            collect_from_gen_src();
            collect_from_sub_src();
            collect_from_macro_ini();
            collect_from_templatesrc();
            collect_from_excel();
            sort();

#if dbg
            var s = string.Empty;
            m_apiList.ForEach(i=>s+= i + Environment.NewLine);

            G.NoticeToUser("Branch API List = " + s);
#endif
        }
        public List<string> GetList() {
            return m_apiList;
        }
        void collect_from_gen_src()
        {
            try
            {
                var text = Converter.GetGeneratedSource();
                update_list(text);
            }
            catch (SystemException e)
            {
                G.NoticeToUser_warning("Unexpected! {0055E59F-22AE-45C3-97C1-3C6BBB4A607C}, " + e.Message);
            }
        }
        void collect_from_sub_src()
        {
            var subsrc = SettingIniUtil.GetSourceForImplementing();
            if (string.IsNullOrEmpty(subsrc) || !File.Exists(subsrc))
            {
                return;
            }
            try
            {
                var text = File.ReadAllText(subsrc, Encoding.UTF8);
                update_list(text);
            }
            catch (SystemException e)
            {
                G.NoticeToUser_warning("Unexpected! {BA93CC39-F95D-4530-AFB9-479B5ABA672A}, " + e.Message);
            }
        }
        void collect_from_macro_ini()
        {
            var macroini = SettingIniUtil.GetMacroIni();
            if (string.IsNullOrEmpty(macroini) || !File.Exists(macroini))
            {
                return;
            }
            try
            {
                var text = File.ReadAllText(macroini, Encoding.UTF8);
                update_list(text);
            } catch ( SystemException e)
            {
                G.NoticeToUser_warning("Unexpected! {9E3B6439-B573-4C01-8D4D-DCC4332AB585} " + e.Message);
            }
        }
        void collect_from_templatesrc()
        {
            var templatesource = G.excel_convertsettings.m_template_src;
            update_list(templatesource);
        }
        void collect_from_excel()
        {
            if (G.state_list==null)
            {
                G.NoticeToUser_warning("Unexpected! {{639BB172-9EFA-4C98-B8B4-EC25898A7D1D}} ");
                return;
            }
            foreach(var s in G.state_list)
            {
                var brstr = G.excel_program.GetString(s,G.STATENAME_branch);
                update_list(brstr);
            }
        }
        private void update_list(string text)
        {
            if (string.IsNullOrEmpty(text)) return;
            var matches1 = RegexUtil.GetAllMatches(@"\b" + MATCH_BR +@"\(",text);   // br_xxxx( の一致
            var matches2 = RegexUtil.GetAllMatches(@"\b" + MATCH_BR +@"\s*=",text); // br_xxxx=の一致
            
            Action<string[]> _ud = (l) => {
                if (l==null) return;
                foreach(var s in l)
                {
                    var s2 = RegexUtil.Get1stMatch(MATCH_BR,s);
                    if (!string.IsNullOrEmpty(s2))
                    {
                        if (s2.StartsWith("br_dummy_")) continue; //ユーザ使用禁止

                        if (m_apiDic.ContainsKey(s2))
                        {
                            m_apiDic[s2] += 1; //Count up
                        }
                        else
                        {
                            m_apiDic.Add(s2,1);
                        }
                    }
                }
            };

            _ud(matches1);
            _ud(matches2);
        }
        private void sort()
        {
            var dic = new Dictionary<string,int>(m_apiDic);
            var list = new List<string>();
            for(var n = 100; n >= 0; n--)
            {
                for(var loop = 0; loop <= 1000; loop++ )
                {
                    if (loop == 1000) throw new SystemException("Unexpected! {CD41DAA1-94EF-404E-B5B2-AED3B123BEBF}");
                    if (dic.ContainsValue(n))
                    {
                        string find = null;
                        foreach(var p in dic)
                        {
                            if (p.Value >= n) {
                                find = p.Key;
                                break;
                            }
                        }
                        if (!string.IsNullOrEmpty(find))
                        { 
                            list.Add(find);
                            dic.Remove(find);
                            continue;
                        }
                    }
                    break;
                }
            }
            m_apiList = list;
        }

        #region        // view_form用
        ToolStripMenuItem m_ts_editbranch;
        public void ModifiyMenuItemInViewForm()
        {
#if obs
            var form = G.view_form;
            var menuitem = form.editBranchToolStripMenuItem;

            ((ToolStripDropDownMenu)menuitem.DropDown).ShowCheckMargin = false;
            ((ToolStripDropDownMenu)menuitem.DropDown).ShowImageMargin = false;


            var items = menuitem.DropDownItems;
            items.Clear();

            // Edit
            var ts_edit = new ToolStripMenuItem();
            ts_edit.Text = "Edit";
            ts_edit.Click += Ts_edit_Click;
            items.Add(ts_edit);

            //branches
            int n = 0;
            foreach(var s in m_apiList)
            {
                var ts = new ToolStripMenuItem();
                ts.Text = "> " + s;
                ts.Click += Ts_add_Click;
                items.Add(ts);

                n++;
                if (n==15) break; // Max まで
            }

            m_ts_editbranch = menuitem;         
#endif
        }

        public Rectangle? GetBounds()
        {
            if (m_ts_editbranch == null) return null;
            if (!m_ts_editbranch.DropDown.Visible) return null;
            return m_ts_editbranch.DropDown.Bounds;
        }

        // Click時の情報
        public string EditClickInfo;
        private void Ts_edit_Click(object sender,EventArgs e)
        {
            EditClickInfo = null; 
            ViewFormStateControl.m_viewFormStateMenuItem = ViewFormStateMenuItem.EDITBRANCH;
        }
        private void Ts_add_Click(object sender,EventArgs e)
        {
            try { 
                var tm = sender as ToolStripMenuItem;
                EditClickInfo = RegexUtil.Get1stMatch(MATCH_BR,tm.Text); 
                ViewFormStateControl.m_viewFormStateMenuItem = ViewFormStateMenuItem.ADDBRANCH;
            } catch (SystemException e2)
            {
                G.NoticeToUser_warning("Unexpected! {8BBDDA1E-6F4A-4B78-AD4C-E95BAE55B948} " + e2.Message);
            }
        }
#endregion

#region branch editor 用
        private Action<string> _ModifyBranchEditorForm_MenuItem_cb;
        public void ModifyBranchEditorForm_MenuItem(ToolStripMenuItem mi,  Action<string> cb)
        {
            _ModifyBranchEditorForm_MenuItem_cb = cb;
            //mi.DropDown .ShowCheckMargin = false;
            var items = mi.DropDownItems;
            items.Clear();

            //branches
            int n = 0;
            foreach(var s in m_apiList)
            {
                var ts = new ToolStripMenuItem();
                
                ts.Text = "> " + s;
                ts.Click += _ModifyBranchEditorForm_MenuItem_onClick;
                items.Add(ts);

                n++;
                if (n==100) break; // Max まで
            }
            ((ToolStripDropDownMenu)mi.DropDown).ShowCheckMargin = false;
            ((ToolStripDropDownMenu)mi.DropDown).ShowImageMargin = false;
        }

        private void _ModifyBranchEditorForm_MenuItem_onClick(object sender,EventArgs e)
        {
            try { 
                var tm = sender as ToolStripMenuItem;
                var s = RegexUtil.Get1stMatch(MATCH_BR,tm.Text); 
                if (!string.IsNullOrEmpty(s)) _ModifyBranchEditorForm_MenuItem_cb(s);
            } catch (SystemException e2)
            {
                G.NoticeToUser_warning("Unexpected! {D9227BF9-2347-4C06-8AA5-362986EB2F4E}" + e2.Message);
            }                
        }
#endregion

    }
}
