using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gat.Base;

namespace Gat.Define
{
    /// <summary>
    /// 資料庫設定項目。
    /// </summary>
    public class DatabaseItem : KeyCollectionItem
    {
        /// <summary>
        /// 建構函式
        /// </summary>
        /// <param name="databaseID">資料庫編號</param>
        public DatabaseItem(string databaseID)
        {
            DatabaseID = databaseID;
        }

        /// <summary>
        /// 資料庫編號
        /// </summary>
        public string DatabaseID { get => base.Key; set => base.Key = value; }
        /// <summary>
        /// 顯示名稱
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;
        /// <summary>
        /// 資料庫伺服器
        /// </summary>
        public string DbServer { get; set; } = string.Empty;
        /// <summary>
        /// 資料庫名稱
        /// </summary>
        public string DbName { get; set; } = string.Empty;
        /// <summary>
        /// 登入帳號
        /// </summary>
        public string LoginID { get; set; } = string.Empty;
        /// <summary>
        /// 登入密碼
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}
