using System.Data.Common;
using System.Data.SqlClient;
using Gat.Base;
using Gat.Define;

namespace Gat.Database
{
    /// <summary>
    /// SQL 資料庫命令輔助類別。
    /// </summary>
    public class SqlCommandHelper : BaseDbCommandHelper
    {
        /// <summary>
        /// 建構函式。
        /// </summary>
        public SqlCommandHelper() : base()
        {}

        /// <summary>
        /// 建構函式。
        /// </summary>
        /// <param name="commandText">命令字串。</param>
        public SqlCommandHelper(string commandText)
            : base(commandText)
        {}

        /// <summary>
        /// 建立資料庫命令。
        /// </summary>
        protected override DbCommand CreateDbCommand()
        {
            return new SqlCommand();
        }

        /// <summary>
        /// 建立資料庫命令參數。
        /// </summary>
        protected override DbParameter CreateDbParameter()
        {
            return new SqlParameter();
        }

        /// <summary>
        /// 參數符號。
        /// </summary>
        public override string ParameterSymbol
        {
            get { return "@"; }
        }

        /// <summary>
        /// 取得含前後符號的資料表名稱，前後加上雙引號。
        /// </summary>
        /// <param name="tableName">資料表名稱。</param>
        public override string GetTableName(string tableName)
        {
            return "[" + tableName + "]";
        }

        /// <summary>
        /// 取得含前後符號的欄位名稱，前後加上雙引號。
        /// </summary>
        /// <param name="fieldName">欄位名稱。</param>
        public override string GetFieldName(string fieldName)
        {
            return "[" + fieldName + "]";
        }

        /// <summary>
        /// 依欄位定義新增過濾絛件命令參數。
        /// </summary>
        /// <param name="fieldDefine">欄位定義。</param>
        public override DbParameter AddFilterParameter(FieldDefine fieldDefine)
        {
            return base.AddFilterParameter(fieldDefine);
        }

        /// <summary>
        /// 設定命令參數的資料型別。
        /// </summary>
        /// <param name="parameter">命令參數。</param>
        /// <param name="dbType">欄位資料型別。</param>
        public override void SetParameterDbType(DbParameter parameter, EFieldDbType dbType)
        {
            SqlParameter oParameter;

            oParameter = parameter as SqlParameter;
            oParameter.SqlDbType = DatabaseFunc.ToSqlDbType(dbType);
        }
    }
}
