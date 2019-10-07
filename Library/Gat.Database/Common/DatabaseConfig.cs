using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gat.Database
{
    /// <summary>
    /// 資料庫命令變數名稱常數。
    /// </summary>
    public class CommandTextVariable
    {
        /// <summary>
        /// 參數名稱集合字串。
        /// </summary>
        public const string Parameters = "{@Parameters}";
    }

    /// <summary>
    /// 欄位比對後的處理動作。
    /// </summary>
    public enum EFieldCompareAction
    {
        /// <summary>
        /// 不存在該欄位，要新增欄位。
        /// </summary>
        Add,
        /// <summary>
        /// 多餘欄位，要刪除欄位。
        /// </summary>
        Remove,
        /// <summary>
        /// 欄位結構不同，要更新欄位。
        /// </summary>
        Update,
    }
}
