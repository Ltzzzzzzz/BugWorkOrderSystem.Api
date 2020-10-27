using System;
using SqlSugar;

namespace BugWorkOrderSystem.Model
{
    public class RootEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public Guid Id { get; set; }
    }
}
