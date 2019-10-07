using Gat.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Gat.Define
{
    /// <summary>
    /// 程式定義
    /// </summary>
    [Serializable]
    public class ProgramDefine : KeyCollectionItem, IProgDefine
    {
        /// <summary>
        /// 建構函式
        /// </summary>
        public ProgramDefine()
        {
            this.Tables = new TableDefineCollection(this);
        }

        /// <summary>
        /// 取得定義檔案路徑
        /// </summary>
        /// <returns></returns>
        public string GetDefineFilePath()
        {
            return SysDefineSettingName.ProgramDefineFilePath(this.ProgId);
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
        /// 資料表定義集合
        /// </summary>
        public TableDefineCollection Tables { get; }
        /// <summary>
        /// 主資料表定義
        /// </summary>
        [JsonIgnore]
        public TableDefine MasterTable => this.Tables[this.ProgId];
        /// <summary>
        /// 主資料表欄位定義
        /// </summary>
        [JsonIgnore]
        public FieldDefineCollection MasterFields => this.MasterTable.Fields;

        /// <summary>
        /// 尋找指定定義欄位。
        /// </summary>
        /// <param name="fieldName">[欄位名稱] 或 [資料表.欄位名稱]。</param>
        public FieldDefine FindField(string fieldName)
        {
            string sTableName;
            string sFieldName;

            if (StrFunc.StrContains(fieldName, "."))
            {
                StrFunc.StrSplitFieldName(fieldName, out sTableName, out sFieldName);
            }
            else
            {
                sTableName = this.ProgId;
                sFieldName = fieldName;
            }

            return FindField(sTableName, sFieldName);
        }

        /// <summary>
        /// 在資料表及延伸資料表尋找指定欄位定義。
        /// </summary>
        /// <param name="tableName">資料表名稱。</param>
        /// <param name="fieldName">欄位名稱。</param>
        public FieldDefine FindField(string tableName, string fieldName)
        {
            var realTableName = StrFunc.StrIsEmpty(tableName) ? this.ProgId : tableName;
            var tableDefine = this.Tables[realTableName];
            if (BaseFunc.IsNotNull(tableDefine))
                return tableDefine.Fields[fieldName];
            else
                return null;
        }
    }
}
