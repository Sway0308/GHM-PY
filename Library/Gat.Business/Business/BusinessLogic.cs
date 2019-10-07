using Gat.Base;
using Gat.Database;
using Gat.Define;
using System;
using System.Data;
using System.Linq;
using System.Text;

namespace Gat.Business
{
    /// <summary>
    /// 功能層級商業邏輯元件
    /// </summary>
    public class BusinessLogic : BaseBusinessLogic, IBusinessLogic
    {
        /// <summary>
        /// 建構函式
        /// </summary>
        /// <param name="progId">程式代碼</param>
        public BusinessLogic(Guid sessionGuid, string progId) : base(sessionGuid, progId)
        {
        }

        /// <summary>
        /// 新增表單資料。
        /// </summary>
        /// <param name="inputArgs">輸入參數</param>
        /// <returns></returns>
        public AddOutputResult Add(AddInputArgs inputArgs)
        {
            var outputResult = new AddOutputResult();

            DoBeforeAdd(inputArgs, outputResult);
            if (inputArgs.Cancel) { return outputResult; }

            DoAdd(inputArgs, outputResult);

            DoAfterAdd(inputArgs, outputResult);

            return outputResult;
        }

        /// <summary>
        /// 執行 Add 方法後呼叫的方法。
        /// </summary>
        /// <param name="inputArgs">輸入參數</param>
        /// <param name="outputResult">傳出參數</param>
        protected virtual void DoBeforeAdd(AddInputArgs inputArgs, AddOutputResult outputResult)
        {

        }

        /// <summary>
        /// 執行 Add 方法的實作。
        /// </summary>
        /// <param name="inputArgs">輸入參數</param>
        /// <param name="outputResult">傳出參數</param>
        protected virtual void DoAdd(AddInputArgs inputArgs, AddOutputResult outputResult)
        {
            var table = DefineFunc.CreateDataTable(this.ProgramDefine.MasterTable);
            var dataSet = DataFunc.CreateDataSet(this.progId);
            dataSet.Tables.Add(table);
            table.Rows.Add(table.NewRow());
            outputResult.DataSet = dataSet;
        }

        /// <summary>
        /// 執行 Add 方法前呼叫的方法。
        /// </summary>
        /// <param name="inputArgs">輸入參數</param>
        /// <param name="outputResult">傳出參數</param>
        protected virtual void DoAfterAdd(AddInputArgs inputArgs, AddOutputResult outputResult)
        {

        }

        /// <summary>
        /// 刪除表單資料。
        /// </summary>
        /// <param name="inputArgs">輸入參數</param>
        /// <returns></returns>
        public DeleteOutputResult Delete(DeleteInputArgs inputArgs)
        {
            var outputResult = new DeleteOutputResult();

            DoBeforeDelete(inputArgs, outputResult);
            if (inputArgs.Cancel) { return outputResult; }
                        
            DoDelete(inputArgs, outputResult);            

            DoAfterDelete(inputArgs, outputResult);

            return outputResult;
        }

        /// <summary>
        /// 執行 Delete 方法前呼叫的方法。
        /// </summary>
        /// <param name="inputArgs">輸入參數</param>
        /// <param name="outputResult">傳出參數</param>
        protected virtual void DoBeforeDelete(DeleteInputArgs inputArgs, DeleteOutputResult outputResult)
        {
            
        }

        /// <summary>
        /// 執行 Delete 方法的實作。
        /// </summary>
        /// <param name="inputArgs">輸入參數</param>
        /// <param name="outputResult">傳出參數</param>
        protected virtual void DoDelete(DeleteInputArgs inputArgs, DeleteOutputResult outputResult)
        {
            var sql = new StringBuilder();
            sql.AppendLine("Delete A");
            sql.AppendLine($"From {this.progId} A");
            sql.AppendLine($"Where A.{SysFields.ID} = {inputArgs.FormID.SQLStr()}");
            this.DbAccess.ExecuteNonQuery(this.DatabaseID, sql.ToString());
        }

        /// <summary>
        /// 執行 Delete 方法後呼叫的方法。
        /// </summary>
        /// <param name="inputArgs">輸入參數</param>
        /// <param name="outputResult">傳出參數</param>
        protected virtual void DoAfterDelete(DeleteInputArgs inputArgs, DeleteOutputResult outputResult)
        {
            
        }

