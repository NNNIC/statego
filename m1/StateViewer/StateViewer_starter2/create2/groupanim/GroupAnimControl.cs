using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StateViewer_starter2;
using G=StateViewer_starter2.CreateNewForm;

public partial class GroupAnimControl  {
    private CreateNewForm m_form = CreateNewForm.V;

    public enum REQ
    {
        NONE,
        G1toG2,
        G2toG3,
        G3toG4,
        G4toG5,
        G5toG6,

        G6toG5,
        G5toG4,
        G4toG3,
        G3toG2,
        G2toG1,

        //special
        G4toG6,
        G6toG4,

        G2toG6,
        G6toG2
    }

    public REQ m_req = REQ.NONE;

    GroupBox m_gb0;
    GroupBox m_gb1;

    Point    m_pp;
    Point    m_p0;
    Point    m_p1;

    GroupBox Gb(int i)
    {
        switch(i)
        {
        case 1: return m_form.groupBox1;
        case 2: return m_form.groupBox2;
        case 3: return m_form.groupBox3;
        case 4: return m_form.groupBox4;
        case 5: return m_form.groupBox5;
        default: return m_form.groupBox6;
        }
    }

    void init()
    {
        m_req = REQ.NONE;
        m_p0 = m_form.groupBox1.Location;
        m_p1 = m_form.groupBox2.Location;
        //m_pp = PointUtil.Add_X(m_p0, m_p0.X -  m_p1.X );
        m_pp = PointUtil.Mod_X(m_p0, -  m_p1.X - 10);
    }

    void br_MOVE(Action<bool> st, string s)
    {
        if (HasNextState()) return;

        var str = s.Substring(3);
        var id = EnumUtil.Parse<REQ>(str,REQ.NONE);
        if (m_req == id)
        {
            m_req = REQ.NONE;
            SetNextState(st);
        }
    }

    void setnext(int s, int g)
    {
        m_gb0 = Gb(s);
        m_gb1 = Gb(g);
    }
    void setback(int s, int g)
    {
        m_gb0 = Gb(g);
        m_gb1 = Gb(s);
    }

    //void set_groupbox()
    //{
    //    m_gb0 = m_form.groupBox1;
    //    m_gb1 = m_form.groupBox2;
    //}

    PointF m_start0;
    PointF m_goal0;
    PointF m_start1;
    PointF m_goal1;
    int   m_cnt;
    int   m_cur;

    #region movefwd
    void movefwd_start(float t)
    {
        m_start0 = m_p0;
        m_goal0  = m_pp;

        m_start1 = m_p1;
        m_goal1  = m_p0;

        m_cnt = (int)(100f * t);
        m_cur = 0;
    }
    void movefwd_update()
    {
        var pos1 = MathX.Lerp(m_start1,m_goal1,(float)m_cur/m_cnt);
        m_gb1.Location = Point.Truncate(pos1);

        var pos0 = MathX.Lerp(m_start0,m_goal0,(float)m_cur/m_cnt);
        m_gb0.Location = Point.Truncate(pos0);

    }
    bool movefwd_isdone()
    {
        m_cur++;
        return (m_cur > m_cnt);
    }
    #endregion

    #region movebk
    void movebk_start(float t)
    {
        m_start0 = m_pp;
        m_goal0  = m_p0;

        m_start1 = m_p0;
        m_goal1  = m_p1;

        m_cnt = (int)(100f * t);
        m_cur = 0;
    }
    void movebk_update()
    {
        var pos1 = MathX.Lerp(m_start1,m_goal1,(float)m_cur/m_cnt);
        m_gb1.Location = Point.Truncate(pos1);

        var pos0 = MathX.Lerp(m_start0,m_goal0,(float)m_cur/m_cnt);
        m_gb0.Location = Point.Truncate(pos0);

    }
    bool movebk_isdone()
    {
        m_cur++;
        return (m_cur > m_cnt);
    }
    #endregion

}
