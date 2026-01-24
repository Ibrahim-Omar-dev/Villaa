using System;
using System.Collections.Generic;
using Magic_villa.Model;

namespace Magic_villa.Data
{
    public class StoreData
    {
        public static List<Villa> SeedData()
        {
            return new List<Villa>
            {
                new Villa
                {
                    Id = 1,
                    Name = "Royal Villa",
                    Details = "A luxurious villa with stunning views.",
                    ImageUrl = "https://example.com/royal-villa.jpg",
                    Rate = 250.00,
                    SquareFeet = "2000",
                    Occupancy = 4,
                    Amenity = "Pool, WiFi, Breakfast",
                    CreatedDate = DateTime.SpecifyKind(new DateTime(2024, 01, 01), DateTimeKind.Utc),
UpdatedDate = DateTime.SpecifyKind(new DateTime(2024, 01, 01), DateTimeKind.Utc)
                },
                new Villa
                {
                    Id = 2,
                    Name = "Premium Pool Villa",
                    Details = "Spacious villa with a private pool.",
                    ImageUrl = "https://example.com/premium-pool-villa.jpg",
                    Rate = 300.00,
                    SquareFeet = "2500",
                    Occupancy = 5,
                    Amenity = "Private Pool, Gym, WiFi",
                    CreatedDate = DateTime.SpecifyKind(new DateTime(2024, 01, 01), DateTimeKind.Utc),
UpdatedDate = DateTime.SpecifyKind(new DateTime(2024, 01, 01), DateTimeKind.Utc)
                }
            };
        }

        public static List<VillaNumber> SeedData2()
        {
            return new List<VillaNumber>
            {
                new VillaNumber
                {
                    VillaNum = 101,
                    VillaId = 1,
                    SpecialDetails = "Ocean view, 2 bedrooms",
                    CreateAt = DateTime.SpecifyKind(new DateTime(2024, 01, 01), DateTimeKind.Utc),
UpdateAt = DateTime.SpecifyKind(new DateTime(2024, 01, 01), DateTimeKind.Utc)
                },
                new VillaNumber
                {
                    VillaNum = 102,
                    VillaId = 2,
                    SpecialDetails = "Mountain view, 3 bedrooms",
                    CreateAt = DateTime.SpecifyKind(new DateTime(2024, 01, 01), DateTimeKind.Utc),
UpdateAt = DateTime.SpecifyKind(new DateTime(2024, 01, 01), DateTimeKind.Utc)
                }
            };
        }
    }
}
