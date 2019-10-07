using Gat.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gat.Define
{
    /// <summary>
    /// Entity 資料集(指定型別)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Obsolete]
    public class EntitySet<T> : EntitySet where T : EntityRow
    {
        /// <summary>
        /// 建構函式
        /// </summary>
        /// <param name="entitySet"></param>
        public EntitySet(EntitySet entitySet, InstanceType instanceType) : base()
        {
            this.DataSetName = entitySet.DataSetName;
            this.OriginalEntitySet = entitySet;
        }

        /// <summary>
        /// 原始Entity資料集
        /// </summary>
        private EntitySet OriginalEntitySet { get; }

        /// <summary>
        /// 資料表
        /// </summary>
        public Dictionary<EntityTable<T>, EntityTable> EntityTables { get; } = new Dictionary<EntityTable<T>, EntityTable>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tables"></param>
        private void ConvertGTables(InstanceType instanceType)
        {
            foreach (EntityTable table in this.OriginalEntitySet.Tables)
                this.EntityTables.Add(new EntityTable<T>(table, instanceType), table);
        }
    }

    /// <summary>
    /// Entity 資料集
    /// </summary>
    public class EntitySet
    {
        /// <summary>
        /// 建構函式
        /// </summary>
        public EntitySet()
        {
        }

        /// <summary>
        /// 建構函式
        /// </summary>
        /// <param name="dataSetName">資料集名稱</param>
        public EntitySet(string dataSetName) : base()
        {
            this.DataSetName = dataSetName;
        }

        /// <summary>
        /// 建構函式
        /// </summary>
        /// <param name="dataSet">資料集</param>
        public EntitySet(DataSet dataSet)
        {
            this.DataSetName = dataSet.DataSetName;
            foreach (DataTable table in dataSet.Tables)
            {
                this.Tables.Add(new EntityTable(table));
            }
        }

        /// <summary>
        /// 資料集名稱
        /// </summary>
        public string DataSetName { get; set; }
        /// <summary>
        /// 資料表集合
        /// </summary>
        public EntityTableCollection Tables { get; } = new EntityTableCollection();

        /// <summary>
        /// 物件描述
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Name：{0}, Count：{1}", this.DataSetName, this.Tables.Count);
        }
    }
}
