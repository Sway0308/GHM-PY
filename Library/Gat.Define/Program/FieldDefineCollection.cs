using Gat.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Gat.Define
{
    /// <summary>
    /// 欄位定義集合
    /// </summary>
    [Serializable]
    public class FieldDefineCollection : KeyCollectionBase<FieldDefine>
    {
        #region 建構函式

        /// <summary>
        /// 建構函式。
        /// </summary>
        /// <param name="owner">資料表定義。</param>
        public FieldDefineCollection(TableDefine owner)
            : base(owner)
        { }

        #endregion

        /// <summary>
        /// 找出關連主欄位的欄位定義
        /// </summary>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public FieldDefine FindActiveLinkFieldDefine(string fieldName)
        {
            var fieldDefine = this[fieldName];
            if (fieldDefine == null)
                return null;

            return this[fieldDefine.FieldName];
        }
    }
}
