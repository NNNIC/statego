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
using System.Reflection;

namespace stateview
{
    public class Addon
    {
        public static dynamic addondll { get {
                if (!_addondllChecked)
                {
                    _addondllChecked = true;
                    try
                    {
                        var dir = SettingIniUtil.GetAddonDir();
                        if (string.IsNullOrEmpty(dir))
                        {
                            dir = @"%USERPROFILE%\AppDate\Local\psgg";
                        }
                        dir =PathUtil.ExtractPathWithEnvVals(dir);

                        var file = SettingIniUtil.GetAddonFile();
                        var fullpath = Path.Combine(dir,file);
                        if (!File.Exists(fullpath))
                        {
                            G.NoticeToUser_warning("addon file not exists." + fullpath);
                            return null;
                        }
                        var dll = Assembly.LoadFrom(fullpath);
                        var typename = Path.GetFileNameWithoutExtension(file) + ".Editor";
                        _addondll = Activator.CreateInstance(dll.GetType( typename /*"psggConverterLib.Convert"*/));

                    } catch (SystemException e)
                    {
                        G.NoticeToUser_warning("addon connect error :" + e.Message);
                    }
                }
                return _addondll;
            } } 
        private static bool    _addondllChecked = false;
        private static dynamic _addondll = null;

        public static bool HasAddOn()
        {
            return addondll!=null;
        }

        public static bool OpenEditor()
        {
            if (addondll==null) return false;
            return (bool)addondll.open_editor((Control)G.view_form);
        }
        public static bool IsDoneEditor()
        {
            if (addondll==null) return false;
            return (bool)addondll.isdone_editor();
        }
        public static DialogResult GetResultEditor()
        {
            if (addondll==null) return DialogResult.Cancel;
            return (DialogResult)addondll.get_editor_result();
        }
        public static void DisposeEditor()
        {
            if (addondll==null) return;
            addondll.dispose_editor();
        }

        public static bool OpenItemEditor()
        {
            if (addondll==null) return false;
            return (bool)addondll.open_itemeditor((Control)G.view_form);
        }
        public static bool IsDoneItemEditor()
        {
            if (addondll==null) return false;
            return (bool)addondll.isdone_itemeditor();
        }
        public static DialogResult GetResultItemEditor()
        {
            if (addondll==null) return DialogResult.Cancel;
            return (DialogResult)addondll.get_itemeditor_result();
        }
        public static void DisposeItemEditor()
        {
            if (addondll==null) return;
            addondll.dispose_itemeditor();
        }
    }
}
