using System.Data;
using System.Data.Common;
using Gat.Base;
using Gat.Define;

namespace Gat.Database
{
    /// <summary>
    /// 資料庫命令輔助類別介面。
    /// </summary>
    public interface IDbCommandHelper
    {
        /// <summary>
        /// 資料庫命令。
        /// </summary>
        DbCommand DbCommand { get; }

        /// <summary>
        /// 設定資料庫命令字串。
        /// </summary>
        /// <param name="commandText">命令字串。</param>
        void SetCommandText(string commandText);

        /// <summary>
        /// 設定資料庫命令字串，並用命令參數集合做格式化字串。
        /// </summary>
        /// <param name="commandText">命令字串。</param>
        void SetCommandFormatText(string commandText);

        /// <summary>
        /// 參數符號。
        /// </summary>
        string ParameterSymbol { get; }

        /// <summary>
        /// 取得含前導字的參數名稱。
        /// </summary>
        /// <param name="name">參數名稱。</param>
        string GetParameterName(string name);

        /// <summary>
        /// 取得含前後符號的資料表名稱。
        /// </summary>
        /// <param name="tableName">資料表名稱。</param>
        string GetTableName(string tableName);

        /// <summary>
        /// 取得含前後符號的欄位名稱。
        /// </summary>
        /// <param name="fieldName">欄位名稱。</param>
        string GetFieldName(string fieldName);

        /// <summary>
        /// 新增命令參數。
        /// </summary>
        /// <param name="name">參數名稱。</param>
        /// <param name="value">參數值。</param>
        DbParameter AddParameter(string name, object value);

        /// <summary>
        /// 新增命令參數。
        /// </summary>
        /// <param name="name">參數名稱。</param>
        /// <param name="dbType">資料型別。</param>
        /// <param name="value">參數值。</param>
        /// <param name="size">資料最大長度。</param>
        DbParameter AddParameter(string name, EFieldDbType dbType, object value, int size);

        /// <summary>
        /// 新增命令參數。
        /// </summary>
        /// <param name="name">參數名稱。</param>
        /// <param name="dbType">資料型別。</param>
        /// <param name="value">參數值。</param>
        DbParameter AddParameter(string name, EFieldDbType dbType, object value);

        /// <summary>
        /// 新增命令參數。
        /// </summary>
        /// <param name="name">參數名稱。</param>
        /// <param name="dbType">資料型別。</param>
        /// <param name="value">參數值。</param>
        /// <param name="direction">設定參數是否為只能輸入、只能輸出、雙向 (Bidirectional) 或預存程序 (Stored Procedure) 傳回值參數。</param>
        DbParameter AddParameter(string name, EFieldDbType dbType, object value, ParameterDirection direction);

        /// <summary>
        /// 依實體欄位定義新增命令參數。
        /// </summary>
        /// <param name="dbFieldDefine">實體欄位定義。</param>
        DbParameter AddParameter(DbFieldDefine dbFieldDefine);

        /// <summary>
        /// 依欄位定義新增命令參數。
        /// </summary>
        /// <param name="fieldDefine">欄位定義。</param>
        DbParameter AddParameter(FieldDefine fieldDefine);

        /// <summary>
        /// 依欄位定義新增 SourceVersion=Original 的命令參數。
        /// </summary>
        /// <param name="fieldDefine">欄位定義。</param>
        DbParameter AddOriginalParameter(FieldDefine fieldDefine);

        /// <summary>
        /// 依欄位定義新增過濾絛件命令參數。
        /// </summary>
        /// <param name="fieldDefine">欄位定義。</param>
        DbParameter AddFilterParameter(FieldDefine fieldDefine);

        /// <summary>
        /// 是否有指定參數。
        /// </summary>
        /// <param name="name"></param>
        bool HasParameter(string name);
    }
}
