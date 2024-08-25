// == manager ==
var TitleControl = function () {
    "use strict";

    this.curfunc   = null;
    this.nextfunc  = null;
    this.candfunc  = null;
    this.nowait    = false;
    
    // [SYN-G-GEN OUTPUT START]  indent(4) $/^S_./->#mems$
//  psggConverterLib.dll converted from TitleControl.xlsx. 
    this.m_win = null;
    this.m_you = null;






    this.m_reqcb = null;


    // [SYN-G-GEN OUTPUT END]
};

// [SYN-G-GEN OUTPUT START]  indent(0) $/^E_./$
//  psggConverterLib.dll converted from TitleControl.xlsx. 
/*
    E_0001
*/
TitleControl.prototype.show_youwin = function (b) {
    this.m_you.visible = b;
    this.m_win.visible = b;
};


// [SYN-G-GEN OUTPUT END]


//TitleControl.prototype = new SMBASE();
TitleControl.prototype.update = function () {
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
TitleControl.prototype.checkstate = function (st) {
    "use strict";
    return this.curfunc === st;
};
TitleControl.prototype.goto = function (st) {
    "use strict";
    this.nextfunc = st;
};
TitleControl.prototype.setnext = function (st) {
    "use strict";
    this.candfunc = st;
};
TitleControl.prototype.gonext = function () {
    "use strict";
    this.nextfunc = this.candfunc;
    this.candfunc = null;
};
TitleControl.prototype.hasnext = function () {
    "use strict";
    return this.candfunc !== null;
};
TitleControl.prototype.setnowait = function () {
    "use strict";
    this.nowait = true;
};
TitleControl.prototype.start = function () {
    "use strict";
    this.goto(this.S_START);
};
TitleControl.prototype.is_end = function () {
    "use strict";
    return this.checkstate(this.S_END);
};

TitleControl.prototype.run = function () {
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
TitleControl.yesno = false;
TitleControl.prototype.br_yes = function (st) {
    "use strict";
    if (!this.hasnext(st)) {
        if (this.yesno) {
            this.setnext(st);
        }
    }
};
TitleControl.prototype.br_no = function (st) {
    "use strict";
    if (!this.hasnext(st)) {
        if (!this.yesno) {
            this.setnext(st);
        }
    }
};

// [SYN-G-GEN OUTPUT START]  indent(0) $/^S_./$
//  psggConverterLib.dll converted from TitleControl.xlsx. 
/*
    S_CREATE_WIN
*/
TitleControl.prototype.S_CREATE_WIN = function (first) {
    "use strict";
    if (first) {
        //正面マテリアルの生成
         var materialFront = new THREE.MeshBasicMaterial( { color: 0xff0000 } );
        //側面マテリアルの生成
         var materialSide = new THREE.MeshBasicMaterial( { color: 0x000088 } );
         var materialArray = [ materialFront, materialSide ];
        //テキスト
         var textGeom = new THREE.TextGeometry( "WIN",
                {
                        size: 500, height: 80, curveSegments: 3,
                        font: this.font,
                        bevelThickness: 1, bevelSize: 2, bevelEnabled: true,
                        material: 0, extrudeMaterial: 1
                });
        var textMaterial = new THREE.MeshFaceMaterial(materialArray);
        var textMesh = new THREE.Mesh(textGeom, textMaterial );
        textMesh.position.set(100,-200,-1200);
        textMesh.rotation.y = -70 * (Math.PI / 180);
        //シーンオブジェクトに追加
        this.scene.add(textMesh);
        this.m_win = textMesh;
    }
    if (!this.hasnext()) {
        this.setnext(this.S_HIDE);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_CREATE_YOU
*/
TitleControl.prototype.S_CREATE_YOU = function (first) {
    "use strict";
    if (first) {
        //正面マテリアルの生成
         var materialFront = new THREE.MeshBasicMaterial( { color: 0xff0000 } );
        //側面マテリアルの生成
         var materialSide = new THREE.MeshBasicMaterial( { color: 0x000088 } );
         var materialArray = [ materialFront, materialSide ];
        //テキスト
         var textGeom = new THREE.TextGeometry( "YOU",
                {
                        size: 500, height: 80, curveSegments: 3,
                        font: this.font,
                        bevelThickness: 1, bevelSize: 2, bevelEnabled: true,
                        material: 0, extrudeMaterial: 1
                });
        var textMaterial = new THREE.MeshFaceMaterial(materialArray);
        var textMesh = new THREE.Mesh(textGeom, textMaterial );
        textMesh.position.set(-600,-200,0);
        textMesh.rotation.y = 70 * (Math.PI / 180);
        //シーンオブジェクトに追加
        this.scene.add(textMesh);
        this.m_you = textMesh;
    }
    if (!this.hasnext()) {
        this.setnext(this.S_CREATE_WIN);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_END
*/
TitleControl.prototype.S_END = function (first) {
    "use strict";
    if (first) {
        console.log("END");
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_HIDE
*/
TitleControl.prototype.S_HIDE = function (first) {
    "use strict";
    if (first) {
        this.m_you.visible = false;
        this.m_win.visible = false;
    }
    if (!this.hasnext()) {
        this.setnext(this.S_END);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_INIT
*/
TitleControl.prototype.S_INIT = function (first) {
    "use strict";
    if (first) {
        this.camera = G.title_camera;
        this.scene   = G.title_scene;
    }
    if (!this.hasnext()) {
        this.setnext(this.S_LOADFONT);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_LOADFONT
*/
TitleControl.prototype.S_LOADFONT = function (first) {
    "use strict";
    if (first) {
        var loader = new THREE.FontLoader();
        this.bDone=false;
        loader.load( 'fonts/helvetiker_bold.typeface.json', function ( font ) {
            G.title_control.bDone = true;
            G.title_control.font = font;
        });
    }
    if (this.bDone == false) { return; }
    if (!this.hasnext()) {
        this.setnext(this.S_CREATE_YOU);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_START
*/
TitleControl.prototype.S_START = function (first) {
    "use strict";
    if (!this.hasnext()) {
        this.setnext(this.S_INIT);
    }
    if (this.hasnext()) {
        this.gonext();
    }
};
/*
    S_WAIT_REQ
*/
TitleControl.prototype.S_WAIT_REQ = function (first) {
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
TitleControl.prototype.S_WAIT_REQ1 = function (first) {
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