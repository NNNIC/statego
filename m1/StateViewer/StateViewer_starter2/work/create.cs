using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;


namespace StateViewer_starter2
{
    public partial class WORK
    {
        #region CREATE

        public class CopyItem {

            public enum Mode {
                CopyFileToFile, //指定ファイルを dstpathへコピー
                SaveTextToFile, //指定テキストを dstpathへセーブ
                SaveTextToSheet //指定テキストを sheetnameでエクセルへセーブ　※エクセル処理はＯＳ上で遅延削除されるのが原因でプロセスが正常終了しない問題がある。よって、後でまとめて行う
            }

            public Mode   copyMode;
            public string srcpath;
            public string text;      //セーブ内容
            public string dstpath;   //コピー先　パスとファイル名
            public string sheetname; //シート名
            public Encoding dstenc;

            public bool     valid;
            public bool     existFile; //既に存在している

            private CopyItem() { dstenc = Encoding.UTF8; }

            public bool CheckDstpath()
            {
                if (!string.IsNullOrEmpty(sheetname)) throw new SystemException("Unexpected {19C23DE9-EBE1-49CF-9048-D53B91C30D7F}");
                return File.Exists(dstpath);
            }

            // 引数の関数は excelのシート用。 エクセル処理の問題に対応するため。
            public void Copy(Action<string,string> excel_func=null)
            {
                if (copyMode == Mode.SaveTextToSheet)
                {
                    if (excel_func!=null)
                    {
                        excel_func(sheetname, text);
                    }
                    return;
                }
                else
                {
                    var dstdir = Path.GetDirectoryName(dstpath);
                    if (!Directory.Exists(dstdir)) Directory.CreateDirectory(dstdir);
                    if (File.Exists(dstpath)) File.Delete(dstpath);

                    if (copyMode == Mode.CopyFileToFile)
                    {
                        File.Copy(srcpath,dstpath);
                    }
                    else if (copyMode == Mode.SaveTextToFile)
                    {
                        File.WriteAllText(dstpath,text,dstenc);
                    }
                }
            }
            public static CopyItem CreateCopyFile(string i_srcpath, string i_dstpath)
            {
                var item = new CopyItem();
                item.copyMode =  Mode.CopyFileToFile;
                item.srcpath  = i_srcpath;
                item.dstpath  = i_dstpath;
                item.existFile = File.Exists(i_dstpath);
                return item;
            }
            public static CopyItem CreateCopyText(string i_text, string i_dstpath, Encoding enc = null)
            {
                var item = new CopyItem();
                item.copyMode = Mode.SaveTextToFile;
                item.text = i_text;
                item.dstpath = i_dstpath;
                if (enc!=null)
                {
                    item.dstenc = enc;
                }
                item.existFile = File.Exists(i_dstpath);
                return item;
            }
            public static CopyItem CreateSheetText(string i_text, string i_sheetname)
            {
                var item       = new CopyItem();
                item.copyMode  = Mode.SaveTextToSheet;
                item.text      = i_text;
                item.sheetname = i_sheetname;
                return item;
            }
        }

