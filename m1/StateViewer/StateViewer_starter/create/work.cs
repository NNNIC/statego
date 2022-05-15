using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace StateViewer_starter
{
    public enum PROGRAMALANG
    {
        UNKNOWN,
        CSHARP,
        CSHARP_UNITY,
        TYPESCRIPT_ANGULAR,
        VBA_EXCEL,
        TYRANOSCRIPT,
        CPP
    }

    public class LangItem
    {
        public PROGRAMALANG lang;
        public RadioButton  radiobutton;
        public string       folder;

        public LangItem(PROGRAMALANG l, RadioButton rbut, string f)
        {
            lang = l;
            radiobutton = rbut;
            folder = f;
        }
    }

    public class SettingItem
    {
        public PROGRAMALANG lang;
        public string       path;
        public string       title;
        public string       detail;

        public string       convertbat;
        public string       viewbat;
        public string       xls;
        public string       templatefunc;
        public string       templatesource;
        public string       managersrc;
        public string       managerdir;
        public string       subsrc;

        public SettingItem(PROGRAMALANG l, string p, Hashtable ht)
        {
            const string setting = "setting";
            const string syslang = "jpn";

            lang  = l;
            path  = p;

            title          = IniUtil.GetValueFromHashtable(syslang,"title",ht);
            detail         = IniUtil.GetValueFromHashtable(syslang,"detail",ht);

            convertbat     = IniUtil.GetValueFromHashtable(setting,"convertbat",ht);
            viewbat        = IniUtil.GetValueFromHashtable(setting,"viewbat",ht);
            xls            = IniUtil.GetValueFromHashtable(setting,"xls",ht);
            templatefunc   = IniUtil.GetValueFromHashtable(setting,"templatefunc",ht);
            templatesource = IniUtil.GetValueFromHashtable(setting,"templatesource",ht);

            managersrc     = IniUtil.GetValueFromHashtable(setting,"managersrc",ht);
            managerdir     = IniUtil.GetValueFromHashtable(setting,"managerdir",ht);

            subsrc         = IniUtil.GetValueFromHashtable(setting,"subsrc",ht);
        }

    }

    public static class WORK
    {
        public static readonly string SETTINGINI="setting.ini";

        public static string INSTALLPATH {
            get {
                return Path.Combine( Environment.GetEnvironmentVariable("USERPROFILE") , @"AppData\Roaming\psgg\v00");
            }
        }
        public static string LANGSETPATH {
            get {
                return Path.Combine( INSTALLPATH, @"kit\0000_starter");
            }
        }

        public static Dictionary<PROGRAMALANG, LangItem> LANGDATA {
            get {

                if (_langdata == null)
                {
                    var form = StartForm.m_form;
                    if (form == null) return null;

                    var list = new Dictionary<PROGRAMALANG, LangItem>();

                    list.Add(PROGRAMALANG.CSHARP,             new LangItem( PROGRAMALANG.CSHARP,             form.radioButton_csharp           ,"csharp"));
                    list.Add(PROGRAMALANG.CSHARP_UNITY,       new LangItem( PROGRAMALANG.CSHARP_UNITY,       form.radioButton_csharpunity      ,"csharp-unity")); 
                    list.Add(PROGRAMALANG.TYPESCRIPT_ANGULAR, new LangItem( PROGRAMALANG.TYPESCRIPT_ANGULAR, form.radioButton_typescriptangular,"typescript-angular"));
                    list.Add(PROGRAMALANG.VBA_EXCEL,          new LangItem( PROGRAMALANG.VBA_EXCEL,          form.radioButton_vbaexcel         ,"vba-excel"));
                    list.Add(PROGRAMALANG.TYRANOSCRIPT,       new LangItem( PROGRAMALANG.TYRANOSCRIPT,       form.radioButton_tyranoscript     ,"tyranoscript"));
                    list.Add(PROGRAMALANG.CPP,                new LangItem( PROGRAMALANG.CPP,                form.radioButton_cpp              ,"cpp"));

                    _langdata = list;
                }
                return _langdata;
            }
        }
        private static Dictionary<PROGRAMALANG, LangItem> _langdata = null;

        public static void SetupRadioButtons()
        {
            foreach(var k in LANGDATA.Keys)
            {
                var i = LANGDATA[k];
                i.radiobutton.CheckedChanged += Radiobutton_CheckedChanged;
            }
        }

        private static void Radiobutton_CheckedChanged(object sender, EventArgs e)
        {
            SETPROGRAMLANG();
        }

        public static  void SETPROGRAMLANG()
        {
            if (LANGDATA == null) return;
            foreach(var k in LANGDATA.Keys)
            {
                var i = LANGDATA[k];
                if (i.radiobutton.Checked)
                {
                    SELECTED = i.lang;
                    break;
                }
            }
        }

        #region PROGRAM LANG によって値変更
        public  static PROGRAMALANG SELECTED {
            get { return _selected; }
            set {
                _selected = value;
                setup_for_lang();
            }
        }
        private static PROGRAMALANG _selected = PROGRAMALANG.UNKNOWN;

        public static string SELECT_LANGDIR;
        public static List<SettingItem> SETTINGS;

        private static void setup_for_lang()
        {
            SELECT_LANGDIR = string.Empty;
            SETTINGS = new List<SettingItem>();

            var form = StartForm.m_form;
            form.listBox_title.Items.Clear();
            form.textBoxDetail.Text = string.Empty;

            if (_selected == PROGRAMALANG.UNKNOWN)
            {
                return;
            }
            SELECT_LANGDIR =  Path.Combine(LANGSETPATH, LANGDATA[SELECTED].folder); 

            if (!Directory.Exists(SELECT_LANGDIR))
                return;
            var dis = (new DirectoryInfo(SELECT_LANGDIR).GetDirectories());
            if (dis == null || dis.Length == 0)
                return;

            foreach (var di in dis)
            {
                var path = Path.Combine(di.FullName, SETTINGINI);
                if (string.IsNullOrEmpty(path)) continue;
                
                var ht = IniUtil.CreateHashtable(File.ReadAllText(path,Encoding.UTF8));
                if (ht == null) continue;

                try {
                    var item = new SettingItem(SELECTED,di.FullName,ht);
                    SETTINGS.Add(item);
                } catch { }
            }

            var titles = GetTitleList();
            if (titles!=null)
            {
                foreach(var s in titles)
                {
                    form.listBox_title.Items.Add(s);
                }
                if (form.listBox_title.Items.Count>0)
                {
                    form.listBox_title.SetSelected(0,true);
                }
            }
        }

        public static string[] GetTitleList()
        {
            if (SETTINGS == null) return null;
            var list = new List<string>();
            foreach(var i in SETTINGS)
            {
                list.Add(i.title);
            }
            return list.ToArray();
        }
        //private static string GetDetail(int index)
        //{
        //    if (SETTINGS==null || SETTINGS.Count <= index)
        //    {
        //        return string.Empty;
        //    }
        //    return SETTINGS[index].detail;
        //}
        #endregion

        #region INPUT TEXTにより内容を変更
        public static string DETAIL  = null;
        public static string PREFIX  = null;
        public static string XLSDIR  = null;
        public static string GENDIR  = null;
        public static string GENRDIR = null; //GENDIRのXLSDIRからの相対位置 

        public static SettingItem SELECT_SETTING; //選択されたセット
       
        public static void SetDetail(int index)
        {
            if (SETTINGS==null || SETTINGS.Count <= index)
            {
                return;
            }

            SELECT_SETTING = SETTINGS[index];

            DETAIL = SELECT_SETTING.detail;

            UpdateByInputText();
        }
        public static void UpdateByInputText()
        {
            var form = StartForm.m_form;
            if (form == null) return;

            PREFIX = form.textBoxPrefix.Text.Trim();
            XLSDIR = form.textBoxExcelFolder.Text.Trim();
            GENDIR = form.textBoxGenerateFolder.Text.Trim();
            GENRDIR = PathUtil.GetRelativePath(XLSDIR+@"\hoge",GENDIR);

            var str = DETAIL;
            str = convert_var_in_text(str);
            form.textBoxDetail.Text = str;
        }
        #endregion

        #region CREATE
        public class CopyItem {
            public bool   bCopyFileOrCopyText;
            public string srcpath;
            public string text;      //セーブ内容
            public string dstpath;   //コピー先　パスとファイル名
            public Encoding dstenc;

            private CopyItem() { dstenc = Encoding.UTF8; }

            public bool CheckDstpath()
            {
                return File.Exists(dstpath);
            }

            public void Copy()
            {
                var dstdir = Path.GetDirectoryName(dstpath);
                if (!Directory.Exists(dstdir)) Directory.CreateDirectory(dstdir);
                if (File.Exists(dstpath)) File.Delete(dstpath);

                if (bCopyFileOrCopyText)
                {
                    File.Copy(srcpath,dstpath);
                }
                else
                {
                    File.WriteAllText(dstpath,text,dstenc);
                }
            }

            public static CopyItem CreateCopyFile(string i_srcpath, string i_dstpath)
            {
                var item = new CopyItem();
                item.bCopyFileOrCopyText = true;
                item.srcpath = i_srcpath;
                item.dstpath = i_dstpath;
                return item;
            }
            public static CopyItem CreateCopyText(string i_text, string i_dstpath, Encoding enc = null)
            {
                var item = new CopyItem();
                item.bCopyFileOrCopyText = false;
                item.text = i_text;
                item.dstpath = i_dstpath;
                if (enc!=null)
                {
                    item.dstenc = enc;
                }
                return item;
            }
        }
        public static void CreateFiles()
        {
            var form = StartForm.m_form;
            var xlspath = string.Empty;
            var stg = SELECT_SETTING;
            if (stg == null) return;
            var list = new List<CopyItem>();
            
            //convert.bat
            {
                var sjis = Encoding.GetEncoding("sjis");
                var srcpath = Path.Combine(stg.path,stg.convertbat);
                var text    = File.ReadAllText(srcpath,sjis);
                text = convert_var_in_text(text);
                var dstpath = Path.Combine(XLSDIR,PREFIX,stg.convertbat);
                list.Add(CopyItem.CreateCopyText(text,dstpath,sjis));
            }
            //view.bat
            {
                var srcpath = Path.Combine(stg.path,stg.viewbat);
                var dstpath = Path.Combine(XLSDIR,PREFIX,stg.viewbat);
                list.Add(CopyItem.CreateCopyFile(srcpath,dstpath));
            }
            //xls
            {
                var srcpath = Path.Combine(stg.path,stg.xls);
                var dstpath = Path.Combine(XLSDIR,PREFIX, stg.xls);
                list.Add(CopyItem.CreateCopyFile(srcpath,dstpath));

                xlspath = dstpath;
            }
            //templatefunc
            {
                var srcpath = Path.Combine(stg.path,stg.templatefunc);
                var dstpath = Path.Combine(XLSDIR,PREFIX, stg.templatefunc);
                list.Add(CopyItem.CreateCopyFile(srcpath,dstpath));
            }
            //templatesource
            {
                var srcpath = Path.Combine(stg.path,stg.templatesource);
                var text    = File.ReadAllText(srcpath,Encoding.UTF8);
                text        = convert_var_in_text(text);
                var dstpath = Path.Combine(XLSDIR,PREFIX, stg.templatesource);
                list.Add(CopyItem.CreateCopyText(text,dstpath));
            }
            //managersrc
            {
                if (!form.checkBoxSkipCopyStateManager.Checked)
                {
                    var srcpath = Path.Combine(stg.path,stg.managersrc);
                    var dstpath = Path.Combine(GENDIR,stg.managerdir,stg.managersrc);
                    list.Add(CopyItem.CreateCopyFile(srcpath,dstpath));
                }
            }
            //subsrc
            {
                
                var subsrc = stg.subsrc;
                var text   = File.ReadAllText(Path.Combine(stg.path,subsrc), Encoding.UTF8);
                text       = convert_var_in_text(text);

                var dstfile = convert_var_in_text(subsrc);
                var dstpath = Path.Combine(GENDIR,dstfile);

                list.Add(CopyItem.CreateCopyText(text,dstpath));
            }
            //setting.ini
            {
                var srcpath = Path.Combine(stg.path, SETTINGINI);
                var text    = File.ReadAllText(srcpath,Encoding.UTF8);
                text        = convert_var_in_text(text);

                var dstfile = Path.Combine(XLSDIR,PREFIX,SETTINGINI);

                list.Add(CopyItem.CreateCopyText(text,dstfile));
            }
            int count = 0;
            var skiplist = new List<string>();
            foreach(var i in list)
            {
                if (i.CheckDstpath())
                {
                    skiplist.Add(Path.GetFileName(i.dstpath));
                    continue;
                }

                count ++;
            }

            var msg = string.Empty;
            if (skiplist.Count != 0)
            {
                msg = "Will beCopied " + count + " Files. \n";
                msg += "Will be Skipped .. \n";
                foreach(var s in skiplist)
                {
                    msg += "   " + s + "\n";
                }
            }
            else
            {
                msg = "It's ready.";
            }

            msg += "\nOK?";

            if (MessageBox.Show(msg, "CONFIRM",MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                foreach(var i in list)
                {
                    if (i.CheckDstpath())
                    {
                        continue;
                    }

                    i.Copy();
                }
                
                //var form = StartForm.m_form;
                form.m_target_xlsx = xlspath;
                form.DialogResult = DialogResult.OK;
                form.Close();
            }
        }
        private static string convert_var_in_text(string text)
        {
            if (string.IsNullOrEmpty(text)) return null;
            var str = text;
            if (!string.IsNullOrEmpty(str))
            {
                if (!string.IsNullOrEmpty(PREFIX))  str = str.Replace("__PREFIX__", PREFIX);
                if (!string.IsNullOrEmpty(XLSDIR))  str = str.Replace("__XLSDIR__", XLSDIR);
                if (!string.IsNullOrEmpty(GENDIR))  str = str.Replace("__GENDIR__", GENDIR);
                if (!string.IsNullOrEmpty(GENRDIR)) str = str.Replace("__GENRDIR__",GENRDIR);
            }
            return str;
        }
        #endregion
    }
}