package tool;

typedef STATEFUNC = Bool->Void;

enum IniUtilReadControl_STATE {
    none;
    //[PSGG OUTPUT START] indent(4) $/^S_/->#enum$
    //             psggConverterLib.dll converted from psgg-file:IniUtilReadControl.psgg

    S_CHECK;
    S_CHECK_MAPG;
    S_CREATE_MAPG;
    S_END;
    S_GET_GROUPNAME;
    S_GET_KEY_VAL;
    S_GET_KEY_VAL1;
    S_IF_MAPG_EXISTS;
    S_INIT;
    S_LOP000;
    S_LOP000_Check____;
    S_LOP000_Next____;
    S_PAS000;
    S_REGISTER_MAPG;
    S_RET000;
    S_SBS000;
    S_SBS001;
    S_SPLIT;
    S_START;


    //[PSGG OUTPUT END]
    unknown;
}

class IniUtilReadControl  {
//#region manager
    var m_curfunc : IniUtilReadControl_STATE;
    var m_nextfunc: IniUtilReadControl_STATE;

    var m_noWait : Bool;

    var m_funcmap : Map<IniUtilReadControl_STATE,STATEFUNC>;

    public function Update() {
        while(true)
        {
            var bFirst = false;
            if (m_nextfunc!=IniUtilReadControl_STATE.none)
            {
                m_curfunc = m_nextfunc;
                m_nextfunc = IniUtilReadControl_STATE.none;
                bFirst = true;
            }
            m_noWait = false;
            if (m_curfunc!=IniUtilReadControl_STATE.none)
            {   
                m_funcmap[m_curfunc](bFirst);
            }
            if (!m_noWait) break;
        }
    }
    function Goto(func : IniUtilReadControl_STATE)
    {
        m_nextfunc = func;
    }
    function CheckState(func : IniUtilReadControl_STATE) : Bool
    {
        return m_curfunc == func;
    }
    function HasNextState() : Bool
    {
        return m_nextfunc != IniUtilReadControl_STATE.none;
    }
    function NoWait()
    {
        m_noWait = true;
    }
//#endregion
//#region gosub
    var MAX_CALLSTACK : Int = 10;
    var m_callstacks : Array<IniUtilReadControl_STATE>;
    var m_callstack_level = 0;
    function GoSubState(nextstate : IniUtilReadControl_STATE, returnstate : IniUtilReadControl_STATE)
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
        m_curfunc    = IniUtilReadControl_STATE.none;
        m_nextfunc   = IniUtilReadControl_STATE.none;
        m_callstacks = [for(i in 0...MAX_CALLSTACK) IniUtilReadControl_STATE.none];
        m_funcmap  = [
            // [PSGG OUTPUT START] indent(12) $/^S_/->#map$
            //             psggConverterLib.dll converted from psgg-file:IniUtilReadControl.psgg

            IniUtilReadControl_STATE.S_CHECK=>S_CHECK,
            IniUtilReadControl_STATE.S_CHECK_MAPG=>S_CHECK_MAPG,
            IniUtilReadControl_STATE.S_CREATE_MAPG=>S_CREATE_MAPG,
            IniUtilReadControl_STATE.S_END=>S_END,
            IniUtilReadControl_STATE.S_GET_GROUPNAME=>S_GET_GROUPNAME,
            IniUtilReadControl_STATE.S_GET_KEY_VAL=>S_GET_KEY_VAL,
            IniUtilReadControl_STATE.S_GET_KEY_VAL1=>S_GET_KEY_VAL1,
            IniUtilReadControl_STATE.S_IF_MAPG_EXISTS=>S_IF_MAPG_EXISTS,
            IniUtilReadControl_STATE.S_INIT=>S_INIT,
            IniUtilReadControl_STATE.S_LOP000=>S_LOP000,
            IniUtilReadControl_STATE.S_LOP000_Check____=>S_LOP000_Check____,
            IniUtilReadControl_STATE.S_LOP000_Next____=>S_LOP000_Next____,
            IniUtilReadControl_STATE.S_PAS000=>S_PAS000,
            IniUtilReadControl_STATE.S_REGISTER_MAPG=>S_REGISTER_MAPG,
            IniUtilReadControl_STATE.S_RET000=>S_RET000,
            IniUtilReadControl_STATE.S_SBS000=>S_SBS000,
            IniUtilReadControl_STATE.S_SBS001=>S_SBS001,
            IniUtilReadControl_STATE.S_SPLIT=>S_SPLIT,
            IniUtilReadControl_STATE.S_START=>S_START,


            // [PSGG OUTPUT END]    
            unknown=>null
        ];

    }
