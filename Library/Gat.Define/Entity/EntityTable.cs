using Gat.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gat.Define
{
    /// <summary>
    /// Entity 資料表(指定型別)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Obsolete]
    public class EntityTable<T> : EntityTable where T : EntityRow
    {
        /// <summary>
        /// 建構函式
        /// </summary>
        /// <param name="entityTable"></param>
        public EntityTable(EntityTable entityTable, InstanceType instanceType) : base()
        {
            this.TableName = entityTable.TableName;
            this.OriginalEntityTable = entityTable;
            ConvertToEntityRow(instanceType);
        }

        /// <summary>
        /// Entity 資料列集合
        /// </summary>
        public Dictionary<T, EntityRow> EntityRows { get; } = new Dictionary<T, EntityRow>();

        /// <summary>
        /// 原始 Entity 資料表
        /// </summary>
        public EntityTable OriginalEntityTable { get; }

        /// <summary>
        /// 轉換為Entity資料列
        /// </summary>
        private void ConvertToEntityRow(InstanceType instanceType)
        {
            foreach (EntityRow entity in OriginalEntityTable.Rows)
            {
                var realEntity = (T)DefineFunc.CreateEntityRow(instanceType);
                this.EntityRows.Add(entity.ToRealEntity(realEntity), entity);
            }
        }
    }

    /// <summary>
    /// Entity 資料表
    /// </summary>
    public class EntityTable : KeyCollectionItem, IEntityTable
    {
        /// <summary>
        /// 建構函式
        /// </summary>
        public EntityTable()
        { }

        /// <summary>
        /// 建構函式
        /// </summary>
        /// <param name="tableName">資料表名稱</param>
        public EntityTable(string tableName) : base()
        {
            this.TableName = tableName;
        }

        /// <summary>
        /// 建構函式
        /// </summary>
        /// <param name="table">資料表</param>
        public EntityTable(DataTable table) : base()
        {
            this.TableName = table.TableName;
            ConvertTableToEntity(table);
        }

        /// <summary>
        /// 資料表名稱
        /// </summary>
        public string TableName { get => base.Key; set => base.Key = value; }

        /// <summary>
        /// 資料列集合
        /// </summary>
        public EntityRowCollection Rows { get; } = new EntityRowCollection();

        /// <summary>
        /// 將資料表轉為實體資料列集合
        /// </summary>
        /// <param name="table"></param>
        public void ConvertTableToEntity(DataTable table)
        {
            foreach (DataRow row in table.Rows)
            {
                var entity = new EntityRow(table.Columns);
                foreach (var fieldName in entity.FieldNames)
                    entity.SetValue(fieldName, row[fieldName]);
                this.Rows.Add(entity);
            }
        }

        /// <summary>
        /// 物件描述
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("Name：{0}, Count：{1}", this.TableName, this.Rows.Count);
        }
    }
}
