using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Xml.Linq;
using System.Linq;

namespace Gat.Base
{
    /// <summary>
    /// 擴充方法
    /// </summary>
    public static class ExtensionMethod
    {
        #region XElement

        /// <summary>
        /// 取得XElement > XElement的值
        /// </summary>
        /// <param name="elem"></param>
        /// <param name="elemName"></param>
        /// <returns></returns>
        public static string ElementValue(this XElement elem, XName elemName)
        {
            if (BaseFunc.IsNotNull(elem.Element(elemName)))
                return elem.Element(elemName).Value;
            else
                return "";
        }

        /// <summary>
        /// 取得符合指定巢狀結構的XElement
        /// </summary>
        /// <param name="elem"></param>
        /// <param name="names"></param>
        /// <returns></returns>
        public static XElement NestElement(this XElement elem, params XName[] names)
        {
            XElement result = elem;
            foreach (var name in names)
            {
                result = result.Element(name);
                if (BaseFunc.IsNull(result))
                    break;
            }

            return result;
        }

        #endregion

        #region JObject

        /// <summary>
        /// 取得指定名稱的字串
        /// </summary>
        /// <param name="source"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static string TokenAsString(this JObject source, string propertyName)
        {
            var value = source.TokenValue(propertyName);
            if (BaseFunc.IsNull(value))
                return "";
            else
                return value.ToString();
        }

        /// <summary>
        /// 取得指定名稱的整數
        /// </summary>
        /// <param name="source"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static int TokenAsInt(this JObject source, string propertyName)
        {
            var value = source.TokenValue(propertyName);
            if (BaseFunc.IsNull(value))
                return 0;
            else
                return BaseFunc.CInt(value.ToString());
        }

        /// <summary>
        /// 取得指定名稱的雙精確浮點數
        /// </summary>
        /// <param name="source"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static double TokenAsDouble(this JObject source, string propertyName)
        {
            var value = source.TokenValue(propertyName);
            if (BaseFunc.IsNull(value))
                return 0;
            else
                return BaseFunc.CDouble(value.ToString());
        }

        /// <summary>
        /// 取得指定名稱的日期時間值
        /// </summary>
        /// <param name="source"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static DateTime TokenAsDateTime(this JObject source, string propertyName)
        {
            var value = source.TokenValue(propertyName);
            if (BaseFunc.IsNull(value))
                return DateTime.MinValue;
            else
                return BaseFunc.CDateTime(value.ToString());
        }

        /// <summary>
        /// 取得指定名稱的布林值
        /// </summary>
        /// <param name="source"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static bool TokenAsBool(this JObject source, string propertyName)
        {
            var value = source.TokenValue(propertyName);
            if (BaseFunc.IsNull(value))
                return false;
            else
                return BaseFunc.CBool(value.ToString());
        }

        /// <summary>
        /// 取得指定名稱的Json物件
        /// </summary>
        /// <param name="source"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static JObject TokenAsJson(this JObject source, string propertyName)
        {
            var value = source.TokenValue(propertyName);
            if (BaseFunc.IsNull(value))
                return null;
            else
                return value as JObject;
        }

        /// <summary>
        /// 取得指定名稱的Json陣列
        /// </summary>
        /// <param name="source"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static JArray TokenAsJArray(this JObject source, string propertyName)
        {
            var value = source.TokenValue(propertyName);
            if (BaseFunc.IsNull(value))
                return null;
            else
                return value as JArray;
        }

        /// <summary>
        /// 檢查Jobject是否存在指定Property Name
        /// </summary>
        /// <param name="source"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static bool IsExists(this JObject source, string propertyName)
        {
            var value = source.TokenValue(propertyName);
            return value != null;
        }

        /// <summary>
        /// 檢查Jobject是否存在指定Property Name
        /// </summary>
        /// <param name="source"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static JToken TokenValue(this JObject source, string propertyName)
        {
            var value = source.Properties().FirstOrDefault(x => x.Name.SameText(propertyName));
            if (value != null)
                return value.Value;
            else
                return null;
        }

        #endregion

        #region string

