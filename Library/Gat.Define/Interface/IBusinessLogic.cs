using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gat.Define
{
    /// <summary>
    /// 功能層級商業邏輯介面。
    /// </summary>
    public interface IBusinessLogic
    { 
        /// <summary>
        /// 取得指定內碼的表單資料。
        /// </summary>
        /// <param name="inputArgs">傳入引數。</param>
        MoveOutputResult Move(MoveInputArgs inputArgs);

        /// <summary>
        /// 新增表單資料。
        /// </summary>
        /// <param name="inputArgs">傳入引數。</param>
        AddOutputResult Add(AddInputArgs inputArgs);

        /// <summary>
        /// 編輯表單資料。
        /// </summary>
        /// <param name="inputArgs">傳入引數。</param>
        EditOutputResult Edit(EditInputArgs inputArgs);

        /// <summary>
        /// 儲存表單資料。
        /// </summary>
        /// <param name="inputArgs">傳入引數。</param>
        SaveOutputResult Save(SaveInputArgs inputArgs);

        /// <summary>
        /// 刪除表單資料。
        /// </summary>
        /// <param name="inputArgs">傳入引數。</param>
        DeleteOutputResult Delete(DeleteInputArgs inputArgs);

        /// <summary>
        /// 查詢清單資料。
        /// </summary>
        /// <param name="inputArgs">傳入引數。</param>
        FindOutputResult Find(FindInputArgs inputArgs);

        /// <summary>
        /// 取得指定資料表符合條件的資料。
        /// </summary>
        SelectOutputResult Select(SelectInputArgs inputArgs);

        /// <summary>
        /// 執行自訂方法。
        /// </summary>
        /// <param name="inputArgs">傳入引數。</param>
        ExecFuncOutputResult ExecFunc(ExecFuncInputArgs inputArgs);
    }
}
