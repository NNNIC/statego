package start;

import tool.IniUtil;
import psgg.HxFile;

typedef STATEFUNC = Bool->Void;

enum ConvControl_STATE {
    none;
    //[PSGG OUTPUT START] indent(4) $/^S_/->#enum$
    //             psggConverterLib.dll converted from psgg-file:ConvControl.psgg

    S_END;
    S_EXTRUCT;
    S_INIT;
    S_START;


    //[PSGG OUTPUT END]
    unknown;
}

class ConvControl  {
//#region manager
    var m_curfunc : ConvControl_STATE;
    var m_nextfunc: ConvControl_STATE;

    var m_noWait : Bool;

    var m_funcmap : Map<ConvControl_STATE,STATEFUNC>;

    public function Update() {
        while(true)
        {
            var bFirst = false;
            if (m_nextfunc!=ConvControl_STATE.none)
            {
                m_curfunc = m_nextfunc;
                m_nextfunc = ConvControl_STATE.none;
                bFirst = true;
            }
            m_noWait = false;
            if (m_curfunc!=ConvControl_STATE.none)
            {   
                m_funcmap[m_curfunc](bFirst);
            }
            if (!m_noWait) break;
        }
    }
    function Goto(func : ConvControl_STATE)
    {
        m_nextfunc = func;
    }
    function CheckState(func : ConvControl_STATE) : Bool
    {
        return m_curfunc == func;
    }
    function HasNextState() : Bool
    {
        return m_nextfunc != ConvControl_STATE.none;
    }
    function NoWait()
    {
        m_noWait = true;
    }
//#endregion
//#region gosub
    var MAX_CALLSTACK : Int = 10;
    var m_callstacks : Array<ConvControl_STATE>;
    var m_callstack_level = 0;
    function GoSubState(nextstate : ConvControl_STATE, returnstate : ConvControl_STATE)
    {
        if (m_callstack_level >= MAX_CALLSTACK -1) {
            trace("CALL STACK OVERFLOW");
            return;
        }
        m_callstacks[m_callstack_level] = returnstate;
        m_callstack_level += 1;
        Goto(nextstate);
    }
    function ReturnState()
    {
        if (m_callstack_level <= 0) {
            trace("CALL STACK UNDERFLOW");
            return;
        }
        m_callstack_level -= 1;
        var nextstate = m_callstacks[m_callstack_level];
        Goto(nextstate);
    }
//#endregion 

//#region CONSTRUCTOR
    public function new(){
        m_curfunc    = ConvControl_STATE.none;
        m_nextfunc   = ConvControl_STATE.none;
        m_callstacks = [for(i in 0...MAX_CALLSTACK) ConvControl_STATE.none];
        m_funcmap  = [
            // [PSGG OUTPUT START] indent(12) $/^S_/->#map$
            //             psggConverterLib.dll converted from psgg-file:ConvControl.psgg

            ConvControl_STATE.S_END=>S_END,
            ConvControl_STATE.S_EXTRUCT=>S_EXTRUCT,
            ConvControl_STATE.S_INIT=>S_INIT,
            ConvControl_STATE.S_START=>S_START,


            // [PSGG OUTPUT END]    
            unknown=>null
        ];

    }
//#endregion

    public function Start()
    {
        Goto(ConvControl_STATE.S_START);
    }
    public function IsEnd() : Bool    
    { 
        return CheckState(ConvControl_STATE.S_END); 
    }
    
    public function Run()
    {
        var LOOPMAX = 100000;
        var bEnd = false;
		Start();
		for(loop_1 in 0...LOOPMAX)
		{
            if (bEnd) break;
            if (loop_1 >= LOOPMAX-1){
                trace("OUT OF LOOP. INCREASE LOOPMAX OR MODIFY USING WHILE"); 
            }
            for(loop_2 in 0...LOOPMAX) {
                Update();
                bEnd = IsEnd();
                if (bEnd) break;
            }
        }
        
	}

	// [PSGG OUTPUT START] indent(4) $/./$
    //             psggConverterLib.dll converted from psgg-file:ConvControl.psgg

    /*
        E_0001
    */
    public var m_psgg_file : String;
    /*
        E_0002
        バッファ
    */
    var m_buf_header   : String;
    var m_buf_chart    : String;
    var m_buf_config   : String;
    var m_buf_tmpsrc   : String;
    var m_buf_tmpfunc  : String;
    var m_buf_setting  : String;
    var m_buf_help     : String;
    var m_buf_iteminfo : String;
    var m_buf_bitmap   : String;
    /*
        E_0003
        マップ
    */
    var m_map_header  : Map<String,Dynamic>;
    var m_map_chart   : Map<String,Dynamic>;
    var m_map_config  : Map<String,Dynamic>;
    var m_map_setting : Map<String,Dynamic>;
    var m_map_help    : Map<String,Dynamic>;
    var m_map_iteminfo: Map<String,Dynamic>;
    /*
        E_0004
        テンプレート
    */
    var m_tmpsrc : Array<String>;
    var m_tmpfunc: Array<String>;
    /*
        S_END
    */
    function S_END(bFirst : Bool)
    {
    }
    /*
        S_EXTRUCT
        抽出
    */
    function S_EXTRUCT(bFirst : Bool)
    {
        //
        if (bFirst)
        {
            extract();
        }
        //
        if (!HasNextState())
        {
            Goto(ConvControl_STATE.S_END);
        }
    }
    /*
        S_INIT
    */
    function S_INIT(bFirst : Bool)
    {
        //
        if (bFirst)
        {
            load();
        }
        //
        if (!HasNextState())
        {
            Goto(ConvControl_STATE.S_EXTRUCT);
        }
    }
    /*
        S_START
    */
    function S_START(bFirst : Bool)
    {
        Goto(ConvControl_STATE.S_INIT);
        NoWait();
    }