        /// <summary>
        /// 編輯表單資料。
        /// </summary>
        /// <param name="inputArgs">輸入參數</param>
        /// <returns></returns>
        public EditOutputResult Edit(EditInputArgs inputArgs)
        {
            var outputResult = new EditOutputResult();

            DoBeforeEdit(inputArgs, outputResult);
            if (inputArgs.Cancel) { return outputResult; }

            DoEdit(inputArgs, outputResult);

            DoAfterEdit(inputArgs, outputResult);

            return outputResult;
        }

        /// <summary>
        /// 執行 Edit 方法前呼叫的方法。
        /// </summary>
        /// <param name="inputArgs">輸入參數</param>
        /// <param name="outputResult">傳出參數</param>
        protected virtual void DoBeforeEdit(EditInputArgs inputArgs, EditOutputResult outputResult)
        {
            
        }

        /// <summary>
        /// 執行 Edit 方法的實作。
        /// </summary>
        /// <param name="inputArgs">輸入參數</param>
        /// <param name="outputResult">傳出參數</param>
        protected virtual void DoEdit(EditInputArgs inputArgs, EditOutputResult outputResult)
        {
            
        }

        /// <summary>
        /// 執行 Edit 方法後呼叫的方法。
        /// </summary>
        /// <param name="inputArgs">輸入參數</param>
        /// <param name="outputResult">傳出參數</param>
        protected virtual void DoAfterEdit(EditInputArgs inputArgs, EditOutputResult outputResult)
        {
            
        }

        /// <summary>
        /// 執行自訂方法。
        /// </summary>
        /// <param name="inputArgs">輸入參數</param>
        /// <returns></returns>
        public ExecFuncOutputResult ExecFunc(ExecFuncInputArgs inputArgs)
        {
            var outputResult = new ExecFuncOutputResult();
            DoExecFunc(inputArgs, outputResult);
            return outputResult;
        }

        /// <summary>
        /// 自訂方法實作。
        /// </summary>
        /// <param name="inputArgs">輸入參數</param>
        /// <param name="outputResult">傳出參數</param>
        protected virtual void DoExecFunc(ExecFuncInputArgs inputArgs, ExecFuncOutputResult outputResult)
        {
            
        }

        /// <summary>
        /// 查詢清單資料。
        /// </summary>
        /// <param name="inputArgs">輸入參數</param>
        /// <returns></returns>
        public FindOutputResult Find(FindInputArgs inputArgs)
        {
            var outputResult = new FindOutputResult();

            DoBeforeFind(inputArgs, outputResult);
            if (inputArgs.Cancel) { return outputResult; }

            DoFind(inputArgs, outputResult);

            DoAfterFind(inputArgs, outputResult);

            return outputResult;
        }

        /// <summary>
        /// 執行 Find 方法前呼叫的方法。
        /// </summary>
        /// <param name="inputArgs">輸入參數</param>
        /// <param name="outputResult">傳出參數</param>
        protected virtual void DoBeforeFind(FindInputArgs inputArgs, FindOutputResult outputResult)
        {
            
        }

        /// <summary>
        /// 執行 Find 方法的實作。
        /// </summary>
        /// <param name="inputArgs">輸入參數</param>
        /// <param name="outputResult">傳出參數</param>
        protected virtual void DoFind(FindInputArgs inputArgs, FindOutputResult outputResult)
        {
            var selectArgs = new SelectInputArgs { TableName = this.progId, SelectFields = inputArgs.SelectFields, FilterItems = inputArgs.FilterItems };
            var selectResult = Select(selectArgs);
            outputResult.Table = selectResult.Table;
        }

        /// <summary>
        /// 執行 Find 方法後呼叫的方法。
        /// </summary>
        /// <param name="inputArgs">輸入參數</param>
        /// <param name="outputResult">傳出參數</param>
        protected virtual void DoAfterFind(FindInputArgs inputArgs, FindOutputResult outputResult)
        {
            
        }

