using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gat.Define
{
    /// <summary>
    /// 定義檔案
    /// </summary>
    public interface IDefineFile
    {
        /// <summary>
        /// 取得定義檔案路徑
        /// </summary>
        /// <returns></returns>
        string GetDefineFilePath();
    }
}
