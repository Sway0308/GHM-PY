using Gat.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gat.Define
{
    /// <summary>
    /// Entity 欄位集合
    /// </summary>
    [Obsolete]
    public class EntityFieldCollection : KeyCollectionBase<EntityField>
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="fieldName">欄位名稱</param>
        /// <param name="value">欄位值</param>
        public void Add(string fieldName, object value)
        {
            var item = new EntityField(fieldName, value);
            base.Add(item);
        }
    }
}
