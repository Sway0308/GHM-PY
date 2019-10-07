using Gat.Base;
using Gat.Cache;
using Gat.Define;
using System;
using System.Data;

namespace Gat.Business
{
    /// <summary>
    /// 商業邏輯共用函式。
    /// </summary>
    public class BusinessFunc
    {
        /// <summary>
        /// 建立指定程式代碼的商業邏輯物件。
        /// </summary>
        /// <param name="progId"></param>
        /// <returns></returns>
        public static IBusinessLogic CreateBusinessLogic(Guid sessionGuid, string progId)
        {
            var progItem = CacheFunc.GetProgramItem(progId);
            if (progItem.BusinessInstanceType.IsEmpty)
                return new BusinessLogic(sessionGuid, progId);
            else
                return DefineFunc.CreateBusinessLogic(progItem.BusinessInstanceType, sessionGuid, progId);
        }

        /// <summary>
        /// 建立指定代碼 > 指定資料表的Entity資料列
        /// </summary>
        /// <param name="progId"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static EntityRow CreateEntityRow(string progId, string tableName)
        {
            var progDefine = CacheFunc.GetProgramDefine(progId);
            var tableDefine = progDefine.Tables[tableName];
            if (tableDefine.EntityInstanceType.IsEmpty)
                return new EntityRow();
            else
                return DefineFunc.CreateEntityRow(tableDefine.EntityInstanceType);
        }

        /// <summary>
        /// 建立指定代碼 > 指定資料表 > 指定型別的Entity資料列
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="progId"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static T CreateEntityRow<T>(string progId, string tableName) where T : EntityRow
        {
            return (T)CreateEntityRow(progId, tableName);
        }

        /// <summary>
        /// 設定資料表中每個欄位的預設值。
        /// </summary>
        /// <param name="tableDefine">資料表定義。</param>
        /// <param name="dataTable">資料表。</param>
        public static void SetDataColumnDefaultValue(TableDefine tableDefine, DataTable dataTable)
        {
            foreach (DataColumn oColumn in dataTable.Columns)
            {
                var oFieldDefine = tableDefine.Fields[oColumn.ColumnName];
                if (BaseFunc.IsNotNull(oFieldDefine) && !oFieldDefine.AllowNull)
                {
                    oColumn.DefaultValue = DataFunc.GetDefaultValue(oFieldDefine.DbType);
                }
            }
        }
    }
}
