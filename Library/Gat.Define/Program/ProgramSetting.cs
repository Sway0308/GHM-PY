using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Gat.Define
{
    /// <summary>
    /// 程式設定
    /// </summary>
    public class ProgramSetting : IDefineFile
    {
        /// <summary>
        /// 建構函式
        /// </summary>
        public ProgramSetting()
        {
        }

        /// <summary>
        /// 取得定義檔案路徑
        /// </summary>
        /// <returns></returns>
        public string GetDefineFilePath()
        {
            return SysDefineSettingName.ProgramSettingFilePath;
        }

        /// <summary>
        /// 顯示名稱
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 程式項目
        /// </summary>
        public ProgramItemCollection Items { get; } = new ProgramItemCollection();
    }
}
