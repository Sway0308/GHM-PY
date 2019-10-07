using Gat.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gat.Define
{
    /// <summary>
    /// 排序欄位集合。
    /// </summary>
    public class SortFieldCollection : KeyCollectionBase<SortField>
    {
        /// <summary>
        /// 取得排序欄位文字
        /// </summary>
        /// <returns></returns>
        public string GetSortedText()
        {
            var result = "";
            foreach (SortField f in this)
            {
                result += (StrFunc.StrIsNotEmpty(result) ? ", " : "") +
                    f.FieldName +
                    (f.SortDirection == ESortDirection.Ascending ? "" : " Desc");
            }

            return result;
        }
    }
}
