using Gat.Base;
using Gat.Define;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gat.Setting
{
    /// <summary>
    /// 儲存定義檔案輔助器
    /// </summary>
    public class SaveDefineHelper
    {
        /// <summary>
        /// 儲存定義檔案
        /// </summary>
        /// <param name="defineFile"></param>
        /// <returns></returns>
        public void SaveDefine(IDefineFile defineFile)
        {
            if (defineFile == null)
                throw new GException("Define is null");

            var json = JsonFunc.ObjectToJson(defineFile);
            FileFunc.FileWriteAllText(defineFile.GetDefineFilePath(), json, true);
        }
    }
}
