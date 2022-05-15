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
using System.Threading;

using System.IO.MemoryMappedFiles;
using System.Diagnostics;

namespace stateview
{
    public class CheckOpenSameDoc
    {
        static MemoryMappedFile m_newMMF = null;

        public class Item {
            public bool    bDirty;
            public Int32   process_id;       //  4  byte
            public byte[]  filename_hash;    // 16  byte
            public Int64   handle;           //  8  byte    ウインドウハンドル　フォーカス用
            public byte[]  window_tile;      //228  byte

            public Item() {
                bDirty = false;
                process_id = 0;                        
                filename_hash = new byte[16];
                handle      = 0;
                window_tile = new byte[256-4-16-8]; //228
            }
            public bool check_filename_hash(byte[] hash)
            {
                for(var i = 0; i<16; i++)
                {
                    if (filename_hash[i] != hash[i]) return false;
                }
                return true;
            }
            public string get_window_title()
            {
                var s = Encoding.UTF8.GetString(window_tile);
                s = s.Trim('\0');
                return s;
            }
            public void set_window_title(string s)
            {
                window_tile = createTextbyte(s, 256-8-16-8);
            }
            public IntPtr get_handle()
            {
                return (IntPtr)handle;
            }
            public void set_handle(IntPtr p)
            {
                handle = (Int64)p;
            }
        }

        const string            MAPNAME ="syn-g-gen-mappedfile.dat";
        const int               MAXPROC = 256;             
        const int               ITEMSIZE= 256;
        const long              MAXSIZE = ITEMSIZE * MAXPROC;

        static string           MAPNAME_FULL { get { return Path.Combine(Path.GetTempPath(),MAPNAME); } }

        static int              my_process_id { get {  return G.PROCESS_ID; } }
        static List<Item>       m_item_list;

        private static  bool Is_FirstProcess() // true - yes.  false - no.
        {
            return (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length == 1);
        }

        private static  void traverse_items(Action<int,MemoryMappedViewAccessor> func) {
            try {
                using(var ac = m_newMMF.CreateViewAccessor(0,MAXSIZE)) {
                    for(var n = 0;n < MAXPROC;n++)
                    {
                        func(n,ac);
                    }
                }
            } catch (SystemException e){
                Console.WriteLine(e.Message);
            }
        }
        private static void read_items()
        {
            var list =new List<Item>();
            traverse_items( (n,ac)=> {
                var start = n * ITEMSIZE;
                var item = new Item();
                ac.Read(start + 0,out item.process_id);
                ac.ReadArray(start + 4, item.filename_hash,0,item.filename_hash.Length);
                ac.Read(start + 4 + 16, out item.handle);
                ac.ReadArray(start + 4 + 16 + 8, item.window_tile, 0, item.window_tile.Length);
                list.Add(item);
            });
            m_item_list = list;
            return;
        }
        private static void write_items()
        {
            traverse_items( (n,ac)=> {
                var start = n * ITEMSIZE;
                if (n < m_item_list.Count)
                {
                    var item = m_item_list[n];
                    if (item.bDirty)
                    {
                        item.bDirty = false;
                        ac.Write(start + 0, item.process_id);
                        ac.WriteArray(start + 4, item.filename_hash,0,item.filename_hash.Length);
                        ac.Write(start + 4 + 16, item.handle);
                        ac.WriteArray(start + 4 + 16 + 8, item.window_tile,0, item.window_tile.Length);
                    }
                }
            });
        }
        private static void check_alive_process()
        {
            if (m_item_list!=null)
            {
                // 走査して、processの存在確認、なければ process_id を０に
                foreach(var i in m_item_list)
                {
                    if (i.process_id == 0) continue;
                    try {
                        var proc = Process.GetProcessById(i.process_id);
                        if (proc != null)
                        {
                            continue;
                        }
                    } catch
                    {
                    }
                    i.process_id = 0;
                    i.bDirty = true;
                }
                write_items(); //アップデート
            }
        }
        private static void create_mem_ifnotexist()
        {
            if (Is_FirstProcess())
            {
                if (File.Exists(MAPNAME_FULL))
                {
                    try {
                        File.Delete(MAPNAME_FULL);
                    } catch {
                        //デバッグ時は、削除できない。
                    }
                }
            }

            try {
                m_newMMF = MemoryMappedFile.OpenExisting(MAPNAME); //既存確認
            }
            catch
            {
                m_newMMF = null;
            }

            if (m_newMMF == null)
            {
                if (File.Exists(MAPNAME_FULL))
                {
                    m_newMMF = MemoryMappedFile.CreateFromFile(MAPNAME_FULL,FileMode.Open,MAPNAME);
                }
                else
                {
                    m_newMMF = MemoryMappedFile.CreateFromFile(MAPNAME_FULL,FileMode.CreateNew,MAPNAME,MAXSIZE);
                }
            }
        }


        private static byte[] calcMd5_psggfile(string path)
        {
            var ipath = path;
            if ( Path.GetExtension(ipath).ToLower() == ".xlsx" ) //旧ファイル
            { 
                //psgg変換
                ipath = Path.Combine( Path.GetDirectoryName(ipath), Path.GetFileNameWithoutExtension(ipath) + ".psgg" ); 
            }
            //フルネーム化
            ipath = (new FileInfo(ipath)).FullName;
            return calcMd5(ipath);
        }
        
        public static byte[] calcMd5( string srcStr ) {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
        	var srcBytes = System.Text.Encoding.UTF8.GetBytes(srcStr);
	        var destBytes = md5.ComputeHash(srcBytes);
            return destBytes;
        }

