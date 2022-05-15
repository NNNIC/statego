using System;
using System.IO;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
public  partial class ExcelDll
{
    #region PSGG FILE W DATA 変更に伴って、Excelへのアクセスを厳格に管理。 psgg w data時は、特殊以外は閉じさせる
    public static bool Enabled=true;
    #endregion

    #region 準備
    private dynamic psggExcelLib { get {

            if (!Enabled) return null;

            if (!_psggExcelLibChecked)
            {
                _psggExcelLibChecked = true;

                try {
                    var exedir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    //var path = Path.Combine( Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "psggExcelWinLib.dll");
                    var path = Path.Combine( exedir, "psggExcelWinLib.dll");
                    if (!File.Exists(path)) return null;
                    var dll = Assembly.LoadFrom(path);
                    psggExcelWork = dll.GetType("psggExcelWinLib.Work");
                    _psggExcelLib = Activator.CreateInstance(dll.GetType("psggExcelWinLib.Work"));
                    //_psggExcelLib.bDebug = true;
                }
                catch (SystemException e) {
                    NoticeToUser_warning("Access Excel dll Error : " + e.Message);     
                }
            }
            return _psggExcelLib;
        } }
    private bool _psggExcelLibChecked = false;
    private dynamic _psggExcelLib = null;

    public Type psggExcelWork;
    #endregion

    #region cell
    public string GetError()
    {
        try {
            var s = (string)psggExcelLib.latest_error;
            return s;
        } catch (SystemException e)
        {
            NoticeToUser_warning("Cannot get latest_error field :" + e.Message);
            return "unkown error";
        }
    }

    public bool Load(string path)
    {
        try {
            if (psggExcelLib!=null)
            {
                psggExcelLib.Load(path);
                var err = GetError();
                if(err != null)
                {
                    NoticeToUser_warning("Exception Invoke : " + err);
                    return false;
                }
            }
            else
            {
                return false;
            }
        } catch (SystemException e)
        {
            NoticeToUser_warning("Exception Invoke : " + e.Message);
            //MessageBox.Show("Exception Invoke :" + e.Message);
            return false;
        }
        return true;
    }

    public bool Save()
    {
        try {
            var b = psggExcelLib.Save();
            var err = GetError();
            if(err != null)
            {
                NoticeToUser_warning("Exception Save : " + err);
                return false;
            }
            if (!b)
            {
                NoticeToUser_msgbox("Faild to save. Please note that the output does not match the file.");
                
            }
            return b;
        } catch (SystemException e)
        {
            NoticeToUser_warning("Exception Save : " + e.Message);
            return false;
        }
    }

    public bool SetSheet(string name)
    {
        try {
            if (psggExcelLib!=null)
            {
                var b = psggExcelLib.SetSheet(name);
                var err = GetError();
                if(err != null)
                {
                    NoticeToUser_warning("Exception SetSheet : " + err);
                    return false;
                }
                return b;
            }
            else
            {
                return false;
            }
        } catch (SystemException e)
        {
            NoticeToUser_warning("Exception SetSheet : " + e.Message);
            return false;
        }
    }

    public void NewSheet(string name)
    {
        try {
            psggExcelLib.NewSheet(name);
            var err = GetError();
            if(err != null)
            {
                NoticeToUser_warning("Exception NewSheet : " + err);
            }
        } catch (SystemException e)
        {
            NoticeToUser_warning("Exception NewSheet : " + e.Message);
        }
    }

    public void NewSheetForce(string name)
    {
        try {
            psggExcelLib.NewSheetForce(name);
            var err = GetError();
            if(err != null)
            {
                NoticeToUser_warning("Exception NewSheetForce : " + err);
            }
        } catch (SystemException e)
        {
            NoticeToUser_warning("Exception NewSheetForce : " + e.Message);
        }
    }

