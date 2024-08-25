// == manager ==
var BoardControl = function() {
    this.curfunc   = null;
    this.nextfunc  = null;
    this.nowait    = false;
    
    this.callstack = [];
};
BoardControl.prototype.update = function() {
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
BoardControl.prototype.checkstate = function(st) {
    return this.curfunc === st;
};
BoardControl.prototype.goto = function(st) {
    this.nextfunc = st;
};
BoardControl.prototype.hasnext = function() {
    return this.nextfunc!=null;
}
BoardControl.prototype.setnowait = function() {
    this.nowait = true;
}
BoardControl.prototype.start = function() {
    this.goto(this.S_START);
};
BoardControl.prototype.is_end = function() {
    return this.checkstate(this.S_END);
};
BoardControl.prototype.run = function() {
    var LOOPMAX = 100000;
    this.start();
    for(var loop = 0; loop <= LOOPMAX; loop++)
    {
        if (loop>=LOOPMAX) alert("Unexpected!");
        this.update();
        if (this.is_end()) break;
    }
};
BoardControl.prototype.step = function() {
    var LOOPMAX = 100000;
    for(var loop = 0; loop <= LOOPMAX; loop++)
    {
        if (loop>=LOOPMAX) alert("Unexpected!");
        this.update();
        if (this.busy===false) break;
    }    
}
BoardControl.prototype.gosubstate = function(sb,nt) {
    this.callstack.push(nt);
    this.goto(sb);
};
BoardControl.prototype.returnstate = function() {
    this.goto(this.callstack.pop());
};

// [STATEGO OUTPUT START] indent(0) $/./$
//  psggConverterLib.dll converted from BoardControl.xlsx.    psgg-file:BoardControl.psgg
/*
    E_RND09
    乱数0～9
    int rnd09();
*/
BoardControl.prototype.rnd09 = function() {
    var n = G.urand(9);
    return n;
}
/*
    E_RND10
    乱数0～10
    int rnd010();
*/
BoardControl.prototype.rnd010 = function() {
    var n = G.urand(10);
    return n;
}
/*
    E_RND19
    乱数1～9
    int rnd19();
*/
BoardControl.prototype.rnd19 = function() {
    var n = G.urand(8);
    return n+1;
}
/*
    S_CHECKRETRY
*/
BoardControl.prototype.S_CHECKRETRY = function(first) {
    if (first)
    {
        G.button = null;
        G.butevt = null;
    }
    if (!this.hasnext()) {
        this.goto(this.S_RET002);
    }
    if (this.hasnext()) {
        this.setnowait();
    }
};
/*
    S_CHECKRETRY1
*/
BoardControl.prototype.S_CHECKRETRY1 = function(first) {
    if (first)
    {
        G.button = null;
        G.butevt = null;
        G.targetobj=null;
    }
    if (!this.hasnext()) {
        this.goto(this.S_RET003);
    }
    if (this.hasnext()) {
        this.setnowait();
    }
};
/*
    S_CLEAR1
*/
BoardControl.prototype.S_CLEAR1 = function(first) {
    if (first)
    {
        G.vclear();
    }
    if (!this.hasnext()) {
        this.goto(this.S_WT);
    }
};
/*
    S_CLEAR2
*/
BoardControl.prototype.S_CLEAR2 = function(first) {
    if (first)
    {
        G.vclear();
    }
    if (!this.hasnext()) {
        this.goto(this.S_CREATE_Q);
    }
};
/*
    S_CLEAR3
*/
BoardControl.prototype.S_CLEAR3 = function(first) {
    if (first)
    {
        G.vclear();
    }
    if (!this.hasnext()) {
        this.goto(this.S_CREATE_Q1);
    }
};
/*
    S_COUNTERUP
*/
BoardControl.prototype.S_COUNTERUP = function(first) {
    if (first)
    {
        this.counter++;
        this.counter = this.counter % G.gb_ymax;
    }
    if (this.counter == 0) { this.goto( this.S_CLEAR1 ); }
    else { this.goto( this.S_NEWSTATE ); }
    if (this.hasnext()) {
        this.setnowait();
    }
};
/*
    S_CREATE_Q
*/
BoardControl.prototype.S_CREATE_Q = function(first) {
    if (first)
    {
        G.qtip = null;
        var f = G.qlist[this.index];
        f();
        G.button = "Reset";
        G.butx = 16;
        G.buty = 12;
    }
    if (!this.hasnext()) {
        this.goto(this.S_PLAY);
    }
};
/*
    S_CREATE_Q1
*/
BoardControl.prototype.S_CREATE_Q1 = function(first) {
    if (first)
    {
        G.qtip = null;
        var f = G.qlist[this.index];
        f();
        G.button = "Reset";
        G.butx = 16;
        G.buty = 12;
    }
    if (!this.hasnext()) {
        this.goto(this.S_PLAY1);
    }
};
/*
    S_END
*/
BoardControl.prototype.S_END = function(first) {
};
/*
    S_EXP
    説明
*/
BoardControl.prototype.S_EXP = function(first) {
    if (first)
    {
        G.vclear();
        G.vprintx(0,0,"-遊び方-");
        G.vprintx(0,2,"ボード下から数字を選び");
        G.vprintx(0,3,"ボード上に置きます。");
        G.vprintx(0,4,"隣同士との合計が１０");
        G.vprintx(0,5,"になると同方向の数字が");
        G.vprintx(0,6,"全て消えます。");
        G.vprintx(0,7,"全数字を消して下さい。");
        G.button = "START";
        G.butx = 6;
        G.buty = 12;
        G.butevt = null;
    }
    if (G.butevt==null) { return; }
    G.button = null;
    G.butevt = null;
    G.vclear();
    if (!this.hasnext()) {
        this.goto(this.S_WT1);
    }
};
/*
    S_GS
*/
BoardControl.prototype.S_GS = function(first) {
    if (first)
    {
        G.qnumber = this.index + 1;
        G.step = 0;
        G.vclear();
        G.vprintx(1,3,"LEVEL " + G.qlevel);
        G.vprintx(1,5,"Q." + G.qnumber);
    }
    if (!this.hasnext()) {
        this.goto(this.S_WAIT6);
    }
};
/*
    S_GS1
*/
BoardControl.prototype.S_GS1 = function(first) {
    if (first)
    {
        G.qnumber = this.index + 1;
        G.step = 0;
        G.vclear();
        G.vprintx(1,3,"LEVEL " + G.qlevel);
        G.vprintx(1,5,"Q." + G.qnumber);
    }
    if (!this.hasnext()) {
        this.goto(this.S_WAIT8);
    }
};
/*
    S_GSB001
*/
BoardControl.prototype.S_GSB001 = function(first) {
    this.gosubstate(this.S_SBS002,this.S_EXP);
    this.setnowait();
};
/*
    S_GSB002
*/
BoardControl.prototype.S_GSB002 = function(first) {
    this.gosubstate(this.S_SBS004,this.S_GSB003);
    this.setnowait();
};
/*
    S_GSB003
    new state
*/
BoardControl.prototype.S_GSB003 = function(first) {
    if (G.qresult=="ok") { this.goto( this.S_LV1 ); }
    else { this.goto( this.S_EXP ); }
};
/*
    S_INIT
*/
BoardControl.prototype.S_INIT = function(first) {
    if (first)
    {
        this.mcon = null;
        this.busy = false;
    }
    if (!this.hasnext()) {
        this.goto(this.S_WAIT3);
    }
};
/*
    S_instance
*/
BoardControl.prototype.S_instance = function(first) {
    if (first)
    {
        G.bcon = this;
    }
    if (!this.hasnext()) {
        this.goto(this.S_INIT);
    }
};
/*
    S_L0EXP
    レベル０説明
*/
BoardControl.prototype.S_L0EXP = function(first) {
    if (first)
    {
        G.vclear();
        G.vprintx(3,2,"レベル０");
        G.vprintx(2,4,"１手でクリア");
        G.button = "NEXT";
        G.butx = 6;
        G.buty = 12;
        G.butevt = null;
    }
    if (G.butevt==null) { return; }
    G.button = null;
    G.butevt = null;
    if (!this.hasnext()) {
        this.goto(this.S_WT2);
    }
};
/*
    S_L0EXP1
    レベル１説明
*/
BoardControl.prototype.S_L0EXP1 = function(first) {
    if (first)
    {
        G.vclear();
        G.vprintx(3,2,"レベル１");
        G.vprintx(2,4,"2手でクリア");
        G.button = "NEXT";
        G.butx = 6;
        G.buty = 12;
        G.butevt = null;
    }
    if (G.butevt==null) { return; }
    G.button = null;
    G.butevt = null;
    if (!this.hasnext()) {
        this.goto(this.S_WT3);
    }
};
/*
    S_LOP001
    loop
*/
BoardControl.prototype.S_LOP001 = function(first) {
    this.index = 0;
    G.qresult="ok";
    this.goto(this.S_LOP001_LoopCheckAndGosub____);
    this.setnowait();
};
BoardControl.prototype.S_LOP001_LoopCheckAndGosub____ = function(first) {
    if (this.index < G.qlist.length && G.qresult=="ok" && G.butevt == null) this.gosubstate(this.S_SBS001,this.S_LOP001_LoopNext____);
    else               this.goto(this.S_CHECKRETRY);
    this.setnowait();
};
BoardControl.prototype.S_LOP001_LoopNext____ = function(first) {
    this.index++;
    this.goto(this.S_LOP001_LoopCheckAndGosub____);
    this.setnowait();
};
/*
    S_LOP002
    loop
*/
BoardControl.prototype.S_LOP002 = function(first) {
    this.index = 0;
    G.qresult="ok";
    this.goto(this.S_LOP002_LoopCheckAndGosub____);
    this.setnowait();
};
BoardControl.prototype.S_LOP002_LoopCheckAndGosub____ = function(first) {
    if (this.index < G.qlist.length && G.qresult=="ok" && G.butevt == null) this.gosubstate(this.S_SBS003,this.S_LOP002_LoopNext____);
    else               this.goto(this.S_CHECKRETRY1);
    this.setnowait();
};
BoardControl.prototype.S_LOP002_LoopNext____ = function(first) {
    this.index++;
    this.goto(this.S_LOP002_LoopCheckAndGosub____);
    this.setnowait();
};
/*
    S_LV0
*/
BoardControl.prototype.S_LV0 = function(first) {
    if (first)
    {
        G.qlist = [];
        G.qlevel=0;
        G.qnumber=0;
    }
    if (!this.hasnext()) {
        this.goto(this.S_L0EXP);
    }
};
/*
    S_LV0Q1
    チュートリアル
*/
BoardControl.prototype.S_LV0Q1 = function(first) {
    if (first)
    {
        var _ = this;
        var f = function() {
            G.qname = "S_LV0Q1";
            G.qlimit = 1;
            G.vprintx(5,5,"9");
            G.qtip = "１を隣に置こう！";
        };
        G.qlist.push(f);
    }
    if (!this.hasnext()) {
        this.goto(this.S_LV0Q4);
    }
};
/*
    S_LV0Q2
    チュートリアル
*/
BoardControl.prototype.S_LV0Q2 = function(first) {
    if (first)
    {
        var _ = this;
        var f = function() {
            G.qname = "S_LV0Q2";
            G.qlimit = 1;
            G.vprintx(5,5,"4 3");
            G.qtip = "4と3の間に3をおこう";
        };
        G.qlist.push(f);
    }
    if (!this.hasnext()) {
        this.goto(this.S_LV0Q3);
    }
};
/*
    S_LV0Q3
    チュートリアル
*/
BoardControl.prototype.S_LV0Q3 = function(first) {
    if (first)
    {
        var _ = this;
        var f = function() {
            G.qname = "S_LV0Q3";
            G.qlimit = 1;
            G.vprintx(5,5,"22 33");
            G.qtip = "間に5をおこう";
        };
        G.qlist.push(f);
    }
    if (!this.hasnext()) {
        this.goto(this.S_LV0Q5);
    }
};
/*
    S_LV0Q4
    チュートリアル
*/
BoardControl.prototype.S_LV0Q4 = function(first) {
    if (first)
    {
        var _ = this;
        var f = function() {
            G.qname = "S_LV0Q4";
            G.qlimit = 1;
            G.vprintx(1,5,"99999");
            G.qtip = "１を隣に置こう！";
        };
        G.qlist.push(f);
    }
    if (!this.hasnext()) {
        this.goto(this.S_LV0Q2);
    }
};
/*
    S_LV0Q5
    チュートリアル
*/
BoardControl.prototype.S_LV0Q5 = function(first) {
    if (first)
    {
        var _ = this;
        var f = function() {
            G.qname = "S_LV0Q5";
            G.qlimit = 1;
            G.vprintd(2,2,"dr","55 55");
            G.qtip = "間に0をおこう";
        };
        G.qlist.push(f);
    }
    if (!this.hasnext()) {
        this.goto(this.S_LV0Q6);
    }
};
/*
    S_LV0Q6
    チュートリアル
*/
BoardControl.prototype.S_LV0Q6 = function(first) {
    if (first)
    {
        var _ = this;
        var f = function() {
            G.qname = "S_LV0Q6";
            G.qlimit = 1;
            G.vprintd(2,8,"ur","444 222");
            G.qtip = "間に4をおこう";
        };
        G.qlist.push(f);
    }
    if (!this.hasnext()) {
        this.goto(this.S_LV0Q7);
    }
};
/*
    S_LV0Q7
    チュートリアル
*/
BoardControl.prototype.S_LV0Q7 = function(first) {
    if (first)
    {
        var _ = this;
        var f = function() {
            G.qname = "S_LV0Q7";
            G.qlimit = 1;
            G.vprintx(2,3," 2 ");
            G.vprintx(2,4,"2 2");
            G.vprintx(2,5," 2 ");
            G.qtip = "間に2をおこう";
        };
        G.qlist.push(f);
    }
    if (!this.hasnext()) {
        this.goto(this.S_LV0Q8);
    }
};
/*
    S_LV0Q8
*/
BoardControl.prototype.S_LV0Q8 = function(first) {
    if (first)
    {
        var _ = this;
        var f = function() {
            G.qname = "S_LV0Q8";
            G.qlimit=2;
            G.vprintx(2,0,"   2   ");
            G.vprintx(2,1,"   2   ");
            G.vprintx(2,2,"   2   ");
            G.vprintx(2,3,"222 222");
            G.vprintx(2,4,"   2   ");
            G.vprintx(2,5,"   2   ");
            G.vprintx(2,6,"   2   ");
            G.qtip = "間に2をおこう";
        };
        G.qlist.push(f);
    }
    if (!this.hasnext()) {
        this.goto(this.S_LV0Q9);
    }
};
/*
    S_LV0Q9
*/
BoardControl.prototype.S_LV0Q9 = function(first) {
    if (first)
    {
        var _ = this;
        var f = function() {
            G.qname = "S_LV0Q9";
            G.qlimit = 1;
            G.vprintx(0,0 ,"1    1    1");
            G.vprintx(0,1 ," 1   1   1");
            G.vprintx(0,2 ,"  1  1  1 ");
            G.vprintx(0,3 ,"   1 1 1  ");
            G.vprintx(0,4 ,"    111   ");
            G.vprintx(0,5 ,"11111 11111");
            G.vprintx(0,6 ,"    111   ");
            G.vprintx(0,7 ,"   1 1 1  ");
            G.vprintx(0,8 ,"  1  1  1 ");
            G.vprintx(0,9 ," 1   1   1");
            G.vprintx(0,10,"1    1    1");
            G.qtip="中央に2をおこう";
        };
        G.qlist.push(f);
    }
    if (!this.hasnext()) {
        this.goto(this.S_GSB002);
    }
};
/*
    S_LV1
*/
BoardControl.prototype.S_LV1 = function(first) {
    if (first)
    {
        G.qlist = [];
        G.qlevel=1;
        G.qnumber=0;
    }
    if (!this.hasnext()) {
        this.goto(this.S_L0EXP1);
    }
};
/*
    S_LV1Q1
*/
BoardControl.prototype.S_LV1Q1 = function(first) {
    if (first)
    {
        var _ = this;
        var f = function() {
            G.qname = "S_LV1Q1";
            G.qlimit = 2;
            var l = G.urandk(8,2);
            G.vprintx(0,_.rnd010(),G.urep("" + (l[0]+1),4)+G.urep("" + (l[1]+1),5) );
        };
        G.qlist.push(f);
    }
    if (!this.hasnext()) {
        this.goto(this.S_LV1Q2);
    }
};
/*
    S_LV1Q10
*/
BoardControl.prototype.S_LV1Q10 = function(first) {
    if (first)
    {
        var _ = this;
        var f = function() {
            G.qname = "S_LV1Q10";
            G.qlimit = 1;
            var l = G.urandk(8,2);
            var l1 = l[0] + 1;
            var l2 = l[1] + 1;
            G.vprintd(0,0,"dr", "" + l2 + G.urep("" + l1,9));
            G.vprintx(9,10,""+l2);
        };
        G.qlist.push(f);
    }
    if (!this.hasnext()) {
        this.goto(this.S_LV1Q11);
    }
};
/*
    S_LV1Q11
*/
BoardControl.prototype.S_LV1Q11 = function(first) {
    if (first)
    {
        var _ = this;
        var f = function() {
            G.qname = "S_LV1Q11";
            G.qlimit = 1;
            G.vprintd(1,G.gb_ymax-2,"ur",G.urep("2",9));
            G.vprintx(3,9,"2");
            G.vprintx(2,10,"2");
        };
        G.qlist.push(f);
    }
    if (!this.hasnext()) {
        this.goto(this.S_LV1Q7);
    }
};
/*
    S_LV1Q12
*/
BoardControl.prototype.S_LV1Q12 = function(first) {
    if (first)
    {
        var _ = this;
        var f = function() {
            G.qname = "S_LV1Q12";
            G.qlimit = 2;
            G.vprintx(0,0 ,"           ");
            G.vprintx(0,1 ," 1   1   1");
            G.vprintx(0,2 ,"  1  1  1 ");
            G.vprintx(0,3 ,"   1 1 1  ");
            G.vprintx(0,4 ,"   1112   ");
            G.vprintx(0,5 ," 1111 1111");
            G.vprintx(0,6 ,"    111   ");
            G.vprintx(0,7 ,"   1 1 1  ");
            G.vprintx(0,8 ,"  1  1  1 ");
            G.vprintx(0,9 ," 1   1   1");
            G.vprintx(0,10,"          ");
        };
        G.qlist.push(f);
    }
    if (!this.hasnext()) {
        this.goto(this.S_LV1Q10);
    }
};
/*
    S_LV1Q2
*/
BoardControl.prototype.S_LV1Q2 = function(first) {
    if (first)
    {
        var _ = this;
        var f = function() {
            G.qname = "S_LV1Q2";
            G.qlimit = 2;
            var rows = G.urandk(10,2);
            G.vprintx(0,rows[0],G.urep("" + _.rnd19(),10));
            G.vprintx(0,rows[1],G.urep("" + _.rnd19(),10));
        };
        G.qlist.push(f);
    }
    if (!this.hasnext()) {
        this.goto(this.S_LV1Q3);
    }
};
/*
    S_LV1Q3
*/
BoardControl.prototype.S_LV1Q3 = function(first) {
    if (first)
    {
        var _ = this;
        var f = function() {
            G.qname  = "S_LV1Q3";
            G.qlimit = 2;
            var cols = G.urandk(10,2);
            var l    = G.urandk(8,2);
            G.vprinty(cols[0] ,0 ,G.urep(""+(l[0]+1),10));
            G.vprinty(cols[1] ,1 ,G.urep(""+(l[1]+1),10));
        };
        G.qlist.push(f);
    }
    if (!this.hasnext()) {
        this.goto(this.S_LV1Q4);
    }
};
/*
    S_LV1Q4
*/
BoardControl.prototype.S_LV1Q4 = function(first) {
    if (first)
    {
        var _ = this;
        var f = function() {
            G.qname  = "S_LV1Q4";
            G.qlimit = 3;
            G.vprinty(_.rnd09(),0,G.urep("" + _.rnd19(),9));
            G.vprinty(_.rnd09(),0,G.urep("" + _.rnd19(),9));
            G.vprinty(_.rnd09(),0,G.urep("" + _.rnd19(),9));
        };
        G.qlist.push(f);
    }
    if (!this.hasnext()) {
        this.goto(this.S_LV1Q5);
    }
};
/*
    S_LV1Q5
*/
BoardControl.prototype.S_LV1Q5 = function(first) {
    if (first)
    {
        var _ = this;
        var f = function() {
            G.qname  = "S_LV1Q5";
            G.qlimit = 2;
            G.vprintx(0,4,"7777 22222");
        };
        G.qlist.push(f);
    }
    if (!this.hasnext()) {
        this.goto(this.S_LV1Q6);
    }
};
/*
    S_LV1Q6
*/
BoardControl.prototype.S_LV1Q6 = function(first) {
    if (first)
    {
        var _ = this;
        var f = function() {
            G.qname = "S_LV1Q6";
            G.qlimit=2;
            G.vprinty(4,0,"7777 22222");
        };
        G.qlist.push(f);
    }
    if (!this.hasnext()) {
        this.goto(this.S_LV1Q12);
    }
};
/*
    S_LV1Q7
*/
BoardControl.prototype.S_LV1Q7 = function(first) {
    if (first)
    {
        var _ = this;
        var f = function() {
            G.qname = "S_LV1Q7";
            G.qlimit=1;
            G.vprintx(2,3," 22");
            G.vprintx(2,4,"2 2");
            G.vprintx(2,5," 0 ");
        };
        G.qlist.push(f);
    }
    if (!this.hasnext()) {
        this.goto(this.S_LV1Q8);
    }
};
/*
    S_LV1Q8
*/
BoardControl.prototype.S_LV1Q8 = function(first) {
    if (first)
    {
        var _ = this;
        var f = function() {
            G.qname = "S_LV1Q8";
            G.qlimit=2;
            G.vprintx(2,1,"   2   ");
            G.vprintx(2,2,"   2   ");
            G.vprintx(2,3,"222 222");
            G.vprintx(2,4,"   2   ");
            G.vprintx(2,5,"   2   ");
        };
        G.qlist.push(f);
    }
    if (!this.hasnext()) {
        this.goto(this.S_LV1Q9);
    }
};
/*
    S_LV1Q9
*/
BoardControl.prototype.S_LV1Q9 = function(first) {
    if (first)
    {
        var _ = this;
        var f = function() {
            G.qname = "S_LV1Q9";
            G.qlimit = 1;
            G.vprintx(0,0 ,"2    1    1");
            G.vprintx(0,1 ," 2   1   1");
            G.vprintx(0,2 ,"  2  1  1 ");
            G.vprintx(0,3 ,"   1 1 1  ");
            G.vprintx(0,4 ,"    111   ");
            G.vprintx(0,5 ,"11111 11111");
            G.vprintx(0,6 ,"    111   ");
            G.vprintx(0,7 ,"   1 1 1  ");
            G.vprintx(0,8 ,"  1  1  1 ");
            G.vprintx(0,9 ," 1   1   1");
            G.vprintx(0,10,"1    1    1");
        };
        G.qlist.push(f);
    }
    if (!this.hasnext()) {
        this.goto(this.S_GSB001);
    }
};
/*
    S_MSG
*/
BoardControl.prototype.S_MSG = function(first) {
    if (first)
    {
        G.vprintx(2,4,"CLEAR!");
    }
    if (!this.hasnext()) {
        this.goto(this.S_WAIT10);
    }
};
/*
    S_MSG1
*/
BoardControl.prototype.S_MSG1 = function(first) {
    if (first)
    {
        G.vprintx(2,4,"CLEAR!");
        G.qexp = null;
        G.qtip = null;
    }
    if (!this.hasnext()) {
        this.goto(this.S_WAIT11);
    }
};
/*
    S_NEWSTATE
*/
BoardControl.prototype.S_NEWSTATE = function(first) {
    if (first)
    {
        G.vprintx(0,this.counter,G.urep(""+this.counter,11));
    }
    if (!this.hasnext()) {
        this.goto(this.S_WAIT);
    }
    if (this.hasnext()) {
        this.setnowait();
    }
};
/*
    S_PLAY
*/
BoardControl.prototype.S_PLAY = function(first) {
    if (first)
    {
        G.qresult=null;
        G.pcon = new PlayControl();
        G.pcon.start();
    }
    G.pcon.update();
    if ((G.pcon.is_end()==false) && (G.butevt==null)) { return; }
    if (G.qresult=="ok") { this.goto( this.S_WAIT5 ); }
    else if (G.butevt!=null) { this.goto( this.S_RET001 ); }
    else { this.goto( this.S_RET001 ); }
};
/*
    S_PLAY1
*/
BoardControl.prototype.S_PLAY1 = function(first) {
    if (first)
    {
        G.qresult=null;
        G.pcon = new PlayControl();
        G.pcon.start();
    }
    G.pcon.update();
    if ((G.pcon.is_end()==false) && (G.butevt==null)) { return; }
    if (G.qresult=="ok") { this.goto( this.S_WAIT7 ); }
    else if (G.butevt!=null) { this.goto( this.S_RET004 ); }
    else { this.goto( this.S_RET004 ); }
};
/*
    S_RET001
*/
BoardControl.prototype.S_RET001 = function(first) {
    this.returnstate();
    this.setnowait();
};
/*
    S_RET002
*/
BoardControl.prototype.S_RET002 = function(first) {
    this.returnstate();
    this.setnowait();
};
/*
    S_RET003
*/
BoardControl.prototype.S_RET003 = function(first) {
    this.returnstate();
    this.setnowait();
};
/*
    S_RET004
*/
BoardControl.prototype.S_RET004 = function(first) {
    this.returnstate();
    this.setnowait();
};
/*
    S_SBS001
*/
BoardControl.prototype.S_SBS001 = function(first) {
    this.goto(this.S_GS);
    this.setnowait();
};
/*
    S_SBS002
*/
BoardControl.prototype.S_SBS002 = function(first) {
    this.goto(this.S_LOP001);
    this.setnowait();
};
/*
    S_SBS003
*/
BoardControl.prototype.S_SBS003 = function(first) {
    this.goto(this.S_GS1);
    this.setnowait();
};
/*
    S_SBS004
*/
BoardControl.prototype.S_SBS004 = function(first) {
    this.goto(this.S_LOP002);
    this.setnowait();
};
/*
    S_START
*/
BoardControl.prototype.S_START = function(first) {
    this.goto(this.S_instance);
    this.setnowait();
};
/*
    S_WAIT
    待つ
*/
BoardControl.prototype.S_WAIT = function(first) {
    if (first)
    {
        this.m_S_WAIT = Date.now() + 0.01 * 1000;
    }
    if (Date.now() < this.m_S_WAIT) { return; }
    if (!this.hasnext()) {
        this.goto(this.S_COUNTERUP);
    }
};
/*
    S_WAIT10
    待つ
*/
BoardControl.prototype.S_WAIT10 = function(first) {
    if (first)
    {
        this.m_S_WAIT10 = Date.now() + 0.5 * 1000;
    }
    if (Date.now() < this.m_S_WAIT10) { return; }
    if (!this.hasnext()) {
        this.goto(this.S_RET001);
    }
};
/*
    S_WAIT11
    待つ
*/
BoardControl.prototype.S_WAIT11 = function(first) {
    if (first)
    {
        this.m_S_WAIT11 = Date.now() + 0.5 * 1000;
    }
    if (Date.now() < this.m_S_WAIT11) { return; }
    if (!this.hasnext()) {
        this.goto(this.S_RET004);
    }
};
/*
    S_WAIT3
*/
BoardControl.prototype.S_WAIT3 = function(first) {
    if (first)
    {
        this.counter = 0;
    }
    if (!this.hasnext()) {
        this.goto(this.S_NEWSTATE);
    }
};
/*
    S_WAIT5
    待つ
*/
BoardControl.prototype.S_WAIT5 = function(first) {
    if (first)
    {
        this.m_S_WAIT5 = Date.now() + 0.5 * 1000;
    }
    if (Date.now() < this.m_S_WAIT5) { return; }
    if (!this.hasnext()) {
        this.goto(this.S_MSG);
    }
};
/*
    S_WAIT6
    待つ
*/
BoardControl.prototype.S_WAIT6 = function(first) {
    if (first)
    {
        this.m_S_WAIT6 = Date.now() + 1 * 1000;
    }
    if (Date.now() < this.m_S_WAIT6) { return; }
    if (!this.hasnext()) {
        this.goto(this.S_CLEAR2);
    }
};
/*
    S_WAIT7
    待つ
*/
BoardControl.prototype.S_WAIT7 = function(first) {
    if (first)
    {
        this.m_S_WAIT7 = Date.now() + 0.5 * 1000;
    }
    if (Date.now() < this.m_S_WAIT7) { return; }
    if (!this.hasnext()) {
        this.goto(this.S_MSG1);
    }
};
/*
    S_WAIT8
    待つ
*/
BoardControl.prototype.S_WAIT8 = function(first) {
    if (first)
    {
        this.m_S_WAIT8 = Date.now() + 1 * 1000;
    }
    if (Date.now() < this.m_S_WAIT8) { return; }
    if (!this.hasnext()) {
        this.goto(this.S_CLEAR3);
    }
};
/*
    S_WT
    待つ
*/
BoardControl.prototype.S_WT = function(first) {
    if (first)
    {
        this.m_S_WT = Date.now() + 0.5 * 1000;
    }
    if (Date.now() < this.m_S_WT) { return; }
    if (!this.hasnext()) {
        this.goto(this.S_EXP);
    }
};
/*
    S_WT1
    待つ
*/
BoardControl.prototype.S_WT1 = function(first) {
    if (first)
    {
        this.m_S_WT1 = Date.now() + 0.5 * 1000;
    }
    if (Date.now() < this.m_S_WT1) { return; }
    if (!this.hasnext()) {
        this.goto(this.S_LV0);
    }
};
/*
    S_WT2
    待つ
*/
BoardControl.prototype.S_WT2 = function(first) {
    if (first)
    {
        this.m_S_WT2 = Date.now() + 0.5 * 1000;
    }
    if (Date.now() < this.m_S_WT2) { return; }
    if (!this.hasnext()) {
        this.goto(this.S_LV0Q1);
    }
};
/*
    S_WT3
    待つ
*/
BoardControl.prototype.S_WT3 = function(first) {
    if (first)
    {
        this.m_S_WT3 = Date.now() + 0.5 * 1000;
    }
    if (Date.now() < this.m_S_WT3) { return; }
    if (!this.hasnext()) {
        this.goto(this.S_LV1Q1);
    }
};


// [STATEGO OUTPUT END]

// == write your code ==

