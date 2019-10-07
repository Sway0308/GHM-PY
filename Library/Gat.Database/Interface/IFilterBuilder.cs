namespace Gat.Database
{
    /// <summary>
    /// 過濾條件產生器介面。
    /// </summary>
    public interface IFilterBuilder
    {
        /// <summary>
        /// 取得過濾條件字串。
        /// </summary>
        /// <param name="join">傳出過濾條件需要額外加入的 JOIN 語法。</param>
        string GetFilter(out string join);
    }
}
