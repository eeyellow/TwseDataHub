using LC.Infrastructure.Database.Interface;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LC.Models.Entities
{
    /// <summary>
    /// 村里
    /// </summary>
    [Comment("村里")]
    public class Village : ARecord
    {
        /// <summary>
        /// 名稱
        /// </summary>        
        [StringLength(20)]
        [Comment("名稱")]
        public string Name { get; set; } = "";
        
        /// <summary>
        /// 縣市代碼
        /// </summary>        
        [StringLength(20)]
        [Comment("村里代碼")]
        public required string VillageCode { get; set; } = "";

        /// <summary>
        /// 名稱
        /// </summary>        
        [StringLength(20)]
        [Comment("縣市名稱")]
        public required string CountyName { get; set; } = "";

        /// <summary>
        /// 縣市代號
        /// </summary>        
        [StringLength(5)]
        [Comment("縣市代號")]
        public required string CountyID { get; set; } = "";

        /// <summary>
        /// 縣市代碼
        /// </summary>        
        [StringLength(10)]
        [Comment("縣市代碼")]
        public required string CountyCode { get; set; } = "";

        /// <summary>
        /// 鄉鎮市區名稱
        /// </summary>        
        [StringLength(20)]
        [Comment("鄉鎮市區名稱")]
        public required string TownName { get; set; } = "";

        /// <summary>
        /// 鄉鎮市區代號
        /// </summary>        
        [StringLength(5)]
        [Comment("鄉鎮市區代號")]
        public required string TownID { get; set; } = "";

        /// <summary>
        /// 鄉鎮市區代碼
        /// </summary>        
        [StringLength(10)]
        [Comment("鄉鎮市區代碼")]
        public required string TownCode { get; set; } = "";

        /// <summary>
        /// 英文名稱
        /// </summary>        
        [StringLength(30)]
        [Comment("英文名稱")]
        public string EngName { get; set; } = "";

        /// <summary>
        /// 備註
        /// </summary>        
        [StringLength(30)]
        [Comment("備註")]
        public string Note { get; set; } = "";

        /// <summary>
        /// 座標點位
        /// </summary>
        [Column(TypeName = "geometry")]
        [Comment("座標點位")]
        public Geometry? Geom { get; set; }
    }
}
