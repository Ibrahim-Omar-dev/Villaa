using Magic_villa.Data;
using Magic_villa.Model;
using Magic_villa.Repository.IRepository;

namespace Magic_villa.Repository
{
    public class VillaNumberRepository : Repository<VillaNumber>, IVillaNumberRepository
    {
        private readonly AppDbContext _context;

        public VillaNumberRepository(AppDbContext context) : base(context,context.VillaNumbers)
        {
            _context = context;
        }

        public Task Update(VillaNumber entity)
        {
            _context.VillaNumbers.Update(entity);
            return Task.CompletedTask;
        }
    }
}
