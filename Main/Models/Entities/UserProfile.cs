using LC.Infrastructure.Database.Interface;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LC.Models.Entities
{
    public class UserProfile : ARecord
    {
        /// <summary>
        /// 名稱
        /// </summary>        
        [StringLength(20)]
        [Comment("名稱")]
        [Required]
        [DefaultValue("")]
        [Description("名稱")]
        public string Name { get; set; }

        /// <summary>
        /// 帳號
        /// </summary>        
        [StringLength(20)]
        [Comment("帳號")]
        [Required]
        [DefaultValue("")]
        [Description("帳號")]
        public string Account { get; set; }

        /// <summary>
        /// 密碼
        /// </summary>        
        [Column(TypeName = "nvarchar(MAX)")]
        [Comment("密碼")]
        [Required]
        [DefaultValue("")]
        [Description("密碼")]
        public string Password { get; set; }
    }
}
