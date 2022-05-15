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
    public class ExcelConvertSettings
    {
        public string m_template_src;
        public string m_template_func;
        public string m_setting_ini;
        public string m_help_ini;
        public string m_items_ini;

        public void Init_template_src(ExcelDll ed)    { filedb_assert(); m_template_src = get_value(ed);                    }
        public void Update_template_src(ExcelDll ed)  { filedb_assert(); update_value(ed,m_template_src); ed.WriteSheet();  }

        public void Init_template_func(ExcelDll ed)   { filedb_assert();  m_template_func = get_value(ed);                   }
        public void Update_template_func(ExcelDll ed) { filedb_assert(); update_value(ed,m_template_func); ed.WriteSheet(); }

        public void Init_setting_ini(ExcelDll ed)     { filedb_assert(); m_setting_ini = get_value(ed);                     }
        public void Update_setting_ini(ExcelDll ed)   { filedb_assert(); update_value(ed,m_setting_ini);   ed.WriteSheet(); }

        public void Init_help_ini(ExcelDll ed)        { filedb_assert(); m_help_ini = get_value(ed);                        }
        public void Update_help_ini(ExcelDll ed)      { filedb_assert(); update_value(ed,m_help_ini);       ed.WriteSheet(); }

        public void Init_items_ini(ExcelDll ed)       { filedb_assert(); m_items_ini = get_value(ed);                        }
        public void Update_items_ini(ExcelDll ed)     { filedb_assert(); update_value(ed, m_items_ini);     ed.WriteSheet(); }

        private string get_value(ExcelDll ed)
        {
            if (ed == null) return string.Empty;

            //Excel.Range cell = null;
            var value = string.Empty;
            try {
                //cell = (Excel.Range)sheet.Cells[1,1];
                value = ed.GetStr(1,1);//(string)cell.Value2;
            }
            catch {
                value = string.Empty;
            }
            //finally {
            //    if (cell!=null)
            //    {
            //        Marshal.ReleaseComObject(cell);
            //        cell = null;
            //    }
            //}
            return value;
        }
        private void update_value(ExcelDll ed, string value)
        {
            //Excel.Range cell = null;
            try {
                //cell = (Excel.Range)sheet.Cells[1,1];
                //cell.Value2 = value;
                ed.SetStr(1,1,value);
            } catch
            { }
            //finally {
            //    if (cell!=null)
            //    {
            //        Marshal.ReleaseComObject(cell);
            //        cell = null;
            //    }
            //}
        }
        #region PSGG FILE W DATA 
        bool m_use_filedb = false;
        void filedb_assert() { if (m_use_filedb) throw new SystemException("{F8EDF2CA-B44B-4365-9D51-DC39F42A136C}"); }

        public void Init_template_src_byFileDB()    { m_use_filedb=true; m_template_src  = FileDbUtil.get_sheet_tempsrc_val();    }
        public void Init_template_func_byFileDB()   { m_use_filedb=true; m_template_func = FileDbUtil.get_sheet_tempfunc_val();   }
        public void Init_setting_ini_byFileDB()     { m_use_filedb=true; m_setting_ini   = FileDbUtil.get_sheet_setting_val();    }
        public void Init_help_ini_byFileDB()        { m_use_filedb=true; m_help_ini      = FileDbUtil.get_sheet_help_val();       }
        public void Init_items_ini_byFileDB()       { m_use_filedb=true; m_items_ini     = FileDbUtil.get_sheet_items_val();      }

        public void Update_template_src_byFileDB()  { m_use_filedb=true; FileDbUtil.set_sheet_tempsrc_val(m_template_src);    }
        public void Update_template_func_byFileDB() { m_use_filedb=true; FileDbUtil.set_sheet_tempfunc_val(m_template_func);  }
        public void Update_setting_ini_byFileDB()   { m_use_filedb=true; FileDbUtil.set_sheet_setting_val(m_setting_ini);     }
        public void Update_help_ini_byFileDB()      { m_use_filedb=true; FileDbUtil.set_sheet_help_val(m_help_ini);           }
        public void Update_items_ini_byFileDB()     { m_use_filedb=true; FileDbUtil.set_sheet_items_val(m_items_ini);         }

        #endregion
    }
}
