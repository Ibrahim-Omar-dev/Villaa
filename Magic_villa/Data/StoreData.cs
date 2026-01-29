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
        ImageUrl = "https://images.unsplash.com/photo-1582268611958-ebfd161ef9cf?w=800",
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
        ImageUrl = "https://images.unsplash.com/photo-1600596542815-ffad4c1539a9?w=800",
        Rate = 300.00,
        SquareFeet = "2500",
        Occupancy = 5,
        Amenity = "Private Pool, Gym, WiFi",
        CreatedDate = DateTime.SpecifyKind(new DateTime(2024, 01, 01), DateTimeKind.Utc),
        UpdatedDate = DateTime.SpecifyKind(new DateTime(2024, 01, 01), DateTimeKind.Utc)
    },
    new Villa
    {
        Id = 3,
        Name = "Diamond Villa",
        Details = "Elegant beachfront villa with panoramic ocean views.",
        ImageUrl = "https://images.unsplash.com/photo-1613490493576-7fde63acd811?w=800",
        Rate = 450.00,
        SquareFeet = "3200",
        Occupancy = 6,
        Amenity = "Beach Access, Jacuzzi, WiFi, Chef Service",
        CreatedDate = DateTime.SpecifyKind(new DateTime(2024, 01, 01), DateTimeKind.Utc),
        UpdatedDate = DateTime.SpecifyKind(new DateTime(2024, 01, 01), DateTimeKind.Utc)
    },
    new Villa
    {
        Id = 4,
        Name = "Sunset Paradise Villa",
        Details = "Modern villa with infinity pool and mountain backdrop.",
        ImageUrl = "https://images.unsplash.com/photo-1564013799919-ab600027ffc6?w=800",
        Rate = 380.00,
        SquareFeet = "2800",
        Occupancy = 5,
        Amenity = "Infinity Pool, BBQ Area, WiFi, Parking",
        CreatedDate = DateTime.SpecifyKind(new DateTime(2024, 01, 01), DateTimeKind.Utc),
        UpdatedDate = DateTime.SpecifyKind(new DateTime(2024, 01, 01), DateTimeKind.Utc)
    },
    new Villa
    {
        Id = 5,
        Name = "Garden Oasis Villa",
        Details = "Tranquil villa surrounded by lush tropical gardens.",
        ImageUrl = "https://images.unsplash.com/photo-1512917774080-9991f1c4c750?w=800",
        Rate = 320.00,
        SquareFeet = "2200",
        Occupancy = 4,
        Amenity = "Garden, Outdoor Dining, WiFi, Air Conditioning",
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
