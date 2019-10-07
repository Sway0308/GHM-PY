using System;
using Gat.Define;

namespace Gat.Database
{
    /// <summary>
    /// SQL Server 資料庫命令產生器。
    /// </summary>
    public class SqlCommandBuilder : BaseDbCommandBuilder, IDbCommandBuilder
    {
        /// <summary>
        /// 建構函式。
        /// </summary>
        /// <param name="sessionGuid">連線識別。</param>
        /// <param name="programDefine">程式定義。</param>
        public SqlCommandBuilder(Guid sessionGuid, ProgramDefine programDefine)
            : base(sessionGuid, programDefine)
        {
        }
    }
}
