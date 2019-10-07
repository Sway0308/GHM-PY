using Gat.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gat.Define
{
    /// <summary>
    /// 資料庫清單設定。
    /// </summary>
    public class DatabaseSettings : IDefineFile
    {
        /// <summary>
        /// 取得定義檔案路徑
        /// </summary>
        /// <returns></returns>
        public string GetDefineFilePath()
        {
            return SysDefineSettingName.DbSettingPath;
        }

        /// <summary>
        /// 資料庫集合。
        /// </summary>
        public DatabaseItemCollection Items { get; } = new DatabaseItemCollection();

        /// <summary>
        /// 新增資料庫項目
        /// </summary>
        /// <param name="displayName">顯示名稱</param>
        /// <param name="dbServer">資料庫伺服器</param>
        /// <param name="dbName">資料庫名稱</param>
        /// <param name="loginID">登入帳號</param>
        /// <param name="password">登入密碼</param>
        public void AddDatabaseItem(string displayName, string dbServer, string dbName, string loginID, string password)
        {
            if (StrFunc.SameTextOr(string.Empty, displayName, dbServer, dbName, loginID, password))
                throw new GException("Empty value is not allowed");

            var maxID = this.Items.Max(x => x.DatabaseID) + 1;
            this.Items.Add(new DatabaseItem(maxID)
            {
                DisplayName = displayName,
                DbServer = dbServer,
                DbName = dbName,
                LoginID = loginID,
                Password = password
            });
        }
    }
}