        /// <summary>
        /// 將字串前後加上單引號
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static string SQLStr(this string source)
        {
            if (StrFunc.StrIsNotEmpty(source))
                return StrFunc.SQLStr(source);
            else
                return "''";
        }

        /// <summary>
        /// 將字串依分隔符號拆解成陣列。
        /// </summary>
        /// <param name="s"></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        public static string[] StrSplit(this string s, string delimiter = BaseConst.DefaultDelimiter)
        {
            return StrFunc.StrSplit(s, delimiter);
        }

        /// <summary>
        /// 依分隔符號合併二個字串。
        /// </summary>
        /// <param name="s1">第一個字串。</param>
        /// <param name="s2">第二個字串。</param>
        /// <param name="delimiter">分隔符號。</param>
        /// <returns></returns>
        public static string StrMerge(this string s1, string s2, string delimiter)
        {
            return StrFunc.StrMerge(s1, s2, delimiter);
        }

        /// <summary>
        /// 判斷二個字串是否相等(忽略大小寫)。
        /// </summary>
        /// <param name="s1">第一個字串。</param>
        /// <param name="s2">第二個字串。</param>
        public static bool SameText(this string s1, string s2)
        {
            return StrFunc.SameText(s1, s2);
        }

        /// <summary>
        /// 判斷傳入字串是否等於陣列中任一成員。
        /// </summary>
        /// <param name="s">字串。</param>
        /// <param name="args">以逗點分隔的字串集合。</param>
        public static bool SameTextOr(this string s, string args)
        {
            return StrFunc.SameTextOr(s, args);
        }

