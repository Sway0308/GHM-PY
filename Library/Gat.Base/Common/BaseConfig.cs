using System;
using System.Collections.Generic;
using System.Text;

namespace Gat.Base
{
    /// <summary>
    /// 欄位資料型別。
    /// </summary>
    public enum EFieldDbType
    {
        /// <summary>
        /// 字串。
        /// </summary>
        String = 0,
        /// <summary>
        /// 備註。
        /// </summary>
        Text = 1,
        /// <summary>
        /// 布林。
        /// </summary>
        Boolean = 2,
        /// <summary>
        /// 整數。
        /// </summary>
        Integer = 3,
        /// <summary>
        /// 浮點數。
        /// </summary>
        Double = 4,
        /// <summary>
        /// 貨幣。
        /// </summary>
        Currency = 5,
        /// <summary>
        /// 日期時間。
        /// </summary>
        DateTime = 6,
        /// <summary>
        /// Guid 值。
        /// </summary>
        GUID = 7,
        /// <summary>
        /// 二進位資料。
        /// </summary>
        Binary = 8
    }

    /// <summary>
    /// 欄位類型。
    /// </summary>
    public enum EFieldType
    {
        /// <summary>
        /// 資料欄位。
        /// </summary>
        DataField,
        /// <summary>
        /// 關連欄位。
        /// </summary>
        LinkField,
        /// <summary>
        /// 虛擬欄位。
        /// </summary>
        VirtualField
    }

    /// <summary>
    /// 含預設值的布林列舉。
    /// </summary>
    public enum EDefaultBoolean
    {
        /// <summary>
        /// 預設。
        /// </summary>
        Default,
        /// <summary>
        /// True。
        /// </summary>
        True,
        /// <summary>
        /// False。
        /// </summary>
        False
    }

    /// <summary>
    /// 含未設定的布林列舉。
    /// </summary>
    public enum ENotSetBoolean
    {
        /// <summary>
        /// 預設值。
        /// </summary>
        NotSet,
        /// <summary>
        /// True。
        /// </summary>
        True,
        /// <summary>
        /// False。
        /// </summary>
        False
    }

    /// <summary>
    /// DateTime 型別的預設值。
    /// </summary>
    public enum EDateTimeDefault
    {
        /// <summary>
        /// 今天日期。
        /// </summary>
        Today = 0,
        /// <summary>
        /// 現在時間。
        /// </summary>
        Now = 1,
        /// <summary>
        /// 最小值。
        /// </summary>
        MinValue = 2
    }

    /// <summary>
    /// 日期單位。
    /// </summary>
    public enum EDateUnit
    {
        /// <summary>
        /// 日。
        /// </summary>
        Day = 0,
        /// <summary>
        /// 週。
        /// </summary>
        Week = 1,
        /// <summary>
        /// 月。
        /// </summary>
        Month = 2,
        /// <summary>
        /// 季。
        /// </summary>
        Quarter = 3,
        /// <summary>
        /// 年。
        /// </summary>
        Year = 4
    }

    /// <summary>
    /// 日期間隔。
    /// </summary>
    public enum EDateInterval
    {
        /// <summary>
        /// 年。
        /// </summary>
        Year = 0,
        /// <summary>
        /// 季 (1 到 4)
        /// </summary>
        Quarter = 1,
        /// <summary>
        /// 月份 (1 到 12)
        /// </summary>
        Month = 2,
        /// <summary>
        /// 年中的日 (1 到 366)
        /// </summary>
        DayOfYear = 3,
        /// <summary>
        /// 月中的日 (1 到 31)
        /// </summary>
        Day = 4,
        /// <summary>
        /// 年中的週 (1 到 53)
        /// </summary>
        WeekOfYear = 5,
        /// <summary>
        /// 星期資訊 (1 到 7)
        /// </summary>
        Weekday = 6,
        /// <summary>
        /// 小時 (1 到 24)
        /// </summary>
        Hour = 7,
        /// <summary>
        /// 分鐘 (1 到 60)
        /// </summary>
        Minute = 8,
        /// <summary>
        /// 秒鐘 (1 到 60)
        /// </summary>
        Second = 9
    }
}
