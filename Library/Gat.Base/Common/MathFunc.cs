using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gat.Base
{
    /// <summary>
    /// 數學函數共用函式庫
    /// </summary>
    public class MathFunc
    {
        /// <summary>
        /// 將浮點數四捨五入至指定位數。
        /// </summary>
        /// <param name="value">浮點數。</param>
        /// <param name="digits">小數位數。</param>
        public static double Round(double value, int digits)
        {
            // 預設為偶捨基入，加上 MidpointRounding.AwayFromZero 才是一般認知的四捨五入
            return Math.Round(value, digits, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// 十進位數值四捨五入至指定位數。
        /// </summary>
        /// <param name="value">十進位數值。</param>
        /// <param name="digits">小數位數。</param>
        public static decimal Round(decimal value, int digits)
        {
            // 預設為偶捨基入，加上 MidpointRounding.AwayFromZero 才是一般認知的四捨五入
            return Math.Round(value, digits, MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// 無條件捨去至指定位數
        /// </summary>
        /// <param name="value">數值</param>
        /// <param name="digit">小數位數</param>
        /// <returns></returns>
        public static double Floor(double value, int digit)
        {
            if (digit < 0) { return Math.Floor(value); }
            var pow = Math.Pow(10, digit);
            var sign = value >= 0 ? 1 : -1;
            return sign * Math.Floor(sign * value * pow) / pow;
        }

        /// <summary>
        /// 無條件進位至指定位數
        /// </summary>
        /// <param name="value">數值</param>
        /// <param name="digit">小數位數</param>
        /// <returns></returns>
        public static double Ceiling(double value, int digit)
        {
            if (digit < 0) { return Math.Floor(value); }
            var pow = Math.Pow(10, digit);
            var sign = value >= 0 ? 1 : -1;
            return sign * Math.Ceiling(sign * value * pow) / pow;
        }
    }
}
