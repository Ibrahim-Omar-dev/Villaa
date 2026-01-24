using Magic_villa.Data;
using Magic_villa.Repository.IRepository;

namespace Magic_villa.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public IVillaRepository Villa { get; }
        public IVillaNumberRepository VillaNumber { get; }

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
            Villa = new VillaRepository(_context);
            VillaNumber = new VillaNumberRepository(_context);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }
    }
}
