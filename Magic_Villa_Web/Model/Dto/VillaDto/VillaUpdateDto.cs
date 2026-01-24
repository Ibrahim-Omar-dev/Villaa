using System.ComponentModel.DataAnnotations;

namespace Magic_Villa_Web.Models.Dto.VillaDto
{
    public class VillaUpdateDto
    {
        [Required]
        [MaxLength(30)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Details { get; set; }
        [Required]
        public double Rate { get; set; }
        [Required]
        public int Occupancy { get; set; }
        [Required]
        public int Sqft { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public string Amenity { get; set; }
    }
}
