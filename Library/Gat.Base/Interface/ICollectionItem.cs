namespace Gat.Base
{
    /// <summary>
    /// 強型別成員集合的成員類別介面。
    /// </summary>
    public interface ICollectionItem
    {
        /// <summary>
        /// 由集合中移除此成員。
        /// </summary>
        void Remove();

        /// <summary>
        /// 設定所屬集合。
        /// </summary>
        /// <param name="collection">集合。</param>
        void SetCollection(ICollectionBase collection);
    }
    
}
