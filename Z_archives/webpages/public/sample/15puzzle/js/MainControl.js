// == manager ==
var MainControl = function () {
    "use strict";

    this.curfunc   = null;
    this.nextfunc  = null;
    this.candfunc  = null;
    this.nowait    = false;

    // [SYN-G-GEN OUTPUT START]  indent(4) $/^S_./->#mems$
//  psggConverterLib.dll converted from MainControl.xlsx. 
    this.m_bS_CREATE_PANELS = false;

    this.bDone = false;
    this.bDone = false;
    this.m_S_GET_TOUCH_done = false;














    this.m_limit_S_WAIT = 0;
    this.m_limit_S_WAIT1 = 0;
    this.m_limit_S_WAIT2 = 0;


    // [SYN-G-GEN OUTPUT END]
};

// [SYN-G-GEN OUTPUT START]  indent(0) $/^E_./$
//  psggConverterLib.dll converted from MainControl.xlsx. 


// [SYN-G-GEN OUTPUT END]


//MainControl.prototype = new SMBASE();
MainControl.prototype.update = function () {
    "use strict";
    var b = true, first = false;
    while (b) {
        this.nowait = false;

        first = false;
        if (this.nextfunc !== null) {
            this.curfunc  = this.nextfunc;
            this.nextfunc = null;
            first = true;
        }
        if (this.curfunc !== null) {
            this.curfunc(first);
        }

        if (!this.nowait) { break; }
    }
};
MainControl.prototype.checkstate = function (st) {
    "use strict";
    return this.candfunc === st;
};
MainControl.prototype.goto = function (st) {
    "use strict";
    this.nextfunc = st;
};
MainControl.prototype.setnext = function (st) {
    "use strict";
    this.candfunc = st;
};
MainControl.prototype.gonext = function () {
    "use strict";
    this.nextfunc = this.candfunc;
    this.candfunc = null;
};
MainControl.prototype.hasnext = function () {
    "use strict";
    return this.candfunc !== null;
};
MainControl.prototype.setnowait = function () {
    "use strict";
    this.nowait = true;
};
MainControl.prototype.start = function () {
    "use strict";
    this.goto(this.S_START);
};
MainControl.prototype.is_end = function () {
    "use strict";
    return this.checkstate(this.S_END);
};
// == yesno set ==
MainControl.yesno = false;
MainControl.prototype.br_yes = function (st) {
    "use strict";
    if (!this.hasnext(st)) {
        if (this.yesno) {
            this.setnext(st);
        }
    }
};
MainControl.prototype.br_no = function (st) {
    "use strict";
    if (!this.hasnext(st)) {
        if (!this.yesno) {
            this.setnext(st);
        }
    }
};

