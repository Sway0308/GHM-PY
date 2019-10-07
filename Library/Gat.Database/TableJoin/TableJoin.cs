using Gat.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gat.Database
{
    /// <summary>
    /// 資料表關連。
    /// </summary>
    public class TableJoin : KeyCollectionItem
    {
        /// <summary>
        /// 左側資料表別名。
        /// </summary>
        public string LeftTableAlias { get; set; } = string.Empty;

        /// <summary>
        /// 左側欄位名稱。
        /// </summary>
        public string LeftFieldName { get; set; } = string.Empty;

        /// <summary>
        /// 右側資料表名稱。
        /// </summary>
        public string RightTableName { get; set; } = string.Empty;

        /// <summary>
        /// 右側資料表別名。
        /// </summary>
        public string RightTableAlias { get; set; } = string.Empty;

        /// <summary>
        /// 右側欄位名稱。
        /// </summary>
        public string RightFieldName { get; set; } = string.Empty;

        /// <summary>
        /// 右側公司編號欄位名稱。
        /// </summary>
        public string RightCompanyID { get; set; } = string.Empty;

        /// <summary>
        /// 物件描述文字。
        /// </summary>
        public override string ToString()
        {
            string sValue;

            sValue = string.Format("Left Join {2} {3} On {0}.{1}={3}.{4}", this.LeftTableAlias, this.LeftFieldName, this.RightTableName, this.RightTableAlias, this.RightFieldName);
            return sValue;
        }
    }
}
