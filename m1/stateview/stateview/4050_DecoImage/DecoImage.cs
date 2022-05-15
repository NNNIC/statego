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

namespace stateview
{

    public class DecoImage
    {
        /*
            おそらく
            モードによって、フォルダを変更
            調整情報 position.iniファイルがフォルダにある。なければ、デフォルトとなる。

        */

        public static string typ_image_folder { get { return G.decoimage_typ_name;} }// = "sym";//"Vladmir-Script";//"Gyosyo";//"Vladmir-Script"; 
        public static string dco_image_folder = ""; //undefined

        public enum Anchor
        {
            none,
            TL,TC,TR,  //top-left, center or right
            ML,MC,MR,  //mid-
            BL,BC,BR   //bottom-
        }

        public class Data
        {
            public string image_folder;          //ビットマップファイルのパス。ハッシュとして利用して、異なるときは前のを破棄させる。
            public Bitmap bitmap;
            public Anchor anchor_self;   //自ビットマップのどこ？
            public Anchor anchor_target; //対象となるビットマップのどこ？
            public Point  location;      //対象アンカーからみた自アンカーの位置
            public int    pri_add;       //ステートの描画プライオリティに追加 
            public float  scale;         //拡縮

            //便利
            public PointF GetBmpPos(RectangleF frect)
            {
                var frame_anchor_pos = frect.Location;
                switch(anchor_target)
                {
                    case Anchor.TL:  frame_anchor_pos =frect.Location;                                                   break;
                    case Anchor.TC:  frame_anchor_pos = PointUtil.Add_X(frect.Location,frect.Width/2)  ;                 break;
                    case Anchor.TR:  frame_anchor_pos = PointUtil.Add_X(frect.Location,frect.Width);                     break;

                    case Anchor.ML:  frame_anchor_pos = PointUtil.Add_Y(frect.Location,frect.Height/2);                  break;
                    case Anchor.MC:  frame_anchor_pos = PointUtil.Add_XY(frect.Location, frect.Width/2, frect.Height/2); break;
                    case Anchor.MR:  frame_anchor_pos = PointUtil.Add_XY(frect.Location, frect.Width,   frect.Height/2); break;

                    case Anchor.BL:  frame_anchor_pos = PointUtil.Add_Y(frect.Location,frect.Height);                    break;
                    case Anchor.BC:  frame_anchor_pos = PointUtil.Add_XY(frect.Location, frect.Width/2, frect.Height);   break;
                    case Anchor.BR:  frame_anchor_pos = PointUtil.Add_XY(frect.Location, frect.Width,   frect.Height);   break;
                }

                if (bitmap == null) return frame_anchor_pos;

                var bmp_anchor_pos = PointUtil.Add_Point(frame_anchor_pos, location);
                switch(anchor_self)
                {
                    case Anchor.TL: return bmp_anchor_pos;
                    case Anchor.TC: return PointUtil.Add_X(bmp_anchor_pos, - bitmap.Width/2);
                    case Anchor.TR: return PointUtil.Add_X(bmp_anchor_pos, - bitmap.Width);

                    case Anchor.ML: return PointUtil.Add_Y(bmp_anchor_pos, - bitmap.Height/2);
                    case Anchor.MC: return PointUtil.Add_XY(bmp_anchor_pos,- bitmap.Width/2, - bitmap.Height/2);
                    case Anchor.MR: return PointUtil.Add_XY(bmp_anchor_pos,- bitmap.Width,   - bitmap.Height/2);

                    case Anchor.BL: return PointUtil.Add_Y(bmp_anchor_pos, - bitmap.Height);
                    case Anchor.BC: return PointUtil.Add_XY(bmp_anchor_pos,- bitmap.Width/2, - bitmap.Height);
                    case Anchor.BR: return PointUtil.Add_XY(bmp_anchor_pos,- bitmap.Width,   - bitmap.Height);
                }
                return bmp_anchor_pos;
            }

        }
        
