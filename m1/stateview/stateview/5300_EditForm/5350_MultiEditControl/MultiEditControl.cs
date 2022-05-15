//<<<include=using.txt
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
//using Excel = Microsoft.Office.Interop.Excel;
//using Office = Microsoft.Office.Core;
using G=stateview.Globals;
using DStateData=stateview.Draw.DrawStateData;
using EFU=stateview._5300_EditForm.EditFormUtil;
using SS=stateview.StateStyle;
using DS=stateview.DesignSpec;
//>>>

using stateview._5300_EditForm;

namespace stateview
{
    /*
        Viewerからダブルクリックでオープンするエディタを管理
        ※エディタのタイプが複数になったため、ここで管理する。

        管理するエディタ
        1．EditForm --- これまで使ってきたもの。ステートの値を直接編集
        2. EditForm_buttonForm --- ボタンとクリックの編集用
        3. EditForm_lvlGoroForm --- レベルの行先編集用
    */
    internal class MultiEditControl
    {
        public MultiEditControl()
        {
            //Console.Write("!!!");
        }

        internal enum Mode
        {
            None,
            //Done,

            EditForm,
            ButtonForm,
            //LvlGotoForm OBS
        }

        internal Mode m_req;
        internal string m_state;

        Form                        m_parentForm;

        public Form                 m_form;
        public EditForm             m_editform    { get { return (EditForm)m_form; } }
        //OBS public EditForm_buttonForm  m_buttonform  { get { return (EditForm_buttonForm)m_form; }  }
        //OBS public EditForm_lvlGotoForm m_lvlgotoform { get { return (EditForm_lvlGotoForm)m_form; } }

        public DialogResult         DialogResult  { get { return m_form!=null ? m_form.DialogResult : DialogResult.None; } }

        bool                        m_bDone;

        public void Update()
        {
            if (m_req != Mode.None)
            {
                switch(m_req)
                {
                case Mode.EditForm:    m_form = new EditForm();             m_form.Text = "Edit " + m_state;     break;
                //OBS case Mode.ButtonForm:  m_form = new EditForm_buttonForm();  break;
                //OBS case Mode.LvlGotoForm: m_form = new EditForm_lvlGotoForm(); break;
                }

                if (m_form!=null) m_form.Show(m_parentForm /*G.view_form*/);

                m_req = Mode.None;
            }

            m_bDone = (DialogResult != DialogResult.None);
            if (m_bDone)
            {
                if (DialogResult != DialogResult.Cancel)
                {
                    if (m_form!=null)
                    {
                        G.req_redraw();
                        m_form = null;
                    }
                }
            }
        }

        // これまで通りにeditformをオープンさせて、変更のとっかかりをつくる
        public void Open(string state,Form parent)
        {
            m_parentForm = parent;
            m_bDone      = false;

            //OBS
            //var type = string.Empty;
            //
            //if (G.JX) {
            //    type = G.excel_program.GetString(state,"!type");
            //}
            //
            //switch (type) {
            //    case "standard":    m_req = Mode.ButtonForm;  break;
            //    case "level":       m_req = Mode.LvlGotoForm; break;
            //    default:            m_req = Mode.EditForm;    break;
            //}

            m_req = Mode.EditForm;
            m_state = state;

            //Update();
        }

        public bool IsDone()
        {
            return (m_form==null || m_bDone);
        }

        //public void Done()
        //{
        //    m_req = Mode.Done;
        //}


    }
}