        /// <summary>
        /// 判斷傳入字串是否等於陣列中任一成員。
        /// </summary>
        /// <param name="s">字串。</param>
        /// <param name="args">字串陣列。</param>
        public static bool SameTextOr(this string s, IEnumerable<string> args)
        {
            foreach (string value in args)
            {
                if (SameText(s, value))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 判斷傳入字串是否等於陣列中任一成員。
        /// </summary>
        /// <param name="s">字串。</param>
        /// <param name="args">字串陣列。</param>
        public static bool SameTextOr(this string s, params string[] args)
        {
            foreach (string value in args)
            {
                if (SameText(s, value))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// 判斷是否為空字串，若為 null 也會視為空字串。
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsEmpty(this string s)
        {
            return StrFunc.StrIsEmpty(s);
        }

        #endregion

        #region DataRow

        /// <summary>
        /// 判斷是否有指定欄位。
        /// </summary>
        /// <param name="row">資料列。</param>
        /// <param name="fieldName">欄位名稱。</param>
        public static bool HasField(this DataRow row, string fieldName)
        {
            return DataFunc.HasField(row, fieldName);
        }

        /// <summary>
        /// 取得欄位值後轉型成int
        /// </summary>
        /// <param name="row">資料列</param>
        /// <param name="fieldName">欄位名稱</param>
        /// <returns></returns>
        public static int ValueAsInt(this DataRow row, string fieldName)
        {
            if (BaseFunc.IsNotNull(row) && DataFunc.HasField(row, fieldName))
                return BaseFunc.CInt(row[fieldName]);
            return 0;
        }

        /// <summary>
        /// 取得欄位值後轉型成double
        /// </summary>
        /// <param name="row">資料列</param>
        /// <param name="fieldName">欄位名稱</param>
        /// <returns></returns>
        public static double ValueAsDouble(this DataRow row, string fieldName)
        {
            if (BaseFunc.IsNotNull(row) && DataFunc.HasField(row, fieldName))
                return BaseFunc.CDouble(row[fieldName]);
            return 0;
        }

        /// <summary>
        /// 取得欄位值後轉型成float
        /// </summary>
        /// <param name="row">資料列</param>
        /// <param name="fieldName">欄位名稱</param>
        /// <returns></returns>
        public static float ValueAsFloat(this DataRow row, string fieldName)
        {
            if (BaseFunc.IsNotNull(row) && DataFunc.HasField(row, fieldName))
                return BaseFunc.CFloat(row[fieldName]);
            return 0;
        }

        /// <summary>
        /// 取得欄位值後轉型成decimal
        /// </summary>
        /// <param name="row"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public static decimal ValueAsDecimal(this DataRow row, string fieldName)
        {
            if (BaseFunc.IsNotNull(row) && DataFunc.HasField(row, fieldName))
                return BaseFunc.CDecimal(row[fieldName]);
            return 0;
        }

        /// <summary>
        /// 取得欄位值後轉型成string
        /// </summary>
        /// <param name="row">資料列</param>
        /// <param name="fieldName">欄位名稱</param>
        /// <returns></returns>
        public static string ValueAsString(this DataRow row, string fieldName)
        {
            if (BaseFunc.IsNotNull(row) && DataFunc.HasField(row, fieldName))
                return BaseFunc.CStr(row[fieldName]);
            return "";
        }

        /// <summary>
        /// 取得欄位值後轉型成bool
        /// </summary>
        /// <param name="row">資料列</param>
        /// <param name="fieldName">欄位名稱</param>
        /// <returns></returns>
        public static bool ValueAsBool(this DataRow row, string fieldName)
        {
            if (BaseFunc.IsNotNull(row) && DataFunc.HasField(row, fieldName))
                return BaseFunc.CBool(row[fieldName]);
            return false;
        }

        /// <summary>
        /// 取得欄位值後轉型成DateTime
        /// </summary>
        /// <param name="row">資料列</param>
        /// <param name="fieldName">欄位名稱</param>
        /// <returns></returns>
        public static DateTime ValueAsDateTime(this DataRow row, string fieldName)
        {
            if (BaseFunc.IsNotNull(row) && DataFunc.HasField(row, fieldName))
                return BaseFunc.CDateTime(row[fieldName]);
            return default(DateTime);
        }

        /// <summary>
        /// 取得欄位值後轉型成Guid
        /// </summary>
        /// <param name="row">資料列</param>
        /// <param name="fieldName">欄位名稱</param>
        /// <returns></returns>
        public static Guid ValueAsGuid(this DataRow row, string fieldName)
        {
            if (BaseFunc.IsNotNull(row) && DataFunc.HasField(row, fieldName))
                return BaseFunc.CGuid(row[fieldName]);
            return Guid.Empty;
        }

        /// <summary>
        /// 取得欄位值後轉型成int。
        /// </summary>
        /// <param name="row">資料列。</param>
        /// <param name="fieldName">欄位名稱。</param>
        /// <param name="dataRowVersion">資料列版本。</param>
        public static int ValueAsInt(this DataRow row, string fieldName, DataRowVersion dataRowVersion)
        {
            if (BaseFunc.IsNotNull(row) && DataFunc.HasField(row, fieldName))
                return BaseFunc.CInt(row[fieldName, dataRowVersion]);
            return 0;
        }

        /// <summary>
        /// 取得欄位值後轉型成double。
        /// </summary>
        /// <param name="row">資料列。</param>
        /// <param name="fieldName">欄位名稱。</param>
        /// <param name="dataRowVersion">資料列版本。</param>
        public static double ValueAsDouble(this DataRow row, string fieldName, DataRowVersion dataRowVersion)
        {
            if (BaseFunc.IsNotNull(row) && DataFunc.HasField(row, fieldName))
                return BaseFunc.CDouble(row[fieldName, dataRowVersion]);
            return 0;
        }

        /// <summary>
        /// 取得欄位值後轉型成float。
        /// </summary>
        /// <param name="row">資料列。</param>
        /// <param name="fieldName">欄位名稱。</param>
        /// <param name="dataRowVersion">資料列版本。</param>
        public static float ValueAsFloat(this DataRow row, string fieldName, DataRowVersion dataRowVersion)
        {
            if (BaseFunc.IsNotNull(row) && DataFunc.HasField(row, fieldName))
                return BaseFunc.CFloat(row[fieldName, dataRowVersion]);
            return 0;
        }

        /// <summary>
        /// 取得欄位值後轉型成decimal。
        /// </summary>
        /// <param name="row">資料列。</param>
        /// <param name="fieldName">欄位名稱。</param>
        /// <param name="dataRowVersion">資料列版本。</param>
        public static decimal ValueAsDecimal(this DataRow row, string fieldName, DataRowVersion dataRowVersion)
        {
            if (BaseFunc.IsNotNull(row) && DataFunc.HasField(row, fieldName))
                return BaseFunc.CDecimal(row[fieldName, dataRowVersion]);
            return 0;
        }

        /// <summary>
        /// 取得欄位值後轉型成string。
        /// </summary>
        /// <param name="row">資料列。</param>
        /// <param name="fieldName">欄位名稱。</param>
        /// <param name="dataRowVersion">資料列版本。</param>
        public static string ValueAsString(this DataRow row, string fieldName, DataRowVersion dataRowVersion)
        {
            if (BaseFunc.IsNotNull(row) && DataFunc.HasField(row, fieldName))
                return BaseFunc.CStr(row[fieldName, dataRowVersion]);
            return "";
        }

        /// <summary>
        /// 取得欄位值後轉型成bool。
        /// </summary>
        /// <param name="row">資料列。</param>
        /// <param name="fieldName">欄位名稱。</param>
        /// <param name="dataRowVersion">資料列版本。</param>
        public static bool ValueAsBool(this DataRow row, string fieldName, DataRowVersion dataRowVersion)
        {
            if (BaseFunc.IsNotNull(row) && DataFunc.HasField(row, fieldName))
                return BaseFunc.CBool(row[fieldName, dataRowVersion]);
            return false;
        }

        /// <summary>
        /// 取得欄位值後轉型成DateTime。
        /// </summary>
        /// <param name="row">資料列。</param>
        /// <param name="fieldName">欄位名稱。</param>
        /// <param name="dataRowVersion">資料列版本。</param>
        public static DateTime ValueAsDateTime(this DataRow row, string fieldName, DataRowVersion dataRowVersion)
        {
            if (BaseFunc.IsNotNull(row) && DataFunc.HasField(row, fieldName))
                return BaseFunc.CDateTime(row[fieldName, dataRowVersion]);
            return default(DateTime);
        }

        /// <summary>
        /// 取得欄位值後轉型成Guid。
        /// </summary>
        /// <param name="row">資料列。</param>
        /// <param name="fieldName">欄位名稱。</param>
        /// <param name="dataRowVersion">資料列版本。</param>
        public static Guid ValueAsGuid(this DataRow row, string fieldName, DataRowVersion dataRowVersion)
        {
            if (BaseFunc.IsNotNull(row) && DataFunc.HasField(row, fieldName))
                return BaseFunc.CGuid(row[fieldName, dataRowVersion]);
            return Guid.Empty;
        }

        /// <summary>
        /// 將字串前後加上單引號。
        /// </summary>
        /// <param name="row">資料列。</param>
        /// <param name="fieldName">欄位名稱。</param>
        /// <returns></returns>
        public static string ValueAsSQLStr(this DataRow row, string fieldName)
        {
            return row.ValueAsString(fieldName).SQLStr();
        }

        #endregion

        #region DataTable

        /// <summary>
        /// DataTable Where 擴充函式
        /// </summary>
        /// <param name="table"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static IEnumerable<DataRow> Where(this DataTable table, Func<DataRow, bool> keySelector)
        {
            foreach (var row in table.Select())
            {
                if (keySelector(row))
                    yield return row;
            }
        }

        #endregion

        #region DateTime

        /// <summary>
        /// 日期轉為日期格式文字
        /// </summary>
        /// <param name="datetime">日期</param>
        /// <returns></returns>
        public static string ToDateText(this DateTime datetime)
        {
            return datetime.ToString(BaseConst.DateFormat);
        }

        /// <summary>
        /// 日期轉為日期格式文字，包含斜線
        /// </summary>
        /// <param name="datetime">日期</param>
        /// <returns></returns>
        public static string ToDateSlashText(this DateTime datetime)
        {
            return datetime.ToString(BaseConst.DateSlashFormat);
        }

        /// <summary>
        /// 日期轉為不包含年度的日期格式文字
        /// </summary>
        /// <param name="datetime">日期</param>
        /// <returns></returns>
        public static string ToDateOnlyText(this DateTime datetime)
        {
            return datetime.ToString("MMdd");
        }

        /// <summary>
        /// 日期轉為不包含年度的日期格式文字，包含斜線
        /// </summary>
        /// <param name="datetime">日期</param>
        /// <returns></returns>
        public static string ToDateOnlySlashText(this DateTime datetime)
        {
            return datetime.ToString("MM/dd");
        }

        /// <summary>
        /// 日期轉為年月格式文字
        /// </summary>
        /// <param name="datetime">日期</param>
        /// <returns></returns>
        public static string ToYearMonthText(this DateTime datetime)
        {
            return datetime.ToString("yyyyMM");
        }

        /// <summary>
        /// 日期轉為年月格式文字，包含斜線
        /// </summary>
        /// <param name="datetime">日期</param>
        /// <returns></returns>
        public static string ToYearMonthSlashText(this DateTime datetime)
        {
            return datetime.ToString("yyyy/MM");
        }

        /// <summary>
        /// 日期轉為日期時間格式文字
        /// </summary>
        /// <param name="datetime">日期</param>
        /// <returns></returns>
        public static string ToDateTimeText(this DateTime datetime)
        {
            return datetime.ToString(BaseConst.DateTimeFormat);
        }

        /// <summary>
        /// 日期轉為日期時間格式文字，包含斜線
        /// </summary>
        /// <param name="datetime">日期</param>
        /// <returns></returns>
        public static string ToDateTimeSlashText(this DateTime datetime)
        {
            return datetime.ToString(BaseConst.DateTimeSlashFormat);
        }

        /// <summary>
        /// 日期轉為時分秒時間格式文字
        /// </summary>
        /// <param name="datetime">日期</param>
        /// <returns></returns>
        public static string ToTimeText(this DateTime datetime)
        {
            return datetime.ToString(BaseConst.TimeFormat);
        }

        /// <summary>
        /// 日期轉為時分秒時間格式文字，包含冒號
        /// </summary>
        /// <param name="datetime">日期</param>
        /// <returns></returns>
        public static string ToTimeColonText(this DateTime datetime)
        {
            return datetime.ToString(BaseConst.TimecClonFormat);
        }

        /// <summary>
        /// 日期轉為時分時間格式文字
        /// </summary>
        /// <param name="datetime">日期</param>
        /// <returns></returns>
        public static string ToHourMinText(this DateTime datetime)
        {
            return datetime.ToString(BaseConst.HourMinFormat);
        }

        /// <summary>
        /// 日期轉為時分時間格式文字，包含冒號
        /// </summary>
        /// <param name="datetime">日期</param>
        /// <returns></returns>
        public static string ToHourMinColonText(this DateTime datetime)
        {
            return datetime.ToString(BaseConst.HourMinClonFormat);
        }

        /// <summary>
        /// 是否為空值。
        /// </summary>
        /// <param name="dateTime">日期。</param>        
        public static bool IsEmpty(this DateTime dateTime)
        {
            return BaseFunc.IsEmpty(dateTime);
        }

        /// <summary>
        /// 檢查日期是否在兩個日期區間之間
        /// </summary>
        /// <param name="dateTime">日期。</param>
        /// <param name="minDate">開始日期</param>
        /// <param name="maxDate">結束日期</param>
        /// <returns></returns>
        public static bool Between(this DateTime dateTime, DateTime minDate, DateTime maxDate)
        {
            return dateTime >= minDate && dateTime <= maxDate;
        }

        #endregion

        #region int

        /// <summary>
        /// 數值是否與傳入數值陣列中有相同的部分
        /// </summary>
        /// <param name="source">數值</param>
        /// <param name="values">比較數值陣列</param>
        /// <returns></returns>
        public static bool In(this int source, params int[] values)
        {
            return (values.Any(x => x == source));
        }

        /// <summary>
        /// 數值是否介於兩個數值之間
        /// </summary>
        /// <param name="source">數值</param>
        /// <param name="min">最小數值</param>
        /// <param name="max">最大數值</param>
        /// <returns></returns>
        public static bool Between(this int source, int min, int max)
        {
            return (source >= min && source <= max);
        }

        #endregion
    }
}
