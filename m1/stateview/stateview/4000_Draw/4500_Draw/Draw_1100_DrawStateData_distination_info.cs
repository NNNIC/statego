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
    internal partial class Draw
    {
        internal partial class DrawStateData
        {
            #region 出力先情報

            internal string        nextstate;
            internal DrawStateData nextstate_data
            {
                get {
                    var nstate = G.node_normalize_state_for_alt(nextstate);
                    if (nstate!=null && G.m_draw_data_list!=null && G.m_draw_data_list.ContainsKey(nstate))
                    {
                        return G.m_draw_data_list[nstate];
                    }
                    return null;
                }
            }

            internal string        gosubstate;
            internal DrawStateData gsout_data
            {
                get {
                    var nstate = G.node_normalize_state_for_alt(gosubstate);
                    if (nstate!=null && G.m_draw_data_list!=null && G.m_draw_data_list.ContainsKey(nstate))
                    {
                        return G.m_draw_data_list[nstate];
                    }
                    return null;
                }
            }


            internal string[]        bout_value_list;       //元テキスト
            internal string[]        bout_state_list;       //
            internal DrawStateData[] bout_state_data_list
            {
                get {
                    if (bout_state_list==null || bout_state_list.Length == 0) return null;
                    var list = new DrawStateData[num_of_branches];
                    for(var i = 0; i<list.Length; i++)
                    {
                        var nstate = G.node_normalize_state_for_alt(bout_state_list[i]);
                        if (nstate!=null && G.m_draw_data_list!=null && G.m_draw_data_list.ContainsKey(nstate))
                        {
                            list[i] = G.m_draw_data_list[nstate];
                        }
                    }
                    return list;
                }
            }
            #endregion
        }
    }
}
