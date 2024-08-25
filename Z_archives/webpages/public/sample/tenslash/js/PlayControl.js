// == manager ==
var PlayControl = function() {
    this.curfunc   = null;
    this.nextfunc  = null;
    this.nowait    = false;
    
    this.callstack = [];
};
PlayControl.prototype.update = function() {
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
PlayControl.prototype.checkstate = function(st) {
    return this.curfunc === st;
};
PlayControl.prototype.goto = function(st) {
    this.nextfunc = st;
};
PlayControl.prototype.hasnext = function() {
    return this.nextfunc!=null;
}
PlayControl.prototype.setnowait = function() {
    this.nowait = true;
}
PlayControl.prototype.start = function() {
    this.goto(this.S_START);
};
PlayControl.prototype.is_end = function() {
    return this.checkstate(this.S_END);
};
PlayControl.prototype.run = function() {
    var LOOPMAX = 100000;
    this.start();
    for(var loop = 0; loop <= LOOPMAX; loop++)
    {
        if (loop>=LOOPMAX) alert("Unexpected!");
        this.update();
        if (this.is_end()) break;
    }
};
PlayControl.prototype.gosubstate = function(sb,nt) {
    this.callstack.push(nt);
    this.goto(sb);
};
PlayControl.prototype.returnstate = function() {
    this.goto(this.callstack.pop());
};

// [STATEGO OUTPUT START] indent(0) $/./$
//  psggConverterLib.dll converted from PlayControl.xlsx.    psgg-file:PlayControl.psgg
/*
    E_CHECKCLEAR
    GB上全部クリア？
    bool checkclear();
*/
PlayControl.prototype.checkclear = function() {
    for(var x = 0; x < G.gb_xmax; x++) for(var y = 0; y < G.gb_ymax; y++) {
        var v = G.GB[G.makekey(x,y)];
        if (v == " " || v == "" || v == null || v == undefined)
        {
            //ok
        }
        else {
            return false;
        }
    }
    return true;
};
/*
    E_DELSHIFT
    上方向にチェックとシフト１回
    bool delshift(tx.ty.s,dir)'
    tx,ty:ターゲット位置
    s:指定文字
    dir:方向 urd
    戻り値: true成功 l
*/
PlayControl.prototype.delshift = function(tx,ty,s,dir) {
    var dx = 0;
    var dy = 0;
    if (dir == "u")       { dy = -1;}
    else if (dir == "r")  { dx =  1;}
    else if (dir == "d")  { dy =  1;}
    else if (dir == "l")  { dx = -1;}
    else if (dir == "ur") { dy = -1; dx = 1;}
    else if (dir == "dr") { dy =  1; dx = 1;}
    else if (dir == "ul") { dy = -1; dx = -1;}
    else if (dir == "dl") { dy =  1; dx = -1;}
    var key = G.makekey(tx,ty);
    if (G.GB[key] != s) { return false; }
    //以降はシフト
    for(var n = 0; n <= 100; n++)
    {
        var key_src = G.makekey(tx + dx * (n + 1), ty + dy * (n + 1));
        var key_dst = G.makekey(tx + dx * n      , ty + dy * n);
        var val_src = G.GB[key_src];
        var val_dst = G.GB[key_dst];
        if (val_dst == undefined || val_dst == null　|| val_dst == "" || val_dst == " ") {
            return false;
        }
        if (val_src == undefined || val_src == null || val_src == "" || val_src == " ") {
            G.GB[key_dst] = " ";
            return true; //ここまで
        }
        G.GB[key_dst] = val_src;
    }
    return true;
};
/*
    E_GETVAL
    string getval(x,y,d);
*/
PlayControl.prototype.getval = function(x,y,d) {
    var dx = 0;
    var dy = 0;
    if      (d=="u") dy = -1;
    else if (d=="r") dx =  1;
    else if (d=="d") dy =  1;
    else if (d=="l") dx = -1;
    else if (d=="ur") { dx = 1; dy = -1; }
    else if (d=="dr") { dx = 1; dy =  1; }
    else if (d=="dl") { dx =-1; dy =  1; }
    else if (d=="ul") { dx =-1; dy = -1; }
    var key = G.makekey(x+dx,y+dy);
    var val = G.GB[key];
    return val;
};
/*
    S_0001
*/
PlayControl.prototype.S_0001 = function(first) {
    if (first)
    {
        G.qresult="playing";
    }
    if (!this.hasnext()) {
        this.goto(this.S_CHECK_PICK);
    }
};
/*
    S_CHECK_ONEWAY_DOWNLEFT
    一方向の下左確認
*/
PlayControl.prototype.S_CHECK_ONEWAY_DOWNLEFT = function(first) {
    if (first)
    {
        G.delmode = 0;
        G.deldir  = null;
        G.dirnums = [];
    }
    var b = false;
    var key = G.makekey(G.targetx + (-1) , G.targety + 1 );
    var val = G.GB[key];
    if (val == null || val == "" || val == " ") {
        b = false;
    }
    else {
        var num = Number(val);
        b = (num + G.targetnum == 10);
        if (b) {
            G.delmode = 1;
            G.deldir = "dl";
            G.dirnums = [ num ];
        }
    }
    if (b) { this.goto( this.S_DELETE_A_LINE ); }
    else { this.goto( this.S_CHECK_ONEWAY_RIGHT2 ); }
    if (this.hasnext()) {
        this.setnowait();
    }
};
/*
    S_CHECK_ONEWAY_DOWNRIGHT
    一方向の下右確認
*/
PlayControl.prototype.S_CHECK_ONEWAY_DOWNRIGHT = function(first) {
    if (first)
    {
        G.delmode = 0;
        G.deldir  = null;
        G.dirnums = [];
    }
    var b = false;
    var key = G.makekey(G.targetx + 1 , G.targety + 1 );
    var val = G.GB[key];
    if (val == null || val == "" || val == " ") {
        b = false;
    }
    else {
        var num = Number(val);
        b = (num + G.targetnum == 10);
        if (b) {
            G.delmode = 1;
            G.deldir = "dr";
            G.dirnums = [ num ];
        }
    }
    if (b) { this.goto( this.S_DELETE_A_LINE ); }
    else { this.goto( this.S_CHECK_ONEWAY_RIGHT1 ); }
    if (this.hasnext()) {
        this.setnowait();
    }
};
/*
    S_CHECK_ONEWAY_RIGHT
    一方向の右確認
*/
PlayControl.prototype.S_CHECK_ONEWAY_RIGHT = function(first) {
    if (first)
    {
        G.delmode = 0;
        G.deldir  = null;
        G.dirnums = [];
    }
    var b = false;
    var key = G.makekey(G.targetx + 1 , G.targety + 0 );
    var val = G.GB[key];
    if (val == null || val == "" || val == " ") {
        b = false;
    }
    else {
        var num = Number(val);
        b = (num + G.targetnum == 10);
        if (b) {
            G.delmode = 1;
            G.deldir = "r";
            G.dirnums = [ num ];
        }
    }
    if (b) { this.goto( this.S_DELETE_A_LINE ); }
    else { this.goto( this.S_CHECK_ONEWAY_DOWNRIGHT ); }
    if (this.hasnext()) {
        this.setnowait();
    }
};
/*
    S_CHECK_ONEWAY_RIGHT1
    一方向の下確認
*/
PlayControl.prototype.S_CHECK_ONEWAY_RIGHT1 = function(first) {
    if (first)
    {
        G.delmode = 0;
        G.deldir  = null;
        G.dirnums = [];
    }
    var b = false;
    var key = G.makekey(G.targetx + 0 , G.targety + 1 );
    var val = G.GB[key];
    if (val == null || val == "" || val == " ") {
        b = false;
    }
    else {
        var num = Number(val);
        b = (num + G.targetnum == 10);
        if (b) {
            G.delmode = 1;
            G.deldir = "d";
            G.dirnums = [ num ];
        }
    }
    if (b) { this.goto( this.S_DELETE_A_LINE ); }
    else { this.goto( this.S_CHECK_ONEWAY_DOWNLEFT ); }
    if (this.hasnext()) {
        this.setnowait();
    }
};
/*
    S_CHECK_ONEWAY_RIGHT2
    一方向の左確認
*/
PlayControl.prototype.S_CHECK_ONEWAY_RIGHT2 = function(first) {
    if (first)
    {
        G.delmode = 0;
        G.deldir  = null;
        G.dirnums = [];
    }
    var b = false;
    var key = G.makekey(G.targetx + (-1) , G.targety + 0 );
    var val = G.GB[key];
    if (val == null || val == "" || val == " ") {
        b = false;
    }
    else {
        var num = Number(val);
        b = (num + G.targetnum == 10);
        if (b) {
            G.delmode = 1;
            G.deldir = "l";
            G.dirnums = [ num ];
        }
    }
    if (b) { this.goto( this.S_DELETE_A_LINE ); }
    else { this.goto( this.S_CHECK_ONEWAY_UPLEFT ); }
    if (this.hasnext()) {
        this.setnowait();
    }
};
/*
    S_CHECK_ONEWAY_UP
    一方向の上確認
*/
PlayControl.prototype.S_CHECK_ONEWAY_UP = function(first) {
    if (first)
    {
        G.delmode = 0;
        G.deldir  = null;
        G.dirnums = [];
    }
    var b = false;
    var key = G.makekey(G.targetx + 0 , G.targety + (-1) );
    var val = G.GB[key];
    if (val == null || val == "" || val == " ") {
        b = false;
    }
    else {
        var num = Number(val);
        b = (num + G.targetnum == 10);
        if (b) {
            G.delmode = 1;
            G.deldir = "u";
            G.dirnums = [ num ];
        }
    }
    if (b) { this.goto( this.S_DELETE_A_LINE ); }
    else { this.goto( this.S_CHECK_ONEWAY_UPRIGHT ); }
    if (this.hasnext()) {
        this.setnowait();
    }
};
/*
    S_CHECK_ONEWAY_UPLEFT
    一方向の上左確認
*/
PlayControl.prototype.S_CHECK_ONEWAY_UPLEFT = function(first) {
    if (first)
    {
        G.delmode = 0;
        G.deldir  = null;
        G.dirnums = [];
    }
    var b = false;
    var key = G.makekey(G.targetx + (-1) , G.targety + (-1) );
    var val = G.GB[key];
    if (val == null || val == "" || val == " ") {
        b = false;
    }
    else {
        var num = Number(val);
        b = (num + G.targetnum == 10);
        if (b) {
            G.delmode = 1;
            G.deldir = "ul";
            G.dirnums = [ num ];
        }
    }
    if (b) { this.goto( this.S_DELETE_A_LINE ); }
    else { this.goto( this.S_TWOWAY_V ); }
    if (this.hasnext()) {
        this.setnowait();
    }
};
/*
    S_CHECK_ONEWAY_UPRIGHT
    一方向の上右確認
*/
PlayControl.prototype.S_CHECK_ONEWAY_UPRIGHT = function(first) {
    if (first)
    {
        G.delmode = 0;
        G.deldir  = null;
        G.dirnums = [];
    }
    var b = false;
    var key = G.makekey(G.targetx + 1 , G.targety + (-1) );
    var val = G.GB[key];
    if (val == null || val == "" || val == " ") {
        b = false;
    }
    else {
        var num = Number(val);
        b = (num + G.targetnum == 10);
        if (b) {
            G.delmode = 1;
            G.deldir = "ur";
            G.dirnums = [ num ];
        }
    }
    if (b) { this.goto( this.S_DELETE_A_LINE ); }
    else { this.goto( this.S_CHECK_ONEWAY_RIGHT ); }
    if (this.hasnext()) {
        this.setnowait();
    }
};
/*
    S_CHECK_PICK
    ピック部の確認
*/
PlayControl.prototype.S_CHECK_PICK = function(first) {
    if (first)
    {
        G.pickobj=null;
        G.qexp="ボード下の数字を選択";
        G.curtxt=null;
    }
    var find = -1;
    if ((G.vy == G.np_y) && (G.vx >= G.np_x && G.vx <= G.np_x + 9)){
        find = G.vx - G.np_x;
        //G.lognl("find="+find);
    }
    if (!(find != -1 && G.mouseOnOff == 1)) { return; }
    G.log("kettei = " + find);
    G.picknum = find;
    G.pickobj = G.D[G.makekey(G.vx,G.vy)];
    if (!this.hasnext()) {
        this.goto(this.S_DROP_PICK);
    }
    if (this.hasnext()) {
        this.setnowait();
    }
};
/*
    S_CHECK_RESULT
    結果確認
*/
PlayControl.prototype.S_CHECK_RESULT = function(first) {
    if (first)
    {
        var b = this.checkclear();
        if (b) {
            G.qresult = "ok";
        }
    }
    if (G.qresult=="ok") { this.goto( this.S_END ); }
    else { this.goto( this.S_CHECK_PICK ); }
};
/*
    S_COUNT_STEP
*/
PlayControl.prototype.S_COUNT_STEP = function(first) {
    if (first)
    {
        G.step++;
        if (G.step > G.qlimit){
            G.life--;
        }
    }
    if (!this.hasnext()) {
        this.goto(this.S_ONE_TICK);
    }
    if (this.hasnext()) {
        this.setnowait();
    }
};
/*
    S_DEL_4LINES
    ４行削除
*/
PlayControl.prototype.S_DEL_4LINES = function(first) {
    if (first)
    {
        G.lognl("4mode当たり");
        this.wt=Date.now();
    }
    var num1 = G.dirnums[0];
    var num2 = G.dirnums[1];
    var num3 = G.dirnums[2];
    var num4 = G.dirnums[3];
    var b = false;
    var waittime = 50; //msec
    if (this.wt > Date.now()) {
        b = true;
    }
    else if (G.deldir=="+") {
        var val1 = this.getval(G.targetx, G.targety, "u");
        var val2 = this.getval(G.targetx, G.targety, "r");
        var val3 = this.getval(G.targetx, G.targety, "d");
        var val4 = this.getval(G.targetx, G.targety, "l");
        if ( val1==num1 && val2==num2 && val3==num3 && val4==num4) {
            var b1 = this.delshift(G.targetx    ,G.targety - 1, ""+num1, "u");
            var b2 = this.delshift(G.targetx + 1,G.targety    , ""+num2, "r");
            var b3 = this.delshift(G.targetx    ,G.targety + 1, ""+num3, "d");
            var b4 = this.delshift(G.targetx - 1,G.targety    , ""+num4, "l");
            var b = b1 && b2 && b3 && b4;
            this.wt = Date.now() + waittime;
        }
    }
    else if (G.deldir=="x") {
        var val1 = this.getval(G.targetx, G.targety, "ur");
        var val2 = this.getval(G.targetx, G.targety, "dr");
        var val3 = this.getval(G.targetx, G.targety, "dl");
        var val4 = this.getval(G.targetx, G.targety, "ul");
        if ( val1==num1 && val2==num2 && val3==num3 && val4==num4) {
            var b1 = this.delshift(G.targetx + 1,G.targety - 1, ""+num1, "ur");
            var b2 = this.delshift(G.targetx + 1,G.targety + 1, ""+num2, "dr");
            var b3 = this.delshift(G.targetx - 1,G.targety + 1, ""+num3, "dl");
            var b4 = this.delshift(G.targetx - 1,G.targety - 1, ""+num4, "ul");
            var b = b1 && b2 && b3 && b4;
            this.wt = Date.now() + waittime;
        }
    }
    if (b == true) { return; }
    G.GB[G.makekey(G.targetx,G.targety)] = " ";
    G.targetobj = null;
    if (!this.hasnext()) {
        this.goto(this.S_CHECK_RESULT);
    }
};
/*
    S_DEL_8LINES
    8行削除
*/
PlayControl.prototype.S_DEL_8LINES = function(first) {
    if (first)
    {
        G.lognl("8mode当たり");
        this.wt=Date.now();
    }
    var num1 = G.dirnums[0];
    var num2 = G.dirnums[1];
    var num3 = G.dirnums[2];
    var num4 = G.dirnums[3];
    var num5 = G.dirnums[4];
    var num6 = G.dirnums[5];
    var num7 = G.dirnums[6];
    var num8 = G.dirnums[7];
    var b = false;
    var waittime = 50; //msec
    if (this.wt > Date.now()) {
        b = true;
    }
    else {
        var val1 = this.getval(G.targetx, G.targety, "u");
        var val2 = this.getval(G.targetx, G.targety, "r");
        var val3 = this.getval(G.targetx, G.targety, "d");
        var val4 = this.getval(G.targetx, G.targety, "l");
        var val5 = this.getval(G.targetx, G.targety, "ur");
        var val6 = this.getval(G.targetx, G.targety, "dr");
        var val7 = this.getval(G.targetx, G.targety, "dl");
        var val8 = this.getval(G.targetx, G.targety, "ul");
        if ( val1==num1 && val2==num2 && val3==num3 && val4==num4 && val5==num5 && val6==num6 && val7==num7 && val8==num8) {
            var b1 = this.delshift(G.targetx    ,G.targety - 1, ""+num1, "u");
            var b2 = this.delshift(G.targetx + 1,G.targety    , ""+num2, "r");
            var b3 = this.delshift(G.targetx    ,G.targety + 1, ""+num3, "d");
            var b4 = this.delshift(G.targetx - 1,G.targety    , ""+num4, "l");
            var b5 = this.delshift(G.targetx + 1,G.targety - 1, ""+num5, "ur");
            var b6 = this.delshift(G.targetx + 1,G.targety + 1, ""+num6, "dr");
            var b7 = this.delshift(G.targetx - 1,G.targety + 1, ""+num7, "dl");
            var b8 = this.delshift(G.targetx - 1,G.targety - 1, ""+num8, "ul");
            var b = b1 && b2 && b3 && b4 && b5 && b6 && b7 && b8;
            this.wt = Date.now() + waittime;
        }
        else {
            b = false;
        }
    }
    if (b == true) { return; }
    G.GB[G.makekey(G.targetx,G.targety)] = " ";
    G.targetobj = null;
    if (!this.hasnext()) {
        this.goto(this.S_CHECK_RESULT);
    }
};
/*
    S_DEL_TWO_LINES
    ２行削除
*/
PlayControl.prototype.S_DEL_TWO_LINES = function(first) {
    if (first)
    {
        G.lognl("2mode当たり");
        this.wt=Date.now();
    }
    var num1 = G.dirnums[0];
    var num2 = G.dirnums[1];
    var b = false;
    var waittime = 50; //msec
    if (this.wt > Date.now()) {
        b = true;
    }
    else if (G.deldir == "v") {
       var val1 = this.getval(G.targetx, G.targety,"u");
       var val2 = this.getval(G.targetx, G.targety,"d");
       if (val1==num1 && val2==num2) {
           var b1 = this.delshift(G.targetx, G.targety -1, ""+num1,"u");
           var b2 = this.delshift(G.targetx, G.targety +1, ""+num2,"d");
           b = b1 && b2;
           this.wt = Date.now() + waittime;
       }
       else {
           b = false;
       }
    }
    else if (G.deldir == "ur") {
       var val1 = this.getval(G.targetx, G.targety,"ur");
       var val2 = this.getval(G.targetx, G.targety,"dl");
       if (val1==num1 && val2==num2) {
           var b1 = this.delshift(G.targetx +1, G.targety -1, ""+num1,"ur");
           var b2 = this.delshift(G.targetx -1, G.targety +1, ""+num2,"dl");
           b = b1 && b2;
           this.wt = Date.now() + waittime;
       }
       else {
           b = false;
       }
    }
    else if (G.deldir == "h") {
       var val1 = this.getval(G.targetx, G.targety,"r");
       var val2 = this.getval(G.targetx, G.targety,"l");
       if (val1==num1 && val2==num2) {
           var b1 = this.delshift(G.targetx + 1, G.targety , ""+num1,"r");
           var b2 = this.delshift(G.targetx - 1, G.targety , ""+num2,"l");
           b = b1 && b2;
           this.wt = Date.now() + waittime;
       }
       else {
           b = false;
       }
    }
    else if (G.deldir == "dr") {
       var val1 = this.getval(G.targetx, G.targety,"dr");
       var val2 = this.getval(G.targetx, G.targety,"ul");
       if (val1==num1 && val2==num2) {
           var b1 = this.delshift(G.targetx +1, G.targety +1, ""+num1,"dr");
           var b2 = this.delshift(G.targetx -1, G.targety -1, ""+num2,"ul");
           b = b1 && b2;
           this.wt = Date.now() + waittime;
       }
       else {
           b = false;
       }
    }
    if (b==true) { return; }
    G.GB[G.makekey(G.targetx,G.targety)] = " ";
    G.targetobj = null;
    if (!this.hasnext()) {
        this.goto(this.S_CHECK_RESULT);
    }
};
/*
    S_DELETE_A_LINE
    ライン削除
*/
PlayControl.prototype.S_DELETE_A_LINE = function(first) {
    if (first)
    {
        G.lognl("当たり！");
        this.wt=Date.now();
    }
    var num = G.dirnums[0];
    var b = false;
    var waittime = 50; //msec
    if (this.wt > Date.now())
    {
        b = true;
    }
    else if (G.deldir == "u") {
        b = this.delshift(G.targetx    , G.targety - 1, ""+num, "u");
        this.wt = Date.now() + waittime;
    }
    else if (G.deldir == "ur") {
        b = this.delshift(G.targetx + 1, G.targety - 1, ""+num, "ur");
        this.wt = Date.now() + waittime;
    }
    else if (G.deldir == "r") {
        b = this.delshift(G.targetx + 1, G.targety    , ""+num, "r");
        this.wt = Date.now() + waittime;
    }
    else if (G.deldir == "dr") {
        b = this.delshift(G.targetx + 1, G.targety + 1, ""+num, "dr");
        this.wt = Date.now() + waittime;
    }
    else if (G.deldir == "d") {
        b = this.delshift(G.targetx    , G.targety + 1, ""+num, "d");
        this.wt = Date.now() + waittime;
    }
    else if (G.deldir == "dl") {
        b = this.delshift(G.targetx - 1, G.targety + 1, ""+num, "dl");
        this.wt = Date.now() + waittime;
    }
    else if (G.deldir == "l") {
        b = this.delshift(G.targetx - 1, G.targety    , ""+num, "l");
        this.wt = Date.now() + waittime;
    }
    else if (G.deldir == "ul") {
        b = this.delshift(G.targetx - 1, G.targety - 1, ""+num, "ul");
        this.wt = Date.now() + waittime;
    }
    if (b == true) { return; }
    G.GB[G.makekey(G.targetx,G.targety)]=" ";
    G.targetobj=null;
    if (!this.hasnext()) {
        this.goto(this.S_CHECK_RESULT);
    }
};
/*
    S_DROP_PICK
    ピックしたものをDROP
*/
PlayControl.prototype.S_DROP_PICK = function(first) {
    if (first)
    {
        G.targetx   = -1;
        G.targety   = -1;
        G.targetobj = null;
        G.qexp="ボード上に配置";
        G.curtxt    = ""+G.picknum;
    }
    if (G.gb_x>=0 && G.gb_y>=0 && G.mouseOnOff==1) {
        var key = G.makekey(G.gb_x, G.gb_y);
        var s = G.GB[key];
        if (s == " "||s==""||s==null)
        {
            G.targetx = G.gb_x;
            G.targety = G.gb_y;
            G.targetobj = G.vgetobj(G.gb_x, G.gb_y);
    　　    G.targetnum = G.picknum;
            G.GB[key] = "" + G.picknum;
            G.picknum = -1;
            G.pickobj = null;
            G.lognl("!!");
        }
    }
    if (G.targetobj==null) { return; }
    G.curtxt=null;
    if (!this.hasnext()) {
        this.goto(this.S_COUNT_STEP);
    }
    if (this.hasnext()) {
        this.setnowait();
    }
};
/*
    S_EIGHTWAY
*/
PlayControl.prototype.S_EIGHTWAY = function(first) {
    if (first)
    {
        G.delmode = 0;
        G.deldir  = null;
        G.dirnums = [];
    }
    var b = false;
    var val1 = this.getval(G.targetx,G.targety,"u");
    var val2 = this.getval(G.targetx,G.targety,"r");
    var val3 = this.getval(G.targetx,G.targety,"d");
    var val4 = this.getval(G.targetx,G.targety,"l");
    var val5 = this.getval(G.targetx,G.targety,"ur");
    var val6 = this.getval(G.targetx,G.targety,"dr");
    var val7 = this.getval(G.targetx,G.targety,"dl");
    var val8 = this.getval(G.targetx,G.targety,"ul");
    if (
        val1 == null || val2 == null || val3 == null || val4 == null || val5 == null || val6 == null || val7 == null || val8 == null
                     ||
        val1 == " "  || val2 == " "  || val3 == " "  || val4 == " "  || val5 == " "  || val6 == " "  || val7 == " "  || val8 == " "
                     ||
        val1 == ""   || val2 == ""   || val3 == ""   || val4 == ""   || val5 == ""   || val6 == ""   || val7 == ""   || val8 == ""
    ) {
        b = false;
    }
    else {
       var num1 = Number(val1);
       var num2 = Number(val2);
       var num3 = Number(val3);
       var num4 = Number(val4);
       var num5 = Number(val5);
       var num6 = Number(val6);
       var num7 = Number(val7);
       var num8 = Number(val8);
       b = (num1 + num2 + num3 + num4 + num5 + num6 + num7 + num8 + G.targetnum == 10);
       if (b) {
           G.delmode =8;
           G.deldir = "*";
           G.dirnums = [ num1, num2, num3, num4, num5, num6, num7, num8 ];
       }
    }
    if (b) { this.goto( this.S_DEL_8LINES ); }
    else { this.goto( this.S_LOOSE ); }
};
/*
    S_END
*/
PlayControl.prototype.S_END = function(first) {
};
/*
    S_FOURWAY_CROSS
*/
PlayControl.prototype.S_FOURWAY_CROSS = function(first) {
    if (first)
    {
        G.delmode = 0;
        G.deldir  = null;
        G.dirnums = [];
    }
    var b = false;
    var val1 = this.getval(G.targetx,G.targety,"u");
    var val2 = this.getval(G.targetx,G.targety,"r");
    var val3 = this.getval(G.targetx,G.targety,"d");
    var val4 = this.getval(G.targetx,G.targety,"l");
    if (
        val1 == null || val2 == null || val3 == null || val4 == null
                     ||
        val1 == " "  || val2 == " "  || val3 == " "  || val4 == " "
                     ||
        val1 == ""   || val2 == ""   || val3 == ""   || val4 == ""
    ) {
        b = false;
    }
    else {
       var num1 = Number(val1);
       var num2 = Number(val2);
       var num3 = Number(val3);
       var num4 = Number(val4);
       b = (num1 + num2 + num3 + num4 + G.targetnum == 10);
       if (b) {
           G.delmode =4;
           G.deldir = "+";
           G.dirnums = [ num1, num2, num3, num4 ];
       }
    }
    if (b) { this.goto( this.S_DEL_4LINES ); }
    else { this.goto( this.S_FOURWAY_DCROSS ); }
};
/*
    S_FOURWAY_DCROSS
*/
PlayControl.prototype.S_FOURWAY_DCROSS = function(first) {
    if (first)
    {
        G.delmode = 0;
        G.deldir  = null;
        G.dirnums = [];
    }
    var b = false;
    var val1 = this.getval(G.targetx,G.targety,"ur");
    var val2 = this.getval(G.targetx,G.targety,"dr");
    var val3 = this.getval(G.targetx,G.targety,"dl");
    var val4 = this.getval(G.targetx,G.targety,"ul");
    if (
        val1 == null || val2 == null || val3 == null || val4 == null
                     ||
        val1 == " "  || val2 == " "  || val3 == " "  || val4 == " "
                     ||
        val1 == ""   || val2 == ""   || val3 == ""   || val4 == ""
    ) {
        b = false;
    }
    else {
       var num1 = Number(val1);
       var num2 = Number(val2);
       var num3 = Number(val3);
       var num4 = Number(val4);
       b = (num1 + num2 + num3 + num4 + G.targetnum == 10);
       if (b) {
           G.delmode =4;
           G.deldir = "x";
           G.dirnums = [ num1, num2, num3, num4 ];
       }
    }
    if (b) { this.goto( this.S_DEL_4LINES ); }
    else { this.goto( this.S_EIGHTWAY ); }
};
/*
    S_LOOSE
*/
PlayControl.prototype.S_LOOSE = function(first) {
    if (first)
    {
        G.lognl("外れ");
    }
    if (!this.hasnext()) {
        this.goto(this.S_CHECK_PICK);
    }
};
/*
    S_ONE_TICK
*/
PlayControl.prototype.S_ONE_TICK = function(first) {
    if (!this.hasnext()) {
        this.goto(this.S_CHECK_ONEWAY_UP);
    }
};
/*
    S_START
*/
PlayControl.prototype.S_START = function(first) {
    this.goto(this.S_0001);
    this.setnowait();
};
/*
    S_TWOWAY_DR
    4.5時とその逆２ヵ所
*/
PlayControl.prototype.S_TWOWAY_DR = function(first) {
    if (first)
    {
        G.delmode = 0;
        G.deldir  = null;
        G.dirnums = [];
    }
    var b = false;
    var key1 = G.makekey(G.targetx + (1), G.targety + (1) );
    var val1 = G.GB[key1];
    var key2 = G.makekey(G.targetx - (1), G.targety - (1) );
    var val2 = G.GB[key2];
    if (
        val1 == null || val2 == null
                     ||
        val1 == " "  || val2 == " "
                     ||
        val1 == ""   || val2 == ""
    ) {
        b = false;
    }
    else {
       var num1 = Number(val1);
       var num2 = Number(val2);
       b = (num1 + num2 + G.targetnum == 10);
       if (b) {
           G.delmode =2;
           G.deldir = "dr";
           G.dirnums = [ num1, num2 ];
       }
    }
    if (b) { this.goto( this.S_DEL_TWO_LINES ); }
    else { this.goto( this.S_FOURWAY_CROSS ); }
};
/*
    S_TWOWAY_H
    3時とその逆２ヵ所
*/
PlayControl.prototype.S_TWOWAY_H = function(first) {
    if (first)
    {
        G.delmode = 0;
        G.deldir  = null;
        G.dirnums = [];
    }
    var b = false;
    var key1 = G.makekey(G.targetx + (1), G.targety + (0) );
    var val1 = G.GB[key1];
    var key2 = G.makekey(G.targetx - (1), G.targety - (0) );
    var val2 = G.GB[key2];
    if (
        val1 == null || val2 == null
                     ||
        val1 == " "  || val2 == " "
                     ||
        val1 == ""   || val2 == ""
    ) {
        b = false;
    }
    else {
       var num1 = Number(val1);
       var num2 = Number(val2);
       b = (num1 + num2 + G.targetnum == 10);
       if (b) {
           G.delmode =2;
           G.deldir = "h";
           G.dirnums = [ num1, num2 ];
       }
    }
    if (b) { this.goto( this.S_DEL_TWO_LINES ); }
    else { this.goto( this.S_TWOWAY_DR ); }
};
/*
    S_TWOWAY_UR
    1.5時方向とその逆２ヵ所
*/
PlayControl.prototype.S_TWOWAY_UR = function(first) {
    if (first)
    {
        G.delmode = 0;
        G.deldir  = null;
        G.dirnums = [];
    }
    var b = false;
    var key1 = G.makekey(G.targetx + (1), G.targety + (-1) );
    var val1 = G.GB[key1];
    var key2 = G.makekey(G.targetx - (1), G.targety - (-1) );
    var val2 = G.GB[key2];
    if (
        val1 == null || val2 == null
                     ||
        val1 == " "  || val2 == " "
                     ||
        val1 == ""   || val2 == ""
    ) {
        b = false;
    }
    else {
       var num1 = Number(val1);
       var num2 = Number(val2);
       b = (num1 + num2 + G.targetnum == 10);
       if (b) {
           G.delmode =2;
           G.deldir = "ur";
           G.dirnums = [ num1, num2 ];
       }
    }
    if (b) { this.goto( this.S_DEL_TWO_LINES ); }
    else { this.goto( this.S_TWOWAY_H ); }
};
/*
    S_TWOWAY_V
    ０時とその逆２ヵ所
*/
PlayControl.prototype.S_TWOWAY_V = function(first) {
    if (first)
    {
        G.delmode = 0;
        G.deldir  = null;
        G.dirnums = [];
    }
    var b = false;
    var key1 = G.makekey(G.targetx + (0), G.targety + (-1) );
    var val1 = G.GB[key1];
    var key2 = G.makekey(G.targetx - (0), G.targety - (-1) );
    var val2 = G.GB[key2];
    if (
        val1 == null || val2 == null
                     ||
        val1 == " "  || val2 == " "
                     ||
        val1 == ""   || val2 == ""
    ) {
        b = false;
    }
    else {
       var num1 = Number(val1);
       var num2 = Number(val2);
       b = (num1 + num2 + G.targetnum == 10);
       if (b) {
           G.delmode =2;
           G.deldir = "v";
           G.dirnums = [ num1, num2 ];
       }
    }
    if (b) { this.goto( this.S_DEL_TWO_LINES ); }
    else { this.goto( this.S_TWOWAY_UR ); }
};


// [STATEGO OUTPUT END]

// == write your code ==

