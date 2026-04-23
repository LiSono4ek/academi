using MiniBlockchain.Infrastructure.Interfaces;
using MiniBlockchain.Infrastructure.Persistence.Contexts;

namespace MiniBlockchain.Infrastructure.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public IBlockRepository Blocks { get; }

    public UnitOfWork(ApplicationDbContext context, IBlockRepository blockRepository)
    {
        _context = context;
        Blocks = blockRepository;
    }

    public async Task<int> SaveChangesAsync(CancellationToken ct) => await _context.SaveChangesAsync(ct);

    public void Dispose() => _context.Dispose();
}