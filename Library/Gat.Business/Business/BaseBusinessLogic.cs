using Gat.Cache;
using Gat.Define;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gat.Business
{
    /// <summary>
    /// 基底功能層級商業邏輯元件
    /// </summary>
    public class BaseBusinessLogic : BaseDbAccess
    {
        /// <summary>
        /// 建構函式
        /// </summary>
        /// <param name="progId">程式代碼</param>
        public BaseBusinessLogic(Guid sessionGuid, string progId) : base(sessionGuid)
        {
            this.progId = progId;
            this.ProgramDefine = CacheFunc.GetProgramDefine(this.progId);
        }

        /// <summary>
        /// 程式代碼
        /// </summary>
        public string progId { get; }
        /// <summary>
        /// 程式定義
        /// </summary>
        public ProgramDefine ProgramDefine { get; }

        /// <summary>
        /// 資料庫代碼
        /// </summary>
        public string DatabaseID { get; } = "001";
    }
}
