namespace Gat.Base
{
    /// <summary>
    /// 鍵值成員介面。
    /// </summary>
    public interface IKeyCollectionItem
    {
        /// <summary>
        /// 鍵值。
        /// </summary>
        string Key { get; set; }

        /// <summary>
        /// 設定所屬集合。
        /// </summary>
        /// <param name="collection">集合。</param>
        void SetCollection(IKeyCollectionBase collection);
    }
}