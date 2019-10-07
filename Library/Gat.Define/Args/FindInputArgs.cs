using Gat.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gat.Define
{
    /// <summary>
    /// BusinessLogic Find 方法的傳入引數。
    /// </summary>
    public class FindInputArgs
    {
        /// <summary>
        /// 要取得的欄位集合字串，以逗點分隔欄位名稱，空字串表示取得所有欄位。
        /// </summary>
        public string SelectFields = string.Empty;

        /// <summary>
        /// 資料過濾項目條件集合。
        /// </summary>
        public FilterItemCollection FilterItems { get; set; } = new FilterItemCollection();

        /// <summary>
        /// 自訂過濾條件。
        /// </summary>
        public string UserFilter { get; set; } = string.Empty;

        /// <summary>
        /// 取消動作
        /// </summary>
        public bool Cancel { get; set; }

        /// <summary>
        /// 是否建立虛擬欄位。
        /// </summary>
        public bool IsBuildVirtualField { get; set; }
    }
}
