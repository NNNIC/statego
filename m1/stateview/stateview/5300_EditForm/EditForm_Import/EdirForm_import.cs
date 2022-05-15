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

namespace stateview._5300_EditForm //     ※注意 -- EditFrom_buttonFormではない！　rowデータを扱うEditFormであることに注意！
{
    public partial class EditForm
    {
		private readonly string m_extention = ".dgtplt";
		private          string m_fillter   { get { return string.Format("Dashstu Game Template(*{0})|*{0}",m_extention); }}
        private void ImportButton_Click(object sender,EventArgs e)
        {
            //if (!string.IsNullOrEmpty(G.appDir))
            //{
            //    openFileDialog1.InitialDirectory = Path.Combine(Environment.CurrentDirectory, G.appDir);
            //}
            openFileDialog1.Filter = m_fillter;
            var rc = openFileDialog1.ShowDialog();
            if (rc!= DialogResult.OK) return;
            var file = openFileDialog1.FileName;
            try {

                var s = File.ReadAllText(file,Encoding.UTF8);

                var hti = IniUtil.CreateHashtable(s,"Info");

                var groupscript = string.Empty;
                if (string.IsNullOrEmpty(groupscript))
                {
                    System.Diagnostics.Debug.WriteLine("!!! group == null ");
                    return;
                }

                var hts = IniUtil.CreateHashtable(s,groupscript);
                if (hts!=null)
                {
                    _clear();

                    var type = IniUtil.GetValueFromHashtable("type",hti);
                    if (type==null)
                    {
                        System.Diagnostics.Debug.WriteLine("!!! type == null ");
                        return;
                    }

                    _write("!type",type);

                    foreach(var k in hts.Keys)
                    {
                        var key = (string)k;
                        var val = (string)hts[k];
                        //if (key == "layer_X")
                        //{
                        //    if (G.jxtype == G.JXTYPE.ROOM)
                        //    {
                        //        _write("layer_0",val);
                        //    }
                        //    else if (G.jxtype == G.JXTYPE.ABOUT)
                        //    {
                        //        _write("layer_1",val);
                        //    }
                        //}
                        //else if (key == "branch")
                        //{
                        //    var val2 = val;
                        //    if (G.jxtype == G.JXTYPE.ROOM)
                        //    {
                        //        val2 = val2.Replace("XFG","RFG");
                        //    }
                        //    else if (G.jxtype == G.JXTYPE.ABOUT)
                        //    {
                        //        val2 = val2.Replace("XFG","AFG");
                        //    }
                        //    _write(key,val2);
                        //}
                        //else
                        //{
                        //    _write(key,val);
                        //}
                        _write(key,val);
                    }
                }
            } catch { }
        }
        private void _clear()
        {
            m_modified = true;
            var dg = this.dataGridView1;
            for(var r = 0; r< dg.Rows.Count; r++)
            {
                var key = string.Empty;
                try { key = dg[1,r].Value.ToString(); } catch { }
                if (key == G.STATENAME_state || key == G.STATENAME_statecmt)
                {
                    continue;
                }
                if (key.StartsWith("!") && key!="!type")
                {
                    continue;
                }
                if (!string.IsNullOrEmpty(key))
                {
                    var val = string.Empty;
                    try { val = dg[2,r].Value.ToString(); } catch { }
                    if (!string.IsNullOrEmpty(val))
                    {
                        dg[2,r].Value = string.Empty;
                    }
                }
            }
        }
        private void _write(string key, string val)
        {
            m_modified = true;
            //this.dataGridView1[2,e.RowIndex].Value = text;
            var dg = this.dataGridView1;
            var findrow = -1;
            for(var r = 0; r< dg.Rows.Count; r++)
            {
                if (dg[1,r].Value.ToString() == key)
                {
                    findrow = r;
                    break;
                }
            }
            if (findrow==-1)
            {
                System.Diagnostics.Debug.WriteLine("!!! Cannot find : " + key);
                return;
            }
            val = StringUtil.ConvertNewLineForExcel(val);
            dg[2,findrow].Value = val;
        }
    }
}
