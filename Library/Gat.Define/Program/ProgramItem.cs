using Gat.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gat.Define
{
    /// <summary>
    /// 程式項目
    /// </summary>
    [Serializable]
    public class ProgramItem : KeyCollectionItem
    {
        /// <summary>
        /// 建構函式
        /// </summary>
        /// <param name="progId">程式代碼</param>
        public ProgramItem(string progId)
        {
            ProgId = progId;
        }

        /// <summary>
        /// 程式代碼
        /// </summary>
        public string ProgId { get => this.Key; set => this.Key = value; }
        /// <summary>
        /// 顯示名稱
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;
        /// <summary>
        /// 商業邏輯載入物件型別
        /// </summary>
        public InstanceType BusinessInstanceType { get; set; } = new InstanceType();
    }
}
