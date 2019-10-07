using Gat.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gat.Define
{
    /// <summary>
    /// 儲存模式。
    /// </summary>
    public enum ESaveMode
    {
        /// <summary>
        /// 新增儲存。
        /// </summary>
        Add,
        /// <summary>
        /// 修改儲存。
        /// </summary>
        Edit
    }

    /// <summary>
    /// 結合運算子。
    /// </summary>
    public enum ECombineOperator
    {
        /// <summary>
        /// 而且。
        /// </summary>
        And,
        /// <summary>
        /// 或者。
        /// </summary>
        Or
    }

    /// <summary>
    /// 比較運算子。
    /// </summary>
    public enum EComparisonOperator
    {
        /// <summary>
        /// 等於。
        /// </summary>
        Equal,
        /// <summary>
        /// 不等於。
        /// </summary>
        NotEqual,
        /// <summary>
        /// 小於。
        /// </summary>
        Less,
        /// <summary>
        /// 小於等於。
        /// </summary>
        LessOrEqual,
        /// <summary>
        /// 大於。
        /// </summary>
        Greater,
        /// <summary>
        /// 大於等於。
        /// </summary>
        GreaterOrEqual,
        /// <summary>
        /// 包含。
        /// </summary>
        Like,
        /// <summary>
        /// 區間。
        /// </summary>
        Between,
        /// <summary>
        /// 多選。 
        /// </summary>
        In,
        /// <summary>
        /// 排除多選。 
        /// </summary>
        NotIn,
        /// <summary>
        /// 變數。
        /// </summary>
        Variable
    }

    /// <summary>
    /// 資料庫類型。
    /// </summary>
    public enum EDatabaseType
    {
        /// <summary>
        /// SQL Server 資料庫。
        /// </summary>
        SQLServer,
        /// <summary>
        /// MySQL 資料庫。
        /// </summary>
        MySQL,
        /// <summary>
        /// Oracle 資料庫。
        /// </summary>
        Oracle,
        /// <summary>
        /// OLE DB 資料來源。
        /// </summary>
        OleDb,
        /// <summary>
        /// ODBC 資料來源。
        /// </summary>
        Odbc
    }

    /// <summary>
    /// 排序方式。
    /// </summary>
    public enum ESortDirection
    {
        /// <summary>
        /// 遞增排序。
        /// </summary>
        Ascending,
        /// <summary>
        /// 遞減排序。
        /// </summary>
        Descending
    }

    /// <summary>
    /// 系統欄位名稱常數。
    /// </summary>
    public class SysFields
    {
        /// <summary>
        /// 公司編號。
        /// </summary>
        public const string CompanyID = "SYS_CompanyID";
        /// <summary>
        /// Common 資料庫的公司編號。
        /// </summary>
        public const string CommonCompanyID = "CompanyID";
        /// <summary>
        /// 內碼編號。
        /// </summary>
        public const string ID = "SYS_ID";
        /// <summary>
        /// 外碼編號。
        /// </summary>
        public const string ViewID = "SYS_ViewID";
        /// <summary>
        /// 明細關連至主檔的編號。
        /// </summary>
        public const string MasterID = "SYS_MasterID";
        /// <summary>
        /// 資料列識別。
        /// </summary>
        public const string RowID = "SYS_RowID";
        /// <summary>
        /// 日期。
        /// </summary>
        public const string Date = "SYS_Date";
        /// <summary>
        /// 名稱。
        /// </summary>
        public const string Name = "SYS_Name";
        /// <summary>
        /// 英文名稱。
        /// </summary>
        public const string EngName = "SYS_EngName";
        /// <summary>
        /// 新增日期。
        /// </summary>
        public const string InsertDate = "INS_DAT";
        /// <summary>
        /// 新增用戶帳號。
        /// </summary>
        public const string InsertUser = "INS_USR";
        /// <summary>
        /// 新增用戶部門內碼。
        /// </summary>
        public const string InsertDepartmentID = "INS_DepartmentID";
        /// <summary>
        /// 異動日期。
        /// </summary>
        public const string UpdateDate = "UPD_DAT";
        /// <summary>
        /// 異動用戶帳號。
        /// </summary>
        public const string UpdateUser = "UPD_USR";
        /// <summary>
        /// 異動用戶部門內碼。
        /// </summary>
        public const string UpdateDepartmentID = "UPD_DepartmentID";
        /// <summary>
        /// 隸屬部門。
        /// </summary>
        public const string DepartmentID = "SYS_DepartmentID";
        /// <summary>
        /// 選取欄位。
        /// </summary>
        public const string Selected = "SYS_SELECTED";
        /// <summary>
        /// 流程實體識別。
        /// </summary>
        public const string FlowInstanceID = "SYS_FlowInstanceID";
        /// <summary>
        /// 表單單況。
        /// </summary>
        public const string FormStatus = "SYS_FlowFormStatus";
        /// <summary>
        /// 撤銷送審中。
        /// </summary>
        public const string FlowRevoke = "SYS_FlowRevoke";
        /// <summary>
        /// 單況燈號。
        /// </summary>
        public const string FlowLightSignal = "SYS_FlowLightSignal";
        /// <summary>
        /// 附件表單識別。
        /// </summary>
        public const string FormAttachID = "SYS_FormAttachID";
        /// <summary>
        /// 資料有效日期區間起始日。
        /// </summary>
        public const string BeginDate = "SYS_BeginDate";
        /// <summary>
        /// 資料有效日期區間終止日。
        /// </summary>
        public const string EndDate = "SYS_EndDate";
        /// <summary>
        /// 資料列欄號。
        /// </summary>
        public const string NO = "SYS_NO";
        /// <summary>
        /// 表單圖片。
        /// </summary>
        public const string Image = "SYS_Image";
        /// <summary>
        /// 表單圖片網址。
        /// </summary>
        public const string ImageUrl = "SYS_ImageUrl";
        /// <summary>
        /// 是否有該筆資料的編輯權限，虛擬欄位。
        /// </summary>
        public const string EditPermission = "SYS_EditPermission";
        /// <summary>
        /// 是否有該筆資料的刪除權限，虛擬欄位。
        /// </summary>
        public const string DeletePermission = "SYS_DeletePermission";
    }

    /// <summary>
    /// 系統設定檔案名稱
    /// </summary>
    public class SysDefineSettingName
    {
        /// <summary>
        /// 設定檔名稱
        /// </summary>
        public const string Setting = "setting.json";
        /// <summary>
        /// 定義檔存放路徑
        /// </summary>
        public const string APP_Data = "APP_Data";
        /// <summary>
        /// 資料庫連線定義檔案名稱
        /// </summary>
        public const string DbSettingName = "DatabaseSettings.json";
        /// <summary>
        /// 公司項目定義檔案名稱
        /// </summary>
        public const string CompanySettingName = "CompanySettings.json";
        /// <summary>
        /// 程式設定檔案名稱
        /// </summary>
        public const string ProgramSettingName = "ProgramSetting.json";
        /// <summary>
        /// 程式定義檔案名稱
        /// </summary>
        public const string ProgramDefineName = "ProgramDefine.json";
        /// <summary>
        /// 資料庫定義檔案名稱
        /// </summary>
        public const string DbTableDefineName = "DbTableDefine.json";
        /// <summary>
        /// 資料庫連線定義檔案路徑
        /// </summary>
        public static string DbSettingPath => FileFunc.PathCombine(BaseInfo.AppDataPath, DbSettingName);
        /// <summary>
        /// 資料庫連線定義檔案路徑
        /// </summary>
        public static string CompanySettingPath => FileFunc.PathCombine(BaseInfo.AppDataPath, CompanySettingName);
        /// <summary>
        /// 系統資料夾
        /// </summary>
        public static string SystemPath => FileFunc.PathCombine(BaseInfo.AppDataPath, "Common");
        /// <summary>
        /// 程式設定檔案路徑
        /// </summary>        
        /// <returns></returns>
        public static string ProgramSettingFilePath => FileFunc.PathCombine(SystemPath, $@"{ProgramSettingName}");
        /// <summary>
        /// 程式定義檔案路徑
        /// </summary>        
        /// <returns></returns>
        public static string ProgramDefinePath => FileFunc.PathCombine(SystemPath, $@"ProgramDefine");
        /// <summary>
        /// 程式定義檔案路徑
        /// </summary>        
        /// <param name="progId">功能定義</param>
        /// <returns></returns>
        public static string ProgramDefineFilePath(string progId) => FileFunc.PathCombine(ProgramDefinePath, $@"{progId}.{ProgramDefineName}");
        /// <summary>
        /// 資料表定義檔案路徑
        /// </summary>        
        /// <returns></returns>
        public static string DbTableDefinePath()
            => FileFunc.PathCombine(SystemPath, $@"DbTableDefine");
        /// <summary>
        /// 資料表定義檔案路徑
        /// </summary>        
        /// <param name="progId">功能定義</param>
        /// <returns></returns>
        public static string DbTableDefineFilePath(string progId)
            => FileFunc.PathCombine(DbTableDefinePath(), $@"{progId}.{DbTableDefineName}");
    }

    /// <summary>
    /// 變數類型。
    /// </summary>
    public enum EVariableType
    {
        /// <summary>
        /// 系統變數，格式為 [@@Variable]。
        /// </summary>
        Variable,
        /// <summary>
        /// 欄位變數，格式為 [@Table.FieldName]。
        /// </summary>
        TableField,
        /// <summary>
        /// 欄位變數，格式為 [@FieldName]。
        /// </summary>
        Field,
        /// <summary>
        /// 常數，非變數。
        /// </summary>
        Constant
    }

    /// <summary>
    /// 系統變數。
    /// </summary>
    public enum ESysVariable
    {
        /// <summary>
        /// 公司編號。
        /// </summary>
        CompanyID,
        /// <summary>
        /// 部門編號(。
        /// </summary>
        DepartmentID,
        /// <summary>
        /// 員工編號。
        /// </summary>
        EmployeeID,
        /// <summary>
        /// 用戶帳號。
        /// </summary>
        UserID,
        /// <summary>
        /// 當年。
        /// </summary>
        ThisYear,
        /// <summary>
        /// 當月。
        /// </summary>
        ThisMonth,
        /// <summary>
        /// 當週。
        /// </summary>
        ThisWeek,
        /// <summary>
        /// 當天。
        /// </summary>
        Today
    }

    /// <summary>
    /// Entity 資料列狀態
    /// </summary>
    public enum EEntityRowState
    {
        /// <summary>
        /// 新增
        /// </summary>
        Added = 0,
        /// <summary>
        /// 刪除
        /// </summary>
        Deleted = 1,
        /// <summary>
        /// 修改
        /// </summary>
        Modified = 2,
        /// <summary>
        /// 未變更
        /// </summary>
        Unchanged = 3
    }
}
