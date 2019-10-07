using Gat.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gat.Define
{
    /// <summary>
    /// 程式分類
    /// </summary>
    public class ProgramCategory : KeyCollectionItem
    {
        /// <summary>
        /// 編號
        /// </summary>
        public string ID { get => base.Key; set => base.Key = value; }
        /// <summary>
        /// 顯示名稱
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;
        /// <summary>
        /// 程式項目集合
        /// </summary>
        public ProgramItemCollection Items { get; } = new ProgramItemCollection();
    }
}
