using System.Collections;
using System.Collections.Generic;
using System;

[Obsolete]
public class StateSequencer {

    public class Item
    {
        public Action<double> func;
    }

    Queue<Item> m_queue; //保持用キュー
    double m_elapsed;    //ステート経過時間

    Item m_curState;     //現在実行中セット
    Item m_nextState;    //次実行のセット

    public StateSequencer()
    { 
        m_queue = new Queue<Item>();
        m_curState  = null;
        m_nextState = null;
    }

    //実行登録
    public void Command(Action<double> func)
    {
        var item = new Item();
        item.func = func;
        m_queue.Enqueue(item);
    }

    //更新処理　上位関数から更新時呼び出しを想定
    public void Update(double deltatime)
    {
        m_elapsed += deltatime;

        if (m_curState == null)
        {
            if (m_queue.Count != 0)
            {
                m_nextState = m_queue.Dequeue();
            }
        }
        if (m_nextState != null)
        {
            m_curState  = m_nextState;
            m_nextState = null;
            m_elapsed = 0;
        }
        if(m_curState != null)
        {
            m_curState.func(m_elapsed);
        }
    }

    //ステートの終了告知用
    public void Done()
    {
        m_curState = null;
    }

    //キューが空かの確認
    public bool IsEmpty()
    {
        return m_queue==null ||  m_queue.Count==0;
    }

    ////キュー最後のアイテムのステート名
    //public string LastStateNameInQueue()
    //{
    //    if (m_queue==null || m_queue.Count==0) return null;

    //    string name = null;
    //    foreach(var i in m_queue)
    //    {
    //        name = i.name;
    //    }
    //    return name;
    // }
}