        /*Obsolete*/
        public static void CreateFiles()
        {
            var enc_sjis = Encoding.GetEncoding("sjis");
			var enc_utf8 = Encoding.UTF8;
            //var xlspath = string.Empty;
            var stg     = SELECT_SETTING;
            if (stg == null) return;
            var list = new List<CopyItem>();

            var srcpath = string.Empty;

            // 要素０はエクセル ※ 最初にExcelファイル 
            {
                try {
                    srcpath = Path.Combine(stg.path, stg.xls);
                    var dstname = convert_var_in_text(stg.xls);
                    var dstpath = Path.Combine(XLSDIR,dstname);
                    list.Add(CopyItem.CreateCopyFile(srcpath,dstpath));
                } catch (SystemException e) {
                    MessageBox.Show("Copying Excel File Error\n" + srcpath + "\n" + e.Message);
                    Application.Exit();
                    //throw new SystemException("Unexpected! @ create ExcelFile (" + e.Message +") {3D16BEE8-1FA1-4B56-B1F4-E203EDA4CF48}"); 
                }
            }
            // 要素１はPSGG
            {
                try {
                    srcpath = Path.Combine(stg.path, stg.psgg);
                    var text    = File.ReadAllText(srcpath, enc_utf8);
                    var newtext = convert_var_in_text(text);
                    var dstname = convert_var_in_text(stg.psgg);
                    var dstpath = Path.Combine(XLSDIR, dstname);
                    list.Add(CopyItem.CreateCopyText(newtext,dstpath,enc_utf8));
                } catch (SystemException e) {
                    MessageBox.Show("Copying PSGG File Error\n" + srcpath + "\n" + e.Message);
                    Application.Exit();
                    //throw new SystemException("Unexpected! @ create viewbat (" + e.Message +") {91588A11-8AF4-45FD-AD6C-E92CB80030E6}"); 
                }
            }
            //mangersrc
            if (!m_form.checkBoxSkipCopyStateManager.Checked)
            {
                try {
                    if (!string.IsNullOrEmpty(stg.manager_src)) {
                        srcpath = Path.Combine(stg.path, stg.manager_src);
                        var dstpath = Path.Combine(GENDIR,stg.manager_dir,stg.manager_src);
                        list.Add(CopyItem.CreateCopyFile(srcpath,dstpath));
                    }
                } catch (SystemException e) {
                    MessageBox.Show("Copying Manager File Error\n" + srcpath + "\n" + e.Message);
                    Application.Exit();
                    //throw new SystemException("Unexpected! @ create managersrc (" + e.Message +") {4F498D85-4DD8-4426-B3CE-8C4D10095B7B}"); 
                }
            }
            //subsrc
            {
                try {
                    if (!string.IsNullOrEmpty(stg.sub_src))
                    {
                        srcpath = Path.Combine(stg.path, stg.sub_src);
                        var text    = File.ReadAllText(srcpath, enc_utf8);
                        var newtext = convert_var_in_text(text);
                        var dstname = convert_var_in_text(stg.sub_src);
                        var dstpath = Path.Combine(GENDIR,dstname);
                        list.Add(CopyItem.CreateCopyText(newtext,dstpath));
                    }
                } catch (SystemException e) {
                    MessageBox.Show("Copying Sub Source File Error\n" + srcpath + "\n" + e.Message);
                    Application.Exit();
                    //throw new SystemException("Unexpected! @ create subsrc (" + e.Message +") {371CA513-635B-4720-8FA3-2E4FB7240B1D}"); 
                }
            }
			//gensrc
			{
				try
				{
                    srcpath = Path.Combine(stg.path, stg.gen_src);
                    var text    = File.ReadAllText(srcpath, enc_utf8);
                    var newtext = convert_var_in_text(text);
                    var dstname = convert_var_in_text(stg.gen_src);
                    var dstpath = Path.Combine(GENDIR,dstname);
                    list.Add(CopyItem.CreateCopyText(newtext,dstpath));			
				} catch (SystemException e) {
                    MessageBox.Show("Copying Generated File Error\n" + srcpath + "\n" + e.Message);
                    Application.Exit();
                    //throw new SystemException("Unexpected! @ create gensrc (" + e.Message +") {D8B8E418-A768-4393-B454-6C7A3D614AB9}"); 
                }
			}
            //macro_ini
            {
                try
                {
                    if (!string.IsNullOrEmpty(stg.macro_ini))
                    {
                        srcpath = Path.Combine(stg.path, stg.macro_ini);
                        var text    = File.ReadAllText(srcpath, enc_utf8);
                        var newtext = convert_var_in_text(text);
                        var dstname = convert_var_in_text(stg.macro_ini);
                        var dstpath = Path.Combine(XLSDIR,dstname);
                        list.Add(CopyItem.CreateCopyText(newtext,dstpath));
                    }
                } catch (SystemException e) {
                    MessageBox.Show("Copying Macro Ini File Error\n" + srcpath + "\n" + e.Message);
                    Application.Exit();
                    //throw new SystemException("Unexpected! @ create macroini (" + e.Message +") {77EFA22B-5105-48D9-9E4C-B01FB36E41EE}"); 
                }

            }
            //template src
            {
                try {
                    srcpath = Path.Combine(stg.path, stg.template_src);
                    var text    = File.ReadAllText(srcpath,enc_utf8);
                    var newtext = convert_var_in_text(text);
                    list.Add(CopyItem.CreateSheetText(newtext, WordStorage.Store.sheettempsrc/* "template-source"*/));
                } catch (SystemException e) {
                    MessageBox.Show("Adding Template Source File Error\n" + srcpath + "\n" + e.Message);
                    Application.Exit();
                    //throw new SystemException("Unexpected! @ create templatesrc (" + e.Message +") {A2145F3D-7427-4040-B649-A5F95C1F6752}"); 
                }
               
            }
            //template func
            {
                try {
                    srcpath = Path.Combine(stg.path, stg.template_func);
                    var text    = File.ReadAllText(srcpath,enc_utf8);
                    var newtext = convert_var_in_text(text);
                    list.Add(CopyItem.CreateSheetText(newtext, WordStorage.Store.sheettempfunc/* "template-statefunc"*/));
                } catch (SystemException e) {
                    MessageBox.Show("Adding Template Func File Error\n" + srcpath + "\n" + e.Message);
                    Application.Exit();
                    //throw new SystemException("Unexpected! @ create templatefunc (" + e.Message +") {E5599FC9-71C6-4110-B3D2-F9B1532F4F23}"); 
                }
            }
            //help
            {
                try {
                    if (!string.IsNullOrEmpty(stg.help))
                    { 
                        srcpath = Path.Combine(stg.path, stg.help);
                        var text    = File.ReadAllText(srcpath, enc_utf8);
                        list.Add(CopyItem.CreateSheetText(text, WordStorage.Store.sheethelp/*help*/));
                    }
                } catch (SystemException e) {
                    MessageBox.Show("Adding Help File Error\n" + srcpath + "\n" + e.Message);
                    Application.Exit();
                    //throw new SystemException("Unexpected! @ create templatefunc (" + e.Message +") {B1B4258F-83DA-4C4E-8FA4-84ABABCE3990}"); 
                }
            }
            //helpweb
            {
                try
                {
                    if (!string.IsNullOrEmpty(stg.helpweb))
                    {
                        srcpath = Path.Combine(stg.path, stg.helpweb);
                        var dstpath = Path.Combine(XLSDIR, stg.helpweb);
                        list.Add(CopyItem.CreateCopyFile(srcpath,dstpath));
                    }
                }
                catch (SystemException e)
                {
                    MessageBox.Show("Copying Helpweb File Error\n" + srcpath + "\n" + e.Message);
                    Application.Exit();
                }
            }
            //setting.ini
            {
                try {
                    srcpath = Path.Combine(stg.path, SETTINGINI);
                    var text    = File.ReadAllText(srcpath, enc_utf8);
                    var newtext = convert_var_in_text(text);
                    list.Add(CopyItem.CreateSheetText(newtext,SETTINGINI));
                } catch (SystemException e) {
                    MessageBox.Show("Adding Help Setting Ini Error\n" + srcpath + "\n" + e.Message);
                    Application.Exit();
                    //throw new SystemException("Unexpected! @ create setting.ini (" + e.Message +") {20C03DCF-EFA3-4FC9-880F-B25DEB9A0CA4}"); 
                }
            }

            var xls_name    = Path.GetFileName(list[0].dstpath);
            var xls_dstpath = list[0].dstpath;

            var copylist  = new List<string>();
            var skiplist  = new List<string>();
            var sheetlist = new List<string>();
            foreach(var i in list)
            {
                i.valid = true;
                if (string.IsNullOrEmpty(i.sheetname))
                {
                    if (i.CheckDstpath())
                    {
                        skiplist.Add(Path.GetFileName(i.dstpath));
                        i.valid = false;
                    }
                    else
                    {
                        copylist.Add(Path.GetFileName(i.dstpath));
                    }
                }
                else
                {
                    sheetlist.Add(i.sheetname);
                }
            }

            var copedfiles = string.Empty;
            copylist.ForEach(i=> {
                if (!string.IsNullOrEmpty(copedfiles)) copedfiles +=",";
                copedfiles += i;
            });

            var skipedfiles = string.Empty;
            skiplist.ForEach(i=> {
                if (!string.IsNullOrEmpty(skipedfiles)) skipedfiles += ",";
                skipedfiles += i;
            });

            var updatesheets = string.Empty;
            sheetlist.ForEach(i=> {
                if (!string.IsNullOrEmpty(updatesheets)) updatesheets += ",";
                updatesheets += i;
            });

            var msg = string.Format(
                "Copy {0} Files\n" +
                " > {1} \n" + 
                "Skip {2} Files\n" +
                " > {3} \n" +
                "Update {4} sheets in {5}\n" +
                " > {6} \n" +
                "\nProceed ?"
                ,
                copylist.Count, //0
                copedfiles,     //1
                skiplist.Count, //2
                skipedfiles,    //3
                sheetlist.Count,//4
                xls_name,       //5
                updatesheets    //6
                );
            
            if (MessageBox.Show(msg, "Confirmation", MessageBoxButtons.OKCancel)==DialogResult.OK)
            {
                var sheetdic = new Dictionary<string,string>();

                foreach(var i in list)
                {
                    if (i.valid)
                    {
                        i.Copy( 
                            (sheetname,value)=>sheetdic.Add(sheetname,value)                           
                            );
                    }
                }

                if (sheetdic.Count>0)
                {
                    ExcelUtil.AddSheetWithValue(xls_dstpath,sheetdic);
                }
	            m_form.DialogResult = DialogResult.OK;
	            m_form.m_target_xlsx = xls_dstpath;
		        m_form.Close();
            }
        }
        #endregion

