using Gat.Define;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gat.Business.Hum
{
    /// <summary>
    /// 部門基本資料
    /// </summary>
    public class DepartEntity : EntityRow
    {
        /// <summary>
        /// 公司編號
        /// </summary>
        public Guid SYS_CompanyId { get; set; }

        /// <summary>
        /// 部門編號
        /// </summary>
        public Guid SYS_Id { get; set; }

        /// <summary>
        /// 部門代碼
        /// </summary>
        public string SYS_ViewId { get; set; }

        /// <summary>
        /// 員工姓名
        /// </summary>
        public string SYS_Name { get; set; }
    }
}
