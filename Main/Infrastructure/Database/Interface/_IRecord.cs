using LC.Infrastructure.Database.Extensions;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LC.Infrastructure.Database.Interface
{
    /// <summary>
    /// 介面 - 紀錄欄位
    /// </summary>
    public interface IRecord
    {
        /// <summary> 新增日期 </summary>
        DateTime CreateDatetime { get; set; }
        /// <summary> 更新日期 </summary>
        DateTime UpdateDatetime { get; set; }
    }

    /// <summary>
    /// 抽象 - 紀錄欄位
    /// </summary>
    public abstract class ARecord : IEntity, IDelete, IRecord
    {
        /// <inheritdoc/>
        [Key]
        [Comment("流水號")]
        public long ID { get; set; }

        /// <inheritdoc/>
        [Column(TypeName = "bit")]
        [Comment("是否刪除")]
        [SqlDefaultValue("CONVERT(bit, 0)")]
        public virtual bool IsDelete { get; set; } = false;

        /// <inheritdoc/>
        [Column(TypeName = "datetime2")]
        [Comment("新增日期")]
        [SqlDefaultValue("GETDATE()")]
        public virtual DateTime CreateDatetime { get; set; }

        /// <inheritdoc/>     
        [Column(TypeName = "datetime2")]
        [Comment("更新日期")]
        [SqlDefaultValue("GETDATE()")]
        public virtual DateTime UpdateDatetime { get; set; }
    }
}
