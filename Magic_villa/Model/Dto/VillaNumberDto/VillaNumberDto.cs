using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Magic_villa.Model.Dto.VillaDto;

namespace Magic_villa.Model.Dto.VillaNumberDto
{
    public class VillaNumberDto
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VillaNum { get; set; }
        [Required]
        public int VillaId { get; set; }
        public string SpecialDetails { get; set; }
        public VillaDto.VillaDto Villa  { get; set; }
    }
}
