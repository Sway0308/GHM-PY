using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gat.Base
{
    /// <summary>
    /// 參數集合。
    /// </summary>
    public class ParameterCollection : KeyCollectionBase<Parameter>
    {
        /// <summary>
        /// 取得參數值。
        /// </summary>
        /// <typeparam name="T">參數型別。</typeparam>
        /// <param name="name">參數名稱。</param>
        public T GetValue<T>(string name)
        {
            return (T)GetParameterValue(name);
        }

        /// <summary>
        /// 取得指定名稱的參數值。
        /// </summary>
        /// <param name="name">參數名稱。</param>
        public object GetParameterValue(string name)
        {
            return GetParameterValue(name, string.Empty);
        }

        /// <summary>
        /// 取得指定名稱的參數值。
        /// </summary>
        /// <param name="name">參數名稱。</param>
        /// <param name="defaultValue">預設值。</param>
        public object GetParameterValue(string name, object defaultValue)
        {
            if (this.Contains(name))
                return this[name].Value;
            else
                return defaultValue;
        }

        /// <summary>
        /// 取得參數值。
        /// </summary>
        /// <typeparam name="T">參數型別。</typeparam>
        /// <param name="name">參數名稱。</param>
        /// <param name="defaultValue">預設值。</param>
        public T GetValue<T>(string name, T defaultValue)
        {
            if (this.Contains(name))
                return (T)GetParameterValue(name);
            else
                return defaultValue;
        }

        /// <summary>
        /// 取得指定名稱的參數值，並轉型為字串。
        /// </summary>
        /// <param name="name">參數名稱。</param>
        public string GetParameterAsString(string name)
        {
            return BaseFunc.CStr(GetParameterValue(name, string.Empty));
        }

        /// <summary>
        /// 取得指定名稱的參數值，並轉型為整數。
        /// </summary>
        /// <param name="name">參數名稱。</param>
        public int GetParameterAsInt(string name)
        {
            return BaseFunc.CInt(GetParameterValue(name, 0));
        }

        /// <summary>
        /// 取得指定名稱的參數值，並轉型為整數。
        /// </summary>
        /// <param name="name">參數名稱。</param>
        public long GetParameterAsLong(string name)
        {
            return BaseFunc.CLong(GetParameterValue(name, 0));
        }

        /// <summary>
        /// 取得指定名稱的參數值，並轉型為雙精度浮點數。
        /// </summary>
        /// <param name="name">參數名稱。</param>
        public double GetParameterAsDouble(string name)
        {
            return BaseFunc.CDouble(GetParameterValue(name, 0));
        }

        /// <summary>
        /// 取得指定名稱的參數值，並轉型為布林值。
        /// </summary>
        /// <param name="name">參數名稱。</param>
        public bool GetParameterAsBool(string name)
        {
            return BaseFunc.CBool(GetParameterValue(name, false));
        }

        /// <summary>
        /// 取得指定名稱的參數值，並轉型為 Guid 值。
        /// </summary>
        /// <param name="name">參數名稱。</param>
        public Guid GetParameterAsGuid(string name)
        {
            return BaseFunc.CGuid(GetParameterValue(name, Guid.Empty));
        }

        /// <summary>
        /// 取得指定名稱的參數值，並轉型為日期時間值。
        /// </summary>
        /// <param name="name">參數名稱。</param>
        public DateTime GetParameterAsDateTime(string name)
        {
            return BaseFunc.CDateTime(GetParameterValue(name, DateTime.MinValue));
        }
    }
}
