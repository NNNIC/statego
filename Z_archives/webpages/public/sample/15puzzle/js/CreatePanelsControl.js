// == manager ==
var CreatePanelsControl = function () {
    "use strict";

    this.curfunc   = null;
    this.nextfunc  = null;
    this.candfunc  = null;
    this.nowait    = false;
    
    // [SYN-G-GEN OUTPUT START]  indent(4) $/^S_./->#mems$
//  psggConverterLib.dll converted from CreatePanelsControl.xlsx. 
    this.box = null;
    this.box = null;
    this.box = null;
    this.box = null;

    this.item = null;
    this.I = 0;


    this.texture = null;
    this.texture = null;
    this.texture = null;
    this.texture = null;
    this.itemdata = [];


    this.touchtex=null;

    this.m_reqcb = null;


    // [SYN-G-GEN OUTPUT END]
};

// [SYN-G-GEN OUTPUT START]  indent(0) $/^E_./$
//  psggConverterLib.dll converted from CreatePanelsControl.xlsx. 


// [SYN-G-GEN OUTPUT END]


//CreatePanelsControl.prototype = new SMBASE();
CreatePanelsControl.prototype.update = function () {
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
CreatePanelsControl.prototype.checkstate = function (st) {
    "use strict";
    return this.curfunc === st;
};
CreatePanelsControl.prototype.goto = function (st) {
    "use strict";
    this.nextfunc = st;
};
CreatePanelsControl.prototype.setnext = function (st) {
    "use strict";
    this.candfunc = st;
};
CreatePanelsControl.prototype.gonext = function () {
    "use strict";
    this.nextfunc = this.candfunc;
    this.candfunc = null;
};
CreatePanelsControl.prototype.hasnext = function () {
    "use strict";
    return this.candfunc !== null;
};
CreatePanelsControl.prototype.setnowait = function () {
    "use strict";
    this.nowait = true;
};
CreatePanelsControl.prototype.start = function () {
    "use strict";
    this.goto(this.S_START);
};
CreatePanelsControl.prototype.is_end = function () {
    "use strict";
    return this.checkstate(this.S_END);
};

CreatePanelsControl.prototype.run = function () {
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
CreatePanelsControl.yesno = false;
CreatePanelsControl.prototype.br_yes = function (st) {
    "use strict";
    if (!this.hasnext(st)) {
        if (this.yesno) {
            this.setnext(st);
        }
    }
};
CreatePanelsControl.prototype.br_no = function (st) {
    "use strict";
    if (!this.hasnext(st)) {
        if (!this.yesno) {
            this.setnext(st);
        }
    }
};

// [SYN-G-GEN OUTPUT START]  indent(0) $/^S_./$
//  psggConverterLib.dll converted from CreatePanelsControl.xlsx. 
/*
    S_CB
*/
CreatePanelsControl.prototype.S_CB = function (first) {
    "use strict";
    if (first) {
        this.m_reqcb();
        this.m_reqcb=null;
    }
    if (!this.hasnext()) {
        this.setnext(this.S_WAIT_REQ);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_CREATE_BACKBOARD
*/
CreatePanelsControl.prototype.S_CREATE_BACKBOARD = function (first) {
    "use strict";
    if (first) {
        this.box = new THREE.Mesh(
            new THREE.PlaneGeometry(512, 512),
            new THREE.MeshBasicMaterial({map: this.texture})  // {map: texture}がキモ
        );
        this.box.position.set(0,0,-2);
        this.box.name = "backboard";
        G.scene.add(this.box);
    }
    if (!this.hasnext()) {
        this.setnext(this.S_CB);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_CREATE_BUTTON1
*/
CreatePanelsControl.prototype.S_CREATE_BUTTON1 = function (first) {
    "use strict";
    if (first) {
        this.box = new THREE.Mesh(
            new THREE.PlaneGeometry(128, 128),
            new THREE.MeshBasicMaterial({map: this.texture})  // {map: texture}がキモ
        );
        //this.box.position.set(50, G.height - 50, 0);
        this.box.name = "P1.png";
        G.scene.add(this.box);
    }
    if (!this.hasnext()) {
        this.setnext(this.S_END);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_CREATE_PANELS
*/
CreatePanelsControl.prototype.S_CREATE_PANELS = function (first) {
    "use strict";
    if (first) {
        this.box = new THREE.Mesh(
            new THREE.PlaneGeometry(128, 128),
            new THREE.MeshBasicMaterial({map: this.texture})  // {map: texture}がキモ
        );
        this.box.position.set(this.item.x * 128, this.item.y * 128, 0);
        this.box.name = this.item.name;
        G.scene.add(this.box);
    }
    if (!this.hasnext()) {
        this.setnext(this.S_CREATE_TOUCHCOL);
    }
    if (this.hasnext()) {
        this.setnowait();
        this.gonext();
    }
};
/*
    S_CREATE_TOUCHCOL
    タッチ検索用パネル
*/
CreatePanelsControl.prototype.S_CREATE_TOUCHCOL = function (first) {
    "use strict";
    if (first) {
        this.box = new THREE.Mesh(
            new THREE.PlaneGeometry(128, 128),
            new THREE.MeshBasicMaterial({map: this.touchtex, transparent: true, opacity: 0   })  // {map: texture}がキモ
        );
        this.box.position.set(this.item.x * 128, this.item.y * 128, 1);
        this.box.name = 'n' + this.item.index;
        G.scene.add(this.box);
    }
    if (!this.hasnext()) {
        this.setnext(this.S_SET_MAPINFO);
    }
    if (this.hasnext()) {
        this.setnowait();
        this.gonext();
    }
};
/*
    S_END
*/
CreatePanelsControl.prototype.S_END = function (first) {
    "use strict";
    if (first) {
        console.log("END");
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_FORINIT
*/
CreatePanelsControl.prototype.S_FORINIT = function (first) {
    "use strict";
    if (first) {
        this.I = 0;
    }
    if (!this.hasnext()) {
        this.setnext(this.S_GET_ITEM);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_FORNEXT
*/
CreatePanelsControl.prototype.S_FORNEXT = function (first) {
    "use strict";
    if (first) {
        this.I++;
    }
    if (this.I < 16) { this.setnext( this.S_GET_ITEM ); }
    else { this.setnext( this.S_LOAD_BBTEX ); }
    if (this.hasnext()) {
        this.setnowait();
        this.gonext();
    }
};
/*
    S_GET_ITEM
*/
CreatePanelsControl.prototype.S_GET_ITEM = function (first) {
    "use strict";
    if (first) {
        this.item = this.itemlist[this.I]
        console.log(this.I + "[index:" + this.item.index +" file:" + this.item.file +" xy:(" + this.item.x +"," + this.item.y  +")]");
    }
    if (!this.hasnext()) {
        this.setnext(this.S_LOADTEX);
    }
    if (this.hasnext()) {
        this.setnowait();
        this.gonext();
    }
};
/*
    S_LOAD_BBTEX
    背面ボードテクスチャ
*/
CreatePanelsControl.prototype.S_LOAD_BBTEX = function (first) {
    "use strict";
    if (first) {
        this.texture = null;
        console.log( "G.datapath=" + G.datapath);
        var self = this;
        G.loader.load( G.datapath + "backboard.png", function(texture) {
            self.texture = texture;
            console.log("loaded texture!");
        });
    }
    if (this.texture==null) { return; }
    if (!this.hasnext()) {
        this.setnext(this.S_CREATE_BACKBOARD);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_LOAD_TEXTUREX
*/
CreatePanelsControl.prototype.S_LOAD_TEXTUREX = function (first) {
    "use strict";
    if (first) {
        this.texture = null;
        console.log( "G.datapath=" + G.datapath);
        var self = this;
        G.loader.load( G.datapath + "P01.png", function(texture) {
            self.texture = texture;
            console.log("loaded texture!");
        });
    }
    if (this.texture==null) { return; }
    if (!this.hasnext()) {
        this.setnext(this.S_CREATE_BUTTON1);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_LOAD_TOCHTEX
    タッチ用テクスチャ
    当たり判定用
*/
CreatePanelsControl.prototype.S_LOAD_TOCHTEX = function (first) {
    "use strict";
    if (first) {
        this.texture = null;
        console.log( "G.datapath=" + G.datapath);
        var self = this;
        G.loader.load( G.datapath + "Nxx.png", function(texture) {
            self.texture = texture;
            console.log("loaded texture!");
        });
    }
    if (this.texture==null) { return; }
    this.touchtex = this.texture;
    if (!this.hasnext()) {
        this.setnext(this.S_SET_DATA);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_LOADTEX
*/
CreatePanelsControl.prototype.S_LOADTEX = function (first) {
    "use strict";
    if (first) {
        this.texture = null;
        console.log( "G.datapath=" + G.datapath);
        var self = this;
        G.loader.load( G.datapath + this.item.file, function(texture) {
            self.texture = texture;
            console.log("loaded texture!");
        });
    }
    if (this.texture==null) { return; }
    if (!this.hasnext()) {
        this.setnext(this.S_CREATE_PANELS);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_SET_DATA
*/
CreatePanelsControl.prototype.S_SET_DATA = function (first) {
    "use strict";
    if (first) {
        this.itemlist = [
             { "index":1, "file":"P01.png", "x":-1.5, "y":+1.5, "name":"P01" },
             { "index":2, "file":"P02.png", "x":-0.5, "y":+1.5, "name":"P02" },
             { "index":3, "file":"P03.png", "x":+0.5, "y":+1.5, "name":"P03" },
             { "index":4, "file":"P04.png", "x":+1.5, "y":+1.5, "name":"P04" },
             { "index":5, "file":"P05.png", "x":-1.5, "y":+0.5, "name":"P05" },
             { "index":6, "file":"P06.png", "x":-0.5, "y":+0.5, "name":"P06" },
             { "index":7, "file":"P07.png", "x":+0.5, "y":+0.5, "name":"P07" },
             { "index":8, "file":"P08.png", "x":+1.5, "y":+0.5, "name":"P08" },
             { "index":9, "file":"P09.png", "x":-1.5, "y":-0.5, "name":"P09" },
             { "index":10, "file":"P10.png", "x":-0.5, "y":-0.5, "name":"P10" },
             { "index":11, "file":"P11.png", "x":+0.5, "y":-0.5, "name":"P11" },
             { "index":12, "file":"P12.png", "x":+1.5, "y":-0.5, "name":"P12" },
             { "index":13, "file":"P13.png", "x":-1.5, "y":-1.5, "name":"P13" },
             { "index":14, "file":"P14.png", "x":-0.5, "y":-1.5, "name":"P14" },
             { "index":15, "file":"P15.png", "x":+0.5, "y":-1.5, "name":"P15" },
             { "index":16, "file":"P16.png", "x":+1.5, "y":-1.5, "name":"P16" },
        ];
    }
    if (!this.hasnext()) {
        this.setnext(this.S_FORINIT);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_SET_MAPINFO
*/
CreatePanelsControl.prototype.S_SET_MAPINFO = function (first) {
    "use strict";
    if (first) {
        var n_name = 'n' + this.item.index;
        G.map_control.set_panel( n_name, this.item.name);
        G.map_control.set_panel_pos(n_name,this.item.x * 128, this.item.y * 128);
    }
    if (!this.hasnext()) {
        this.setnext(this.S_FORNEXT);
    }
    if (this.hasnext()) {
        this.setnowait();
        this.gonext();
    }
};
/*
    S_START
*/
CreatePanelsControl.prototype.S_START = function (first) {
    "use strict";
    if (!this.hasnext()) {
        this.setnext(this.S_WAIT_REQ1);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_VAR_TOUCHTEX
    タッチテクスチャ格納用
*/
CreatePanelsControl.prototype.S_VAR_TOUCHTEX = function (first) {
    "use strict";
    if (!this.hasnext()) {
        this.setnext(this.S_LOAD_TOCHTEX);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_WAIT_REQ
*/
CreatePanelsControl.prototype.S_WAIT_REQ = function (first) {
    "use strict";
    if (this.m_reqcb==null) { return; }
    if (!this.hasnext()) {
        this.setnext(this.S_VAR_TOUCHTEX);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_WAIT_REQ1
*/
CreatePanelsControl.prototype.S_WAIT_REQ1 = function (first) {
    "use strict";
    if (first) {
        this.m_reqcb = null;
    }
    if (!this.hasnext()) {
        this.setnext(this.S_WAIT_REQ);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};


// [SYN-G-GEN OUTPUT END]

// == write your code ==

/*
:psgg-macro-start

@setitem=@@@
{ "index":{%0}, "file":"{%1}.png", "x":{%2}, "y":{%3}, "name":"{%1}" },
@@@


:psgg-macro-end
*/