using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gat.Define
{
    /// <summary>
    /// 系統定義介面
    /// </summary>
    public interface ISystemDefine : IDefineFile
    {
        /// <summary>
        /// 系統編號
        /// </summary>
        string SystemID { get; set; }
    }
}
