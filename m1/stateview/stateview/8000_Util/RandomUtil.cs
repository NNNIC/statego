using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class RandomUtil
{
    private static Random m_rand = null;

    /// <summary>
    /// 最小値、最大値を含む数からランダムに選択
    /// </summary>
    public static int Select(int min, int max)
    {
        if (m_rand == null)
        {
            m_rand = new Random((int)DateTime.Now.ToBinary() + System.Environment.TickCount);
        }
        var n = m_rand.Next();
        var range = max - min + 1; //最大値も含むため
        var select = n % range; // 0～ range-1
        return min + select;
    }
}

