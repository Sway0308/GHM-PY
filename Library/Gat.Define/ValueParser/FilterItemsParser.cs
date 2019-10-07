using System;
using System.Data;
using Gat.Base;

namespace Gat.Define
{
    /// <summary>
    /// 過濾條件集合解析輔助類別。
    /// </summary>
    public class FilterItemsParser
    {
        private VariableParser _VariableParser = null;

        #region 建構函式

        /// <summary>
        /// 建構函式。
        /// </summary>
        /// <param name="sessionInfo">連線資訊。</param>
        /// <param name="filterItems">過濾條件集合。</param>
        public FilterItemsParser(SessionInfo sessionInfo, FilterItemCollection filterItems)
        {
            FilterItems = filterItems;
            SessionInfo = sessionInfo;
        }

        #endregion

        /// <summary>
        /// 連線資訊。
        /// </summary>
        private SessionInfo SessionInfo { get; } = null;

        /// <summary>
        /// 過濾條件集合。
        /// </summary>
        private FilterItemCollection FilterItems { get; } = null;

        /// <summary>
        /// 變數解析輔助類別。
        /// </summary>
        private VariableParser VariableParser
        {
            get
            {
                if (BaseFunc.IsNull(_VariableParser))
                    _VariableParser = new VariableParser();
                return _VariableParser;
            }
        }

        /// <summary>
        /// 取得作用資料列。
        /// </summary>
        /// <param name="dataSet">資料集。</param>
        /// <param name="tableName">資料表。</param>
        /// <param name="fieldName">欄位名稱，若不存在指定欄位，會回傳 null。</param>
        /// <param name="rowIndex">資料列索引。</param>
        private DataRow GetDataRow(DataSet dataSet, string tableName, string fieldName, int rowIndex)
        {
            DataTable oTable;

            // 資料集為空，則回傳 null
            if (BaseFunc.IsEmpty(dataSet)) { return null; }
            // 資料表不存在，則回傳 null
            if (!dataSet.Tables.Contains(tableName)) { return null; }
            // 欄位不存在，則回傳 null
            oTable = dataSet.Tables[tableName];
            if (!DataFunc.HasField(oTable, fieldName)) { return null; }
            // 主檔的資料列索引一徫為 0
            if (StrFunc.SameText(dataSet.DataSetName, tableName))
                rowIndex = 0;
            // 回傳作用資料列
            if (0 <= rowIndex && rowIndex <= oTable.Rows.Count - 1)
                return oTable.Rows[rowIndex];
            else
                return null;
        }
        
        /// <summary>
        /// 解析過濾條件中的欄位變數。
        /// </summary>
        /// <param name="dataSet">資料集。</param>
        /// <param name="tableName">作用資料表名稱。</param>
        /// <param name="rowIndex">作用資料列索引。</param>
        public FilterItemCollection ParseField(DataSet dataSet, string tableName, int rowIndex)
        {
            var oFilterItems = new FilterItemCollection();
            foreach (FilterItem item in this.FilterItems)
            {
                // 產生過濾條件複本
                var oFilterItem = item.Clone();
                // 解析變數值
                this.VariableParser.Parse(item.FilterValue);
                // 變數類型
                var oVariableType = this.VariableParser.VariableType;
                // 取得欄位變數的值
                if (oVariableType == EVariableType.Field || oVariableType == EVariableType.TableField)
                {
                    // 拆解字串的資料表名稱及欄位名稱
                    StrFunc.StrSplitFieldName(this.VariableParser.Value, out string sTableName, out string sFieldName);
                    if (StrFunc.StrIsEmpty(sTableName))
                        sTableName = tableName;
                    // 取得作用資料列
                    var oRow = GetDataRow(dataSet, sTableName, sFieldName, rowIndex);
                    if (BaseFunc.IsNotNull(oRow))
                        oFilterItem.FilterValue = BaseFunc.CStr(oRow[sFieldName]);
                    else
                        oFilterItem.FilterValue = string.Empty;
                }
                oFilterItems.Add(oFilterItem);
            }

            // 移除無查詢值的條件
            oFilterItems.RemoveEmpty();
            return oFilterItems;
        }

        /// <summary>
        /// 解析過濾條件中的系統變數
        /// </summary>
        /// <returns></returns>
        public FilterItemCollection ParseVariable()
        {
            FilterItemCollection oItems;
            FilterItem oItem;

            oItems = new FilterItemCollection();
            foreach (FilterItem item in this.FilterItems)
            {
                if (item.ComparisonOperator == EComparisonOperator.Variable)
                    oItem = ConvertItemForVariable(item);
                else
                    oItem = item.Clone();

                if (BaseFunc.IsNotNull(oItem))
                    oItems.Add(oItem);
            }

            // 移除無查詢值的條件
            oItems.RemoveEmpty();
            return oItems;
        }