        /// <summary>
        /// 取得指定內碼的表單資料。
        /// </summary>
        /// <param name="inputArgs">輸入參數</param>
        /// <returns></returns>
        public MoveOutputResult Move(MoveInputArgs inputArgs)
        {
            var outputResult = new MoveOutputResult();

            DoBeforeMove(inputArgs, outputResult);
            if (inputArgs.Cancel) { return outputResult; }

            DoMove(inputArgs, outputResult);

            DoAfterMove(inputArgs, outputResult);

            return outputResult;
        }

        /// <summary>
        /// 執行 Move 方法前呼叫的方法。
        /// </summary>
        /// <param name="inputArgs">輸入參數</param>
        /// <param name="outputResult">傳出參數</param>
        protected virtual void DoBeforeMove(MoveInputArgs inputArgs, MoveOutputResult outputResult)
        {
            
        }

        /// <summary>
        /// 執行 Move 方法的實作。
        /// </summary>
        /// <param name="inputArgs">輸入參數</param>
        /// <param name="outputResult">傳出參數</param>
        protected virtual void DoMove(MoveInputArgs inputArgs, MoveOutputResult outputResult)
        {
            outputResult.DataSet = new DataSet(this.progId);
            foreach (var tableDefine in this.ProgramDefine.Tables)
            {
                if (tableDefine.IsMaster)
                {
                    var selectArgs = new SelectInputArgs { TableName = tableDefine.TableName, FilterItems = { new FilterItem(SysFields.ID, inputArgs.FormID) } };
                    var selectResult = Select(selectArgs);
                    outputResult.DataSet.Tables.Add(selectResult.Table);
                }
                else
                {
                    var selectArgs = new SelectInputArgs { TableName = tableDefine.TableName, FilterItems = { new FilterItem(SysFields.MasterID, inputArgs.FormID) } };
                    var selectResult = Select(selectArgs);
                    outputResult.DataSet.Tables.Add(selectResult.Table);
                }
            }
        }

        /// <summary>
        /// 取得查詢資料
        /// </summary>
        /// <param name="tableDefine">資料表定義</param>
        /// <param name="formID">單據編號</param>
        /// <returns></returns>
        private DataTable GetQueryTable(TableDefine tableDefine, string formID)
        {
            var fieldName = tableDefine.IsMaster ? SysFields.ID : SysFields.MasterID;
            var args = new SelectInputArgs { TableName = tableDefine.TableName, FilterItems = { new FilterItem(fieldName, formID) } };
            foreach (var filterItem in tableDefine.FilterItems)
            {
                args.FilterItems.Add(filterItem.Clone());
            }
            var result = Select(args);
            return result.Table;
        }

        /// <summary>
        /// 執行 Move 方法後呼叫的方法。
        /// </summary>
        /// <param name="inputArgs">輸入參數</param>
        /// <param name="outputResult">傳出參數</param>
        protected virtual void DoAfterMove(MoveInputArgs inputArgs, MoveOutputResult outputResult)
        {
            
        }

        /// <summary>
        /// 儲存表單資料。
        /// </summary>
        /// <param name="inputArgs">輸入參數</param>
        /// <returns></returns>
        public SaveOutputResult Save(SaveInputArgs inputArgs)
        {
            var outputResult = new SaveOutputResult();

            DoBeforeSave(inputArgs, outputResult);
            if (inputArgs.Cancel) { return outputResult; }

            DoSave(inputArgs, outputResult);

            DoAfterSave(inputArgs, outputResult);

            return outputResult;
        }

        /// <summary>
        /// 執行 Save 方法前呼叫的方法。
        /// </summary>
        /// <param name="inputArgs">輸入參數</param>
        /// <param name="outputResult">傳出參數</param>
        protected virtual void DoBeforeSave(SaveInputArgs inputArgs, SaveOutputResult outputResult)
        {
            
        }

