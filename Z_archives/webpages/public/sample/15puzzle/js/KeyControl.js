// == manager ==
var KeyControl = function () {
    "use strict";

    this.curfunc   = null;
    this.nextfunc  = null;
    this.candfunc  = null;
    this.nowait    = false;

    // [SYN-G-GEN OUTPUT START]  indent(4) $/^S_./->#mems$
//  psggConverterLib.dll converted from KeyControl.xlsx. 


    // [SYN-G-GEN OUTPUT END]
    
    
};

// [SYN-G-GEN OUTPUT START]  indent(0) $/^E_./$
//  psggConverterLib.dll converted from KeyControl.xlsx. 


// [SYN-G-GEN OUTPUT END]


//KeyControl.prototype = new SMBASE();
KeyControl.prototype.update = function () {
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
KeyControl.prototype.checkstate = function (st) {
    "use strict";
    return this.candfunc === st;
};
KeyControl.prototype.goto = function (st) {
    "use strict";
    this.nextfunc = st;
};
KeyControl.prototype.setnext = function (st) {
    "use strict";
    this.candfunc = st;
};
KeyControl.prototype.gonext = function () {
    "use strict";
    this.nextfunc = this.candfunc;
    this.candfunc = null;
};
KeyControl.prototype.hasnext = function () {
    "use strict";
    return this.candfunc !== null;
};
KeyControl.prototype.setnowait = function () {
    "use strict";
    this.nowait = true;
};
KeyControl.prototype.start = function () {
    "use strict";
    this.goto(this.S_START);
};
KeyControl.prototype.is_end = function () {
    "use strict";
    return this.checkstate(this.S_END);
};
// == yesno set ==
KeyControl.yesno = false;
KeyControl.prototype.br_yes = function (st) {
    "use strict";
    if (!this.hasnext(st)) {
        if (this.yesno) {
            this.setnext(st);
        }
    }
};
KeyControl.prototype.br_no = function (st) {
    "use strict";
    if (!this.hasnext(st)) {
        if (!this.yesno) {
            this.setnext(st);
        }
    }
};

// [SYN-G-GEN OUTPUT START]  indent(0) $/^S_./$
//  psggConverterLib.dll converted from KeyControl.xlsx. 
/*
    S_DEBUG_ON_OFF
*/
KeyControl.prototype.S_DEBUG_ON_OFF = function (first) {
    "use strict";
    if (first) {
        console.log("S_DEBUG_ON_OFF");
        this.move_button('fade');
        this.move_button('shot');
        this.move_button('Q');
    }
    if (!this.hasnext()) {
        this.setnext(this.S_WAIT_KEY);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_END
*/
KeyControl.prototype.S_END = function (first) {
    "use strict";
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_START
*/
KeyControl.prototype.S_START = function (first) {
    "use strict";
    if (!this.hasnext()) {
        this.setnext(this.S_WAIT_KEY);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_WAIT_KEY
*/
KeyControl.prototype.S_WAIT_KEY = function (first) {
    "use strict";
    if (G.key_down === '') { return; }
    if (G.key_down == 'D') { this.setnext( this.S_DEBUG_ON_OFF ); }
    if (this.hasnext()) {
        this.gonext();
    }
};


// [SYN-G-GEN OUTPUT END]

// == write your code ==

KeyControl.prototype.move_button = function (name) {
    var obj = find_obj_in_2dscene(name);
    if (obj === null) return;
    console.log(obj.position);
    obj.position.x = - obj.position.x;
    
};




/*
:psgg-macro-start

#mems=[[members]]

@setmapinfo=@@@
G.mapinfo.pus( [ index : {%0},  info : { up : {%1},  right: {%2}, down : {%3}, left : {%4} } ] );
@@@

:psgg-macro-end

*/