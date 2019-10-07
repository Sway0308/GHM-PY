using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Gat.Base
{
    /// <summary>
    /// 處理 DataSet、DataTable、DataView 等資料的函式庫。
    /// </summary>
    public class DataFunc
    {
        /// <summary>
        /// 建立資料集。
        /// </summary>
        /// <param name="datasetName">資料集名稱。</param>
        public static DataSet CreateDataSet(string datasetName)
        {
            DataSet oDataSet;

            oDataSet = new DataSet(datasetName);
            oDataSet.RemotingFormat = SerializationFormat.Binary;
            return oDataSet;
        }

        /// <summary>
        /// 建立資料集。
        /// </summary>
        public static DataSet CreateDataSet()
        {
            return CreateDataSet("DataSet");
        }

        /// <summary>
        /// 建立資料表。
        /// </summary>
        /// <param name="tableName">資料表名稱。</param>
        public static DataTable CreateDataTable(string tableName)
        {
            DataTable oTable;

            oTable = new DataTable(tableName);
            oTable.RemotingFormat = SerializationFormat.Binary;
            return oTable;
        }

        /// <summary>
        /// 建立資料表。
        /// </summary>
        public static DataTable CreateDataTable()
        {
            return CreateDataTable("DataTable");
        }

        /// <summary>
        /// 判斷資料集是否無資料，為 Null 或資料表數為零，皆視為無資料。
        /// </summary>
        /// <param name="dataSet">要判斷的資料表。</param>
        public static bool IsEmpty(DataSet dataSet)
        {
            //資料集為 Null 或無資料表，皆視為無資料
            if (BaseFunc.IsNull(dataSet) || (dataSet.Tables.Count == 0)) { return true; }

            // 主檔資料表無資料時，也視為無資料
            if (StrFunc.StrIsNotEmpty(dataSet.DataSetName) && dataSet.Tables.Contains(dataSet.DataSetName))
            {
                if (IsEmpty(dataSet.Tables[dataSet.DataSetName])) { return true; }
            }

            return false;
        }

        /// <summary>
        /// 判斷資料表是否無資料，為 Null 或資料列數為零，皆視為無資料。
        /// </summary>
        /// <param name="dataTable">要判斷的資料表。</param>
        /// <param name="isDefaultViewCount">是否以 DefaultView 做為資料筆數的判斷基準。</param>
        public static bool IsEmpty(DataTable dataTable, bool isDefaultViewCount = true)
        {
            if (BaseFunc.IsNull(dataTable)) { return true; }

            // 判斷資料筆數是否為零
            if (isDefaultViewCount)
                return dataTable.DefaultView.Count == 0;
            else
                return dataTable.Rows.Count == 0;
        }

        /// <summary>
        /// 判斷檢視表是否無資料，Null 或資料列數為零，皆視為無資料。
        /// </summary>
        /// <param name="dataView">要判斷的檢視表。</param>
        public static bool IsEmpty(DataView dataView)
        {
            //檢視表為 Null 或資料列數為零，皆視為無資料
            return (BaseFunc.IsNull(dataView) || (dataView.Count == 0));
        }

        /// <summary>
        /// 由來源資料列異動目前資料列，目的資料列存在相同欄位時才會進行異動。
        /// </summary>
        /// <param name="sourceRow">來源資料列。</param>
        /// <param name="destRow">目的資料列。</param>
        /// <param name="allowNull">是否允許Null值</param>
        public static void UpdateRow(DataRow sourceRow, DataRow destRow, bool allowNull = true)
        {
            DataTable oSoruceTable;
            DataTable oDestTable;

            if (BaseFunc.IsEmpty(sourceRow)) return;
            if (BaseFunc.IsEmpty(destRow)) return;

            oSoruceTable = sourceRow.Table;
            oDestTable = destRow.Table;

            foreach (DataColumn oColumn in oSoruceTable.Columns)
            {
                if (oDestTable.Columns.Contains(oColumn.ColumnName) && 
                    (allowNull || !BaseFunc.IsNullOrDBNull(sourceRow[oColumn.ColumnName])))
                {
                    destRow[oColumn.ColumnName] = sourceRow[oColumn.ColumnName];
                }
            }
        }

        /// <summary>
        /// 由來源資料列異動目前資料列，目的資料列存在相同欄位，且不在排除欄位中時才會進行異動。
        /// </summary>
        /// <param name="sourceRow">來源資料列。</param>
        /// <param name="destRow">目的資料列。</param>
        /// <param name="excludeFields">排除欄位</param>
        public static void UpdateRow(DataRow sourceRow, DataRow destRow, params string[] excludeFields)
        {
            DataTable oSoruceTable;
            DataTable oDestTable;

            if (BaseFunc.IsEmpty(sourceRow)) return;
            if (BaseFunc.IsEmpty(destRow)) return;

            oSoruceTable = sourceRow.Table;
            oDestTable = destRow.Table;

            foreach (DataColumn oColumn in oSoruceTable.Columns)
            {
                //確認該欄位是否存在於排除欄位中
                var matchExcludeFields = from excludeField in excludeFields
                                         where StrFunc.SameText(excludeField, oColumn.ColumnName)
                                         select excludeField;

                if (matchExcludeFields.ToList().Count == 0 && oDestTable.Columns.Contains(oColumn.ColumnName)
                    && !BaseFunc.IsNullOrDBNull(sourceRow[oColumn.ColumnName]))
                {
                    destRow[oColumn.ColumnName] = sourceRow[oColumn.ColumnName];
                }
            }
        }

        /// <summary>
        /// 由來源資料列異動目前資料列，更新指定欄位值
        /// </summary>
        /// <param name="sourceRow">來源資料列。</param>
        /// <param name="destRow">目的資料列。</param>
        /// <param name="includeFields">指定欄位</param>
        public static void UpdateDesignateRow(DataRow sourceRow, DataRow destRow, params string[] includeFields)
        {
            if (BaseFunc.IsEmpty(sourceRow)) return;
            if (BaseFunc.IsEmpty(destRow)) return;

            foreach (var s in includeFields)
            {
                if (HasField(sourceRow, s) && HasField(destRow, s))
                    destRow[s] = sourceRow[s];
            }
        }

        /// <summary>
        /// 對目的資料表新增資料列，有相同欄位時才會塞值。
        /// </summary>
        /// <param name="destDataTable">目的資料表。</param>
        /// <param name="sourceRow">來源資料列。</param>
        /// <param name="allowNull">是否允許Null值</param>
        public static void AddRow(DataTable destDataTable, DataRow sourceRow, bool allowNull = true)
        {
            DataRow oNewRow;
            string sColumnName;

            if (BaseFunc.IsNull(destDataTable)) return;
            if (BaseFunc.IsEmpty(sourceRow)) return;

            oNewRow = destDataTable.NewRow();
            foreach (DataColumn column in destDataTable.Columns)
            {
                sColumnName = column.ColumnName;
                if (sourceRow.Table.Columns.Contains(sColumnName))
                {
                    var value = sourceRow[sColumnName];
                    if (allowNull || !BaseFunc.IsNullOrDBNull(value))
                        oNewRow[sColumnName] = value;
                }
            }
            destDataTable.Rows.Add(oNewRow);
        }

        /// <summary>
        /// 對目的資料表新增來源資料表中的資料列，有相同欄位時才會塞值。
        /// </summary>
        /// <param name="destDataTable">目的資料表。</param>
        /// <param name="sourceTable">來源資料表。</param>
        /// <param name="allowNull">是否允許Null值</param>
        public static void AddTableRows(DataTable destDataTable, DataTable sourceTable, bool allowNull = true)
        {
            if (BaseFunc.IsNull(destDataTable)) return;
            if (DataFunc.IsEmpty(sourceTable)) return;

            foreach (DataRowView sourceRow in sourceTable.DefaultView)
                AddRow(destDataTable, sourceRow.Row, allowNull);
        }

        /// <summary>
        /// 判斷資料列指定欄位值是否有異動。
        /// </summary>
        /// <param name="row">資料列。</param>
        /// <param name="fieldName">欄位名稱。</param>
        public static bool FieldValueIsChanged(DataRow row, string fieldName)
        {
            switch (row.RowState)
            {
                case DataRowState.Added:
                    return true;
                case DataRowState.Modified:
                    return !row[fieldName, DataRowVersion.Original].Equals(row[fieldName, DataRowVersion.Current]);
                case DataRowState.Unchanged:
                    return false;
                default:
                    throw new GException("Not Supported DataRowState '{0}'", row.RowState);
            }
        }

        /// <summary>
        /// 判斷資料表是否有異動。
        /// </summary>
        /// <param name="dataTable">資料表。</param>
        public static bool DataTableIsChanged(DataTable dataTable)
        {
            if (BaseFunc.IsNull(DataTableGetChanges(dataTable)))
                return false;
            else
                return true;
        }

        /// <summary>
        /// 取得資料表的異動資料。
        /// </summary>
        /// <param name="dataTable">資料表</param>
        public static DataTable DataTableGetChanges(DataTable dataTable)
        {
            if (BaseFunc.IsNull(dataTable))
                return null;
            else
                return dataTable.GetChanges();
        }

        /// <summary>
        /// 取得不重覆資料。
        /// </summary>
        /// <param name="dataTable">資料表。</param>
        /// <param name="fieldNames">判斷重覆的欄位名稱集合字串，以逗點分隔欄位名稱。</param>
        public static DataTable Distinct(DataTable dataTable, string fieldNames)
        {
            string[] oFieldNames;

            oFieldNames = StrFunc.StrSplit(fieldNames, ",");
            return dataTable.DefaultView.ToTable(true, oFieldNames);
        }

        /// <summary>
        /// 判斷二筆資料的欄位值是否相同。
        /// </summary>
        /// <param name="value1">第一個欄位值。</param>
        /// <param name="value2">第二個欄位值。</param>
        /// <param name="ignoreCase">忽略大小寫。</param>
        public static bool FieldValueIsEquals(object[] value1, object[] value2, bool ignoreCase)
        {
            object oValue1;
            object oValue2;

            for (int N1 = 0; N1 < value1.Length; N1++)
            {
                oValue1 = value1[N1];
                oValue2 = value2[N1];

                if (ignoreCase && (oValue1 is string) && (oValue2 is string))
                {
                    if (!StrFunc.SameText(oValue1, oValue2))
                        return false;
                }
                else
                {
                    if (!FieldValueIsEquals(oValue1, oValue2))
                        return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 判斷二個欄位值是否相同。
        /// </summary>
        /// <param name="value1">第一個欄位值。</param>
        /// <param name="value2">第二個欄位值。</param>
        public static bool FieldValueIsEquals(object value1, object value2)
        {
            if (BaseFunc.IsNotNull(value1) && BaseFunc.IsNotNull(value1))
            {
                if (!BaseFunc.IsDBNull(value1) && !BaseFunc.IsDBNull(value2))
                    return value1.Equals(value2);
                else if (BaseFunc.IsDBNull(value1) && BaseFunc.IsDBNull(value2))
                    return true;
                else
                    return false;
            }
            return false;
        }

        /// <summary>
        /// 依欄位資料型別取得預設值(字串預設為空字串、數值預設為 0，布林值預設為 false，其他為 DBNull.Value)。
        /// </summary>
        /// <param name="dbType">欄位資料型別。</param>
        public static object GetDefaultValue(EFieldDbType dbType)
        {
            switch (dbType)
            {
                case EFieldDbType.String:
                case EFieldDbType.Text:
                    return string.Empty;
                case EFieldDbType.Boolean:
                    return false;
                case EFieldDbType.Integer:
                case EFieldDbType.Double:
                case EFieldDbType.Currency:
                    return 0;
                case EFieldDbType.DateTime:
                    return DateTime.Today;
                case EFieldDbType.GUID:
                    return Guid.Empty;
                default:
                    return DBNull.Value;
            }
        }

        /// <summary>
        /// 依欄位資料型別設定欄位值
        /// </summary>
        /// <param name="dbType"></param>
        /// <param name="fieldValue"></param>
        /// <returns></returns>
        public static object SetFieldValue(EFieldDbType dbType, object fieldValue)
        {
            object result = null;

            switch (dbType)
            {
                case EFieldDbType.String:
                case EFieldDbType.Text:
                    result = BaseFunc.CStr(fieldValue);
                    break;
                case EFieldDbType.Boolean:
                    result = BaseFunc.CBool(fieldValue);
                    break;
                case EFieldDbType.Integer:
                    result = BaseFunc.CInt(fieldValue);
                    break;
                case EFieldDbType.Double:
                    result = BaseFunc.CDouble(fieldValue);
                    break;
                case EFieldDbType.Currency:
                    result = BaseFunc.CDecimal(fieldValue);
                    break;
                case EFieldDbType.DateTime:
                    result = BaseFunc.CDateTime(fieldValue);
                    break;
                case EFieldDbType.GUID:
                    result = Guid.Parse(fieldValue.ToString());
                    break;
                default:
                    result = fieldValue;
                    break;
            }

            return result;
        }

        /// <summary>
        /// 依資料型別取得預設值(字串預設為空字串、數值預設為 0，布林值預設為 false，其他為 DBNull.Value)。
        /// </summary>
        /// <param name="dataType">資料型別。</param>
        /// <param name="dateTimeDefault">DateTime 型別的預設值。</param>
        public static object GetDefaultValue(Type dataType, EDateTimeDefault dateTimeDefault = EDateTimeDefault.Today)
        {
            switch (DbTypeConverter.ToTypeCode(dataType))
            {
                case TypeCode.Boolean:
                    return false;
                case TypeCode.Char:
                case TypeCode.String:
                    return string.Empty;
                case TypeCode.SByte:
                case TypeCode.Byte:
                case TypeCode.Int16:
                case TypeCode.UInt16:
                case TypeCode.Int32:
                case TypeCode.UInt32:
                case TypeCode.Int64:
                case TypeCode.UInt64:
                case TypeCode.Single:
                case TypeCode.Double:
                case TypeCode.Decimal:
                    return 0;
                case TypeCode.DateTime:
                    switch (dateTimeDefault)
                    {
                        case EDateTimeDefault.Today:
                            return DateTime.Today;
                        case EDateTimeDefault.Now:
                            return DateTime.Now;
                        default: // EDateTimeDefault.MinValue:
                            return DateTime.MinValue;
                    }
            }
            return DBNull.Value;
        }

        /// <summary>
        /// 取得無 DbNull 的欄位值。
        /// </summary>
        /// <param name="row">資料列。</param>
        /// <param name="fieldName">欄位值。</param>
        public static object GetFieldValueNotDbNull(DataRow row, string fieldName)
        {
            object oFieldValue;

            oFieldValue = row[fieldName];
            if (BaseFunc.IsNullOrDBNull(oFieldValue))
                return GetDefaultValue(row.Table.Columns[fieldName].DataType, EDateTimeDefault.MinValue);
            else
                return oFieldValue;
        }

        /// <summary>
        /// 取得無 DbNull 的欄位值。
        /// </summary>
        /// <param name="row">資料列。</param>
        /// <param name="fieldName">欄位值。</param>
        public static object GetFieldValueNotDbNull(DataRowView row, string fieldName)
        {
            return GetFieldValueNotDbNull(row.Row, fieldName);
        }

        /// <summary>
        /// 取得欄位值，若不存在該欄位或為 DbNull 值，則傳回預設值。
        /// </summary>
        /// <param name="row">資料列。</param>
        /// <param name="fieldName">欄位名稱。</param>
        /// <param name="defaultValue">預設值。</param>
        public static object GetFieldValue(DataRow row, string fieldName, object defaultValue)
        {
            object oFieldValue;

            // 若資料列為 Null 則傳回預設值
            if (BaseFunc.IsNull(row)) { return defaultValue; }
            // 若不存在該欄位，則傳回預設值
            if (!HasField(row, fieldName)) { return defaultValue; }

            oFieldValue = row[fieldName];

            if (BaseFunc.IsNullOrDBNull(oFieldValue))
                return defaultValue;  // 若欄位值為 DbNull 值，則傳回預設值
            else
                return oFieldValue;
        }

        /// <summary>
        /// 取得無 DbNull 的欄位值，組成以逗號分隔的字串
        /// </summary>
        /// <param name="table">資料表</param>
        /// <param name="fieldName">欄位名稱</param>
        /// <param name="checkEmpty">檢查空白</param>
        /// <param name="isSqlStr">是否用單引號包起來</param>
        /// <param name="distinct">是否過濾同樣資料</param>
        /// <returns></returns>
        public static string GetFieldValues(DataTable table, string fieldName, bool checkEmpty = true, bool isSqlStr = true, bool distinct = false)
        {
            string result = string.Empty;
            if (!IsEmpty(table))
            {
                if (HasField(table, fieldName))
                {
                    foreach (DataRow row in table.Rows)
                    {
                        if (row.RowState != DataRowState.Deleted)
                        {
                            string value = BaseFunc.CStr(GetFieldValueNotDbNull(row, fieldName));
                            if (checkEmpty && StrFunc.StrIsEmpty(value))
                                continue;
                            else
                            {
                                value = isSqlStr ? StrFunc.SQLStr(value) : value;
                                if (distinct && StrFunc.StrContains(result, value))
                                    continue;
                                result += (StrFunc.StrIsNotEmpty(result) ? "," : "") + value;
                            }
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 取得無 DbNull 的欄位值，組成以逗號分隔的字串
        /// </summary>
        /// <param name="rows">資料列陣列</param>
        /// <param name="fieldName">欄位名稱</param>
        /// <param name="checkEmpty">檢查空白</param>
        /// <param name="isSqlStr">是否用單引號包起來</param>
        /// <param name="distinct">是否過濾同樣資料</param>
        /// <returns></returns>
        public static string GetFieldValues(IEnumerable<DataRow> rows, string fieldName, bool checkEmpty = true, bool isSqlStr = true, bool distinct = false)
        {
            string result = string.Empty;
            if (rows.Count() != 0)
            {
                if (HasField(rows.First(), fieldName))
                {
                    foreach (DataRow row in rows)
                    {
                        if (row.RowState != DataRowState.Deleted)
                        {
                            string value = BaseFunc.CStr(GetFieldValueNotDbNull(row, fieldName));
                            if (checkEmpty && StrFunc.StrIsEmpty(value))
                                continue;
                            else
                            {
                                value = isSqlStr ? StrFunc.SQLStr(value) : value;
                                if (distinct && StrFunc.StrContains(result, value))
                                    continue;
                                result += (StrFunc.StrIsNotEmpty(result) ? "," : "") + value;
                            }
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 取得無 DbNull 的欄位值，組成以指定分隔符號分隔的字串
        /// </summary>
        /// <param name="rows">資料列陣列</param>
        /// <param name="fieldName">欄位名稱</param>
        /// <param name="delimiter">分隔符號</param>
        /// <param name="checkEmpty">檢查空白</param>
        /// <returns></returns>
        public static string GetFieldValues(IEnumerable<DataRow> rows, string fieldName, string delimiter, bool checkEmpty = true)
        {
            var result = string.Empty;
            if (rows.Count() == 0 || !HasField(rows.First(), fieldName))
                return result;
                
            foreach (var row in rows)
            {
                if (row.RowState != DataRowState.Deleted)
                {
                    var value = BaseFunc.CStr(GetFieldValueNotDbNull(row, fieldName));
                    if (checkEmpty && StrFunc.StrIsEmpty(value))
                        continue;

                    result = result.StrMerge(value, delimiter);
                }
            }                
            
            return result;
        }

        /// <summary>
        /// 資料集加入指定資料表，若資料集中已存在相同 TableName 的資料表，會先移除舊資料表後再加入新資料表。
        /// </summary>
        /// <param name="dataSet">資料集。</param>
        /// <param name="dataTable">欲加入資料表的資料表。</param>
        public static void DataSetAddTable(DataSet dataSet, DataTable dataTable)
        {
            if (dataSet.Tables.Contains(dataTable.TableName))
                dataSet.Tables.Remove(dataTable.TableName);

            if (dataTable.DataSet != null)
                dataTable.DataSet.Tables.Remove(dataTable);

            dataSet.Tables.Add(dataTable);
        }

        /// <summary>
        /// 清除資料表的錯誤訊息。
        /// </summary>
        /// <param name="dataTable">資料表。</param>
        public static void DataTableClearErrors(DataTable dataTable)
        {
            if (dataTable.HasErrors)
            {
                foreach (DataRow oRow in dataTable.GetErrors())
                {
                    oRow.ClearErrors();
                }
            }
        }

        /// <summary>
        /// 設定資料表的主索引鍵。
        /// </summary>
        /// <param name="dataTable">資料表。</param>
        /// <param name="fieldNames">主索引鍵的欄位集合字串(以逗點分隔多個欄位名稱)。</param>
        public static void DataTableSetPrimaryKey(DataTable dataTable, string fieldNames)
        {
            string[] oFieldNames;
            DataColumn[] oDataColumns;
            int iIndex = 0;

            oFieldNames = StrFunc.StrSplit(fieldNames);
            oDataColumns = new DataColumn[oFieldNames.Length];
            foreach (string sFieldName in oFieldNames)
            {
                oDataColumns[iIndex] = dataTable.Columns[sFieldName];
                iIndex++;
            }
            dataTable.PrimaryKey = oDataColumns;
        }

        /// <summary>
        /// 刪除檢視表中所有的資料列。
        /// </summary>
        /// <param name="dataView">檢視表。</param>
        /// <param name="isAcceptChanges">是否同意變更。</param>
        public static void DeleteRows(DataView dataView, bool isAcceptChanges)
        {
            for (int N1 = dataView.Count - 1; N1 >= 0; N1 += -1)
                dataView.Delete(N1);

            if (isAcceptChanges)
                dataView.Table.AcceptChanges();
        }

        /// <summary>
        /// 刪除檢視表中所有的資料列。
        /// </summary>
        /// <param name="dataView">檢視表。</param>
        public static void DeleteRows(DataView dataView)
        {
            DeleteRows(dataView, false);
        }

        /// <summary>
        /// 刪除資料表中所有的資料列。
        /// </summary>
        /// <param name="dataTable">資料表。</param>
        /// <param name="isAcceptChanges">是否同意變更。</param>
        public static void DeleteRows(DataTable dataTable, bool isAcceptChanges)
        {
            DeleteRows(dataTable.DefaultView, isAcceptChanges);
        }

        /// <summary>
        /// 刪除資料表中所有的資料列。
        /// </summary>
        /// <param name="dataTable">資料表。</param>
        public static void DeleteRows(DataTable dataTable)
        {
            DeleteRows(dataTable.DefaultView);
        }

        /// <summary>
        /// 判斷資料列是否為新增或修改狀態。
        /// </summary>
        /// <param name="row">資料列。</param>
        public static bool DataRowIsAddedOrModified(DataRow row)
        {
            if (row.RowState == DataRowState.Added || row.RowState == DataRowState.Modified)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 設定資料表所有資料列為新增狀態。
        /// </summary>
        /// <param name="dataTable">資料表。</param>
        public static void DataRowsSetAdded(DataTable dataTable)
        {
            dataTable.AcceptChanges();
            foreach (DataRow oRow in dataTable.Rows)
            {
                oRow.SetAdded();
            }
        }

        /// <summary>
        /// 設定資料集中所有資料表的資料列皆為新增狀態。
        /// </summary>
        /// <param name="dataSet">資料集。</param>
        public static void DataRowsSetAdded(DataSet dataSet)
        {
            foreach (DataTable oTable in dataSet.Tables)
            {
                DataRowsSetAdded(oTable);
            }
        }

        /// <summary>
        /// 設定資料表所有資料列為更新狀態。
        /// </summary>
        /// <param name="dataTable">資料表。</param>
        public static void DataRowsSetModified(DataTable dataTable)
        {
            dataTable.AcceptChanges();
            foreach (DataRow oRow in dataTable.Rows)
            {
                oRow.SetModified();
            }
        }

        /// <summary>
        /// 設定資料集中所有資料表的資料列皆為更新狀態。
        /// </summary>
        /// <param name="dataSet">資料集。</param>
        public static void DataRowsSetModified(DataSet dataSet)
        {
            foreach (DataTable oTable in dataSet.Tables)
            {
                DataRowsSetModified(oTable);
            }
        }

        /// <summary>
        /// 驗證列索引是否在合理值範圍。
        /// </summary>
        /// <param name="dataView">檢視資料表。</param>
        /// <param name="rowIndex">資料列索引。</param>
        public static bool ValidateRowIndex(DataView dataView, int rowIndex)
        {
            if ((rowIndex >= 0) && (rowIndex < dataView.Count))
                return true;
            else
                return false;
        }

        /// <summary>
        /// 驗證列索引是否在合理值範圍。
        /// </summary>
        /// <param name="dataTable">資料表。</param>
        /// <param name="rowIndex">資料列索引。</param>
        public static bool ValidateRowIndex(DataTable dataTable, int rowIndex)
        {
            if ((rowIndex >= 0) && (rowIndex < dataTable.Rows.Count))
                return true;
            else
                return false;
        }

        /// <summary>
        /// 取得指定資料列。
        /// </summary>
        /// <param name="dataView">檢視資料表。</param>
        /// <param name="rowIndex">資料列索引。</param>
        public static DataRowView GetRow(DataView dataView, int rowIndex)
        {
            if (ValidateRowIndex(dataView, rowIndex))
                return dataView[rowIndex];
            else
                return null;
        }

        /// <summary>
        /// 取得指定資料列。
        /// </summary>
        /// <param name="dataTable">資料表。</param>
        /// <param name="rowIndex">資料列索引。</param>
        public static DataRow GetRow(DataTable dataTable, int rowIndex)
        {
            if (ValidateRowIndex(dataTable, rowIndex))
                return dataTable.Rows[rowIndex];
            else
                return null;
        }

        /// <summary>
        /// 取得主檔資料列。
        /// </summary>
        /// <param name="dataSet">資料集。</param>
        public static DataRow GetMasterRow(DataSet dataSet)
        {
            DataTable oTable;

            if (DataFunc.IsEmpty(dataSet)) { return null; }

            oTable = dataSet.Tables[dataSet.DataSetName];
            if (!DataFunc.IsEmpty(oTable))
                if (oTable.Rows.Count > 0)
                    return oTable.Rows[0];
                else
                    return null;
            else
                return null;
        }

        /// <summary>
        /// 判斷是否有指定欄位。
        /// </summary>
        /// <param name="dataTable">資料表。</param>
        /// <param name="fieldNames">欄位名稱陣列。</param>
        public static bool HasFields(DataTable dataTable, params string[] fieldNames)
        {
            foreach (string fieldName in fieldNames)
            {
                if (!HasField(dataTable, fieldName))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 判斷是否有指定欄位。
        /// </summary>
        /// <param name="dataTable">資料表。</param>
        /// <param name="fieldName">欄位名稱。</param>
        public static bool HasField(DataTable dataTable, string fieldName)
        {
            return dataTable.Columns.Contains(fieldName);
        }

        /// <summary>
        /// 判斷是否有指定欄位。
        /// </summary>
        /// <param name="dataView">檢視資料表。</param>
        /// <param name="fieldName">欄位名稱。</param>
        public static bool HasField(DataView dataView, string fieldName)
        {
            return HasField(dataView.Table, fieldName);
        }

        /// <summary>
        /// 判斷是否有指定欄位。
        /// </summary>
        /// <param name="row">資料列。</param>
        /// <param name="fieldName">欄位名稱。</param>
        public static bool HasField(DataRow row, string fieldName)
        {
            return HasField(row.Table, fieldName);
        }

        /// <summary>
        /// 判斷是否有指定欄位。
        /// </summary>
        /// <param name="row">檢視資料列。</param>
        /// <param name="fieldName">欄位名稱。</param>
        public static bool HasField(DataRowView row, string fieldName)
        {
            return HasField(row.Row, fieldName);
        }

        /// <summary>
        /// 將來源欄位值複製至目的欄位欄。
        /// </summary>
        /// <param name="dataTable">資料表。</param>
        /// <param name="sourceField">來源欄位。</param>
        /// <param name="destField">目的欄位。</param>
        public static void CopyField(DataTable dataTable, string sourceField, string destField)
        {
            // 確認目的欄位是否存在，不存在則新增
            if (!dataTable.Columns.Contains(destField))
                dataTable.Columns.Add(destField, dataTable.Columns[sourceField].DataType);
            // 複製欄位值
            foreach (DataRow row in dataTable.Rows)
                row[destField] = row[sourceField];
        }

        /// <summary>
        /// 根據來源單指定欄位資料，新增目的單指定欄位資料列
        /// </summary>
        /// <param name="destTable"></param>
        /// <param name="srcTable"></param>
        /// <param name="destField"></param>
        /// <param name="srcField"></param>
        public static void AddSrcTableRow(DataTable destTable, DataTable srcTable, string destField, string srcField)
        {
            if (DataFunc.HasField(destTable, destField) && DataFunc.HasField(srcTable, srcField))
            {
                foreach (DataRow row in srcTable.Rows)
                {
                    DataRow newRow = destTable.NewRow();
                    newRow[destField] = row[srcField].ToString();
                    destTable.Rows.Add(newRow);
                }
            }
        }

        /// <summary>
        /// 取得延伸屬性值。
        /// </summary>
        /// <param name="column">欄位。</param>
        /// <param name="propertyName">屬性名稱。</param>
        /// <param name="defaultValue">預設值。</param>
        public static object GetExtendedPropertyValue(DataColumn column, string propertyName, object defaultValue)
        {
            if (column.ExtendedProperties.Contains(propertyName))
                return column.ExtendedProperties[propertyName];
            else
                return defaultValue;
        }

        /// <summary>
        /// 計算欄位值總和
        /// </summary>
        /// <param name="table">取值計算的資料表。</param>
        /// <param name="fieldName">欄位名稱。</param>        
        public static double SumFieldValues(DataTable table, string fieldName)
        {
            double sum = 0;

            if (!IsEmpty(table, false) && HasField(table, fieldName))
            {
                foreach (DataRow row in table.Rows)
                {
                    if (row.RowState != DataRowState.Deleted)
                        sum += BaseFunc.CDouble(row[fieldName]);
                }
            }

            return sum;
        }

        /// <summary>
        /// 計算欄位值總和
        /// </summary>
        /// <param name="rows">取值計算的資料列陣列。</param>
        /// <param name="fieldName">欄位名稱。</param>        
        public static double SumFieldValues(IEnumerable<DataRow> rows, string fieldName)
        {
            double sum = 0;

            foreach (var r in rows)
            {
                if (r.RowState != DataRowState.Deleted && HasField(r, fieldName))
                    sum += BaseFunc.CDouble(r[fieldName]);
            }           

            return sum;
        }

        /// <summary>
        /// 計算欄位值總和
        /// </summary>
        /// <param name="rows">取值計算的資料列陣列。</param>
        /// <param name="fieldNames">欄位名稱。</param>        
        public static double SumFieldValues(IEnumerable<DataRow> rows, params string[] fieldNames)
        {
            double sum = 0;

            foreach (var r in rows)
            {
                foreach (var fieldName in fieldNames)
                    if (r.RowState != DataRowState.Deleted && HasField(r, fieldName))
                        sum += BaseFunc.CDouble(r[fieldName]);
            }

            return sum;
        }

        /// <summary>
        /// 計算欄位值總和
        /// </summary>
        /// <param name="row">取值計算的資料列。</param>
        /// <param name="fieldNames">欄位名稱陣列。</param>        
        public static double SumFieldValues(DataRow row, params string[] fieldNames)
        {
            double sum = 0;

            if (BaseFunc.IsNull(row))
                return sum;
            
            foreach (var fieldName in fieldNames)
                if (row.RowState != DataRowState.Deleted && HasField(row, fieldName))
                    sum += BaseFunc.CDouble(row[fieldName]);

            return sum;
        }

        /// <summary>
        /// 過濾來源資料表，只取得在查詢區間的資料
        /// </summary>
        /// <param name="srcTable">來源資料表</param>
        /// <param name="selectCount">查詢筆數</param>
        /// <param name="selectSection">查詢區間</param>
        /// <param name="acceptChanges">是否同意資料表變更</param>
        public static void FilterDataTableBySelectSection(DataTable srcTable, int selectCount, int selectSection, bool acceptChanges = false)
        {
            if (selectCount <= 0 || srcTable.DefaultView.Count == 0)
                return;
            
            if (selectSection <= 0) selectSection = 1;
            var dataCount = srcTable.DefaultView.Count;
            var sectionCount = (dataCount / selectCount) + (dataCount % selectCount > 0 ? 1 : 0);
            var sectionStart = selectCount * (selectSection - 1) + 1;
            var sectionEnd = selectCount * selectSection;

            //區間後面先砍光
            if (sectionEnd < dataCount)
                for (var i = dataCount; i >= sectionEnd + 1; i--)
                    srcTable.DefaultView.Delete(i - 1);
            //再開始砍前面的區間
            if (sectionStart > 1)
                for (var i = 1; i <= sectionStart - 1; i++)
                    if (srcTable.DefaultView.Count > 0)
                        srcTable.DefaultView.Delete(0);            

            if (acceptChanges)
                srcTable.AcceptChanges();
        }
    }
}
