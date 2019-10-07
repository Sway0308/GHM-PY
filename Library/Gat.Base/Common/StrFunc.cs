using System;
using System.Data.Common;
using System.Text;

namespace Gat.Base
{
    /// <summary>
    /// 字串相關函式庫。
    /// </summary>
    public class StrFunc
    {
        /// <summary>
        /// 字串取代。 
        /// </summary>
        /// <param name="s">字串。</param>
        /// <param name="oldValue">被取代的舊值。</param>
        /// <param name="newValue">要取代的新增。</param>
        public static string StrReplace(string s, string oldValue, string newValue)
        {
            if (StrIsEmpty(s)) { return string.Empty; } //判斷空字串直接回傳 string.Empty
            return s.Replace(oldValue, newValue);
        }

        /// <summary>
        /// 將字串前後加上單引號。
        /// </summary>
        /// <param name="s">原字串。</param>
        /// <returns>將原字串前後加上單引號並傳回，通常於組 Sql 語法時使用。</returns>
        public static string SQLStr(string s)
        {
            return "'" + StrReplace(s, "'", "''") + "'";
        }

        /// <summary>
        /// 去除字串左右空白。
        /// </summary>
        /// <param name="s">字串。</param>
        public static string StrTrim(string s)
        {
            if (s == null)
                return string.Empty;
            else
                return s.Trim();
        }

        /// <summary>
        /// 判斷是否為空字串，若為 null 也會視為空字串。
        /// </summary>
        /// <param name="s">字串。</param>
        /// <param name="isTrim">是否去除左右空白。</param>
        public static bool StrIsEmpty(string s, bool isTrim)
        {
            if (s == null) { return true; }
            if (isTrim)
                s = s.Trim();
            return string.IsNullOrEmpty(s);
        }

        /// <summary>
        /// 判斷是否為空字串，若為 null 也會視為空字串。
        /// </summary>
        /// <param name="s">字串。</param>
        public static bool StrIsEmpty(string s)
        {
            return StrIsEmpty(s, true);
        }

        /// <summary>
        /// 先轉型為字串再判斷是否為空字串，若為 null 也會視為空字串。
        /// </summary>
        /// <param name="s">字串。</param>
        public static bool StrIsEmpty(object s)
        {
            return StrIsEmpty(BaseFunc.CStr(s));
        }

        /// <summary>
        /// 判斷是否不為空字串。
        /// </summary>
        /// <param name="s">字串。</param>
        /// <param name="isTrim">是否去除左右空白。</param>
        public static bool StrIsNotEmpty(string s, bool isTrim)
        {
            return !StrIsEmpty(s, isTrim);
        }

        /// <summary>
        /// 判斷是否不為空字串。
        /// </summary>
        /// <param name="s">字串。</param>
        public static bool StrIsNotEmpty(string s)
        {
            return StrIsNotEmpty(s, true);
        }

        /// <summary>
        /// 判斷陣列所有成員都不是空字串。
        /// </summary>
        /// <param name="args">字串陣列。</param>
        public static bool StrIsNotEmpty(params string[] args)
        {
            foreach (string s in args)
            {
                // 任何一個成員為空字串則傳回 false
                if (StrFunc.StrIsEmpty(s)) { return false; }
            }
            return true;
        }

        /// <summary>
        /// 取得字串左邊指定長度的子字串。 
        /// </summary>
        /// <param name="s">字串。</param>
        /// <param name="length">長度。</param>
        public static string StrLeft(string s, int length)
        {
            // 若字串為空或擷取長度為 0，則傳回空字串
            if (StrIsEmpty(s) || (length <= 0)) { return string.Empty; }
            // 若擷取長度大於字串長度，則傳回原字串
            if (length > s.Length) { return s; }

            return s.Substring(0, length);
        }

        /// <summary>
        /// 取得字串右邊指定長度的子字串。
        /// </summary>
        /// <param name="s">字串。</param>
        /// <param name="length">長度。</param>
        public static string StrRight(string s, int length)
        {
            int iStartIndex;

            // 若字串為空或擷取長度為 0，則傳回空字串
            if (StrIsEmpty(s) || (length <= 0)) { return string.Empty; }
            // 若擷取長度大於字串長度，則傳回原字串
            if (length > s.Length) { return s; }

            //計算擷取字串的起始位置
            iStartIndex = s.Length - length;
            //在字串中擷取指定起始位置後子字串
            return StrFunc.StrSubstring(s, iStartIndex);
        }