	// [PSGG OUTPUT END]

    // write your code below
    
    function load(){
        var buf : String = psgg.HxFile.ReadAllText_String_Encoding(m_psgg_file, new system.text.UTF8Encoding());
        var list = new List<String>();
        while(buf!=null && buf.length > 1) {
            var index = buf.indexOf(wordstrage.Store.PSGG_MARK_PREFIX,1);
            if (index < 0) {
                break;
            }
            var pick = buf.substr(0,index);
            list.add(pick);
            buf = buf.substr(index);
        }
        if (buf!=null && buf.length > 0) {
            list.add(buf);
        }
        
        var i = 0;
        
        for(item in list) {
            trace(item);
            if (i == 0) {
                m_buf_header = item;
            }
            else if (item.indexOf(wordstrage.Store.PSGG_MARK_STATECHART_SHEET) >= 0) {
                m_buf_chart = item;
            } else if (item.indexOf(wordstrage.Store.PSGG_MARK_VARIOUS_SHEET) >= 0) {
                if (item.indexOf("sheet=config") >= 0 ) {
                    m_buf_config = item;
                }
                else if (item.indexOf("sheet=template-source") >= 0 ) {
                    m_buf_tmpsrc = item;
                }
                else if (item.indexOf("sheet=template-statefunc") >= 0) {
                    m_buf_tmpfunc = item;
                }
                else if (item.indexOf("sheet=setting.ini") >= 0) {
                    m_buf_setting = item;
                }
                else if (item.indexOf("sheet=help") >= 0) {
                    m_buf_help = item;
                }
                else if (item.indexOf("sheet=itemsinfo") >= 0) {
                    m_buf_iteminfo = item;
                }
             } else if (item.indexOf(wordstrage.Store.PSGG_MARK_BITMAP_DATA) >= 0) {
                m_buf_bitmap = item;
             }
            i++;
        }
        trace("#m_buf_header=" + m_buf_header.substr(0,80));
        trace("#m_buf_chart="  + m_buf_chart.substr(0,80));
        trace("#m_buf_config=" + m_buf_config.substr(0,80));
        trace("#m_buf_tmpsrc=" + m_buf_tmpsrc.substr(0,80));
        trace("#m_buf_tmpfnc=" + m_buf_tmpfunc.substr(0,80));
        trace("#m_buf_setting=" + m_buf_setting.substr(0,80));
        trace("#m_buf_help=" + m_buf_help.substr(0,80));
        trace("#m_buf_iteminfo=" + m_buf_iteminfo.substr(0,80));
        trace("#m_buf_bitmap=" + m_buf_bitmap.substr(0,80));
    }
    function extract() {
        m_map_header  = IniUtil.ReadIni(m_buf_header);
        m_map_chart   = IniUtil.ReadIni(m_buf_chart);
        m_map_config  = IniUtil.ReadIni(m_buf_config);
        m_map_setting = IniUtil.ReadIni(m_buf_setting);
        m_map_help    = IniUtil.ReadIni(m_buf_help);
        m_map_iteminfo= IniUtil.ReadIni(m_buf_iteminfo);


        var sep = StringUtil.FindNewLineChar(m_buf_tmpsrc);
        var lines = m_buf_tmpsrc.split(sep);
        var refval = new CsRef<Int>();
        refval.Value = 0;
        m_tmpsrc = StringUtil.FindMatchedLines2(lines, wordstrage.Store.PSGG_MARK_VARIOUS_BEGIN, wordstrage.Store.PSGG_MARK_VARIOUS_END, refval);
    
        lines = m_buf_tmpfunc.split(sep);
        var refval = new CsRef<Int>();
        refval.Value = 0;
        m_tmpfunc = StringUtil.FindMatchedLines2(lines, wordstrage.Store.PSGG_MARK_VARIOUS_BEGIN, wordstrage.Store.PSGG_MARK_VARIOUS_END, refval);
        trace(m_tmpfunc);
    }
}

/*  :::: PSGG MACRO ::::
:psgg-macro-start

commentline=// {%0}

@branch=@@@
<<<?"{%0}"/^brifc{0,1}$/
if ([[brcond:{%N}]]) { Goto( $statemachine$_STATE.{%1} ); }
>>>
<<<?"{%0}"/^brelseifc{0,1}$/
else if ([[brcond:{%N}]]) { Goto( $statemachine$_STATE.{%1} ); }
>>>
<<<?"{%0}"/^brelse$/
else { Goto( $statemachine$_STATE.{%1} ); }
>>>
<<<?"{%0}"/^br_/
{%0}($statemachine$_STATE.{%1});
>>>
@@@

#enum=@@@
[[state]];
<<<?state-typ/^loop$/
[[state]]_Check____;
[[state]]_Next____;
>>>
@@@

#map=@@@
$statemachine$_STATE.[[state]]=>[[state]],
<<<?state-typ/^loop$/
$statemachine$_STATE.[[state]]_Check____=>[[state]]_Check____,
$statemachine$_STATE.[[state]]_Next____=>[[state]]_Next____,
>>>
@@@

:psgg-macro-end
*/

