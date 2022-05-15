using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

public class FileUtil
{
    public static bool IsReadOnly(string path)
    {
        return (new FileInfo(path)).Attributes.HasFlag(FileAttributes.ReadOnly);
    }

    public static int GetLineNumOfSearchingWord(string path, Encoding enc, string word)
    {
        var buf = File.ReadAllText(path,enc);
        return StringUtil.GetLineNumOfSerchingWord(buf, word);
    }
    
    /// <summary>
    /// バッファを行リストにして、開始行と終了行の間を取り出す。行は基数０。　開始行と終了行は含まれる
    /// </summary>
    public static List<string> CropByLineNum(string path, Encoding enc, int startNum, int endNum) 
    {
        var buf = File.ReadAllText(path,enc);
        return StringUtil.CropByLineNum(buf,startNum,endNum);
    }

    public static void DeleteAllFiles(string path)
    {
        Action<DirectoryInfo> trv = null;
        trv = (d)=> {
            var list = new List<string>();
            foreach(var f in d.GetFiles())
            {
                list.Add(f.FullName);
            }
            list.ForEach(i=>File.Delete(i));

            foreach(var d2 in d.GetDirectories())
            {
                trv(d2);
            }
        };  

        trv(new DirectoryInfo(path));
    }

    /// <summary>
    /// 先頭からXバイト読込む
    /// </summary>
    public static byte[] ReadFile(string path, int size)
    {
        using(var fs = File.OpenRead(path))
        {
            var data = new byte[size];
            fs.Read(data,0,size);
            return data;
        }
    }
    /// <summary>
    /// データ追加
    /// </summary>
    public static void AppendData(string path, byte[] data)
    {
        using (var fs = new FileStream(path, FileMode.Append))
        {
            fs.Write(data,0,data.Length);
        }
    }
    /// <summary>
    /// テキスト追加
    /// </summary>
    public static void AppendUTF8Text(string path, string text)
    {
        var data = Encoding.UTF8.GetBytes(text);
        AppendData(path,data);
    }
    public static void AppendUTF8TextFile(string path, string appendfile)
    {
        var bytes = File.ReadAllBytes(appendfile);

        if (bytes.Length>=3 && 
            bytes[0]==0xEF && bytes[1]==0xBB && bytes[2]==0xBF) //BOM有り
        {
            byte[] bytes2 = new byte[bytes.Length - 3];  //BOM除外
            Array.Copy(bytes,3,bytes2,0,bytes2.Length);
            AppendData(path,bytes2);
        }
        else // BOM無し
        {
            AppendData(path,bytes);
        }
    }
    /// <summary>
    /// テキスト追加２
    /// </summary>
    public static FileStream AppendOpen(string path)
    {
        return new FileStream(path,FileMode.Append);
    }
    public static void AppendUTF8Text(FileStream fs, string text)
    {
        var data = Encoding.UTF8.GetBytes(text);
        fs.Write(data,0,data.Length);
    }
    public static void AppendUTF8TextFile(FileStream fs, string appendfile)
    {
        var bytes = File.ReadAllBytes(appendfile);

        if (bytes.Length>=3 && 
            bytes[0]==0xEF && bytes[1]==0xBB && bytes[2]==0xBF) //BOM有り
        {
            //byte[] bytes2 = new byte[bytes.Length - 3];  //BOM除外
            //Array.Copy(bytes,3,bytes2,0,bytes2.Length);
            fs.Write(bytes,3,bytes.Length-3);
        }
        else // BOM無し
        {
            fs.Write(bytes,0,bytes.Length);
        }
    }
    public static void AppendClose(FileStream fs)
    {
        fs.Close();
        fs.Dispose();
        fs = null;
    }
    /// <summary>
    ///  Base64文字列をfile名にエンコード
    ///  ※ファイル名に使えない文字を変換  / -> ^
    /// </summary>
    public static string EncodeFilenameFromBase64(string base64str)
    {
        return base64str.Replace('/','^');
    }
    public static string DecodeFilenameToBase64(string filename)
    {
        return filename.Replace('^','/');
    }
    /// <summary>
    /// フォルダを遡ってさがす。
    /// enddir_regexは、最終フォルダ名を正規表現で判断。NULLはルートまで検索する。
    /// </summary>
    public static string FindFileUpperDirs(string startdir, string file, string enddir_regex = null )
    {
        string findpath = null;
        Action<DirectoryInfo> find = null;
        find = (d)=> {
            if (d == null) return;
            if (findpath != null) return;

            if (enddir_regex!=null)
            {
                if (RegexUtil.IsMatch(enddir_regex,d.Name))
                {
                    findpath = "#enddir";
                    return;
                }
            }
            foreach(var fi in d.GetFiles())
            {
                if (fi.Attributes.HasFlag(FileAttributes.Hidden)) continue;
                if (fi.Name.ToLower() == file.ToLower()) {
                    findpath = fi.FullName;
                    break;
                }
            }
            find(d.Parent);
        };

        find(new DirectoryInfo(startdir));
        return findpath;
    }

    /// <summary>
    /// フォルダをコピーする。
    /// </summary>
    public static void CopyDir(string src, string dst)
    {
        if (!Directory.Exists(src)) {
            throw new SystemException("{CA566635-99DF-4C6E-A696-0B8435752ACF}");
        }

        Action<DirectoryInfo, string> _copy = null;
        _copy = (d,s) => {
            if (!Directory.Exists(s))
            {
                Directory.CreateDirectory(s);
            }
            foreach(var fi in d.GetFiles())
            {
                File.Copy(fi.FullName, Path.Combine(s,fi.Name));
            }
            foreach(var di in d.GetDirectories())
            {
                _copy(di, Path.Combine(s,di.Name));
            }
        };

        _copy(new DirectoryInfo(src), dst);
    }
}

