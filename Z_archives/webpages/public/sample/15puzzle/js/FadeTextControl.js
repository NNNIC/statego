// == manager ==
var FadeTextControl = function () {
    "use strict";

    this.curfunc   = null;
    this.nextfunc  = null;
    this.candfunc  = null;
    this.nowait    = false;

    // [SYN-G-GEN OUTPUT START]  indent(4) $/^S_./->#mems$
//  psggConverterLib.dll converted from FadeTextControl.xlsx. 
    this.FT = 1; //フェード経過秒
    this.m_reqcb = null;
    this.m_in_or_out = false;

    this.opacity = 1;


    // [SYN-G-GEN OUTPUT END]
    
    
};

// [SYN-G-GEN OUTPUT START]  indent(0) $/^E_./$
//  psggConverterLib.dll converted from FadeTextControl.xlsx. 


// [SYN-G-GEN OUTPUT END]


//FadeTextControl.prototype = new SMBASE();
FadeTextControl.prototype.update = function () {
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
FadeTextControl.prototype.checkstate = function (st) {
    "use strict";
    return this.candfunc === st;
};
FadeTextControl.prototype.goto = function (st) {
    "use strict";
    this.nextfunc = st;
};
FadeTextControl.prototype.setnext = function (st) {
    "use strict";
    this.candfunc = st;
};
FadeTextControl.prototype.gonext = function () {
    "use strict";
    this.nextfunc = this.candfunc;
    this.candfunc = null;
};
FadeTextControl.prototype.hasnext = function () {
    "use strict";
    return this.candfunc !== null;
};
FadeTextControl.prototype.setnowait = function () {
    "use strict";
    this.nowait = true;
};
FadeTextControl.prototype.start = function () {
    "use strict";
    this.goto(this.S_START);
};
FadeTextControl.prototype.is_end = function () {
    "use strict";
    return this.checkstate(this.S_END);
};
// == yesno set ==
FadeTextControl.yesno = false;
FadeTextControl.prototype.br_yes = function (st) {
    "use strict";
    if (!this.hasnext(st)) {
        if (this.yesno) {
            this.setnext(st);
        }
    }
};
FadeTextControl.prototype.br_no = function (st) {
    "use strict";
    if (!this.hasnext(st)) {
        if (!this.yesno) {
            this.setnext(st);
        }
    }
};

// [SYN-G-GEN OUTPUT START]  indent(0) $/^S_./$
//  psggConverterLib.dll converted from FadeTextControl.xlsx. 
/*
    S_CB
*/
FadeTextControl.prototype.S_CB = function (first) {
    "use strict";
    if (first) {
        this.m_reqcb();
        this.m_reqcb=null;
    }
    if (!this.hasnext()) {
        this.setnext(this.S_WAIT_REQ1);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_END
*/
FadeTextControl.prototype.S_END = function (first) {
    "use strict";
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_FADE_IN
*/
FadeTextControl.prototype.S_FADE_IN = function (first) {
    "use strict";
    var frame_per_sec = 1000 / G.time_diff;
    var total_frames = this.FT * frame_per_sec;
    var diff = 1.0 / total_frames;
    if (first) {
        console.log("S_FADE_IN");
        this.opacity = 1;
    }
    this.opacity -= diff;
    if (this.opacity < 0) this.opacity = 0;
    $('.msg').css('opacity',this.opacity);
    if (this.opacity!=0) { return; }
    if (!this.hasnext()) {
        this.setnext(this.S_CB);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_FADE_OUT
*/
FadeTextControl.prototype.S_FADE_OUT = function (first) {
    "use strict";
    var frame_per_sec = 1000 / G.time_diff;
    var total_frames = this.FT * frame_per_sec;
    var diff = 1.0 / total_frames;
    if (first) {
        console.log("S_FADE_OUT");
        this.opacity = 0;
    }
    this.opacity += diff;
    if (this.opacity > 1) this.opacity = 1;
    $('.msg').css('opacity',this.opacity);
    if (this.opacity!=1) { return; }
    if (!this.hasnext()) {
        this.setnext(this.S_CB);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_INIT
    リクエスト待ち変数と初期化
*/
FadeTextControl.prototype.S_INIT = function (first) {
    "use strict";
    if (first) {
        this.m_reqcb = null;
        this.m_in_or_out = true;
    }
    if (!this.hasnext()) {
        this.setnext(this.S_WAIT_REQ1);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_LOG
*/
FadeTextControl.prototype.S_LOG = function (first) {
    "use strict";
    if (first) {
        console.log("Fade");
    }
    if (!this.hasnext()) {
        this.setnext(this.S_FADE_IN);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_SELECT_FADE
*/
FadeTextControl.prototype.S_SELECT_FADE = function (first) {
    "use strict";
    if (this.m_in_or_out===false) { this.setnext( this.S_FADE_IN ); }
    else { this.setnext( this.S_FADE_OUT ); }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_START
*/
FadeTextControl.prototype.S_START = function (first) {
    "use strict";
    if (!this.hasnext()) {
        this.setnext(this.S_INIT);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_WAIT_REQ1
    リクエスト待ち
*/
FadeTextControl.prototype.S_WAIT_REQ1 = function (first) {
    "use strict";
    //console.log("!");
    if (this.m_reqcb===null) { return; }
    if (!this.hasnext()) {
        this.setnext(this.S_SELECT_FADE);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};


// [SYN-G-GEN OUTPUT END]

// == write your code ==
/*
:psgg-macro-start

#mems=[[members]]

@setmapinfo=@@@
G.mapinfo.pus( [ index : {%0},  info : { up : {%1},  right: {%2}, down : {%3}, left : {%4} } ] );
@@@

:psgg-macro-end

*/