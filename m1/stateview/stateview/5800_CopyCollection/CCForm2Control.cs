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
using System.Collections.Specialized;
using System.Threading;
using stateview;

public partial class CCForm2Control  {

    public stateview._5800_CopyCollection.CCForm2 m_form;       
    DataGridView m_dg    { get { return m_form.dataGridView1;    } }
    int UUID_COL         { get { return m_form.UUID_COL;         } }
    int CMT_COL          { get { return m_form.CMT_COL;          } }
    int m_cur_item_index {
        get { return m_form.m_cur_item_index; }
        set { m_form.m_cur_item_index = value; }
    }

    bool m_mouse_up_down   { get { return m_form.m_mouse_up_down;   } }
    int m_mouse_down_start { get { return m_form.m_mousedown_start; } }

    bool m_bMenuCopy       { get { return m_form.m_bMenuCopy;       }  set { m_form.m_bMenuCopy       = value; } }
    bool m_bMenuPaste      { get { return m_form.m_bMenuPaste;      }  set { m_form.m_bMenuPaste      = value; } }
    bool m_bMenuDelete     { get { return m_form.m_bMenuDelete;     }  set { m_form.m_bMenuDelete     = value; } }
    bool m_bMenuCut        { get { return m_form.m_bMenuCut;        }  set { m_form.m_bMenuCut        = value; } }
    bool m_bMenuEdit       { get { return m_form.m_bMenuEdit;       }  set { m_form.m_bMenuEdit       = value; } }
    bool m_bMenuUp         { get { return m_form.m_bMenuUp;         }  set { m_form.m_bMenuUp         = value; } }
    bool m_bMenuDown       { get { return m_form.m_bMenuDown;       }  set { m_form.m_bMenuDown       = value; } }
    bool m_bMenuEditBitmap { get { return m_form.m_bMenuEditBitmap; }  set { m_form.m_bMenuEditBitmap = value; } }
    bool m_bMenuEditTitle  { get { return m_form.m_bMenuEditTitle;  }  set { m_form.m_bMenuEditTitle  = value; } } 
    bool m_bMenuExplorer   { get { return m_form.m_bMenuExplorer;   }  set { m_form.m_bMenuExplorer   = value; } } 

