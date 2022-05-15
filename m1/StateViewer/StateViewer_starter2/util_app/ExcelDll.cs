using System;
using System.IO;
using System.Drawing;
using System.Reflection;

public  partial class ExcelDll
{
    #region 準備
    public dynamic psggExcelLib { get {
            if (!_psggExcelLibChecked)
            {
                _psggExcelLibChecked = true;

                try {
                    var path = Path.Combine( Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "psggExcelWinLib.dll");
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
        //var f = psggExcelWork.GetField("latest_error");
        //if (f==null)
        //{
        //    NoticeToUser_warning("Cannot file field");
        //    return null;
        //}     
        //var v = f.GetValue(psggExcelLib);
        //return  (v!=null) ? (string)v : null;

        try {
            var s = (string)psggExcelLib.latest_error;
            return s;
        } catch (SystemException e)
        {
            NoticeToUser_warning("Cannot get latest_error field :" + e.Message);
            return "unkown error";
        }
    }

    public void Load(string path)
    {
        //var m = psggExcelWork.GetMethod("Load");
        //if (m==null)
        //{
        //    NoticeToUser_warning("Cannot find Load Method");
        //    return;
        //}
        //try {
        //    var ret = m.Invoke(psggExcelLib,new object[] { path });
        //    var err = GetError();
        //    if (err!=null)
        //    {
        //        NoticeToUser_warning("Exception Invoke : " + err);
        //    }
        //}
        //catch (SystemException e) {
        //    NoticeToUser_warning("Exception Invoke : " + e.Message);
        //}
        try {
            psggExcelLib.Load(path);
            var err = GetError();
            if(err != null)
            {
                NoticeToUser_warning("Exception Invoke : " + err);
            }
        } catch (SystemException e)
        {
            NoticeToUser_warning("Exception Invoke : " + e.Message);
        }
    }

    public bool Save()
    {
        //var m = psggExcelWork.GetMethod("Save");
        //if (m==null)
        //{
        //    NoticeToUser_warning("Cannot find Save Method");
        //    return;
        //}
        //try {
        //    var ret = m.Invoke(psggExcelLib, new object[] { });
        //    var err = GetError();
        //    if (err!=null)
        //    {
        //        NoticeToUser_warning("Exception Save : " + err);
        //    }
        //}
        //catch (SystemException e) {
        //    NoticeToUser_warning("Exception Load : " + e.Message);
        //}
        try {
            var b = psggExcelLib.Save();
            var err = GetError();
            if(err != null)
            {
                NoticeToUser_warning("Exception Save : " + err);
                return false;
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
            var b = psggExcelLib.SetSheet(name);
            var err = GetError();
            if(err != null)
            {
                NoticeToUser_warning("Exception SetSheet : " + err);
                return false;
            }
            return b;
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
            var i = psggExcelLib.MaxRow();
            var err = GetError();
            if(err != null)
            {
                NoticeToUser_warning("Exception MaxRow : " + err);
                return -1;
            }
            return i;
        } catch (SystemException e)
        {
            NoticeToUser_warning("Exception MaxRow : " + e.Message);
            return -1;
        }
    }

    public int MaxCol()
    {
        try {
            var i = psggExcelLib.MaxCol();
            var err = GetError();
            if(err != null)
            {
                NoticeToUser_warning("Exception MaxCol : " + err);
                return -1;
            }
            return i;
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
                NoticeToUser_warning("Exception GetStr : " + err);
                return null;
            }
            return i;
        } catch (SystemException e)
        {
            NoticeToUser_warning("Exception GetStr : " + e.Message);
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
                NoticeToUser_warning("Exception SetStr : " + err);
            }
        } catch (SystemException e)
        {
            NoticeToUser_warning("Exception SetStr : " + e.Message);
        }
    }
    public void Dispose()
    {
        try {
            psggExcelLib.Dispose();
            var err = GetError();
            if(err != null)
            {
                NoticeToUser_warning("Exception Dispose : " + err);
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
            psggExcelLib.ReadPics();
            var err = GetError();
            if(err != null)
            {
                NoticeToUser_warning("Exception ReadPics : " + err);
            }
        } catch (SystemException e)
        {
            NoticeToUser_warning("Exception ReadPics : " + e.Message);
        }
    }
    public Bitmap GetBmp(int row, int col)
    {
        try {
            var bmp = psggExcelLib.GetBmp(row,col);
            var err = GetError();
            if(err != null)
            {
                NoticeToUser_warning("Exception GetBmp : " + err);
                return null;
            }
            return bmp;
        } catch (SystemException e)
        {
            NoticeToUser_warning("Exception GetBmp : " + e.Message);
            return null;
        }
    }
    public bool SetBmp(int  row, int col, Bitmap bmp)
    {
        try {
            var b = psggExcelLib.SetBmp(row,col);
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
    #endregion

}

