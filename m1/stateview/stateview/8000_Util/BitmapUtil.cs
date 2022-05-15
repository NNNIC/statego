using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using G=stateview.Globals;

public class BitmapUtil
{
    public static string ToBase64(Bitmap bmp)
    {
        try { 
            using (var ms = new MemoryStream())
            {
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                var data = ms.ToArray();
                return Convert.ToBase64String(data);
            }
        } catch (SystemException e)
        {
            G.NoticeToUser_warning("{5AE10585-DC1A-4BA8-86C8-5799179B00B3}" + e.Message);
        }
        return null;
    }
    public static Bitmap FromBase64(string s)
    {
        try { 
            var data = Convert.FromBase64String(s);
            using (var ms = new MemoryStream(data))
            {
                var bmp = new Bitmap(ms);
                return bmp;
            }
        } catch (SystemException e)
        {
            G.NoticeToUser_warning("{B002AE2E-38AA-4CB5-A81C-8E34A22E17CE}" + e.Message);
        }
        return null;
    }
    public static string GetHash(Bitmap bmp)
    {
        try { 
            using (var ms = new MemoryStream()) { 
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                var data = ms.ToArray();
                return HashUtil.makehash(data);
            }
        } catch (SystemException e)
        {
            G.NoticeToUser_warning("{0ABC618C-FC6D-479D-BCFB-B13F4C7B1ADA}" + e.Message);
        }
        return null;
    }
    public static string GetHash_escape_for_filename(Bitmap bmp)
    {
        return FileUtil.EncodeFilenameFromBase64( GetHash(bmp) );
    }
}
