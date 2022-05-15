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
    public class LineColor
    {
        [Serializable]
        public class Item
        {
            public string pattern;
            public Color  color;
        }

        public List<Item> m_itemlist = new List<Item>();

        public void Reset()
        {
            m_itemlist.Clear();
            var item = SetItem(@"BACKTO_",ColorUtil.FromRRGGBB("6F6F6F"));
            m_itemlist.Add(item);
        }
        public Item SetItem( string pattern, Color col)
        {
            var item = new Item();
            item.pattern = pattern;
            item.color = col;
            return item;
        }

        public Color GetColor(string s, bool bFocus)
        {
            if (bFocus) return DS.ArrowColor_highlite;

            var col = DS.ArrowColor_normal;

            foreach(var i in m_itemlist)
            {
                if (RegexUtil.IsMatch(i.pattern,s))
                {
                    col = i.color;
                    break;
                }
            }
            return col;
        }

        public void Write_to_LineColorTab()
        {
            var dg = G.view_form.dataGridViewLineColor;

            dg.Rows.Clear();
            if (m_itemlist!=null) { 
                foreach(var i in m_itemlist)
                {
                    dg.Rows.Add(i.pattern, ColorUtil.ToRRGGBB(i.color));
                    //dg[0,r].Value = i.pattern;
                    //dg[r,1].Value = ColorUtil.ToRRGGBB(i.color);

                }
            }

            dg.Refresh();

            for(var r=0;r<dg.Rows.Count;r++)
            {
                try {
                    var cell = dg[1,r];
                    var col = ColorUtil.FromRRGGBB(cell.Value.ToString());
                    var cell2 = dg[2,r];
                    cell2.Style.BackColor = col;
                    cell2.Style.ForeColor = col;
                    cell2.Style.SelectionBackColor = col;
                    cell2.Style.SelectionForeColor = col;
                } catch { }
            }
        }

        public void Read_from_LineColorTab()
        {
            var dg = G.view_form.dataGridViewLineColor;

            if (m_itemlist == null)
            {
                m_itemlist = new List<Item>();
            }
            else
            {
                m_itemlist.Clear();
            }
            for(var r = 0; r<dg.Rows.Count; r++)
            {
                try {
                    var pattern =  dg[0,r].Value.ToString();
                    var color  = ColorUtil.FromRRGGBB( dg[1,r].Value.ToString() );
                    m_itemlist.Add( SetItem(pattern,color));
                }
                catch
                {
                    if (r < dg.Rows.Count-1)
                    {
                        G.NoticeToUser_warning("The data on line tab is invalid. Line:" + r);
                    }
                }
            }
        }

    }
}
