namespace StoreBlazor.Server.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CnDbContext _cnContext;

        public UnitOfWork(CnDbContext cnContext)
        {
            _cnContext = cnContext;
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _cnContext.SaveChangesAsync(cancellationToken);
        }
    }
}
