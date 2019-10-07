using System;
using System.Text;
using System.Data;
using System.Data.Common;
using Gat.Base;
using Gat.Define;

namespace Gat.Database
{
    /// <summary>
    /// 資料庫命令輔助基底類別。
    /// </summary>
    public abstract class BaseDbCommandHelper : IDbCommandHelper
    {
        private DbCommand _DbCommand = null;
        private int _FilterIndex = 0;  //過濾參數索引

        #region 建構函式

        /// <summary>
        /// 建構函式。
        /// </summary>
        public BaseDbCommandHelper()
        {
            _DbCommand = CreateDbCommand();
        }

        /// <summary>
        /// 建構函式。
        /// </summary>
        /// <param name="commandText">命令字串。</param>
        public BaseDbCommandHelper(string commandText) : this()
        {
            if (StrFunc.StrIsNotEmpty(commandText))
                this.SetCommandText(commandText);
        }

        #endregion

        /// <summary>
        /// 建立資料庫命令。
        /// </summary>
        protected abstract DbCommand CreateDbCommand();

        /// <summary>
        /// 建立資料庫命令參數。
        /// </summary>
        protected abstract DbParameter CreateDbParameter();

        /// <summary>
        /// 參數符號。
        /// </summary>
        public abstract string ParameterSymbol { get; }
        
        /// <summary>
        /// 取得含參數符號的參數名稱。
        /// </summary>
        /// <param name="name">參數名稱。</param>
        public string GetParameterName(string name)
        {
            if (StrFunc.StrLeftWith(name, this.ParameterSymbol))
                return name;
            else
                return this.ParameterSymbol + name;
        }

        /// <summary>
        /// 取得含前後符號的資料表名稱。
        /// </summary>
        /// <param name="tableName">資料表名稱。</param>
        public abstract string GetTableName(string tableName);

        /// <summary>
        /// 取得含前後符號的欄位名稱。
        /// </summary>
        /// <param name="fieldName">欄位名稱。</param>
        public abstract string GetFieldName(string fieldName);

        /// <summary>
        /// 資料庫命令。
        /// </summary>
        public DbCommand DbCommand
        {
            get { return _DbCommand; }
        }

        /// <summary>
        /// 設定資料庫命令字串。
        /// </summary>
        /// <param name="commandText">命令字串。</param>
        public void SetCommandText(string commandText)
        {
            this.DbCommand.CommandText = commandText.ToUpper();
        }

        /// <summary>
        /// 設定資料庫命令字串，並用命令參數集合做格式化字串。
        /// </summary>
        /// <param name="commandText">命令字串。</param>
        public void SetCommandFormatText(string commandText)
        {
            StringBuilder oBuffer;

            if (StrFunc.StrContains(commandText, CommandTextVariable.Parameters))
            {
                oBuffer = new StringBuilder();
                for (int N1 = 0; N1 < this.DbCommand.Parameters.Count; N1++)
                    StrFunc.StrMerge(oBuffer, "{" + N1 + "}", ",");
                commandText = StrFunc.StrReplace(commandText, CommandTextVariable.Parameters, oBuffer.ToString());
            }

            this.DbCommand.CommandText = StrFunc.SQLFormat(commandText.ToUpper(), this.DbCommand.Parameters);
        }

        /// <summary>
        /// 新增命令參數。
        /// </summary>
        /// <param name="name">參數名稱。</param>
        /// <param name="value">參數值。</param>
        public DbParameter AddParameter(string name, object value)
        {
            DbParameter oParameter;

            oParameter = CreateDbParameter();
            oParameter.ParameterName = GetParameterName(name);
            oParameter.Value = value;

            this.DbCommand.Parameters.Add(oParameter);
            return oParameter;
        }

        /// <summary>
        /// 新增命令參數。
        /// </summary>
        /// <param name="name">參數名稱。</param>
        /// <param name="dbType">資料型別。</param>
        public DbParameter AddParameter(string name, EFieldDbType dbType)
        {
            DbParameter oParameter;

            oParameter = CreateDbParameter();
            oParameter.ParameterName = GetParameterName(name);
            // 設定命令參數的資料型別
            this.SetParameterDbType(oParameter, dbType);

            this.DbCommand.Parameters.Add(oParameter);
            return oParameter;
        }

        /// <summary>
        /// 新增命令參數。
        /// </summary>
        /// <param name="name">參數名稱。</param>
        /// <param name="dbType">資料型別。</param>
        /// <param name="value">參數值。</param>
        /// <param name="size">資料最大長度。</param>
        public DbParameter AddParameter(string name, EFieldDbType dbType, object value, int size)
        {
            DbParameter oParameter;

            oParameter = this.AddParameter(name, dbType);
            oParameter.Value = GetParameterValue(dbType, value);
            oParameter.Size = size;
            return oParameter;
        }

