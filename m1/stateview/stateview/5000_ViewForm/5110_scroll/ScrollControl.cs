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
using G=stateview.Globals;
using DStateData=stateview.Draw.DrawStateData;
using EFU=stateview._5300_EditForm.EditFormUtil;
using SS=stateview.StateStyle;
using DS=stateview.DesignSpec;
//>>>

public partial class ScrollControl  {
   
    #region manager
    Action<bool> m_curfunc;
    Action<bool> m_nextfunc;

    bool         m_noWait;
    
    public void Update()
    {
        while(true)
        {
            var bFirst = false;
            if (m_nextfunc!=null)
            {
                m_curfunc = m_nextfunc;
                m_nextfunc = null;
                bFirst = true;
            }
            m_noWait = false;
            if (m_curfunc!=null)
            {   
                m_curfunc(bFirst);
            }
            if (!m_noWait) break;
        }
    }
    void Goto(Action<bool> func)
    {
        m_nextfunc = func;
    }
    bool CheckState(Action<bool> func)
    {
        return m_curfunc == func;
    }
    bool HasNextState()
    {
        return m_nextfunc != null;
    }
    void NoWait()
    {
        m_noWait = true;
    }
    #endregion
    #region gosub
    List<Action<bool>> m_callstack = new List<Action<bool>>();
    void GoSubState(Action<bool> nextstate, Action<bool> returnstate)
    {
        m_callstack.Insert(0,returnstate);
        Goto(nextstate);
    }
    void ReturnState()
    {
        var nextstate = m_callstack[0];
        m_callstack.RemoveAt(0);
        Goto(nextstate);
    }
    #endregion 

    public void Start()
    {
        Goto(S_START);
    }
    public bool IsEnd()     
    { 
        return CheckState(S_END); 
    }
    
    public void Run()
    {
		int LOOPMAX = (int)(1E+5);
		Start();
		for(var loop = 0; loop <= LOOPMAX; loop++)
		{
			if (loop>=LOOPMAX) throw new SystemException("Unexpected.");
			Update();
			if (IsEnd()) break;
		}
	}

	#region    // [PSGG OUTPUT START] indent(4) $/./$
//  psggConverterLib.dll converted from ScrollControl.xlsx.    psgg-file:ScrollControl.psgg
    /*
        E_0002
    */
    public enum Mode {
        none,
        init,
        mpb_changed,
        scrolled
    }
    public Mode m_mode = Mode.none;
    /*
        E_0003
    */
    VScrollBar m_vSB    { get { return G.view_form.vScrollBar1;     } }
    HScrollBar m_hSB    { get { return G.view_form.hScrollBar1;     } }
    Panel      m_panel1 { get { return G.view_form.panel1;          } }
    PictureBox m_mpb    { get { return G.main_picturebox;           } }
    int        m_spw    { get { return G.view_form.splitter1.Width; } }
    /*
        E_0005
    */
    void update_locwh()
    {
        m_vSB.Location = new Point(m_panel1.Width - m_vSB.Width,0);
        m_vSB.Height   = m_panel1.Height;// - m_hSB.Height;
        m_hSB.Location = new Point(m_spw, m_panel1.Height - m_hSB.Height);
        m_hSB.Width    = m_panel1.Width - m_vSB.Width - m_spw;
    }
    /*
        S_0001
        Mode 確認
    */
    void S_0001(bool bFirst)
    {
        // branch
        if (m_mode == Mode.init) { Goto( S_INIT ); }
        else if (m_mode == Mode.mpb_changed) { Goto( S_SETTING ); }
        else if (m_mode == Mode.scrolled) { Goto( S_BARCHANGED ); }
        else { Goto( S_0002 ); }
    }
    /*
        S_0002
    */
    void S_0002(bool bFirst)
    {
        //
        if (bFirst)
        {
            m_mode = Mode.none;
        }
        //
        if (!HasNextState())
        {
            Goto(S_END);
        }
    }
    /*
        S_BARCHANGED
    */
    void S_BARCHANGED(bool bFirst)
    {
        //
        if (bFirst)
        {
            scroll_barchanged();
        }
        //
        if (!HasNextState())
        {
            Goto(S_0002);
        }
    }
    /*
        S_END
    */
    void S_END(bool bFirst)
    {
    }
    /*
        S_INIT
    */
    void S_INIT(bool bFirst)
    {
        //
        if (bFirst)
        {
            m_vSB.Parent   = m_panel1;
            m_vSB.Visible  = true;
            m_hSB.Parent   = m_panel1;
            m_hSB.Visible  = true;
            update_locwh();
        }
        //
        if (!HasNextState())
        {
            Goto(S_0002);
        }
    }
    /*
        S_SETTING
    */
    void S_SETTING(bool bFirst)
    {
        //
        if (bFirst)
        {
            scroll_changevalue();
        }
        //
        if (!HasNextState())
        {
            Goto(S_0002);
        }
    }
    /*
        S_START
    */
    void S_START(bool bFirst)
    {
        Goto(S_0001);
        NoWait();
    }