//#endregion

    public function Start()
    {
        Goto(IniUtilReadControl_STATE.S_START);
    }
    public function IsEnd() : Bool    
    { 
        return CheckState(IniUtilReadControl_STATE.S_END); 
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
    //             psggConverterLib.dll converted from psgg-file:IniUtilReadControl.psgg

    /*
        E_0000
        入力バッファ
    */
    public var m_buf : String;
    /*
        E_0001
        ワーク用
    */
    var m_lines : Array<String>;
    var m_index : Int;
    var m_line  : String;
    var m_mapg  : Map<String, Dynamic>;
    var m_group : String;
    /*
        E_0002
        ワーク用２
    */
    var m_key : String;
    var m_val : String;
    /*
        E_0003
        出力
    */
    public var m_map   : Map<String, Dynamic>;
    /*
        S_CHECK
    */
    function S_CHECK(bFirst : Bool)
    {
        var bGroup = psgg.HxRegexUtil.IsMatch("^\\[.+\\]", m_line);
        var bCmt   = psgg.HxRegexUtil.IsMatch("^\\s*;",m_line);
        var bMulti = psgg.HxRegexUtil.IsMatch("^\\s*.+=@@@",m_line);
        var bSingle= psgg.HxRegexUtil.IsMatch("^\\s*.+=.+",m_line);
        // branch
        if (bGroup) { Goto( IniUtilReadControl_STATE.S_CHECK_MAPG ); }
        else if (bMulti) { Goto( IniUtilReadControl_STATE.S_GET_KEY_VAL1 ); }
        else if (bSingle) { Goto( IniUtilReadControl_STATE.S_GET_KEY_VAL ); }
        else if (bCmt) { Goto( IniUtilReadControl_STATE.S_PAS000 ); }
        else { Goto( IniUtilReadControl_STATE.S_PAS000 ); }
    }
    /*
        S_CHECK_MAPG
    */
    function S_CHECK_MAPG(bFirst : Bool)
    {
        // branch
        if (m_mapg==null) { Goto( IniUtilReadControl_STATE.S_GET_GROUPNAME ); }
        else { Goto( IniUtilReadControl_STATE.S_REGISTER_MAPG ); }
    }
    /*
        S_CREATE_MAPG
    */
    function S_CREATE_MAPG(bFirst : Bool)
    {
        //
        if (bFirst)
        {
            m_mapg = new Map<String,Dynamic>();
        }
        //
        if (!HasNextState())
        {
            Goto(IniUtilReadControl_STATE.S_RET000);
        }
    }
    /*
        S_END
    */
    function S_END(bFirst : Bool)
    {
    }
    /*
        S_GET_GROUPNAME
    */
    function S_GET_GROUPNAME(bFirst : Bool)
    {
        //
        if (bFirst)
        {
            var  word = psgg.HxRegexUtil.Get1stMatch("\\[.+\\]",m_line);
            word = word.substr(1);
            word = word.substr(0,word.length-1);
            m_group = word;
        }
        //
        if (!HasNextState())
        {
            Goto(IniUtilReadControl_STATE.S_CREATE_MAPG);
        }
    }
    /*
        S_GET_KEY_VAL
    */
    function S_GET_KEY_VAL(bFirst : Bool)
    {
        //
        if (bFirst)
        {
            var eqindex = m_line.indexOf("=");
            m_key = m_line.substr(0,eqindex);
            m_key = StringTools.trim(m_key);
            m_val = m_line.substr(eqindex+1);
            m_val = StringTools.trim(m_val);
            var map = m_mapg != null ? m_mapg : m_map;
            m_map.set(m_key,m_val);
        }
        //
        if (!HasNextState())
        {
            Goto(IniUtilReadControl_STATE.S_RET000);
        }
    }
    /*
        S_GET_KEY_VAL1
    */
    function S_GET_KEY_VAL1(bFirst : Bool)
    {
        //
        if (bFirst)
        {
            var eqindex = m_line.indexOf("=");
            m_key = m_line.substr(0,eqindex);
            m_key = StringTools.trim(m_key);
            m_val = null;
            m_index += 1;
            while(m_index < m_lines.length) {
                m_line = m_lines[m_index];
                m_line = StringTools.rtrim(m_line);
                if (psgg.HxRegexUtil.IsMatch("^@@@$",m_line)) {
                    break;
                }
                if (m_val!=null) {
                    m_val += "\x0d\x0a";
                    m_val += m_line;
                }
                else {
                    m_val = m_line;
                }
                m_index += 1;
            }
            var map = m_mapg!=null ? m_mapg : m_map;
            map.set(m_key,m_val);
        }
        //
        if (!HasNextState())
        {
            Goto(IniUtilReadControl_STATE.S_RET000);
        }
    }
    /*
        S_IF_MAPG_EXISTS
    */
    function S_IF_MAPG_EXISTS(bFirst : Bool)
    {
        //
        if (bFirst)
        {
            if (m_mapg!=null) {
                m_map.set(m_group, m_mapg);
                m_mapg = null;
            }
        }
        //
        if (!HasNextState())
        {
            Goto(IniUtilReadControl_STATE.S_END);
        }
    }
    /*
        S_INIT
        初期化
    */
    function S_INIT(bFirst : Bool)
    {
        //
        if (bFirst)
        {
            m_map = new Map<String,Dynamic>();
            m_mapg = null;
            m_group = null;
        }
        //
        if (!HasNextState())
        {
            Goto(IniUtilReadControl_STATE.S_SPLIT);
        }
    }
    /*
        S_LOP000
    */
    function S_LOP000(bFirst : Bool)
    {
        m_index = 0;
        Goto(IniUtilReadControl_STATE.S_LOP000_Check____);
        NoWait();
    }
    function S_LOP000_Check____(bFirst : Bool)
    {
        if (m_index < m_lines.length) GoSubState(IniUtilReadControl_STATE.S_SBS000,IniUtilReadControl_STATE.S_LOP000_Next____);
        else               Goto(IniUtilReadControl_STATE.S_IF_MAPG_EXISTS);
        NoWait();
    }
    function S_LOP000_Next____(bFirst : Bool)
    {
        m_index+=1;
        Goto(IniUtilReadControl_STATE.S_LOP000_Check____);
        NoWait();
    }
    /*
        S_PAS000
    */
    function S_PAS000(bFirst : Bool)
    {
        //
        if (!HasNextState())
        {
            Goto(IniUtilReadControl_STATE.S_RET000);
        }
    }
    /*
        S_REGISTER_MAPG
    */
    function S_REGISTER_MAPG(bFirst : Bool)
    {
        //
        if (bFirst)
        {
            m_map[m_group] = m_mapg;
            m_mapg = null;
        }
        //
        if (!HasNextState())
        {
            Goto(IniUtilReadControl_STATE.S_GET_GROUPNAME);
        }
    }
    /*
        S_RET000
    */
    function S_RET000(bFirst : Bool)
    {
        ReturnState();
        NoWait();
    }
    /*
        S_SBS000
    */
    function S_SBS000(bFirst : Bool)
    {
        Goto(IniUtilReadControl_STATE.S_SBS001);
        NoWait();
    }
    /*
        S_SBS001
    */
    function S_SBS001(bFirst : Bool)
    {
        //
        if (bFirst)
        {
            m_line = m_lines[m_index];
        }
        //
        if (!HasNextState())
        {
            Goto(IniUtilReadControl_STATE.S_CHECK);
        }
    }
    /*
        S_SPLIT
        バッファ分解
    */
    function S_SPLIT(bFirst : Bool)
    {
        //
        if (bFirst)
        {
            m_lines = m_buf.split("\x0a");
            for(i in 0...m_lines.length) {
                m_lines[i] = StringTools.rtrim(m_lines[i]);
            }
        }
        //
        if (!HasNextState())
        {
            Goto(IniUtilReadControl_STATE.S_LOP000);
        }
    }
    /*
        S_START
    */
    function S_START(bFirst : Bool)
    {
        Goto(IniUtilReadControl_STATE.S_INIT);
        NoWait();
    }


	// [PSGG OUTPUT END]

	// write your code below

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