        /// <summary>
        /// 解析並移除不需要的 [欄位變數] 過濾條件。
        /// </summary>
        /// <param name="programDefine">程式定義。</param>
        public FilterItemCollection ParseRemoveField(ProgramDefine programDefine)
        {
            FilterItemCollection oFilterItems;
            FilterItem oFilterItem;
            FieldDefine oFieldDefine;
            EVariableType oVariableType;
            string sTableName;
            string sFieldName;
            
            oFilterItems = new FilterItemCollection();
            foreach (FilterItem item in this.FilterItems)
            {
                // 產生過濾條件複本
                oFilterItem = item.Clone();
                // 解析變數值
                this.VariableParser.Parse(item.FilterValue);
                // 變數類型
                oVariableType = this.VariableParser.VariableType;

                if (oVariableType == EVariableType.Field || oVariableType == EVariableType.TableField)
                {
                    // 拆解字串的資料表名稱及欄位名稱
                    StrFunc.StrSplitFieldName(this.VariableParser.Value, out sTableName, out sFieldName);
                    if (StrFunc.StrIsEmpty(sTableName))
                        sTableName = programDefine.ProgId;
                    // 只保留存在的 [欄位變數] 過濾條件
                    oFieldDefine = programDefine.FindField(sTableName, sFieldName);
                    if (BaseFunc.IsNotNull(oFieldDefine))
                        oFilterItems.Add(oFilterItem);
                }
                else
                {
                    // 加入非 [欄位變數] 過濾條件
                    oFilterItems.Add(oFilterItem);
                }
            }

            // 移除無查詢值的條件
            oFilterItems.RemoveEmpty();
            return oFilterItems;
        }

        /// <summary>
        /// 解析並移除 [欄位變數] 過濾條件，GridFilter 無資料集故需移除欄位變數。
        /// </summary>
        public FilterItemCollection ParseRemoveField()
        {
            FilterItemCollection oFilterItems;

            oFilterItems = new FilterItemCollection();
            foreach (FilterItem item in this.FilterItems)
            {
                // 解析變數值
                this.VariableParser.Parse(item.FilterValue);
                // 只保留系統變數及常數類型的過濾條件
                if (this.VariableParser.VariableType == EVariableType.Variable ||
                    this.VariableParser.VariableType == EVariableType.Constant)
                {
                    oFilterItems.Add(item.Clone());
                }
            }
            return oFilterItems;
        }
       
        /// <summary>
        /// 系統變數過濾條件轉換為一般過濾條件。
        /// </summary>
        /// <param name="filterItem">系統變數過濾條件。</param>
        private FilterItem ConvertItemForVariable(FilterItem filterItem)
        {
            FilterItem oFilterItem;
            ESysVariable oVariable;
            string sValue;

            // 取得系統變數，若無法正確轉換系統變數，則傳回空字串
            try
            {
                oVariable = GetSysVariable(filterItem.FilterValue);
            }
            catch
            {
                return null;
            }

            // 取得系統變數值
            sValue = GetSysVariableValue(this.SessionInfo, oVariable);
            if (StrFunc.StrIsEmpty(sValue)) { return null; }

            switch (oVariable)
            {
                case ESysVariable.CompanyID:
                case ESysVariable.DepartmentID:
                case ESysVariable.EmployeeID:
                case ESysVariable.UserID:
                    // 轉換為等於過濾條件
                    oFilterItem = filterItem.Clone();
                    oFilterItem.ComparisonOperator = EComparisonOperator.Equal;
                    oFilterItem.FilterValue = sValue;
                    return oFilterItem;
                default:  // 日期相關變數
                    // 轉換為日期區間過濾條件
                    oFilterItem = filterItem.Clone();
                    oFilterItem.ComparisonOperator = EComparisonOperator.Between;
                    oFilterItem.FilterValue = sValue;
                    return oFilterItem;
            }
        }

        /// <summary>
        /// 系統變數字串傳為系統變數列舉。
        /// </summary>
        /// <param name="variable">系統變數字串。</param>
        private ESysVariable GetSysVariable(string variable)
        {
            string sVariable;

            // 系統變數字串格式為 [@@系統變數]，去除左右標記符號取得系統變數
            sVariable = StrFunc.StrLeftCut(variable, "[@@");
            sVariable = StrFunc.StrRightCut(sVariable, "]");
            return BaseFunc.CEnum<ESysVariable>(sVariable);
        }

        /// <summary>
        /// 取得系統變數值。
        /// </summary>
        /// <param name="sessionInfo">連線資訊。</param>
        /// <param name="variable">系統變數。</param>
        public string GetSysVariableValue(SessionInfo sessionInfo, ESysVariable variable)
        {
            switch (variable)
            {
                case ESysVariable.CompanyID:
                    // 公司編號
                    return sessionInfo.CompanyID;
                case ESysVariable.DepartmentID:
                    // 部門編號內碼
                    if (BaseFunc.IsNotNull(sessionInfo.Employee))
                        return sessionInfo.Employee.DepartmentViewID;
                    else
                        return string.Empty;
                case ESysVariable.EmployeeID:
                    // 員工編號內碼
                    if (BaseFunc.IsNotNull(sessionInfo.Employee))
                        return sessionInfo.Employee.ViewID;
                    else
                        return string.Empty;
                case ESysVariable.UserID:
                    // 用戶帳號
                    return sessionInfo.UserID;
                case ESysVariable.ThisYear:
                    return GetDateVariable(EDateUnit.Year);
                case ESysVariable.ThisMonth:
                    return GetDateVariable(EDateUnit.Month);
                case ESysVariable.ThisWeek:
                    return GetDateVariable(EDateUnit.Week);
                default:  // ESysVariable.Today:
                    return GetDateVariable(EDateUnit.Day);
            }
        }

        /// <summary>
        /// 取得日期變數值。
        /// </summary>
        /// <param name="dateTimeUnit">日期單位。</param>
        private string GetDateVariable(EDateUnit dateTimeUnit)
        {
            DateFunc.GetDateRange(DateTime.Now, dateTimeUnit, out DateTime oStartDate, out DateTime oEndTime);
            return string.Format("{0},{1}", oStartDate.ToString("yyyy/MM/dd"), oEndTime.ToString("yyyy/MM/dd"));
        }
    }
}