        private static byte[] createTextbyte(string s,int max)
        {
            var bytes = new byte[max]; 
            var textbytes = Encoding.UTF8.GetBytes(s);
            for(var i = 0; i<max; i++)
            {
                byte n = i < textbytes.Length ? textbytes[i] : (byte)0;
                bytes[i] = n; 
            }
            return bytes;
        }

        public static IntPtr handle_check_firstOpen;
        public static bool Check_firstOpen()
        {
            var b = Check_firstOpen(G.load_file);
            return b;
        }
        public static bool Check_firstOpen(string i_load_file)
        {
            var load_file = string.Empty;

            // イレギュラー対応
            if (i_load_file.Contains(".psgg.xlsx"))
            {
                load_file = i_load_file.Replace(".psgg.xlsx", ".xlsx");
            }
            else
            {
                load_file = i_load_file;
            }

            load_file = PathUtil.GetFullPath(load_file); //正規化

            G.NoticeToUser("Check_firstOpen :" + load_file);

            //var file = (new FileInfo(load_file)).FullName;  //Path.GetFullPath(G.load_file);

            var proc_id = my_process_id;
            var md5 =calcMd5_psggfile(load_file);
            //var textbytes = createTextbyte(G.view_form.Text, 256-8-16-8);

            create_mem_ifnotexist();

            read_items();

            if (m_item_list== null) throw new SystemException("Unexpected! {96B32462-7542-43F1-90A5-CE9108FC9F2C}");

            // 走査して、processの存在確認、なければ process_id を０に
            check_alive_process();

            // 操作して、同じハッシュがあればfalseを返す
            foreach(var i in m_item_list)
            {
                try
                {
                    if (i.process_id != 0 && i.process_id != proc_id && i.check_filename_hash(md5))
                    {
                        handle_check_firstOpen = (IntPtr)i.handle;
                        return false;
                    }
                }
                catch
                {
                    continue;
                }
            }

            var item = m_item_list.Find(i=>i.process_id == proc_id);
            if (item==null)
            {
                item = m_item_list.Find(i=>i.process_id == 0);
            }
            if (item!=null)
            {
                item.bDirty = true;
                item.process_id = proc_id;
                item.filename_hash = md5;
                item.set_handle(G.view_form.Handle);
                item.set_window_title(G.view_form.Text);

                //更新
                write_items();
            }
            return true;
        }
        public static bool Check_firstOpenByMutex()
        {
            var path = PathUtil.GetFullPath(G.psgg_file);

            var md5 =calcMd5(path);
            var mutexname = "statego_" + Convert.ToBase64String(md5) ;
            //https://dobon.net/vb/dotnet/process/checkprevinstance.html
            //G.NoticeToUser("mutexpath =" + path);
            //G.NoticeToUser("mutex =" + mutexname);
            bool createdNew;
            var mutex = new Mutex(true, mutexname, out createdNew);
            if (!createdNew)
            {
                mutex.Close();
                return false;
            }
            G.mutex = mutex;
            return true;
        }

        
        #region 
        public static void Set_title()
        {
            read_items();
            check_alive_process();
            var item = m_item_list.Find(i=>i.process_id == my_process_id);
            if (item!=null && G.view_form != null)
            {
                item.set_window_title(G.view_form.Text); 
                item.bDirty = true;
                write_items();
            }
        }
        public static Dictionary<int,string> Get_WindowTitles()
        {
            Set_title();

            var list = new Dictionary<int,string>();
            m_item_list.ForEach( i => {
                if (i.process_id!=0)
                {
                    list.Add(i.process_id, i.get_window_title());
                }
            });

            return list;
        }
        public static Dictionary<IntPtr,string> Get_WindowTitles2()
        {
            Set_title();

            var list = new Dictionary<IntPtr,string>();
            m_item_list.ForEach( i => {
                if (i.process_id!=0)
                {
                    list.Add(i.get_handle(), i.get_window_title());
                }
            });

            return list;
        }
        public static Dictionary<IntPtr,byte[]> Get_WindowHandleAndFileHash()
        {
            Set_title();

            var list = new Dictionary<IntPtr,byte[]>();
            m_item_list.ForEach( i => {
                if (i.process_id!=0)
                {
                    list.Add(i.get_handle(), i.filename_hash);
                }
            });

            return list;
        }
        #endregion

        #region Activate Window
        public static bool ActivateWindow(string docname_wo_ext)
        {
            var list = CheckOpenSameDoc.Get_WindowTitles2();
            foreach (var p in list)
            {
                try {
                    var file = RegexUtil.Get1stMatch(@"\[.+?\]", p.Value).Trim('[', ']').Trim();
                    if (Path.GetFileNameWithoutExtension(file) == docname_wo_ext)
                    {
                        WindowsUtil.ActiveWindow(p.Key);
                        return true;
                    }
                } catch { }
            }
            return false;
        }
        //こちらが正確
        public static bool ActivateWindowByHash(string filepath)
        {
            var searchpath = filepath;
            if (
                Path.GetDirectoryName(PathUtil.GetFullPath(filepath))
                ==
                Path.GetDirectoryName(G.load_file)
                &&
                Path.GetFileNameWithoutExtension(filepath)
                ==
                Path.GetFileNameWithoutExtension(G.load_file)
                )
            {
                searchpath = G.load_file;
            }

            searchpath = PathUtil.GetFullPath(searchpath); //正規化

            var md5 = calcMd5_psggfile(searchpath);
            var list = CheckOpenSameDoc.Get_WindowHandleAndFileHash();
            foreach (var p in list)
            {
                if ( ArrayUtil.IsEquals(p.Value,md5))
                {
                    try {
                            WindowsUtil.ActiveWindow(p.Key);
                            return true;
                    } catch { }
                }
            }
            return false;
        }
        #endregion
    }
}
