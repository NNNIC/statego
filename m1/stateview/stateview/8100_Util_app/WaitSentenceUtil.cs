using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stateview
{
    // wait文用
    internal class WaitSentenceUtil
    {
        public static int? GetMillsec(string waitstr)
        {
            if (!RegexUtil.IsMatch(@"\[\bwait time\=[0-9]+\b",waitstr))
            {
                return null;
            }
            var word = RegexUtil.Get1stMatch(@"\=[0-9]+\b",waitstr);
            if (string.IsNullOrEmpty(word)) return null;
            var word2 = word.Trim('=').Trim();
            return int.Parse(word2);
        }

        public static string GetWaitString(string millisec)
        {
            var waittime = 100;
            if (int.TryParse(millisec,out waittime))
            {
                if (waittime < 1)
                {
                    waittime = 100;
                }
            }

            return string.Format("[wait time={0}]",waittime.ToString());
        }

    }
}
