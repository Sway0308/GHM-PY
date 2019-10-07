using System;
using System.Collections;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Gat.Base
{
    /// <summary>
    /// 基礎函式庫。
    /// </summary>
    public class BaseFunc
    {
        #region 判斷空值相關方法

        /// <summary>
        /// 是否為 DBNull 值。
        /// </summary>
        /// <param name="value">要判斷的值。</param>
        public static bool IsDBNull(object value)
        {
            return Convert.IsDBNull(value);
        }

        /// <summary>
        /// 是傳入值否為 Null 值。
        /// </summary>
        /// <param name="value">要判斷的值。</param>
        public static bool IsNull(object value)
        {
            return (value == null);
        }

        /// <summary>
        /// 判斷傳入值是否不為 Null 值。
        /// </summary>
        /// <param name="value">要判斷的值。</param>
        public static bool IsNotNull(object value)
        {
            return (value != null);
        }

        /// <summary>
        /// 是否為空值，Null 或 DBNull 皆視為空值。
        /// </summary>
        /// <param name="value">要判斷的值。</param>
        public static bool IsEmpty(object value)
        {
            if (value is string)
                return StrFunc.StrIsEmpty(BaseFunc.CStr(value));
            else
                return IsNull(value);
        }

        /// <summary>
        /// 判斷 Guid 值是否為空值。
        /// </summary>
        /// <param name="value">要判斷的 Guid 值。</param>
        public static bool IsEmpty(Guid value)
        {
            return (value == Guid.Empty);
        }

        /// <summary>
        /// 判斷日期值是否為空值。
        /// </summary>
        /// <param name="value">要判斷的日期值。</param>
        public static bool IsEmpty(DateTime value)
        {
            // 日期為 Null、DbNull、DateTime.MinValue 皆視為空值
            return (IsNull(value) || DateTime.Compare(value, DateTime.MinValue) == 0);
        }

        /// <summary>
        /// 判斷 IDictionary 型別的集合是否無資料。
        /// </summary>
        /// <param name="value">要判斷的集合。</param>
        public static bool IsEmpty(IDictionary value)
        {
            if ((value != null) && (value.Count != 0))
                return false;
            else
                return true;
        }

        #endregion

        #region 型別轉換相關方法

        /// <summary>
        /// 轉型為文字。
        /// </summary>
        /// <param name="value">要轉型的值。</param>
        /// <param name="defaultValue">無法成功轉型的預設值。</param>
        public static string CStr(object value, string defaultValue)
        {
            //若為 Null 或 DBNull 值，則傳回預設值
            if (BaseFunc.IsNull(value))
            {
                return defaultValue;
            }
            //若為列舉型別，則傳回列舉名稱
            if (value is Enum)
            {
                return GetEnumName((Enum)value);
            }
            //轉型為文字
            //return (string)value;
            return Convert.ToString(value);
        }

        /// <summary>
        /// 轉型為文字。
        /// </summary>
        /// <param name="value">要轉型的值。</param>
        public static string CStr(object value)
        {
            return BaseFunc.CStr(value, string.Empty);
        }

        /// <summary>
        /// 轉型為文字，並去除左右空白。
        /// </summary>
        /// <param name="value">要轉型的值。</param>
        public static string CStrTrim(object value)
        {
            return BaseFunc.CStr(value).Trim();
        }

        /// <summary>
        /// 轉型為布林值。
        /// </summary>
        /// <param name="value">要轉型的值。</param>
        public static bool CBool(object value)
        {
            if (IsNull(value)) { return false; }

            if (value is string)
            {
                return CBool(CStr(value));
            }
            else
            {
                return Convert.ToBoolean(value);
            }
        }

        /// <summary>
        /// 轉型為 Guid 值。
        /// </summary>
        /// <param name="value">要轉型的值。</param>
        public static Guid CGuid(string value)
        {
            if (IsEmpty(value) || StrFunc.SameText(value, "*"))
                return Guid.Empty;
            else
            {
                var result = Guid.Empty;
                if (Guid.TryParse(value, out result))
                    return result;
                else
                    return result;
            }
        }

        /// <summary>
        /// 轉型為 Guid 值。
        /// </summary>
        /// <param name="value">要轉型的值。</param>
        public static Guid CGuid(object value)
        {
            if (IsEmpty(value))
                return Guid.Empty;
            else if (value is Guid)
                return (Guid)value;
            else if (value is string)
                return CGuid((string)value);
            else
                return Guid.Empty;
        }

        /// <summary>
        /// 字串轉型為布林值。
        /// </summary>
        /// <param name="value">要轉型的值。</param>
        public static bool CBool(string value)
        {
            if (StrFunc.SameText(value, "Y"))
                return true;
            else if (StrFunc.SameText(value, "T"))
                return true;
            else if (StrFunc.SameText(value, "true"))
                return true;
            else if (StrFunc.SameText(value, "1"))
                return true;
            else if (StrFunc.SameText(value, "是"))
                return true;
            else if (StrFunc.SameText(value, "真"))
                return true;
            else
                return false;
        }

        /// <summary>
        /// 轉型為布林值。
        /// </summary>
        /// <param name="value">要轉型的值。</param>
        /// <param name="defalutValue">列舉值為 Default 時的預設值。</param>
        public static bool CBool(EDefaultBoolean value, bool defalutValue)
        {
            switch (value)
            {
                case EDefaultBoolean.True:
                    return true;
                case EDefaultBoolean.False:
                    return false;
                default:
                    return defalutValue;
            }
        }

        /// <summary>
        /// 轉型為布林值。
        /// </summary>
        /// <param name="value">要轉型的值。</param>
        /// <param name="defalutValue">列舉值為 NotSet 時的預設值。</param>
        public static bool CBool(ENotSetBoolean value, bool defalutValue)
        {
            switch (value)
            {
                case ENotSetBoolean.True:
                    return true;
                case ENotSetBoolean.False:
                    return false;
                default:
                    return defalutValue;
            }
        }

        /// <summary>
        /// 轉型為列舉值。
        /// </summary>
        /// <param name="value">要轉型的值。</param>
        /// <param name="type">列舉型別。</param>
        public static object CEnum(string value, Type type)
        {
            return Enum.Parse(type, value, true);
        }

        /// <summary>
        /// 轉型為列舉值。
        /// </summary>
        /// <typeparam name="T">列舉型別。</typeparam>
        /// <param name="value">要轉型的值。</param>
        public static T CEnum<T>(string value)
        {
            return (T)CEnum(value, typeof(T));
        }

        /// <summary>
        /// 轉型為列舉值。
        /// </summary>
        /// <typeparam name="T">列舉型別。</typeparam>
        /// <param name="value">要轉型的值。</param>
        /// <param name="defaultValue">無法轉型成功的預設值。</param>
        public static T CEnum<T>(string value, object defaultValue)
        {
            try
            {
                return CEnum<T>(value);
            }
            catch
            {
                return (T)defaultValue;
            }
        }

        /// <summary>
        /// 轉型為列舉值。
        /// </summary>
        /// <typeparam name="T">列舉型別。</typeparam>
        /// <param name="value">要轉型的數值</param>
        /// <param name="defaultValue">無法轉型成功的預設值。</param>
        /// <returns></returns>
        public static T CEnum<T>(int value, object defaultValue)
        {
            try
            {
                T result = (T)defaultValue;
                for (int i = 0; i < Enum.GetNames(typeof(T)).Length; i++)
                {
                    string enumName = Enum.GetNames(typeof(T))[i];
                    int enumValue = (int)Enum.Parse(typeof(T), enumName);
                    if (enumValue == value)
                    {
                        result = (T)Enum.Parse(typeof(T), enumName);
                        break;
                    }
                }

                return result;
            }
            catch
            {
                return (T)defaultValue;
            }
        }

        /// <summary>
        /// 字串轉16進位字串。
        /// </summary>
        /// <param name="value">要轉的值。</param>
        public static string CHex(string value)
        {
            char[] oChars;
            string sReturnValue;

            if (StrFunc.StrIsEmpty(value)) return string.Empty;

            sReturnValue = string.Empty;
            oChars = value.ToCharArray();

            foreach (char letter in oChars)
            {
                sReturnValue += Convert.ToString(letter, 16);
            }
            return sReturnValue;
        }

        /// <summary>
        /// 判斷是否為數值。
        /// </summary>
        /// <param name="value">要判斷的值。</param>
        public static bool IsNumeric(object value)
        {
            Regex NumberPattern=new Regex("[^0-9.-]");
            return !NumberPattern.IsMatch(CStr(value));
        }

        /// <summary>
        /// 轉型為數值。
        /// </summary>
        /// <param name="value">要轉型的值。</param>
        public static object CNum(object value)
        {
            // Null 傳回 0
            if (IsNull(value)) { return 0; }
            // 針對文字型別做處理
            if (value is string)
            {
                // 空字串傳回 0
                if (StrFunc.StrIsEmpty(value)) { return 0; }
                // 若為數值先轉型為 Double 型別，防止有小數點無法轉型為整數
                if (BaseFunc.IsNumeric(value)) { value = Convert.ToDouble(value); }
            }

            if (!BaseFunc.IsNumeric(value))
                throw new System.Exception(value + " 無法轉型為數值");

            return value;
        }

        /// <summary>
        /// 轉型為整數。
        /// </summary>
        /// <param name="value">要轉型的值。</param>
        public static int CInt(object value)
        {
            return Convert.ToInt32(CNum(value));
        }

        /// <summary>
        /// 轉型為長整數。
        /// </summary>
        /// <param name="value">要轉型的值。</param>
        public static long CLong(object value)
        {
            return Convert.ToInt64(CNum(value));
        }

        /// <summary>
        /// 轉型為單精確浮點數。
        /// </summary>
        /// <param name="value">要轉型的值。</param>
        public static float CFloat(object value)
        {
            return Convert.ToSingle(CNum(value));
        }

        /// <summary>
        /// 轉型為雙精確浮點數。
        /// </summary>
        /// <param name="value">要轉型的值。</param>
        public static double CDouble(object value)
        {
            return Convert.ToDouble(CNum(value));
        }

        /// <summary>
        /// 轉型為 Decimal 浮點數。
        /// </summary>
        /// <param name="value">要轉型的值。</param>
        public static decimal CDecimal(object value)
        {
            return Convert.ToDecimal(CNum(value));
        }

        /// <summary>
        /// 轉型為日期值。
        /// </summary>
        /// <param name="value">要轉型的值。</param>
        public static DateTime CDateTime(object value)
        {
            try
            {
                if (value is DateTime)
                    return (DateTime)value;
                else if (value is string)
                {
                    var provider = CultureInfo.InvariantCulture;
                    var style = DateTimeStyles.None;
                    var a = DateTime.TryParseExact(value as string, "yyyy/MM/dd HH:mm:ss", provider, style, out DateTime result);
                    if (a)
                        return result;
                    else
                        return Convert.ToDateTime(value as string);
                }
                else
                    return Convert.ToDateTime(value);
                
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// 轉型成時間間格值
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static TimeSpan CTimeSpan(string time)
        {
            var result = new TimeSpan();

            if (StrFunc.StrIsNotEmpty(time))
            {
                int hours = BaseFunc.CInt(StrFunc.StrLeft(time, 2));
                int minutes = BaseFunc.CInt(StrFunc.StrRight(time, 2));
                result = new TimeSpan(hours, minutes, 0);
            }

            return result;
        }

        #endregion

        /// <summary>
        /// 取得列舉成員的名稱。
        /// </summary>
        /// <param name="value">列舉值。</param>
        public static string GetEnumName(Enum value)
        {
            return Enum.GetName(value.GetType(), value);
        }

        /// <summary>
        /// 判斷列舉值是否有指定成員。
        /// </summary>
        /// <param name="value">列舉值。</param>
        /// <param name="flag">要判斷的成員。</param>
        public static bool HasFlag(Enum value, Enum flag)
        {
            return value.HasFlag(flag);
        }

        /// <summary>
        /// 判斷列舉值是否有指定成員之一。
        /// </summary>
        /// <param name="value">列舉值。</param>
        /// <param name="flag1">要判斷的成員一。</param>
        /// <param name="flag2">要判斷的成員二。</param>
        public static bool HasFlag(Enum value, Enum flag1, Enum flag2)
        {
            if (HasFlag(value, flag1) || HasFlag(value, flag2))
                return true;
            else
                return false;
        }

        /// <summary>
        /// 產生新的 Guid 值。
        /// </summary>
        public static System.Guid NewGuid()
        {
            return System.Guid.NewGuid();
        }

        /// <summary>
        /// 產生新的 Guid 值字串。
        /// </summary>
        public static string NewGuidString()
        {
            return NewGuid().ToString();
        }

        /// <summary>
        /// 以預設建構函式，建立指定類別的執行個體。
        /// </summary>
        /// <typeparam name="T">要建立執行個體的型別。</typeparam>
        public static T CreateInstance<T>()
        {
            return (T)Activator.CreateInstance(typeof(T));
        }

        /// <summary>
        /// 若傳入值為 Null 時，則建立指定類別的執行個體。
        /// </summary>
        /// <typeparam name="T">要建立執行個體的型別。</typeparam>
        /// <param name="value">要判斷的執行個體。</param>
        public static void NullCreateInstance<T>(ref T value)
        {
            if (IsNull(value))
                value = CreateInstance<T>();
        }

        /// <summary>
        /// 將 Byte 陣列轉換為十六進位字串。
        /// </summary>
        /// <param name="buffer">Byte 陣列。</param>
        public static string ByteToHex(byte[] buffer)
        {
            StringBuilder oBuffer;

            oBuffer = new StringBuilder();
            for (int i = 0; i < buffer.Length; i++)
            {
                oBuffer.Append(buffer[i].ToString("X2")); // hex format
            }
            return oBuffer.ToString();
        }

        /// <summary>
        /// 取得下一個流水號。
        /// </summary>
        /// <param name="value">目前編號。</param>
        /// <param name="numberBase">流水號進位基底，2至36進位。</param>
        public static string GetNextID(string value, int numberBase)
        {
            string sValue;
            char[] oBaseValues;
            int iBaseValue;

            oBaseValues = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            sValue = StrFunc.StrTrim(value);

            for (int N1 = sValue.Length - 1; N1 >= 0; N1--)
            {
                iBaseValue = Array.IndexOf(oBaseValues, sValue[N1]);
                if (iBaseValue != -1 && iBaseValue < numberBase - 1)
                {
                    return string.Format("{0}{1}{2}", StrFunc.StrLeft(sValue, N1), oBaseValues[iBaseValue + 1], StrFunc.StrSubstring(sValue, N1 + 1));
                }
                else
                {
                    // 需要進位，將目前位數歸零
                    if (iBaseValue != -1 && iBaseValue < numberBase)
                    {
                        sValue = string.Format("{0}{1}{2}", StrFunc.StrLeft(sValue, N1), "0", StrFunc.StrSubstring(sValue, N1 + 1));
                    }
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 取得下一個流水號。
        /// </summary>
        /// <param name="value">目前編號。</param>
        /// <param name="baseValues">編碼基底字串。</param>
        public static string GetNextID(string value, string baseValues)
        {
            string sValue;
            char[] oBaseValues;
            int iCount;
            int iIndex;

            oBaseValues = baseValues.ToCharArray();
            iCount = oBaseValues.Length;
            sValue = StrFunc.StrTrim(value);

            for (int N1 = sValue.Length - 1; N1 >= 0; N1--)
            {
                iIndex = Array.IndexOf(oBaseValues, sValue[N1]);
                if (iIndex != -1 && iIndex < iCount - 1)
                {
                    return string.Format("{0}{1}{2}", StrFunc.StrLeft(sValue, N1), oBaseValues[iIndex + 1], StrFunc.StrSubstring(sValue, N1 + 1));
                }
                else
                {
                    // 需要進位，將目前位數歸零
                    if (iIndex != -1 && iIndex < iCount)
                    {
                        sValue = string.Format("{0}{1}{2}", StrFunc.StrLeft(sValue, N1), oBaseValues[0], StrFunc.StrSubstring(sValue, N1 + 1));
                        if (N1 == 0)
                            return oBaseValues[0] + sValue;
                    }
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// 驗證Email格式正確性
        /// </summary>
        /// <param name="email">email</param>
        /// <returns></returns>
        public static bool ValidateEmailFormat(string email)
        {
            Match match = Regex.Match(email, "^[\\w-]+@[\\w-]+\\.(com|net|org|edu|mil|tv|biz|info)$");
            return match.Success;
        }

        /// <summary>
        /// 隨機取得一個整數值。
        /// </summary>
        /// <param name="min">最小值。</param>
        /// <param name="max">最大值。</param>
        public static int RndInt(int min, int max)
        {
            Random oRnd = new Random(Guid.NewGuid().GetHashCode());
            int iValue = 0;

            iValue = BaseFunc.CInt(oRnd.Next(min, max));
            return iValue;
        }

        /// <summary>
        /// 由參考字串中隨機取得一個字元。
        /// </summary>
        /// <param name="s">參考字串。</param>
        public static string RndWord(string s)
        {
            int iIndex = 0;

            iIndex = RndInt(1, s.Length);
            return StrFunc.StrSubstring(s, iIndex, 1);
        }

        /// <summary>
        /// 由參考字串中隨機取得一組指定長度字串。
        /// </summary>
        /// <param name="s">參考字串。</param>
        /// <param name="length">隨機字串的長度。</param>
        public static string RndString(string s, int length)
        {
            StringBuilder oBuffer = default(StringBuilder);
            int N1 = 0;

            oBuffer = new StringBuilder();
            for (N1 = 1; N1 <= length; N1++)
            {
                oBuffer.Append(RndWord(s));
            }
            return oBuffer.ToString();
        }

        /// <summary>
        /// 取得亂數產生的英文字母大小寫與數字組合的字串
        /// </summary>
        /// <param name="length">要產生的亂數字串長度</param>
        /// <returns></returns>
        public static string GetRandomString(int length)
        {
            string s = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            string code = RndString(s, length);
            return code;
        }

        /// <summary>
        /// 是否為 Null 或 DBNull 值。
        /// </summary>
        /// <param name="value">要判斷的值。</param>
        public static bool IsNullOrDBNull(object value)
        {
            if (IsNull(value) || IsDBNull(value))
                return true;
            else
                return false;
        }

        /// <summary>
        /// 將傳入值轉型為指定欄位資料型別。
        /// </summary>
        /// <param name="fieldDbType">欄位資料型別。</param>
        /// <param name="value">傳入值。</param>
        public static object CFieldValue(EFieldDbType fieldDbType, object value)
        {
            switch (fieldDbType)
            {
                case EFieldDbType.String:
                    return BaseFunc.CStr(value).Trim();  // 去除左右空白
                case EFieldDbType.Text:
                    return BaseFunc.CStr(value).Trim();  // 去除左右空白
                case EFieldDbType.Boolean:
                    return BaseFunc.CBool(value);
                case EFieldDbType.Integer:
                    return BaseFunc.CInt(value);
                case EFieldDbType.Double:
                    return BaseFunc.CDouble(value);
                case EFieldDbType.Currency:
                    return BaseFunc.CDecimal(value);
                case EFieldDbType.DateTime:
                    if (BaseFunc.IsDBNull(value))
                        return value;
                    else
                        return BaseFunc.CDateTime(value);
                case EFieldDbType.GUID:
                    return BaseFunc.CGuid(value);
                default:
                    return value;
            }
        }

        /// <summary>
        /// 將傳入值轉型為指定欄位資料型別。
        /// </summary>
        /// <param name="fieldDbType">欄位資料型別。</param>
        /// <param name="value">傳入值。</param>
        /// <param name="defaultValue">無法正確轉型的預設值。</param>
        public static object CFieldValue(EFieldDbType fieldDbType, object value, object defaultValue)
        {
            try
            {
                return CFieldValue(fieldDbType, value);
            }
            catch
            {
                return defaultValue;
            }
        }
    }
}
