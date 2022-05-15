using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;


public class HashUtil
{
    public static string makehash(string s)
    {
        var srcbytes = Encoding.UTF8.GetBytes(s);
        var md5 = new MD5CryptoServiceProvider();
        var md5bytes = md5.ComputeHash(srcbytes);
        var b64str = Convert.ToBase64String(md5bytes);
        return b64str;
    }

    public static string makehash(byte[] srcbytes)
    {
        var md5 = new MD5CryptoServiceProvider();
        var md5bytes = md5.ComputeHash(srcbytes);
        var b64str = Convert.ToBase64String(md5bytes);
        return b64str;
    }
}
