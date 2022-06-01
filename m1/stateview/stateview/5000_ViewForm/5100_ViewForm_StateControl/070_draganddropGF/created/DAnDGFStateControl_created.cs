using System;
using System.Collections.Generic;
public partial class DAnDGFStateControl  {
  

	#region    // [PSGG OUTPUT START] indent(4) $/./$
    //             psggConverterLib.dll converted from psgg-file:..\doc\DAnDGFStateControl.psgg

    /*
        S_DefCancel
    */
    void S_DefCancel(int phase, bool bFirst)
    {
        //
        if (phase == 0)
        {
    	    if (bFirst)
    	    {
    	        def_Cancel();
    	    }
    	    if (!HasNextState())
    	    {
    	        SetNextState(S_Destroy);
    	    }
            GoNextState();
            return;
        }
        //
    }
    /*
        S_DefSuccess
        正常
    */
    void S_DefSuccess(int phase, bool bFirst)
    {
        //
        if (phase == 0)
        {
    	    if (bFirst)
    	    {
    	        def_Success();
    	    }
    	    if (!HasNextState())
    	    {
    	        SetNextState(S_Destroy);
    	    }
            GoNextState();
            return;
        }
        //
    }
    /*
        S_Destroy
    */
    void S_Destroy(int phase, bool bFirst)
    {
        //
        if (phase == 0)
        {
    	    if (bFirst)
    	    {
    	        destroy_all();
    	    }
    	    if (!HasNextState())
    	    {
    	        SetNextState(S_END);
    	    }
            GoNextState();
            return;
        }
        //
    }
    /*
        S_DragMove
        ドラッグ移動
    */
    void S_DragMove(int phase, bool bFirst)
    {
        //
        if (phase == 0)
        {
    	    if (bFirst)
    	    {
    	        ovlpb_show();
    	    }
    	    move_update();
    	    if (!wait_mouseany()) return;
    	    // branch
    	    br_IsDrop(S_Drop);
    	    br_NotAbove(S_DefCancel);
            GoNextState();
            return;
        }
        //
    }
    /*
        S_Drop
        ドロップ
    */
    void S_Drop(int phase, bool bFirst)
    {
        //
        if (phase == 0)
        {
    	    if (bFirst)
    	    {
    	        ovlpb_hide();
    	        statebox_redraw();
    	    }
    	    if (!HasNextState())
    	    {
    	        SetNextState(S_DefSuccess);
    	    }
    	    //
    	    NoWait();
            GoNextState();
            return;
        }
        //
    }
    /*
        S_END
    */
    void S_END(int phase, bool bFirst)
    {
    }
    /*
        S_INIT
        グループフォーカスのステート移動開始
    */
    void S_INIT(int phase, bool bFirst)
    {
        //
        if (phase == 0)
        {
    	    if (bFirst)
    	    {
    	        ovlbmp_create();
    	        ovldraw_do();
    	        ovlpb_setbmp();
    	    }
    	    if (!HasNextState())
    	    {
    	        SetNextState(S_DragMove);
    	    }
    	    //
    	    NoWait();
            GoNextState();
            return;
        }
        //
    }
    /*
        S_START
    */
    void S_START(int phase, bool bFirst)
    {
        SetNextState(S_INIT);
        NoWait();
        GoNextState();
    }


	#endregion // [PSGG OUTPUT END]

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

