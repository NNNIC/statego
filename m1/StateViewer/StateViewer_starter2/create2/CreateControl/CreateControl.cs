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

public partial class CreateControl {

    public object m_inputObject;

    Button m_but_g1next   { get { return G.V.button_g1ok;     } }
    Button m_but_g1cancel { get { return G.V.button_g1cancel; } }

    Button m_but_g2next   { get { return G.V.button_g2ok;     } }
    Button m_but_g2back   { get { return G.V.button_g2back;   } }

    Button m_but_g3next   { get { return G.V.button_g3ok;     } }
    Button m_but_g3back   { get { return G.V.button_g3back;   } }

    Button m_but_g4next   { get { return G.V.button_g4ok;     } }
    Button m_but_g4back   { get { return G.V.button_g4back;   } }

    Button m_but_g5next   { get { return G.V.button_g5ok;     } }
    Button m_but_g5back   { get { return G.V.button_g5back;   } }

    Button m_but_g6create { get { return G.V.button_g6ok;     } }
    Button m_but_g6back   { get { return G.V.button_g6back;   } }
    Button m_but_g6cancel { get { return G.V.button_g6cancel; } }

    bool m_bOKNG;

    void clear_input()
    {
        m_inputObject = null;
    }

    bool wait_object()
    {
        return m_inputObject != null;
    }

    //void br_G4NEXT(Action<bool> st)
    //{
    //    if (!HasNextState())
    //    {
    //        if (m_inputObject == m_but_g4next)
    //        {
    //            SetNextState(st);
    //        }
    //    }
    //}
    //void br_G6BACK(Action<bool> st)
    //{
    //    if (!HasNextState())
    //    {
    //        if (m_inputObject == m_but_g6back)
    //        {
    //            SetNextState(st);
    //        }
    //    }
    //}
    //void br_OTHER(Action<bool> st)
    //{
    //    if (!HasNextState())
    //    {
    //        SetNextState(st);
    //    }
    //}
    //void request_skip()
    //{
    //    if (m_inputObject == m_but_g4next)
    //    {
    //        G.V.movecontrol_req(GroupAnimControl.REQ.G4toG6);
    //    }
    //    if (m_inputObject == m_but_g6back)
    //    {
    //        G.V.movecontrol_req(GroupAnimControl.REQ.G6toG4);
    //    }
    //}
    void set_disables()
    {
        m_but_g2next.Enabled = false;
        m_but_g4next.Enabled = false;
    }
    
    [Obsolete]
    void enable_g2next_if_has_prefix()
    {
        m_bOKNG = !string.IsNullOrEmpty(G.V.textBoxPrefix.Text);
        if (m_bOKNG)
        {
            m_but_g2next.Enabled = true;
        }
    }
    void enable_g2next_if_has_statemachinename()
    {
        m_bOKNG = false;
        var text = G.V.textBoxStateMachineName.Text;
        text = text.Trim();
        m_bOKNG = (
            !string.IsNullOrEmpty(text) 
            &&
            (RegexUtil.Get1stMatch(@"[_a-zA-Z][_a-zA-Z0-9]*", text) == text)
            );
        if (m_bOKNG)
        {
            m_but_g2next.Enabled = true;
        }
    }
    void enable_g4next_if_has_folders()
    {
        m_bOKNG = !string.IsNullOrEmpty(G.V.textBoxDocFolder.Text) 
                    &&
                  !string.IsNullOrEmpty(G.V.textBoxGenFolder.Text);
        if (m_bOKNG)
        {
            m_but_g4next.Enabled = true;
        }
    }
    
    void br_OK(Action<bool> st)
    {
        if (!HasNextState())
        {
            if (m_bOKNG)
            {
                SetNextState(st);
            }
        }
    }        
    void br_NG(Action<bool> st)
    {
        if (!HasNextState())
        {
            if (!m_bOKNG)
            {
                SetNextState(st);
            }
        }
    }
}
