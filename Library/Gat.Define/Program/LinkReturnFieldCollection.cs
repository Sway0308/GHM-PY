using Gat.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gat.Define
{
    /// <summary>
    /// 關連欄位對象集合
    /// </summary>
    [Serializable]
    public class LinkReturnFieldCollection : KeyCollectionBase<LinkReturnField>
    {
        /// <summary>
        /// 根據來源欄位找到關連欄位對象
        /// </summary>
        /// <param name="sourceFieldName">來源欄位名稱</param>
        /// <returns></returns>
        public LinkReturnField FindBySourceField(string sourceFieldName)
        {
            return this.FirstOrDefault(x => x.SourceField.SameText(sourceFieldName));
        }

        /// <summary>
        /// 根據目的欄位找到關連欄位對象
        /// </summary>
        /// <param name="destFieldName">目的欄位名稱</param>
        /// <returns></returns>
        public LinkReturnField FindByDestField(string destFieldName)
        {
            return this.FirstOrDefault(x => x.DestField.SameText(destFieldName));
        }
    }
}
