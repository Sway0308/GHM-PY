using Gat.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gat.Define
{
    /// <summary>
    /// BusinessLogic Select 方法的傳入引數。
    /// </summary>
    public class SelectInputArgs
    {
        /// <summary>
        /// 資料表名稱
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 資料過濾項目條件集合。
        /// </summary>
        public FilterItemCollection FilterItems { get; set; } = new FilterItemCollection();

        /// <summary>
        /// 要取得的欄位集合字串，以逗點分隔欄位名稱，空字串表示取得所有欄位。
        /// </summary>
        public string SelectFields { get; set; } = string.Empty;

        /// <summary>
        /// 自訂過濾條件。
        /// </summary>
        public string UserFilter { get; set; } = string.Empty;

        /// <summary>
        /// 執行排序
        /// </summary>
        public bool IsOrderBy { get; set; } = true;

        /// <summary>
        /// 取消動作
        /// </summary>
        public bool Cancel { get; set; }

        /// <summary>
        /// 是否建立虛擬欄位。
        /// </summary>
        public bool IsBuildVirtualField { get; set; } = true;
    }
}
