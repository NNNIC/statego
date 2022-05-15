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
    public enum FocucTrackTyp
    {
        none,
        init,   //初期化後
        singl,  //単体フォーカス
        group,  //グループフォーカス
        multi,  //複数フォーカス
        cd,     //dirpath変更
    }

    class FocusTrack
    {
        public class Item
        {
            public string        pathdir;
            public FocucTrackTyp typ = FocucTrackTyp.none;
            public List<string>  states;
            public Point         topleft;
            public double        scale;
            public string        comment;
            public Guid          guid;

            public Item()
            {
                guid = Guid.NewGuid();
            }

            public bool is_equal(Item i)
            {
                if (pathdir != i.pathdir) return false;
                if (topleft != i.topleft) return false;
                if (states==null && i.states==null)
                {
                    return true;
                }
                if (states!=null && i.states!=null)
                {
                    states.Sort();
                    i.states.Sort();
                    if (states.Count == i.states.Count)
                    {
                        for(var n = 0; n < states.Count; n++)
                        {
                            if (states[n] != i.states[n]) return false;
                        }
                        return true;
                    }
                }
                return false;
            }
            public bool is_equal(string i_pathdir, string i_state)
            {
                if (i_pathdir != pathdir) return false;
                if (states!=null && states.Count==1 && states[0] == i_state)
                {
                    return true;
                }
                return false;
            }
            public bool is_equal(string i_pathdie, List<string> i_states)
            {
                if (states==null || i_states==null) return false;
                var input_states = new List<string>(i_states);
                input_states.Sort();
                states.Sort();
                if (states.Count != input_states.Count) return false;
                for(var n = 0; n < states.Count; n++)
                {
                    if (states[n] != input_states[n]) return false;
                }
                return true;
            }
        }

        List<Item> m_trackList = null;
        List<Item> m_trackListRest = null; //バックの際、こちらにインサートする

        void init()
        {
            m_trackList = new List<Item>();
            m_trackListRest = new List<Item>();
        }

        void record(string pathdir, string focusstate, Point topleft, double scale)
        {
            var focusstates = string.IsNullOrEmpty(focusstate) ?  null : new List<string>() { focusstate };
            record(pathdir, new List<string>() { focusstate }, topleft,scale);
        }
        void record(string pathdir, List<string> focusstates, Point topleft, double scale)
        {
            // m_latest と m_trackListの先頭が同じでなければ、pushしておく
            if (m_latest!=null)
            {
                if (m_trackList.Count == 0)
                {
                    m_trackList.Insert(0,m_latest);
                }
                else if (m_trackList.Count > 0 && m_trackList[0].guid != m_latest.guid)
                {
                    m_trackList.Insert(0,m_latest);
                }
            }

            //
            var typ = FocucTrackTyp.none;
            var cmt = string.Empty;
            if (focusstates.Count == 1)
            {
                var s = focusstates[0];
                if (AltState.IsAltState(s))
                {
                    typ = FocucTrackTyp.group;
                    cmt = AltState.TrimAltStateName(s);
                }
                else
                { 
                    typ = FocucTrackTyp.singl;
                    cmt = s;
                }
            }
            else if (focusstates.Count > 1)
            {
                typ = FocucTrackTyp.multi;
                foreach(var s in focusstates)
                {
                    if (cmt.Length > 50) { cmt += "..."; break; }
                    if (cmt.Length!=0) cmt += ",";
                    if (AltState.IsAltState(s))
                    {
                        cmt += "g:"+AltState.TrimAltStateName(s);
                    }
                    else
                    { 
                        cmt += s;
                    }
                }
            }
            else
            {
                return;
            }

            var item = new Item() { pathdir = pathdir, states = ListUtil.Clone( focusstates ), topleft = topleft, scale =scale, typ = typ, comment = cmt };
            var fistitem = ListUtil.GetVal(m_trackList,0);
            if (fistitem!=null && fistitem.is_equal(item))
            {
                return; //記録せず
            }
            m_trackList.Insert(0,item);
            m_trackListRest.Clear(); //追加されるといままでの残りはクリアされる。

            focustrack_from_update(item);
        }
        void record(string pathdir, Point topleft, double scale, FocucTrackTyp typ)
        {
            var item = new Item() { pathdir = pathdir, states =null, topleft = topleft, scale=scale, typ = typ, comment = pathdir };
            m_trackList.Insert(0,item);
            m_trackListRest.Clear(); //追加されるといままでの残りはクリアされる。

            focustrack_from_update(item);
        }

        Item forward()
        {
            if (m_trackListRest!=null && m_trackListRest.Count>0)
            {
                var item = m_trackListRest[0];
                m_trackListRest.RemoveAt(0);
                m_trackList.Insert(0,item);

                focustrack_from_update(item);

                return item;
            }
            return null;
        }
        Item back()
        {
            var item = ListUtil.GetVal(m_trackList,0);
            if (item == null) return null;
            if (m_trackList!=null)
            {
                if (m_trackList.Count >= 2)   //１つのこす
                {
                    m_trackList.RemoveAt(0);
                    m_trackListRest.Insert(0,item);
                }
            }

            focustrack_from_update(item);

            return item;
        }
        #region static singleton

        private static FocusTrack V
        {
            get { if (_V==null) _V = new FocusTrack(); return _V; }
        }
        private static FocusTrack _V;

        public static void Init() { V.init(); }
        public static void Record(string focusstate)
        {
            var pathdir = G.node_get_cur_dirpath();
            var topleft = ViewUtil.GetViewTopLeft();
            V.record(pathdir,focusstate,topleft,G.scale);
        }
        public static void Record(List<string> focusstates)
        {
            var pathdir = G.node_get_cur_dirpath();
            var topleft = ViewUtil.GetViewTopLeft();
            V.record(pathdir,focusstates,topleft,G.scale);
        }
        public static void Record_curpath()
        {
            var pathdir = G.node_get_cur_dirpath();
            var topleft = ViewUtil.GetViewTopLeft();
            V.record(pathdir,topleft, G.scale, FocucTrackTyp.cd);
        }
        public static void Record_atinitiaze()
        {
            var pathdir = G.node_get_cur_dirpath();
            var topleft = ViewUtil.GetViewTopLeft();
            V.record(pathdir,topleft, G.scale, FocucTrackTyp.init);
        }
        public static Item Back()
        {
            return V.back();
        }
        public static Item Forward()
        {
            return V.forward();
        }
        public static int GetDataCount()
        {
            return V.m_trackList.Count;
        }
        #endregion

        #region focus track update
        Item m_latest;
        void focustrack_from_update(Item latest = null)
        {
            m_latest = latest;
            var dg = G.m_focus_track_panel.m_dgv; //G.focustrack_form.dataGridView1;

            var number = 1;
            dg.Rows.Clear();
            for(var n = m_trackList.Count-1; n >=0; n--)
            {
                var item = m_trackList[n];
                dg.Rows.Insert(0,number.ToString(),item.typ.ToString(),item.comment);
                dg.Rows[0].Tag = item.guid;
                number ++;
            }

            if (latest == null && m_trackListRest.Count == 0)
            {
                //dg.Rows.Insert(0,"-","","- current top -");
                dg.Rows[0].Selected = true;
                return;
            }
            //for(var n = m_trackListRest.Count-1; n >= 0; n--)//foreach (var item in m_trackListRest)
            //var selected = number; 
            for (var n = 0; n < m_trackListRest.Count; n++)
            {
                var item = m_trackListRest[n];
                dg.Rows.Insert(0,number.ToString(),item.typ.ToString(),item.comment);
                dg.Rows[0].Tag = item.guid;

                number++;
            }

            if (latest!=null)
            { 
                for (var r = 0; r < dg.Rows.Count; r++)
                {
                    var tguid = (Guid)dg.Rows[r].Tag ;
                    if (tguid == latest.guid)
                    {
                        dg.Rows[r].Selected = true;
                        break;
                    }
                }
            }

            if ( dg.SelectedCells.Count > 0 ) { 
                // 対象行が非表示の場合は何もしない
                if ( !dg.SelectedCells[0].Visible ) {
                    return;
                }
                // 対象行が既に画面内に表示されている時は何もしない
                if ( dg.SelectedCells[0].Displayed ) {
                    return;
                }
                dg.FirstDisplayedScrollingRowIndex = dg.SelectedCells[0].RowIndex;
               
            }
        }
        #endregion

    }
}
