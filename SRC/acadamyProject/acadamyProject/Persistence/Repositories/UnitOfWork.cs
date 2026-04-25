using acadamyProject.Interfaces;
using acadamyProject.Persistence.Contexts;

namespace acadamyProject.Persistence.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public IBlockRepository Blocks { get; }

    public UnitOfWork(ApplicationDbContext context, IBlockRepository blocks)
    {
        _context = context;
        Blocks = blocks;
    }

    public async Task<int> SaveChangesAsync(CancellationToken ct) => await _context.SaveChangesAsync(ct);

    public void Dispose() => _context.Dispose();
}