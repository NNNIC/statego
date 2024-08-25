<?php

class dispControl {

    // :::: PSGG MANAGER ::::

    private $m_curfunc  = NULL;
    private $m_nextfunc = NULL;
    private $m_noWait   = FALSE;

    private const MAX_CALL_STACK = 10;
    private $m_callstack = array();
    private $m_callstack_level = 0;

    function __construct() {
        $m_callstack = array_fill(0,self::MAX_CALL_STACK,'');
    }

    public function Update() {
        while(TRUE) {
            $bFirst = FALSE;
            if ($this->m_nextfunc!=NULL) {
                $this->m_curfunc = $this->m_nextfunc;
                $this->m_nextfunc = NULL;
                $bFirst = TRUE;
            }
            $this->m_noWait = FALSE;
            if ($this->m_curfunc!=NULL) {
                $this->call_state($this->m_curfunc, $bFirst);
            }
            if ($this->m_noWait == FALSE) {
                break;
            }
        }
    }

    public function GotoState($st) {
        $this->m_nextfunc = $st;
    }

    public function CheckState($st) {
        return $st == $this->m_curfunc;
    }
 
    public function HasNextState() {
        return $this->m_nextfunc != NULL;
    }

    public function NoWait() {
        $this->m_noWait = TRUE;
    }

    public function GoSubState($sub, $next) {
        if ($this->m_callstack_level >= self::MAX_CALL_STACK - 1) {
            echo 'CALL STACK OVER FLOW<br>';
            exit();
        }
        $this->m_callstack[$this->m_callstack_level++] = $next;
        $this->GotoState($sub);
    }

    public function ReturnState()
    {
        if ($this->m_callstack_level <= 0) {
            echo 'CALL STACK UNDER FLOW<br>';
            exit();
        }
        $this->m_callstack_level--;
        $st = $this->m_callstack[$this->m_callstack_level];
        
        $this->GotoState($st);        
    }
    
    function Start() {
        $this->GotoState('S_START');
    }

    function IsEnd() {
        return $this->CheckState('S_END');
    }

    // :::: PSG OUPUT ::::

    function call_state($state, $bFirst) {
        switch($state) {
            //[PSGG OUTPUT START] indent(12) $/^S_/->#case$
            //             psggConverterLib.dll converted from psgg-file:dispControl.psgg

            case 'S_DRAW': $this->S_DRAW($bFirst); break;
            case 'S_END': $this->S_END($bFirst); break;
            case 'S_GET_FILE': $this->S_GET_FILE($bFirst); break;
            case 'S_READFILE': $this->S_READFILE($bFirst); break;
            case 'S_READFILE1': $this->S_READFILE1($bFirst); break;
            case 'S_READFILE3': $this->S_READFILE3($bFirst); break;
            case 'S_SET_FILE': $this->S_SET_FILE($bFirst); break;
            case 'S_SET_FILE1': $this->S_SET_FILE1($bFirst); break;
            case 'S_START': $this->S_START($bFirst); break;


            //[PSGG OUTPUT END]
        }
    }

    //[PSGG OUTPUT START] indent(4) $/./$
    //             psggConverterLib.dll converted from psgg-file:dispControl.psgg

