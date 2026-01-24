using Magic_villa.Data;
using Magic_villa.Model;
using Magic_villa.Repository.IRepository;


namespace Magic_villa.Repository
{
    public class VillaRepository : Repository<Villa>, IVillaRepository
    {
        private readonly AppDbContext _context;

        public VillaRepository(AppDbContext context) :base(context, context.Villas)
        {
            _context = context;
        }

        public Task Update(Villa entity)
        {
            _context.Villas.Update(entity);
            return Task.CompletedTask;
        }
    }
}
