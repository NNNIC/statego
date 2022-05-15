using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace StateViewer_starter2
{
    public class PSGGFileUtil
    {
        public class Data
        {
            public string version;
            public string file;
        }
        
        public static Data Read(string file)
        {
            var pfr = new psggFileRead();
            if (pfr.Read(file))
            {
                var data = new Data();
                data.version = pfr.version;
                data.file = pfr.xlsfile;
                return data;
            }

            return null;
        }

        /// <summary>
        /// 手がかりからStateGoファイルを探す
        /// </summary>
        /// <param name="file"></param>
        public static bool FindPsggUsingClue(string file, out string psgg, out string xlsx, bool bAskMsgBox)
        {
            var ext = Path.GetExtension(file).ToLower();
            if(ext == ".psgg")
            {
                return _setFile_psgg(file, out psgg,out xlsx);
            }
            else if (ext == ".xlsx" || ext==".elxm") //エクセルファイル or マクロ入りエクセルファイル
            {
                /*m_target_xlsx*/xlsx = file;
                /*m_target_psgg*/psgg = null;
                return true;
            }

            var dirname = Path.GetDirectoryName(file);

            // 1. ファイル内に psgg-file:相対位置がある。
            // 2. ファイル内にある　psggConverterLib.dll converted from ファイル名から、そのファイルを検索する。
            try { 
                var buf = File.ReadAllText(file);
                if (!string.IsNullOrEmpty(buf))
                {
                    var psggfile = "psgg-file:";
                    var find = buf.IndexOf(psggfile);
                    if (find >=0)
                    {
                        var sample = buf.Substring( find + psggfile.Length );
                        var testfile = sample.Split('\x0d','\x0a')[0].Trim();
                        var testfile2 = Path.Combine(dirname, testfile);
                        if (_setFile_psgg(testfile2, out psgg, out xlsx))
                        {
                            return true;
                        }
                    }

                    var psggconverter = "psggConverterLib.dll converted from";
                    var find2 = buf.IndexOf(psggconverter);
                    if (find2>=0)
                    {
                        var sample = buf.Substring( find2 + psggconverter.Length );
                        var testfile = sample.Split('\x0d','\x0a')[0].Trim().TrimEnd('.');
                        var bTimeout=false;
                        var testfullpath = PathUtil.FindTraverseDownAndUp(dirname,testfile,1000, out bTimeout );
                        if (!bTimeout)
                        {
                            var bOk = true;
                            if (bAskMsgBox)
                            {
                                bOk = MessageBox.Show("The following file will be opened.\n" + testfullpath,"Confirmation", MessageBoxButtons.OKCancel) == DialogResult.OK;
                            }
                            if (bOk)
                            {
                                /*m_target_xlsx*/xlsx = testfullpath;
                                /*m_target_psgg*/psgg = null;
                                return true;
                            }
                        }
                    }
                }
                {
                    //extを.psggで検索
                    {
                        var newpath = Path.Combine(dirname, Path.GetFileNameWithoutExtension(file) + ".psgg");
                        if (File.Exists(newpath))
                        {
                            xlsx = newpath + ".xlsx"; //dummy
                            psgg = newpath;
                            return true;
                        }
                    }
               }
               {
                    var cfile = Path.GetFileNameWithoutExtension(file);
                    if (cfile.EndsWith("_created")) cfile = cfile.Substring(0,cfile.Length - "_created".Length);
                    var cfile_w_ext = cfile + ".psgg";
                    
                    var bTimeOut = false;
                    var testfullpath = PathUtil.FindTraverseDownAndUp(dirname, cfile_w_ext,1000, out bTimeOut);
                    if (!bTimeOut)
                    {
                        var bOk = true;
                        if (bAskMsgBox)
                        {
                            bOk = MessageBox.Show("The following file will be opened.\n" + testfullpath,"Confirmation", MessageBoxButtons.OKCancel) == DialogResult.OK;
                        }
                        if (bOk)
                        { 
                            return _setFile_psgg(testfullpath, out psgg, out xlsx);
                        }
                    } 
                }


            } catch (SystemException e)
            {
                Console.WriteLine("{A6C7A169-D9AE-4CBB-8EF5-906603E1E9BF} "  + e.Message);
            }
            psgg = null;
            xlsx = null;
            return false;
        }

        private static bool _setFile_psgg(string file, out string psgg, out string xlsx)
        {
            var data = PSGGFileUtil.Read(file);
            if (data != null && !string.IsNullOrEmpty(data.file))
            {
                /*m_target_xlsx*/ xlsx = (new FileInfo(Path.Combine(Path.GetDirectoryName(file), data.file))).FullName;
                /*m_target_psgg*/ psgg = (new FileInfo(file)).FullName;
                return true;
            }
            else
            {
                /*m_target_xlsx*/ xlsx = null;
                /*m_target_psgg*/ psgg = null;
                return false;
            }
        }

    }
}
