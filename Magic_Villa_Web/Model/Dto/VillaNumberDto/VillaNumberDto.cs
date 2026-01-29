using Magic_Villa_Web.Models.Dto.VillaDto;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Magic_Villa_Web.Models.Dto.VillaNumberDto
{
    public class VillaNumberDto
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VillaNum { get; set; }
        [Required]
        public int VillaId { get; set; }
        public string SpecialDetails { get; set; }
        public VillaDto.VillaDto Villa { get; set; }
    }
}
