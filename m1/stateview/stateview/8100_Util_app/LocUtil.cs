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
    public class LocUtil
    {
        public static PointF? Get_lo_position_from_excel(string state)
        {
            return G.node_get_pos(state);
        }

        public static PointF? Get_lo_position(string state)
        {
            Dictionary<string, PointF> list = null;

#if x
            if ( FillterWork2.HasFillterValue(G.fillter_cur_id) )
            {
                if (G.fillter_state_location_list!=null && G.fillter_state_location_list.ContainsKey(G.fillter_cur_id))
                {
                    list = G.fillter_state_location_list[G.fillter_cur_id];
                }
            }
#else
            if (G.fillter_state_location_list!=null)
            {
                var id = G.node_get_cur_dirpath();
                if (G.fillter_state_location_list.ContainsKey(id))
                {
                    list = G.fillter_state_location_list[id];
                }
                if (list == null)
                {
                    if (id == "/") id = ""; //旧ロケーション対策
                    if (G.fillter_state_location_list.ContainsKey(id))
                    {
                        list = G.fillter_state_location_list[id];
                    }
                }
            }
#endif
            if (list == null)
            {
                list = G.state_location_list;
            }

            if (list!=null)
            {
                var lodic = list;
                if (lodic.ContainsKey(state))
                {
                    return lodic[state];
                }
            }
            return null;
        }

    }
}
