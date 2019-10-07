using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gat.Base
{
    /// <summary>
    /// 傳遞參數。
    /// </summary>
    public class Parameter : KeyCollectionItem
    {
        /// <summary>
        /// 建構函式
        /// </summary>
        /// <param name="name">參數名稱。</param>
        /// <param name="value">參數值。</param>
        public Parameter(string key, object value)
        {
            this.Key = key;
            this.Value = value;
        }

        /// <summary>
        /// 參數名稱。
        /// </summary>
        public string Name { get => base.Key; set => base.Key = value; }
        /// <summary>
        /// 參數值。
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// 物件描述
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}.{1}", this.Name, this.Value?.ToString());
        }
    }
}