    public bool ReadSheet()
    {
        try {
            var b = psggExcelLib.ReadSheet();
            var err = GetError();
            if(err != null)
            {
                NoticeToUser_warning("Exception NewSheetForce : " + err);
                return false;
            }
            return b;
        } catch (SystemException e)
        {
            NoticeToUser_warning("Exception NewSheetForce : " + e.Message);
            return false;
        }
    }

    public bool WriteSheet()
    {
        try {
            var b = psggExcelLib.WriteSheet();
            var err = GetError();
            if(err != null)
            {
                NoticeToUser_warning("Exception WriteSheet : " + err);
                return false;
            }
            return b;
        } catch (SystemException e)
        {
            NoticeToUser_warning("Exception WriteSheet : " + e.Message);
            return false;
        }
    }

    public int MaxRow()
    {
        try {
            if (psggExcelLib!=null)
            {
                var i = psggExcelLib.MaxRow();
                var err = GetError();
                if(err != null)
                {
                    NoticeToUser_warning("Exception MaxRow : " + err);
                    return -1;
                }
                return i;
            }
            else
            {
                return -1;
            }


        } catch (SystemException e)
        {
            NoticeToUser_warning("Exception MaxRow : " + e.Message);
            return -1;
        }
    }

    public int MaxCol()
    {
        try {
            if (psggExcelLib!=null)
            {
                var i = psggExcelLib.MaxCol();
                var err = GetError();
                if(err != null)
                {
                    NoticeToUser_warning("Exception MaxCol : " + err);
                    return -1;
                }
                return i;
            }
            else
            {
                return -1;
            }
        } catch (SystemException e)
        {
            NoticeToUser_warning("Exception MaxCol : " + e.Message);
            return -1;
        }
    }
    public string GetStr(int row, int col)
    {
        try {
            var i = psggExcelLib.GetStr(row,col);
            var err = GetError();
            if(err != null)
            {
                NoticeToUser_warning("Exception GetStr #1 : " + err);
                return null;
            }
            return i;
        } catch (SystemException e)
        {
            NoticeToUser_warning("Exception GetStr #2 : " + e.Message);
            NoticeToUser_warning(row.ToString() + "," + col.ToString());
            return null;
        }
    }
    public void SetStr(int row, int col, string text)
    {
        try {
            psggExcelLib.SetStr(row,col,text);
            var err = GetError();
            if(err != null)
            {
                NoticeToUser_warning("Exception SetStr #1 : " + err);
            }
        } catch (SystemException e)
        {
            NoticeToUser_warning("Exception SetStr #2 : " + e.Message);
            NoticeToUser_warning(row.ToString() + "," + col.ToString());
        }
    }
    public void CopyFormat(int src_row, int src_col, int dst_row, int dst_col)
    {
        try {
            psggExcelLib.CopyFormat(src_row, src_col, dst_row, dst_col);
            var err = GetError();
            if (err != null)
            {
                NoticeToUser_warning("Exception CopyFormat #1 : " + err);
            }
        }
        catch (SystemException e)
        {
            NoticeToUser_warning("Exception CopyFormat #2 : " + e.Message);
            NoticeToUser_warning(dst_row.ToString() + "," + dst_col.ToString());
        }
    }
    public void InsertRow(int row)
    {
        try {
            psggExcelLib.InsertRow(row);
        }
        catch (SystemException e)
        {
            NoticeToUser_warning("Exception InsertRow : " + e.Message);
        }
    }
    public void RemoveRow(int row)
    {
        try {
            psggExcelLib.RemoveRow(row);
        }
        catch (SystemException e)
        {
            NoticeToUser_warning("Exception RemoveRow : " + e.Message);
        }        
    }
    public void CopyRow(int rowsrc, int rowdst)
    {
        try {
            psggExcelLib.CopyRow(rowsrc, rowdst);
        }
        catch (SystemException e)
        {
            NoticeToUser_warning("Exception CopyRow : " + e.Message);
        }        
    }
    public void CopyFormatRow(int rowsrc, int rowdst)
    {
        try {
            psggExcelLib.CopyFormatRow(rowsrc, rowdst);
        }
        catch (SystemException e)
        {
            NoticeToUser_warning("Exception CopyFormatRow : " + e.Message);
        }        
    }
    public int GetBackColor(int row, int col)
    {
        try {
            return psggExcelLib.GetBackColor(row,col);
        }
        catch (SystemException e)
        {
            NoticeToUser_warning("Exception GetBackColor : " + e.Message);
        }
        return -1;
    }
    public void SetBackColor(int row, int col, int backcolor)
    {
        try {
            psggExcelLib.SetBackColor(row,col,backcolor);
        }
        catch (SystemException e)
        {
            NoticeToUser_warning("Exception SetBackColor : " + e.Message);
        }
    }
    public void Dispose()
    {
        try {
            if (psggExcelLib!=null)
            {
                psggExcelLib.Dispose();
                var err = GetError();
                if(err != null)
                {
                    NoticeToUser_warning("Exception Dispose : " + err);
                }
            }
        } catch (SystemException e)
        {
            NoticeToUser_warning("Exception Dispose : " + e.Message);
        }
    }
    #endregion

