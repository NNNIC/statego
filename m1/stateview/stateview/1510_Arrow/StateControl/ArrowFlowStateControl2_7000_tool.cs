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

public partial class ArrowFlowStateControl2
{
    bool IsHit_statebox_obs(PointF a, PointF b,bool exSelf=false)
    {
        var rect = _create_rect(a,b);

        foreach(var p in m_stateData)
        {
            var nm = p.Key;
            var st = p.Value;

            if (exSelf)
            {
                if (nm == m_state_start.state) continue;
                if (nm == m_state_goal.state) continue;
            }
            if (st!=null && st.wp_outframe_drect!=null)
            {
                if (st.wp_outframe_drect.IntersectsWith(rect))
                {
                    //var rectx = st.m_layout.offset_Frame;
                    //rectx.Intersect(rect);
                    //if (!rectx.IsEmpty)
                    //{
                    //    return true;
                    //}
                    return true;
                }
            }
        }
        return false;
    }

    bool IsHit_statebox(PointF a, PointF b,bool exSelf=false)
    {
        var points = _create_points(a,b);

        foreach(var p in m_stateData)
        {
            var nm = p.Key;
            var st = p.Value;

            if (exSelf)
            {
                if (nm == m_state_start.state) continue;
                if (nm == m_state_goal.state) continue;
            }
            if (st!=null && st.wp_outframe_drect!=null)
            {
                foreach(var ps in points)
                {
                    if (st.wp_outframe_drect.Contains(ps))
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }


    //bool IsHit_statebox(PointF a, PointF b, string exclude_state1, string exclude_state2)
    //{
    //    var rect = _create_rect(a,b);

    //    foreach(var p in m_stateData)
    //    {
    //        if (p.Key == exclude_state1) continue;
    //        if (p.Key == exclude_state2) continue;
    //        var st = p.Value;
    //        if (st!=null && st.wp_outframe_drect!=null)
    //        {
    //            if (st.wp_outframe_drect.IntersectsWith(rect))
    //            {
    //                //var rectx = st.m_layout.offset_Frame;
    //                //rectx.Intersect(rect);
    //                //if (!rectx.IsEmpty)
    //                //{
    //                //    return true;
    //                //}
    //                return true;
    //            }
    //        }
    //    }
    //    return false;
    //}


    bool IsHit_HorizontalLine(PointF a, PointF b) //aとbの領域が 他の水平ラインと重なるか？
    {
        var rect = _create_rect(a,b);

        foreach(var p in m_stateData)
        {
            var st = p.Value;
            if (st!=null)
            {
                //if (st.m_ArrowLine_toNext!=null && _isHit_Line(rect,st.m_ArrowLine_toNext,true))
                if (st.output_arrow!=null && _isHit_Line(rect,st.output_arrow.list,true))
                {
                    return true;
                }
                if (st.gsout_arrow!=null && _isHit_Line(rect,st.gsout_arrow.list,true))
                {
                    return true;
                }
                if (st.bout_arrow_list!=null && st.bout_arrow_list.Length>0)
                {
                    foreach(var plist in st.bout_arrow_list)
                    {
                        if (_isHit_Line(rect,plist.list,true))
                        {
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }
    bool IsHit_startLine(DStateData st,PointF a, PointF b) //aとbの領域がステートから出るNextへの開始線およびbranchからの開始線に重ならないかの確認
    {
        if (st==null) return false;
        var rect = _create_rect(a,b);

        Func<List<PointF>,bool> _isHit = (plist) => {
            if (plist==null) return false;
            if (plist.Count<=2) return false;
            var rect2 = _create_rect(plist[0],plist[2]);
            if (rect2.IntersectsWith(rect))
            {
                if (b.X == plist[2].X)
                {
                    return true;
                }
            }
            return false;
        };

        if (st.output_arrow!=null && _isHit(st.output_arrow.list))
        {
            return true;
        }

        if (st.gsout_arrow!=null && _isHit(st.gsout_arrow.list))
        {
            return true;
        }

        if (st.bout_arrow_list==null)
        {
            return false;
        }
        foreach(var plist in st.bout_arrow_list)
        {
            if (_isHit(plist.list))
            {
                return true;
            }
        }
        return false;
    }
    bool IsHit_goalLine(DStateData st,PointF a, PointF b) //stはgoal先,bがgoal
    {
        if (st==null) return false;
        var rect = _create_rect(a,b);

        Func<List<PointF>,bool> _isHit = (plist)=> {
            if (plist==null) return false;
            if (plist.Count<=2) return false;
            var rect2 = _create_rect(plist[plist.Count-1],plist[plist.Count-2]);
            if (rect2.IntersectsWith(rect))
            {
                if (b.X == plist[2].X)
                {
                    return true;
                }
            }
            return false;
        };

        foreach(var p in m_stateData)
        {
            var st2 = p.Value;
            if (st2==null) continue;
            if (st2.nextstate_data!=null)
            {
                if (st2.nextstate_data == st)
                {
                    if (_isHit(st2.output_arrow.list))
                    {
                        return true;
                    }
                }
            }
            if (st2.gsout_data!=null)
            {
                if (st2.gsout_data == st)
                {
                    if (_isHit(st2.gsout_arrow.list))
                    {
                        return true;
                    }
                }
            }
            if (st2.bout_arrow_list!=null)
            {
                for(var i = 0;  i<st2.bout_arrow_list.Length; i++)
                {
                    if (st2.bout_state_data_list[i] == st)
                    {
                        if (st2.bout_arrow_list!=null && i < st2.bout_arrow_list.Length)
                        {
                            if (_isHit(st2.bout_arrow_list[i].list))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
        }
        return false;
    }

    bool _isHit_Line(RectangleF rect, List<PointF> plist, bool HorV)
    {
        if (plist==null)       return false;
        if (plist.Count < 2)   return false;
        for(var i = 0; i<plist.Count-1; i++)
        {
            var a = plist[i];
            var b = plist[i+1];
            if (HorV)
            {
                if (a.Y - b.Y == 0)
                {
                    var abrect = _create_rect(a,b);
                    if (abrect.IntersectsWith(rect))
                        return true;
                }
            }
            else
            {
                if (a.X - b.X == 0)
                {
                    var abrect = _create_rect(a,b);
                    if (abrect.IntersectsWith(rect))
                        return true;
                }
            }
        }
        return false;
    }

    RectangleF _create_rect(PointF a, PointF b)
    {
        var width = Math.Abs(a.X-b.X);
        var height= Math.Abs(a.Y-b.Y);
        if (width==0 ) width = 5;
        if (height==0) height =5;
        var x = Math.Min(a.X,b.X);
        var y = Math.Min(a.Y,b.Y);

        return new RectangleF(x,y,width,height);
    }


    List<PointF> _create_points(PointF a, PointF b)
    {
        var list = new List<PointF>();

        var xd = Math.Abs(a.X-b.X);
        var yd = Math.Abs(a.Y-b.Y);

        Func<float,int> get_divnum = (d)=>{
            var f = d * 0.1f;
            var n = (int)f;
            if (f - (float)n > 0)
            {
                n++;
            }
            if (n==0) n++;
            return n;
        };

        if (xd > yd) // xで分割
        {
            var n = get_divnum(xd);
            for(var i = 1; i<n-1; i++)
            {
                var t = (float)i/n;
                var p = MathX.Lerp(a,b,t);
                list.Add(p);
            }
        }
        else if (xd < yd) //yで分割
        {
            var n = get_divnum(yd);
            for(var i = 1; i<n-1; i++)
            {
                var t = (float)i/n;
                var p = MathX.Lerp(a,b,t);
                list.Add(p);
            }
        }

        return list;
    }
}
