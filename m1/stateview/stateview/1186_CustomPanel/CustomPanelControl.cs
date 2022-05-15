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
public partial class CustomPanelControl  {
   
    public static bool m_locked = false;

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
//  psggConverterLib.dll converted from CustomPanelControl.xlsx.    psgg-file:CustomPanelControl.psgg
    /*
        E_0002
    */
    public GroupBox m_gb;
    public DataGridView m_dgv;
    /*
        S_1TICK
    */
    void S_1TICK(bool bFirst)
    {
        //
        if (!HasNextState())
        {
            Goto(S_CHECK);
        }
    }
    /*
        S_1TICK1
    */
    void S_1TICK1(bool bFirst)
    {
        //
        if (!HasNextState())
        {
            Goto(S_SIZE_CHANGING);
        }
    }
    /*
        S_1TICK2
    */
    void S_1TICK2(bool bFirst)
    {
        //
        if (!HasNextState())
        {
            Goto(S_SIZE_CHANGING1);
        }
    }
    /*
        S_CHECK
        １．表示中か
        ２．グループぼっくの中か？
        ２．四隅でマウスダウンしたか->拡縮
        ５．それ以外でダウンしたか->移動
    */
    void S_CHECK(bool bFirst)
    {
        // branch
        if (checkvisible()==false) { Goto( S_1TICK ); }
        else if (checkpermission()==false) { Goto( S_1TICK ); }
        else if (checkmousedown()==false) { Goto( S_UNLOCKED ); }
        else if (checkbounds()==false) { Goto( S_UNLOCKED ); }
        else if (checkcorner()) { Goto( S_SIZE_START ); }
        else { Goto( S_MOVE_START ); }
        //
        if (HasNextState())
        {
            NoWait();
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
        初期化
    */
    void S_INIT(bool bFirst)
    {
        //
        if (bFirst)
        {
            savemargin();
        }
        //
        if (!HasNextState())
        {
            Goto(S_CHECK);
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_MOVE_START
        移動開始
        ポジション記録
    */
    void S_MOVE_START(bool bFirst)
    {
        //
        if (bFirst)
        {
            savemovepos();
        }
        //
        if (!HasNextState())
        {
            Goto(S_1TICK2);
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_SIZE_CHANGE_WORK
    */
    void S_SIZE_CHANGE_WORK(bool bFirst)
    {
        //
        if (bFirst)
        {
            changesize();
            savemargin();
        }
        //
        if (!HasNextState())
        {
            Goto(S_1TICK1);
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_SIZE_CHANGE_WORK1
    */
    void S_SIZE_CHANGE_WORK1(bool bFirst)
    {
        //
        if (bFirst)
        {
            changeloc();
            savemargin();
        }
        //
        if (!HasNextState())
        {
            Goto(S_1TICK2);
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_SIZE_CHANGING
    */
    void S_SIZE_CHANGING(bool bFirst)
    {
        // branch
        if (checkmousedown()) { Goto( S_SIZE_CHANGE_WORK ); }
        else { Goto( S_UNLOCKED ); }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_SIZE_CHANGING1
    */
    void S_SIZE_CHANGING1(bool bFirst)
    {
        // branch
        if (checkmousedown()) { Goto( S_SIZE_CHANGE_WORK1 ); }
        else { Goto( S_UNLOCKED ); }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_SIZE_START
        拡縮開始
        ポジション記録
    */
    void S_SIZE_START(bool bFirst)
    {
        //
        if (bFirst)
        {
            savesizepos();
        }
        //
        if (!HasNextState())
        {
            Goto(S_1TICK1);
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }
    /*
        S_START
    */
    void S_START(bool bFirst)
    {
        Goto(S_INIT);
        NoWait();
    }
    /*
        S_UNLOCKED
    */
    void S_UNLOCKED(bool bFirst)
    {
        //
        if (bFirst)
        {
            m_locked = false;
        }
        //
        if (!HasNextState())
        {
            Goto(S_1TICK);
        }
        //
        if (HasNextState())
        {
            NoWait();
        }
    }


	#endregion // [PSGG OUTPUT END]

	// write your code below

    bool checkpermission()
    {
        if (stateview.ViewUtil.IsBusy()) return false;
        if (m_locked == true) return false; 
        m_locked = true;
        return true;
    }

    bool checkvisible()
    {
        //G.NoticeToUser("vf.focus=" + G.view_form.panel1.Focused.ToString() );

        var f = G.view_form;
        return  (f.panel1.Focused || f.MainPictureBox.Focused || f.dataGridView_historyrecord.Focused || f.dataGridViewFocusTrack.Focused ) && m_gb.Visible;
    }

    bool checkbounds()
    {
        try { //終了中にハングのため
            var posonpanel = G.view_form.PointToClient( Cursor.Position );
            return m_gb.Bounds.Contains(posonpanel );
        } catch { }
        return false;
    }

    bool checkmousedown()
    {
        try {  //終了中にハングのため
        var posonpanel = G.view_form.PointToClient( Cursor.Position );
        //G.NoticeToUser(posonpanel.ToString() + " bounds=" + m_gb.Bounds.ToString() + " contains=" + m_gb.Bounds.Contains(posonpanel).ToString());
        

        return (int)(Control.MouseButtons & (MouseButtons.Left | MouseButtons.Right | MouseButtons.Middle)) != 0;
        } catch { }
        return false;
    }

    public enum CORNER { none,TL,TR,BL,BR  }
    CORNER m_corner;
    bool checkcorner()
    {
        m_corner = CORNER.none;
        var gbb = m_gb.Bounds;
        var size = 10;
        var tl = new Rectangle( gbb.Left, gbb.Top,size,size);
        var tr = new Rectangle( gbb.Right - size, gbb.Top, size,size);
        var bl = new Rectangle( gbb.Left, gbb.Bottom - size, size,size);
        var br = new Rectangle( gbb.Right - size, gbb.Bottom - size, size,size);

        var posonpanel = G.view_form.PointToClient( Cursor.Position );
        if (tl.Contains(posonpanel)) m_corner = CORNER.TL;
        if (tr.Contains(posonpanel)) m_corner = CORNER.TR;
        if (bl.Contains(posonpanel)) m_corner = CORNER.BL;
        if (br.Contains(posonpanel)) m_corner = CORNER.BR;

        //G.NoticeToUser("Corner="+m_corner.ToString());

        return m_corner!= CORNER.none;
    }
    Point     m_startsizepos;
    Point     m_startsizeloc;
    int       m_startsizewidth;
    int       m_startsizeheight;
    void savesizepos()
    {
        m_startsizepos = Cursor.Position;
        m_startsizeloc = m_gb.Location;
        m_startsizewidth  = m_gb.Width;
        m_startsizeheight = m_gb.Height;
    }
    void changesize()
    {
        var diff = PointUtil.Sub_Point(Cursor.Position, m_startsizepos);
        if (diff.X == 0 && diff.Y == 0) return;
        var newloc   = m_startsizeloc;
        var newwidth = m_startsizewidth;
        var newheight= m_startsizeheight;
        switch (m_corner)
        {
            case CORNER.none:
                break;
            case CORNER.TL:
                newloc =  PointUtil.Add_Point(newloc, diff);
                newwidth -= diff.X;
                newheight -= diff.Y;
                break;
            case CORNER.TR:
                newloc = PointUtil.Add_Y(newloc, diff.Y);
                newwidth  += diff.X;
                newheight -= diff.Y;
                break;
            case CORNER.BL:
                newloc = PointUtil.Add_X(newloc, diff.X);
                newwidth -= diff.X;
                newheight+= diff.Y;
                break;
            case CORNER.BR:
                newwidth += diff.X;
                newheight += diff.Y;
                break;
            default:
                break;
        }

        if (newwidth < 80) return;
        if (newheight < 80) return;
        var checkrect = new Rectangle(newloc.X, newloc.Y, newwidth, newheight);

        //var panelbounds = G.view_form.panel1.Bounds;
        //panelbounds.Width  = G.view_form.panel1.ClientSize.Width;
        //panelbounds.Height = G.view_form.panel1.ClientSize.Height;
        var panelbounds = Get_panelbounds();

        if (!panelbounds.Contains(checkrect))
        {
            return;
        }
        

        m_gb.Location = newloc;
        m_gb.Width = newwidth;
        m_gb.Height = newheight;

        m_dgv.Width = newwidth - 8;
        m_dgv.Height = newheight - 18;
        m_dgv.Columns[2].Width = m_dgv.Width - m_dgv.Columns[0].Width - m_dgv.Columns[1].Width;
    }
    Point m_startmovepos;
    Point m_startlocpos;
    void savemovepos()
    {
        m_startmovepos = Cursor.Position;
        m_startlocpos = m_gb.Location;
    }
    void changeloc()
    {
        var diff = PointUtil.Sub_Point(Cursor.Position, m_startmovepos);
        if (diff.X != 0 || diff.Y!=0 )
        { 
            var newloc = PointUtil.Add_Point(m_startlocpos,diff);
            var checkrect = new Rectangle(newloc.X, newloc.Y, m_gb.Width, m_gb.Height);

            //var panelbounds = G.view_form.panel1.Bounds;
            //panelbounds.Width  = G.view_form.panel1.ClientSize.Width;
            //panelbounds.Height = G.view_form.panel1.ClientSize.Height;

            var panelbounds = Get_panelbounds();
            
            if (!panelbounds.Contains(checkrect))
            {
                return;
            }

            m_gb.Location = newloc;
        }
    }

    Rectangle Get_panelbounds()
    {
        var spw = G.view_form.splitter1.Width;

        var width  = G.view_form.panel1.ClientSize.Width - 2 *spw;
        var height = G.view_form.panel1.ClientSize.Height - 2 * spw;
        
        var newloc = PointUtil.Add_XY(G.view_form.panel1.Location, spw , spw );

        return new Rectangle(newloc, new Size( width, height) );
    }

    int m_margin_top;
    int m_margin_right;
    int m_margin_bot;
    int m_margin_left;
    void savemargin()
    {
        var panelbounds = Get_panelbounds();
        m_margin_top = panelbounds.Top - m_gb.Top;
        m_margin_right = panelbounds.Right - m_gb.Right;
        m_margin_bot = panelbounds.Bottom - m_gb.Bottom;
        m_margin_left = m_gb.Left - panelbounds.Left;
    }

    public void adjustloc() //panelのサイズ変更の際に呼ばれる
    {
        var range = 50;
        var rect = m_gb.Bounds;
        
        var panelbounds = Get_panelbounds();
        var dx = 0;
        var dy = 0;

        if (m_margin_bot < range)
        { 
            var margin_bot = panelbounds.Bottom - m_gb.Bottom;
            if (margin_bot > m_margin_bot)
            {
                var t = panelbounds.Bottom - m_margin_bot;
                dy = t - m_gb.Bottom; 
            }
        }
        if (m_margin_right < range)
        {
            var margin_right = panelbounds.Right - m_gb.Right;
            if (margin_right > m_margin_right)
            {
                var t = panelbounds.Right - m_margin_right;
                dx= t - m_gb.Right;
            }
        }

        if (rect.Left < panelbounds.Left)     dx = panelbounds.Left - rect.Left;
        if (rect.Right > panelbounds.Right)   dx = panelbounds.Right - rect.Right;
        if (rect.Bottom > panelbounds.Bottom) dy = panelbounds.Bottom - rect.Bottom;

        var newloc = PointUtil.Add_XY(m_gb.Location,dx,dy);

        m_gb.Anchor = AnchorStyles.None;
        m_gb.Location = newloc;
        m_gb.Anchor = AnchorStyles.Top | AnchorStyles.Right;

        savemargin();
    }
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

