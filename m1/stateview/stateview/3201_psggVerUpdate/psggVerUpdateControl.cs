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

public partial class psggVerUpdateControl  {
   
    public Control m_parent;

    #region manager
    Action<bool> m_curfunc;
    Action<bool> m_nextfunc;

    bool         m_noWait;
    
    public void Update()
    {
        while(true)
        {
            var bFirst = false;
            if (m_nextfunc!=null)
            {
                m_curfunc = m_nextfunc;
                m_nextfunc = null;
                bFirst = true;
            }
            m_noWait = false;
            if (m_curfunc!=null)
            {   
                m_curfunc(bFirst);
            }
            if (!m_noWait) break;
        }
    }
    void Goto(Action<bool> func)
    {
        m_nextfunc = func;
    }
    bool CheckState(Action<bool> func)
    {
        return m_curfunc == func;
    }
    bool HasNextState()
    {
        return m_nextfunc != null;
    }
    void NoWait()
    {
        m_noWait = true;
    }
    #endregion
    #region gosub
    List<Action<bool>> m_callstack = new List<Action<bool>>();
    void GoSubState(Action<bool> nextstate, Action<bool> returnstate)
    {
        m_callstack.Insert(0,returnstate);
        Goto(nextstate);
    }
    void ReturnState()
    {
        var nextstate = m_callstack[0];
        m_callstack.RemoveAt(0);
        Goto(nextstate);
    }
    #endregion 

    public void Start()
    {
        Goto(S_START);
    }
    public bool IsEnd()     
    { 
        return CheckState(S_END); 
    }
    
    public void Run()
    {
		int LOOPMAX = (int)(1E+5);
		Start();
		for(var loop = 0; loop <= LOOPMAX; loop++)
		{
			if (loop>=LOOPMAX) throw new SystemException("Unexpected.");
			Update();
			if (IsEnd()) break;
		}
	}

	#region    // [PSGG OUTPUT START] indent(4) $/./$
//  psggConverterLib.dll converted from psggVerUpdateControl.xlsx.    psgg-file:doc\psggVerUpdateControl.psgg
    /*
        E_RESULT
    */
    public bool m_success;
    /*
        S_CHECK
        対象ファイルは古いバージョンか？
    */
    void S_CHECK(bool bFirst)
    {
        var b = is_old_psgg();
        // branch
        if (b) { Goto( S_WARNING ); }
        else { Goto( S_MSG ); }
    }
    /*
        S_CHECK_WRITABLE
    */
    void S_CHECK_WRITABLE(bool bFirst)
    {
        var b = check_writable();
        // branch
        if (b) { Goto( S_READ_PSGG_HEADER ); }
        else { Goto( S_MSG2 ); }
    }
    /*
        S_CREATE_BACKUP
    */
    public bool m_needbackup = false;
    void S_CREATE_BACKUP(bool bFirst)
    {
        //
        if (bFirst)
        {
            if (m_needbackup) {
                create_psgg_backup();
            }
        }
        //
        if (!HasNextState())
        {
            Goto(S_READ_EXCEL);
        }
    }
    /*
        S_CREATE_NEW_EXCEL
    */
    void S_CREATE_NEW_EXCEL(bool bFirst)
    {
        //
        if (bFirst)
        {
            create_new_excel();
        }
        //
        if (!HasNextState())
        {
            Goto(S_HIDE_BUSY);
        }
    }
    /*
        S_CREATE_NEW_PSGG
    */
    void S_CREATE_NEW_PSGG(bool bFirst)
    {
        //
        if (bFirst)
        {
            create_new_psgg();
        }
        //
        if (!HasNextState())
        {
            Goto(S_CREATE_NEW_EXCEL);
        }
    }
    /*
        S_END
    */
    void S_END(bool bFirst)
    {
    }
    /*
        S_HIDE_BUSY
    */
    void S_HIDE_BUSY(bool bFirst)
    {
        //
        if (bFirst)
        {
            hide_busy();
        }
        //
        if (!HasNextState())
        {
            Goto(S_MSG1);
        }
    }
    /*
        S_INIT
    */
    void S_INIT(bool bFirst)
    {
        //
        if (bFirst)
        {
            m_success = false;
        }
        //
        if (!HasNextState())
        {
            Goto(S_CHECK_WRITABLE);
        }
    }
    /*
        S_MSG
    */
    void S_MSG(bool bFirst)
    {
        //
        if (bFirst)
        {
            MessageBox.Show(WordStorage.Res.Get("pvdc_already",G.system_lang));
        }
        //
        if (!HasNextState())
        {
            Goto(S_END);
        }
    }
    /*
        S_MSG1
    */
    void S_MSG1(bool bFirst)
    {
        //
        if (bFirst)
        {
            MessageBox.Show(WordStorage.Res.Get("pvdc_done",G.system_lang));
        }
        //
        if (!HasNextState())
        {
            Goto(S_SUCCESS);
        }
    }
    /*
        S_MSG2
    */
    void S_MSG2(bool bFirst)
    {
        //
        if (bFirst)
        {
            MessageBox.Show(WordStorage.Res.Get("pvdc_readonly",G.system_lang));
        }
        //
        if (!HasNextState())
        {
            Goto(S_END);
        }
    }
    /*
        S_READ_EXCEL
    */
    void S_READ_EXCEL(bool bFirst)
    {
        //
        if (bFirst)
        {
            read_excel();
            G.psgg_file_w_data = true;
        }
        //
        if (!HasNextState())
        {
            Goto(S_CREATE_NEW_PSGG);
        }
    }
    /*
        S_READ_PSGG_HEADER
        PSGGのヘッダ情報を読む
    */
    void S_READ_PSGG_HEADER(bool bFirst)
    {
        //
        if (bFirst)
        {
            read_psgg();
        }
        //
        if (!HasNextState())
        {
            Goto(S_CHECK);
        }
    }
    /*
        S_SHOW_BUSY
    */
    void S_SHOW_BUSY(bool bFirst)
    {
        //
        if (bFirst)
        {
            show_busy();
        }
        //
        if (!HasNextState())
        {
            Goto(S_CREATE_BACKUP);
        }
    }
    /*
        S_START
    */
    void S_START(bool bFirst)
    {
        //
        if (!HasNextState())
        {
            Goto(S_INIT);
        }
    }
    /*
        S_SUCCESS
    */
    void S_SUCCESS(bool bFirst)
    {
        //
        if (bFirst)
        {
            m_success = true;
        }
        //
        if (!HasNextState())
        {
            Goto(S_END);
        }
    }
    /*
        S_WARNING
    */
    void S_WARNING(bool bFirst)
    {
        var bOK = (MessageBox.Show(WordStorage.Res.Get("pvdc_warn",G.system_lang),"Confirm",MessageBoxButtons.OKCancel) == DialogResult.OK);
        // branch
        if (bOK) { Goto( S_SHOW_BUSY ); }
        else { Goto( S_END ); }
    }


