using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gat.Define
{
    /// <summary>
    /// 連線資訊。
    /// </summary>
    public class SessionInfo
    {
        /// <summary>
        /// 連線識別。
        /// </summary>
        public Guid SessionGuid { get; set; }
        /// <summary>
        /// 員工帳號
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// 公司編號
        /// </summary>
        public string CompanyID { get; set; }
        /// <summary>
        /// 員工資訊
        /// </summary>
        public EmployeeInfo Employee { get; set; }

        /// <summary>
        /// 公司資訊
        /// </summary>
        public CompanyInfo CompanyInfo { get; set; }

        /// <summary>
        /// 資料庫類型。
        /// </summary>
        public EDatabaseType DatabaseType { get; set; } = EDatabaseType.SQLServer;
    }
}
