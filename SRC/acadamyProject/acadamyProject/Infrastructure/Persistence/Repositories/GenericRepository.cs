using Microsoft.EntityFrameworkCore;
using MiniBlockchain.Infrastructure.Interfaces;
using MiniBlockchain.Infrastructure.Persistence.Contexts;

namespace MiniBlockchain.Infrastructure.Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly ApplicationDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T?> GetByIdAsync(Guid id, CancellationToken ct) => await _dbSet.FindAsync(new object[] { id }, ct);

    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken ct) => await _dbSet.ToListAsync(ct);

    public async Task AddAsync(T entity, CancellationToken ct) => await _dbSet.AddAsync(entity, ct);

    public void Update(T entity) => _dbSet.Update(entity);

    public void Delete(T entity) => _dbSet.Remove(entity);
}