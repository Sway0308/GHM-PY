namespace Gat.Base
{
    /// <summary>
    /// 強型別成員集合介面。
    /// </summary>
    public interface ICollectionBase
    {
        /// <summary>
        /// 移除成員。
        /// </summary>
        /// <param name="value">成員。</param>
        void Remove(ICollectionItem value);

        /// <summary>
        /// 依索引取得成員。
        /// </summary>
        /// <param name="index">索引。</param>
        object GetItem(int index);

        /// <summary>
        /// 成員數目。
        /// </summary>
        int Count { get; }
    }

}
