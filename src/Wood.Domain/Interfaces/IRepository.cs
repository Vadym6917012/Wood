using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wood.Domain.Common;

namespace Wood.Domain.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<IReadOnlyList<T>> GetAllAsync(CancellationToken ct = default);
        Task<T> AddAsync(T entity, CancellationToken ct = default);
        Task UpdateAsync(T entity, CancellationToken ct = default);
        Task DeleteAsync(T entity, CancellationToken ct = default);

    }

    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }
}
