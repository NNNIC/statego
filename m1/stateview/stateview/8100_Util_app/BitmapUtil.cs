using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;

namespace stateview
{
    public class BitmapUtil
    {
        public static string makehash(Bitmap bmp)
        {
            try
            {
                using (var ms = new MemoryStream())
                {
                    bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    var data = ms.ToArray();
                    return HashUtil.makehash(data);
                }
            }
            catch
            {
            
            }
            return string.Empty;
        }
    }
}
