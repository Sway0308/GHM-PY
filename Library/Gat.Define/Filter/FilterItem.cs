using Gat.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gat.Define
{
    /// <summary>
    /// 資料過濾條件項目。
    /// </summary>
    public class FilterItem : KeyCollectionItem
    {
        #region 建構函式

        /// <summary>
        /// 建構函式。
        /// </summary>
        public FilterItem()
        { }

        /// <summary>
        /// 建構函式。
        /// </summary>
        /// <param name="fieldName">查詢欄位名稱。</param>
        /// <param name="filterValue">查詢值。</param>
        public FilterItem(string fieldName, string filterValue)
        {
            FieldName = fieldName;
            FilterValue = filterValue;
        }

        /// <summary>
        /// 建構函式。
        /// </summary>
        /// <param name="fieldName">查詢欄位名稱。</param>
        /// <param name="filterValue">查詢值。</param>
        /// <param name="comparisonOperator">比較運算子</param>
        public FilterItem(string fieldName, string filterValue, EComparisonOperator comparisonOperator)
        {
            FieldName = fieldName;
            FilterValue = filterValue;
            ComparisonOperator = comparisonOperator;
        }

        #endregion

        /// <summary>
        /// 查詢欄位名稱。
        /// </summary>
        public string FieldName { get; set; } = string.Empty;

        /// <summary>
        /// 查詢值。
        /// </summary>
        public string FilterValue { get; set; } = string.Empty;

        /// <summary>
        /// 比較運算子。
        /// </summary>
        public EComparisonOperator ComparisonOperator { get; set; } = EComparisonOperator.Equal;

        /// <summary>
        /// 結合運算子。
        /// </summary>
        public ECombineOperator CombineOperator { get; set; } = ECombineOperator.And;

        /// <summary>
        /// 條件群組編號。
        /// </summary>
        public int GroupNumber { get; set; } = -1;

        /// <summary>
        /// 必要條件。
        /// </summary>
        public bool Required { get; set; } = false;

        /// <summary>
        /// 建立物件複本。
        /// </summary>
        public FilterItem Clone()
        {
            FilterItem oItem;

            oItem = new FilterItem
            {
                FieldName = this.FieldName,
                FilterValue = this.FilterValue,
                CombineOperator = this.CombineOperator,
                ComparisonOperator = this.ComparisonOperator,
                GroupNumber = this.GroupNumber,
                Required = this.Required
            };
            return oItem;
        }

        /// <summary>
        /// 物件描述文字。
        /// </summary>
        public override string ToString()
        {
            return string.Format("{0} {1} {2}", this.FieldName, this.ComparisonOperator, this.FilterValue);
        }

        /// <summary>
        /// 取得運算子。
        /// </summary>
        public string GetComparisonText()
        {
            switch (this.ComparisonOperator)
            {
                case EComparisonOperator.Equal: return "=";
                case EComparisonOperator.NotEqual: return "<>";
                case EComparisonOperator.Less: return "<";
                case EComparisonOperator.LessOrEqual: return "<=";
                case EComparisonOperator.Greater: return ">";
                case EComparisonOperator.GreaterOrEqual: return ">=";
                case EComparisonOperator.Like: return "LIKE";
                case EComparisonOperator.Between: return "BETWEEN";
                case EComparisonOperator.In: return "IN";
                case EComparisonOperator.NotIn: return "NOT IN";
                default: // Variable
                    return "VARIABLE";
            }
        }
    }
}
