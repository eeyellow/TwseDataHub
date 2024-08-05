using LC.Infrastructure.Database.Extensions;
using LC.Infrastructure.Database.Interface;

namespace LC.Models.Entities
{
    /// <summary>
    /// 上市個股當日成交資訊
    /// </summary>
    [Comment("上市個股當日成交資訊")]
    public class StockDaily : AEntity
    {
        /// <summary>代碼</summary>        
        [StringLength(30)]
        [Comment("代碼")]
        public required string Code { get; set; } = "";

        /// <summary>交易日期</summary>
        [Comment("交易日期")]
        public required DateTime TransactionDate { get; set; }

        /// <summary> 成交量 </summary>  
        [Comment("成交量")]
        public long? TradeVolume { get; set; }

        /// <summary> 成交值 </summary>  
        [Comment("成交值")]
        public long? TradeValue { get; set; }

        /// <summary> 開盤價 </summary>
        [Comment("開盤價")]
        [DecimalPrecision(18, 4)]
        public decimal? OpeningPrice { get; set; }

        /// <summary> 盤中最高價 </summary>
        [Comment("盤中最高價")]
        [DecimalPrecision(18, 4)]
        public decimal? HighestPrice { get; set; }

        /// <summary> 盤中最低價 </summary>
        [Comment("盤中最低價")]
        [DecimalPrecision(18, 4)]
        public decimal? LowestPrice { get; set; }

        /// <summary> 收盤價 </summary>
        [Comment("收盤價")]
        [DecimalPrecision(18, 4)]
        public decimal? ClosingPrice { get; set; }

        ///// <summary> 調整後收盤價 </summary>
        //[Comment("調整後收盤價")]
        //[DecimalPrecision(18, 4)]
        //public decimal? AdjustedClosingPrice { get; set; }

        /// <summary> 漲跌價差 </summary>
        [Comment("漲跌價差")]
        [DecimalPrecision(18, 4)]
        public decimal? Change { get; set; }

        /// <summary> 成交筆數 </summary>
        [Comment("成交筆數")]
        [JsonProperty("Transaction")]
        public long? TransactionCount { get; set; }
    }
}
