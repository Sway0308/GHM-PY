using Gat.Base;
using Gat.Cache;
using Gat.Define;
using Gat.Business;
using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.Linq;
using Gat.Setting;

namespace SettingHelper
{
    /// <summary>
    /// 檔案初始化輔助器
    /// </summary>
    public class InitDataHelper
    {
        private string CurrentPath => Directory.GetParent(BaseInfo.AppDataPath).FullName;
        private Guid SessionGuid => Guid.NewGuid();

        /// <summary>
        /// 定義儲存輔助器
        /// </summary>
        private SaveDefineHelper SaveDefineHelper { get; } = new SaveDefineHelper();

        /// <summary>
        /// 檔案初始化
        /// </summary>
        public void InitData()
        {
            InitDbSetting();
            InitProgSetting();
            InitProgDefine();
        }

        /// <summary>
        /// 初始化程式定義
        /// </summary>
        private void InitProgDefine()
        {
            var depart = CreateProgDefines("Depart", "部門");
            var duty = CreateProgDefines("Duty", "職缺");
            var employee = CreateProgDefines("Employee", "員工");
            AddLinkReturnField(employee.MasterFields, depart);
            AddLinkReturnField(employee.MasterFields, duty);
            ProgDefineToJson(depart);
            ProgDefineToJson(duty);
            ProgDefineToJson(employee);
        }

        /// <summary>
        /// 初始化程式設定
        /// </summary>
        private void InitProgSetting()
        {
            var progSetting = new ProgramSetting { DisplayName = "人資管理" };
            progSetting.Items.Add(new ProgramItem("Depart") { DisplayName = "部門", BusinessInstanceType = new InstanceType { AssemblyFile = "Gat.Business.Hum", TypeName = "Gat.Business.Hum.DepartBL" } });
            progSetting.Items.Add(new ProgramItem("Duty") { DisplayName = "職務", BusinessInstanceType = new InstanceType { AssemblyFile = "Gat.Business.Hum", TypeName = "Gat.Business.Hum.DutyBL" } });
            progSetting.Items.Add(new ProgramItem("Employee") { DisplayName = "員工", BusinessInstanceType = new InstanceType { AssemblyFile = "Gat.Business.Hum", TypeName = "Gat.Business.Hum.EmployeeBL" } });
            this.SaveDefineHelper.SaveDefine(progSetting);
        }

        /// <summary>
        /// 初始化資料庫清單設定
        /// </summary>
        private void InitDbSetting()
        {
            var dbSetting = new DatabaseSettings();
            dbSetting.Items.Add(new DatabaseItem("001")
            {
                DisplayName = "Demo",
                DbServer = "localhost",
                DbName = "SkyDb",
                LoginID = "sa",
                Password = "Hn54510058"
            });
            this.SaveDefineHelper.SaveDefine(dbSetting);
        }

        /// <summary>
        /// 產生程式定義
        /// </summary>
        /// <param name="progId"></param>
        /// <param name="displayName"></param>
        /// <returns></returns>
        private ProgramDefine CreateProgDefines(string progId, string displayName)
        {
            var progDefine = new ProgramDefine { ProgId = progId, DisplayName = displayName };
            var tableDefine = new TableDefine { TableName = progId, DisplayName = displayName, DbTableName = progId, PrimaryKey = $"{progId}ID", EntityInstanceType = new InstanceType { AssemblyFile = "Gat.Business.Hum", TypeName = $"Gat.Business.Hum.{progId}Entity" } };
            tableDefine.Fields.Add(new FieldDefine { FieldName = SysFields.CompanyID, DisplayName = $"公司編號", MaxLength = 10 });
            tableDefine.Fields.Add(new FieldDefine { FieldName = SysFields.ID, DisplayName = $"{displayName}編號", MaxLength = 10 });
            tableDefine.Fields.Add(new FieldDefine { FieldName = SysFields.ViewID, DisplayName = $"{displayName}代碼", MaxLength = 10 });
            tableDefine.Fields.Add(new FieldDefine { FieldName = SysFields.Name, DisplayName = $"{displayName}名稱", MaxLength = 10 });
            progDefine.Tables.Add(tableDefine);
            return progDefine;
        }

