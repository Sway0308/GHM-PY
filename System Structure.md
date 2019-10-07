# 系統架構 

## **Gat.Base**
>基底資料類別庫，包含共用函式、強型別Collection類別
>StrFunc, FileFunc, DateFunc, JsonFunc...

## **Gat.Define**
>定義類別庫

### 功能定義
1. ProgramDefine：功能定義
	* ProgId：程式代碼
	* DisplayName
	* Description
2. TableDefine：資料表定義
	* TableName：表頭的TableName=ProgId
	* DbTableName：實際的資料表名稱
	* PrimaryKey：用逗號分隔
	* MasterKey
	* Indexes
	* FilterItems：語法過濾條件
		* FieldName
		* FieldValue		
		* ComparisonOperator：比較運算子
4. FieldDefine：欄位定義
	* FieldName
	* DbFieldName：預設等於FieldName
	* FieldType
	* Length
	* IsAllowNull
	* DbDefaultValue
	---
	* DefaultValue
	* IsRequired
	* FieldType：資料欄位/關聯欄位/虛擬欄位
	* LinkProgId：關聯程式代碼
	* LinkReturnFields：SourceField, DestField
5. DbTableDefine：資料表升級定義，由TableDefine而來
6. ~~FormLayout：表單布局定義，由ProgramDefine > TableDefine > FieldDefine而來~~
7. ~~ReportDefine：報表定義~~

### 系統定義
* ApplicationSetting(系統設定)
* CompanySettings(公司設定)
  * CompanyItem
* DatabaseSettings(資料庫設定)
  * DatabaseItme
* ProgramSetting(功能設定)
  * ProgramItem：用ProgId跟ProgramDefine對應
* ~~Menu功能~~

## **Gat.Cache**
>快取資料類別庫(*MemoryCache*)

1. 定義快取
2. 系統資料快取：角色，權限，Token，基本資料(*ex：Employee, Depart*)

## **Gat.Database**
>資料庫連接類別庫，CRUD語法透過定義設定跟DataTable狀態自動產生對應語法

## **Gat.Business**
>底層商業邏輯類別庫(*使用 **參數式物件**，避免參數頻繁異動導致修改成本增加*)

1. Add：按下新增時觸發的事件，回傳透過定義而產生的資料集
	* DoBeforeAdd
	* DoAdd
	* DoAfterAdd

2. Edit：按下修改時觸發的事件
	* DoBeforeEdit
	* DoEdit
	* DoAfterEdit

3. Save：按下儲存時觸發的事件
	* DoBeforeSave
	* DoSave
	* DoAfterSave

4. Delete：按下刪除時觸發的事件
	* DoBeforeDelete
	* DoDelete
	* DoAfterDelete

5. Move：單筆資料取得時觸發的事件
	* DoBeforeMove
	* DoMove
	* DoAfterMove

6. Find：清單資料取得時觸發的事件
	* DoBeforeFind
	* DoFind
	* DoAfterFind

7. Select：單筆 & 清單取得時，觸發的事件
	* DoBeforeSelect
	* DoSelect
	* DoAfterSelect

8. FieldValueChange：欄位值異動時所觸發的事件，可透過定義決定是否呼叫
9. ExecFunc：執行自訂函式 

BusinessFunc：機制提供各功能資料取得(**Move, Find, Select**)
UpdateItems：異動資料集合
	* UpdateField
	* UpdateControl


## **Gat.Setting**
>設定匯入，資料表升級類別庫

## **第三方元件**
> Newtonsoft.Json
> Dapper

## **`Gat.BusinessLogic`.PY**
> PY商業邏輯撰寫

繼承BusinessLogic，只注重在Before/After等Override函式邏輯，以及FieldValudChange & ExecFunc

