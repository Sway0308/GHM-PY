using Gat.Base;
using Gat.Define;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gat.Database
{
    /// <summary>
    /// 資料庫相關函式。
    /// </summary>
    public class DatabaseFunc
    {
        /// <summary>
        /// 轉換 FieldDbType 為 OracleTypem。
        /// </summary>
        /// <param name="fieldDbType">FieldType列舉。</param>
        public static OracleType ToOracleType(EFieldDbType fieldDbType)
        {
            switch (fieldDbType)
            {
                case EFieldDbType.String: return OracleType.NChar;
                case EFieldDbType.Text: return OracleType.NVarChar;
                case EFieldDbType.Boolean: return OracleType.Char;
                case EFieldDbType.Integer: return OracleType.Int32;
                case EFieldDbType.Double: return OracleType.Double;
                case EFieldDbType.DateTime: return OracleType.DateTime;
                case EFieldDbType.Currency: return OracleType.Number;
                case EFieldDbType.GUID: return OracleType.NVarChar;
                default:
                    throw new GException("{0} can't convert to OracleType", fieldDbType.ToString());
            }
        }

        /// <summary>
        /// 轉換 FieldDbType 為 SqlDbType。
        /// </summary>
        /// <param name="fieldDbType">FieldType列舉。</param>
        public static SqlDbType ToSqlDbType(EFieldDbType fieldDbType)
        {
            switch (fieldDbType)
            {
                case EFieldDbType.String: return SqlDbType.NVarChar;
                case EFieldDbType.Text: return SqlDbType.NVarChar;
                case EFieldDbType.Boolean: return SqlDbType.Bit;
                case EFieldDbType.Integer: return SqlDbType.Int;
                case EFieldDbType.Currency: return SqlDbType.Money;
                case EFieldDbType.Double: return SqlDbType.Float;
                case EFieldDbType.DateTime: return SqlDbType.DateTime;
                case EFieldDbType.GUID: return SqlDbType.UniqueIdentifier;
                case EFieldDbType.Binary: return SqlDbType.Binary;
                default:
                    throw new GException("{0} can't convert to SqlDbType", fieldDbType.ToString());
            }
        }

        /// <summary>
        /// 取得下一個資料表別名。
        /// </summary>
        /// <param name="tableAlias">目前資料表別名。</param>
        public static string GetNextTableAlias(string tableAlias)
        {
            var baseValues = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var newTableAlias = BaseFunc.GetNextID(tableAlias, baseValues);
            // 若資料表別名為關鍵字，則重取資料表別名
            if (StrFunc.SameTextOr(newTableAlias, "AS", "BY"))
                newTableAlias = BaseFunc.GetNextID(newTableAlias, baseValues);

            return newTableAlias;
        }

        /// <summary>
        /// 依資料庫類型建立資料庫命令輔助類別。
        /// </summary>
        /// <param name="databaseType">資料庫類型。</param>
        /// <param name="commandText">命令字串。</param>
        public static IDbCommandHelper CreateDbCommandHelper(EDatabaseType databaseType, string commandText)
        {
            string sMessage;

            switch (databaseType)
            {
                case EDatabaseType.SQLServer:
                    return new SqlCommandHelper(commandText);
                case EDatabaseType.Oracle:
                    return new OracleCommandHelper(commandText);
                default:
                    sMessage = string.Format(@"DatabaseType={0} Not Supported IDbCommandHelper", BaseFunc.GetEnumName(databaseType));
                    throw new NotSupportedException(sMessage);
            }
        }

        /// <summary>
        /// 依資料庫類型建立資料庫命令輔助類別。
        /// </summary>
        /// <param name="databaseType">資料庫類型。</param>
        public static IDbCommandHelper CreateDbCommandHelper(EDatabaseType databaseType)
        {
            return CreateDbCommandHelper(databaseType, string.Empty);
        }

        /// <summary>
        /// 建立資料庫命令產生器。
        /// </summary>
        /// <param name="sessionInfo">連線資訊。</param>
        /// <param name="programDefine">程式定義。</param>
        public static IDbCommandBuilder CreateDbCommandBuilder(SessionInfo sessionInfo, ProgramDefine programDefine)
        {
            string sMessage;

            switch (sessionInfo.DatabaseType)
            {
                case EDatabaseType.SQLServer:
                    return new SqlCommandBuilder(sessionInfo.SessionGuid, programDefine);
                case EDatabaseType.Oracle:
                    return new OracleCommandBuilder(sessionInfo.SessionGuid, programDefine);
                default:
                    sMessage = string.Format(@"DatabaseType={0} Not Supported IDbCommandBuilder", BaseFunc.GetEnumName(sessionInfo.DatabaseType));
                    throw new NotSupportedException(sMessage);
            }
        }
    }
}
