// Licensed to the .NET Foundation under one or more agreements.

namespace DataShareHub.Services.Common
{
    public class ModifyDataModel<T>
    {
        /// <summary>
        /// 刪除
        /// </summary>
        public List<T> WillDelete { get; set; }
        /// <summary>
        /// 新增
        /// </summary>
        public List<T> WillInsert { get; set; }
        /// <summary>
        /// 更新
        /// </summary>
        public List<T> WillUpdate { get; set; }
    }
}
