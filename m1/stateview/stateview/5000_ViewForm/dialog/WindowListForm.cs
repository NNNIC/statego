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
using stateview;
namespace stateview._5000_ViewForm.dialog
{
    public partial class WindowListForm : Form
    {
        private List<IntPtr> m_main_panel_winkey_list { get {  return G.view_form.m_main_panel_winkey_list; } }
        public WindowListForm()
        {
            InitializeComponent();
        }

        private void WindowListForm_Load(object sender, EventArgs e)
        {
            var related_loc = new Point(G.view_form.Width / 2 - this.Width / 2, G.view_form.Height / 2 - this.Height / 2);
            Location = PointUtil.Add_Point(G.view_form.Location, related_loc);

            var list = CheckOpenSameDoc.Get_WindowTitles2();
            var winitems = listBox1.Items;
            winitems.Clear();
            m_main_panel_winkey_list.Clear();
            foreach(var p in list)
            {
                var s = p.Value;
                var psgg = s;//RegexUtil.Get1stMatch(@"\[.+\]",s).Trim('[',']');

                if (p.Key == G.VIEWFORM_HANDLE)
                {
                    s = psgg + " (this window)";
                }
                else
                {
                    s = psgg;
                }
                winitems.Add(s);
                m_main_panel_winkey_list.Add(p.Key);
            }
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            var index = listBox1.SelectedIndex;
            var key = ListUtil.GetVal( m_main_panel_winkey_list, index);

            if (key!=null && key != G.VIEWFORM_HANDLE)
            {
                WindowsUtil.ActiveWindow(key);
                this.Close();
            }
            else
            {
                G.NoticeToUser_warning(G.Localize("w_selectewin_this")/*"The selected window is this one."*/);
                this.Close();
            }
        }
    }
}
