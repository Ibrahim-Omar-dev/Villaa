namespace Magic_villa.Repository.IRepository
{
    public interface IUnitOfWork
    {
        public IVillaRepository Villa { get; }
        public IVillaNumberRepository VillaNumber { get; }
        public Task Save();
    }
}
