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
    /// <summary>
    /// アイテム定義用
    /// </summary>
    public class ItemDefUtil
    {
        public class Item
        {
            public string id;
            public string alt;
            public string comment;
        }
        public static Item[] m_list = null;
        public static Item[] GetList()
        {
            if (m_list!=null) return m_list;

            return null;

            //OBS
            //if (!File.Exists(G.itemdefFile)) return null;
            //var lines = File.ReadAllLines(G.itemdefFile,Encoding.GetEncoding("sjis"));

            //var list = new List<Item>();
            //foreach(var i in lines)
            //{
            //    var l = i.Trim();
            //    if (string.IsNullOrEmpty(l)) continue;
            //    if (l.StartsWith("/")) continue;
            //    var tokens = l.Split(',');
            //    if (tokens.Length < 3) continue;
            //    var item     = new Item();
            //    item.id      = tokens[0];
            //    item.alt     = tokens[1];
            //    item.comment = tokens[2];

            //    list.Add(item);
            //}

            //m_list = list.ToArray();

            //return m_list;
        }
    }
}