	#endregion // [PSGG OUTPUT END]

    float m_vernum;
    void read_psgg()
    {
        var s = FileDbUtil.read_version_from_psgg();
        m_vernum = ParseUtil.ParseFloat(s);
    }

    bool is_old_psgg()
    {
        return m_vernum == 1.0f;
    }
    void create_psgg_backup()
    {
        var backupfile = G.psgg_file + ".backup";
        while(File.Exists(backupfile))
        {
            backupfile += ".backup";
        }

        File.Copy(G.psgg_file, backupfile);

        // Excel backup
        if (m_needbackup)
        {
            var bqckup_excelfile = G.load_file;
            while(File.Exists(bqckup_excelfile))
            {
                bqckup_excelfile = Path.Combine(Path.GetDirectoryName(bqckup_excelfile),Path.GetFileNameWithoutExtension(bqckup_excelfile) + "__backup" + Path.GetExtension(bqckup_excelfile)  );
            }

            File.Copy(G.load_file,bqckup_excelfile);
        }

    }
    void read_excel()
    {
        G.file_db = new StateViewer_filedb.FileDb();
        G.file_db.LoadExcel(G.load_file, G.guid);
        G.file_db.write_filedb_all();
    }
    void create_new_psgg()
    {
        G.file_db.CreatePsgg(G.psgg_file);
    }

    void create_new_excel()
    {
        FileDbUtil.create_excel();
    }

    bool check_writable()
    {
        if (FileUtil.IsReadOnly(G.load_file))
        {
            return false;
        }
        if (FileUtil.IsReadOnly(G.psgg_file))
        {
            return false;
        }
        return true;
    }


    #region // open waiting dlg

    stateview._5000_ViewForm.dialog.UpgradePsggSubForm m_waitform;
    void show_busy()
    {
        m_waitform = new stateview._5000_ViewForm.dialog.UpgradePsggSubForm();
        m_waitform.Text = "Waiting";
        m_waitform.textBox1.Text = "Convering ...";
        m_waitform.textBox1.SelectionStart=m_waitform.textBox1.Text.Length;
        m_waitform.textBox1.SelectionLength = 0;
        m_waitform.Width = m_parent.Width;
        m_waitform.Height = m_parent.Height;
        m_waitform.Show(m_parent);
        m_waitform.textBox1.Location = PointUtil.Mod_Y(m_waitform.textBox1.Location,(int)((double)m_waitform.Height * 0.5f - 20) );


        m_waitform.Location = m_parent.Location;
        m_waitform.Refresh();
    }
    void hide_busy()
    {
        m_waitform.Hide();
    }
    #endregion
}

/*  :::: PSGG MACRO ::::
:psgg-macro-start

commentline=// {%0}

@branch=@@@
<<<?"{%0}"/^brifc{0,1}$/
if ([[brcond:{%N}]]) { Goto( {%1} ); }
>>>
<<<?"{%0}"/^brelseifc{0,1}$/
else if ([[brcond:{%N}]]) { Goto( {%1} ); }
>>>
<<<?"{%0}"/^brelse$/
else { Goto( {%1} ); }
>>>
<<<?"{%0}"/^br_/
{%0}({%1});
>>>
@@@

:psgg-macro-end
*/

