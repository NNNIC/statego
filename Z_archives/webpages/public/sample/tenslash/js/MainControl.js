// == manager ==
var MainControl = function() {
    this.curfunc   = null;
    this.nextfunc  = null;
    this.nowait    = false;
    
    this.callstack = [];
};
MainControl.prototype.update = function() {
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
MainControl.prototype.checkstate = function(st) {
    return this.curfunc === st;
};
MainControl.prototype.goto = function(st) {
    this.nextfunc = st;
};
MainControl.prototype.hasnext = function() {
    return this.nextfunc!=null;
}
MainControl.prototype.setnowait = function() {
    this.nowait = true;
}
MainControl.prototype.start = function() {
    this.goto(this.S_START);
};
MainControl.prototype.is_end = function() {
    return this.checkstate(this.S_END);
};
MainControl.prototype.run = function() {
    var LOOPMAX = 100000;
    this.start();
    for(var loop = 0; loop <= LOOPMAX; loop++)
    {
        if (loop>=LOOPMAX) alert("Unexpected!");
        this.update();
        if (this.is_end()) break;
    }
};
MainControl.prototype.gosubstate = function(sb,nt) {
    this.callstack.push(nt);
    this.goto(sb);
};
MainControl.prototype.returnstate = function() {
    this.goto(this.callstack.pop());
};

// [STATEGO OUTPUT START] indent(0) $/./$
//  psggConverterLib.dll converted from MainControl.xlsx.    psgg-file:MainControl.psgg
/*
    E_CLEARVIEW
    void this.clearview(Array)
*/
MainControl.prototype.clearview = function() {
    for(var c=0; c<G.xmax; c++) for(var r = 0; r<G.ymax; r++) {
        var key = G.makekey(c,r);
        var o = G.D[key];
        if (o!=null) {
            o.innerHTML = "";
            o.style.backgroundColor = "#fcfcfc";
        }
    }
};
/*
    E_FINDXY
*/
MainControl.prototype.findxy = function(cx,cy) {
    var findx = -1;
    var findy = -1;
    var mx = cx;
    var my = cy;
    for(var x = 0; x < G.xmax; x++){
        var key = G.makekey(x,0);
        var r = G.D[key].getBoundingClientRect();
        var left  = r.left  + window.pageXOffset;
        var right = r.right + window.pageYOffset;
        if (mx > left && mx < right)
        {
              findx = x;
        }
    }
    for(var y = 0; y < G.ymax; y++){
        var key = G.makekey(0,y);
        var r = G.D[key].getBoundingClientRect();
        var top = r.top    + window.pageYOffset;
        var bot = r.bottom + window.pageYOffset;
        if (my > top && my < bot)
        {
              findy = y;
        }
    }
    return [findx,findy];
};
/*
    E_MAKEVIEW
    Array this.makeview(int row,int col)
*/
MainControl.prototype.makeview = function(px,py) {
    var row = G.ymax;
    var col = G.xmax;
    var d = new Array();
    for(var c=0; c<col; c++) for(var r = 0; r<row; r++) {
        var leftstr = (c + 1) * 25 + 100;
        leftstr = "left:" + leftstr + "px;";
        var topstr = (r + 1) * 25 + 100;
        topstr = "top:" + topstr + "px;";
        var o = document.createElement("span");
        o.innerHTML = " ";
        //o.style.cssText = "background-color:yellow; text-align:center;  color:black; border:1px solid blue; width:25px; Height:25px;  position:absolute;" + topstr + leftstr;
        o.style.cssText = "user-select: none; cursor : pointer; background-color:#f0f0f0; text-align:center;  color:black; border:1px solid #e4e4e4; width:25px; Height:25px;  position:absolute;" + topstr + leftstr;
        rootobj.appendChild(o);
        var key = G.makekey(c,r);
        d[key]=o;
    }
    return d;
};
/*
    E_PRINTD
    void rprintd(x,y,dir,s);
    指定方向に表示
    dir = u,r,d,l ur,dr,dl,ur
*/
MainControl.prototype.rprintd = function(x,y,dir,s) {
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
        var o = G.D[G.makekey(x + dx * n,y + dy * n)];
        if (o!=null) {
            o.innerHTML = a;
        }
    }
};
/*
    E_PRINTX
*/
MainControl.prototype.rprintx = function(x,y,s) {
    var len = s.length;
    for(var n = 0; n < len; n++) {
        var a = s.charAt(n);
        G.D[G.makekey(x + n,y)].innerHTML = a;
    }
};
/*
    E_PRINTXC
    void this.rprintxc(x,y,[fc],[bc])
*/
MainControl.prototype.rprintxc = function(x,y,s,fc,bc) {
    var len = s.length;
    for(var n = 0; n < len; n++) {
        var a = s.charAt(n);
        var o =G.D[G.makekey(x + n,y)];
        o.innerHTML = a;
        if (fc!==undefined) {
            o.style.color = fc;
        }
        if (bc!==undefined) {
            o.style.backgroundColor=bc;
        }
    }
};
/*
    E_PRINTYC
    void this.rprintyc(x,y,[fc],[bc])
*/
MainControl.prototype.rprintyc = function(x,y,s,fc,bc) {
    var len = s.length;
    for(var n = 0; n < len; n++) {
        var a = s.charAt(n);
        var o =G.D[G.makekey(x,y + n)];
        o.innerHTML = a;
        if (fc!==undefined) {
            o.style.color = fc;
        }
        if (bc!==undefined) {
            o.style.backgroundColor=bc;
        }
    }
};
/*
    E_R_BPRINTX
    this.rbprintx(x,y,s);
    ボード用横表示
*/
MainControl.prototype.rbprintx = function(x,y,s,fc,bc) {
    this.rprintxc(G.gb_lx + x, G.gb_ly + y,s,fc,bc);
};
/*
    E_R_BPRINTY
    this.rbprinty(x,y,s,[fc],[bc]);
    ボード用縦表示
*/
MainControl.prototype.rbprinty = function(x,y,s,fc,bc) {
    this.rprintyc(G.gb_lx + x, this.G.gb_ly + y,s,fc,bc);
};
/*
    E_R_COPYVBOARD
    仮想ボードのコピー
*/
MainControl.prototype.rcopy = function() {
   for(var y = 0; y < G.gb_ymax; y++) {
       var s = "";
       for(var x = 0; x < G.gb_xmax; x++) {
           var v = G.GB[G.makekey(x,y)];
           if (v == null || v == undefined || v=="") {
               v = " ";
           }
           s += v;
       }
       this.rbprintx(0,y,s,"black");
   }
};
/*
    E_R_MAKEBOARD
    ゲーム用の無地ボード表示
*/
MainControl.prototype.rmakeboard = function(x,y) {
    var width  = G.gb_xmax;
    var height = G.gb_ymax;
    //
    G.gb_lx = x;
    G.gb_ly = y;
    //
    for(var ix = 0; ix < width; ix++) for(var iy = 0; iy < height; iy++) {
        var o = G.D[G.makekey(ix+x,iy+y)];
        o.style.backgroundColor = "yellow";
        o.style.borderColor = "#444444";
    }
    //bottom border
    for(var ix = 0; ix < width; ix++) {
        var o = G.D[G.makekey(ix + x , height + y)];
        o.style.borderTopColor = "#444444";
    }
    //right border
    for(var iy = 0; iy < height; iy++) {
        var o = G.D[G.makekey(width + x , iy + y)];
        o.style.borderLeftColor = "#444444";
    }
};
/*
    E_RENDER
    void this.render()
*/
MainControl.prototype.render = function() {
    this.clearview();
    if (G.RS.length!=0) {
        for(var n = 0; n<G.RS.length; n++) {
            var f = G.RS[n];
            if (f!=null) {
                f();
            }
        }
    }
    G.RS=[];
};
/*
    S_ADDEVENTS
*/
MainControl.prototype.S_ADDEVENTS = function(first) {
    if (first)
    {
        G.mouseX=-1;
        G.mouseY=-1;
        G.mouseClick=-1;
        G.mouseOnOff=-1;
        var _=this;
            window.document.body.addEventListener("mousemove", function(e){
                G.mouseX=e.pageX;
                G.mouseY=e.pageY;
            });
            window.document.body.addEventListener("click", function(e){
                G.mouseClick++;
            });
            window.document.body.addEventListener("mouseup", function(e){
                G.mouseOnOff=0;
            });
            window.document.body.addEventListener("mousedown", function(e){
                G.mouseOnOff=1;
            });
    }
    if (!this.hasnext()) {
        this.goto(this.S_INIT);
    }
};
/*
    S_ADDPOINTER1
    new state
*/
MainControl.prototype.S_ADDPOINTER1 = function(first) {
    var _ = this;
    var mx = G.mouseX;
    var my = G.mouseY;
    var mc = G.mouseClick;
    var md = G.mouseOnOff;
    var gx = G.gb_x;
    //var xy = this.findxy(mx,my);
    var f = function() {
        _.rprintx(0 ,18,("CUR:"+mx +"," +my +"," + md + ":CLICK=" + mc));
        _.rprintx(0 ,19,("VPOS:"  + G.vx + ","  + G.vy) );
        _.rprintx(15,19,("GBPOS:" + G.gb_x + ","  + G.gb_y) );
        var o  = G.D[G.makekey(G.vx,G.vy)];
        if (o!=null) {
            o.style.backgroundColor="red";
            if (G.curtxt!=null) {
                o.innerHTML=G.curtxt;
            }
        }
    };
    G.RS.push(f);
    if (!this.hasnext()) {
        this.goto(this.S_RENDER1);
    }
    if (this.hasnext()) {
        this.setnowait();
    }
};
/*
    S_BUTTON
*/
MainControl.prototype.S_BUTTON = function(first) {
    if (first)
    {
        if (G.button != null) {
            var _ = this;
            var x = G.butx;
            var y = G.buty;
            var l = G.button.length;
            if (G.vx >=x && G.vx < x + l && G.vy == y && G.mouseOnOff==1)
            {
                G.butevt = G.button;
            }
            else {
                var f = function() {
                    _.rprintxc(x,y,G.button,"white","#a0a0a0");
                };
                G.RS.push(f);
            }
        }
    }
    if (!this.hasnext()) {
        this.goto(this.S_ADDPOINTER1);
    }
    if (this.hasnext()) {
        this.setnowait();
    }
};
/*
    S_COPY
*/
MainControl.prototype.S_COPY = function(first) {
    if (first)
    {
        var _=this;
        var f = function() {
            _.rcopy();
        };
        G.RS.push(f);
    }
    if (!this.hasnext()) {
        this.goto(this.S_PICKUP);
    }
    if (this.hasnext()) {
        this.setnowait();
    }
};
/*
    S_END
*/
MainControl.prototype.S_END = function(first) {
};
/*
    S_INIT
*/
MainControl.prototype.S_INIT = function(first) {
    if (first)
    {
        G.ymax = 20;
        G.xmax = 30;
        G.D  = this.makeview(100,100);
        G.RS = [];//レンダスタック
        G.GB = G.vmakegameboard(11,11);
    }
    if (!this.hasnext()) {
        this.goto(this.S_INIT2);
    }
    if (this.hasnext()) {
        this.setnowait();
    }
};
/*
    S_INIT2
*/
MainControl.prototype.S_INIT2 = function(first) {
    if (first)
    {
        this.bcon=new BoardControl();
        this.bcon.start();
        this.bcon.step();
        this.bcon.mcon = this;
    }
    if (!this.hasnext()) {
        this.goto(this.S_LOGTEST);
    }
};
/*
    S_INPUT
*/
MainControl.prototype.S_INPUT = function(first) {
    if (first)
    {
        G.vxy = this.findxy(G.mouseX,G.mouseY);
        G.vx = G.vxy[0];
        G.vy = G.vxy[1];
        G.gb_x = G.vx - G.gb_lx;
        G.gb_y = G.vy - G.gb_ly;
        if (
           G.gb_x < G.gb_xmax
                  &&
           G.gb_y < G.gb_ymax
                  &&
           G.gb_x >= 0
                  &&
           G.gb_y >= 0
        ) {
            // ok
        }
        else {
            G.gb_x = -1;
            G.gb_y = -1;
        }
    }
    if (!this.hasnext()) {
        this.goto(this.S_STEP);
    }
    if (this.hasnext()) {
        this.setnowait();
    }
};
/*
    S_LOGTEST
*/
MainControl.prototype.S_LOGTEST = function(first) {
    if (first)
    {
        G.log("test");
        G.log("hoge");
        G.lognl("ccc");
        G.lognl("vvvv");
    }
    if (!this.hasnext()) {
        this.goto(this.S_INPUT);
    }
};
/*
    S_MAKEBOARD
*/
MainControl.prototype.S_MAKEBOARD = function(first) {
    if (first)
    {
        var _=this;
        var f = function(){
            _.rprintx(3,2,""+G.qlevel+"-"+G.qnumber);
            _.rprintx(9,2,G.qname);
            _.rprintx(15,4,"STEP  - "+ G.step);
            _.rprintx(15,5,"LIFE  - "+ G.life);
            _.rprintx(15,6,"Q LMT - "+ G.qlimit);
            if (G.qexp!=null) _.rprintx(15,8,G.qexp);
            if (G.qtip!=null) _.rprintx(15,10,G.qtip);
            _.rmakeboard(3,3);
            G.np_x = 3;
            G.np_y = 15;
            _.rprintxc(G.np_x,G.np_y,"0123456789","white","#489ee2");
        };
        G.RS.push(f);
    }
    if (!this.hasnext()) {
        this.goto(this.S_COPY);
    }
    if (this.hasnext()) {
        this.setnowait();
    }
};
/*
    S_PICKUP
*/
MainControl.prototype.S_PICKUP = function(first) {
    if (first)
    {
        var _=this;
        var f = function() {
            if (G.pickobj!=null) {
                G.pickobj.style.backgroundColor="red";
            }
        };
        G.RS.push(f);
    }
    if (!this.hasnext()) {
        this.goto(this.S_TARGET);
    }
    if (this.hasnext()) {
        this.setnowait();
    }
};
/*
    S_PRINT_TITLE
*/
MainControl.prototype.S_PRINT_TITLE = function(first) {
    if (first)
    {
        var _ = this;
        var f = function() {
            _.rprintx(0,0,"PROTOTYPE (C)2020 PROGRAMANIC");
        };
        G.RS.push(f);
    }
    if (!this.hasnext()) {
        this.goto(this.S_MAKEBOARD);
    }
    if (this.hasnext()) {
        this.setnowait();
    }
};
/*
    S_PRINT2
*/
MainControl.prototype.S_PRINT2 = function(first) {
    if (first)
    {
        var _ = this;
        var f = function() {
            _.printx(0,1,"We are the ailans from the universe");
        };
        this.RS.push(f);
    }
    if (!this.hasnext()) {
        this.goto(this.S_PRINT3);
    }
    if (this.hasnext()) {
        this.setnowait();
    }
};
/*
    S_PRINT3
*/
MainControl.prototype.S_PRINT3 = function(first) {
    if (first)
    {
        var _ = this;
        var f = function() {
            _.printxc(0,2,"red character","red");
        };
        this.RS.push(f);
    }
    if (!this.hasnext()) {
        this.goto(this.S_PRINT4);
    }
    if (this.hasnext()) {
        this.setnowait();
    }
};
/*
    S_PRINT4
*/
MainControl.prototype.S_PRINT4 = function(first) {
    if (first)
    {
        var _ = this;
        var f = function() {
            _.printxc(0,3,"red character and whiteback","red","white");
        };
        this.RS.push(f);
    }
    if (this.hasnext()) {
        this.setnowait();
    }
};
/*
    S_RENDER1
*/
MainControl.prototype.S_RENDER1 = function(first) {
    if (first)
    {
        this.render();
    }
    if (!this.hasnext()) {
        this.goto(this.S_TICK);
    }
    if (this.hasnext()) {
        this.setnowait();
    }
};
/*
    S_SetInstance
    インスタンス登録
*/
MainControl.prototype.S_SetInstance = function(first) {
    if (first)
    {
        G.mcon = this;
    }
    if (!this.hasnext()) {
        this.goto(this.S_ADDEVENTS);
    }
};
/*
    S_START
*/
MainControl.prototype.S_START = function(first) {
    this.goto(this.S_SetInstance);
    this.setnowait();
};
/*
    S_STEP
*/
MainControl.prototype.S_STEP = function(first) {
    this.bcon.step();
    if (!this.hasnext()) {
        this.goto(this.S_PRINT_TITLE);
    }
    if (this.hasnext()) {
        this.setnowait();
    }
};
/*
    S_TARGET
*/
MainControl.prototype.S_TARGET = function(first) {
    if (first)
    {
        var _=this;
        var f = function() {
            if (G.targetobj!=null) {
                G.targetobj.style.backgroundColor="white";
            }
        };
        G.RS.push(f);
    }
    if (!this.hasnext()) {
        this.goto(this.S_BUTTON);
    }
    if (this.hasnext()) {
        this.setnowait();
    }
};
/*
    S_TICK
*/
MainControl.prototype.S_TICK = function(first) {
    if (!this.hasnext()) {
        this.goto(this.S_INPUT);
    }
};


// [STATEGO OUTPUT END]

// == write your code ==

