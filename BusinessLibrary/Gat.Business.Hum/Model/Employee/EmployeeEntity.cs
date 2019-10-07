using Gat.Define;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gat.Business.Hum
{
    /// <summary>
    /// 員工基本資料
    /// </summary>
    public class EmployeeEntity : EntityRow
    {
        /// <summary>
        /// 公司編號
        /// </summary>
        public Guid SYS_CompanyId { get; set; }

        /// <summary>
        /// 員工編號
        /// </summary>
        public Guid SYS_Id { get; set; }

        /// <summary>
        /// 員工工號
        /// </summary>
        public string SYS_ViewId { get; set; }

        /// <summary>
        /// 員工姓名
        /// </summary>
        public string SYS_Name { get; set; }

        /// <summary>
        /// 職務編號
        /// </summary>
        public Guid DutyID { get; set; }

        /// <summary>
        /// 部門編號
        /// </summary>
        public Guid DepartId { get; set; }
    }
}