        /// <summary>
        /// 執行 Save 方法的實作。
        /// </summary>
        /// <param name="inputArgs">輸入參數</param>
        /// <param name="outputResult">傳出參數</param>
        protected virtual void DoSave(SaveInputArgs inputArgs, SaveOutputResult outputResult)
        {
            var table = inputArgs.DataSet.Tables[this.progId];
            if (inputArgs.SaveMode == ESaveMode.Add)
            {
                foreach (DataRow row in table.Rows)
                {
                    var sql = new StringBuilder();
                    sql.AppendLine($"Insert Into {this.progId} (");
                    var isFirst = true;
                    foreach (var fieldDefine in this.ProgramDefine.MasterFields.Where(x => x.FieldType == EFieldType.DataField))
                    {
                        if (!row.HasField(fieldDefine.FieldName))
                            continue;
                        var fieldName = fieldDefine.FieldName;
                        sql.AppendLine((isFirst ? "" : ",") + $"{fieldName}");
                        isFirst = false;
                    }
                    sql.AppendLine(")");
                    sql.AppendLine("Values (");
                    isFirst = true;
                    foreach (var fieldDefine in this.ProgramDefine.MasterFields.Where(x => x.FieldType == EFieldType.DataField))
                    {
                        var fieldName = fieldDefine.FieldName;
                        if (row.HasField(fieldName))
                        {
                            sql.AppendLine((isFirst ? "" : ",") + $"N{row.ValueAsString(fieldName).SQLStr()}");
                            isFirst = false;
                        }
                    }
                    sql.AppendLine(")");
                    this.DbAccess.ExecuteNonQuery(this.DatabaseID, sql.ToString());
                }
            }
            else
            {
                foreach (DataRow row in table.Rows)
                {
                    var sql = new StringBuilder();
                    sql.AppendLine("Update A Set");
                    var isFirst = true;
                    foreach (var fieldDefine in this.ProgramDefine.MasterFields.Where(x => x.FieldType == EFieldType.DataField))
                    {
                        var fieldName = fieldDefine.FieldName;
                        if (row.HasField(fieldName) && !fieldName.SameText($"{SysFields.ID}"))
                        {
                            sql.AppendLine((isFirst ? "" : ",") + $"{fieldName} = {row.ValueAsString(fieldName).SQLStr()}");
                            isFirst = false;
                        }
                    }
                    sql.AppendLine($"From {this.progId} A");
                    sql.AppendLine($"Where A.{SysFields.ID} = {row.ValueAsString($"{SysFields.ID}").SQLStr()}");
                    this.DbAccess.ExecuteNonQuery(this.DatabaseID, sql.ToString());
                }
            }
        }

        /// <summary>
        /// 執行 Save 方法後呼叫的方法。
        /// </summary>
        /// <param name="inputArgs">輸入參數</param>
        /// <param name="outputResult">傳出參數</param>
        protected virtual void DoAfterSave(SaveInputArgs inputArgs, SaveOutputResult outputResult)
        {
            
        }

        /// <summary>
        /// 取得指定資料表符合條件的資料。
        /// </summary>
        /// <param name="inputArgs">輸入參數</param>
        /// <returns></returns>
        public SelectOutputResult Select(SelectInputArgs inputArgs)
        {
            var outputResult = new SelectOutputResult();

            DoBeforeSelect(inputArgs, outputResult);
            if (inputArgs.Cancel) { return outputResult; }

            DoSelect(inputArgs, outputResult);

            DoAfterSelect(inputArgs, outputResult);

            return outputResult;
        }

        /// <summary>
        /// 執行 Select 方法前呼叫的方法
        /// </summary>
        /// <param name="inputArgs">輸入參數</param>
        /// <param name="outputResult">傳出參數</param>
        protected virtual void DoBeforeSelect(SelectInputArgs inputArgs, SelectOutputResult outputResult)
        {
            
        }

