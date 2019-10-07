using System;
using System.Data;
using Gat.Base;
using Gat.Define;

namespace Gat.Database
{
    /// <summary>
    /// 資料表虛擬欄位產生器。
    /// </summary>
    public class VirtualFieldBuilder
    {
        /// <summary>
        /// 建構函式。
        /// </summary>
        /// <param name="tableDefine">資料表定義。</param>
        /// <param name="dataTable">資料表。</param>
        public VirtualFieldBuilder(TableDefine tableDefine, DataTable dataTable)
        {
            this.TableDefine = tableDefine;
            this.DataTable = dataTable;
        }

        /// <summary>
        /// 資料表定義。
        /// </summary>
        private TableDefine TableDefine { get; set; }

        /// <summary>
        /// 資料表。
        /// </summary>
        private DataTable DataTable { get; set; }

        /// <summary>
        /// 執行建立虛擬欄位的作業。
        /// </summary>
        public void Execute()
        {
            var oHelper = new DataTableHelper(this.DataTable);
            foreach (FieldDefine fieldDefine in TableDefine.Fields)
            {
                // 針對 VirtualField 欄位，動態加入 DataColumn
                if (fieldDefine.FieldType == EFieldType.VirtualField && !this.DataTable.Columns.Contains(fieldDefine.FieldName))
                {
                    var dataCol = oHelper.AddColumn(fieldDefine.FieldName, fieldDefine.DbType);
                    SetAllowNullSetting(dataCol, fieldDefine);
                }
            }
        }

        /// <summary>
        /// 判斷指定的欄位集合字串是是否有虛擬欄位，並執行建立虛擬欄位的作業。
        /// </summary>
        public void Execute(string fields)
        {
            if (StrFunc.StrIsEmpty(fields)) { return; }

            var oHelper = new DataTableHelper(this.DataTable);

            var oFields = StrFunc.StrSplit(fields, ",");
            foreach (string fieldName in oFields)
            {
                var oFieldDefine = this.TableDefine.Fields[fieldName];
                // 針對 VirtualField 欄位，動態加入 DataColumn
                if (BaseFunc.IsNotNull(oFieldDefine) && oFieldDefine.FieldType == EFieldType.VirtualField && !this.DataTable.Columns.Contains(oFieldDefine.FieldName))
                {
                    var dataCol = oHelper.AddColumn(oFieldDefine.FieldName, oFieldDefine.DbType);
                    SetAllowNullSetting(dataCol, oFieldDefine);
                }
            }
        }

        /// <summary>
        /// 加入AllowNull設定
        /// </summary>
        /// <param name="dataCol"></param>
        /// <param name="fieldDefine"></param>
        private void SetAllowNullSetting(DataColumn dataCol, FieldDefine fieldDefine)
        {
            if (fieldDefine.AllowNull)
            {
                dataCol.AllowDBNull = true;
                dataCol.DefaultValue = DBNull.Value;
            }
        }
    }
}
