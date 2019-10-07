using Gat.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gat.Define
{
    /// <summary>
    /// 排序欄位。
    /// </summary>
    public class SortField : KeyCollectionItem
{
        /// <summary>
        /// 建構函式。
        /// </summary>
        /// <param name="fieldName">欄位名稱。</param>
        /// <param name="sortDirection">排序方式。</param>
        public SortField(string fieldName, ESortDirection sortDirection)
        {
            this.FieldName = fieldName;
            this.SortDirection = sortDirection;
        }

        /// <summary>
        /// 欄位名稱。
        /// </summary>
        public string FieldName { get { return base.Key; } set { base.Key = value; } }

        /// <summary>
        /// 排序方式。
        /// </summary>
        public ESortDirection SortDirection { get; set; } = ESortDirection.Ascending;

        /// <summary>
        /// 產生物件複本。
        /// </summary>
        public SortField Clone()
        {
            return new SortField(this.FieldName, this.SortDirection);
        }

        /// <summary>
        /// 物件的描述文字。
        /// </summary>
        public override string ToString()
        {
            return string.Format("{0} {1}", this.FieldName, this.SortDirection);
        }
    }
    
}
