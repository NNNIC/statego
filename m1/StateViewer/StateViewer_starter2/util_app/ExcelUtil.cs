using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;


namespace StateViewer_starter2
{
    public class ExcelUtil
    {
        //xlsファイルに 指定シートを値付きで追加
        //※　値は 1,1の部分に格納される
        //※  Open, save and close 
        //public static void AddSheetWithValue(string xlsfile, string sheetname, string value)
        //{
        //    var ew = new ExcelDll(); // ExcelWork();

        //    ew.Load(xlsfile);
        //    ew.NewSheetForce(sheetname);

        //    //var sheet = ew.GetSheet();
        //    //var cell = (Excel.Range)sheet.Cells[1,1];
        //    //cell.Value = value;

        //    ew.SetStr(1,1,value);
        //    ew.Save();

        //    //Marshal.ReleaseComObject(cell);
        //    //cell = null;

        //    ew.Dispose();
        //}

        //xlsファイルに、指定シートと値のセットを追加
        public static void AddSheetWithValue(string xlsfile, Dictionary<string,string> sheetvalue )
        {
            var ew =  new ExcelDll();  //new ExcelWork();
            ew.Load(xlsfile);
            
            foreach(var sheetname in sheetvalue.Keys)
            {
                //ew.SetSheet(sheetname);
                //if (ew.GetSheet()==null)
                //{
                //    ew.NewSheet(sheetname);
                //}
                //var sheet = ew.GetSheet();
                ew.NewSheetForce(sheetname);

                //var cell = (Excel.Range)sheet.Cells[1,1];
                //cell.Value = sheetvalue[sheetname];

                ew.SetStr(1,1,sheetvalue[sheetname]);

                ew.WriteSheet();

                //Marshal.ReleaseComObject(cell);
                //cell = null;                
            }

            ew.SetSheet(WordStorage.Store.sheetchart);

            ew.Save();
            ew.Dispose();
        }
    }
}
