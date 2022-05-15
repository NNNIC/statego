using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vm
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length==0) { Console.WriteLine("vm.exe {prefix} [start-date-string] "); return;}
            var prefix = args[0];
            var starttime = DateTime.MinValue;
                ;
            if (args.Length>1)
            {
                starttime = DateTime.Parse(args[1]);
            }

            var now = DateTime.Now;

            var diff = now - starttime;

            var min = (int)diff.TotalMinutes;

            Console.Write(prefix + min);
        }
    }
}
