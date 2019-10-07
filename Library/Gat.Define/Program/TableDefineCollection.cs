using Gat.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gat.Define
{
    /// <summary>
    /// 資料表定義集合
    /// </summary>
    public class TableDefineCollection : KeyCollectionBase<TableDefine>
    {
        /// <summary>
        /// 建構函式。
        /// </summary>
        /// <param name="programDefine">程式定義。</param>
        public TableDefineCollection(ProgramDefine programDefine)
            : base(programDefine)
        { }
    }
}
