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

public class UnityUtil
{
    public static bool HasUnityProject()
    {
        /*
            1. genfileのパスに "Assets"がある。
            2. Assetsフォルダの親フォルダに Assembly-CSharp.csprojがある。
            3. Assembly-CSharp.csprojないに<UnityVersion>がある。
        */
        string sAssets="Assets";
        string sCsproj="Assembly-CSharp.csproj";
        string tagUnityVersion = "<UnityVersion>";

        if (!string.IsNullOrEmpty(G.gen_file) && G.gen_file.Contains(sAssets))
        {
            var index = G.gen_file.IndexOf(sAssets);
            var assets_parent = G.gen_file.Substring(0,index);
            var csproj_path = Path.Combine(assets_parent, sCsproj);
            if (File.Exists(csproj_path))
            {
                var text = File.ReadAllText(csproj_path,Encoding.UTF8);
                if (!string.IsNullOrEmpty(text) && text.Contains(tagUnityVersion))
                {
                    return true;
                }
            } 
        }
        return false;
    }

    public static bool IsInUnityProject()
    {
        /*
            1. genfileのパスに "Assets"がある。
            2. Assetsフォルダの親フォルダにProjectSettings\がある。
            3. ProjectSettings内にProjectSettings.assetがある。
            4. ProjectSettings.assetに unity3d.com がある。
        */
        string sAssets="Assets";
        string sProjectsttings = @"ProjectSettings\ProjectSettings.asset";
        string sUnity3dcom = "unity3d.com";

        if (!string.IsNullOrEmpty(G.gen_file) && G.gen_file.Contains(sAssets))
        {
            var index = G.gen_file.LastIndexOf(sAssets);
            var assets_parent = G.gen_file.Substring(0,index);
            var projsettings_path = Path.Combine(assets_parent, sProjectsttings);
            if (File.Exists(projsettings_path))
            {
                var text = File.ReadAllText(projsettings_path,Encoding.UTF8);
                if (!string.IsNullOrEmpty(text) && text.Contains(sUnity3dcom))
                {
                    return true;
                }
            } 
        }
        return false;
    }

    public static string GetSlnFile()
    {
        /*
        1. IsInUnityProjectがtrue
        2. Assetsフォルダの前のフォルダ名をプロジェクト名として取得
        3. Assetsフォルダがあるフォルダに、プロジェクト名の.slnファイルがある。
        4. そのフルパスを返す
        */

        if (IsInUnityProject()==false) return null;

        var plist = PathUtil.PathToList(G.gen_file);
        if (plist == null) return null;

        var ast_index = Array.FindLastIndex(plist, i=>i=="Assets");
        if (ast_index <= 0) return null;
        
        var projname = plist[ast_index-1];
        var projpath = string.Empty;
        for(var i = 0; i <= ast_index-1; i++)
        {
            if (!string.IsNullOrEmpty(projpath)) {
                projpath += "\\";
            }
            projpath += plist[i];
        }
        projpath += "\\" + projname + ".sln";

        if (!File.Exists(projpath))
        {
           return null;
        }

        return projpath;
    }
}
