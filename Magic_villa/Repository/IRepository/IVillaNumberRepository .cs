using Magic_villa.Model;

namespace Magic_villa.Repository.IRepository
{
    public interface IVillaNumberRepository:Repository<VillaNumber>
    {
        Task Update(VillaNumber villaNumber);
    }
}
