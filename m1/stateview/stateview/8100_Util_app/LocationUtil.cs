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
    public class LocationUtil
    {
        public static Dictionary<string, Dictionary<string, PointF> > CleanUp(Dictionary<string, Dictionary<string, PointF> > src, List<string> state_list)
        {
            var dst = new Dictionary<string, Dictionary<string, PointF> >();
            if (src != null) foreach(var p in src)
            {
                var tmp = new Dictionary<string, PointF>();
                foreach(var q in src[p.Key])
                {
                    if (state_list.Contains(q.Key))
                    {
                        tmp.Add(q.Key,q.Value);
                    }
                    else
                    {
                        if (AltState.IsAltState(p.Key)) //代替ステートは無条件に保存
                        {
                            tmp.Add(q.Key,q.Value);
                        }
                    }
                }
                if (tmp.Count!=0)
                {
                    dst.Add(p.Key, tmp);
                }
            }
            return dst;
        }
    }
}