// [SYN-G-GEN OUTPUT START]  indent(0) $/^S_./$
//  psggConverterLib.dll converted from MainControl.xlsx. 
/*
    S_CHECK_COMPLETION
*/
MainControl.prototype.S_CHECK_COMPLETION = function (first) {
    "use strict";
    var b = this.check_completion();
    console.log("RESULT:" + b);
    if (b) { this.setnext( this.S_SHOW_YOUWIN ); }
    else { this.setnext( this.S_GET_TOUCH ); }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_CREATE_PANELS
*/
MainControl.prototype.S_CREATE_PANELS = function (first) {
    "use strict";
    if (first) {
        this.m_bS_CREATE_PANELS = false;
        G.create_panel_control.m_reqcb=()=>{  G.main_control.m_bS_CREATE_PANELS = true;  };
    }
    if (this.m_bS_CREATE_PANELS === false) { return; }
    if (!this.hasnext()) {
        this.setnext(this.S_REMOVE_16);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_END
*/
MainControl.prototype.S_END = function (first) {
    "use strict";
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_FADE_IN
*/
MainControl.prototype.S_FADE_IN = function (first) {
    "use strict";
    var fc  = G.fade_control;
    var fct = G.fadetxt_control;
    if (first) {
        console.log("S_FADE_IN start");
        fc.m_in_or_out = true;
        fct.m_in_or_out = true;
        this.bDone = false;
        fc.m_reqcb = () => {  G.main_control.bDone = true;  console.log("done!!!")  };
        fct.m_reqcb=()=>{};
    }
    if (!this.bDone) { return; }
    if (!this.hasnext()) {
        this.setnext(this.S_LOG);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_FADE_OUT
*/
MainControl.prototype.S_FADE_OUT = function (first) {
    "use strict";
    var fc  = G.fade_control;
    var fct = G.fadetxt_control;
    if (first) {
        console.log("S_FADE_OUT start");
        fc.m_in_or_out = false;
        fct.m_in_or_out = false;
        this.bDone = false;
        fc.m_reqcb = () => {  G.main_control.bDone = true;  console.log("done!!!")  };
        fct.m_reqcb=()=>{};
    }
    if (!this.bDone) { return; }
    if (!this.hasnext()) {
        this.setnext(this.S_HIDE_YOUWIN);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_GET_TOUCH
*/
MainControl.prototype.S_GET_TOUCH = function (first) {
    "use strict";
    if (first) {
        this.m_S_GET_TOUCH_done = false;
        G.touch_control.m_reqcb = ()=>{  this.m_S_GET_TOUCH_done = true; };
    }
    if (this.m_S_GET_TOUCH_done===false) { return; }
    if (!this.hasnext()) {
        this.setnext(this.S_GET_TOUCH_RESULT);
    }
    if (this.hasnext()) {
        this.setnowait();
        this.gonext();
    }
};
/*
    S_GET_TOUCH_RESULT
*/
MainControl.prototype.S_GET_TOUCH_RESULT = function (first) {
    "use strict";
    if (first) {
        console.log("hitobj=" + G.touch_control.m_hitobj_name);
        console.log("dir=" + G.touch_control.m_dir);
    }
    if (!this.hasnext()) {
        this.setnext(this.S_TRY_MOVE);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_HIDE_KEY
*/
MainControl.prototype.S_HIDE_KEY = function (first) {
    "use strict";
    if (first) {
        console.log("S_HIDE_KEY");
        G.key_control.move_button('fade');
        G.key_control.move_button('shot');
        G.key_control.move_button('Q');
    }
    if (!this.hasnext()) {
        this.setnext(this.S_REQUEST_FADE);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_HIDE_NEXT
*/
MainControl.prototype.S_HIDE_NEXT = function (first) {
    "use strict";
    if (first) {
        G.fe_control.show_next(false);
    }
    if (this.bS_HIDE_NEXT === false) { return; }
    if (!this.hasnext()) {
        this.setnext(this.S_SET_MONDAL_PANEL1);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_HIDE_YOUWIN
*/
MainControl.prototype.S_HIDE_YOUWIN = function (first) {
    "use strict";
    if (first) {
        G.title_control.show_youwin(false);
    }
    if (!this.hasnext()) {
        this.setnext(this.S_HIDE_NEXT);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_LOG
*/
MainControl.prototype.S_LOG = function (first) {
    "use strict";
    if (first) {
        G.map_control.log_map_info();
    }
    if (!this.hasnext()) {
        this.setnext(this.S_GET_TOUCH);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_REMOVE_16
*/
MainControl.prototype.S_REMOVE_16 = function (first) {
    "use strict";
    if (first) {
        G.map_control.remove_panel16();
    }
    if (!this.hasnext()) {
        this.setnext(this.S_SET_MONDAL_PANEL);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_REQUEST_FADE
*/
MainControl.prototype.S_REQUEST_FADE = function (first) {
    "use strict";
    if (first) {
        console.log("S_REQUEST_FADE");
        G.fade_control.m_reqcb = function () {
            console.log("REQ END!");
        };
    }
    if (!this.hasnext()) {
        this.setnext(this.S_LOG);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_SET_MONDAL_PANEL
*/
MainControl.prototype.S_SET_MONDAL_PANEL = function (first) {
    "use strict";
    if (first) {
        var n = Math.floor( 100 * Math.random() );
        show_mondai(n);
    }
    if (!this.hasnext()) {
        this.setnext(this.S_HIDE_KEY);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_SET_MONDAL_PANEL1
*/
MainControl.prototype.S_SET_MONDAL_PANEL1 = function (first) {
    "use strict";
    if (first) {
        var n = Math.floor( 100 * Math.random() );
        show_mondai(n);
    }
    if (!this.hasnext()) {
        this.setnext(this.S_FADE_IN);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_SHOW_NEXT
*/
MainControl.prototype.S_SHOW_NEXT = function (first) {
    "use strict";
    if (first) {
        this.bS_SHOW_NEXT = false;
        G.fe_control.next_cb=()=>{ G.main_control.bS_SHOW_NEXT = true;   };
        G.fe_control.show_next(true);
    }
    if (this.bS_SHOW_NEXT === false) { return; }
    if (!this.hasnext()) {
        this.setnext(this.S_FADE_OUT);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_SHOW_NEXT1
    new state
*/
MainControl.prototype.S_SHOW_NEXT1 = function (first) {
    "use strict";
    if (first) {
        alert("!");
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_SHOW_YOUWIN
*/
MainControl.prototype.S_SHOW_YOUWIN = function (first) {
    "use strict";
    if (first) {
        G.title_control.show_youwin(true);
    }
    if (!this.hasnext()) {
        this.setnext(this.S_WAIT2);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_START
*/
MainControl.prototype.S_START = function (first) {
    "use strict";
    if (!this.hasnext()) {
        this.setnext(this.S_WAIT);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_TRY_MOVE
*/
MainControl.prototype.S_TRY_MOVE = function (first) {
    "use strict";
    if (first) {
        G.move_logic_control.m_hitobj_name = G.touch_control.m_hitobj_name;
        G.move_logic_control.m_dir = G.touch_control.m_dir;
        G.move_logic_control.run();
    }
    if (!this.hasnext()) {
        this.setnext(this.S_CHECK_COMPLETION);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_WAIT
*/
MainControl.prototype.S_WAIT = function (first) {
    "use strict";
    if (first) {
        this.m_limit_S_WAIT = Date.now() + 1000;
    }
    if (this.m_limit_S_WAIT > Date.now()) { return; }
    if (!this.hasnext()) {
        this.setnext(this.S_CREATE_PANELS);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_WAIT1
*/
MainControl.prototype.S_WAIT1 = function (first) {
    "use strict";
    if (first) {
        this.m_limit_S_WAIT1 = Date.now() + 1000;
    }
    if (this.m_limit_S_WAIT1 > Date.now()) { return; }
    if (!this.hasnext()) {
        this.setnext(this.S_REMOVE_16);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_WAIT2
*/
MainControl.prototype.S_WAIT2 = function (first) {
    "use strict";
    if (first) {
        this.m_limit_S_WAIT2 = Date.now() + 1000;
    }
    if (this.m_limit_S_WAIT2 > Date.now()) { return; }
    if (!this.hasnext()) {
        this.setnext(this.S_SHOW_NEXT);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};


// [SYN-G-GEN OUTPUT END]

// == write your code ==
MainControl.prototype.check_completion = function () {
    "use strict";
    for(var i = 1; i <= 15; i++)
    {
        var item = G.mapinfo['n'+i];
        if (item.panel !== 'P' + zeroPadding(i,2) )
            return false;
    }
    return true;
};


/*
:psgg-macro-start

#mems=[[members]]

@setmapinfo=@@@
G.mapinfo.pus( [ index : {%0},  info : { up : {%1},  right: {%2}, down : {%3}, left : {%4} } ] );
@@@

:psgg-macro-end

*/