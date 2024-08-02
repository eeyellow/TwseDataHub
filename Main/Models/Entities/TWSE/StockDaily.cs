using LC.Infrastructure.Database.Interface;

namespace LC.Models.Entities
{
    /// <summary>
    /// 上市個股當日成交資訊
    /// </summary>
    [Comment("上市個股當日成交資訊")]
    public class StockDaily : AEntity
    {
        /// <summary>
        /// 代碼
        /// </summary>        
        [StringLength(30)]
        [Comment("代碼")]
        public required string Code { get; set; } = "";

        /// <summary> 成交量 </summary>  
        [Comment("成交量")]
        public long? TradeVolume { get; set; }

        /// <summary> 成交值 </summary>  
        [Comment("成交值")]
        public long? TradeValue { get; set; }

        /// <summary> 開盤價 </summary>
        [Comment("開盤價")]
        public decimal? OpeningPrice { get; set; }

        /// <summary> 盤中最高價 </summary>
        [Comment("盤中最高價")]
        public decimal? HighestPrice { get; set; }

        /// <summary> 盤中最低價 </summary>
        [Comment("盤中最低價")]
        public decimal? LowestPrice { get; set; }

        /// <summary> 收盤價 </summary>
        [Comment("收盤價")]
        public decimal? ClosingPrice { get; set; }

        /// <summary> 漲跌價差 </summary>
        [Comment("漲跌價差")]
        public decimal? Change { get; set; }

        /// <summary> 成交筆數 </summary>
        [Comment("成交筆數")]
        public long? Transaction { get; set; }
    }
}
