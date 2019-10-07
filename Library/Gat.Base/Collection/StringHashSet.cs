using System;
using System.Collections.Generic;
using System.Text;

namespace Gat.Base
{
    /// <summary>
    /// 不允許重覆的字串集合，字串忽略大小寫。
    /// </summary>
    public class StringHashSet : HashSet<string>
    {
        #region 建構函式

        /// <summary>
        /// 建構函式。
        /// </summary>
        public StringHashSet() : base(StringComparer.InvariantCultureIgnoreCase)
        { }

        /// <summary>
        /// 建構函式。
        /// </summary>
        /// <param name="s">字串。</param>
        /// <param name="delimiter">分隔符號。</param>
        public StringHashSet(string s, string delimiter) : this()
        {
            this.Add(s, delimiter);
        }

        #endregion

        /// <summary>
        /// 將集合成員依分隔符號組成字串。
        /// </summary>
        /// <param name="delimiter">分隔符號。</param>
        public string ToString(string delimiter)
        {
            var buffer = new StringBuilder();
            foreach (var item in this)
            {
                if (buffer.Length > 0) { buffer.Append(delimiter); }
                buffer.Append(item);
            }
            return buffer.ToString();
        }

        /// <summary>
        /// 加入字串陣列為成員。
        /// </summary>
        /// <param name="array">字串陣列。</param>
        public void Add(string[] array)
        {
            foreach (var value in array)
                this.Add(value);
        }

        /// <summary>
        /// 加入集合字串為成員。
        /// </summary>
        /// <param name="s">字串。</param>
        /// <param name="delimiter">分隔符號。</param>
        public void Add(string s, string delimiter)
        {
            if (StrFunc.StrIsEmpty(s)) { return;  }

            var values = StrFunc.StrSplit(s, delimiter);
            foreach (var value in values)
            {
                if (!this.Contains(value))
                    this.Add(value);
            }
        }
    }
}
