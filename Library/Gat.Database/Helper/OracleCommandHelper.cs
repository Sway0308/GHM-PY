using System.Data.Common;
using System.Data.OracleClient;
using Gat.Base;

namespace Gat.Database
{
    /// <summary>
    /// Oracle 資料庫命令輔助類別。
    /// </summary>
    public class OracleCommandHelper : BaseDbCommandHelper
    {
        /// <summary>
        /// 建構函式。
        /// </summary>
        public OracleCommandHelper() : base()
        {}

        /// <summary>
        /// 建構函式。
        /// </summary>
        /// <param name="commandText">命令字串。</param>
        public OracleCommandHelper(string commandText)
            : base(commandText)
        {}

        /// <summary>
        /// 建立資料庫命令。
        /// </summary>
        protected override DbCommand CreateDbCommand()
        {
#pragma warning disable 618
            return new OracleCommand();
#pragma warning restore 618
        }

        /// <summary>
        /// 建立資料庫命令參數。
        /// </summary>
        protected override DbParameter CreateDbParameter()
        {
            return new OracleParameter();
        }

        /// <summary>
        /// 參數符號。
        /// </summary>
        public override string ParameterSymbol
        {
            get { return ":"; }
        }

        /// <summary>
        /// 取得含前後符號的資料表名稱，前後加上雙引號。
        /// </summary>
        /// <param name="tableName">資料表名稱。</param>
        public override string GetTableName(string tableName)
        {
            return "\"" + tableName + "\"";
        }

        /// <summary>
        /// 取得含前後符號的欄位名稱，前後加上雙引號。
        /// </summary>
        /// <param name="fieldName">欄位名稱。</param>
        public override string GetFieldName(string fieldName)
        {
            return "\"" + fieldName + "\"";
        }

        /// <summary>
        /// 設定命令參數的資料型別。
        /// </summary>
        /// <param name="parameter">命令參數。</param>
        /// <param name="dbType">欄位資料型別。</param>
        public override void SetParameterDbType(DbParameter parameter, EFieldDbType dbType)
        {
            OracleParameter oParameter;

            oParameter = parameter as OracleParameter;
            oParameter.OracleType = DatabaseFunc.ToOracleType(dbType);
        }

        /// <summary>
        /// 取得轉換後的參數值。
        /// </summary>
        /// <param name="dbType">欄位資料型別。</param>
        /// <param name="value">參數值。</param>
        public override object GetParameterValue(EFieldDbType dbType, object value)
        {
            if (dbType == EFieldDbType.GUID)
                return value.ToString();
            else
                return base.GetParameterValue(dbType, value);
        }

    }
}
