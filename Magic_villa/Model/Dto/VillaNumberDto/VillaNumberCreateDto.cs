using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Magic_villa.Model.Dto.VillaNumberDto
{
    public class VillaNumberCreateDto
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VillaNum { get; set; }
        [Required]
        public int VillaId { get; set; }
        public string SpecialDetails { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
    }
}
