// == manager ==
var MapControl = function () {
    "use strict";

    this.curfunc   = null;
    this.nextfunc  = null;
    this.candfunc  = null;
    this.nowait    = false;

    // [SYN-G-GEN OUTPUT START]  indent(4) $/^S_./->#mems$
//  psggConverterLib.dll converted from MapControl.xlsx. 


    // [SYN-G-GEN OUTPUT END]
    
    
};

// [SYN-G-GEN OUTPUT START]  indent(0) $/^E_./$
//  psggConverterLib.dll converted from MapControl.xlsx. 
/*
    E_REMOVE_PANEL16
    P16パネルを除外
    可視域から外へ
*/
MapControl.prototype.remove_panel16 = function() {
    var children = G.scene.children;
    for(var i = 0; i< children.length; i++)
    {
        var obj = children[i];
        if (obj.name == 'P16')
        {
            //G.scene.remove(obj);
            obj.position.set(100000,100000);
        }
        G.mapinfo.n16.panel = '';
    }
};
/*
    E_REMOVE_PANEL17
    new state
*/
MapControl.prototype.log_map_info = function() {
    for(var key in G.mapinfo)
    {
        console.log(key);
        var i = G.mapinfo[key];
        console.log(i);
    }
};
/*
    E_SET_PANEL
*/
MapControl.prototype.set_panel = function (node, panel) {
console.log(node + "," + panel);
    G.mapinfo[node].panel = panel;
};
/*
    E_SET_PANELPOS
    位置を記録する。
*/
MapControl.prototype.set_panel_pos = function (node, x, y) {
    G.mapinfo[node].x = x;
    G.mapinfo[node].y = y;
};


// [SYN-G-GEN OUTPUT END]


//MapControl.prototype = new SMBASE();
MapControl.prototype.update = function () {
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
MapControl.prototype.checkstate = function (st) {
    "use strict";
    return this.candfunc === st;
};
MapControl.prototype.goto = function (st) {
    "use strict";
    this.nextfunc = st;
};
MapControl.prototype.setnext = function (st) {
    "use strict";
    this.candfunc = st;
};
MapControl.prototype.gonext = function () {
    "use strict";
    this.nextfunc = this.candfunc;
    this.candfunc = null;
};
MapControl.prototype.hasnext = function () {
    "use strict";
    return this.candfunc !== null;
};
MapControl.prototype.setnowait = function () {
    "use strict";
    this.nowait = true;
};
MapControl.prototype.start = function () {
    "use strict";
    this.goto(this.S_START);
};
MapControl.prototype.is_end = function () {
    "use strict";
    return this.checkstate(this.S_END);
};
// == yesno set ==
MapControl.yesno = false;
MapControl.prototype.br_yes = function (st) {
    "use strict";
    if (!this.hasnext(st)) {
        if (this.yesno) {
            this.setnext(st);
        }
    }
};
MapControl.prototype.br_no = function (st) {
    "use strict";
    if (!this.hasnext(st)) {
        if (!this.yesno) {
            this.setnext(st);
        }
    }
};

// [SYN-G-GEN OUTPUT START]  indent(0) $/^S_./$
//  psggConverterLib.dll converted from MapControl.xlsx. 
/*
    S_CHECK_VALUES1
*/
MapControl.prototype.S_CHECK_VALUES1 = function (first) {
    "use strict";
    if (first) {
        this.log_map_info();
    }
    if (!this.hasnext()) {
        this.setnext(this.S_MAKE_MONDAI);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_END
*/
MapControl.prototype.S_END = function (first) {
    "use strict";
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_MAKE_MONDAI
*/
MapControl.prototype.S_MAKE_MONDAI = function (first) {
    "use strict";
    if (first) {
        G.mondai = [
            [ "P01","P02","P03","P04","P05","P06","P07","P08","P09","P10","P11","P12","P13","P14","","P15"  ],
            [ "P10","P01","P06","P03","P08","P09","P04","P07","","P15","P14","P11","P05","P13","P02","P12"  ],
            [ "P01","P08","P02","P07","P09","P05","P04","","P13","P15","P10","P12","P14","P06","P11","P03"  ],
            [ "P11","P05","","P04","P02","P08","P03","P12","P06","P07","P15","P14","P13","P10","P09","P01"  ],
            [ "P03","P13","P11","P05","P10","P01","","P07","P15","P09","P06","P12","P04","P08","P14","P02"  ],
        ];
    }
    if (!this.hasnext()) {
        this.setnext(this.S_END);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_SET_MAPINFO
    マップ情報
    接続ノードデータ
    up,right,down,left .. 接続ノード
    panel : 現在のパネル
*/
MapControl.prototype.S_SET_MAPINFO = function (first) {
    "use strict";
    if (first) {
        G.mapinfo = {
        n1 : { up : 'n0',  right: 'n2', down : 'n5', left : 'n0', panel : 'p1' },
        n2 : { up : 'n0',  right: 'n3', down : 'n6', left : 'n1', panel : 'p2' },
        n3 : { up : 'n0',  right: 'n4', down : 'n7', left : 'n2', panel : 'p3' },
        n4 : { up : 'n0',  right: 'n0', down : 'n8', left : 'n3', panel : 'p4' },
        n5 : { up : 'n1',  right: 'n6', down : 'n9', left : 'n0', panel : 'p5' },
        n6 : { up : 'n2',  right: 'n7', down : 'n10', left : 'n5', panel : 'p6' },
        n7 : { up : 'n3',  right: 'n8', down : 'n11', left : 'n6', panel : 'p7' },
        n8 : { up : 'n4',  right: 'n0', down : 'n12', left : 'n7', panel : 'p8' },
        n9 : { up : 'n5',  right: 'n10', down : 'n13', left : 'n0', panel : 'p9' },
        n10 : { up : 'n6',  right: 'n11', down : 'n14', left : 'n9', panel : 'p10' },
        n11 : { up : 'n7',  right: 'n12', down : 'n15', left : 'n10', panel : 'p11' },
        n12 : { up : 'n8',  right: 'n0', down : 'n16', left : 'n11', panel : 'p12' },
        n13 : { up : 'n9',  right: 'n14', down : 'n0', left : 'n0', panel : 'p13' },
        n14 : { up : 'n10',  right: 'n15', down : 'n0', left : 'n13', panel : 'p14' },
        n15 : { up : 'n11',  right: 'n16', down : 'n0', left : 'n14', panel : 'p15' },
        n16 : { up : 'n12',  right: 'n0', down : 'n0', left : 'n15', panel : 'p16' },
        };
    }
    if (!this.hasnext()) {
        this.setnext(this.S_CHECK_VALUES1);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_START
*/
MapControl.prototype.S_START = function (first) {
    "use strict";
    if (!this.hasnext()) {
        this.setnext(this.S_SET_MAPINFO);
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
n{%0} : { up : 'n{%1}',  right: 'n{%2}', down : 'n{%3}', left : 'n{%4}', panel : 'p{%0}' },
@@@

@setmondai=@@@
[ {%0},{%1},{%2},{%3},{%4},{%5},{%6},{%7},{%8},{%9},{%10},{%11},{%12},{%13},{%14},{%15}  ],
@@@

:psgg-macro-end

*/