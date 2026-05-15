using Wood.Application.Common.Interfaces;

namespace Wood.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;
        public UnitOfWork(AppDbContext db) => _db = db;

        public Task<int> SaveChangesAsync(CancellationToken ct = default)
            => _db.SaveChangesAsync(ct);

        public void Dispose() => _db.Dispose();
    }

}
