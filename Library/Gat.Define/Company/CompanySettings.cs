using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gat.Define
{
    /// <summary>
    /// 公司項目設定
    /// </summary>
    public class CompanySettings : IDefineFile
    {
        /// <summary>
        /// 定義檔案路徑
        /// </summary>
        /// <returns></returns>
        public string GetDefineFilePath()
        {
            return SysDefineSettingName.CompanySettingPath;
        }

        /// <summary>
        /// 公司清單集合。
        /// </summary>
        public CompanyItemCollection Items { get; } = new CompanyItemCollection();
    }
}
