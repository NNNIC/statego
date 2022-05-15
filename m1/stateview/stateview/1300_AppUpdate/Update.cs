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
    static internal class AppUpdate
    {
        //internal static StateSequencer m_seq  = new StateSequencer();
        static DateTime       m_past = default(DateTime);
        internal static void main_update()
        {
            #region シーケンサー
            var now = DateTime.Now;
            var diff= (now - m_past).TotalMilliseconds;
            m_past = now;
            G.delta_time = diff * 0.001f;

            //m_seq.Update(G.delta_time);
            #endregion

            // ステータス表示 廃止
            //G.view_form.status_textBox.Text = G.status;

            //コルーチン
            Coroutine.Update();
            if (Coroutine.IsRunning()) return;


            //マウス外れ確認
            var pos = Cursor.Position;//G.view_form.PointToClient(Cursor.Position);
            if (!G.view_form.Bounds.Contains(pos))
            {
                G.mouse_event = MouseEventId.CANCEL;
                G.mouse_down_or_up = false;
            }

            //view form state control update
            if (G.vf_sc!=null) G.vf_sc.Update();

            //multiEditControl update
            if (G.multiedit_control!=null) G.multiedit_control.Update();

            //bmpos update
            if (G.point_on_bmp != null)
            {
                var x = G.point_on_bmp.X;
                var y = G.point_on_bmp.Y;
                //var d = G.mouse_down_or_up ? "d":"u";
                G.view_form.label_bmpos.Text = string.Format("({0},{1})", x, y);
            }
        }


        internal static void mouse_update(MouseEventId ev) //マウスイベントからコール
        {
            G.mouse_event = ev;
            if (ev == MouseEventId.DOUBLECLICK) G.mouse_double_clicked = true;

            if (ev == MouseEventId.MOUSEDOWN) {
                G.mouse_down_or_up = true;
                G.mouse_latest_button = Control.MouseButtons;
            }
            if (ev == MouseEventId.MOUSEUP)   G.mouse_down_or_up = false;

            main_update();

            G.mouse_event = MouseEventId.NONE;
            G.mouse_double_clicked = false;
        }
    }
}
