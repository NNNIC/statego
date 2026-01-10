using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace psggConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location),"psggConverterLib.dll");
            var dll = Assembly.LoadFrom(path);
            dynamic converter = Activator.CreateInstance(dll.GetType("psggConverterLib.Convert"));
            converter.TEST();
        }
    }
}
