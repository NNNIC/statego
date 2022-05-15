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
    internal class EditForm_Control
    {
        //internal static void Load_from_celldata(string statename)
        //{
        //    Show_all_items(statename);
        //}

        internal static void Show_all_items(string org_statename, string tmp_statename, ref Dictionary<string,string> dgcache)
        {
            if (dgcache == null) dgcache =  new Dictionary<string, string>();

            var dgv = G.edit_form.dataGridView1;
            dgv.Rows.Clear();

            var col = G.get_state_col(org_statename);
            if (col < 0) return;

            dgv.Columns.Add("name", "");

            var max_row = G.excel_program.GetNameMaxUsingNameRowList();  //G.excel_program.GetNameRowList();

            for (var row = 1; row <= max_row + 2; row++)
            {
                var dgrow = row - 1;
                dgv.Rows.Add();
                dgv[0, dgrow].Value = row.ToString();

                var itemname = string.Empty;
                var namecell = G.get_excel_cell_cache(row, G.NAME_COL);
                if (namecell != null)
                {
                    itemname = namecell.text;
                    dgv[1, dgrow].Value = itemname;

                    var help = G.help_program2.GetHelp(itemname);
                    //if (!string.IsNullOrEmpty(help)) dgv[1,dgrow].ToolTipText = help;
                    dgv[2, dgrow].Value = help;

                    if (itemname == "thumbnail")
                    {
                        dgv[3, dgrow].Value = "(bitmap)";
                        continue;
                    }
                }

                var val = string.Empty;
                if (dgcache.ContainsKey(itemname))
                {
                    val = dgcache[itemname];
                }
                else
                {
                    var statecell = G.get_excel_cell_cache(row, col);
                    if (statecell != null)
                    {
                        val = statecell.text;
                    }
                }
                if (!string.IsNullOrEmpty(val))
                {
                    dgv[3, dgrow].Value = val;
                    if (val.Contains("\x0a"))
                    {
                        dgv[3, dgrow].ToolTipText = val;
                    }
                }
            }
        }

        [Obsolete]
        internal static void Show_optimized_items(string org_statename, string tmp_statename, ref Dictionary<string,string> dgcache, bool bMinmum)
        {
            if (dgcache == null) dgcache =  new Dictionary<string, string>();
            
            var dgv = G.edit_form.dataGridView1;
            dgv.Rows.Clear();

            var col = G.get_state_col(org_statename);
            if (col < 0) return;

            dgv.Columns.Add("name", "");
            for (var row = 1; row < G.m_excel_max_row; row++)
            {
                var namecell = G.get_excel_cell_cache(row, G.NAME_COL);
                if (namecell==null) continue;

                var itemname = namecell.text;
                if (string.IsNullOrEmpty(itemname)) continue;

                if (!G.itemsInfo_program.IsValid(tmp_statename,itemname))
                {
                    continue;
                }

                if (bMinmum)
                { 
                    if (itemname.Contains("-")) // -cmt等も見せない。
                    {
                        continue; 
                    }

                    if (                                     //ブランチも見せない
                        itemname == G.STATENAME_branch
                        ||
                        itemname == G.STATENAME_brcond
                        )
                    {
                        continue;
                    }
                }

                var help = G.help_program2.GetHelp(itemname);

                dgv.Rows.Add();
                var dgrow = dgv.Rows.Count-1;
                dgv[0, dgrow].Value = row.ToString();

                dgv[1, dgrow].Value = itemname;
                //if (!string.IsNullOrEmpty(help)) dgv[1, dgrow].ToolTipText = help;

                dgv[2, dgrow].Value = help;

                if (itemname == G.STATENAME_thumbnail)
                {
                    dgv[3, dgrow].Value = "(bitmap)";
                    continue;
                }

                var val = string.Empty;
                if (dgcache.ContainsKey(itemname))
                {
                    val = dgcache[itemname];
                }
                else
                {
                    var statecell = G.get_excel_cell_cache(row, col);
                    if (statecell != null)
                    {
                        val = statecell.text;
                    }
                }
                
                if (!string.IsNullOrEmpty(val))
                {
                    dgv[3, dgrow].Value = val;
                }

            }
        }


        internal static void Store_to_celldata(string statename)
        {

        }

        internal static void Call_click_cotrol(_5300_EditForm.EditForm parent, string state, string name, ref string text, bool bCmt, ref string cmt, bool bRef, ref string refx,ref Bitmap bmp, out bool bModifyBmp, out bool bNeedUpdtDgv)
        {
            var cc = new EditForm_ClickControl();

            cc.m_parentForm = parent;
            cc.m_state = state;
            cc.m_name = name;
            cc.m_text = text;

            cc.m_bCmt = bCmt;
            cc.m_cmt  = cmt;

            cc.m_bRef = bRef;
            cc.m_ref  = refx;

            cc.m_bmp  = bmp;

            cc.Start();
            for(var loop=0;loop<=1000;loop++)
            {
                cc.Update();
                if (cc.IsDone())
                {
                    break;
                }
            }


            name  = cc.m_name;
            text  = cc.m_text;
            cmt   = cc.m_cmt;
            refx  = cc.m_ref;

            bmp   = cc.m_bmp;
            bModifyBmp = cc.m_modifiedBmp;
            bNeedUpdtDgv = cc.m_needUpdateDgv;

        }

    }
}
