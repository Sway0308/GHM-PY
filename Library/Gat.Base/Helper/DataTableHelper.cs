using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gat.Base
{
    /// <summary>
    /// DataTable 操作輔助類別。
    /// </summary>
    public class DataTableHelper
    {
        #region 建構函式

        /// <summary>
        /// 建構函式。
        /// </summary>
        /// <param name="dataTable">資料表。</param>
        public DataTableHelper(DataTable dataTable)
        {
            DataTable = dataTable;
        }

        /// <summary>
        /// 建構函式。
        /// </summary>
        /// <param name="tableName">資料表名稱。</param>
        public DataTableHelper(string tableName)
        {
            DataTable = DataFunc.CreateDataTable(tableName);
            DataTable.RemotingFormat = SerializationFormat.Binary;
        }

        #endregion

        /// <summary>
        /// 資料表。
        /// </summary>
        public DataTable DataTable { get; set; } = null;

        /// <summary>
        /// 建立欄位並加入資料表中。
        /// </summary>
        /// <param name="fieldName">欄位名稱。</param>
        /// <param name="caption">欄位標題。</param>
        /// <param name="dataType">資料型別。</param>
        /// <param name="defaultValue">預設值。</param>
        /// <param name="dateTimeMode">設定資料行的 DateTimeMode。</param>
        public DataColumn AddColumn(string fieldName, string caption, Type dataType, object defaultValue, DataSetDateTime dateTimeMode = DataSetDateTime.Unspecified)
        {
            DataColumn oDataColumn;
            string sFieldName;

            // 欄位名稱全轉為大寫
            sFieldName = fieldName.ToUpper();
            oDataColumn = new DataColumn(sFieldName, dataType);
            oDataColumn.DefaultValue = defaultValue;

            if (dataType == typeof(DateTime))
                oDataColumn.DateTimeMode = dateTimeMode;

            if (!BaseFunc.IsDBNull(defaultValue))
                oDataColumn.AllowDBNull = false;

            if (StrFunc.StrIsNotEmpty(caption))
                oDataColumn.Caption = caption;

            this.DataTable.Columns.Add(oDataColumn);
            return oDataColumn;
        }

        /// <summary>
        /// 建立欄位並加入資料表中。
        /// </summary>
        /// <param name="fieldName">欄位名稱。</param>
        /// <param name="caption">欄位標題。</param>
        /// <param name="dataType">資料型別。</param>
        public DataColumn AddColumn(string fieldName, string caption, Type dataType)
        {
            object oDefaultValue = DataFunc.GetDefaultValue(dataType);
            return AddColumn(fieldName, caption, dataType, oDefaultValue);
        }

        /// <summary>
        /// 建立欄位並加入資料表中。
        /// </summary>
        /// <param name="fieldName">欄位名稱。</param>
        /// <param name="dataType">資料型別。</param>
        /// <param name="defaultValue">預設值。</param>
        public DataColumn AddColumn(string fieldName, Type dataType, object defaultValue)
        {
            return AddColumn(fieldName, string.Empty, dataType, defaultValue);
        }

        /// <summary>
        /// 建立欄位並加入資料表。
        /// </summary>
        /// <param name="fieldName">欄位名稱。</param>
        /// <param name="dataType">資料型別。</param>
        public DataColumn AddColumn(string fieldName, Type dataType)
        {
            object oDefaultValue = DataFunc.GetDefaultValue(dataType);
            return this.AddColumn(fieldName, dataType, oDefaultValue);
        }

        /// <summary>
        /// 建立欄位並加入資料表。
        /// </summary>
        /// <param name="fieldName">欄位名稱。</param>
        /// <param name="fieldDbType">欄位資料型別。</param>
        public DataColumn AddColumn(string fieldName, EFieldDbType fieldDbType)
        {
            Type oDataType;

            oDataType = DbTypeConverter.ToType(fieldDbType);
            object oDefaultValue = DataFunc.GetDefaultValue(oDataType);
            return this.AddColumn(fieldName, oDataType, oDefaultValue);
        }

        /// <summary>
        /// 建立欄位並加入資料表。
        /// </summary>
        /// <param name="fieldName">欄位名稱。</param>
        /// <param name="fieldDbType">欄位資料型別。</param>
        /// <param name="defaultValue">預設值。</param>
        public DataColumn AddColumn(string fieldName, EFieldDbType fieldDbType, object defaultValue)
        {
            Type oDataType;

            oDataType = DbTypeConverter.ToType(fieldDbType);
            return this.AddColumn(fieldName, oDataType, defaultValue);
        }

        /// <summary>
        /// 建立欄位並加入資料表中。
        /// </summary>
        /// <param name="fieldName">欄位名稱。</param>
        /// <param name="caption">欄位標題。</param>
        /// <param name="fieldDbType">欄位資料型別。</param>
        public DataColumn AddColumn(string fieldName, string caption, EFieldDbType fieldDbType)
        {
            Type oDataType;

            oDataType = DbTypeConverter.ToType(fieldDbType);
            return AddColumn(fieldName, caption, oDataType);
        }

        /// <summary>
        /// 建立欄位並加入資料表中。
        /// </summary>
        /// <param name="fieldName">欄位名稱。</param>
        /// <param name="caption">欄位標題。</param>
        /// <param name="fieldDbType">欄位資料型別。</param>
        /// <param name="defaultValue">預設值。</param>
        public DataColumn AddColumn(string fieldName, string caption, EFieldDbType fieldDbType, object defaultValue)
        {
            Type oDataType;

            oDataType = DbTypeConverter.ToType(fieldDbType);
            return AddColumn(fieldName, caption, oDataType, defaultValue);
        }

        /// <summary>
        /// 設定資料表的主索引鍵。
        /// </summary>
        /// <param name="fieldNames">主索引鍵的欄位集合字串(以逗點分隔多個欄位名稱)。</param>
        public void SetPrimaryKey(string fieldNames)
        {
            DataFunc.DataTableSetPrimaryKey(this.DataTable, fieldNames);
        }
    }
}
