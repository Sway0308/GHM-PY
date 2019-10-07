using System;
using Gat.Define;

namespace Gat.Business.Hum
{
    /// <summary>
    /// 員工基本資料BL
    /// </summary>
    public class EmployeeBL : BusinessLogic
    {
        /// <summary>
        /// 建構函式
        /// </summary>
        /// <param name="sessionGuid"></param>
        /// <param name="progId"></param>
        public EmployeeBL(Guid sessionGuid, string progId) : base(sessionGuid, progId)
        {
        }

        /// <summary>
        /// 執行 Save 方法前呼叫的方法。
        /// </summary>
        /// <param name="inputArgs">輸入參數</param>
        /// <param name="outputResult">傳出參數</param>
        protected override void DoBeforeSave(SaveInputArgs inputArgs, SaveOutputResult outputResult)
        {
            base.DoBeforeSave(inputArgs, outputResult);
        }
    }
}
