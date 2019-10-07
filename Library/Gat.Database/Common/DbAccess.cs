using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Dapper;
using System.Data;
using Gat.Define;
using System.Data.Common;
using System.Transactions;
using Gat.Base;
using System.Linq;
using Gat.Cache;

namespace Gat.Database
{
    /// <summary>
    /// 資料庫存取物件。
    /// </summary>
    public class DbAccess
    {
        /// <summary>
        /// 建構函式
        /// </summary>
        public DbAccess(DatabaseSettings dbSetting)
        {
            this.DbSetting = dbSetting;
        }

        /// <summary>
        /// 資料庫清單設定
        /// </summary>
        private DatabaseSettings DbSetting { get; }

        /// <summary>
        /// 資料庫連線
        /// </summary>
        private SqlConnection SqlConnection(string databaseID)
        {
            var item = this.DbSetting.Items[databaseID];
            if (item == null)
                return null;
            var connectionString = string.Format("Server={0};Database={1};User ID={2};Password={3};Connection Timeout=30;ConnectRetryCount=6;ConnectRetryInterval=5",
                item.DbServer,
                item.DbName,
                item.LoginID,
                item.Password    
            );
            return new SqlConnection(connectionString);
        }

        /// <summary>
        /// 建立新的 DataTable，並執行命令載入資料。 
        /// </summary>
        /// <param name="databaseID">資料庫編號。</param>
        /// <param name="command">資料庫命令。</param>
        /// <param name="commandTimeout">等待命令執行的時間 (以秒為單位)，-1 表示不指定。</param>
        public DataTable ExecuteDataTable(string databaseID, DbCommand command, int commandTimeout = -1)
        {
            var dbParams = new Dictionary<string, object>();
            foreach (DbParameter para in command.Parameters)
                dbParams.Add(para.ParameterName, para.Value);

            var dt = new DataTable();
            var row = dt.NewRow();
            dt.Load(this.SqlConnection(databaseID).ExecuteReader(command.CommandText, dbParams));
            return dt;
        }

        /// <summary>
        /// 執行資料庫命令，並傳回資料表
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable ExecuteDataTable(string databaseID, string sql)
        {
            var dt = new DataTable();
            dt.Load(this.SqlConnection(databaseID).ExecuteReader(sql, new { }));
            return dt;
        }

        /// <summary>
        /// 執行資料庫命令，並傳回指定型別列舉
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public IEnumerable<T> ExecuteQuery<T>(string databaseID, string sql)
        {
            return this.SqlConnection(databaseID).Query<T>(sql);
        }

        /// <summary>
        /// 執行資料庫命令，並傳回影響資料列數。
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string databaseID, string sql)
        {
            return this.SqlConnection(databaseID).Execute(sql);
        }

        /// <summary>
        /// 使用SqlBulkCopy大量新增資料
        /// </summary>
        /// <param name="databaseID">資料庫編號</param>
        /// <param name="progId">程式定義代碼</param>
        /// <param name="dataSet">資料集</param>
        public void SqlBulkCopy(string databaseID, string progId, DataSet dataSet)
        {
            foreach (DataTable table in dataSet.Tables)
                SqlBulkCopy(databaseID, progId, table.TableName, table);
        }

        /// <summary>
        /// 使用SqlBulkCopy大量新增資料
        /// </summary>
        /// <param name="databaseID">資料庫編號</param>
        /// <param name="progId">程式定義代碼</param>
        /// <param name="tableName">資料定義代碼</param>
        /// <param name="table">資料表</param>
        public void SqlBulkCopy(string databaseID, string progId, string tableName, DataTable table)
        {
            var progDefine = CacheFunc.GetProgramDefine(progId);
            var tableDefine = progDefine.Tables[tableName];
            if (tableDefine == null || !tableDefine.IsCreateDbTable || DataFunc.IsEmpty(table))
                return;
            var sqlBulkCopy = CreateSqlBulkCopy(databaseID, tableDefine, table);
            SqlBulkCopy(sqlBulkCopy, table);
        }

        /// <summary>
        /// 產生SqlBulkCopy
        /// </summary>
        /// <param name="databaseID">資料庫編號</param>
        /// <param name="tableDefine">資料表定義</param>
        /// <param name="table">資料表</param>
        /// <returns></returns>
        private SqlBulkCopy CreateSqlBulkCopy(string databaseID, TableDefine tableDefine, DataTable table)
        {
            var sqlBulkCopy = new SqlBulkCopy(this.SqlConnection(databaseID)) { DestinationTableName = tableDefine.DbTableName };
            foreach (var fieldDefine in tableDefine.Fields.Where(x => DataFunc.HasField(table, x.FieldName)))
                sqlBulkCopy.ColumnMappings.Add(fieldDefine.FieldName, fieldDefine.DbFieldName);
            return sqlBulkCopy;
        }

        /// <summary>
        /// 使用SqlBulkCopy大量新增資料
        /// </summary>
        /// <param name="sqlBulkCopy"></param>
        /// <param name="table">資料表</param>
        private void SqlBulkCopy(SqlBulkCopy sqlBulkCopy, DataTable table)
        {
            using (var tx = new TransactionScope())
            {
                sqlBulkCopy.WriteToServer(table);
                sqlBulkCopy.Close();
                tx.Complete();
            }
        }
    }
}
