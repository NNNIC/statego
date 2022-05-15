using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



public static class Coroutine
{
    static Dictionary<int,IEnumerator> m_list = new Dictionary<int, IEnumerator>();
    static int m_lastindex = 0;
    public static void Update()
    {
        var keys = new List<int>(m_list.Keys);
        foreach(var k in keys)
        {
            var p = m_list[k];
            if (p!=null)
            {
                var b = p.MoveNext();
                if (!b)
                {
                    m_list[k] = null;
                }
            }
        }
        foreach(var k in m_list.Keys)
        {
            var p = m_list[k];
            if (p==null)
            {
                m_list.Remove(k);
                break;
            }
        }
    }
    public static int Start(IEnumerator ie)
    {
        var idx = m_lastindex;
        m_lastindex++;
        m_list.Add(idx,ie);
        return idx;
    }
    public static bool IsRunning(int index)
    {
        if (m_list.ContainsKey(index))
        {
            return true;
        }
        return false;
    }
    public static bool IsRunning()
    {
        return m_list!=null && m_list.Count > 0;
    }
}

