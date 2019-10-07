using System;
using System.Collections.Generic;
using System.Text;

namespace Gat.Business.Hum
{
    /// <summary>
    /// 職務BL
    /// </summary>
    public class DutyBL : BusinessLogic
    {
        /// <summary>
        /// 建構函式
        /// </summary>
        /// <param name="sessionGuid"></param>
        /// <param name="progId"></param>
        public DutyBL(Guid sessionGuid, string progId) : base(sessionGuid, progId)
        {
        }
    }
}
