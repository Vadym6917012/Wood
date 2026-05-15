using Microsoft.EntityFrameworkCore;
using Wood.Application.Common.Interfaces;
using Wood.Domain.Entities;

namespace Wood.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _db;
        public ProductRepository(AppDbContext db) => _db = db;

        public async Task<Product?> GetByIdAsync(int id, CancellationToken ct = default)
            => await _db.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id, ct);

        public async Task<IReadOnlyList<Product>> GetAllAsync(CancellationToken ct = default)
            => await _db.Products.Include(p => p.Category).ToListAsync(ct);

        public async Task<IReadOnlyList<Product>> GetFilteredAsync(
            int? categoryId, string? search, string? sortBy, bool? inStock, CancellationToken ct = default)
        {
            var q = _db.Products.Include(p => p.Category).AsQueryable();

            if ( categoryId.HasValue )
                q = q.Where(p => p.CategoryId == categoryId.Value);

            if ( !string.IsNullOrWhiteSpace(search) )
                q = q.Where(p => p.Name.Contains(search) || p.Species.Contains(search));

            if ( inStock.HasValue )
                q = q.Where(p => p.InStock == inStock.Value);

            q = sortBy switch
            {
                "price_asc" => q.OrderBy(p => p.PricePerCubicMeter),
                "price_desc" => q.OrderByDescending(p => p.PricePerCubicMeter),
                "name" => q.OrderBy(p => p.Name),
                _ => q.OrderBy(p => p.Id)
            };

            return await q.ToListAsync(ct);
        }

        public async Task<IReadOnlyList<Product>> GetFeaturedAsync(CancellationToken ct = default)
            => await _db.Products
                .Include(p => p.Category)
                .Where(p => p.IsFeatured && p.InStock)
                .ToListAsync(ct);

        public async Task<Product> AddAsync(Product product, CancellationToken ct = default)
        {
            _db.Products.Add(product);
            return product;
        }

        public async Task UpdateAsync(Product product, CancellationToken ct = default)
            => _db.Products.Update(product);
    }

    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _db;
        public CategoryRepository(AppDbContext db) => _db = db;

        public async Task<IReadOnlyList<Category>> GetAllAsync(CancellationToken ct = default)
            => await _db.Categories.Include(c => c.Products).ToListAsync(ct);

        public async Task<Category?> GetByIdAsync(int id, CancellationToken ct = default)
            => await _db.Categories.Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == id, ct);
    }

    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _db;
        public OrderRepository(AppDbContext db) => _db = db;

        public async Task<Order?> GetByIdAsync(int id, CancellationToken ct = default)
            => await _db.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.Id == id, ct);

        public async Task<IReadOnlyList<Order>> GetAllAsync(CancellationToken ct = default)
            => await _db.Orders
                .Include(o => o.Items)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync(ct);

        public async Task<Order> AddAsync(Order order, CancellationToken ct = default)
        {
            _db.Orders.Add(order);
            return order;
        }

        public async Task UpdateAsync(Order order, CancellationToken ct = default)
            => _db.Orders.Update(order);
    }

}
