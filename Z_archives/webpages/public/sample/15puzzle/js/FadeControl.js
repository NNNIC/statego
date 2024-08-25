// == manager ==
var FadeControl = function () {
    "use strict";

    this.curfunc   = null;
    this.nextfunc  = null;
    this.candfunc  = null;
    this.nowait    = false;
    
    // [SYN-G-GEN OUTPUT START]  indent(4) $/^S_./->#mems$
//  psggConverterLib.dll converted from FadeControl.xlsx. 
    this.material = null;



    this.FT = 1; //フェード経過秒
    this.m_reqcb = null;
    this.m_in_or_out = false;


    // [SYN-G-GEN OUTPUT END]
};

// [SYN-G-GEN OUTPUT START]  indent(0) $/^E_./$
//  psggConverterLib.dll converted from FadeControl.xlsx. 


// [SYN-G-GEN OUTPUT END]


//FadeControl.prototype = new SMBASE();
FadeControl.prototype.update = function () {
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
FadeControl.prototype.checkstate = function (st) {
    "use strict";
    return this.candfunc === st;
};
FadeControl.prototype.goto = function (st) {
    "use strict";
    this.nextfunc = st;
};
FadeControl.prototype.setnext = function (st) {
    "use strict";
    this.candfunc = st;
};
FadeControl.prototype.gonext = function () {
    "use strict";
    this.nextfunc = this.candfunc;
    this.candfunc = null;
};
FadeControl.prototype.hasnext = function () {
    "use strict";
    return this.candfunc !== null;
};
FadeControl.prototype.setnowait = function () {
    "use strict";
    this.nowait = true;
};
FadeControl.prototype.start = function () {
    "use strict";
    this.goto(this.S_START);
};
FadeControl.prototype.is_end = function () {
    "use strict";
    return this.checkstate(this.S_END);
};
// == yesno set ==
FadeControl.yesno = false;
FadeControl.prototype.br_yes = function (st) {
    "use strict";
    if (!this.hasnext(st)) {
        if (this.yesno) {
            this.setnext(st);
        }
    }
};
FadeControl.prototype.br_no = function (st) {
    "use strict";
    if (!this.hasnext(st)) {
        if (!this.yesno) {
            this.setnext(st);
        }
    }
};

// [SYN-G-GEN OUTPUT START]  indent(0) $/^S_./$
//  psggConverterLib.dll converted from FadeControl.xlsx. 
/*
    S_CB
*/
FadeControl.prototype.S_CB = function (first) {
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
    S_CREATE_FADE_OBJ1
    FADE OBJ作成
*/
FadeControl.prototype.S_CREATE_FADE_OBJ1 = function (first) {
    "use strict";
    if (first) {
        const geometry = new THREE.PlaneGeometry(100, 100);
        const material   = new THREE.MeshBasicMaterial({color: 0xFF000000,transparent: true, opacity: 1 } );
        var o               = new THREE.Mesh(geometry, material);
        this.material = material;
        G.fade_scene.add(o);
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
FadeControl.prototype.S_END = function (first) {
    "use strict";
    if (first) {
        console.log("END");
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_FADE_IN
*/
FadeControl.prototype.S_FADE_IN = function (first) {
    "use strict";
    var material = this.material;
    var frame_per_sec = 1000 / G.time_diff;
    var total_frames = this.FT * frame_per_sec;
    var diff = 1.0 / total_frames;
    if (first) {
        console.log("S_FADE_IN");
        material.opacity = 1;
    }
    var o = material.opacity;
    o -= diff;
    if (o < 0) o = 0;
    material.opacity = o;
    if (o!=0) { return; }
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
FadeControl.prototype.S_FADE_OUT = function (first) {
    "use strict";
    var material = this.material;
    var frame_per_sec = 1000 / G.time_diff;
    var total_frames = this.FT * frame_per_sec;
    var diff = 1.0 / total_frames;
    if (first) {
        console.log("S_FADE_OUT");
        material.opacity = 0;
    }
    var o = material.opacity;
    o += diff;
    if (o > 1) o = 1;
    material.opacity = o;
    if (o!=1) { return; }
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
FadeControl.prototype.S_INIT = function (first) {
    "use strict";
    if (first) {
        this.m_reqcb = null;
        this.m_in_or_out = true;
    }
    if (!this.hasnext()) {
        this.setnext(this.S_CREATE_FADE_OBJ1);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_LOG
*/
FadeControl.prototype.S_LOG = function (first) {
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
FadeControl.prototype.S_SELECT_FADE = function (first) {
    "use strict";
    if (this.m_in_or_out) { this.setnext( this.S_FADE_IN ); }
    else { this.setnext( this.S_FADE_OUT ); }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_START
*/
FadeControl.prototype.S_START = function (first) {
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
FadeControl.prototype.S_WAIT_REQ1 = function (first) {
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