        /// <summary>
        /// 加入關連欄位
        /// </summary>
        /// <param name="fields"></param>
        /// <param name="linkProgDefine"></param>
        private void AddLinkReturnField(FieldDefineCollection fields, ProgramDefine linkProgDefine)
        {
            var linkprogId = linkProgDefine.ProgId;
            var linkDisplayName = linkProgDefine.DisplayName;

            var linkField = new FieldDefine { FieldName = $"{linkprogId}ID", DisplayName = $"{linkDisplayName}編號", MaxLength = 10, LinkProgId = linkprogId };
            linkField.LinkReturnFields.Add(new LinkReturnField { SourceField = SysFields.ID, DestField = $"{linkprogId}ID" });
            linkField.LinkReturnFields.Add(new LinkReturnField { SourceField = SysFields.ViewID, DestField = $"TMP_{linkprogId}ID" });
            linkField.LinkReturnFields.Add(new LinkReturnField { SourceField = SysFields.Name, DestField = $"TMP_{linkprogId}Name" });
            fields.Add(linkField);
            fields.Add(new FieldDefine { FieldName = $"TMP_{linkprogId}ID", DisplayName = $"{linkDisplayName}代碼", FieldType = EFieldType.LinkField, LinkFieldName = $"{linkprogId}ID" });
            fields.Add(new FieldDefine { FieldName = $"TMP_{linkprogId}Name", DisplayName = $"{linkDisplayName}名稱", FieldType = EFieldType.LinkField, LinkFieldName = $"{linkprogId}ID" });
        }

        /// <summary>
        /// 程式定義轉XML
        /// </summary>
        /// <param name="programDefine"></param>
        public void ProgDefineToJson(ProgramDefine programDefine)
        {
            this.SaveDefineHelper.SaveDefine(programDefine);

            var dbDefines = DefineFunc.ProgDefineToDbTableDefine(programDefine);
            foreach (var d in dbDefines)
            {
                this.SaveDefineHelper.SaveDefine(d);
            }
        }

        /// <summary>
        /// 建立資料表
        /// </summary>
        public void CreateDbTable()
        {
            var dbDefines = CacheFunc.GetDbTableDefines();
            var dbSetting = CacheFunc.GetDatabaseSettings();
            var helper = new UpgradeTableHelper(dbSetting);
            foreach (var define in dbDefines)
            {
                helper.UpgradeTable(define);
            }
        }

        /// <summary>
        /// 新增資料
        /// </summary>
        public void AddData()
        {
            var files = from f in Directory.EnumerateFiles($@"{this.CurrentPath}\DemoData\ImportData", "*.json", SearchOption.TopDirectoryOnly)
                        select new { FileName = FileFunc.GetFileName(f).Replace(".json", ""), Text = FileFunc.FileReadAllText(f) };
            foreach (var file in files)
            {
                var table = JsonConvert.DeserializeObject<DataTable>(file.Text);
                table.TableName = file.FileName;
                var dataSet = new DataSet(file.FileName) { Tables = { table } };

                var bl = BusinessFunc.CreateBusinessLogic(this.SessionGuid, file.FileName);
                var result = bl.Save(new SaveInputArgs { DataSet = dataSet, SaveMode = ESaveMode.Add });
            }
        }

        /// <summary>
        /// 修改資料
        /// </summary>
        public void EditData()
        {
            var bl = BusinessFunc.CreateBusinessLogic(this.SessionGuid, "Employee");
            var result = bl.Find(new FindInputArgs());
            var table = result.Table;
            foreach (DataRow row in result.Table.Rows)
            {
                row[SysFields.ViewID] = row.ValueAsString(SysFields.ViewID) + "_2";
            }
            var dataSet = new DataSet(table.TableName);
            dataSet.Tables.Add(table);
            var saveResult = bl.Save(new SaveInputArgs { DataSet = dataSet, SaveMode = ESaveMode.Edit });
        }

        /// <summary>
        /// 刪除資料
        /// </summary>
        public void DeleteData()
        {
            var bl = BusinessFunc.CreateBusinessLogic(this.SessionGuid, "Employee");
            var result = bl.Find(new FindInputArgs());
            var table = result.Table;
            foreach (DataRow row in result.Table.Rows)
            {
                bl.Delete(new DeleteInputArgs { FormID = row.ValueAsString(SysFields.ID) });
            }
        }

        /// <summary>
        /// 查詢單一資料
        /// </summary>
        public string FindSingleData(string progId, string formId)
        {
            var bl = BusinessFunc.CreateBusinessLogic(this.SessionGuid, progId);
            var result = bl.Move(new MoveInputArgs { FormID = formId });
            var json = JsonConvert.SerializeObject(result.DataSet.Tables[progId].Rows[0], Formatting.Indented);
            return json;
        }

        /// <summary>
        /// 清單查詢
        /// </summary>
        public string FindData(string progId)
        {
            var bl = BusinessFunc.CreateBusinessLogic(this.SessionGuid, progId);
            var result = bl.Find(new FindInputArgs());
            var table = result.Table;
            var json = JsonConvert.SerializeObject(table, Formatting.Indented);
            //FileFunc.FileWriteAllText($@"{CurrentPath}\DemoData\FindData", "Employee.json", json);
            return json;
        }
    }
}
