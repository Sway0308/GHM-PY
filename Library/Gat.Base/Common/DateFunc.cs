using System;
using Microsoft.VisualBasic;
using System.Globalization;

namespace Gat.Base
{
    /// <summary>
    /// 時間日期相關函式庫。
    /// </summary>
    public class DateFunc
    {
        /// <summary>
        /// 傳回目前時間。
        /// </summary>
        public static DateTime Now()
        {
            return DateTime.Now;
        }

        /// <summary>
        /// 傳回今天日期。
        /// </summary>
        public static DateTime Today()
        {
            return DateTime.Today;
        }

        /// <summary>
        /// 判斷傳入值是否為日期格式。
        /// </summary>
        /// <param name="value">要判斷的值。</param>
        public static bool IsDate(object value)
        {
            return DateTime.TryParse(value?.ToString(), out DateTime dateTime);
        }

        /// <summary>
        /// 轉換為 DateTime 型別。
        /// </summary>
        /// <param name="date">日期字串。</param>
        /// <param name="time">時間字串。</param>
        public static DateTime ToDateTime(string date, string time)
        {
            string sValue;
            string sTime;

            sTime = time;
            // 若時間是四碼或六碼數值，則加上時間冒號
            if (BaseFunc.IsNumeric(sTime))
            {
                if (StrFunc.StrLen(sTime) == 4)
                    sTime = string.Format("{0}:{1}", StrFunc.StrLeft(sTime, 2), StrFunc.StrSubstring(sTime, 2, 2));
                else if (StrFunc.StrLen(sTime) == 6)
                    sTime = string.Format("{0}:{1}:{2}", StrFunc.StrLeft(sTime, 2), StrFunc.StrSubstring(sTime, 2, 2), StrFunc.StrSubstring(sTime, 4, 2));
            }

            sValue = string.Format("{0} {1}", date, sTime);
            if (IsDate(sValue))
                return BaseFunc.CDateTime(sValue);
            else
                return DateTime.MinValue;
        }

        /// <summary>
        /// 轉換為 DateTime 型別。
        /// </summary>
        /// <param name="date">日期格式。</param>
        /// <param name="time">時間字串。</param>
        public static DateTime ToDateTime(DateTime date, string time)
        {
            return ToDateTime(date.ToString("yyyy/MM/dd"), time);
        }

        /// <summary>
        /// 轉換為 DateTime 型別。
        /// </summary>
        /// <param name="date">日期格式。</param>
        /// <param name="time">時間字串。</param>
        /// <param name="addColonToTime">時間需加入冒號。</param>
        public static DateTime ToDateTime(DateTime date, string time, bool addColonToTime)
        {
            if (addColonToTime)
            {
                time = time.PadRight(6, '0')
                           .Insert(2, ":")
                           .Insert(5, ":");
            }

            return ToDateTime(date.ToString("yyyy/MM/dd"), time);
        }

        /// <summary>
        /// 判斷兩組日期區間是否交疊。(True 為交疊)
        /// </summary>
        /// <param name="beginDate1">第一組的開始時間。</param>
        /// <param name="endDate1">第一組的結束時間。</param>
        /// <param name="beginDate2">第二組的開始時間。</param>
        /// <param name="endDate2">第二組的結束時間。</param>
        public static bool IsOverlap(DateTime beginDate1, DateTime endDate1, DateTime beginDate2, DateTime endDate2)
        {
            return ((endDate2.CompareTo(beginDate1)) > 0 && (endDate1.CompareTo(beginDate2) > 0));
        }

        /// <summary>
        /// 更新日期時間值的時間部分。
        /// </summary>
        /// <param name="value">日期時間值。</param>
        /// <param name="timeValue">要異動時間部分的參考值。</param>
        public static DateTime UpdateTime(DateTime value, DateTime timeValue)
        {
            return new DateTime(value.Year, value.Month, value.Day, timeValue.Hour, timeValue.Minute, timeValue.Second);
        }

