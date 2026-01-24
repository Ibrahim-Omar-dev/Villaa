using System.ComponentModel.DataAnnotations;

namespace Magic_Villa_Web.Models.Dto.VillaDto
{
    public class VillaDto
    {
        [Required]
        public string Name { get; set; }
        public string Details { get; set; }
        public string ImageUrl { get; set; }
        public double Rate { get; set; }
        public string SquareFeet { get; set; }
        public int Occupancy { get; set; }
        public string Amenity { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
