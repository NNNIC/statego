// == manager ==
var FEControl = function () {
    "use strict";

    this.curfunc   = null;
    this.nextfunc  = null;
    this.candfunc  = null;
    this.nowait    = false;
    
    // [SYN-G-GEN OUTPUT START]  indent(4) $/^S_./->#mems$
//  psggConverterLib.dll converted from FEControl.xlsx. 
    this.box = null;
    this.m_nextbutton = null;
    this.box = null;
    this.box = null;

    this.bDone = false;

    this.texture = null;
    this.texture = null;
    this.texture = null;
    this.texture = null;
    this.next_cb = null;
    this.modai_num =0;


    // [SYN-G-GEN OUTPUT END]
};

// [SYN-G-GEN OUTPUT START]  indent(0) $/^E_./$
//  psggConverterLib.dll converted from FEControl.xlsx. 
/*
    E_SHOW_NEXT
*/
FEControl.prototype.show_next = function(b) {
    this.m_nextbutton.visible = b;
};


// [SYN-G-GEN OUTPUT END]


//FEControl.prototype = new SMBASE();
FEControl.prototype.update = function () {
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
FEControl.prototype.checkstate = function (st) {
    "use strict";
    return this.candfunc === st;
};
FEControl.prototype.goto = function (st) {
    "use strict";
    this.nextfunc = st;
};
FEControl.prototype.setnext = function (st) {
    "use strict";
    this.candfunc = st;
};
FEControl.prototype.gonext = function () {
    "use strict";
    this.nextfunc = this.candfunc;
    this.candfunc = null;
};
FEControl.prototype.hasnext = function () {
    "use strict";
    return this.candfunc !== null;
};
FEControl.prototype.setnowait = function () {
    "use strict";
    this.nowait = true;
};
FEControl.prototype.start = function () {
    "use strict";
    this.goto(this.S_START);
};
FEControl.prototype.is_end = function () {
    "use strict";
    return this.checkstate(this.S_END);
};
// == yesno set ==
FEControl.yesno = false;
FEControl.prototype.br_yes = function (st) {
    "use strict";
    if (!this.hasnext(st)) {
        if (this.yesno) {
            this.setnext(st);
        }
    }
};
FEControl.prototype.br_no = function (st) {
    "use strict";
    if (!this.hasnext(st)) {
        if (!this.yesno) {
            this.setnext(st);
        }
    }
};

// [SYN-G-GEN OUTPUT START]  indent(0) $/^S_./$
//  psggConverterLib.dll converted from FEControl.xlsx. 
/*
    S_CREATE_BUTTON1
*/
FEControl.prototype.S_CREATE_BUTTON1 = function (first) {
    "use strict";
    if (first) {
        this.box = new THREE.Mesh(
            new THREE.PlaneGeometry(80, 80),
            new THREE.MeshBasicMaterial({map: this.texture})  // {map: texture}がキモ
        );
        this.box.name = "fade";
        G._2d_scene.add(this.box);
    }
    this.box.position.set(50, G.height - 50 -100, 0);
    if (!this.hasnext()) {
        this.setnext(this.S_LOAD_SHOT);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_CREATE_NEXTBUT
*/
FEControl.prototype.S_CREATE_NEXTBUT = function (first) {
    "use strict";
    if (first) {
        this.box = new THREE.Mesh(
                    new THREE.PlaneGeometry(160, 80),
                    new THREE.MeshBasicMaterial({map: this.texture})  // {map: texture}がキモ
        );
        this.box.name = "NEXT";
        G._2d_scene.add(this.box);
    }
    this.box.position.set(480, G.height - 570, 0);
    this.m_nextbutton = this.box;
    this.show_next(false);
    if (!this.hasnext()) {
        this.setnext(this.S_IS_CLICK1);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_CREATE_SHOTBUT
*/
FEControl.prototype.S_CREATE_SHOTBUT = function (first) {
    "use strict";
    if (first) {
        this.box = new THREE.Mesh(
            new THREE.PlaneGeometry(80, 80),
            new THREE.MeshBasicMaterial({map: this.texture})  // {map: texture}がキモ
        );
        this.box.name = "shot";
        G._2d_scene.add(this.box);
    }
    this.box.position.set(50, G.height - 150-100, 0);
    if (!this.hasnext()) {
        this.setnext(this.S_LOAD_Q);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_CREATE_SHOTBUT1
*/
FEControl.prototype.S_CREATE_SHOTBUT1 = function (first) {
    "use strict";
    if (first) {
        this.box = new THREE.Mesh(
            new THREE.PlaneGeometry(80, 80),
            new THREE.MeshBasicMaterial({map: this.texture})  // {map: texture}がキモ
        );
        this.box.name = "Q";
        G._2d_scene.add(this.box);
    }
    this.box.position.set(50, G.height - 250 - 100, 0);
    if (!this.hasnext()) {
        this.setnext(this.S_LOAD_NEXT);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_END
*/
FEControl.prototype.S_END = function (first) {
    "use strict";
    if (first) {
        console.log("END");
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_FADE_ONOFF
*/
FEControl.prototype.S_FADE_ONOFF = function (first) {
    "use strict";
    var fc  = G.fade_control;
    var fct = G.fadetxt_control;
    if (first) {
        console.log("S_FADE_ONOFF start");
        fc.m_in_or_out = !fc.m_in_or_out;
        fct.m_in_or_out = !fct.m_in_or_out;
        this.bDone = false;
        fc.m_reqcb = () => {  G.fe_control.bDone = true;  console.log("done!!!")  };
        fct.m_reqcb=()=>{};
    }
    if (!this.bDone) { return; }
    if (!this.hasnext()) {
        this.setnext(this.S_IS_CLICK1);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_IS_CLICK1
*/
FEControl.prototype.S_IS_CLICK1 = function (first) {
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
    S_LOAD_NEXT
*/
FEControl.prototype.S_LOAD_NEXT = function (first) {
    "use strict";
    if (first) {
        this.texture = null;
        console.log( "G.datapath=" + G.datapath);
        var self = this;
        G.loader.load( G.datapath + "NEXT.png", function(texture) {
            self.texture = texture;
            console.log("loaded texture!");
        });
    }
    if (this.texture==null) { return; }
    if (!this.hasnext()) {
        this.setnext(this.S_CREATE_NEXTBUT);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_LOAD_Q
*/
FEControl.prototype.S_LOAD_Q = function (first) {
    "use strict";
    if (first) {
        this.texture = null;
        console.log( "G.datapath=" + G.datapath);
        var self = this;
        G.loader.load( G.datapath + "Q.png", function(texture) {
            self.texture = texture;
            console.log("loaded texture!");
        });
    }
    if (this.texture==null) { return; }
    if (!this.hasnext()) {
        this.setnext(this.S_CREATE_SHOTBUT1);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_LOAD_SHOT
*/
FEControl.prototype.S_LOAD_SHOT = function (first) {
    "use strict";
    if (first) {
        this.texture = null;
        console.log( "G.datapath=" + G.datapath);
        var self = this;
        G.loader.load( G.datapath + "shot.png", function(texture) {
            self.texture = texture;
            console.log("loaded texture!");
        });
    }
    if (this.texture==null) { return; }
    if (!this.hasnext()) {
        this.setnext(this.S_CREATE_SHOTBUT);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_LOAD_TEXTURE
*/
FEControl.prototype.S_LOAD_TEXTURE = function (first) {
    "use strict";
    if (first) {
        this.texture = null;
        console.log( "G.datapath=" + G.datapath);
        var self = this;
        G.loader.load( G.datapath + "fade.png", function(texture) {
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
    S_NEXT
*/
FEControl.prototype.S_NEXT = function (first) {
    "use strict";
    if (first) {
        if (this.next_cb!=null) this.next_cb();
        console.log("NEXT BUTTON CLICKED!!!");
    }
    if (!this.hasnext()) {
        this.setnext(this.S_IS_CLICK1);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_QUESTION1
    出題
*/
FEControl.prototype.S_QUESTION1 = function (first) {
    "use strict";
    if (first) {
        var n = this.modai_num++;
        show_mondai(n);
    }
    if (!this.hasnext()) {
        this.setnext(this.S_IS_CLICK1);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_RAYCAST
*/
FEControl.prototype.S_RAYCAST = function (first) {
    "use strict";
    // レイキャスト = マウス位置からまっすぐに伸びる光線ベクトルを生成
    G.raycaster.setFromCamera(G.mouse, G._2d_camera);
    // その光線とぶつかったオブジェクトを得る
    const intersects = G.raycaster.intersectObjects(G._2d_scene.children);
    //if (intersects.length > 0){
    //    // ぶつかったオブジェクトに対してなんかする
    //   console.log("hit");
    //}
    //if (G.mouse_click) console.log("clicked!!");
    if (!(intersects.length > 0 && G.mouse_click)) { return; }
    var button = intersects[0].object.name;
    console.log("Clicked Obj = " + button );
    if (button == "fade") { this.setnext( this.S_FADE_ONOFF ); }
    else if (button == "shot") { this.setnext( this.S_SHOT ); }
    else if (button == "Q") { this.setnext( this.S_QUESTION1 ); }
    else if (button == "NEXT") { this.setnext( this.S_NEXT ); }
    else { this.setnext( this.S_IS_CLICK1 ); }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_SHOT
    問題用に現データを表示
*/
FEControl.prototype.S_SHOT = function (first) {
    "use strict";
    if (first) {
        console.log(G.mapinfo);
        var s = "";
        for(var  i = 1; i<=16; i++)
        {
            var n = G.mapinfo['n'+i].panel;
            if (s !== "") s+=",";
            s+= '"' + n + '"';
        }
        copyToClipboard(s);
        alert("clipboardへコピーしました\n" +   s.toString());
    }
    if (!this.hasnext()) {
        this.setnext(this.S_IS_CLICK1);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_START
*/
FEControl.prototype.S_START = function (first) {
    "use strict";
    if (!this.hasnext()) {
        this.setnext(this.S_LOAD_TEXTURE);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};


// [SYN-G-GEN OUTPUT END]

// == write your code ==


