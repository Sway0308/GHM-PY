using System;
using System.ComponentModel;
using Gat.Base;

namespace Gat.Define
{
    /// <summary>
    /// 員工資訊。
    /// </summary>
    public class EmployeeInfo
    {
        /// <summary>
        /// 員工內碼。
        /// </summary>
        public string ID { get; set; } = string.Empty;

        /// <summary>
        /// 員工外碼。
        /// </summary>
        public string ViewID { get; set; } = string.Empty;

        /// <summary>
        /// 員工名稱。
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 部門內碼。
        /// </summary>
        public string DepartmentID { get; set; } = string.Empty;

        /// <summary>
        /// 部門外碼。
        /// </summary>
        public string DepartmentViewID { get; set; } = string.Empty;

        /// <summary>
        /// 部門歸階。
        /// </summary>
        public string DepartmentLevel { get; set; } = string.Empty;

        /// <summary>
        /// 部門名稱。
        /// </summary>
        public string DepartmentName { get; set; } = string.Empty;

        /// <summary>
        /// 根部門內碼。
        /// </summary>
        public string RootDepartmentID { get; set; } = string.Empty;

        /// <summary>
        /// 根部門外碼。
        /// </summary>
        public string RootDepartmentViewID { get; set; } = string.Empty;

        /// <summary>
        /// 根部門歸階。
        /// </summary>
        public string RootDepartmentLevel { get; set; } = string.Empty;

        /// <summary>
        /// 根部門名稱。
        /// </summary>
        public string RootDepartmentName { get; set; } = string.Empty;

        /// <summary>
        /// 物件的描述文字。
        /// </summary>
        public override string ToString()
        {
            return string.Format("{0} - {1}", this.ViewID, this.Name);
        }
    }
}
