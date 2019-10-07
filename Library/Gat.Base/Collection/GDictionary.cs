using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gat.Base
{
    /// <summary>
    /// 索引鍵和值的集合(鍵值不區分大小寫)。 
    /// </summary>
    public class GDictionary<T> : Dictionary<string, T>
    {
        /// <summary>
        /// 建構函式。
        /// </summary>
        public GDictionary() : base(StringComparer.InvariantCultureIgnoreCase)
        { }

        /// <summary>
        /// 加入成員
        /// </summary>
        /// <param name="key">索引鍵。</param>
        /// <param name="value">值。</param>
        public void AddReplace(string key, T value)
        {
            // 若索引鍵已存在則移除
            if (this.ContainsKey(key))
                this.Remove(key);
            // 加入索引鍵及值
            this.Add(key, value);
        }
    }
}