    /*
        S_DRAW
    */
    function S_DRAW($bFirst) {
        if ($bFirst)
        {
            $text = $this->data;
            $array = explode("\n", $text); // とりあえず行に分割
            //$array = array_map('trim', $array); // 各行にtrim()をかける
            $array = array_filter($array, 'strlen'); // 文字数が0の行を取り除く
            $array = array_values($array); // これはキーを連番に振りなおしてるだけ
            echo '<div class="mermaid">' . "\n";
            foreach($array as $i)
            {
                echo $i . "\n";
            }
            echo '</div><br>';
        }
        if ($this->HasNextState()==FALSE)
        {
            $this->GotoState('S_END');
        }
    }
    /*
        S_END
    */
    function S_END($bFirst)
    {
    }
    /*
        S_GET_FILE
    */
    function S_GET_FILE($bFirst) {
        if ($bFirst)
        {
            $this->file = $_GET['file'];
        }
        if (empty($this->file)) { $this->GotoState( 'S_SET_FILE1' ); }
        else { $this->GotoState( 'S_READFILE3' ); }
    }
    /*
        S_READFILE
    */
    function S_READFILE($bFirst) {
        if ($bFirst)
        {
            $data = file_get_contents('http://localhost/mermaid/psgg2mermaid/conv.php');
            echo $data;
        }
        if ($this->HasNextState()==FALSE)
        {
            $this->GotoState('S_READFILE1');
        }
    }
    /*
        S_READFILE1
    */
    function S_READFILE1($bFirst) {
        if ($bFirst)
        {
            echo 'end<br>';
        }
        if ($this->HasNextState()==FALSE)
        {
            $this->GotoState('S_END');
        }
    }
    /*
        S_READFILE3
    */
    function S_READFILE3($bFirst) {
        if ($bFirst)
        {
            $this->data = file_get_contents('http://statego.programanic.com/mermaid/psgg2mermaid/conv.php?file=' . $this->file );
        }
        if ($this->HasNextState()==FALSE)
        {
            $this->GotoState('S_DRAW');
        }
    }
    /*
        S_SET_FILE
    */
    function S_SET_FILE($bFirst) {
        if ($bFirst)
        {
            $this->file = 'https://raw.githubusercontent.com/NNNIC/psgg-haxe-sample/master/src/TestControl.psgg';
        }
        if ($this->HasNextState()==FALSE)
        {
            $this->GotoState('S_READFILE3');
        }
    }
    /*
        S_SET_FILE1
    */
    function S_SET_FILE1($bFirst) {
        if ($bFirst)
        {
            $this->file = 'https://raw.githubusercontent.com/NNNIC/psgg-ruby-sample/master/sample/TestControl.psgg';
        }
        if ($this->HasNextState()==FALSE)
        {
            $this->GotoState('S_READFILE3');
        }
    }
    /*
        S_START
    */
    function S_START($bFirst)
    {
        $this->GotoState('S_GET_FILE');
        $this->NoWait();
    }


    //[PSGG OUTPUT END]

    // :::: WRITE YOUR CODE HERE ::::

    protected $m_bYesNo;

    function br_YES($st) {
        if ($this->HasNextState()==FALSE) {
            if ($this->m_bYesNo==TRUE) {
                $this->GotoState($st);
            }
        }
    }

    function br_NO($st) {
        if ($this->HasNextState()==FALSE) {
            if ($this->m_bYesNo==FALSE) {
                $this->GotoState($st);
            }
        }
    }
}

/* :::: PSGG MACRO ::::
:psgg-macro-start

; This section has macro defines for converting.

; commentline format  {%0} will be replaced to a comment.
commentline=// {%0} 

@branch=@@@
<<<?"{%0}"/^brifc{0,1}$/
if ([[brcond:{%N}]]) { $this->GotoState( '{%1}' ); }
>>>
<<<?"{%0}"/^brelseifc{0,1}$/
elseif ([[brcond:{%N}]]) { $this->GotoState( '{%1}' ); }
>>>
<<<?"{%0}"/^brelse$/
else { $this->GotoState( '{%1}' ); }
>>>
<<<?"{%0}"/^br_/
$this->{%0}('{%1}');
>>>
@@@

#case=@@@
case '[[state]]': $this->[[state]]($bFirst); break;
<<<?state-typ/^loop$/
case '[[state]]_LoopCheckAndGosub____': $this->[[state]]_LoopCheckAndGosub____($bFirst); break;
case '[[state]]_LoopNext____': $this->[[state]]_LoopNext____($bFirst); break;
>>>
@@@

:psgg-macro-end
*/

?>