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


namespace StateViewer_filedb
{
    public partial class FileDb
    {
        /* See エクセル消滅作戦.pptx
        セーブエクセル・ロードエクセル　　　　　　    －－エクセルファイルに対して
        ライトFileDB・リードFileDB　－－内部FileDBに対して


        ■ エクセルロード・セーブ
          1. LoadExcel(エクセルファイル)  -- 現フォーマットを固定とする。 
          2. SaveExcel(エクセルファイル)  -- 既存も、新規も
　　　　*/

        private string m_guid;
        private string __dbroot = null;
        private string m_dbroot
        {
            get { 
                if (__dbroot == null)
                {
                    __dbroot = Path.Combine( Path.GetTempPath(), string.Format("~psggdb_{0}",m_guid) );
                }
                return __dbroot;
            }
        }
        private string m_statechart_folder         { get { return Path.Combine(m_dbroot, "v2","state-chart");} }
        private string m_statechart_manager_folder { get { return Path.Combine(m_statechart_folder,"0_man"); } }
        private string m_statechart_data_folder    { get { return Path.Combine(m_statechart_folder,"1_data");} }
        private string m_statechart_bmp_folder     { get { return Path.Combine(m_statechart_folder,"2_bmp"); } }
        private string m_various_folder            { get { return Path.Combine(m_dbroot, "v2","various");    } }
        private string m_temp_folder               { get { return Path.Combine(m_dbroot, "v2","temp");       } }

        private string m_statechart_manager_file   { get { return Path.Combine(m_statechart_manager_folder,"man.ini"); } }

        public string GetTempFolder() { return m_temp_folder; }
    }
}


