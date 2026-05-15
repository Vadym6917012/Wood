using Wood.Domain.Entities;
using Wood.Domain.Enums;

namespace Wood.Application.Common.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken ct = default);
        Task<IReadOnlyList<Product>> GetFilteredAsync(
            int? categoryId, string? search, string? sortBy, bool? inStock, CancellationToken ct = default);
        Task<IReadOnlyList<Product>> GetFeaturedAsync(CancellationToken ct = default);
        Task<Product> AddAsync(Product product, CancellationToken ct = default);
        Task UpdateAsync(Product product, CancellationToken ct = default);

    }

    public interface ICategoryRepository
    {
        Task<IReadOnlyList<Category>> GetAllAsync(CancellationToken ct = default);
        Task<Category?> GetByIdAsync(int id, CancellationToken ct = default);
    }

    public interface IOrderRepository
    {
        Task<Order?> GetByIdAsync(int id, CancellationToken ct = default);
        Task<IReadOnlyList<Order>> GetAllAsync(CancellationToken ct = default);
        Task<Order> AddAsync(Order order, CancellationToken ct = default);
        Task UpdateAsync(Order order, CancellationToken ct = default);
    }

    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken ct = default);
    }

}
