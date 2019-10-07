using System;
using System.Collections.Generic;
using System.Text;

namespace Gat.Base
{
    /// <summary>
    /// 執行期間的例外錯誤。
    /// </summary>
    public class GException : System.Exception
    {
        #region 建構函式

        /// <summary>
        /// 建構函式。
        /// </summary>
        /// <param name="message">錯誤訊息。</param>
        public GException(string message)
            : base(message)
        { }

        /// <summary>
        /// 建構函式。
        /// </summary>
        /// <param name="message">錯誤訊息。</param>
        /// <param name="exception">內部例外參考。</param>
        public GException(string message, System.Exception exception)
            : base(message, exception)
        { }

        /// <summary>
        /// 建構函式。
        /// </summary>
        /// <param name="message">錯誤訊息。</param>
        /// <param name="args">參數陣列。</param>
        public GException(string message, params object[] args)
            : base(string.Format(message, args))
        { }

        /// <summary>
        /// 建構函式。
        /// </summary>
        /// <param name="isHandle">是否為已處理的例外。</param>
        /// <param name="message">錯誤訊息。</param>
        /// <param name="args">參數陣列。</param>
        public GException(bool isHandle, string message, params object[] args)
            : this(message, args)
        {
            IsHandle = isHandle;
        }

        #endregion

        /// <summary>
        /// 是否為已處理的例外。
        /// </summary>
        public bool IsHandle { get; set; } = true;

        /// <summary>
        /// WebService 呼叫堆疊。
        /// </summary>
        public string WsStackTrace { get; set; } = string.Empty;
    }
}
