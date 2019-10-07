using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gat.Define
{
    /// <summary>
    /// BusinessLogic Select 方法的傳出結果。
    /// </summary>
    public class SelectOutputResult
    {
        /// <summary>
        /// Entity 資料表
        /// </summary>
        public DataTable Table { get; set; }
    }
}
