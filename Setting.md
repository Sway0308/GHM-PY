# 功能架構

## 基本設定

* ProgramDefine(**ProgId**)為架構核心，不同功能以ProgId互相溝通
* SQL語法透過Code Gen，參照定義檔 & 權限設定產生，因此不會包含Sum, Count, Group By等進階語法，* 有相關需求，override AfterMove/AfterFind 來達到預期的目的
* 除了報表外，不建議自己寫SQL語法(**但提供這樣的機制**)，一律透過機制函式取得不同功能資料，可直接過濾權限
* 無特殊邏輯功能，直接透過定義檔，產生CRUD功能，不須另外撰寫BL
* 一律透過BL做CRUD，不自行做Insert/Update/Delete語法
* 特殊邏輯語法，如需取得有歷史性質表單，將語法寫成store procedure
* 主鍵一律用GUID
* 欄位基本上都要有預設值(*為了不同功能使用同一個資料表的狀況*)
* 特殊狀況才允許Null存在
* SessionGuid：Token，紀錄在快取之中
* SessionInfo：登入者相關資訊，員工編號、公司編號、角色，客製代碼，公司參數...
* 定義格式：Json
* 定義儲存方式：SQL Server
* 不直接處理DataTable，一律轉型為Entity處理，處理完再Synchronize回原資料載具
* 語系：使用語系資料表
* 清單：使用清單資料表
* namespace：一律為專案名稱，簡化系統架構(**不做過多using**)

## 設定差異

|  |Scs  |New|
|--|--|--|
|ProgId  | 模組名+序列數字組成，如HUM0020100，EIP0010100 |直接以功能名稱定義，如Employee，記憶上更直覺|



## 語法結構

|欄位名稱\資料表名稱|Depart|Duty|Employee|
|--|--|--|--|
|公司代碼|SYS_CompanyID|SYS_CompanyID  |SYS_CompanyID  |
|編號|SYS_ID|SYS_ID|SYS_ID|
|代碼|SYS_ViewID|SYS_ViewID|SYS_ViewID|
|名稱|SYS_Name|SYS_Name|SYS_Name|
|部門編號|||DepartID|
|職務代碼|||DutyID|

```sql
Select 
A.SYS_CompanyID, --資料欄位
A.SYS_ID,
A.SYS_ViewID,
A.SYS_Name,
A.DepartID,
B.SYS_ViewID as Tmp_DepartID, --關聯欄位
B.SYS_Name as Tmp_DepartName,
A.DutyID,
C.SYS_ViewID as Tmp_DutyID,
C.SYS_Name as Tmp_DutyName
From Employee A
Left Join Depart B On A.DepartID = B.SYS_ID
Left Join Duty C On A.DutyID = C.SYS_ID
```

## 表單類型

  1. Basic：基本資料
  2. Form：單據
  3. Report：報表

## 表單型態

 1. Single Form：單檔
 2. Multi Form：多檔
 3. Grid Form：附屬在表單上，但不顯示在表單本身，屬於表單的延伸，以表格形式存在

## 表單基本欄位

 1. CompanyId：公司代碼
 2. SYS_ID：內碼(*Primary Key, GUID*)
 3. SYS_ViewID：外碼(*string，編號自動累加*)
 4. 新增人員，新增時間
 5. 修改人員，修改時間
 6. RowId：表身主鍵
 7. MasterId：表身關聯表頭主鍵

## 表單額外欄位

 1. SYS_Date：日期 (*單據使用*)
 2. Sys_Name：名稱 (*基本資料使用*)

## Permission(權限)

+ 使用等比數列方式記錄權限設定(1, 2, 4, 8, 16, 32)
+ 以部門為維度，判斷可視範圍，基本為新增人員/修改人員 & 表單上的員工編號為預設可視
By 角色做設定

1. Executing：是否可視/可執行
2. Add
3. Edit
4. Delete
5. Print：單據報表
6. Export：資料匯出

- 檢視範圍
- 編輯範圍
- 自訂部門範圍

## 資料載具
* DataTable