        /// <summary>
        /// 執行 Select 方法的實作
        /// </summary>
        /// <param name="inputArgs">輸入參數</param>
        /// <param name="outputResult">傳出參數</param>
        protected virtual void DoSelect(SelectInputArgs inputArgs, SelectOutputResult outputResult)
        {
            var sTableName = inputArgs.TableName;
            var oTableDefine = this.ProgramDefine.Tables[sTableName];
            if (BaseFunc.IsNull(oTableDefine)) { return; }

            // 因為 Find 與 Move 都會統一進入 Select
            // 所以放在這邊統一做判斷過濾條件是否有包含 SYS_ID 欄位且其值是 *
            // 如滿足條件則需要判斷資料型別，並更改其查詢值
            CheckFilterItemsForDbType(inputArgs.FilterItems);

            // 取得 Select 的欄位集合字串
            var sSelectFields = GetSelectFields(oTableDefine, inputArgs.SelectFields);

            var oDbCommandBuilder = this.CreateDbCommandBuilder();
            var oDbCommand = oDbCommandBuilder.BuildSelectCommand(sTableName, sSelectFields, inputArgs.FilterItems, inputArgs.UserFilter,
                inputArgs.IsOrderBy);
            if (BaseFunc.IsNull(oDbCommand)) { return; }

            var oDataTable = base.DbAccess.ExecuteDataTable(this.DatabaseID, oDbCommand);
            oDataTable.TableName = sTableName;

            if (inputArgs.IsBuildVirtualField)
            {
                // 加入虛擬欄位
                var oVFBuilder = new VirtualFieldBuilder(oTableDefine, oDataTable);
                oVFBuilder.Execute();
            }
            else
            {
                // 判斷 Select 的欄位是否有虛擬欄位
                var oVFBuilder = new VirtualFieldBuilder(oTableDefine, oDataTable);
                oVFBuilder.Execute(sSelectFields);
            }

            // 設定資料表中每個欄位的預設值
            BusinessFunc.SetDataColumnDefaultValue(oTableDefine, oDataTable);

            // 設定DataTable主索引鍵
            if (StrFunc.StrIsNotEmpty(oTableDefine.TablePrimaryKey))
                DataFunc.DataTableSetPrimaryKey(oDataTable, oTableDefine.TablePrimaryKey);

            // 資料表同意變更，讓資料表無異動狀態
            oDataTable.AcceptChanges();
            outputResult.Table = oDataTable;
        }

        /// <summary>
        /// 建立資料庫命令產生器。
        /// </summary>
        private IDbCommandBuilder CreateDbCommandBuilder()
        {
            return DatabaseFunc.CreateDbCommandBuilder(this.SessionInfo, this.ProgramDefine);
        }

        /// <summary>
        /// 檢查過濾條件的 SYS_ID 欄位的資料型別
        /// </summary>
        /// <param name="filterItems">過濾條件集合。</param>
        /// <remarks>
        /// 如果過濾條件的 SYS_ID 欄位其查詢值是 *
        /// 需要判斷 SYS_ID 的欄位資料型別，再依照型別更改為正確的值。
        /// </remarks>
        private void CheckFilterItemsForDbType(FilterItemCollection filterItems)
        {
            if (!this.ProgramDefine.MasterTable.Fields.Contains(SysFields.ID)) return;
            var oFieldDefine = this.ProgramDefine.MasterTable.Fields[SysFields.ID];

            foreach (FilterItem filterItem in filterItems)
            {
                if (StrFunc.SameText(filterItem.FieldName, SysFields.ID) && StrFunc.SameText(filterItem.FilterValue, "*"))
                {
                    // 資料型別是 Interger 時把查詢值換成 -1
                    if (oFieldDefine.DbType == EFieldDbType.Integer)
                        filterItem.FilterValue = "-1";
                    // 資料型別是 DateTime 時把查詢值換成最小時間 0001/01/01
                    else if (oFieldDefine.DbType == EFieldDbType.DateTime)
                        filterItem.FilterValue = BaseFunc.CDateTime("0001/1/1").ToString("yyyy/MM/dd");
                }
            }
        }

        /// <summary>
        /// 取得 Select 的欄位集合字串。
        /// </summary>
        /// <param name="tableDefine">資料表。</param>
        /// <param name="selectFields"></param>
        private string GetSelectFields(TableDefine tableDefine, string selectFields)
        {
            if (StrFunc.StrIsEmpty(selectFields)) { return string.Empty; }

            var oSelectFields = StrFunc.StrSplit(selectFields, ",");
            var oFields = new StringHashSet(selectFields, ",");

            return oFields.ToString(",");
        }

        /// <summary>
        /// 執行 Select 方法後呼叫的方法
        /// </summary>
        /// <param name="inputArgs">輸入參數</param>
        /// <param name="outputResult">傳出參數</param>
        protected virtual void DoAfterSelect(SelectInputArgs inputArgs, SelectOutputResult outputResult)
        {
            
        }
    }
}