	#endregion // [PSGG OUTPUT END]

	void scroll_changevalue(bool bSuspendLayout=true)
    {
        if (bSuspendLayout)
        {
            m_vSB.SuspendLayout();
            m_hSB.SuspendLayout();
        }
        //
        update_locwh();
        // Vertical
        m_vSB.Minimum = 0;
        m_vSB.Maximum = m_panel1.Height;
        m_vSB.LargeChange = m_panel1.Height;
        // Horizontal
        m_hSB.Minimum = 0;
        m_hSB.Maximum = m_panel1.Width;
        m_hSB.LargeChange = m_panel1.Width;

        // Position
        Action<bool> calc = (b)=> {
            var mpbTopLeftXY  = b ? m_mpb.Location.X : m_mpb.Location.Y;
            var mpbBotRightXY = b ? m_mpb.Location.X + m_mpb.Width : m_mpb.Location.Y + m_mpb.Height;
            
            var mpbWH    = b ? m_mpb.Width : m_mpb.Height;
            var scbar    = b ? (ScrollBar)m_hSB : (ScrollBar)m_vSB;
            var p1wh     = b ? m_panel1.Width : m_panel1.Height;

            var p1gtr  = b ? ( m_panel1.Width > m_mpb.Width ) : (m_panel1.Height > m_mpb.Height);

            /*
                 CASE  A           B             C              D  
                    P1>mpb      P1 < mpb   p1loc > mpbloc   p1loc < mpbloc
            P1   |----------|     |---|       |----|           |----|
            mpb     |----|      |--------|  |---|                 |---|

            CASE A
                 max = p1.w
                 lc  = mpb.w
                 val = mpb.x
                 start = 0

            CASE B
                 max = mpb.w
                 lc  = p1w
                 val = -mpb.x
                 start = mpb.x

            CASE C
                max = -mpb.x + p1w 
                lc  = min(p1.w,mpb.w)
                val = -mpb.x
                start = mpb.x

            CASE D
                max = mpb.x + mpb.w
                lc =  min(p1w,mpb.w)
                val = mpb.x    
                start = 0                     
            */

            var caseA = mpbTopLeftXY >= 0 && mpbBotRightXY <  p1wh;
            var caseB = mpbTopLeftXY <= 0 && mpbBotRightXY >= p1wh;
            var caseC = mpbTopLeftXY <= 0 && mpbBotRightXY <= p1wh;

            var visible = true;
            var max = 0;
            var lc = p1wh;
            var val = 0;
            var caseid = string.Empty;

            if (caseA)
            {
                max = p1wh;
                lc = mpbWH;
                val = mpbTopLeftXY;
                caseid = "A";
            }
            else if (caseB)
            {
                max    =  mpbWH;
                lc     =  p1wh;
                val    = -mpbTopLeftXY;
                caseid =  "B";
            }
            else if (caseC)
            {
                max = -mpbTopLeftXY + p1wh;
                if (p1gtr)
                {
                    lc = mpbWH;
                    val = 0;
                }
                else
                {
                    lc = p1wh;
                    val = -mpbTopLeftXY;
                }
                caseid = "C";
            }
            else // caseD
            {
                max = mpbTopLeftXY + mpbWH;
                if (p1gtr)
                {
                    lc = mpbWH;
                    val = max;
                }
                else
                {
                    lc = p1wh;
                    val = 0;
                }
                
                caseid = "D";
            }
            
            scbar.Visible = visible;
            scbar.Maximum = max;
            scbar.LargeChange = lc;
            scbar.Value = val;

            var hash = new Hashtable();
            hash["caseid"] =  caseid;
            hash["start"]  =  val;
            hash["loc"]    =  mpbTopLeftXY;
            hash["p1gtr"]  =  p1gtr;
            scbar.Tag = hash;

            //G.NoticeToUser(string.Format("{0}:Case {1}, panel{2}mpb",(b ? "H":"V"), caseid, (p1gtr ? ">":"<")));

        };

        calc(true);
        calc(false);

        //
        if (bSuspendLayout)
        {
            m_vSB.ResumeLayout();
            m_hSB.ResumeLayout();
        }
    }

