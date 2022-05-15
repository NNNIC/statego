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
    class CopyRenameStateSave
    {
        static string latest_src_copyed_state; //今しがたコピーされたステート元
        static string latest_dst_copyed_state; //今しがたコピーされたステート先

        static PointF new_dst_state_position;  //コピー先の位置

        //リネームとコピー後すぐに呼び出して記録
        public static PointF? calc_new_dst_position( string orgname, string newname, bool copy_or_rename)
        {
            latest_src_copyed_state = latest_dst_copyed_state = null;

            var dsf = G.get_draw_wp_outframe(orgname);
            if (dsf == null)
            {
                return null;
            }

            latest_src_copyed_state = orgname;
            latest_dst_copyed_state = newname;

            if (copy_or_rename)
            {
                new_dst_state_position 　= PointUtil.Add_XY(((RectangleF)dsf).Location,100,100);
            }
            else
            {
                new_dst_state_position 　= ((RectangleF)dsf).Location;
            }

            return new_dst_state_position;
        }
        //新規用
        public static void set_new_state_psition(PointF pos, string newstate)
        {
            latest_dst_copyed_state = newstate;
            new_dst_state_position = pos;
        }

        //新ロケーションを得る時に返す
        public static PointF? get_new_position(string state,bool clearDataIfMatched)
        {
            //指定した stateと latest_dst_copyed_stateが一致した時に値を返す
            if (state!=null && state == latest_dst_copyed_state)
            {
                var ret = new_dst_state_position;
                if (clearDataIfMatched)
                {
                    latest_src_copyed_state = latest_dst_copyed_state = null;
                }
                return ret;
            }
            return null;
        }
    }
}