    CopyCollection.WorkItem m_pageitem { get { return m_form.m_pageitem; } }

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
    //             psggConverterLib.dll converted from psgg-file:CCForm2Control.psgg                                // *DoNotEdit*
                                                                            // *DoNotEdit*
    /*                                                                      // *DoNotEdit*
        E_0001                                                              // *DoNotEdit*
    */                                                                      // *DoNotEdit*
    public bool m_ForceUpdate;                                              // *DoNotEdit*
    /*                                                                      // *DoNotEdit*
        S_CHECK_MODE                                                        // *DoNotEdit*
    */                                                                      // *DoNotEdit*
    void S_CHECK_MODE(bool bFirst)                                          // *DoNotEdit*
    {                                                                       // *DoNotEdit*
        var bDone = check_mousehold();                                      // *DoNotEdit*
        if (bDone == false) return;                                         // *DoNotEdit*
        select_work();                                                      // *DoNotEdit*
        m_form.Refresh();                                                   // *DoNotEdit*
        // branch                                                           // *DoNotEdit*
        if (check_mousedown_menu()) { Goto( S_OpenMenu ); }                 // *DoNotEdit*
        else if (check_mousedown_hold()) { Goto( S_DoDragDrop ); }          // *DoNotEdit*
        else { Goto( S_CHECK_MOUSE_DOWN ); }                                // *DoNotEdit*
        //                                                                  // *DoNotEdit*
        if (HasNextState())                                                 // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            NoWait();                                                       // *DoNotEdit*
        }                                                                   // *DoNotEdit*
    }                                                                       // *DoNotEdit*
    /*                                                                      // *DoNotEdit*
        S_CHECK_MOUSE_DOWN                                                  // *DoNotEdit*
    */                                                                      // *DoNotEdit*
    void S_CHECK_MOUSE_DOWN(bool bFirst)                                    // *DoNotEdit*
    {                                                                       // *DoNotEdit*
        //                                                                  // *DoNotEdit*
        if (bFirst)                                                         // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            mouse_init();                                                   // *DoNotEdit*
            m_ForceUpdate=false;                                            // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        var b = mouse_is_down();                                            // *DoNotEdit*
        if (b==false && m_ForceUpdate == false) return;                     // *DoNotEdit*
        // branch                                                           // *DoNotEdit*
        if (m_ForceUpdate) { Goto( S_FORCEUPDATE ); }                       // *DoNotEdit*
        else { Goto( S_TICK ); }                                            // *DoNotEdit*
        //                                                                  // *DoNotEdit*
        if (HasNextState())                                                 // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            NoWait();                                                       // *DoNotEdit*
        }                                                                   // *DoNotEdit*
    }                                                                       // *DoNotEdit*
    /*                                                                      // *DoNotEdit*
        S_Copy                                                              // *DoNotEdit*
    */                                                                      // *DoNotEdit*
    void S_Copy(bool bFirst)                                                // *DoNotEdit*
    {                                                                       // *DoNotEdit*
        //                                                                  // *DoNotEdit*
        if (bFirst)                                                         // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            menu_copy();                                                    // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        //                                                                  // *DoNotEdit*
        if (!HasNextState())                                                // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            Goto(S_UPDATE_OTHERS);                                          // *DoNotEdit*
        }                                                                   // *DoNotEdit*
    }                                                                       // *DoNotEdit*
    /*                                                                      // *DoNotEdit*
        S_Cut                                                               // *DoNotEdit*
    */                                                                      // *DoNotEdit*
    void S_Cut(bool bFirst)                                                 // *DoNotEdit*
    {                                                                       // *DoNotEdit*
        //                                                                  // *DoNotEdit*
        if (bFirst)                                                         // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            menu_cut();                                                     // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        //                                                                  // *DoNotEdit*
        if (!HasNextState())                                                // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            Goto(S_UPDATE_OTHERS);                                          // *DoNotEdit*
        }                                                                   // *DoNotEdit*
    }                                                                       // *DoNotEdit*
    /*                                                                      // *DoNotEdit*
        S_Delete                                                            // *DoNotEdit*
    */                                                                      // *DoNotEdit*
    void S_Delete(bool bFirst)                                              // *DoNotEdit*
    {                                                                       // *DoNotEdit*
        //                                                                  // *DoNotEdit*
        if (bFirst)                                                         // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            menu_delete();                                                  // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        //                                                                  // *DoNotEdit*
        if (!HasNextState())                                                // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            Goto(S_UPDATE_OTHERS);                                          // *DoNotEdit*
        }                                                                   // *DoNotEdit*
    }                                                                       // *DoNotEdit*
    /*                                                                      // *DoNotEdit*
        S_DoDragDrop                                                        // *DoNotEdit*
    */                                                                      // *DoNotEdit*
    void S_DoDragDrop(bool bFirst)                                          // *DoNotEdit*
    {                                                                       // *DoNotEdit*
        //                                                                  // *DoNotEdit*
        if (bFirst)                                                         // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            dodrag();                                                       // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        //                                                                  // *DoNotEdit*
        if (!HasNextState())                                                // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            Goto(S_UPDATE_OTHERS);                                          // *DoNotEdit*
        }                                                                   // *DoNotEdit*
    }                                                                       // *DoNotEdit*
    /*                                                                      // *DoNotEdit*
        S_Down                                                              // *DoNotEdit*
    */                                                                      // *DoNotEdit*
    void S_Down(bool bFirst)                                                // *DoNotEdit*
    {                                                                       // *DoNotEdit*
        //                                                                  // *DoNotEdit*
        if (bFirst)                                                         // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            menu_down();                                                    // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        //                                                                  // *DoNotEdit*
        if (!HasNextState())                                                // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            Goto(S_UPDATE_OTHERS);                                          // *DoNotEdit*
        }                                                                   // *DoNotEdit*
    }                                                                       // *DoNotEdit*
    /*                                                                      // *DoNotEdit*
        S_EditBitmap                                                        // *DoNotEdit*
    */                                                                      // *DoNotEdit*
    void S_EditBitmap(bool bFirst)                                          // *DoNotEdit*
    {                                                                       // *DoNotEdit*
        //                                                                  // *DoNotEdit*
        if (bFirst)                                                         // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            menu_edit_bitmap();                                             // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        if (!menu_edit_bitmap_done()) return;                               // *DoNotEdit*
        menu_edit_bitmap_post();                                            // *DoNotEdit*
        //                                                                  // *DoNotEdit*
        if (!HasNextState())                                                // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            Goto(S_UPDATE_OTHERS);                                          // *DoNotEdit*
        }                                                                   // *DoNotEdit*
    }                                                                       // *DoNotEdit*
    /*                                                                      // *DoNotEdit*
        S_EditTitle                                                         // *DoNotEdit*
    */                                                                      // *DoNotEdit*
    void S_EditTitle(bool bFirst)                                           // *DoNotEdit*
    {                                                                       // *DoNotEdit*
        //                                                                  // *DoNotEdit*
        if (bFirst)                                                         // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            menu_edit_title();                                              // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        if (!menu_edit_title_done()) return;                                // *DoNotEdit*
        menu_edit_title_post();                                             // *DoNotEdit*
        //                                                                  // *DoNotEdit*
        if (!HasNextState())                                                // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            Goto(S_UPDATE_OTHERS);                                          // *DoNotEdit*
        }                                                                   // *DoNotEdit*
    }                                                                       // *DoNotEdit*
    /*                                                                      // *DoNotEdit*
        S_END                                                               // *DoNotEdit*
    */                                                                      // *DoNotEdit*
    void S_END(bool bFirst)                                                 // *DoNotEdit*
    {                                                                       // *DoNotEdit*
    }                                                                       // *DoNotEdit*
    /*                                                                      // *DoNotEdit*
        S_Explorer                                                          // *DoNotEdit*
        アイテム保存場所をExplorerで開く                                    // *DoNotEdit*
    */                                                                      // *DoNotEdit*
    void S_Explorer(bool bFirst)                                            // *DoNotEdit*
    {                                                                       // *DoNotEdit*
        //                                                                  // *DoNotEdit*
        if (bFirst)                                                         // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            menu_explorer();                                                // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        //                                                                  // *DoNotEdit*
        if (!HasNextState())                                                // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            Goto(S_PAS000);                                                 // *DoNotEdit*
        }                                                                   // *DoNotEdit*
    }                                                                       // *DoNotEdit*
    /*                                                                      // *DoNotEdit*
        S_FORCEUPDATE                                                       // *DoNotEdit*
    */                                                                      // *DoNotEdit*
    void S_FORCEUPDATE(bool bFirst)                                         // *DoNotEdit*
    {                                                                       // *DoNotEdit*
        //                                                                  // *DoNotEdit*
        if (bFirst)                                                         // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            force_update();                                                 // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        //                                                                  // *DoNotEdit*
        if (!HasNextState())                                                // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            Goto(S_TICK2);                                                  // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        //                                                                  // *DoNotEdit*
        if (HasNextState())                                                 // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            NoWait();                                                       // *DoNotEdit*
        }                                                                   // *DoNotEdit*
    }                                                                       // *DoNotEdit*
    /*                                                                      // *DoNotEdit*
        S_OpenMenu                                                          // *DoNotEdit*
    */                                                                      // *DoNotEdit*
    void S_OpenMenu(bool bFirst)                                            // *DoNotEdit*
    {                                                                       // *DoNotEdit*
        //                                                                  // *DoNotEdit*
        if (bFirst)                                                         // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            open_menu();                                                    // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        if (!done_menu()) return;                                           // *DoNotEdit*
        // branch                                                           // *DoNotEdit*
        if (m_bMenuCopy) { Goto( S_Copy ); }                                // *DoNotEdit*
        else if (m_bMenuPaste) { Goto( S_Paste ); }                         // *DoNotEdit*
        else if (m_bMenuDelete) { Goto( S_Delete ); }                       // *DoNotEdit*
        else if (m_bMenuCut) { Goto( S_Cut ); }                             // *DoNotEdit*
        else if (m_bMenuUp) { Goto( S_Up ); }                               // *DoNotEdit*
        else if (m_bMenuDown) { Goto( S_Down ); }                           // *DoNotEdit*
        else if (m_bMenuEditBitmap) { Goto( S_EditBitmap ); }               // *DoNotEdit*
        else if (m_bMenuEditTitle) { Goto( S_EditTitle ); }                 // *DoNotEdit*
        else if (m_bMenuExplorer) { Goto( S_Explorer ); }                   // *DoNotEdit*
        else { Goto( S_OpenMenu1 ); }                                       // *DoNotEdit*
        //                                                                  // *DoNotEdit*
        if (HasNextState())                                                 // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            NoWait();                                                       // *DoNotEdit*
        }                                                                   // *DoNotEdit*
    }                                                                       // *DoNotEdit*
    /*                                                                      // *DoNotEdit*
        S_OpenMenu1                                                         // *DoNotEdit*
        new state                                                           // *DoNotEdit*
    */                                                                      // *DoNotEdit*
    void S_OpenMenu1(bool bFirst)                                           // *DoNotEdit*
    {                                                                       // *DoNotEdit*
        //                                                                  // *DoNotEdit*
        if (!HasNextState())                                                // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            Goto(S_UPDATE_OTHERS);                                          // *DoNotEdit*
        }                                                                   // *DoNotEdit*
    }                                                                       // *DoNotEdit*
    /*                                                                      // *DoNotEdit*
        S_PAS000                                                            // *DoNotEdit*
    */                                                                      // *DoNotEdit*
    void S_PAS000(bool bFirst)                                              // *DoNotEdit*
    {                                                                       // *DoNotEdit*
        //                                                                  // *DoNotEdit*
        if (!HasNextState())                                                // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            Goto(S_CHECK_MOUSE_DOWN);                                       // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        //                                                                  // *DoNotEdit*
        if (HasNextState())                                                 // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            NoWait();                                                       // *DoNotEdit*
        }                                                                   // *DoNotEdit*
    }                                                                       // *DoNotEdit*
    /*                                                                      // *DoNotEdit*
        S_Paste                                                             // *DoNotEdit*
    */                                                                      // *DoNotEdit*
    void S_Paste(bool bFirst)                                               // *DoNotEdit*
    {                                                                       // *DoNotEdit*
        //                                                                  // *DoNotEdit*
        if (bFirst)                                                         // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            menu_paste();                                                   // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        //                                                                  // *DoNotEdit*
        if (!HasNextState())                                                // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            Goto(S_UPDATE_OTHERS);                                          // *DoNotEdit*
        }                                                                   // *DoNotEdit*
    }                                                                       // *DoNotEdit*
    /*                                                                      // *DoNotEdit*
        S_START                                                             // *DoNotEdit*
    */                                                                      // *DoNotEdit*
    void S_START(bool bFirst)                                               // *DoNotEdit*
    {                                                                       // *DoNotEdit*
        Goto(S_CHECK_MOUSE_DOWN);                                           // *DoNotEdit*
        NoWait();                                                           // *DoNotEdit*
    }                                                                       // *DoNotEdit*
    /*                                                                      // *DoNotEdit*
        S_TICK                                                              // *DoNotEdit*
    */                                                                      // *DoNotEdit*
    void S_TICK(bool bFirst)                                                // *DoNotEdit*
    {                                                                       // *DoNotEdit*
        //                                                                  // *DoNotEdit*
        if (!HasNextState())                                                // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            Goto(S_CHECK_MODE);                                             // *DoNotEdit*
        }                                                                   // *DoNotEdit*
    }                                                                       // *DoNotEdit*
    /*                                                                      // *DoNotEdit*
        S_TICK2                                                             // *DoNotEdit*
    */                                                                      // *DoNotEdit*
    void S_TICK2(bool bFirst)                                               // *DoNotEdit*
    {                                                                       // *DoNotEdit*
        //                                                                  // *DoNotEdit*
        if (!HasNextState())                                                // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            Goto(S_CHECK_MOUSE_DOWN);                                       // *DoNotEdit*
        }                                                                   // *DoNotEdit*
    }                                                                       // *DoNotEdit*
    /*                                                                      // *DoNotEdit*
        S_Up                                                                // *DoNotEdit*
    */                                                                      // *DoNotEdit*
    void S_Up(bool bFirst)                                                  // *DoNotEdit*
    {                                                                       // *DoNotEdit*
        //                                                                  // *DoNotEdit*
        if (bFirst)                                                         // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            menu_up();                                                      // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        //                                                                  // *DoNotEdit*
        if (!HasNextState())                                                // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            Goto(S_UPDATE_OTHERS);                                          // *DoNotEdit*
        }                                                                   // *DoNotEdit*
    }                                                                       // *DoNotEdit*
    /*                                                                      // *DoNotEdit*
        S_UPDATE_OTHERS                                                     // *DoNotEdit*
        他の同ウインドウを更新                                              // *DoNotEdit*
    */                                                                      // *DoNotEdit*
    void S_UPDATE_OTHERS(bool bFirst)                                       // *DoNotEdit*
    {                                                                       // *DoNotEdit*
        //                                                                  // *DoNotEdit*
        if (bFirst)                                                         // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            update_others();                                                // *DoNotEdit*
        }                                                                   // *DoNotEdit*
        if (!update_others_done()) return;                                  // *DoNotEdit*
        //                                                                  // *DoNotEdit*
        if (!HasNextState())                                                // *DoNotEdit*
        {                                                                   // *DoNotEdit*
            Goto(S_PAS000);                                                 // *DoNotEdit*
        }                                                                   // *DoNotEdit*
    }                                                                       // *DoNotEdit*
                                                                            // *DoNotEdit*
                                                                            // *DoNotEdit*
    #endregion // [PSGG OUTPUT END]

    #region マウス
    bool m_bHold;
    bool m_bClick;
    void mouse_init()
    {
        m_bHold  = false;
        m_bClick = false;
    }
    bool mouse_is_down()
    {
        return m_mouse_up_down == false;
    }

    bool check_mousehold()
    {
        int TIMEOUT = 300;

        var span = Environment.TickCount - m_mouse_down_start;
        var bUp = m_mouse_up_down;
        var to  = (span > TIMEOUT);
        if (bUp)
        {
            if (to == false)
            {
                m_bClick = true;
                return true;
            }
            return true;
        }
        else
        {
            if (to)
            {
                m_bHold = true;
                return true;
            }
        }
        return false;
    }

    //CopyCollection.WorkItem m_prev_item;
    bool check_mousedown_menu()
    {
        /*        
        var bOk = m_prev_item == m_select_item;
        m_prev_item = m_select_item;

        if (m_bClick)
        {
            if (bOk)
            {
                return true;
            }
        }
        return false;
        */
        return m_bClick;
    }

    bool check_mousedown_hold()
    {
        return m_bHold;
    }

    CopyCollection.WorkItem m_select_item;
    void select_work()
    {
        var selectrow = m_dg.SelectedRows!=null && m_dg.SelectedRows.Count>0 ? m_dg.SelectedRows[0] : null;
        if (selectrow == null) return;
        m_cur_item_index = selectrow.Index;
        m_dg.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        m_dg.Rows[m_cur_item_index].Selected = true;

        var uuid      = m_dg[UUID_COL,m_cur_item_index].Value.ToString();
        m_select_item = G.cc.FindItem(uuid);
    }
    #endregion

    void dodrag()
    {
        if (m_select_item == null) return;
        var files = new StringCollection { m_select_item.cappng_path };
        var dataobj = new DataObject(DataFormats.FileDrop, files);

        var bmp = default(Bitmap);
        {
            var rbmp = m_select_item.bitmap;
            var max = 200f;
            var dx = max / rbmp.Width;
            var dy = max / rbmp.Height;
            var d  = Math.Min(dx,dy);
            var w2 = (double)rbmp.Width;
            var h2 = (double)rbmp.Height;
            if (d < 1.0f)
            {
                w2 *= d;
                h2 *= d;
            }
            bmp = new Bitmap((int)w2,(int)h2);
            Graphics g = Graphics.FromImage(bmp);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.DrawImage(rbmp, 0,0,(int)w2,(int)h2);
            g.Dispose();
        }

        Cursor.Current = Cursors.Hand;

        dataobj.SetData(DataFormats.Bitmap,bmp);
        m_dg.DoDragDrop(dataobj,DragDropEffects.All);
        bmp.Dispose();
        bmp = null;

        Cursor.Current = Cursors.Default;
    }

    #region 
    void open_menu()
    {
        m_bMenuCopy       = false;
        m_bMenuDelete     = false;
        m_bMenuPaste      = false;
        m_bMenuCut        = false;
        m_bMenuEdit       = false;
        m_bMenuUp         = false;
        m_bMenuDown       = false;
        m_bMenuEditTitle  = false;
        m_bMenuEditBitmap = false;
        m_bMenuExplorer   = false;

        ((ToolStripDropDownMenu)m_form.editToolStripMenuItem.DropDown).ShowCheckMargin = false;
        ((ToolStripDropDownMenu)m_form.editToolStripMenuItem.DropDown).ShowImageMargin = false;
        m_form.contextMenuStripDg.Show(Cursor.Position);
    }
    bool done_menu()
    {
        var bouds = RectangleUtil.AddMargin( m_form.contextMenuStripDg.Bounds , 5);
        Rectangle? editbouds = m_form.editToolStripMenuItem.DropDown.Visible ?  (Rectangle?)m_form.editToolStripMenuItem.DropDown.Bounds : null;
        if (editbouds!=null)
        {
            bouds = RectangleUtil.Combine( Rectangle.Round( bouds ),(Rectangle)editbouds);
        }

        if (!bouds.Contains(Cursor.Position))
        {
            m_form.contextMenuStripDg.Visible = false;
        }

        return m_form.contextMenuStripDg.Visible == false;
    }
    CopyCollection.WorkItem m_copy_pageitem;
    CopyCollection.WorkItem m_copy_item;
    bool? m_copypate_cutpaste;

    void menu_copy()
    {
        if (m_select_item!=null)
        {
            m_copy_item = m_select_item;
            m_copy_pageitem = m_pageitem;

            m_copypate_cutpaste = true; //コピーです。            

            if (m_copy_item.datatxt_path!=null)
            {
                try {
                    var data = File.ReadAllText(m_copy_item.datatxt_path,Encoding.UTF8);
                    Clipboard.SetText(data);
                } catch (SystemException e)
                {
                    G.NoticeToUser_warning("{A167B5A4-8879-4E77-8B62-C862E05A3ED2}" + e.Message);
                }
            }

            G.NoticeToUser("Copyed " + m_select_item.name +".");
        }
        else
        {
            G.NoticeToUser_warning("{4EF886EE-812F-4C80-B68B-3F44BA0952C5}");
        }
    }
    void menu_cut()
    {
        if (m_select_item!=null)
        {
            m_copy_item = m_select_item;
            m_copy_pageitem = m_pageitem;

            m_copypate_cutpaste = false; //ペーストです。            

            G.NoticeToUser("Cut " + m_select_item.name +".");
        }
        else
        {
            G.NoticeToUser_warning("{7AEE4EF0-4EC2-42B4-8EE8-4C23ECEEE06C}");
        }

    }
    void menu_paste()
    {
        if (m_copypate_cutpaste==true)
        {
            menu_copypaste();
        }
        else if (m_copypate_cutpaste==false)
        {
            menu_cutpaste();
        }
        else
        {
            G.NoticeToUser_warning("Neither cut paste nor copy paste.");
        }
    }
    void menu_copypaste()
    {
        var newitem = G.cc.CloneItem(m_copy_item, m_pageitem.folder);
        m_form.loaddata();
    }
    void menu_cutpaste()
    {
        var newitem = G.cc.CloneItem(m_copy_item, m_pageitem.folder);
        G.cc.DeleteItem(m_copy_item);
        m_form.loaddata();
    }

    void menu_delete()
    {
        if (m_pageitem.uuid == CopyCollection.TRASH_DIR)
        {
            G.cc.DeleteItem(m_select_item);
        }
        else
        {
            var trashdir = G.cc.GetTrashItem().folder;
            var newitem = G.cc.CloneItem(m_select_item, trashdir);
            G.cc.DeleteItem(m_select_item);
        }
        m_form.loaddata();
    }

    void menu_edit()
    {
    }
    stateview._5800_CopyCollection.CCBmpForm m_bmpform;
    void menu_edit_bitmap()
    {
        m_bmpform = new stateview._5800_CopyCollection.CCBmpForm();
        m_bmpform.DialogResult = DialogResult.None;
        m_bmpform.m_item = m_select_item;
        m_bmpform.Show();
    }
    bool menu_edit_bitmap_done()
    {
        return m_bmpform.DialogResult != DialogResult.None;
    }
    void menu_edit_bitmap_post()
    {
        if (m_bmpform.DialogResult == DialogResult.OK)
        {
            G.cc.UpdateItem(m_select_item,true);
            m_form.loaddata();
        }
    }
    bool m_menu_edit_done;
    void menu_edit_title()
    {
        G.NoticeToUser_warning("menu_edit_title");

        m_dg.SelectionMode = DataGridViewSelectionMode.CellSelect;
        m_menu_edit_done = false;
        m_dg[CMT_COL,m_cur_item_index].Selected = true;
        m_dg.CellLeave += M_dg_CellLeave;

        m_dg.BeginEdit(true);
    }

    private void M_dg_CellLeave(object sender, DataGridViewCellEventArgs e)
    {
        m_menu_edit_done = true;
    }

    bool menu_edit_title_done()
    {
        return m_menu_edit_done;
    }
    void menu_edit_title_post()
    {
        m_dg.CellLeave -= M_dg_CellLeave;
        m_dg.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        m_select_item.comment = m_dg[CMT_COL,m_cur_item_index].Value.ToString();
        G.cc.UpdateItem(m_select_item);
        m_form.loaddata();
    }
    void menu_up()
    {
        if (
            (m_cur_item_index==0)
            ||
           (m_dg.Rows.Count<=1)
            ||
           (m_cur_item_index>=m_dg.Rows.Count)
            )
        {
            G.NoticeToUser_warning("Error up");
            return;
        }
        var up1_item = get_item(m_cur_item_index-1);
        var up2_item = get_item(m_cur_item_index-2);
        var disporder_1 = up1_item!=null ? up1_item.disporder :  m_select_item.disporder - 1;
        var disporder_2 = up2_item!=null ? up2_item.disporder :  disporder_1 - 1;
        var newdisporder = (disporder_1 + disporder_2) * 0.5f;
        m_select_item.disporder = newdisporder;

        update_all_items_on_this_page();

        m_cur_item_index = m_cur_item_index - 1;
        m_form.loaddata();
    }
    void menu_down()
    {
        if (m_cur_item_index + 1 >= m_dg.Rows.Count)
        {
            G.NoticeToUser_warning("Error down");
            return;
        }
        var down1_item = get_item(m_cur_item_index + 1);
        var down2_item = get_item(m_cur_item_index + 2);
        var disporder_1 = down1_item!=null ? down1_item.disporder : m_select_item.disporder + 1;
        var disporder_2 = down2_item!=null ? down2_item.disporder : disporder_1 + 1 ;
        var newdisporder = (disporder_1 + disporder_2) * 0.5f;
        m_select_item.disporder = newdisporder;

        update_all_items_on_this_page();

        m_cur_item_index = m_cur_item_index + 1;
        m_form.loaddata();
    }
    void menu_explorer()
    {
        if (m_select_item != null)
        {
            ExecUtil.execute_start2(m_select_item.folder,null);
        }
    }
    #endregion

    void force_update()
    {
        m_form.loaddata();
        m_ForceUpdate = false;
    }
    bool m_update_others_done;
    void update_others()
    {
        m_update_others_done = true;
    }
    bool update_others_done()
    {
        return m_update_others_done;
    }


    #region  Utility
    string get_uuid(int row)
    {
        if (row < 0 || m_dg.Rows.Count <= row) return null;
        return m_dg[UUID_COL,row].Value.ToString();
    }
    CopyCollection.WorkItem get_item(int row)
    {
        var uuid = get_uuid(row);
        var item = G.cc.FindItem(uuid,m_pageitem);
        return item;
    }
    void update_all_items_on_this_page(bool bWithBitmap = false)
    {
        var items = G.cc.GetItemsIfFolder(m_pageitem);
        foreach(var i in items)
        {
            G.cc.UpdateItem(i, bWithBitmap);
        }
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

