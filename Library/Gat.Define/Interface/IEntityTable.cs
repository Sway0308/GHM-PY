using System;

namespace Gat.Define
{
    [Obsolete]
    public interface IEntityTable
    {
        EntityRowCollection Rows { get; }
        string TableName { get; set; }
    }
}