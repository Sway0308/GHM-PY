using Gat.Define;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gat.Business.Hum
{
    /// <summary>
    /// 職務
    /// </summary>
    public class DutyEntity : EntityRow
    {
        /// <summary>
        /// 公司編號
        /// </summary>
        public Guid SYS_CompanyId { get; set; }

        /// <summary>
        /// 職務編號
        /// </summary>
        public Guid SYS_Id { get; set; }

        /// <summary>
        /// 職務代碼
        /// </summary>
        public string SYS_ViewId { get; set; }

        /// <summary>
        /// 員工姓名
        /// </summary>
        public string SYS_Name { get; set; }
    }
}
