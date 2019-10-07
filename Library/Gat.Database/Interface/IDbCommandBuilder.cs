using System;
using System.Data.Common;
using Gat.Define;

namespace Gat.Database
{
    /// <summary>
    /// 資料庫命令產生器介面，產生 Select、Insert、Update、Delete 的資料庫命令。
    /// </summary>
    public interface IDbCommandBuilder
    {
        /// <summary>
        /// 建立 Select 語法的資料庫命令。
        /// </summary>
        /// <param name="inputArgs">Select 方法的傳入引數。</param>
        DbCommand BuildSelectCommand(SelectInputArgs inputArgs);

        /// <summary>
        /// 建立 Select 語法的資料庫命令。
        /// </summary>
        /// <param name="tableName">資料表名稱。</param>
        /// <param name="selectFields">要取得的欄位集合字串，以逗點分隔欄位名稱，空字串表示取得所有欄位。</param>
        /// <param name="filterItems">資料過濾條件項目集合。</param>
        /// <param name="userFilter">自訂過濾條件。</param>
        /// <param name="isOrderBy">執行排序</param>
        DbCommand BuildSelectCommand(string tableName, string selectFields, FilterItemCollection filterItems, string userFilter, bool isOrderBy = true);

        /// <summary>
        /// 建立 Insert 語法的資料庫命令。
        /// </summary>
        /// <param name="tableName">資料表名稱。</param>
        DbCommand BuildInsertCommand(string tableName);

        /// <summary>
        /// 建立 Update 語法的資料庫命令。
        /// </summary>
        /// <param name="tableName">資料表名稱。</param>
        DbCommand BuildUpdateCommand(string tableName);
        
        /// <summary>
        /// 建立 Delete 語法的資料庫命令。
        /// </summary>
        /// <param name="tableName">資料表名稱。</param>
        DbCommand BuildDeleteCommand(string tableName);

        /// <summary>
        /// 建立符合過濾條件的 Delete 語法的資料庫命令。
        /// </summary>
        /// <param name="tableName">資料表名稱。</param>
        /// <param name="filterItems">資料過濾條件項目集合. </param>
        DbCommand BuildDeleteCommand(string tableName, FilterItemCollection filterItems);
    }
}
