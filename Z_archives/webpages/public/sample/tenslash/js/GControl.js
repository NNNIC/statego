// == manager ==
var GControl = function() {
    this.curfunc   = null;
    this.nextfunc  = null;
    this.nowait    = false;
    
    this.callstack = [];
};
GControl.prototype.update = function() {
    while(true)
    {
        this.nowait = false;

        var first = false;
        if (this.nextfunc!=null) {
            this.curfunc  = this.nextfunc;
            this.nextfunc = null;
            first = true;
        }
        if (this.curfunc!=null) {
            this.curfunc(first);
        }

        if (!this.nowait) break;
    }
};
GControl.prototype.checkstate = function(st) {
    return this.curfunc === st;
};
GControl.prototype.goto = function(st) {
    this.nextfunc = st;
};
GControl.prototype.hasnext = function() {
    return this.nextfunc!=null;
}
GControl.prototype.setnowait = function() {
    this.nowait = true;
}
GControl.prototype.start = function() {
    this.goto(this.S_START);
};
GControl.prototype.is_end = function() {
    return this.checkstate(this.S_END);
};
GControl.prototype.run = function() {
    var LOOPMAX = 100000;
    this.start();
    for(var loop = 0; loop <= LOOPMAX; loop++)
    {
        if (loop>=LOOPMAX) alert("Unexpected!");
        this.update();
        if (this.is_end()) break;
    }
};
GControl.prototype.gosubstate = function(sb,nt) {
    this.callstack.push(nt);
    this.goto(sb);
};
GControl.prototype.returnstate = function() {
    this.goto(this.callstack.pop());
};