        /// <summary>
        /// 更新日期時間值的時間部分。
        /// </summary>
        /// <param name="value">日期時間值。</param>
        /// <param name="timeValue">要異動時間部分的時間字串。</param>
        public static DateTime UpdateTime(DateTime value, string timeValue)
        {
            DateTime oTime;

            if (IsDate(timeValue))
                oTime = BaseFunc.CDateTime(timeValue);
            else
                oTime = DateTime.MinValue;

            return UpdateTime(value, oTime);
        }

        /// <summary>
        /// 更新日期時間值的日期部分。
        /// </summary>
        /// <param name="value">日期時間值。</param>
        /// <param name="dateValue">要異動日期部分的參考值。</param>
        public static DateTime UpdateDate(DateTime value, DateTime dateValue)
        {
            return new DateTime(dateValue.Year, dateValue.Month, dateValue.Day, value.Hour, value.Minute, value.Second);
        }

        /// <summary>
        /// 指定時間加上指定天數。
        /// </summary>
        /// <param name="value">指定時間。</param>
        /// <param name="days">累加天數。</param>
        public static DateTime AddDays(DateTime value, int days)
        {
            return value.AddDays(days);
        }

        /// <summary>
        /// 計算兩個日期間的總天數
        /// </summary>
        /// <param name="date1">第一個日期時間值。</param>
        /// <param name="date2">第二個日期時間值。</param>
        /// <returns></returns>
        public static double TotalDays(DateTime date1, DateTime date2)
        {
            return (date2 - date1).TotalDays;
        }

        /// <summary>
        /// 計算兩個日期間的總時數
        /// </summary>
        /// <param name="date1">第一個日期時間值。</param>
        /// <param name="date2">第二個日期時間值。</param>
        /// <returns></returns>
        public static double TotalHours(DateTime date1, DateTime date2)
        {
            return (date2 - date1).TotalHours;
        }

        /// <summary>
        /// 計算兩個日期間的總毫秒數
        /// </summary>
        /// <param name="date1">第一個日期時間值。</param>
        /// <param name="date2">第二個日期時間值。</param>
        /// <returns></returns>
        public static double TotalMilliseconds(DateTime date1, DateTime date2)
        {
            return (date2 - date1).TotalMilliseconds;
        }

        /// <summary>
        /// 計算兩個日期間的總分鐘數
        /// </summary>
        /// <param name="date1">第一個日期時間值。</param>
        /// <param name="date2">第二個日期時間值。</param>
        /// <returns></returns>
        public static double TotalMinutes(DateTime date1, DateTime date2)
        {
            return (date2 - date1).TotalMinutes;
        }

        /// <summary>
        /// 計算兩個日期間的總秒數
        /// </summary>
        /// <param name="date1">第一個日期時間值。</param>
        /// <param name="date2">第二個日期時間值。</param>
        /// <returns></returns>
        public static double TotalSeconds(DateTime date1, DateTime date2)
        {
            return (date2 - date1).TotalSeconds;
        }

        /// <summary>
        /// 取得指定年月的該月天數。
        /// </summary>
        /// <param name="year">年月。</param>
        /// <param name="month">月份。</param>
        public static int DaysInMonth(int year, int month)
        {
            return DateTime.DaysInMonth(year, month);
        }

        /// <summary>
        /// 取得基準日期的該月天數。
        /// </summary>
        /// <param name="value">基準日期。</param>
        public static int DaysInMonth(DateTime value)
        {
            return DaysInMonth(value.Year, value.Month);
        }

        /// <summary>
        /// 取得指定月份週數。
        /// </summary>
        /// <param name="year">年份。</param>
        /// <param name="month">月份。</param>
        public static int WeeksInMonth(int year, int month)
        {
            DateTime oDate;
            int iDays;

            // 取得該月最後一天，並判斷日期為該月的第幾週
            iDays = DateFunc.DaysInMonth(year, month);
            oDate = new DateTime(year, month, iDays);
            return GetWeekOfYear(oDate);
        }

