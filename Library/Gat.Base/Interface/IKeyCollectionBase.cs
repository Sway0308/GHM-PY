namespace Gat.Base
{
    /// <summary>
    /// 鍵值成員集合介面。
    /// </summary>
    public interface IKeyCollectionBase : ICollectionBase
    {
        /// <summary>
        /// 擁有者。
        /// </summary>
        object Owner { get; }
    }
}
