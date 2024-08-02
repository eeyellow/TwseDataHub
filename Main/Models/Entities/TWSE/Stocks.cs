using LC.Infrastructure.Database.Interface;

namespace LC.Models.Entities
{
    /// <summary>
    /// 上市個股
    /// </summary>
    [Comment("上市個股")]
    public class Stocks : ARecord
    {
        /// <summary>
        /// 名稱
        /// </summary>        
        [StringLength(30)]
        [Comment("名稱")]
        public required string Name { get; set; } = "";

        /// <summary>
        /// 代碼
        /// </summary>        
        [StringLength(30)]
        [Comment("代碼")]
        public required string Code { get; set; } = "";
    }
}
