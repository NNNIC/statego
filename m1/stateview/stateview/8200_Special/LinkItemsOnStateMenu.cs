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
using G = stateview.Globals;
using DStateData = stateview.Draw.DrawStateData;
using EFU = stateview._5300_EditForm.EditFormUtil;
using SS = stateview.StateStyle;
using DS = stateview.DesignSpec;
//>>>

    /*
        ステートをクリックした際に表示するステートメニューの
        リンク部分の処理
    */

namespace stateview
{
    public class LinkItemsOnStateMenu
    {
        List<string> m_linkList = new List<string>();
        
        public void Refresh(string state)
        {
            m_linkList.Clear();
            foreach(var n in  G.name_list)
            {
                if (!n.EndsWith("-ref")) continue;
                var s = G.excel_program.GetStringIfSubname(state,n);
                if (string.IsNullOrEmpty(s)) continue;

                m_linkList.Add(s);
            }
        }

        public List<string> GetList() {
            return m_linkList;
        }

        ToolStripMenuItem m_ts_link;
        public void ModifyMenuItemInViewForm()
        {
            var form = G.view_form;
            var menuitem = form.MenuItemLink;

            ((ToolStripDropDownMenu)menuitem.DropDown).ShowCheckMargin = false;
            ((ToolStripDropDownMenu)menuitem.DropDown).ShowImageMargin = false;

            var items = menuitem.DropDownItems;
            items.Clear();

            if (m_linkList!=null)
            {
                foreach(var s in m_linkList)
                {
                    var ts = new ToolStripMenuItem();
                    ts.Text = s;
                    ts.Click += Ts_Click;
                    items.Add(ts);
                }
            }
            m_ts_link = menuitem;
        }
        public string ClickedLink;
        private void Ts_Click(object sender, EventArgs e)
        {
            try {
                var tm = sender as ToolStripMenuItem;
                ClickedLink = tm.Text;
                ViewFormStateControl.m_viewFormStateMenuItem = ViewFormStateMenuItem.LINK;
            } catch (SystemException e2)
            {
                G.NoticeToUser_warning("Unexpected! {C74F8BB2-9313-4C54-823B-5126C686BBE6}" + e2.Message);
            }
        }

        public Rectangle? GetBounds()
        {
            if (m_ts_link==null) return null;
            if (!m_ts_link.DropDown.Visible) return null;
            return m_ts_link.DropDown.Bounds;
        }


    }
}
