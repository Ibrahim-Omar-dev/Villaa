using System.ComponentModel.DataAnnotations;

namespace Magic_Villa_Web.Models.Dto.VillaDto
{
    public class VillaUpdateDto
    {
        public int Id { get; set; }
        [MaxLength(30)] 
        public string Name { get; set; }

        [MaxLength(500)] 
        public string Details { get; set; }

        [Range(0, int.MaxValue)] 
        public int Sqft { get; set; }

        [Range(1, 10)] 
        public int Occupancy { get; set; }

        [Range(0.01, 10000.00)] 
        public double Rate { get; set; }

        [MaxLength(500)] 
        public string ImageUrl { get; set; }

        [MaxLength(500)] 
        public string Amenity { get; set; }
    }
}

