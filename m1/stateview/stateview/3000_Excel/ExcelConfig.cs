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
using System.Runtime.InteropServices;

namespace stateview
{
    public class ExcelConfig
    {
        public string m_configini;

        public void Init(ExcelDll ed) // current sheet
        {
            m_configini = string.Empty;
            try {
                //var cell = (Excel.Range)sheet.Cells[1,1];
                m_configini =  ed.GetStr(1,1); //(string)cell.Value2;
                //Marshal.ReleaseComObject(cell);
            }
            catch { }
        }

        //Excel.Range m_cell = null;
        public void Update(ExcelDll ed)
        {
            if (m_use_fiedb) throw new SystemException("{59C3108E-B83A-4FAE-8EEA-B500B69FF599}");
            try {
                //m_cell = (Excel.Range)sheet.Cells[1,1];

                //m_cell.Value2 = m_configini;
                ed.SetStr(1,1,m_configini);
                ed.WriteSheet();
            }
            catch { }
        }

        public void UpdateDestroy()
        {
            //if (m_cell!=null)
            //{
            //    Marshal.ReleaseComObject(m_cell);
            //}
        }

        public void SaveForce()
        {
            if (m_use_fiedb) throw new SystemException("{1F9D6071-8269-421F-B599-80F86487AA50}");

            var ed = new ExcelDll();
            ed.Load(G.load_file);
            ed.NewSheetForce(G.sheetconfig);
            Update(ed);
            ed.Dispose();
        }
        
        /*
            PSGG FILE W DATA
        */
        private bool m_use_fiedb = false;
        public void Init_byFileDB()
        {
            m_use_fiedb = true;
            m_configini = FileDbUtil.get_sheet_config_val();
        }
        public void Update_byFileDB()
        {
            FileDbUtil.set_sheet_config_val(m_configini);
            FileDbUtil.write_sheet_config();
            //FileDbUtil.create_psgg();
        }
        public void SaveForce_byFileDB()
        {
            Update_byFileDB();
        }
    }

}
