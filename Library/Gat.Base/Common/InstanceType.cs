using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gat.Base
{
    /// <summary>
    /// 動態載入物件的型別描述
    /// </summary>
    public class InstanceType
    {
        /// <summary>
        /// 組件檔案名稱。
        /// </summary>
        public string AssemblyFile { get; set; } = string.Empty;

        /// <summary>
        /// 物件型別。
        /// </summary>
        public string TypeName { get; set; } = string.Empty;

        /// <summary>
        /// 設定值是否為空
        /// </summary>
        [JsonIgnore]
        public bool IsEmpty { get => this.AssemblyFile.IsEmpty() || this.TypeName.IsEmpty(); }
    }
}
