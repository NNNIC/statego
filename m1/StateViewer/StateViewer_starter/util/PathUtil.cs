using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PathUtil
{
    public static string GetRelativePath(string baseUri,　string targetUri)
    {
        if (string.IsNullOrEmpty(baseUri) || string.IsNullOrEmpty(targetUri)) return null;
        var u1 = new Uri(baseUri + @"\");
        var u2 = new Uri(targetUri+ @"\");

        var relativeUri = u1.MakeRelativeUri(u2);

        var relativePath = relativeUri.ToString();

        relativePath = relativePath.Replace('/', '\\');

        //Console.WriteLine(relativePath);

        return relativePath.TrimEnd('\\');
    }
}

