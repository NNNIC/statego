// == manager ==
var ResizeControl = function () {
    "use strict";

    this.curfunc   = null;
    this.nextfunc  = null;
    this.candfunc  = null;
    this.nowait    = false;
    
    // [SYN-G-GEN OUTPUT START]  indent(4) $/^S_./->#mems$
//  psggConverterLib.dll converted from ResizeControl.xlsx. 
    this.witdh  = 960; //現在の幅　　入力
    this.height = 640; //現在の高さ　入力
    this.fixwidth= 960; //修正幅    出力
    this.fixheight=640; //修正高さ　出力


    // [SYN-G-GEN OUTPUT END]
};

// [SYN-G-GEN OUTPUT START]  indent(0) $/^E_./$
//  psggConverterLib.dll converted from ResizeControl.xlsx. 
/*
    E_DEFINES
*/
ResizeControl.WIDTH =  960;
ResizeControl.HEIGHT= 640;
ResizeControl.WPH    = ResizeControl.WIDTH / ResizeControl.HEIGHT;


// [SYN-G-GEN OUTPUT END]


//ResizeControl.prototype = new SMBASE();
ResizeControl.prototype.update = function () {
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
ResizeControl.prototype.checkstate = function (st) {
    "use strict";
    return this.curfunc === st;
};
ResizeControl.prototype.goto = function (st) {
    "use strict";
    this.nextfunc = st;
};
ResizeControl.prototype.setnext = function (st) {
    "use strict";
    this.candfunc = st;
};
ResizeControl.prototype.gonext = function () {
    "use strict";
    this.nextfunc = this.candfunc;
    this.candfunc = null;
};
ResizeControl.prototype.hasnext = function () {
    "use strict";
    return this.candfunc !== null;
};
ResizeControl.prototype.setnowait = function () {
    "use strict";
    this.nowait = true;
};
ResizeControl.prototype.start = function () {
    "use strict";
    this.goto(this.S_START);
};
ResizeControl.prototype.is_end = function () {
    "use strict";
    return this.checkstate(this.S_END);
};

ResizeControl.prototype.run = function () {
    "use strict";
    this.start();
    for(var loop = 0; loop<=1000; loop++) {
        if (loop==1000) {
            console.log("TOO MANY LOOPS");        
            break;
        }
        this.update();
        if (this.is_end())
        {
            console.log("End of loop");
            break;
        }
    }
};


// == yesno set ==
ResizeControl.yesno = false;
ResizeControl.prototype.br_yes = function (st) {
    "use strict";
    if (!this.hasnext(st)) {
        if (this.yesno) {
            this.setnext(st);
        }
    }
};
ResizeControl.prototype.br_no = function (st) {
    "use strict";
    if (!this.hasnext(st)) {
        if (!this.yesno) {
            this.setnext(st);
        }
    }
};

// [SYN-G-GEN OUTPUT START]  indent(0) $/^S_./$
//  psggConverterLib.dll converted from ResizeControl.xlsx. 
/*
    S_CHANGE_HEIGHT
    高さを修正する
*/
ResizeControl.prototype.S_CHANGE_HEIGHT = function (first) {
    "use strict";
    if (first) {
        this.fixheight = this.width / ResizeControl.WIDTH * ResizeControl.HEIGHT;
    }
    if (!this.hasnext()) {
        this.setnext(this.S_CHANGE_STYLE);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_CHANGE_STYLE
*/
ResizeControl.prototype.S_CHANGE_STYLE = function (first) {
    "use strict";
    if (first) {
        var s = Math.round(40 * this.fixwidth / 960);
        $('.msg').css('font-size', s.toString() + 'px');
        $('.msg').css('line-height', s.toString() + 'px');
        var t = Math.round(60 * this.fixwidth / 960);
        $('.msg').css('top', t.toString() + 'px');
        $('.msg').css('width', this.fixwidth.toString() + 'px');
        $('.msg').css('text-align', 'center');
    }
    if (!this.hasnext()) {
        this.setnext(this.S_END);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_CHANGE_WIDTH
    幅を修正する
*/
ResizeControl.prototype.S_CHANGE_WIDTH = function (first) {
    "use strict";
    if (first) {
        this.fixwidth = this.height / ResizeControl.HEIGHT * ResizeControl.WIDTH;
    }
    if (!this.hasnext()) {
        this.setnext(this.S_CHANGE_STYLE);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_CHECK_WPH
    横縦比の確認
*/
ResizeControl.prototype.S_CHECK_WPH = function (first) {
    "use strict";
    var cur_wph = this.width / this.height;
    if (cur_wph > ResizeControl.WPH) { this.setnext( this.S_CHANGE_WIDTH ); }
    else { this.setnext( this.S_CHANGE_HEIGHT ); }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_END
*/
ResizeControl.prototype.S_END = function (first) {
    "use strict";
    if (first) {
        console.log("END");
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_INIT
*/
ResizeControl.prototype.S_INIT = function (first) {
    "use strict";
    if (first) {
        this.fixwidth = this.width;
        this.fixheight= this.height;
    }
    if (!this.hasnext()) {
        this.setnext(this.S_CHECK_WPH);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_START
*/
ResizeControl.prototype.S_START = function (first) {
    "use strict";
    if (!this.hasnext()) {
        this.setnext(this.S_INIT);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};


// [SYN-G-GEN OUTPUT END]

// == write your code ==

