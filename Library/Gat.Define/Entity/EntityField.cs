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
    /// Entity 欄位
    /// </summary>
    [Obsolete]
    public class EntityField : KeyCollectionItem
    {
        /// <summary>
        /// 建構函式
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="value"></param>
        public EntityField(string fieldName, object value)
        {
            this.FieldName = fieldName;
            this.OriginalValue = value;
            this.Value = value;
        }

        /// <summary>
        /// 設定欄位值
        /// </summary>
        /// <param name="value"></param>
        public void SetValue(object value)
        {
            this.Value = value;
        }

        /// <summary>
        /// 欄位名稱
        /// </summary>
        public string FieldName { get => base.Key; private set => base.Key = value; }
        /// <summary>
        /// 原始欄位值
        /// </summary>
        [JsonIgnore]
        public object OriginalValue { get; }
        /// <summary>
        /// 目前欄位值
        /// </summary>
        public object Value { get; set; }
    }
}