// [STATEGO OUTPUT START] indent(0) $/./$
//  psggConverterLib.dll converted from GControl.xlsx.    psgg-file:GControl.psgg
/*
    E_LOG
    void log(s);
    void lognl(s);
*/
GControl.prototype.log = function(s) {
    logobj.innerHTML += s;
};
GControl.prototype.lognl = function(s) {
    logobj.innerHTML += s + "\n";
};
/*
    E_MAKEKEY
    int this.makekey(row,col)
*/
GControl.prototype.makekey = function(x,y) {
    return y * 100 + x;
};
/*
    E_U_RAND
    ランダム
    int urand(最大値)
*/
GControl.prototype.urand=function(n) {
   var v1 = (n+1) * Math.random();
   var v2 = Math.floor(v1);
   return v2;
}
/*
    E_U_RAND1
    ランダム
    int[] urand(最大値,個数)
    ※指定最大値内から、重複のない数値を個数分取得
*/
GControl.prototype.urandk=function(n,k) {
    var l = [];
    for(var loop = 0; loop<=1E+5;loop++) {
        if (loop == 1E+5) {
            alert("urand loop exceeded!");
            return null;
        }
        var r = this.urand(n);
        if (!l.includes(r)) {
            l.push(r);
            if (l.length >= k) {
                return l;
            }
        }
    }
    return null;
}
/*
    E_U_REP
    文字列繰り返し
    string urep(s,num);
*/
GControl.prototype.urep=function(c,num) {
    var s = "";
    for(var n = 0; n<num;n++) {
        s += c;
    }
    return s;
};
/*
    E_V_CLEAR
    仮想ボードクリア
    void vcrear()
*/
GControl.prototype.vclear=function() {
     var s = G.urep(" ", G.gb_xmax);
     for(var y = 0; y < G.gb_ymax; y++) {
         G.vprintx(0,y,s);
     }
};
/*
    E_V_GETOBJ
    仮想ボード上のViewOBJを得る
    Eelement vgetobj(x,y);
*/
GControl.prototype.vgetobj=function(x,y) {
    var vx = G.gb_lx + x;
    var vy = G.gb_ly + y;
    return G.D[G.makekey(vx,vy)];
};
/*
    E_V_MAKEGAMEBOARD
    仮想のゲームボード作成
*/
GControl.prototype.vmakegameboard = function(width,height) {
    this.gb_xmax = width;
    this.gb_ymax= height;
    this.gb_cx  = (width - 1) / 2;
    this.gb_cy  = (height- 1) / 2;
    var gb=[];
    for(var x = 0; x < width; x++) for(var y = 0; y < height; y++) {
        var key = this.makekey(x,y);
        gb[key] = " ";
    }
    return gb;
};
/*
    E_V_PRINTX
    仮想ボードPRINTX
    void vprint(x,y,s)
*/
GControl.prototype.vprintx = function(x,y,s) {
    var len = s.length;
    for(var n = 0; n < len; n++) {
        var key = this.makekey(x + n,y);
        G.GB[key] = s.charAt(n);
    }
};
/*
    E_V_PRINTY
    仮想ボードPRINTY
    void vprinty(x,y,s)
*/
GControl.prototype.vprinty = function(x,y,s) {
    var len = s.length;
    for(var n = 0; n < len; n++) {
        var key = this.makekey(x,y + n);
        G.GB[key] = s.charAt(n);
    }
};
/*
    E_VPRINTD
    void vprintd(x,y,dir,s);
    指定方向に表示
    dir = u,r,d,l ur,dr,dl,ur
*/
GControl.prototype.vprintd = function(x,y,dir,s) {
    var dx = 0;
    var dy = 0;
    if      (dir == "u" ){        dy=-1; }
    else if (dir == "ur"){ dx= 1; dy=-1; }
    else if (dir == "r" ){ dx= 1;        }
    else if (dir == "dr"){ dx= 1; dy= 1; }
    else if (dir == "d" ){        dy= 1; }
    else if (dir == "dl"){ dx=-1; dy= 1; }
    else if (dir == "l" ){ dx=-1;        }
    else if (dir == "ul"){ dx=-1; dy=-1; }
    var len = s.length;
    for(var n = 0; n < len; n++) {
        var a = s.charAt(n);
        var key =G.makekey(x + dx * n,y + dy * n);
        if (G.GB[key] != null) {
            G.GB[key] = a;
        }
    }
};
/*
    S_BUTTON
    button : ボタン名
    butevt : 発生イベント名
    butx,buty:位置
*/
GControl.prototype.S_BUTTON = function(first) {
    if (first)
    {
        this.button = null;
        this.butx   = 16;
        this.buty   = 11;
        this.butevt = null;
    }
    if (!this.hasnext()) {
        this.goto(this.S_END);
    }
};
/*
    S_DIRNUM
    方向数字（複数）
    dirnums
*/
GControl.prototype.S_DIRNUM = function(first) {
    if (first)
    {
        this.dirnums=[];
    }
    if (!this.hasnext()) {
        this.goto(this.S_Q_VALS);
    }
};
/*
    S_END
*/
GControl.prototype.S_END = function(first) {
};
/*
    S_gameboard
    gb_xmax:ゲーム盤X最大
    gb_ymax:ゲーム盤Y最大
    gb_lx  :ロケーションx
    gb_ly  :            y
    GB[]   :ゲーム盤データ
    gb_cx  :ゲーム盤中央X
    gb_cy  :ゲーム盤中央Y
*/
GControl.prototype.S_gameboard = function(first) {
    if (first)
    {
        this.gb_xmax = -1;
        this.gb_ymax = -1;
        this.gb_lx   = -1;
        this.gb_ly   = -1;
        this.GB      = null;
        this.gb_cx   = -1;
        this.gb_cy   = -1;
    }
    if (!this.hasnext()) {
        this.goto(this.S_numpick);
    }
};
/*
    S_instances
    mcon = MainControl
    bcon = BoardControl
    pcon = PlayControl
*/
GControl.prototype.S_instances = function(first) {
    if (first)
    {
        this.mcon = null;
        this.bcon = null;
        this.pcon = null;
    }
    if (!this.hasnext()) {
        this.goto(this.S_view);
    }
};
/*
    S_mouse
    mouseX/mouseY
    mouseClick:回数
    mouseOnOff: on=1
    vx,vy,vxy: View位置
    gb_x,gb_y: 盤上位置
    curtxt:カーソル文字
*/
GControl.prototype.S_mouse = function(first) {
    if (first)
    {
        this.mouseX=-1;
        this.mouseY=-1;
        this.mouseClick=-1;
        this.mouseOnOff=-1;
        this.vxy = [];
        this.vx = -1;
        this.vy = -1;
        this.gb_x=-1;
        this.gb_y=-1;
        this.curtxt=null;
    }
    if (!this.hasnext()) {
        this.goto(this.S_gameboard);
    }
};
/*
    S_numpick
    番号ピック用の場所
    np_x 左端 view
    np_y 高さ view
*/
GControl.prototype.S_numpick = function(first) {
    if (first)
    {
        this.np_x=-1;
        this.np_y=-1;
    }
    if (!this.hasnext()) {
        this.goto(this.S_picknum_and_obj);
    }
};
/*
    S_picknum_and_obj
    picknum : 選択番号
    pickobj : そのOBJ
*/
GControl.prototype.S_picknum_and_obj = function(first) {
    if (first)
    {
        this.picknum = -1;
        this.pickobj = null;
    }
    if (!this.hasnext()) {
        this.goto(this.S_target_mode);
    }
};
/*
    S_Q_VALS
    問題の結果
    qlevel
    qnumber
    qname
    qlimit
    qexp-説明
    qtip-ヒント
    qresult : playing|ok|ng|cancel
    qlist=[]問題
*/
GControl.prototype.S_Q_VALS = function(first) {
    if (first)
    {
        this.qlevel  = 0;
        this.qnumber = 0;
        this.qname = "";
        this.qlimit=0;
        this.qexp="";
        this.qtip="";
        this.qresult=null;
        this.qlist=[];
    }
    if (!this.hasnext()) {
        this.goto(this.S_SCORE);
    }
};
/*
    S_SCORE
    step:手
    life:命
*/
GControl.prototype.S_SCORE = function(first) {
    if (first)
    {
        this.step = 0;
        this.life = 10;
    }
    if (!this.hasnext()) {
        this.goto(this.S_BUTTON);
    }
};
/*
    S_START
*/
GControl.prototype.S_START = function(first) {
    this.goto(this.S_instances);
    this.setnowait();
};
/*
    S_target_mode
    置く位置とobj
    ※GB上
    targetx/targety
    targetobj
    targetnum
    delmode:1,2,4,8
    deldir :u,r,b,l
*/
GControl.prototype.S_target_mode = function(first) {
    if (first)
    {
        this.tagetx=-1;
        this.targety=-1;
        this.targetobj=null;
        this.targetnum=-1;
        this.delmode=null;
        this.deldir=null;
    }
    if (!this.hasnext()) {
        this.goto(this.S_DIRNUM);
    }
};
/*
    S_view
    xmax : view x最大値
    ymax : view y最大値
    D[]  : viewデータ
*/
GControl.prototype.S_view = function(first) {
    if (first)
    {
        this.xmax = 0;
        this.ymax = 0;
    }
    if (!this.hasnext()) {
        this.goto(this.S_mouse);
    }
};


// [STATEGO OUTPUT END]

// == write your code ==

