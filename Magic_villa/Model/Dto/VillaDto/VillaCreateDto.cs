using System.ComponentModel.DataAnnotations;

namespace Magic_villa.Model.Dto.VillaDto
{
    public class VillaCreateDto
    {
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public string? Details { get; set; }
        public string? ImageUrl { get; set; }
        public double Rate { get; set; }
        public string SquareFeet { get; set; }
        public int Occupancy { get; set; }
        public string? Amenity { get; set; }
    }
}
