using Gat.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Gat.Define
{
    /// <summary>
    /// 關連欄位對象
    /// </summary>
    [Serializable]
    public class LinkReturnField : KeyCollectionItem
    {
        /// <summary>
        /// 來源欄位
        /// </summary>
        public string SourceField { get => this.Key; set => this.Key = value; }
        /// <summary>
        /// 目的欄位
        /// </summary>
        public string DestField { get; set; }
    }
}