    #region pic
    public void ReadPics()
    {
        try {
            if (psggExcelLib!=null)
            {
                psggExcelLib.ReadPics();
                var err = GetError();
                if(err != null)
                {
                    NoticeToUser_warning("Exception ReadPics : " + err);
                }
            }
            else
            {
                return;
            }
        } catch (SystemException e)
        {
            NoticeToUser_warning("Exception ReadPics : " + e.Message);
        }
    }
    public Bitmap GetBmp(int row, int col)
    {
        try {
            if (psggExcelLib!=null)
            {
                var bmp = psggExcelLib.GetBmp(row,col);
                var err = GetError();
                if(err != null)
                {
                    NoticeToUser_warning("Exception GetBmp : " + err);
                    return null;
                }
                return bmp;
            }
            else
            {
                return null;
            }
        } catch (SystemException e)
        {
            NoticeToUser_warning("Exception GetBmp : " + e.Message);
            return null;
        }
    }
    public bool SetBmp(int  row, int col, Bitmap bmp)
    {
        try {
            bool b = false;
            if (bmp == null)
            {
                b = DelBmp(row, col);
            }
            else
            {
                b = psggExcelLib.SetBmp(row,col,bmp);
            }
            var err = GetError();
            if(err != null)
            {
                NoticeToUser_warning("Exception GetBmp : " + err);
                return false;
            }
            return b;
        } catch (SystemException e)
        {
            NoticeToUser_warning("Exception GetBmp : " + e.Message);
            return false;
        }
    }
    public bool UpdateBmps()
    {
        try {
            var b = psggExcelLib.UpdateBmps();
            var err = GetError();
            if(err != null)
            {
                NoticeToUser_warning("Exception UpdateBmps : " + err);
                return false;
            }
            return b;
        } catch (SystemException e)
        {
            NoticeToUser_warning("Exception UpdateBmps : " + e.Message);
            return false;
        }
    }
    public bool DelBmp(int row, int col)
    {
        try {
            var b = psggExcelLib.DelBmp(row,col);
            var err = GetError();
            if (err != null)
            {
                NoticeToUser_warning("Exception UpdateBmps : " + err);
                return false;
            }
            return b;
        }
        catch (SystemException e)
        {
            NoticeToUser_warning("Exception DelBmps : " + e.Message);
            return false;
        }
    }
    #endregion

    #region for clone
    public string ReplaceWord(string excelfile, string sheetname, string target, string replace)
    {
        return psggExcelLib.ReplaceWord(excelfile, sheetname, target, replace);
    }
    public string ReadCellSpecial(string excelfile, string sheetname, int row, int col)
    {
        return psggExcelLib.ReadCellSpecial(excelfile, sheetname,  row,  col);
    }
    public string WriteCellSpecial(string excelfile, string sheetname, int row, int col, string val)
    {
        return psggExcelLib.WriteCellSpecial(excelfile, sheetname, row, col, val);
    }
    #endregion

}

