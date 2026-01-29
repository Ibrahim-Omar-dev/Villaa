using Magic_villa.Model;
using Microsoft.EntityFrameworkCore;

namespace Magic_villa.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Villa>().HasData(StoreData.SeedData());
            modelBuilder.Entity<VillaNumber>().HasData(StoreData.SeedData2());

            modelBuilder.Entity<VillaNumber>()
                       .HasOne(vn => vn.Villa)
                       .WithMany()            
                       .HasForeignKey(vn => vn.VillaId);

        }
        public DbSet<Villa> Villas { get; set; }
        public DbSet<LocalUser> LocalUsers { get; set; }    
        public DbSet<VillaNumber> VillaNumbers { get; set; }
    }
}
