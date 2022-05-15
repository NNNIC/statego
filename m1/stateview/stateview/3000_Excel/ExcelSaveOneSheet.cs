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
using G = stateview.Globals;
using DStateData = stateview.Draw.DrawStateData;
using EFU = stateview._5300_EditForm.EditFormUtil;
using SS = stateview.StateStyle;
using DS = stateview.DesignSpec;
//>>>


namespace stateview
{
    /// <summary>
    /// １シートのみ更新する
    /// </summary>
    public class ExcelSaveOneSheet
    {
        public static void WriteTemplateSource()
        {
            var file = G.load_file;
            var ed = new ExcelDll();
            ed.Load(file);
            ed.NewSheetForce(G.sheettempsrc);
            G.excel_convertsettings.Update_template_src(ed);
            ed.Save();
            ed.Dispose();
        }
        public static void WriteTemplateFunction()
        {
            var file = G.load_file;
            var ed = new ExcelDll();
            ed.Load(file);
            ed.NewSheetForce(G.sheettempfunc);
            G.excel_convertsettings.Update_template_func(ed);
            ed.Save();
            ed.Dispose();
        }
        public static void WriteSettings()
        {
            var file = G.load_file;
            var ed = new ExcelDll();
            ed.Load(file);
            ed.NewSheetForce(G.sheetsetting);
            G.excel_convertsettings.Update_setting_ini(ed);
            ed.Save();
            ed.Dispose();
        }
        public static void WriteHelp()
        {
            var file = G.load_file;
            var ed = new ExcelDll();
            ed.Load(file);
            ed.NewSheetForce(G.sheethelp);
            G.excel_convertsettings.Update_help_ini(ed);
            ed.Save();
            ed.Dispose();
        }
        public static void WriteItemsInfo()
        {
            var file = G.load_file;
            var ed = new ExcelDll();
            ed.Load(file);
            ed.NewSheetForce(G.sheetitems);
            G.excel_convertsettings.Update_items_ini(ed);
            ed.Save();
            ed.Dispose();
        }
    }
}