    void scroll_barchanged()
    {
        m_vSB.SuspendLayout();
        m_hSB.SuspendLayout();

        Action<bool,int> setlocXY = (b,v)=> {
            if (b)
            {
                m_mpb.Location = PointUtil.Mod_X(m_mpb.Location,v);
            }
            else
            {
                m_mpb.Location = PointUtil.Mod_Y(m_mpb.Location,v);
            }
        };
        Action<bool,int> sublocXY = (b,v)=> {
            if (b)
            {
                m_mpb.Location = PointUtil.Add_X(m_mpb.Location,-v);
            }
            else
            {
                m_mpb.Location = PointUtil.Add_Y(m_mpb.Location,-v);
            }
        };

        Action<bool> scr=(b)=> {
            var scbar    = b ? (ScrollBar)m_hSB : (ScrollBar)m_vSB;
            var max = scbar.Maximum;
            var val = scbar.Value;
            var hash = (Hashtable)scbar.Tag;
            var start  = (int)hash["start"];   //初期時のval位置
            var caseid = (string)hash["caseid"]; 
            var loc    = (int)hash["loc"];
            var p1gtr  = (bool)hash["p1gtr"];

            var vdiff = val - start; //valの移動量

            var XY2 = 0;
            if (p1gtr)
            {
                XY2 = loc + vdiff;
            }
            else
            {
                XY2 = loc - vdiff;
            }
            setlocXY(b,XY2);
        };

        scr(true);
        scr(false);

        m_vSB.ResumeLayout();
        m_hSB.ResumeLayout();
    }

    #region ユーティリティ
    /*
        SetScrollTopLeft_at_0to1(x,y)
        x,yは0～1に正規化した値
        トップレフトを指定した値にする。

        - p1サイズがmpbサイズより大きい時は、中央に配置させる。
    */
    public void SetScrollTopLeft_at_0to1(double x, double y)
    {
        Action<bool,double> settl=(b,s)=> {
            var scbar    = b ? (ScrollBar)m_hSB : (ScrollBar)m_vSB;
            var mpbWH    = b ? m_mpb.Width : m_mpb.Height;

            var max = scbar.Maximum;
            var lc  = scbar.LargeChange;
            var val = scbar.Value;
            var hash = (Hashtable)scbar.Tag;
            var start  = (int)hash["start"];   //初期時のval位置
            var caseid = (string)hash["caseid"]; 
            var loc    = (int)hash["loc"];
            var p1gtr  = (bool)hash["p1gtr"];

            if (p1gtr) //中央に
            {
                if (max > mpbWH)
                {
                    val = (max - mpbWH) / 2;
                }
                else
                {
                    val = 0;
                }
            }
            else
            {
                var rang = max - lc;
                val = (int)((float)rang * s);
            }
            scbar.Value = val;
        };       
        
        settl(true,  x);
        settl(false, y);
    }

    /// <summary>
    /// 表示域の左端を指定 x,yはビットマップ位置
    /// - p1サイズがmpbサイズより大きい時は、中央に配置させる。
    /// </summary>
    public void SetViewCenter(double x, double y)
    {
        var scpos = G.vf_sc.GetScreenPosFormPointOnImage(new PointF((float)x,(float)y));
        var center = RectangleUtil.Center( new RectangleF(m_panel1.Location, m_panel1.Size) );
        var center_scpos = m_panel1.Parent.PointToScreen(Point.Round(center));
        var diff = PointUtil.Sub_Point(center_scpos , scpos);
        m_mpb.Location = Point.Round( PointUtil.Add_Point(m_mpb.Location, diff) );
        G.Scroll_mpb_changed();
    }
    /// <summary>
    /// main_picterboxのロケーション移動
    /// 見えない手前で止めたい
    /// </summary>
    public void MoveMpbLoc(double dx, double dy)
    {
        var add = new PointF((float)dx,(float)dy);
        m_mpb.Location = Point.Round( PointUtil.Add_Point(m_mpb.Location, add) );
        G.Scroll_mpb_changed();
    }
    public void SetViewTopLeft(double nx, double ny)
    {
        m_mpb.Location = Point.Round( new PointF((float)nx,(float)ny) );
        G.Scroll_mpb_changed();
    }
    #endregion

}

/*  :::: PSGG MACRO ::::
:psgg-macro-start

commentline=// {%0}

@branch=@@@
<<<?"{%0}"/^brifc{0,1}$/
if ([[brcond:{%N}]]) { Goto( {%1} ); }
>>>
<<<?"{%0}"/^brelseifc{0,1}$/
else if ([[brcond:{%N}]]) { Goto( {%1} ); }
>>>
<<<?"{%0}"/^brelse$/
else { Goto( {%1} ); }
>>>
<<<?"{%0}"/^br_/
{%0}({%1});
>>>
@@@

:psgg-macro-end
*/

