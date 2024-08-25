// == manager ==
var TouchControl = function () {
    "use strict";

    this.curfunc   = null;
    this.nextfunc  = null;
    this.candfunc  = null;
    this.nowait    = false;

    // [SYN-G-GEN OUTPUT START]  indent(4) $/^S_./->#mems$
//  psggConverterLib.dll converted from TouchControl.xlsx. 
    this.m_hitobj_name = null;

    this.m_savepos_x = 0;
    this.m_savepos_y = 0;
    this.m_xdiff = 0;
    this.m_ydiff = 0;
    this.TH=100;



    this.m_dir = 0; //0:none 1:up 2:right 3:down 4:left

    this.m_limit_S_WAIT = 0;


    this.m_reqcb = null;


    // [SYN-G-GEN OUTPUT END]
    
    
};

// [SYN-G-GEN OUTPUT START]  indent(0) $/^E_./$
//  psggConverterLib.dll converted from TouchControl.xlsx. 


// [SYN-G-GEN OUTPUT END]


//TouchControl.prototype = new SMBASE();
TouchControl.prototype.update = function () {
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
TouchControl.prototype.checkstate = function (st) {
    "use strict";
    return this.candfunc === st;
};
TouchControl.prototype.goto = function (st) {
    "use strict";
    this.nextfunc = st;
};
TouchControl.prototype.setnext = function (st) {
    "use strict";
    this.candfunc = st;
};
TouchControl.prototype.gonext = function () {
    "use strict";
    this.nextfunc = this.candfunc;
    this.candfunc = null;
};
TouchControl.prototype.hasnext = function () {
    "use strict";
    return this.candfunc !== null;
};
TouchControl.prototype.setnowait = function () {
    "use strict";
    this.nowait = true;
};
TouchControl.prototype.start = function () {
    "use strict";
    this.goto(this.S_START);
};
TouchControl.prototype.is_end = function () {
    "use strict";
    return this.checkstate(this.S_END);
};
// == yesno set ==
TouchControl.yesno = false;
TouchControl.prototype.br_yes = function (st) {
    "use strict";
    if (!this.hasnext(st)) {
        if (this.yesno) {
            this.setnext(st);
        }
    }
};
TouchControl.prototype.br_no = function (st) {
    "use strict";
    if (!this.hasnext(st)) {
        if (!this.yesno) {
            this.setnext(st);
        }
    }
};

// [SYN-G-GEN OUTPUT START]  indent(0) $/^S_./$
//  psggConverterLib.dll converted from TouchControl.xlsx. 
/*
    S_CHECK_MDOWN
    マウスダウン
*/
TouchControl.prototype.S_CHECK_MDOWN = function (first) {
    "use strict";
    if (first) {
        console.log("S_CHECK_MDOWN");
    }
    if (G.mouse_down === false) { return; }
    if (!this.hasnext()) {
        this.setnext(this.S_RAYCAST1);
    }
    if (this.hasnext()) {
        this.setnowait();
        this.gonext();
    }
};
/*
    S_END
*/
TouchControl.prototype.S_END = function (first) {
    "use strict";
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_IS_CLICK
*/
TouchControl.prototype.S_IS_CLICK = function (first) {
    "use strict";
    if (G.mouse_click===false) { return; }
    if (!this.hasnext()) {
        this.setnext(this.S_RAYCAST);
    }
    if (this.hasnext()) {
        this.setnowait();
        this.gonext();
    }
};
/*
    S_RAYCAST
*/
TouchControl.prototype.S_RAYCAST = function (first) {
    "use strict";
    // レイキャスト = マウス位置からまっすぐに伸びる光線ベクトルを生成
    G.raycaster.setFromCamera(G.mouse, G.camera);
    // その光線とぶつかったオブジェクトを得る
    const intersects = G.raycaster.intersectObjects(G.scene.children);
    //if (intersects.length > 0){
    //    // ぶつかったオブジェクトに対してなんかする
    //   console.log("hit");
    //}
    //if (G.mouse_click) console.log("clicked!!");
    if (!(intersects.length > 0 && G.mouse_click)) { return; }
    var name = intersects[0].object.name;
    console.log("Clicked Obj = " + name );
    if (!this.hasnext()) {
        this.setnext(this.S_IS_CLICK);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_RAYCAST1
*/
TouchControl.prototype.S_RAYCAST1 = function (first) {
    "use strict";
    if (first) {
        this.m_hitobj_name = null;
        console.log("S_RAYCAST1");
    }
    // レイキャスト = マウス位置からまっすぐに伸びる光線ベクトルを生成
    G.raycaster.setFromCamera(G.mouse, G.camera);
    // その光線とぶつかったオブジェクトを得る
    const intersects = G.raycaster.intersectObjects(G.scene.children);
    if (intersects.length > 0){
        // ぶつかったオブジェクトに対してなんかする
        this.m_hitobj_name = intersects[0].object.name;
        console.log("hit .. " + this.m_hitobj_name);
    }
    //alert("!#"+  this.m_hitobj_name );
    if (this.m_hitobj_name !== null) { this.setnext( this.S_SAVE_POS ); }
    else { this.setnext( this.S_WAIT_MOUSE_UP ); }
    if (this.hasnext()) {
        this.setnowait();
        this.gonext();
    }
};
/*
    S_REQCB
*/
TouchControl.prototype.S_REQCB = function (first) {
    "use strict";
    if (first) {
        this.m_reqcb();
        this.m_reqcb = null;
        var s = "";
        if (this.m_dir == 1) s = "UP";
        else if (this.m_dir == 2) s = "RIGHT";
        else if (this.m_dir == 3) s = "DOWN";
        else s = "LEFT";
        console.log(  s + " MOVED! ");
    }
    if (!this.hasnext()) {
        this.setnext(this.S_WAIT_REQCB);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_SAVE_POS
    開始位置保存&閾値移動
*/
TouchControl.prototype.S_SAVE_POS = function (first) {
    "use strict";
    if (first) {
        this.m_savepos_x = G.mouse.x;
        this.m_savepos_y = G.mouse.y;
        console.log("S_SAVE_POS  ");
        //console.log(G.mouse);
    }
    this.m_xdiff = (G.mouse.x - this.m_savepos_x) * G.width;
    this.m_ydiff = (G.mouse.y - this.m_savepos_y) * G.height;
    var xdiff_a = Math.abs(this.m_xdiff);
    var ydiff_a = Math.abs(this.m_ydiff);
    if (G.mouse_down === false) { this.setnext( this.S_CHECK_MDOWN ); }
    else if (ydiff_a > this.TH && this.m_ydiff > 0) { this.setnext( this.S_SET_UP ); }
    else if (xdiff_a > this.TH && this.m_xdiff > 0) { this.setnext( this.S_SET_RIGHT ); }
    else if (ydiff_a > this.TH && this.m_ydiff < 0) { this.setnext( this.S_SET_DOWN ); }
    else if (xdiff_a > this.TH && this.m_xdiff < 0) { this.setnext( this.S_SET_LEFT ); }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_SET_DOWN
*/
TouchControl.prototype.S_SET_DOWN = function (first) {
    "use strict";
    if (first) {
        this.m_dir = 3;
    }
    if (!this.hasnext()) {
        this.setnext(this.S_REQCB);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_SET_LEFT
*/
TouchControl.prototype.S_SET_LEFT = function (first) {
    "use strict";
    if (first) {
        this.m_dir = 4;
    }
    if (!this.hasnext()) {
        this.setnext(this.S_REQCB);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_SET_RIGHT
*/
TouchControl.prototype.S_SET_RIGHT = function (first) {
    "use strict";
    if (first) {
        this.m_dir = 2;
    }
    if (!this.hasnext()) {
        this.setnext(this.S_REQCB);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_SET_UP
*/
TouchControl.prototype.S_SET_UP = function (first) {
    "use strict";
    if (first) {
        this.m_dir = 1;
    }
    if (!this.hasnext()) {
        this.setnext(this.S_REQCB);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_START
*/
TouchControl.prototype.S_START = function (first) {
    "use strict";
    if (!this.hasnext()) {
        this.setnext(this.S_WAIT);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_WAIT
*/
TouchControl.prototype.S_WAIT = function (first) {
    "use strict";
    if (first) {
        this.m_limit_S_WAIT = Date.now() + 1000;
    }
    if (this.m_limit_S_WAIT > Date.now()) { return; }
    if (!this.hasnext()) {
        this.setnext(this.S_WAIT_REQCB);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_WAIT_MOUSE_UP
    念のためMOUSE UPまで待つ
*/
TouchControl.prototype.S_WAIT_MOUSE_UP = function (first) {
    "use strict";
    if (G.mouse_down === true) { return; }
    if (!this.hasnext()) {
        this.setnext(this.S_CHECK_MDOWN);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_WAIT_MOUSE_UP1
    念のためMOUSE UPまで待つ
*/
TouchControl.prototype.S_WAIT_MOUSE_UP1 = function (first) {
    "use strict";
    if (G.mouse_down === true) { return; }
    if (!this.hasnext()) {
        this.setnext(this.S_CHECK_MDOWN);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_WAIT_REQCB
*/
TouchControl.prototype.S_WAIT_REQCB = function (first) {
    "use strict";
    if (this.m_reqcb === null) { return; }
    if (!this.hasnext()) {
        this.setnext(this.S_WAIT_MOUSE_UP1);
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