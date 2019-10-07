using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Data;
using Gat.Base;

namespace Gat.Define
{
    /// <summary>
    /// 項目保管器
    /// </summary>
    public class ItemKeeper
    {
        /// <summary>
        /// 建構函式
        /// </summary>
        public ItemKeeper()
        {
        }

        /// <summary>
        /// 項目儲存器
        /// </summary>
        private readonly Hashtable ItemStorage = new Hashtable();

        /// <summary>
        /// 根據鍵值取得項目
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool HasItem<T>(string key)
        {
            var keeper = GetKeeper<T>();
            return keeper.ContainsKey(key);
        }

        /// <summary>
        /// 確認是否有特定型別的快取項目
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool HasItem<T>()
        {
            var key = typeof(T).ToString();
            return (this.ItemStorage.ContainsKey(key));            
        }

        /// <summary>
        /// 根據鍵值取得項目
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetItem<T>(string key)
        {
            var keeper = GetKeeper<T>();
            return keeper[key];
        }

        /// <summary>
        /// 根據鍵值取得項目
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public T GetItem<T>(string key, Func<T> func)
        {
            if (!HasItem<T>(key))
                AddItem(key, func.Invoke());
            return GetItem<T>(key);
        }

        /// <summary>
        /// 根據鍵值新增項目
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="key"></param>
        public void AddItem<T>(string key, T value)
        {
            var keeper = this.GetKeeper<T>();
            if (keeper.ContainsKey(key))
                keeper.Remove(key);
            keeper.Add(key, value);
        }

        /// <summary>
        /// 取得項目列舉
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        private GDictionary<T> GetKeeper<T>()
        {
            var key = typeof(T).ToString();
            if (this.ItemStorage.ContainsKey(key))
            {
                var value = this.ItemStorage[key];
                if (value is GDictionary<T>)
                    return (GDictionary<T>)this.ItemStorage[key];
                else
                    this.ItemStorage.Remove(key);
            }

            var keeper = new GDictionary<T>();
            this.ItemStorage.Add(key, keeper);
            return keeper;
        }

        /// <summary>
        /// 取得指定型別的暫存項目列舉
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> GetAllItems<T>()
        {
            var keeper = GetKeeper<T>();
            return keeper.Values.ToList();
        }

        /// <summary>
        /// 根據資料表名稱判斷是否存在資料表
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public bool HasDataTable(string tableName) => HasItem<DataTable>(tableName);

        /// <summary>
        /// 根據資料表名稱取得資料表
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable GetDataTable(string tableName)
        {
            if (!HasDataTable(tableName))
                return null;
            return GetItem<DataTable>(tableName);
        }

        /// <summary>
        /// 增加資料表
        /// </summary>
        /// <param name="table"></param>
        public void AddDataTable(DataTable table)
        {
            if (table == null)
                return;
            AddItem(table.TableName, table);
        }

        /// <summary>
        /// 增加資料表
        /// </summary>
        /// <param name="progId"></param>
        /// <param name="func"></param>
        public DataTable GetDataTable(string progId, Func<DataTable> func) => GetItem(progId, func);
    }
}