        /// <summary>
        /// 取得指定日期為該年的第幾週。
        /// </summary>
        /// <param name="value">指定日期。</param>
        public static int GetWeekOfYear(DateTime value)
        {
            GregorianCalendar oCalendar;

            oCalendar = new GregorianCalendar();
            return oCalendar.GetWeekOfYear(value, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }

        /// <summary>
        /// 取得指定日期為該月的第幾週。
        /// </summary>
        /// <param name="value">指定日期。</param>
        public static int GetWeekOfMonth(DateTime value)
        {
            DateTime oFirstDate;

            oFirstDate = new DateTime(value.Year, value.Month, 1);
            return GetWeekOfYear(value) - GetWeekOfYear(oFirstDate) + 1;
        }

        /// <summary>
        /// 傳回指定日期是星期幾，1=星期一 3=星期三 0=星期日。
        /// </summary>
        /// <param name="value">指定日期。</param>
        public static int Weekday(DateTime value)
        {
            return (int)value.DayOfWeek;
        }

        /// <summary>
        /// 以指定日期為基準，取得當季的日期區間。
        /// </summary>
        /// <param name="value">指定日期。</param>
        /// <param name="startDate">傳出起始日期。</param>
        /// <param name="endDate">傳出終止日期。</param>
        private static void GetQuarterRange(DateTime value, out DateTime startDate, out DateTime endDate)
        {
            startDate = DateTime.MinValue;
            endDate = DateTime.MinValue;
            switch (value.Month)
            {
                case 1:
                case 2:
                case 3:
                    startDate = new DateTime(value.Year, 1, 1);
                    endDate = new DateTime(value.Year, 3, 31);
                    break;
                case 4:
                case 5:
                case 6:
                    startDate = new DateTime(value.Year, 4, 1);
                    endDate = new DateTime(value.Year, 6, 30);
                    break;
                case 7:
                case 8:
                case 9:
                    startDate = new DateTime(value.Year, 7, 1);
                    endDate = new DateTime(value.Year, 9, 30);
                    break;
                case 10:
                case 11:
                case 12:
                    startDate = new DateTime(value.Year, 10, 1);
                    endDate = new DateTime(value.Year, 12, 31);
                    break;
            }
        }

        /// <summary>
        /// 檢查日期時間是否合法。
        /// </summary>
        /// <param name="datetime">日期或時間字串。</param>
        /// <param name="format">格式化字串。</param>
        /// <remarks>
        /// format格式：
        /// 年: yyyy
        /// 月: MM
        /// 日: dd
        /// 時: HH
        /// 分: mm
        /// 秒: ss
        /// </remarks>
        public static bool ValidateDateTime(string datetime, string format)
        {
            if (datetime == null || datetime.Length == 0)
            {
                return false;
            }
            try
            {
                System.Globalization.DateTimeFormatInfo dtfi = new System.Globalization.DateTimeFormatInfo();
                dtfi.FullDateTimePattern = format;
                DateTime dt = DateTime.ParseExact(datetime, "F", dtfi);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 西曆轉農曆
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToLunarDate(DateTime value)
        {
            var oLCalendar = new TaiwanLunisolarCalendar();

            var y = oLCalendar.GetYear(value);
            var m = oLCalendar.GetMonth(value);
            var d = oLCalendar.GetDayOfMonth(value);

            var l = y.ToString() +
                    StrFunc.StrPadLeft(m.ToString(), 2, '0') +
                    StrFunc.StrPadLeft(d.ToString(), 2, '0');

            return l;
        }

        /// <summary>
        /// 農曆轉西曆。
        /// </summary>
        /// <param name="value">欲進行轉換的日期。</param>        
        public static string LunarDateToWestDate(DateTime value)
        {
            var oLCalendar = new TaiwanLunisolarCalendar();
            var isLeap = oLCalendar.IsLeapYear(value.Year - 1911);

            return oLCalendar.ToDateTime(value.Year - 1911, isLeap ? value.Month + 1 : value.Month, value.Day, 0, 0, 0, 0).ToString("yyyyMMdd");
        }

        /// <summary>
        /// 以指定日期為基準，取得指定範圍的日期區間。
        /// </summary>
        /// <param name="value">指定日期。</param>
        /// <param name="unit">日期單位。</param>
        /// <param name="startDate">傳出起始日期。</param>
        /// <param name="endDate">傳出終止日期。</param>
        public static void GetDateRange(DateTime value, EDateUnit unit, out DateTime startDate, out DateTime endDate)
        {
            int iWeekday;
            int iDaysOfMonth;

            startDate = DateTime.MinValue;
            endDate = DateTime.MinValue;
            switch (unit)
            {
                case EDateUnit.Day:
                    startDate = value;
                    endDate = value;
                    break;
                case EDateUnit.Week:
                    iWeekday = Weekday(value);
                    startDate = value.AddDays(iWeekday * -1);   //取得該週的第一天
                    endDate = value.AddDays(7 - iWeekday);        //取得該週的最後一天
                    break;
                case EDateUnit.Month:
                    iDaysOfMonth = DateFunc.DaysInMonth(value);
                    startDate = new DateTime(value.Year, value.Month, 1);                       //取得該月的第一天
                    endDate = new DateTime(value.Year, value.Month, iDaysOfMonth); //取得該月的最後一天
                    break;
                case EDateUnit.Quarter:
                    GetQuarterRange(value, out startDate, out endDate);
                    break;
                case EDateUnit.Year:
                    startDate = new DateTime(value.Year, 1, 1);  //取得該年度的第一天
                    endDate = new DateTime(value.Year, 12, 31); //取得該年度的最後一天
                    break;
            }
        }

        #region CRM 使用方法

        /// <summary>
        /// 傳回參數該月月底。
        /// </summary>
        /// <param name="odate">傳入引數。</param>
        public static DateTime GetEndMonth(DateTime odate)
        {
            return new DateTime(odate.Year, odate.Month, DateTime.DaysInMonth(odate.Year, odate.Month));
        }
        /// <summary>
        /// 傳回參數該月月初。
        /// </summary>
        /// <param name="odate">傳入引數。</param>
        public static DateTime GetBeginMonth(DateTime odate)
        {
            return new DateTime(odate.Year, odate.Month, 1);
        }
        /// <summary>
        /// 回傳相隔天數 odate1，odate2 。
        /// </summary>
        /// <param name="odate1">傳入引數。</param>
        /// <param name="odate2">傳入引數。</param>
        public static int SubtractDate(DateTime odate1, DateTime odate2)
        {
            return odate1.Subtract(odate2).Days;
        }
        /// <summary>
        /// 回傳相隔月 odate1，odate2 。
        /// </summary>
        /// <param name="odate1">傳入引數。</param>
        /// <param name="odate2">傳入引數。</param>
        public static int SubtractMonth(DateTime odate1, DateTime odate2)
        {
            return (odate2.Year - odate1.Year) * 12 + (odate2.Month - odate1.Month) + 1;
        }

        /// <summary>
        /// 判斷是否日期區間 。
        /// </summary>
        /// <param name="oStartDat">起始日。</param>
        /// <param name="oEndDat">結束日。</param>
        /// <param name="oTargetDate">目標日期。</param>
        /// <param name="canEqual">日期可以相等</param>
        public static bool IsBetweenDate(DateTime oStartDat, DateTime oEndDat, DateTime oTargetDate, bool canEqual = false)
        {
            if (canEqual)
            {
                if (oTargetDate >= oStartDat && oTargetDate <= oEndDat)
                    return true;
            }
            else
            {
                if (oTargetDate > oStartDat && oTargetDate < oEndDat)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 轉為日期格式，並傳出該日的啟始時間。
        /// </summary>
        /// <param name="date">日期。</param>
        public static DateTime ToDate(object date)
        {
            return BaseFunc.CDateTime(date).Date;
        }
        #endregion

    }
}
