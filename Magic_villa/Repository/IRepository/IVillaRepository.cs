using Magic_villa.Model;

namespace Magic_villa.Repository.IRepository
{
    public interface IVillaRepository : Repository<Villa>
    {
        Task Update(Villa villa);
    }
}
