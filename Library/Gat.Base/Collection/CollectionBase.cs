using System;
using System.Collections;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;

namespace Gat.Base
{
    /// <summary>
    /// 強型別集合基底類別
    /// </summary>
    /// <typeparam name="T">成員類別。</typeparam>
    public abstract class CollectionBase<T> : IList<T>, ICollectionBase where T : ICollectionItem
    {
        /// <summary>
        /// 儲存物件
        /// </summary>
        public List<T> StorageList { get; } = new List<T>();

        /// <summary>
        ///  擁有者。
        /// </summary>
        public virtual object Owner { get; protected set; }

        /// <summary>
        /// 總數
        /// </summary>
        public int Count => this.StorageList.Count;

        /// <summary>
        /// 是否唯讀
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// 傳回指定鍵值的快取資料。
        /// </summary>
        /// <param name="key">鍵值。</param>
        public virtual T this[int index]
        {
            get
            {
                return this.StorageList[index];
            }
            set
            {
                this.StorageList[index] = value;
            }
        }

        /// <summary>
        /// 依索引取得成員。
        /// </summary>
        /// <param name="index">索引。</param>
        public object GetItem(int index)
        {
            return this.StorageList[index];
        }

        /// <summary>
        /// 移除成員
        /// </summary>
        /// <param name="item">成員。</param>
        public void Remove(ICollectionItem item)
        {
            this.Remove((T)item);
        }

        /// <summary>
        /// 移除成員。
        /// </summary>
        /// <param name="item"></param>
        public void Remove(T item)
        {
            OnRemove(item);
            this.StorageList.Remove(item);
        }

        /// <summary>
        /// 新增成員
        /// </summary>
        /// <param name="item"></param>
        public virtual void Add(T item)
        {
            OnAdd(item);
            item.SetCollection(this);
            this.StorageList.Add(item);
        }

        /// <summary>
        /// 清除清單資料
        /// </summary>
        public void Clear()
        {
            this.StorageList.Clear();
        }

        /// <summary>
        /// Add 事件
        /// </summary>
        /// <param name="value"></param>
        protected virtual void OnAdd(T value)
        {

        }

        /// <summary>
        /// Remove 事件
        /// </summary>
        /// <param name="value"></param>
        protected virtual void OnRemove(T value)
        {

        }

        #region 實作IEnumerable

        /// <summary>
        /// 傳入物件在集合的位置
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int IndexOf(T item)
        {
            return this.StorageList.IndexOf(item);
        }

        /// <summary>
        /// 插入傳入物件在集合的指定位置
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        public void Insert(int index, T item)
        {
            this.StorageList.Insert(index, item);
        }

        /// <summary>
        /// 移除集合的指定位置的物件
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            this.StorageList.RemoveAt(index);
        }

        /// <summary>
        /// 集合是否存在傳入物件
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(T item)
        {
            return this.StorageList.Contains(item);
        }

        /// <summary>
        /// 複製指定位置的物件至指定的陣列
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            this.StorageList.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// 移除指定物件
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool ICollection<T>.Remove(T item)
        {
            return this.StorageList.Remove(item);
        }

        /// <summary>
        /// 取得物件列舉
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return this.StorageList.GetEnumerator();
        }

        /// <summary>
        /// 取得列舉
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }
}
