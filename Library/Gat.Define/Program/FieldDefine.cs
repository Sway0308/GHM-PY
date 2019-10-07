using Gat.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Gat.Define
{
    /// <summary>
    /// 欄位定義
    /// </summary>
    [Serializable]
    public class FieldDefine : BaseField
    {
        private string _DbFieldName = string.Empty;

        /// <summary>
        /// 實體欄位名稱。
        /// </summary>
        public string DbFieldName
        {
            get
            {
                if (StrFunc.StrIsEmpty(_DbFieldName))
                    return this.FieldName;
                else
                    return _DbFieldName;
            }
            set { _DbFieldName = value; }
        }
        /// <summary>
        /// 欄位類型
        /// </summary>
        public EFieldType FieldType { get; set; } = EFieldType.DataField;
        /// <summary>
        /// 最大長度
        /// </summary>
        public int MaxLength { get; set; }
        /// <summary>
        /// 是否允許 Null 值
        /// </summary>
        public bool AllowNull { get; set; }
        /// <summary>
        /// 關連欄位必填屬性，設定 Select 語法中，關連欄位是由那個來源編號欄位一併取回。
        /// </summary>
        public string LinkFieldName { get; set; } = string.Empty;
        /// <summary>
        /// 關連對象的程式代碼
        /// </summary>
        public string LinkProgId { get; set; } = string.Empty;
        /// <summary>
        /// 關聯取回對象
        /// </summary>
        public LinkReturnFieldCollection LinkReturnFields { get; } = new LinkReturnFieldCollection();

        /// <summary>
        /// 資料表定義。
        /// </summary>
        [JsonIgnore]
        public TableDefine Table => ((FieldDefineCollection)this.Collection).Owner as TableDefine;
    }
}
