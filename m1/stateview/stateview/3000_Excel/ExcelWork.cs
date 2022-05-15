using System;
//using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;

#if obs
public class ExcelWork 
{
    Excel.Application m_app;
    Excel.Workbooks   m_workbooks;
    Excel.Workbook    m_workbook;
    Excel.Worksheet   m_worksheet;


    public void Dispose() //ref https://blogs.msdn.microsoft.com/office_client_development_support_blog/2012/02/09/office-5/
    {
        if (m_worksheet!=null)
        {
            Marshal.ReleaseComObject(m_worksheet);
            m_worksheet = null;
        }
        if (m_workbook!=null)
        {
            Marshal.ReleaseComObject(m_workbook);
            m_workbook = null;
        }
        if (m_workbooks!=null)
        {
            Marshal.ReleaseComObject(m_workbooks);
            m_workbooks = null;
        }

        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
        m_app.Quit();
        Marshal.ReleaseComObject(m_app);
        m_app = null;
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();
    }

    public void Load(string file)
    {
        if (string.IsNullOrEmpty(file)) throw new SystemException("Unexpected! {DFDE83F4-CB29-4F08-A311-DBA404C844D0}");
        if (m_app!=null) throw new SystemException("Unexpected! {5E4F6BD1-B5B6-418F-BBE7-88E119417E88}");
        m_app = new Excel.Application();
        m_workbooks = m_app.Workbooks;
        m_workbook = m_workbooks.Open(file);
    }

    public void Save()
    {
        try {
         m_workbook.Save();
        } catch { }
    }

    public void SetSheet(string name)
    {
        if (m_worksheet!=null)
        {
            Marshal.ReleaseComObject(m_worksheet);
            m_worksheet = null;
        }

        for(var i = 1; i <= m_workbook.Sheets.Count; i++)
        {
            var sheet = (Excel.Worksheet)m_workbook.Sheets[i];
            if (sheet.Name == name)
            {
                m_worksheet = sheet;
                break;
            }
            Marshal.ReleaseComObject(sheet);
            sheet = null;
        }
    }

    public void NewSheet(string name)
    {
        m_workbook.Sheets.Add(After:m_workbook.Sheets[1]);
        m_worksheet = (Excel.Worksheet)m_workbook.ActiveSheet;
        m_worksheet.Name = name;
    }

    public Excel.Worksheet GetSheet()
    {
        return m_worksheet;
    }
}

#endif