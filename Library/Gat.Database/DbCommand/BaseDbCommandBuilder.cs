using Gat.Base;
using Gat.Cache;
using Gat.Define;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gat.Database
{
    /// <summary>
    /// 資料庫命令產生器基底類別。
    /// </summary>
    public class BaseDbCommandBuilder : BaseSessionObject
    {
        #region 建構函式

        /// <summary>
        /// 建構函式。
        /// </summary>
        /// <param name="programDefine">程式定義。</param>
        public BaseDbCommandBuilder(Guid sessionGuid, ProgramDefine programDefine) : base(sessionGuid)
        {
            this.ProgramDefine = programDefine;
        }

        #endregion

        /// <summary>
        /// 資料庫類型。
        /// </summary>
        public EDatabaseType DatabaseType { get; } = EDatabaseType.SQLServer;

        /// <summary>
        /// 程式定義。 
        /// </summary>
        public ProgramDefine ProgramDefine { get; }

        /// <summary>
        /// 取得指定的資料表定義。
        /// </summary>
        /// <param name="tableName">資料表名稱。</param>
        protected TableDefine GeTableDefine(string tableName)
        {
            // 無程式定義則傳回 Null
            if (BaseFunc.IsNull(this.ProgramDefine)) { return null; }
            // 無資料表定義則傳回 Null
            var tableDefine = this.ProgramDefine.Tables[tableName];
            if (BaseFunc.IsNull(tableDefine)) { return null; }

            return tableDefine;
        }

        #region BuildSelectCommand 方法

        /// <summary>
        /// 建立 Select 語法的資料庫命令。
        /// </summary>
        /// <param name="inputArgs">Select 方法的傳入引數。</param>
        public virtual DbCommand BuildSelectCommand(SelectInputArgs inputArgs)
        {
            return BuildSelectCommand(inputArgs.TableName, inputArgs.SelectFields, inputArgs.FilterItems,
                inputArgs.UserFilter, inputArgs.IsOrderBy);
        }

        /// <summary>
        /// 建立 Select 語法的資料庫命令。
        /// </summary>
        /// <param name="tableName">資料表名稱。</param>
        /// <param name="selectFields">要取得的欄位集合字串，以逗點分隔欄位名稱，空字串表示取得所有欄位。</param>
        /// <param name="filterItems">資料過濾條件項目集合。</param>
        /// <param name="userFilter">自訂過濾條件。</param>
        /// <param name="isOrderBy">執行排序</param>
        public virtual DbCommand BuildSelectCommand(string tableName, string selectFields, 
            FilterItemCollection filterItems, string userFilter, bool isOrderBy = true)
        {
            IDbCommandHelper oHelper;
            TableJoinProvider oTableJoinProvider;
            StringBuilder oBuffer;
            StringBuilder oFilterBuffer;
            IFilterBuilder oFilterBuilder;
            TableDefine oTableDefine;
            SortFieldCollection oSortFields;
            FilterInputArgs oFilterInputArgs;
            string sCommandText;
            string sFilter;
            string sSort;
            string sJoin;

            // 無資料表定義則離開
            oTableDefine = this.GeTableDefine(tableName);
            if (BaseFunc.IsNull(oTableDefine)) { return null; }

            // 建立資料庫命令輔助類別
            oHelper = DatabaseFunc.CreateDbCommandHelper(this.DatabaseType);

            // 取得排序欄位集合
            oSortFields = GetSortFields(oTableDefine);

            // 建置資料表關連資訊
            oTableJoinProvider = new TableJoinProvider();
            oTableJoinProvider.Execute(this.ProgramDefine, tableName, selectFields, filterItems, oSortFields);

            oBuffer = new StringBuilder();

            // 產生 SELECT 欄位的語法
            sCommandText = GetSelectFieldsCommandText(oHelper, oTableJoinProvider, tableName, selectFields);
            oBuffer.AppendLine(sCommandText);


            oFilterBuffer = new StringBuilder();
            // 查詢過濾條件
            oFilterInputArgs = new FilterInputArgs(oHelper, this.ProgramDefine, tableName, oTableJoinProvider, filterItems, this.SessionInfo, true);
            oFilterBuilder = new FilterBuilder(oFilterInputArgs);
            sFilter = oFilterBuilder.GetFilter(out sJoin);
            AddFilter(oFilterBuffer, sFilter);

            // 自訂過濾條件
            AddFilter(oFilterBuffer, userFilter);

            // 產生 FROM 及 JOIN 語法
            sCommandText = GeGTableJoinCommandText(oHelper, oTableJoinProvider, oTableDefine);
            oBuffer.Append(sCommandText);

            if (StrFunc.StrIsNotEmpty(sJoin))
                oBuffer.Append(sJoin);

            // 加入過濾條件
            if (oFilterBuffer.Length > 0)
            {
                oBuffer.AppendLine(" Where ");
                oBuffer.AppendLine(oFilterBuffer.ToString());
            }

            // 加入排序語法
            if (isOrderBy)
            {
                sSort = GetSortCommandText(oHelper, oTableJoinProvider, oTableDefine, oSortFields);
                if (StrFunc.StrIsNotEmpty(sSort))
                    oBuffer.Append(sSort);
            }

            oHelper.SetCommandText(oBuffer.ToString());
            return oHelper.DbCommand;
        }

        /// <summary>
        /// 加入過濾條件字串。
        /// </summary>
        /// <param name="buffer">過濾條件暫存區。</param>
        /// <param name="filter">要加入的過濾條件。</param>
        private void AddFilter(StringBuilder buffer, string filter)
        {
            if (StrFunc.StrIsNotEmpty(filter))
            {
                if (buffer.Length > 0)
                    buffer.Append(" And ");
                buffer.Append("(" + filter + ")");
            }
        }

        /// <summary>
        /// 取得 SELECT 欄位的語法。
        /// </summary>
        /// <param name="helper">資料庫命令輔助類別。</param>
        /// <param name="provider">資料表關連資訊提供者。</param>
        /// <param name="tableName">資料表名稱。</param>
        /// <param name="selectFields">選取得欄位集合字串。</param>
        /// <param name="selectCount">取得筆數。</param>
        /// <param name="selectSection">查詢區間</param>
        private string GetSelectFieldsCommandText(IDbCommandHelper helper, TableJoinProvider provider, string tableName, string selectFields)
        {
            TableDefine oTableDefine;
            FieldDefine oFieldDefine;
            StringBuilder oBuffer;
            string[] oSelectFields;
            string sCommandText;

            oTableDefine = this.ProgramDefine.Tables[tableName];

            oBuffer = new StringBuilder();
            if (StrFunc.StrIsEmpty(selectFields))
            {
                foreach (FieldDefine fieldDefine in oTableDefine.Fields)
                {
                    if (fieldDefine.FieldType != EFieldType.VirtualField)
                    {
                        if (oBuffer.Length > 0)
                            oBuffer.AppendLine(",");
                        sCommandText = provider.GetSelectField(helper, fieldDefine);
                        oBuffer.Append(sCommandText);
                    }
                }
            }
            else
            {
                oSelectFields = StrFunc.StrSplit(selectFields, ",");
                foreach (string fieldName in oSelectFields)
                {
                    oFieldDefine = this.ProgramDefine.FindField(fieldName);
                    if (oFieldDefine.FieldType != EFieldType.VirtualField)
                    {
                        if (oBuffer.Length > 0)
                            oBuffer.AppendLine(",");

                        if (StrFunc.StrContains(fieldName, "."))
                            sCommandText = provider.GetDetailSelectField(helper, oFieldDefine);
                        else
                            sCommandText = provider.GetSelectField(helper, oFieldDefine);

                        oBuffer.Append(sCommandText);
                    }
                }
            }

            return string.Format("Select \n") + oBuffer.ToString();
        }

        /// <summary>
        /// 取得資料表關連語法。
        /// </summary>
        /// <param name="helper">資料庫命令輔助類別。</param>
        /// <param name="provider">資料表關連資訊提供者。</param>
        /// <param name="tableDefine">資料表定義。</param>
        private string GeGTableJoinCommandText(IDbCommandHelper helper, TableJoinProvider provider, TableDefine tableDefine)
        {
            StringBuilder oBuffer;

            oBuffer = new StringBuilder();
            oBuffer.AppendFormat("From {0} {1} ", helper.GetTableName(tableDefine.DbTableName), "A");
            oBuffer.AppendLine();

            foreach (TableJoin item in provider.TableJoins)
            {
                oBuffer.AppendFormat("Left Join {2} {3} On {0}.{1}={3}.{4} ",
                    item.LeftTableAlias, helper.GetFieldName(item.LeftFieldName),
                    helper.GetTableName(item.RightTableName), item.RightTableAlias, helper.GetFieldName(item.RightFieldName));

                // 過濾公司編號
                if (StrFunc.StrIsNotEmpty(item.RightCompanyID))
                {
                    oBuffer.AppendFormat("And {0}.{1}='{2}' ",
                        item.RightTableAlias, helper.GetFieldName(item.RightCompanyID), this.SessionInfo.CompanyID);
                }

                oBuffer.AppendLine();
            }

            return oBuffer.ToString();
        }

        /// <summary>
        /// 取得排序欄位集合。
        /// </summary>
        private SortFieldCollection GetSortFields(TableDefine tableDefine)
        {
            SortFieldCollection oSortFields;

            // 取得排序欄位集合複本
            oSortFields = new SortFieldCollection();
            foreach (var sortField in tableDefine.SortFields)
                oSortFields.Add(sortField.Clone());

            return oSortFields;
        }

        /// <summary>
        /// 取得排序語法。
        /// </summary>
        /// <param name="helper">資料庫命令輔助類別。</param>
        /// <param name="provider">資料表關連資訊提供者。</param>
        /// <param name="tableDefine">資料表定義。</param>
        /// <param name="sortFields">排序欄位集合。</param>
        private string GetSortCommandText(IDbCommandHelper helper, TableJoinProvider provider, TableDefine tableDefine, SortFieldCollection sortFields)
        {
            StringBuilder oBuilder;
            FieldDefine oFieldDefine;
            string sDbFieldName;
            string sSortDirection;

            // 無排序欄位則傳回空字串
            if (BaseFunc.IsEmpty(sortFields) || !sortFields.Any()) { return string.Empty; }

            oBuilder = new StringBuilder();
            foreach (var sortField in sortFields)
            {
                // 取得包含資料表別名的欄位名稱
                oFieldDefine = tableDefine.Fields[sortField.FieldName];
                sDbFieldName = provider.GetDbFieldName(helper, oFieldDefine);
                // 排序方式
                sSortDirection = sortField.SortDirection == ESortDirection.Ascending ? "ASC" : "DESC";

                if (oBuilder.Length > 0)
                    oBuilder.Append(", ");
                oBuilder.Append(string.Format("{0} {1}", sDbFieldName, sSortDirection));
            }
            return " Order By " + oBuilder.ToString();
        }

        #endregion

        #region BuildInsertCommand 方法

        /// <summary>
        /// 建立 Insert 語法的資料庫命令。
        /// </summary>
        /// <param name="tableName">資料表名稱。</param>
        public virtual DbCommand BuildInsertCommand(string tableName)
        {
            IDbCommandHelper oHelper;
            StringBuilder oBuffer;
            TableDefine oTableDefine;
            string sSeparator;

            // 無資料表定義或不啟用資料庫命令，則傳回 Null
            oTableDefine = this.GeTableDefine(tableName);
            if (BaseFunc.IsNull(oTableDefine)) { return null; }

            // 建立資料庫命令輔助類別
            oHelper = DatabaseFunc.CreateDbCommandHelper(this.DatabaseType);

            oBuffer = new StringBuilder();
            oBuffer.AppendFormat("INSERT INTO {0} ", oHelper.GetTableName(oTableDefine.DbTableName));
            oBuffer.AppendLine();

            // 處理 Insert 的欄位名稱
            oBuffer.Append("(");
            sSeparator = string.Empty;
            foreach (FieldDefine fieldDefine in oTableDefine.Fields)
            {
                //FieldType 為資料欄位(DataField)才做欄位處理
                if (fieldDefine.FieldType == EFieldType.DataField)
                {
                    oBuffer.Append(sSeparator + oHelper.GetFieldName(fieldDefine.DbFieldName));
                    sSeparator = ",";
                }
            }
            oBuffer.AppendLine(")");

            // 處理 Insert 的欄位值
            oBuffer.AppendLine("VALUES");
            oBuffer.Append("(");
            sSeparator = string.Empty;
            foreach (FieldDefine fieldDefine in oTableDefine.Fields)
            {
                //FieldType 為資料欄位(DataField)才做欄位處理
                if (fieldDefine.FieldType == EFieldType.DataField)
                {
                    oBuffer.Append(sSeparator + oHelper.GetParameterName(fieldDefine.FieldName));
                    sSeparator = ",";
                    oHelper.AddParameter(fieldDefine); // 加入命令參數
                }
            }
            oBuffer.AppendLine(")");

            oHelper.SetCommandText(oBuffer.ToString());
            return oHelper.DbCommand; ;
        }

        #endregion

        #region BuildUpdateCommand 方法

        /// <summary>
        /// 建立 Update 語法的資料庫命令。
        /// </summary>
        /// <param name="tableName">資料表名稱。</param>
        public virtual DbCommand BuildUpdateCommand(string tableName)
        {
            IDbCommandHelper oHelper;
            StringBuilder oBuffer;
            TableDefine oTableDefine;
            FieldDefine oKeyFieldDefine;
            DbParameter oParameter;
            string sSeparator, sFormat;
            string[] oKeyFields;

            // 無資料表定義或不啟用資料庫命令，則傳回 Null
            oTableDefine = this.GeTableDefine(tableName);
            if (BaseFunc.IsNull(oTableDefine)) { return null; }

            // 建立資料庫命令輔助類別
            oHelper = DatabaseFunc.CreateDbCommandHelper(this.DatabaseType);

            oBuffer = new StringBuilder();
            oBuffer.AppendFormat("UPDATE {0} WITH ( ROWLOCK ) ", oHelper.GetTableName(oTableDefine.DbTableName));
            oBuffer.AppendLine();

            // 處理 Update 的欄位名稱與值
            sSeparator = string.Empty;
            oBuffer.AppendLine("SET");

            foreach (FieldDefine fieldDefine in oTableDefine.Fields)
            {
                // FieldType 為 DataField 才需做更新，但排除 SYS_CompanyID、SYS_ID、SYS_RowID、SYS_INSDAT、SYS_USR
                if (fieldDefine.FieldType == EFieldType.DataField && !StrFunc.SameTextOr(fieldDefine.FieldName, SysFields.CompanyID, SysFields.ID, SysFields.RowID, SysFields.InsertDate, SysFields.InsertUser))
                {
                    // 加入命令參數
                    oParameter = oHelper.AddParameter(fieldDefine);
                    sFormat = "{0}{1}={2}";
                    oBuffer.AppendFormat(sFormat, sSeparator, oHelper.GetFieldName(fieldDefine.DbFieldName), oParameter.ParameterName);
                    oBuffer.AppendLine();
                    sSeparator = ", ";
                }
            }

            // 更新主鍵優先順序為 SYS_RowID、SYS_CompanyID+SYS_ID，CompanyID+SYS_ID、PrimaryKey 
            if (oTableDefine.Fields.Contains(SysFields.RowID))
                oKeyFields = new string[] { SysFields.RowID };
            else if (oTableDefine.Fields.Contains(SysFields.CompanyID) && oTableDefine.Fields.Contains(SysFields.ID))
                oKeyFields = new string[] { SysFields.CompanyID, SysFields.ID };
            else if (oTableDefine.Fields.Contains(SysFields.CommonCompanyID) && oTableDefine.Fields.Contains(SysFields.ID))
                oKeyFields = new string[] { SysFields.CommonCompanyID, SysFields.ID };
            else if (StrFunc.StrIsNotEmpty(oTableDefine.PrimaryKey))
                oKeyFields = StrFunc.StrSplit(oTableDefine.PrimaryKey, ",");
            else
                throw new GException("{0} 未包含 Sys_ID 或 Sys_RowID 欄位，無法建立 Update Command", tableName);

            // 產生更新的 Where 條件
            oBuffer.AppendLine("WHERE 1=1");
            foreach (string sFieldName in oKeyFields)
            {
                oKeyFieldDefine = oTableDefine.Fields[sFieldName];
                oParameter = oHelper.AddOriginalParameter(oKeyFieldDefine);
                sFormat = "AND {0}={1}";
                oBuffer.AppendLine(string.Format(sFormat, oHelper.GetFieldName(oKeyFieldDefine.DbFieldName), oParameter.ParameterName));
            }

            oHelper.SetCommandText(oBuffer.ToString());
            return oHelper.DbCommand; ;
        }

        #endregion

        #region BuildDeleteCommand 方法

        /// <summary>
        /// 建立 Delete 語法的資料庫命令。
        /// </summary>
        /// <param name="tableName">資料表名稱。</param>
        public virtual DbCommand BuildDeleteCommand(string tableName)
        {
            IDbCommandHelper oHelper;
            StringBuilder oBuffer;
            TableDefine oTableDefine;
            FieldDefine oKeyFieldDefine;
            string sKeyField, sParameterSymbol;
            string[] oKeyFields;

            // 無資料表定義或不啟用資料庫命令，則傳回 Null
            oTableDefine = this.GeTableDefine(tableName);
            if (BaseFunc.IsNull(oTableDefine)) { return null; }

            // 建立資料庫命令輔助類別
            oHelper = DatabaseFunc.CreateDbCommandHelper(this.DatabaseType);
            sParameterSymbol = oHelper.ParameterSymbol;

            oBuffer = new StringBuilder();
            oBuffer.AppendFormat("DELETE FROM {0} WITH ( ROWLOCK ) ", oTableDefine.DbTableName);
            oBuffer.AppendLine("WHERE 1=1");

            if (StrFunc.StrIsEmpty(oTableDefine.PrimaryKey))
            {
                // 主檔以 Sys_ID 為更新主鍵，明細檔以 Sys_RowID 為更新主鍵，其他則以 PrimaryKey 為更新主鍵
                if (oTableDefine.Fields.Contains(SysFields.ID))
                    sKeyField = SysFields.ID;
                else if (oTableDefine.Fields.Contains(SysFields.RowID))
                    sKeyField = SysFields.RowID;
                else
                    throw new GException("{0} TableDefine 未包含 Sys_ID 或 Sys_RowID 欄位", tableName);

                oKeyFieldDefine = oTableDefine.Fields[sKeyField];
                oBuffer.AppendLine(string.Format("AND {1}={0}{2}", sParameterSymbol, oKeyFieldDefine.DbFieldName, oKeyFieldDefine.FieldName));
                oHelper.AddParameter(oKeyFieldDefine);
            }
            else
            {
                // 以自訂的 PrimaryKey 為準
                oKeyFields = StrFunc.StrSplit(oTableDefine.PrimaryKey, ",");
                foreach (string sFieldName in oKeyFields)
                {
                    oKeyFieldDefine = oTableDefine.Fields[sFieldName];
                    oBuffer.AppendLine(string.Format("AND {1}={0}{2}", sParameterSymbol, oKeyFieldDefine.DbFieldName, oKeyFieldDefine.FieldName));
                    oHelper.AddParameter(oKeyFieldDefine);
                }
            }

            oHelper.SetCommandText(oBuffer.ToString());
            return oHelper.DbCommand; ;
        }

        /// <summary>
        /// 建立符合過濾條件的 Delete 語法的資料庫命令。
        /// </summary>
        /// <param name="tableName">資料表名稱。</param>
        /// <param name="filterItems">資料過濾條件項目集合. </param>
        public virtual DbCommand BuildDeleteCommand(string tableName, FilterItemCollection filterItems)
        {
            IDbCommandHelper oHelper;
            StringBuilder oBuffer;
            TableDefine oTableDefine;
            FilterBuilder oFilterBuilder;
            FilterInputArgs oFilterInputArgs;
            string sFilter;
            string sJoin;

            // 無資料表定義或不啟用資料庫命令，則傳回 Null
            oTableDefine = this.GeTableDefine(tableName);
            if (BaseFunc.IsNull(oTableDefine)) { return null; }

            // 建立資料庫命令輔助類別
            oHelper = DatabaseFunc.CreateDbCommandHelper(this.DatabaseType);

            oBuffer = new StringBuilder();

            if (this.SessionInfo.DatabaseType == EDatabaseType.Oracle)
                oBuffer.AppendLine(string.Format("DELETE FROM {0} A ", oTableDefine.DbTableName));
            else
                oBuffer.AppendLine(string.Format("DELETE FROM {0} FROM {0} A WITH ( ROWLOCK ) ", oTableDefine.DbTableName));

            // 處理過濾條件
            if (!BaseFunc.IsEmpty(filterItems))
            {
                oFilterInputArgs = new FilterInputArgs(oHelper, this.ProgramDefine, tableName, null, filterItems, this.SessionInfo, true);
                oFilterBuilder = new FilterBuilder(oFilterInputArgs);
                sFilter = oFilterBuilder.GetFilter(out sJoin);

                if (StrFunc.StrIsNotEmpty(sFilter))
                    oBuffer.AppendLine("Where " + sFilter);
            }

            oHelper.SetCommandText(oBuffer.ToString());
            return oHelper.DbCommand;
        }

        #endregion
    }
}
