namespace LC.Infrastructure.Database.Interface
{
    /// <summary>
    /// 介面 - 紀錄欄位
    /// </summary>
    public interface IEntity
    {
        /// <summary> 流水號 </summary>
        long ID { get; set; }
    }

    /// <summary>
    /// 抽象 - 紀錄欄位
    /// </summary>
    public abstract class AEntity : IEntity
    {
        /// <inheritdoc/>
        [Key]
        [Comment("流水號")]
        public long ID { get; set; }
    }
}
