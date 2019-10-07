using Gat.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gat.Define
{
    /// <summary>
    /// BusinessLogic.ExecFunc 方法的傳出結果。
    /// </summary>
    public class ExecFuncOutputResult
    {
        /// <summary>
        /// 參數設定
        /// </summary>
        public ParameterCollection Parameters { get; } = new ParameterCollection();
    }
}
