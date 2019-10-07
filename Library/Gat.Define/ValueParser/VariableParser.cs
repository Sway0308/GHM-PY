using System.Text.RegularExpressions;
using Gat.Base;

namespace Gat.Define
{
    /// <summary>
    /// 變數解析輔助類別。
    /// </summary>
    public class VariableParser
    {
        private string _Value = null;
        private EVariableType _VariableType = EVariableType.Variable;

        #region 建構函式

        /// <summary>
        /// 建構函式。
        /// </summary>
        public VariableParser()
        {
        }

        #endregion
        
        /// <summary>
        /// 解析過後的欄位。
        /// </summary>
        public string Value
        {
            get { return _Value; }
        }

        /// <summary>
        /// 解析後的變數種類。
        /// </summary>
        public EVariableType VariableType
        {
            get { return _VariableType; }
        }

        /// <summary>
        /// 解析變數。
        /// </summary>
        /// <param name="value">解析值。</param>
        /// <remarks>
        /// 只對單一欄位解析，如:[@@Session]、[@FieldName]、[@Table.FieldName] 與常數。
        /// </remarks>
        public void Parse(string value)
        {
            Regex oRegex;
            MatchCollection oMatches;
            GroupCollection oGroups;
            string sPattern;
            string sWord;
            string[] oArray;

            sPattern = @"\[(?<word>@+[\w]*.?[\w]*)\]";
            oRegex = new Regex(sPattern, RegexOptions.IgnoreCase);
            oMatches = oRegex.Matches(value);
            foreach (Match m in oMatches)
            {
                oGroups = m.Groups;
                sWord = oGroups["word"].Value;

                if (sWord.Contains("@@"))
                {
                    // 系統變數
                    _Value = StrFunc.StrSubstring(sWord, 2);
                    _VariableType = EVariableType.Variable;
                }
                else if (sWord.Contains("@"))
                {
                    // 欄位變數
                    _Value = StrFunc.StrSubstring(sWord, 1);

                    if (sWord.Contains("."))
                    {
                        // 資料欄位，格式為 [@Table.FieldName]，但其中 [@DB.NULL] 或 [@DB.NOTNULL] 為常數
                        oArray = StrFunc.StrSplit(this.Value, ".");
                        if (oArray.Length > 0)
                        {
                            if (StrFunc.SameText(oArray[0].ToUpper(), "DB"))
                                _VariableType = EVariableType.Constant;  // DB.NULL 或 DB.NOTNULL 為常數
                            else
                                _VariableType = EVariableType.TableField; 
                        }
                    }
                    else
                    {
                        // 欄位變數，格式為 [@FieldName]
                        _VariableType = EVariableType.Field;  // 格式為 [@Table.FieldName] 
                    }
                }

                return;
            }

            _Value = value;
            _VariableType = EVariableType.Constant;
        }
    }
}