        public static Data GetTypImageData(string name)
        {
            //System.Diagnostics.Debugger.Launch();

            if (string.IsNullOrEmpty(name)) return null;
            if (m_cache_dic.ContainsKey(name))
            {
                var cd = m_cache_dic[name];
                if (cd!=null && cd.image_folder!=typ_image_folder)
                {
                    if (cd.bitmap!=null) cd.bitmap.Dispose();
                    cd = null;
                }

                if (cd!=null) return cd;
            }

            try { 
                var d = _create_new_data(name,typ_image_folder);

                DictionaryUtil.Set(m_cache_dic,name,d);
                return d;
            } catch(SystemException e)
            {
                return null;
            }
        }
        static Data _create_new_data(string name, string typ_image_folder)
        {
            Data d = new Data();
            d.image_folder = typ_image_folder;
            d.scale = 0.7f;

            d.anchor_self   = Anchor.BL;//Anchor.BC;
            d.anchor_target = Anchor.TL;//Anchor.TC;
            d.location      = new Point(0, 0);
            d.pri_add       = 0;

            try { 
                var inipath = Path.Combine(G.decoimage_typ_folder, typ_image_folder, "_pos.ini");
                if (File.Exists(inipath))
                {
                    var initext = File.ReadAllText(inipath);
                    if (!string.IsNullOrEmpty(initext))
                    {
                        var ht = IniUtil.CreateHashtable(initext);
                        if (ht!=null)
                        { 
                            __read_float("scale",name,ht, ref d.scale);
                            __read_anchor("anchor_self",name,ht, ref d.anchor_self);
                            __read_anchor("anchor_target",name,ht, ref d.anchor_target);
                            __read_int("pri_add",name,ht, ref d.pri_add);
                            __read_point("location",name,ht, ref d.location); 
                        }
                    }
                }
                d.bitmap = GetTypImage(name,d.scale);
            }
            catch
            {
            }
            return d;
        }
        static void __read_float(string item, string name, Hashtable ht, ref float fval)
        {
            var valstr = ___get_val(item,name,ht);
            var f = ParseUtil.ParseFloat(valstr);
            if (f!=float.MinValue)
            {
                fval = f;
            }
        }
        static void __read_anchor(string item, string name, Hashtable ht, ref Anchor anc)
        {
            var valstr= ___get_val(item,name,ht);
            var ret = EnumUtil.Parse(valstr,Anchor.none);
            if (ret!=Anchor.none)
            {
                anc = ret;
            }
        }
        static void __read_int(string item, string name, Hashtable ht, ref int valint)
        {
            var valstr= ___get_val(item,name,ht);
            var i = ParseUtil.ParseInt(valstr);
            if (i != int.MinValue)
            {
                valint = i;
            }
        }
        static void __read_point(string item, string name, Hashtable ht, ref Point pos)
        {
            var valstr= ___get_val(item,name,ht);
            var ary = ParseUtil.ParseIntArray(valstr);
            if (ary!=null && ary.Length==2)
            {
                pos = new Point(ary[0],ary[1]);
            }
        }
        static string ___get_val(string item, string name, Hashtable ht)
        {
            string _DEF_CATEGORY = "default";
            var category  = name;
            var key       = item;
            string valstr = null;
            if (IniUtil.HasKeyFromHashtable(category,key,ht))
            {
                valstr = IniUtil.GetValueFromHashtable(category,key,ht);
            }
            else if (IniUtil.HasKeyFromHashtable(_DEF_CATEGORY,key,ht))
            {
                valstr = IniUtil.GetValueFromHashtable(_DEF_CATEGORY,key,ht);
            }
            return valstr;
        }

        static Bitmap GetTypImage(string name)
        {
            //まずはテストで、
            var path = G.decoimage_typ_folder;
            path = Path.Combine(path , typ_image_folder + @"\" + name + ".png" );

            if (File.Exists(path))
            {
                return new Bitmap(path);
            }
            return null;
        }
        static Bitmap GetTypImage(string name, float scale)
        {
            var path = G.decoimage_typ_folder;
            path = Path.Combine(path , typ_image_folder + @"\" + name + ".png" );
            
            if (File.Exists(path))
            {
                var bmp = new Bitmap(path);
                if (scale == 1)
                {
                    return bmp;
                }
                var w = (int)(bmp.Width * scale);
                var h = (int)(bmp.Height * scale);
                if (w < 0) w = 1;
                if (h < 0) h =1;
                var resizebmp = new Bitmap(w,h);
                var g = Graphics.FromImage(resizebmp);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(bmp,0,0,w,h);
                g.Dispose();
                bmp.Dispose();

                return resizebmp;
            }
            return null;
        }

        static Dictionary<string,Data> m_cache_dic = new Dictionary<string, Data>(); 

        public static void Reset()
        {
            m_cache_dic.Clear();
        }
    }
}
