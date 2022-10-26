using StoreBlazor.Client.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Data.Common;
using System.Linq.Expressions;
using System.Data.SqlClient;

namespace StoreBlazor.Server.Repositories
{
    public class Repository<T> : DapperRepository, IRepository<T> where T : class, IAggregateRoot
    {
        public IUnitOfWork UnitOfWork { get; set; }
        protected readonly CnDbContext CnContext;
        private IUnitOfWork _unitOfWork;
        private CnDbContext _cnContext;

        public Repository(IOptions<ConnectionSettings> connectionSettings,
                          IUnitOfWork unitOfWork,
                          CnDbContext cnContext) : base(connectionSettings)
        {
            if (connectionSettings is null)
            {
                throw new ArgumentNullException(nameof(connectionSettings));
            }

            UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            CnContext = cnContext ?? throw new ArgumentNullException(nameof(cnContext));
        }
        
        public void Add(T item)
        {
            CnContext.Set<T>().Add(item);
        }

        public Task<bool> Any(Expression<Func<T, bool>> predicate)
        {
            return CnContext.Set<T>().AnyAsync(predicate);
        }

        public virtual Task<T?> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return GetAggregateQueryable().FirstOrDefaultAsync(predicate);
        }

        public virtual Task<List<T>> GetListAsync(Expression<Func<T, bool>> predicate)
        {
            return GetAggregateQueryable().Where(predicate).ToListAsync();
        }

        public T Remove(T item)
        {
            return CnContext.Set<T>().Remove(item).Entity;
        }

        protected virtual IQueryable<T> GetAggregateQueryable()
        {
            return CnContext.Set<T>();
        }


    }

    public class DapperRepository
    {
        readonly ConnectionSettings _connectionSettings;

        public DapperRepository(IOptions<ConnectionSettings> connectionSettings)
        {
            _connectionSettings = connectionSettings.Value;
        }

        protected SqlConnection GetConnection()
        {
            var connection = new SqlConnection(_connectionSettings.DefaultConnection);
            return connection;
        }
    }
}