        /// <summary>
        /// 字串開頭是否符合指定字串。
        /// </summary>
        /// <param name="s">字串。</param>
        /// <param name="value">判斷符合的指定字串。</param>
        public static bool StrLeftWith(string s, string value)
        {
            if (StrIsEmpty(s)) { return false; }
            return s.StartsWith(value, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// 判斷字串開頭是否符合指定字串，若無則加上該指定字串。
        /// </summary>
        /// <param name="s">字串。</param>
        /// <param name="value">判斷符合的指定字串。</param>
        public static string StrLeftCheck(string s, string value)
        {
            if (StrLeftWith(s, value))
                return s;
            else
                return value + s;
        }

        /// <summary>
        /// 字串結尾是否符合指定字串。
        /// </summary>
        /// <param name="s">字串。</param>
        /// <param name="value">判斷符合的指定字串。</param>
        public static bool StrRightWith(string s, string value)
        {
            if (StrIsEmpty(s)) { return false; }
            return s.EndsWith(value, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// 判斷字串結尾是否符合指定字串，若無則加上該指定字串。
        /// </summary>
        /// <param name="s">字串。</param>
        /// <param name="value">判斷符合的指定字串。</param>
        public static string StrRightCheck(string s, string value)
        {
            if (StrRightWith(s, value))
                return s;
            else
                return s + value;
        }

        /// <summary>
        /// 去除字串左邊指定長度的字串。
        /// </summary>
        /// <param name="s">字串。</param>
        /// <param name="length">要去除的長度。</param>
        public static string StrLeftCut(string s, int length)
        {
            return StrSubstring(s, length);
        }

        /// <summary>
        /// 判斷字串左邊是否有指定字串，有得話則去除該指定字串。
        /// </summary>
        /// <param name="s">字串。</param>
        /// <param name="value">判斷的指定字串。</param>
        public static string StrLeftCut(string s, string value)
        {
            if (StrLeftWith(s, value))
                return StrLeftCut(s, value.Length);
            else
                return s;
        }

        /// <summary>
        /// 去除字串右邊指定長度的字串。 
        /// </summary>
        /// <param name="s">字串。</param>
        /// <param name="length">要去除的長度。</param>
        public static string StrRightCut(string s, int length)
        {
            return StrLeft(s, s.Length - length);
        }

        /// <summary>
        /// 判斷字串右邊是否有指定字串，有得話則去除該指定字串。
        /// </summary>
        /// <param name="s">字串。</param>
        /// <param name="value">判斷的指定字串。</param>
        public static string StrRightCut(string s, string value)
        {
            if (StrRightWith(s, value))
                return StrRightCut(s, value.Length);
            else
                return s;
        }

        /// <summary>
        /// 去除字串左右邊指定長度的字串。
        /// </summary>
        /// <param name="s">字串。</param>
        /// <param name="length">要去除的長度。</param>
        public static string StrLeftRightCut(string s, int length)
        {
            string sResult;

            sResult = StrLeftCut(s, length);
            sResult = StrRightCut(sResult, length);
            return sResult;
        }

        /// <summary>
        /// 判斷字串是否包含指定的子字串。
        /// </summary>
        /// <param name="s">字串。</param>
        /// <param name="value">子字串。</param>
        public static bool StrContains(string s, string value)
        {
            if (StrIsEmpty(s)) { return false; }

            // 先轉為大寫再判斷是否包含子字串
            return s.ToUpper().Contains(value.ToUpper());
        }

        /// <summary>
        /// 在字串中擷取指定起始位置及長度的子字串。
        /// </summary>
        /// <param name="s">字串。</param>
        /// <param name="startIndex">以零為起始，擷取子字串的起始位置，若起始位置小於零，會強制設為零。</param>
        /// <param name="length">擷取長度。</param>
        public static string StrSubstring(string s, int startIndex, int length)
        {
            if (StrIsEmpty(s) || (length <= 0) || (startIndex >= s.Length))
                return string.Empty;

            //計算擷取字串的起始位置，若起始位置小於零，則強制設為零
            if (startIndex < 0)
                startIndex = 0;

            //若擷取長度大於範圍，則取起始位置後的子字串，忽略擷取長度
            if ((startIndex + length) > s.Length)
                return s.Substring(startIndex);

            return s.Substring(startIndex, length);
        }

        /// <summary>
        /// 在字串中擷取指定起始位置後子字串。
        /// </summary>
        /// <param name="s">字串。</param>
        /// <param name="startIndex">以零為起始，擷取子字串的起始位置，若起始位置小於零，會強制設為零。</param>
        public static string StrSubstring(string s, int startIndex)
        {
            if (StrIsEmpty(s))
            {
                return string.Empty;
            }

            //計算擷取字串的起始位置，若起始位置小於零，則強制設為零
            if (startIndex < 0)
            {
                startIndex = 0;
            }
            return s.Substring(startIndex);
        }

        /// <summary>
        /// 在字串中判斷子字串的起始位置，若無指定子字串會傳回 -1。
        /// </summary>
        /// <param name="s">字串。</param>
        ///<param name="subString">子字串。</param>
        public static int StrPos(string s, string subString)
        {
            if (StrIsEmpty(s))
            {
                return -1;
            }
            //忽略大小寫，故先轉為大寫後，再判斷子字串位置
            return s.ToUpper().IndexOf(subString.ToUpper());
        }

        /// <summary>
        /// 判斷第一個字串是否與後面的字串組的任何一個相等
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public static bool ContainText(string s1, params string[] param)
        {
            bool isSame = false;
            foreach (string s2 in param)
            {
                isSame = SameText(s1, s2);
                if (isSame)
                    break;
            }

            return isSame;
        }

        /// <summary>
        /// 判斷二個字串是否相等(忽略大小寫)。
        /// </summary>
        /// <param name="s1">第一個字串。</param>
        /// <param name="s2">第二個字串。</param>
        public static bool SameText(string s1, string s2)
        {
            if (s1 == null)
            {
                return (s2 == null);
            }
            if (s2 == null)
            {
                return false;
            }
            return s1.Equals(s2, StringComparison.CurrentCultureIgnoreCase);
        }

        /// <summary>
        /// 將傳入值先轉型為字串，再判斷二個字串是否相等(忽略大小寫)。
        /// </summary>
        /// <param name="s1">第一個字串。</param>
        /// <param name="s2">第二個字串。</param>
        public static bool SameText(object s1, object s2)
        {
            return StrFunc.SameText(BaseFunc.CStr(s1), BaseFunc.CStr(s2));
        }

        /// <summary>
        /// 判斷傳入字串是否等於陣列中任一成員。
        /// </summary>
        /// <param name="s">字串。</param>
        /// <param name="args">字串陣列。</param>
        public static bool SameTextOr(string s, params string[] args)
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
        /// <param name="args">以逗點分隔的字串集合。</param>
        public static bool SameTextOr(string s, string args)
        {
            return SameTextOr(s, StrFunc.StrSplit(args));
        }

        /// <summary>
        /// 將字串依分隔符號拆解成陣列。
        /// </summary>
        /// <param name="s">字串。</param>
        /// <param name="delimiter">分隔符號。</param>
        public static string[] StrSplit(string s, string delimiter, bool removeEmptyEntries = false)
        {
            if (removeEmptyEntries)
                return s.Split(delimiter.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            else
                return s.Split(delimiter.ToCharArray());
        }

        /// <summary>
        /// 將字串依預設分隔符號拆解成陣列。
        /// </summary>
        /// <param name="s">字串。</param>
        public static string[] StrSplit(string s)
        {
            return StrSplit(s, BaseConst.DefaultDelimiter);
        }

        /// <summary>
        /// 將字串依分隔符號拆解成陣列，並取得指定索引的成員。
        /// </summary>
        /// <param name="s">字串。</param>
        /// <param name="delimiter">分隔符號。</param>
        /// <param name="index">陣列成員的索引。</param>
        public static string StrSplit(string s, string delimiter, int index)
        {
            string[] oItems;

            oItems = StrSplit(s, delimiter);
            if (index < oItems.Length)
                return oItems[index];
            else
                return string.Empty;
        }

        /// <summary>
        /// 將字串依分隔符號拆解成左右二字串。
        /// </summary>
        /// <param name="s">字串。</param>
        /// <param name="delimiter">分隔符號。</param>
        /// <param name="left">傳出左邊的字串。</param>
        /// <param name="right">傳出右邊的字串。</param>
        public static void StrSplit(string s, string delimiter, out string left, out string right)
        {
            int iPos;

            iPos = StrPos(s, delimiter);
            left = StrLeft(s, iPos);
            right = StrSubstring(s, iPos + 1);
        }

        /// <summary>
        /// 依分隔符號合併二個字串。
        /// </summary>
        /// <param name="s1">第一個字串。</param>
        /// <param name="s2">第二個字串。</param>
        /// <param name="delimiter">分隔符號。</param>
        public static string StrMerge(string s1, string s2, string delimiter)
        {
            if (StrFunc.StrIsNotEmpty(s1))
                s1 = s1 + delimiter;
            return s1 + s2;
        }

        /// <summary>
        /// 在暫存區加入分隔符號及字串。
        /// </summary>
        /// <param name="buffer">暫存區。</param>
        /// <param name="s">字串。</param>
        /// <param name="delimiter">分隔符號。</param>
        public static void StrMerge(StringBuilder buffer, string s, string delimiter)
        {
            if (buffer.Length > 0)
                buffer.Append(delimiter);
            buffer.Append(s);
        }

        /// <summary>
        /// 刪除包住傳入字串的雙引號
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string RemoveOuterDoubleQuote(string s)
        {
            s = StrRightCut(s, "\"");
            s = StrLeftCut(s, "\"");
            return s;
        }

        /// <summary>
        /// 取得字串長度。
        /// </summary>
        /// <param name="s">字串。</param>        
        public static int StrLen(string s)
        {
            if (StrIsEmpty(s))
                return 0;
            else
                return s.Length;
        }

        /// <summary>
        /// 以空格填補左邊至指定長度。
        /// </summary>
        /// <param name="s">字串。</param>
        /// <param name="totalWidth">指定長度。</param>
        public static string StrPadLeft(string s, int totalWidth)
        {
            if (StrIsEmpty(s) || StrLen(s) <= 0)
                return string.Empty;

            return s.PadLeft(totalWidth);
        }

        /// <summary>
        /// 以指定的 Unicode 字元填補左邊至指定長度。
        /// </summary>
        /// <param name="s">字串。</param>
        /// <param name="totalWidth">指定長度。</param>
        /// <param name="paddingChar">指定字元。</param>
        public static string StrPadLeft(string s, int totalWidth, char paddingChar)
        {
            if (StrIsEmpty(s) || StrLen(s) <= 0)
                return string.Empty;

            return s.PadLeft(totalWidth, paddingChar);
        }

        /// <summary>
        /// 以指定的 Unicode 字元填補右邊至指定長度。
        /// </summary>
        /// <param name="s">字串。</param>
        /// <param name="totalWidth">指定長度。</param>
        /// <param name="paddingChar">指定字元。</param>
        public static string StrPadRight(string s, int totalWidth, char paddingChar)
        {
            if (StrIsEmpty(s) || StrLen(s) <= 0)
                return string.Empty;

            return s.PadRight(totalWidth, paddingChar);
        }

        /// <summary>
        /// 拆解字串的資料表名稱及欄位名稱。
        /// </summary>
        /// <param name="s">字串。</param>
        /// <param name="tableName">資料表名稱。</param>
        /// <param name="fieldName">欄位名稱。</param>
        public static void StrSplitFieldName(string s, out string tableName, out string fieldName)
        {
            string[] oValues;

            oValues = StrFunc.StrSplit(s, ".");
            if (oValues.Length == 1)
            {
                tableName = string.Empty;
                fieldName = oValues[0];
            }
            else if (oValues.Length == 2)
            {
                tableName = oValues[0];
                fieldName = oValues[1];
            }
            else
            {
                throw new GException("錯誤的欄位名欄位 {0}", s);
            }
        }

        /// <summary>
        /// SQL 命令文字格式化。
        /// </summary>
        /// <param name="s">SQL 命令文字。</param>
        /// <param name="parameters">命令參數集合。</param>
        public static string SQLFormat(string s, DbParameterCollection parameters)
        {
            DbParameter[] oParameters;

            oParameters = new DbParameter[parameters.Count];
            parameters.CopyTo(oParameters, 0);
            return string.Format(s, oParameters);
        }
    }
}
