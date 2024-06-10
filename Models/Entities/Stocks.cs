using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LC.Models.Entities
{
    [PrimaryKey(nameof(ID))]
    public class Stocks
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Comment("流水號")]
        public int ID { get; set; }

        [Comment("個股名稱")]
        public string Name { get; set; } = default!;
    }
}