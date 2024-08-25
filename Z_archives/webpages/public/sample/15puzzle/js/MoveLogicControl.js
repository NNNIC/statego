// == manager ==
var MoveLogicControl = function () {
    "use strict";

    this.curfunc   = null;
    this.nextfunc  = null;
    this.candfunc  = null;
    this.nowait    = false;

    // [SYN-G-GEN OUTPUT START]  indent(4) $/^S_./->#mems$
//  psggConverterLib.dll converted from MoveLogicControl.xlsx. 
    this.m_hitmap = null;
    this.m_aclist = [];
    this.m_nextmap = null;
    this.m_dirstr = null;
    this.m_ok2move = false;

    this.m_hitobj_name = null;
    this.m_dir = 0;


    // [SYN-G-GEN OUTPUT END]
    
    
};

// [SYN-G-GEN OUTPUT START]  indent(0) $/^E_./$
//  psggConverterLib.dll converted from MoveLogicControl.xlsx. 


// [SYN-G-GEN OUTPUT END]


//MoveLogicControl.prototype = new SMBASE();
MoveLogicControl.prototype.update = function () {
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
MoveLogicControl.prototype.checkstate = function (st) {
    "use strict";
    return this.curfunc === st;
};
MoveLogicControl.prototype.goto = function (st) {
    "use strict";
    this.nextfunc = st;
};
MoveLogicControl.prototype.setnext = function (st) {
    "use strict";
    this.candfunc = st;
};
MoveLogicControl.prototype.gonext = function () {
    "use strict";
    this.nextfunc = this.candfunc;
    this.candfunc = null;
};
MoveLogicControl.prototype.hasnext = function () {
    "use strict";
    return this.candfunc !== null;
};
MoveLogicControl.prototype.setnowait = function () {
    "use strict";
    this.nowait = true;
};
MoveLogicControl.prototype.start = function () {
    "use strict";
    this.goto(this.S_START);
};
MoveLogicControl.prototype.is_end = function () {
    "use strict";
    return this.checkstate(this.S_END);
};
// == yesno set ==
MoveLogicControl.yesno = false;
MoveLogicControl.prototype.br_yes = function (st) {
    "use strict";
    if (!this.hasnext(st)) {
        if (this.yesno) {
            this.setnext(st);
        }
    }
};
MoveLogicControl.prototype.br_no = function (st) {
    "use strict";
    if (!this.hasnext(st)) {
        if (!this.yesno) {
            this.setnext(st);
        }
    }
};
MoveLogicControl.prototype.run = function() {
    this.start();
    do {
        this.update();
    } while(this.is_end()===false);
}

// [SYN-G-GEN OUTPUT START]  indent(0) $/^S_./$
//  psggConverterLib.dll converted from MoveLogicControl.xlsx. 
/*
    S_0003
    タッチ場所のマップ情報を得る。
    G.mapinfo
*/
MoveLogicControl.prototype.S_0003 = function (first) {
    "use strict";
    if (first) {
        this.m_hitmap = G.mapinfo[this.m_hitobj_name];
    }
    if (!this.hasnext()) {
        this.setnext(this.S_0004);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_0004
    初期設定
    m_nextmap に m_hitmapを入れる
*/
MoveLogicControl.prototype.S_0004 = function (first) {
    "use strict";
    if (first) {
        this.m_dirstr = D.dir[this.m_dir];
        this.m_aclist = [];
        this.m_nextmap = this.m_hitmap;
        this.m_ok2move = false;
    }
    if (!this.hasnext()) {
        this.setnext(this.S_0005);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_0005
    m_nextmapをm_aclistに入れる。
    m_nextmapの方向データを取得する。
    方向データのnextmapを得る。
    nextmap内のpanelが無いときは、移動可となり終了
    nextmapが0の時は移動不可となり終了
    以外は、繰り返す。
*/
MoveLogicControl.prototype.S_0005 = function (first) {
    "use strict";
    console.log(this.m_dirstr);
    console.log(this.m_nextmap);
    var next_n = this.m_nextmap[this.m_dirstr];
    this.m_aclist.push(this.m_nextmap);
    this.m_nextmap = null;
    if (next_n!="n0") {
        var next_n_name = next_n;
        var nextmap = G.mapinfo[next_n_name];
        if (nextmap.panel === "")
        {
            this.m_aclist.push(nextmap);//ラスト
            this.m_ok2move = true;
        }
        else
        {
            this.m_nextmap = nextmap;
        }
    }
    if (this.m_nextmap !== null) { return; }
    if (this.m_ok2move) { this.setnext( this.S_MOVE ); }
    else { this.setnext( this.S_END ); }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_CHECK_INPUT
    入力の確認
    タッチ場所 nXX
    方向      TRDL
*/
MoveLogicControl.prototype.S_CHECK_INPUT = function (first) {
    "use strict";
    if (!this.hasnext()) {
        this.setnext(this.S_0003);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_END
*/
MoveLogicControl.prototype.S_END = function (first) {
    "use strict";
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_MOVE
    移動
*/
MoveLogicControl.prototype.S_MOVE = function (first) {
    "use strict";
    var aclist = this.m_aclist;
    for(var i = aclist.length - 2; i >=0 ; i--) //後ろから
    {
        var smap = aclist[i];
        var dmap = aclist[i+1];
        var panel = smap.panel;
        smap.panel = "";
        dmap.panel = panel;
        move_obj(panel, dmap.x, dmap.y);
    }
    if (!this.hasnext()) {
        this.setnext(this.S_END);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_START
*/
MoveLogicControl.prototype.S_START = function (first) {
    "use strict";
    if (!this.hasnext()) {
        this.setnext(this.S_CHECK_INPUT);
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