using System;
using System.ComponentModel;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Gat.Base
{
    /// <summary>
    /// 強型別集合成員基礎類別。
    /// </summary>
    public class CollectionItem : ICollectionItem
    {
        #region ICollectionItem 介面

        /// <summary>
        /// 設定所屬集合。
        /// </summary>
        /// <param name="collection">集合。</param>
        public void SetCollection(ICollectionBase collection)
        {
            Collection = collection;
        }

        /// <summary>
        /// 移除成員
        /// </summary>
        public void Remove()
        {
            if (Collection != null)
                Collection.Remove(this);
        }

        #endregion

        /// <summary>
        /// 所屬集合。
        /// </summary>
        [JsonIgnore]
        public ICollectionBase Collection { get; protected set; } = null;
    }
}
