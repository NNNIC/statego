using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stateview
{
    /// <summary>
    /// ステートに流入するステートラベル
    /// ステートから流出するステートラベル
    /// ステートが参照するベースのステートラベル
    /// ステートがベースとして参照されるステートラベル
    /// 以上に利用される。
    /// </summary>
    public class InOutBaseData
    {
        public enum ATTRIB
        {
            nextstate,
            branch,
            gosub,
            _base
        }
        public string target_state;
        public ATTRIB attrib;
        public string state;
        public int    branch_index;
    }
}
