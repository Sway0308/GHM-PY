using System;
using System.Collections.Generic;
using System.Text;

namespace Gat.Business.Hum
{
    /// <summary>
    /// 部門基本資料
    /// </summary>
    public class DepartBL : BusinessLogic
    {
        /// <summary>
        /// 建構函式
        /// </summary>
        /// <param name="sessionGuid"></param>
        /// <param name="progId"></param>
        public DepartBL(Guid sessionGuid, string progId) : base(sessionGuid, progId)
        {
        }
    }
}
