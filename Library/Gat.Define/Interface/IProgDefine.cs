using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gat.Define
{
    /// <summary>
    /// 功能定義介面
    /// </summary>
    public interface IProgDefine : IDefineFile
    {
        /// <summary>
        /// 程式定義
        /// </summary>
        string ProgId { get; set; }
    }
}
