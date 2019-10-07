using Gat.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gat.Define
{
    /// <summary>
    /// 實體資料表定義。
    /// </summary>
    public class DbTableDefine : KeyCollectionItem, IProgDefine
    {
        /// <summary>
        /// 取得定義檔案路徑
        /// </summary>
        /// <returns></returns>
        public string GetDefineFilePath()
        {
            return SysDefineSettingName.DbTableDefineFilePath(this.ProgId);
        }

        /// <summary>
        /// 程式代碼
        /// </summary>
        public string ProgId { get; set; }

        /// <summary>
        /// 資料表名稱
        /// </summary>
        public string TableName { get => base.Key; set => base.Key = value; }
        /// <summary>
        /// 顯示名稱
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;
        /// <summary>
        /// 欄位定義集合
        /// </summary>
        public DbFieldDefineCollection Fields { get; } = new DbFieldDefineCollection();
    }
}
