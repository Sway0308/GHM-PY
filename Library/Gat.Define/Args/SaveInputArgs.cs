using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gat.Define
{
    /// <summary>
    /// BusinessLogic Save 方法的傳入引數。
    /// </summary>
    public class SaveInputArgs
    {
        /// <summary>
        /// 取消動作
        /// </summary>
        public bool Cancel { get; set; }
        /// <summary>
        /// Entity資料集。
        /// </summary>
        public DataSet DataSet { get; set; }
        /// <summary>
        /// 儲存模式
        /// </summary>
        public ESaveMode SaveMode { get; set; }
    }
}
