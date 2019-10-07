using Gat.Base;
using Gat.Cache;
using Gat.Define;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gat.Database
{
    /// <summary>
    /// 資料表關連產生器。
    /// </summary>
    public class TableJoinBuilder
    {
        #region 建構函式

        /// <summary>
        /// 建構函式。
        /// </summary>
        /// <param name="programDefine">程式定義。</param>
        /// <param name="tableName">資料表名稱。</param>
        /// <param name="selectFields">取回欄位集合字串。</param>
        /// <param name="filterItems">過濾條件項目集合。</param>
        /// <param name="sortFields">排序欄位集合。</param>
        public TableJoinBuilder(ProgramDefine programDefine, string tableName, string selectFields, FilterItemCollection filterItems, SortFieldCollection sortFields)
        {
            ProgramDefine = programDefine;
            TableName = tableName;
            SelectFields = selectFields;
            FilterItems = filterItems;
            SortFields = sortFields;
        }

        #endregion

        /// <summary>
        /// 程式定義。
        /// </summary>
        public ProgramDefine ProgramDefine { get; } = null;

        /// <summary>
        /// 資料表名稱。
        /// </summary>
        public string TableName { get; } = string.Empty;

        /// <summary>
        /// 取回欄位集合字串。
        /// </summary>
        public string SelectFields { get; } = string.Empty;

        /// <summary>
        /// 過濾條件項目集合。
        /// </summary>
        public FilterItemCollection FilterItems { get; } = null;

        /// <summary>
        /// 排序欄位集合。
        /// </summary>
        public SortFieldCollection SortFields { get; } = null;

        /// <summary>
        /// 目前作用的資料表別名。
        /// </summary>
        private string ActiveTableAlias { get; set; } = "A";

        /// <summary>
        /// 產生資料表關連集合。
        /// </summary>
        /// <param name="provider">資料表關連資訊提供者。</param>
        public void Execute(TableJoinProvider provider)
        {
            provider.TableJoins.Clear();
            provider.Mappings.Clear();

            // 取得資料表定義
            var oTableDefine = this.ProgramDefine.Tables[this.TableName];
            // 取得使用到的欄位集合
            var oUseFields = GetUseFields(oTableDefine);

            this.ActiveTableAlias = "A";
            // 取得主要資料表使用欄位集合
            var oUseTableFields = GetUseTableFields(oUseFields);
            // 建立主要資料表關連
            BuildTableJoins(provider, oTableDefine, oUseTableFields);

            // 取得關連明細資料表使用欄位集合
            oUseTableFields = GetUseDetailTableFields(oUseFields, out string detailTableName);
            if (oUseTableFields.Count > 0)
            {
                // 明細資料表別名初始值
                this.ActiveTableAlias = "DA";
                // 取得明細資料表定義
                oTableDefine = this.ProgramDefine.Tables[detailTableName];
                // 建立主檔與明細關連
                BuildDetailTableJoin(provider, oTableDefine.DbTableName);
                // 建立資料表關連
                BuildTableJoins(provider, oTableDefine, oUseTableFields, detailTableName);
            }
        }

        /// <summary>
        /// 取得使用到的欄位集合。
        /// </summary>
        /// <param name="tableDefine">資料表定義。</param>
        private StringHashSet GetUseFields(TableDefine tableDefine)
        {
            // 取得資料表定義
            var useFieldSet = new StringHashSet();
            if (StrFunc.StrIsEmpty(this.SelectFields))
            {
                // 包含所有欄位
                foreach (FieldDefine fieldDefine in tableDefine.Fields)
                    useFieldSet.Add(fieldDefine.FieldName);
            }
            else
            {
                // 加入取回欄位
                useFieldSet.Add(this.SelectFields, ",");
                // 加入篩選欄位
                foreach (FilterItem filterItem in this.FilterItems)
                    useFieldSet.Add(filterItem.FieldName);
                // 加入排序欄位
                foreach (var sortField in this.SortFields)
                    useFieldSet.Add(sortField.FieldName);
            }
            return useFieldSet;
        }

        /// <summary>
        /// 取得主要資料表使用欄位集合。
        /// </summary>
        /// <param name="useFields">使用欄位集合。</param>
        private StringHashSet GetUseTableFields(StringHashSet useFields)
        {
            var useFieldSet = new StringHashSet();
            foreach (string fieldName in useFields)
            {
                if (!StrFunc.StrContains(fieldName, "."))
                    useFieldSet.Add(fieldName);
            }
            return useFieldSet;
        }

        /// <summary>
        /// 取得關連明細資料表使用欄位集合。
        /// </summary>
        /// <param name="useFields">取得使用的欄位集合。</param>
        /// <param name="detailTableName">傳回明細資料表名稱。</param>
        private StringHashSet GetUseDetailTableFields(StringHashSet useFields, out string detailTableName)
        {
            StringHashSet oUseFields;
            string sDetailTableName;
            string sTableName;
            string sFieldName;

            sDetailTableName = string.Empty;
            oUseFields = new StringHashSet();
            foreach (string fieldName in useFields)
            {
                if (StrFunc.StrContains(fieldName, "."))
                {
                    // 折解資料表及欄位名稱
                    StrFunc.StrSplitFieldName(fieldName, out sTableName, out sFieldName);

                    // 主檔不能同時 Join 多個明細資料表
                    if (StrFunc.StrIsNotEmpty(sDetailTableName) && !StrFunc.SameText(sDetailTableName, sTableName))
                        throw new GException("Master Table can't Join multi Detail Table");

                    sDetailTableName = sTableName;
                    oUseFields.Add(sFieldName);
                }
            }

            detailTableName = sDetailTableName;
            return oUseFields;
        }

        /// <summary>
        /// 取得關連取回欄位集合。
        /// </summary>
        /// <param name="tableDefine">資料表定義。</param>
        /// <param name="linkFieldName">關連來源欄位。</param>
        /// <param name="useFields">使用到的欄位集合。</param>
        private StringHashSet GetReturnFields(TableDefine tableDefine, string linkFieldName, StringHashSet useFields)
        {
            StringHashSet oReturnFields;
            FieldDefine oFieldDefine;

            oReturnFields = new StringHashSet();
            foreach (string fieldName in useFields)
            {
                oFieldDefine = tableDefine.Fields[fieldName];
                if (oFieldDefine.FieldType == EFieldType.LinkField && StrFunc.SameText(oFieldDefine.LinkFieldName, linkFieldName))
                    oReturnFields.Add(fieldName);
            }
            return oReturnFields;
        }

        /// <summary>
        /// 取得作用的資料表別名。
        /// </summary>
        private string GetActiveTableAlias()
        {
            this.ActiveTableAlias = DatabaseFunc.GetNextTableAlias(this.ActiveTableAlias);
            return this.ActiveTableAlias;
        }

        /// <summary>
        /// 建立資料表關連。
        /// </summary>
        /// <param name="provider">資料表關連資訊提供者。</param>
        /// <param name="tableDefine">資料表定義。</param>
        /// <param name="useFields">使用到的欄位集合。</param>
        /// <param name="detailTableName">明細資料表名稱。</param>
        private void BuildTableJoins(TableJoinProvider provider, TableDefine tableDefine, StringHashSet useFields, string detailTableName = "")
        {
            foreach (FieldDefine fieldDefine in tableDefine.Fields)
            {
                if (fieldDefine.FieldType == EFieldType.DataField && StrFunc.StrIsNotEmpty(fieldDefine.LinkProgId))
                {
                    var oReturnFields = GetReturnFields(tableDefine, fieldDefine.FieldName, useFields);
                    if (oReturnFields.Count > 0)
                    {
                        var key = string.Format("{0}.{1}.{2}", tableDefine.TableName, fieldDefine.FieldName, fieldDefine.LinkProgId);
                        var leftTableAlias = StrFunc.StrIsEmpty(detailTableName) ? "A" : "DA";
                        BuildTableJoin(key, provider, fieldDefine, oReturnFields, leftTableAlias, detailTableName);
                    }
                }
            }
        }

        /// <summary>
        /// 建立資料表關連。
        /// </summary>
        /// <param name="key">關連鍵值。</param>
        /// <param name="provider">資料表關連資訊提供者。</param>
        /// <param name="fieldDefine">關連欄位定義。</param>
        /// <param name="returnFields">關連取回欄位集合。</param>
        /// <param name="leftTableAlias">左側資料表別名。</param>
        /// <param name="detailTableName">明細資料表名稱。</param>
        /// <param name="destFieldName">目的欄位名稱。</param>
        private void BuildTableJoin(string key, TableJoinProvider provider, FieldDefine fieldDefine, StringHashSet returnFields, string leftTableAlias, string detailTableName, string destFieldName = "")
        {
            // 取得關連程式定義
            var programDefine = CacheFunc.GetProgramDefine(fieldDefine.LinkProgId);
            if (BaseFunc.IsNull(programDefine))
                throw new GException("'{0}' ProgramDefine not found", fieldDefine.LinkProgId);

            // 取得關連資料表定義
            var tableDefine = programDefine.MasterTable;

            foreach (string fieldName in returnFields)
            {
                var linkReturnField = fieldDefine.LinkReturnFields.FindByDestField(fieldName);
                if (BaseFunc.IsNull(linkReturnField))
                    throw new GException("'{0}' FieldDefine's LinkReturnFields not find DestField '{1}'", fieldDefine.FieldName, fieldName);

                var sourceFieldDefine = tableDefine.Fields[linkReturnField.SourceField];
                if (BaseFunc.IsNull(sourceFieldDefine))
                    throw new GException("'{0}' TableDefine not find '{1}' FieldDefine", tableDefine.TableName, linkReturnField.SourceField);

                if (sourceFieldDefine.FieldType == EFieldType.VirtualField)
                    throw new GException("'{0}' TableDefine's '{1}' FieldDefine not allow VirtualField", tableDefine.TableName, sourceFieldDefine.FieldName);

                var tableJoin = provider.TableJoins[key];
                if (BaseFunc.IsNull(tableJoin))
                {
                    // 建立資料表關連
                    tableJoin = new TableJoin
                    {
                        Key = key,
                        LeftTableAlias = leftTableAlias,
                        LeftFieldName = tableDefine.GetLinkReturnActiveField(fieldDefine).DbFieldName,
                        RightTableName = tableDefine.DbTableName,
                        RightTableAlias = GetActiveTableAlias()
                    };
                    var sKeyField = tableDefine.Fields.Contains(SysFields.ID) ? SysFields.ID : SysFields.RowID;
                    tableJoin.RightFieldName = tableDefine.Fields[sKeyField].DbFieldName;
                    if (tableDefine.Fields.Contains(SysFields.CompanyID))
                        tableJoin.RightCompanyID = tableDefine.Fields[SysFields.CompanyID].DbFieldName;
                    else if (tableDefine.Fields.Contains(SysFields.CommonCompanyID))
                        tableJoin.RightCompanyID = tableDefine.Fields[SysFields.CommonCompanyID].DbFieldName;
                    provider.TableJoins.Add(tableJoin);
                }

                // 若來源欄位的欄位類型是 LinkField，則需往上階找關連來源
                if (sourceFieldDefine.FieldType == EFieldType.LinkField)
                {
                    var linkFieldDefine = tableDefine.GetLinkReturnActiveField(sourceFieldDefine);
                    var sKey = key + "." + linkFieldDefine.LinkProgId;
                    var returnFieldSet = new StringHashSet { sourceFieldDefine.DbFieldName };
                    BuildTableJoin(sKey, provider, linkFieldDefine, returnFieldSet, tableJoin.RightTableAlias, detailTableName, fieldName);
                }
                else
                {
                    // 記錄關連欄位對應
                    var linkFieldMapping = new LinkFieldMapping();
                    linkFieldMapping.FieldName = (StrFunc.StrIsNotEmpty(destFieldName)) ? destFieldName : fieldName;
                    if (StrFunc.StrIsNotEmpty(detailTableName))
                        linkFieldMapping.FieldName = string.Format("{0}.{1}", detailTableName, linkFieldMapping.FieldName);
                    linkFieldMapping.TableAlias = tableJoin.RightTableAlias;
                    linkFieldMapping.SourceFieldName = sourceFieldDefine.DbFieldName;
                    provider.Mappings.Add(linkFieldMapping);
                }
            }
        }


        /// <summary>
        /// 建立主檔與明細關連。
        /// </summary>
        /// <param name="provider">資料表關連資訊提供者。</param>
        /// <param name="detailTableName">明細資料表名稱。</param>
        private void BuildDetailTableJoin(TableJoinProvider provider, string detailTableName)
        {
            // 建立資料表關連
            var oTableJoin = new TableJoin
            {
                Key = "Detail",
                LeftTableAlias = "A",
                LeftFieldName = SysFields.ID,
                RightTableName = detailTableName,
                RightTableAlias = "DA",
                RightFieldName = SysFields.MasterID
            };
            provider.TableJoins.Add(oTableJoin);
        }
    }
}