        /// <summary>
        /// 新增命令參數。
        /// </summary>
        /// <param name="name">參數名稱。</param>
        /// <param name="dbType">資料型別。</param>
        /// <param name="value">參數值。</param>
        public DbParameter AddParameter(string name, EFieldDbType dbType, object value)
        {
            return AddParameter(name, dbType, value, 0);
        }

        /// <summary>
        /// 設定命令參數的資料型別。
        /// </summary>
        /// <param name="parameter">命令參數。</param>
        /// <param name="dbType">欄位資料型別。</param>
        public virtual void SetParameterDbType(DbParameter parameter, EFieldDbType dbType)
        {
            parameter.DbType = DbTypeConverter.ToDbType(dbType);
        }

        /// <summary>
        /// 取得轉換後的參數值。
        /// </summary>
        /// <param name="dbType">欄位資料型別。</param>
        /// <param name="value">參數值。</param>
        public virtual object GetParameterValue(EFieldDbType dbType, object value)
        {
            if (dbType == EFieldDbType.DateTime)
            {
                if (BaseFunc.IsEmpty(BaseFunc.CDateTime(value)))
                    return DBNull.Value;
                else
                    return value;
            }
            else
            {
                return value;
            }
        }

        /// <summary>
        /// 新增命令參數。
        /// </summary>
        /// <param name="name">參數名稱。</param>
        /// <param name="dbType">資料型別。</param>
        /// <param name="value">參數值。</param>
        /// <param name="direction">設定參數是否為只能輸入、只能輸出、雙向 (Bidirectional) 或預存程序 (Stored Procedure) 傳回值參數。</param>
        public DbParameter AddParameter(string name, EFieldDbType dbType, object value, ParameterDirection direction)
        {
            DbParameter oParameter;

            oParameter = this.AddParameter(name, dbType, value);
            oParameter.Direction = direction;
            return oParameter;
        }

        /// <summary>
        /// 依實體欄位定義新增命令參數。
        /// </summary>
        /// <param name="dbFieldDefine">實體欄位定義。</param>
        public virtual DbParameter AddParameter(DbFieldDefine dbFieldDefine)
        {
            DbParameter oParameter;

            oParameter = AddParameter(dbFieldDefine.FieldName, dbFieldDefine.DbType);
            oParameter.SourceColumn = dbFieldDefine.FieldName;
            if (!dbFieldDefine.AllowNull)
                oParameter.Value = DataFunc.GetDefaultValue(dbFieldDefine.DbType);
            return oParameter;
        }

        /// <summary>
        /// 依欄位定義新增命令參數。
        /// </summary>
        /// <param name="fieldDefine">欄位定義。</param>
        public virtual DbParameter AddParameter(FieldDefine fieldDefine)
        {
            DbParameter oParameter;

            oParameter = AddParameter(fieldDefine.FieldName, fieldDefine.DbType);
            oParameter.SourceColumn = fieldDefine.FieldName;
            return oParameter;
        }

        /// <summary>
        /// 依欄位定義新增 SourceVersion=Original 的命令參數。
        /// </summary>
        /// <param name="fieldDefine">欄位定義。</param>
        public virtual DbParameter AddOriginalParameter(FieldDefine fieldDefine)
        {
            DbParameter oParameter;

            oParameter = AddParameter(fieldDefine.FieldName + "_ORG", fieldDefine.DbType);
            oParameter.SourceColumn = fieldDefine.FieldName;
            oParameter.SourceVersion = DataRowVersion.Original;
            return oParameter;
        }

        /// <summary>
        /// 取得新的資料過濾絛件的參數名稱。
        /// </summary>
        protected string NewFilterParameterName()
        {
            string sName;
            bool bFlag;

            bFlag = false;
            do
            {
                _FilterIndex = _FilterIndex + 1;
                sName = string.Format("Filter_{0}", _FilterIndex);
                sName = GetParameterName(sName);

                // 參數名稱不存在則設定 Flag 為 True，以跳離迴圈
                if (!this.DbCommand.Parameters.Contains(sName))
                {
                    bFlag = true;
                }
            } while (bFlag == false);

            return sName;
        }

        /// <summary>
        /// 依欄位定義新增過濾絛件命令參數。
        /// </summary>
        /// <param name="fieldDefine">欄位定義。</param>
        public virtual DbParameter AddFilterParameter(FieldDefine fieldDefine)
        {
            DbParameter oParameter;
            string sName;

            sName = this.NewFilterParameterName();
            oParameter = AddParameter(sName, fieldDefine.DbType);
            oParameter.Direction = ParameterDirection.Input;
            return oParameter;
        }

        /// <summary>
        /// 是否有指定參數。
        /// </summary>
        /// <param name="name">參數名稱。</param>
        public virtual bool HasParameter(string name)
        { 
            string sParameterName;

            sParameterName = GetParameterName(name);
            return this.DbCommand.Parameters.Contains(sParameterName);
        }

    }
}