        public class CreateFileWork
        {
            //readonly Encoding enc_sjis = Encoding.GetEncoding("sjis");
            readonly Encoding enc_utf8 = Encoding.UTF8;

            SettingItem stg             { get { return WORK.SELECT_SETTING; } }
            List<CopyItem> m_list          = new List<CopyItem>();
            List<CopyItem>   m_doclist       = new List<CopyItem>(); //ドキュメントフォルダに生成されるファイル
            List<CopyItem>   m_genlist       = new List<CopyItem>(); //ソースフォルダに生成されるファイル
            List<CopyItem>   m_exelsheetlist = new List<CopyItem>(); //エクセルシートの追加リストになるファイル

            string         m_info;

            public CreateFileWork()
            {
                if (stg == null) { m_info = "nothing"; return; }

                var srcpath = string.Empty;

                // 要素０はエクセル ※ 最初にExcelファイル 
                {
                    try {
                        srcpath = Path.Combine(stg.path, stg.xls);
                        var dstname = convert_var_in_text(stg.xls);
                        var dstpath = Path.Combine(XLSDIR,dstname);
                        var item = CopyItem.CreateCopyFile(srcpath,dstpath);
                        m_list.Add(item);
                        m_doclist.Add(item);

                    } catch (SystemException e) {
                        MessageBox.Show("Copying Excel File Error\n" + srcpath + "\n" + e.Message);
                        Application.Exit();
                    }
                }
                //psgg
                {
                    try {
                        srcpath = Path.Combine(stg.path, stg.psgg);
                        var text    = File.ReadAllText(srcpath, enc_utf8);
                        var newtext = convert_var_in_text(text);
                        var dstname = convert_var_in_text(stg.psgg);
                        var dstpath = Path.Combine(XLSDIR, dstname);
                        var item = CopyItem.CreateCopyText(newtext,dstpath,enc_utf8);
                        m_list.Add(item);
                        m_doclist.Add(item);

                    } catch (SystemException e) {
                        MessageBox.Show("Copying PSGG File Error\n" + srcpath + "\n" + e.Message);
                        Application.Exit();
                    }
                }
                //mangersrc
                //if (!m_form.checkBoxSkipCopyStateManager.Checked || m_cf.radioButtonYes.Checked)
                if (m_cf.radioButtonYes.Checked)
                {
                    try {
                        if (!string.IsNullOrEmpty(stg.manager_src)) {
                            srcpath = Path.Combine(stg.path, stg.manager_src);
                            var text    = File.ReadAllText(srcpath, enc_utf8);
                            var newtext = convert_var_in_text(text);
                            var dstpath = Path.Combine(GENDIR,stg.manager_dir,stg.manager_src);
                            var item = CopyItem.CreateCopyText(newtext,dstpath,SELECT_SETTING.SRC_ENCODING);
                            m_list.Add(item);
                            m_genlist.Add( item );

                        }
                    } catch (SystemException e) {
                        MessageBox.Show("Copying Manager File Error\n" + srcpath + "\n" + e.Message);
                        Application.Exit();
                    }
                }
                //subsrc
                {
                    try {
                        if (!string.IsNullOrEmpty(stg.sub_src))
                        {
                            srcpath = Path.Combine(stg.path, stg.sub_src);
                            var text    = File.ReadAllText(srcpath, enc_utf8);
                            var newtext = convert_var_in_text(text);
                            var dstname = convert_var_in_text(stg.sub_src);
                            var dstpath = Path.Combine(GENDIR,dstname);
                            var item = CopyItem.CreateCopyText(newtext,dstpath,SELECT_SETTING.SRC_ENCODING);
                            m_list.Add(item);
                            m_genlist.Add(item);

                        }
                    } catch (SystemException e) {
                        MessageBox.Show("Copying Sub Source File Error\n" + srcpath + "\n" + e.Message);
                        Application.Exit();
                    }
                }
			    //gensrc
			    {
				    try
				    {
                        srcpath = Path.Combine(stg.path, stg.gen_src);
                        var text    = File.ReadAllText(srcpath, enc_utf8);
                        var newtext = convert_var_in_text(text);
                        var dstname = convert_var_in_text(stg.gen_src);
                        var dstpath = Path.Combine(GENDIR,dstname);
                        var item = CopyItem.CreateCopyText(newtext,dstpath,SELECT_SETTING.SRC_ENCODING);
                        m_list.Add(item);	
                        m_genlist.Add(item);                        		
				    } catch (SystemException e) {
                        MessageBox.Show("Copying Generated File Error\n" + srcpath + "\n" + e.Message);
                        Application.Exit();
                    }
			    }
                //macro_ini
                {
                    try
                    {
                        if (!string.IsNullOrEmpty(stg.macro_ini))
                        {
                            srcpath = Path.Combine(stg.path, stg.macro_ini);
                            var text    = File.ReadAllText(srcpath, enc_utf8);
                            var newtext = convert_var_in_text(text);
                            var dstname = convert_var_in_text(stg.macro_ini);
                            var dstpath = Path.Combine(XLSDIR,dstname);
                            var item = CopyItem.CreateCopyText(newtext,dstpath);
                            m_list.Add(item);
                            m_doclist.Add(item);
                        }
                    } catch (SystemException e) {
                        MessageBox.Show("Copying Macro Ini File Error\n" + srcpath + "\n" + e.Message);
                        Application.Exit();
                    }

                }
                //template src
                {
                    try {
                        if (!string.IsNullOrEmpty(stg.template_src))
                        {
                            srcpath = Path.Combine(stg.path, stg.template_src);
                            var text    = File.ReadAllText(srcpath,enc_utf8);
                            var newtext = convert_var_in_text(text);
                            var item    = CopyItem.CreateSheetText(newtext, WordStorage.Store.sheettempsrc/* "template-source"*/);
                            m_list.Add(item);
                            m_exelsheetlist.Add(item);
                        }
                    } catch (SystemException e) {
                        MessageBox.Show("Adding Template Source File Error\n" + srcpath + "\n" + e.Message);
                        Application.Exit();
                    }
                }
                //template func
                {
                    try {
                        srcpath = Path.Combine(stg.path, stg.template_func);
                        var text    = File.ReadAllText(srcpath,enc_utf8);
                        var newtext = convert_var_in_text(text);
                        var item    = CopyItem.CreateSheetText(newtext, WordStorage.Store.sheettempfunc/* "template-statefunc"*/);
                        m_list.Add(item);
                        m_exelsheetlist.Add(item);

                    } catch (SystemException e) {
                        MessageBox.Show("Adding Template Func File Error\n" + srcpath + "\n" + e.Message);
                        Application.Exit();
                    }
                }
                //help
                {
                    try {
                        if (!string.IsNullOrEmpty(stg.help))
                        { 
                            srcpath = Path.Combine(stg.path, stg.help);
                            var text    = File.ReadAllText(srcpath, enc_utf8);
                            var item    = CopyItem.CreateSheetText(text, WordStorage.Store.sheethelp/*help*/);
                            m_list.Add(item);
                            m_exelsheetlist.Add(item);
                        }
                    } catch (SystemException e) {
                        MessageBox.Show("Adding Help File Error\n" + srcpath + "\n" + e.Message);
                        Application.Exit();
                    }
                }
                //helpweb  ※ 2019.7.19 helpwebは　コピー元のスタータキットを参照に変更へ
                {
                    //try
                    //{
                    //    if (!string.IsNullOrEmpty(stg.helpweb))
                    //    {
                    //        srcpath = Path.Combine(stg.path, stg.helpweb);
                    //        var dstpath = Path.Combine(XLSDIR, stg.helpweb);
                    //        var item = CopyItem.CreateCopyFile(srcpath,dstpath);
                    //        m_list.Add(item);
                    //        m_doclist.Add(item);
                    //    }
                    //}
                    //catch (SystemException e)
                    //{
                    //    MessageBox.Show("Copying Helpweb File Error\n" + srcpath + "\n" + e.Message);
                    //    Application.Exit();
                    //}
                }
                //setting.ini
                {
                    try {
                        srcpath = Path.Combine(stg.path, SETTINGINI);
                        var text    = File.ReadAllText(srcpath, enc_utf8);
                        var newtext = convert_var_in_text(text);
                        newtext     = modify_helpweb(newtext,stg);
                        var item    = CopyItem.CreateSheetText(newtext,SETTINGINI);
                        m_list.Add(item);
                        m_exelsheetlist.Add(item);

                    } catch (SystemException e) {
                        MessageBox.Show("Adding Help Setting Ini Error\n" + srcpath + "\n" + e.Message);
                        Application.Exit();
                    }
                }

                Func<CopyItem,string> getRep = (i) => {
                    var s = Path.GetFileName(i.dstpath);
                    if (i.existFile) s +=  WordStorage.Res.Get("C11",m_form.m_syslang);//  "... will be skipped because it exists.";
                    return s;
                };

                var nl = Environment.NewLine;
                m_info  = "Document Folder: " + XLSDIR + nl;
                m_doclist.ForEach(i=> m_info += "   " + getRep(i) + nl);
                m_info += "Source code Folder: " + GENDIR + nl;
                m_genlist.ForEach(i=> m_info += "   " + getRep(i) + nl);

                m_cf.textBoxCreateFiles.Text = m_info;
                m_cf.button_g6ok.Text = "Create [ " + stg.title + " ]";

                m_cf.button_g6ok.Enabled = !(m_doclist.TrueForAll(i=>i.existFile) && m_genlist.TrueForAll(i=>i.existFile));
            }


            public void Save()
            {
                var xls_name    = Path.GetFileName(m_list[0].dstpath);
                var xls_dstpath = m_list[0].dstpath;
                var xls_exists  = File.Exists(xls_dstpath);

                var sheetdic = new Dictionary<string,string>();

                var result = string.Empty;

                foreach(var i in m_list)
                {
                    i.valid = true;
                    if (i.copyMode == CopyItem.Mode.SaveTextToSheet)
                    {
                        if (xls_exists) i.valid = false;
                    }
                    else
                    {
                        if (i.existFile) i.valid = false;
                    }

                    if (i.valid)
                    {
                        i.Copy( 
                            (sheetname,value)=>sheetdic.Add(sheetname,value)                           
                            );
                    }
                }

                if (sheetdic.Count>0)
                {
                    ExcelUtil.AddSheetWithValue(xls_dstpath,sheetdic);
                }
                m_cf.DialogResult = DialogResult.OK;
                m_cf.Close();
	            m_form.DialogResult = DialogResult.OK;
	            m_form.m_target_xlsx = xls_dstpath;
                m_form.m_target_psgg = m_list[1].dstpath;
		        m_form.Close();
            }
        }
    }
}
